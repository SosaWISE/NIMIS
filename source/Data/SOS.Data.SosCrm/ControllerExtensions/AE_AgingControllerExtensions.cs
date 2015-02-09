using AR = SOS.Data.SosCrm.AE_AgingView;
using ARCollection = SOS.Data.SosCrm.AE_AgingViewCollection;
using ARController = SOS.Data.SosCrm.AE_AgingViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class AE_AgingControllerExtensions
	{
		public static ARCollection GetByCMFID(this ARController cntlr, long cmfid)
		{
			return cntlr.LoadCollection(SosCrmDataStoredProcedureManager.AE_AgingStepByCMFID(cmfid));
		}
	}
}
