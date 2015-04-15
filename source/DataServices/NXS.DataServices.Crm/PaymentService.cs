using NXS.Data;
using NXS.Data.Crm;
using NXS.DataServices.Crm.Models;
using SOS.Lib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NXS.DataServices.Crm
{
	public class PaymentService
	{
		string _gpEmployeeId;
		public PaymentService(string gpEmployeeId)
		{
			_gpEmployeeId = gpEmployeeId;
		}

		public async Task<Result<AePaymentMethod>> AccountPaymentMethod(long accountId, bool isInitial)
		{
			using (var db = CrmDb.Connect())
			{
				var result = new Result<AePaymentMethod>();

				var msi = await db.MS_AccountSalesInformations.ByIdAsync(accountId).ConfigureAwait(false);
				if (msi == null)
					return result.Fail(-1, "Invalid Account Sales Information ID");
				var paymentMethodId = isInitial ? msi.InitialPaymentMethodId : msi.PaymentMethodId;
				if (!paymentMethodId.HasValue)
					return result;

				var tbl = db.AE_PaymentMethods;
				var item = (await tbl.ByIdAsync(paymentMethodId.Value).ConfigureAwait(false));
				result.Value = AePaymentMethod.FromDb(item);
				return result;
			}
		}

		public async Task<Result<AePaymentMethod>> SavePaymentMethod(long accountId, bool isInitial, AePaymentMethod inputItem)
		{
			using (var db = CrmDb.Connect())
			{
				var result = new Result<AePaymentMethod>();
				AE_PaymentMethod item = null;

				await db.TransactionAsync(async () =>
				{
					var msi = await db.MS_AccountSalesInformations.ByIdAsync(accountId).ConfigureAwait(false);
					if (msi == null)
					{
						result.Fail(-1, "Invalid Account Sales Information ID");
						return false;
					}

					var tbl = db.AE_PaymentMethods;
					if (inputItem.ID <= 0)
					{
						// create new
						item = new AE_PaymentMethod();
						inputItem.ToDb(item);
						await tbl.InsertAsync(item, _gpEmployeeId);
					}
					else
					{
						item = await tbl.ByIdAsync(inputItem.ID).ConfigureAwait(false);
						if (item == null)
						{
							result.Fail(-1, "Invalid PaymentMethod ID");
							return false;
						}
						// check ModifiedOn matches input
						if (!string.IsNullOrEmpty((result.Message = VersionException.ModifiedOnErrMsg(item.ModifiedOn, inputItem.ModifiedOn))))
						{
							result.Fail(-1, "PaymentMethod(" + inputItem.ID + "): " + result.Message);
							return false;
						}

						// update item
						var snapShot = Snapshotter.Start(item);
						inputItem.ToDb(item);
						await tbl.UpdateAsync(snapShot, _gpEmployeeId).ConfigureAwait(false);
					}

					var paymentMethodId = isInitial ? msi.InitialPaymentMethodId : msi.PaymentMethodId;
					if (paymentMethodId != item.ID)
					{
						var snapShot = Snapshotter.Start(msi);
						if (isInitial)
							msi.InitialPaymentMethodId = item.ID;
						else
							msi.PaymentMethodId = item.ID;
						await db.MS_AccountSalesInformations.UpdateAsync(snapShot, _gpEmployeeId).ConfigureAwait(false);
					}

					// commit transaction
					return true;
				}).ConfigureAwait(false);

				// return payment method
				result.Value = AePaymentMethod.FromDb(item);
				return result;
			}
		}
	}
}
