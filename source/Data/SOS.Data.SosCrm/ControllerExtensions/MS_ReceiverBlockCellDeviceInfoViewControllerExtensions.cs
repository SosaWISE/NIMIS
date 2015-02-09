using AR = SOS.Data.SosCrm.MS_ReceiverBlockCellDeviceInfoView;
using ARCollection = SOS.Data.SosCrm.MS_ReceiverBlockCellDeviceInfoViewCollection;
using ARController = SOS.Data.SosCrm.MS_ReceiverBlockCellDeviceInfoViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class MS_ReceiverBlockCellDeviceInfoViewControllerExtensions
	{
		public static AR GetByAccountId(this ARController cnltr, long accountId)
		{
			return cnltr.LoadSingle(SosCrmDataStoredProcedureManager.MS_ReceiverBlockCellDeviceInfoViewGetByAccountID(accountId));
		}
	}
}
