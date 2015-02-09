// ReSharper disable once CheckNamespace
namespace SOS.Data.SosCrm
{
// ReSharper disable once InconsistentNaming
	public partial class MS_Account
	{
		public MS_EmergencyContactCollection GetEmergencyContact()
		{
			// ** Initialize
			var qry = MS_EmergencyContact.Query()
				.WHERE(MS_EmergencyContact.Columns.AccountId, AccountID)
				.WHERE(MS_EmergencyContact.Columns.IsActive, true)
				.WHERE(MS_EmergencyContact.Columns.IsDeleted, false)
				.ORDER_BY(MS_EmergencyContact.Columns.OrderNumber);

			return SosCrmDataContext.Instance.MS_EmergencyContacts.LoadCollection(qry);
		}

		public string GetMsAccountSignalFormat()
		{
			if (SignalFormatTypeId == null || SignalFormatTypeId == 0) return "CID";

			return SignalFormatType.MsSignalFormatTypeId;
		}

		public MS_MonitronicsEntitySystemType GetMoniSysTypeId()
		{
			// ** Initialize
			return SosCrmDataContext.Instance.MS_MonitronicsEntitySystemTypes.LoadSingle(
				SosCrmDataStoredProcedureManager.MS_MonitronicsEntitySysTypesPanelGet(AccountID));
		}

		public MS_MonitronicsEntitySystemType GetMoniCellTypeId()
		{
			return SosCrmDataContext.Instance.MS_MonitronicsEntitySystemTypes.LoadSingle(
				SosCrmDataStoredProcedureManager.MS_MonitronicsEntitySysTypesCellDeviceGet(AccountID));
		}

		public MS_MonitronicsEntityOption GetMoniCellProvider()
		{
			return SosCrmDataContext.Instance.MS_MonitronicsEntityOptions.LoadSingle(
				SosCrmDataStoredProcedureManager.MS_MonitronicsEntityOptionsCellProvGet(AccountID));
		}

		public MS_ReceiverBlockCellDeviceInfoView GetCellDeviceSerialNumber()
		{
			return SosCrmDataContext.Instance.MS_ReceiverBlockCellDeviceInfoViews.LoadSingle(
				SosCrmDataStoredProcedureManager.MS_ReceiverBlockCellDeviceInfoViewGetByAccountID(AccountID));
		}

		public MS_MonitronicsEntityCellService GetMoniCellService()
		{
			return SosCrmDataContext.Instance.MS_MonitronicsEntityCellServices.LoadSingle(
				SosCrmDataStoredProcedureManager.MS_MonitronicsEntityCellServicesGet(AccountID));
		}
	}
}