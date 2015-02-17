/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 02/13/15
 * Time: 08:42
 * 
 * Description:  Service that manages the CRM information.
 *********************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Activation;
using SOS.Data.SosCrm;
using SOS.FOS.MonitoringStationServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Helper;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.Reporting;
using SOS.FunctionalServices.Models;
using SOS.FunctionalServices.Models.Reporting;
using SOS.Lib.Core.ErrorHandling;

namespace SOS.FunctionalServices
{
	[ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.NotAllowed)]
	public class ReportingService : IReportingService
	{
		public IFnsResult<List<IFnsMsAccountOnlineStatusInfo>> GetAccountOnlineStatusInfoByAccountId(long accountId, string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "FNS-GetAccountOnlineStatusInfoByAccountId";
			var result = new FnsResult<List<IFnsMsAccountOnlineStatusInfo>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{

				var msAccount = SosCrmDataContext.Instance.MS_Accounts.LoadByPrimaryKey(accountId);
				var csStatustList = GetCentralStationInfo(msAccount, gpEmployeeId);

				// ** Setup Return
				result.Code = BaseErrorCodes.ErrorCodes.Success.Code();
				result.Message = BaseErrorCodes.ErrorCodes.Success.Message();
				result.Value = csStatustList;

			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsMsAccountOnlineStatusInfo>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		#region #Central Statio information

		private List<IFnsMsAccountOnlineStatusInfo> GetCentralStationInfo(MS_Account msAccount, string gpEmployeeId)
		{
			#region Initialize
			// ** Initialize
			bool shellAccount;
			var msChoice = Main.GetMsChoice(msAccount.IndustryAccount.ReceiverLine, out shellAccount);

			var msXmlService = new Main(msChoice);
			var status = msXmlService.ServiceStatus(msAccount.AccountID, gpEmployeeId);

			#endregion Initialize

			#region Build result

			var resultList = new List<IFnsMsAccountOnlineStatusInfo>();
			var centralStation = msAccount.IndustryAccount.ReceiverLine.MonitoringStationOS.MonitoringStation.MonitoringStationName;
			var msAccountSalesInformation = SosCrmDataContext.Instance.MS_AccountSalesInformations.LoadByPrimaryKey(msAccount.AccountID);
			resultList.Add(new FnsMsAccountOnlineStatusInfo
			{
				KeyName = "Central Station",
				Text = "Central Station: " + centralStation,
				Status = "Good",
				Value = string.Format("Online Date: {0} | {1} Confirmation #: {2}", msAccountSalesInformation.SubmittedToCSDate, centralStation, msAccountSalesInformation.CsConfirmationNumber)
			});

			// ** Monitoring Station Status
			var statusGroup = "Good";
			if (!status.Value.InService) 
				statusGroup = "Critical";
			else if (status.Value.OnTest)
				statusGroup = "Warning";
			resultList.Add(new FnsMsAccountOnlineStatusInfo
			{
				KeyName = "Monitoring Status",
				Text = string.Format("Monitoring: {0} | On Test: {1}", status.Value.InService ? "On Line" : "Offline", status.Value.OnTest ? "Yes" : "No"),
				Status = statusGroup,
				Value = string.Format("Monitoring: {0} | On Test: {1}", status.Value.InService ? "On Line" : "Offline", status.Value.OnTest ? "Yes" : "No"),
			});

			#endregion Build result

			// ** Return list
			return resultList;
		}

		#endregion #Central Statio information
	}
}
