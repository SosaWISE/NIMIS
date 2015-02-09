using AR = SOS.Data.SosCrm.QL_LeadProductOffer;
using ARCollection = SOS.Data.SosCrm.QL_LeadProductOfferCollection;
using ARController = SOS.Data.SosCrm.QL_LeadProductOfferController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
	public static class QL_LeadProductOfferControllerExtensions
	{
		public static ARCollection LoadByLeadId(this ARController oCntlr, long lLeadID)
		{
			/** Initialize. */
			var oQry = AR.Query()
				.WHERE(AR.Columns.LeadId, lLeadID)
				.ORDER_BY(AR.Columns.OfferDate, "DESC");

			var oResult = oCntlr.LoadCollection(oQry);

			/** Return result. */
			return oResult;
		}
	}
}
