/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 02/13/15
 * Time: 08:42
 * 
 * Description:  Service that manages the CRM information.
 *********************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using SOS.Data.SosCrm;
using SOS.Data.SosCrm.ControllerExtensions;
using SOS.FOS.CellStation.AlarmCom;
using SOS.FOS.MonitoringStationServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Helper;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.Reporting;
using SOS.FunctionalServices.Models;
using SOS.FunctionalServices.Models.Reporting;
using SOS.Lib.Core.ErrorHandling;
using SOS.Lib.Core.ExceptionHandling;

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
				GetCellularDeviceInfo(msAccount, csStatustList);

				// ** Setup Return
				result.Code = BaseErrorCodes.ErrorCodes.Success.Code();
				result.Message = BaseErrorCodes.ErrorCodes.Success.Message();
				result.Value = csStatustList;

			}
				#endregion TRY

				#region CATCH

			catch (NXSResultException<bool> nxsex)
			{
				result = new FnsResult<List<IFnsMsAccountOnlineStatusInfo>>
				{
					Code = nxsex.NXSResult.Code,
					Message = nxsex.NXSResult.Message
				};
			}
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsMsAccountOnlineStatusInfo>>
				{
					Code = (int) ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
				};
			}

			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<List<IFnsMsAccountCreditsAndInstalls>> GetCreditAndInstallsByOfficeIdAndRepId(int? officeId, string salesRepId, string gpEmployeeId, DateTime startDate, DateTime endDate)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "FNS-GetCreditAndInstallsByOfficeIdAndRepId";
			var result = new FnsResult<List<IFnsMsAccountCreditsAndInstalls>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				var rawResult = SosCrmDataContext.Instance.MS_AccountCreditsAndInstallsViews.GetByOfficeIdAndRepId(officeId, salesRepId, startDate, endDate, gpEmployeeId);
				var valueResult = rawResult.Select(item => new FnsMsAccountCreditsAndInstalls(item)).ToList();


				// ** Setup Return
				result.Code = BaseErrorCodes.ErrorCodes.Success.Code();
				result.Message = BaseErrorCodes.ErrorCodes.Success.Message();
				result.Value = new List<IFnsMsAccountCreditsAndInstalls>(valueResult);

			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsMsAccountCreditsAndInstalls>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		#region #Central Station information

		private List<IFnsMsAccountOnlineStatusInfo> GetCentralStationInfo(MS_Account msAccount, string gpEmployeeId)
		{
			#region Initialize
			// ** Initialize
			bool shellAccount;
			var msChoice = Main.GetMsChoice(msAccount.IndustryAccount.ReceiverLine, out shellAccount);

			#endregion Initialize

			#region Build result

			#region Get Central Station Status

			var msXmlService = new Main(msChoice);
			var status = msXmlService.ServiceStatus(msAccount.AccountID, gpEmployeeId);

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
			{
				string statusGroup, text;
				if (status.Value == null)
				{
					statusGroup = "Unknown";
					text = "Monitoring: Unknown | On Test: Unknown";
				}
				else
				{
					if (!status.Value.InService)
						statusGroup = "Critical";
					else if (status.Value.OnTest)
						statusGroup = "Warning";
					else
						statusGroup = "Good";
					text = string.Format("Monitoring: {0} | On Test: {1}", status.Value.InService ? "On Line" : "Offline", status.Value.OnTest ? "Yes" : "No");
				}
				resultList.Add(new FnsMsAccountOnlineStatusInfo
				{
					KeyName = "Monitoring Status",
					Text = text,
					Status = statusGroup,
					Value = text,
				});
			}
			#endregion Get Central Station Status

			#endregion Build result

			// ** Return list
			return resultList;
		}

		// ReSharper disable once UnusedMethodReturnValue.Local
		private void GetCellularDeviceInfo(MS_Account msAccount, List<IFnsMsAccountOnlineStatusInfo> resultList)
		{
			#region Get Cellular Device Status

			var cellServices = new CellStationService();
			var cellStation = cellServices.GetStation(msAccount.AccountID);
			if (cellStation.Failure)
			{
				resultList.Add(new FnsMsAccountOnlineStatusInfo
				{
					KeyName = "Cellular Provider: Unknown",
					Text = cellStation.Message,
					Status = "Unknown",
					Value = cellStation.Message,
				});
				return;
			}

			var cellDevStat = new AlarmComDeviceStatus(cellStation.Value.Account);
			cellDevStat.RetrieveDeviceStatus(cellStation.Value.Account);

			// TODO:  ANDRES
			// ** Add result to list.
			resultList.Add(new FnsMsAccountOnlineStatusInfo
			{
				KeyName = string.Format("Cellular Provider: {0}", "Alarm.Com"),
				Text = string.Format("Cellular Vendor: {0}", "Alarm.Com"),
				Status = "Good",
				Value = string.Format("Cellular Vendor: {0}", "Alarm.Com"),
			});

			resultList.Add(new FnsMsAccountOnlineStatusInfo
			{
				KeyName = string.Format("Device Status: {0}", cellDevStat.IsRegistered ? "Registered" : "Unregistered"),
				Text = string.Format("Device: {0}", cellDevStat.IsRegistered),
				Status = cellDevStat.IsRegistered ? "Good" : "Warning",
				Value = string.Format("Serial Number: {0}", cellDevStat.ModemSerial),
			});

			#endregion Get Cellular Device Status
		}

		#endregion #Central Station information
	}
}
