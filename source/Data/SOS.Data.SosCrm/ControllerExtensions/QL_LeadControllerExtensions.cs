using AR = SOS.Data.SosCrm.QL_Lead;
using ARCollection = SOS.Data.SosCrm.QL_LeadCollection;
using ARController = SOS.Data.SosCrm.QL_LeadController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
	public static class QL_LeadControllerExtensions
	{
		public static AR LoadByPrimaryKeyAndCMFId(this ARController oCntlr, long lLeadId, long lCMFId, int nDealerId)
		{
			/** Initialize. */
			var oQuery = AR.Query();
			oQuery.WHERE(AR.Columns.LeadID, lLeadId)
				.AND(AR.Columns.CustomerMasterFileId, lCMFId)
				.AND(AR.Columns.DealerId, nDealerId);

			/** Execute. */
			var oResult = oCntlr.LoadSingle(oQuery);

			/** Return result. */
			return oResult;
		}

		public static ARCollection ByCmfID(this ARController cntlr, long cmfid)
		{
			var qry = AR.Query().WHERE(AR.Columns.CustomerMasterFileId, cmfid);
			return cntlr.LoadCollection(qry);
		}
	}
}
