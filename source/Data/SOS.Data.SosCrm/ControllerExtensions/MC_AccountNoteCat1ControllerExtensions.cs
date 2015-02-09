using AR = SOS.Data.SosCrm.MC_AccountNoteCat1;
using ARCollection = SOS.Data.SosCrm.MC_AccountNoteCat1Collection;
using ARController = SOS.Data.SosCrm.MC_AccountNoteCat1Controller;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class MC_AccountNoteCat1ControllerExtensions
	{
		public static ARCollection GetByDepartmentId(this ARController cntlr, string departmentId)
		{
			return cntlr.LoadCollection(SosCrmDataStoredProcedureManager.MC_AccountNoteCat1ByDepartmentId(departmentId));
		}
	}
}
