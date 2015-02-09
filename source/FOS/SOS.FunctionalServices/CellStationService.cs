using SOS.Data.SosCrm;
using SOS.Data.SosCrm.ControllerExtensions;
using SOS.FOS.CellStation;
using SOS.FOS.CellStation.AlarmCom;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Models;
using SOS.Lib.Core;
using SSE.Lib.Interfaces.FOS;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace SOS.FunctionalServices
{
	//// ReSharper disable once InconsistentNaming
	//public static class IFosResultExtensions
	//{
	//	public static IFnsResult ToFnsResult(this IFosResult fos)
	//	{
	//		return new FnsResult
	//		{
	//			Code = fos.Code,
	//			Message = fos.Message,
	//			Value = fos.GetValue(),
	//		};
	//	}
	//}

	[ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.NotAllowed)]
	public class CellStationService : ICellStationService
	{

		#region .ctor
		#endregion .ctor

		#region Properties

		private const string _ALARMCOM = "ALARMCOM";
		//private const string _ALARMMET = "ALARMNET";
		//private const string _TELGAURD = "TELGUARD";

		#endregion Properties

		#region Methods
		private Result<AlarmComStation> GetStation(long accountID)
		{
			var context = SosCrmDataContext.Instance;
			var msAccount = context.MS_Accounts.LoadByPrimaryKey(accountID);
			var msInduAct = msAccount.IndustryAccount;
			var msVndrADC = context.MS_VendorAlarmComAccounts.GetByAccountId(accountID);
			var aePriCust = context.AE_Customers.GetByAccountID(accountID, "PRI");

			return new CellularStationFactory().GetAlarmComStation(
				msAccount,
				aePriCust,
				msVndrADC,
				msInduAct);
		}

		public Result<object> Register(long accountID, string serialNumber, bool enableTwoWay)
		{
			var stationResult = GetStation(accountID);
			if (stationResult.Code != 0)
			{
				return new Result<object>(stationResult.Code, stationResult.Message);
			}
			// get and return account status
			var result = stationResult.Value.CreateAccount(serialNumber, enableTwoWay);
			return new Result<object>(result.Code, result.Message, result.Value);
		}

		public Result<object> GetEquipmentList(long accountID)
		{
			var stationResult = GetStation(accountID);
			if (stationResult.Code != 0)
			{
				return new Result<object>(stationResult.Code, stationResult.Message);
			}
			// tell alarm.com to get equipment from panel
			var requestEquipmentResult = stationResult.Value.RequestEquipmentList(true);
			if (requestEquipmentResult.Code != 0)
			{
				return new Result<object>(requestEquipmentResult.Code, requestEquipmentResult.Message);
			}
			// get equipment from alarm.com
			var result = stationResult.Value.GetEquipmentList();
			return new Result<object>(result.Code, result.Message, result.Value);
		}

		public Result<bool> SwapModem(long accountID, string newSerialNumber, string swapReason, string specialRequest, bool restoreBackedUpSettingsAfterSwap)
		{
			var stationResult = GetStation(accountID);
			if (stationResult.Code != 0)
			{
				return new Result<bool>(stationResult.Code, stationResult.Message);
			}
			// get and return account status
			return stationResult.Value.SwapModem(newSerialNumber, swapReason, specialRequest, restoreBackedUpSettingsAfterSwap);
		}

		public Result<bool> Unregister(long accountID)
		{
			var stationResult = GetStation(accountID);
			if (stationResult.Code != 0)
			{
				return new Result<bool>(stationResult.Code, stationResult.Message);
			}
			// get and return account status
			return stationResult.Value.TerminateAccount();
		}

		public Result<object> AccountStatus(long accountID)
		{
			var cellVendor = SosCrmDataContext.Instance.MS_ReceiverBlockCellDeviceInfoViews.GetByAccountId(accountID);
			if (cellVendor == null || !cellVendor.Vendor.Equals(_ALARMCOM))
			{
				return new Result<object>();
			}
			var stationResult = GetStation(accountID);
			if (stationResult.Code != 0)
			{
				return new Result<object>(stationResult.Code, stationResult.Message);
			}
			// get and return account status
			var result = stationResult.Value.AccountStatus();
			return new Result<object>(result.Code, result.Message, result.Value);
		}

		public Result<bool> ChangeServicePackage(long accountID, string gpEmployeeID, string newCellPackageItemId)
		{
			var stationResult = GetStation(accountID);
			if (stationResult.Code != 0)
			{
				return new Result<bool>(stationResult.Code, stationResult.Message);
			}
			// get and return account status
			return stationResult.Value.ChangeServicePackage(gpEmployeeID, newCellPackageItemId);
		}
		#endregion //Methods
	}
}
