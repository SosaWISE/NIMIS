using AR = SOS.Data.SosCrm.QL_LeadDisposition;
using ARCollection = SOS.Data.SosCrm.QL_LeadDispositionCollection;
using ARController = SOS.Data.SosCrm.QL_LeadDispositionController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
	public static class QL_LeadDispositionControllerExtensions
	{
		public static ARCollection GetByDealerId(this ARController oCntlr, int nDealerId)
		{
			/** Initialize. */
			/** Execute SP. */
			QL_LeadDispositionCollection oResult = oCntlr.LoadCollection(SosCrmDataStoredProcedureManager.QL_LeadDispositionGetByDealerId(nDealerId));

			/** Return result. */
			return oResult;
		}
	}
}
