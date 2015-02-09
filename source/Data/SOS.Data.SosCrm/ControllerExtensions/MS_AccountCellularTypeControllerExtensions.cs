using AR = SOS.Data.SosCrm.MS_AccountCellularType;
using ARCollection = SOS.Data.SosCrm.MS_AccountCellularTypeCollection;
using ARController = SOS.Data.SosCrm.MS_AccountCellularTypeController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
	public static class MsAccountCellularTypeControllerExtensions
	{
		public static ARCollection GetAll(this ARController cntlr)
		{
			return cntlr.LoadCollection(SosCrmDataStoredProcedureManager.MS_AccountCellularTypesGet());
		}
	}
}
