using NXS.Data;
using NXS.Data.AuthenticationControl;
using NXS.DataServices.AuthenticationControl.Models;
using SOS.Lib.Core;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security;

namespace NXS.DataServices.AuthenticationControl
{
	public class ActionRequestsService
	{
		string _gpEmployeeId;
		public ActionRequestsService(string gpEmployeeId)
		{
			_gpEmployeeId = gpEmployeeId;
		}

		public async Task<Result<AcActionRequest>> CreateActionRequestAsync(AcActionRequestNew inputItem)//, )Func<byte[],string> authNumToKey)
		{
			using (var db = DBase.Connect())
			{
				var result = new Result<AcActionRequest>();
				var tbl = db.AC_ActionRequests;

				var item = new AC_ActionRequest();
				inputItem.ToDb(item);

				await AutoSignSync(tbl, item);

				await tbl.InsertAsync(item, _gpEmployeeId).ConfigureAwait(false);

				result.Value = ToApi(item);
				return result;
			}
		}

		public AcActionRequest Use(string actionKey)
		{
			using (var db = DBase.Connect())
			{
				AC_ActionRequest item = null;
				db.Transaction(() =>
				{
					var tbl = db.AC_ActionRequests;
					item = tbl.ByActionKeyWithUpdateLock(actionKey);
					if (item == null)
						return false;
					var status = GetStatus(item);
					if (status != AcActionRequest.Statuses.Approved)
					{
						string msg;
						switch (status)
						{
							case AcActionRequest.Statuses.Pending: msg = "Action Request is still pending"; break;
							case AcActionRequest.Statuses.Denied: msg = "Action Request was denied"; break;
							case AcActionRequest.Statuses.Expired: msg = "Action Request has expired"; break;
							case AcActionRequest.Statuses.Used: msg = "Action Request has already been used."; break;
							default: msg = "Invalid Action Request Status"; break;
						}
						throw new ResultException(-1, msg);
					}

					var snapshot = Snapshotter.Start(item);
					item.UsedOn = DateTime.UtcNow;
					//item.ActionKey = null; //@REVIEW: null out ActionKey on use???
					tbl.Update(snapshot, _gpEmployeeId);

					return true;
				});
				return ToApi(item, true);
			}
		}

		private static AcActionRequest ToApi(AC_ActionRequest item, bool nullable = false)
		{
			if (item == null)
			{
				if (nullable)
					return null;
				else
					throw new Exception("action request item is null");
			}
			return AcActionRequest.FromDb(item, GetStatus(item));
		}

		private static string GetStatus(AC_ActionRequest item)
		{
			string status;
			if (item.UsedOn.HasValue)
				status = "used";
			else if (item.DeniedReasonId.HasValue)
				status = "denied";
			else if (item.SignedOn.HasValue)
				status = (true) ? "approved" : "expired"; //@REVIEW: expiration
			else
				status = "pending";

			return status;
		}

		private static readonly string SystemAutoSigner = "SYSTEMAUTOSIGNER";

		private static async Task AutoSignSync(DBase.AC_ActionRequestTable tbl, AC_ActionRequest item)
		{
			//
			if (item.ActionId == AC_Action.MetaData.CRM_ByPassCredit_RepExceptionID &&
				item.RequestReasonId == (int)AC_RequestReason.IDEnum.Rep_Exception_By_Pass_Credit)
			{
				item.SignedOn = DateTime.UtcNow;
				item.SignedBy = SystemAutoSigner;

				// by pass credit
				var startRange = SOS.Lib.Util.DateUtility.GetMonthStart(DateTime.UtcNow.Date);
				startRange = SOS.Lib.Util.DateUtility.SpecifyUtcKind(startRange);
				var endRange = startRange.AddMonths(1).AddMilliseconds(-3);
				var firstResult = (await tbl.SearchByOnBehalfOfAsync(1, item.OnBehalfOf, item.RequestReasonId, startRange, endRange).ConfigureAwait(false)).FirstOrDefault();
				if (firstResult != null)
				{
					item.DeniedReasonId = (int)AC_DeniedReason.IDEnum.Surpassed_Quota;
					item.DeniedReason = "Reps are allowed only one exception per month to bypass running credit on an account. " +
						item.OnBehalfOf + " has already used this allowance.";
				}
			}
			else
			{
				item.SignedOn = DateTime.UtcNow;
				item.SignedBy = SystemAutoSigner;

				item.DeniedReasonId = (int)AC_DeniedReason.IDEnum.Surpassed_Quota;
				item.DeniedReason = "Request Not Supported: Invalid Action or Request Reason.";
			}
		}
	}
}
