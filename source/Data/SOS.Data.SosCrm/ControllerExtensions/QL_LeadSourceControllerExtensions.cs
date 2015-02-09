using AR = SOS.Data.SosCrm.QL_LeadSource;
using ARCollection = SOS.Data.SosCrm.QL_LeadSourceCollection;
using ARController = SOS.Data.SosCrm.QL_LeadSourceController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
	public static class QL_LeadSourceControllerExtensions
	{
		public static ARCollection GetByDealerId(this ARController oCntlr, int nDealerId)
		{
			/** Initialize. */
			/** Execute SP. */
			QL_LeadSourceCollection oResult = oCntlr.LoadCollection(SosCrmDataStoredProcedureManager.QL_LeadSourceGetByDealerId(nDealerId));

			/** Return result. */
			return oResult;
		}
	}
}
