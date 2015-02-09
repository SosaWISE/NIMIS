using AR = SOS.Data.SosCrm.QL_LeadProductOffersView;
using ARCollection = SOS.Data.SosCrm.QL_LeadProductOffersViewCollection;
using ARController = SOS.Data.SosCrm.QL_LeadProductOffersViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
	public static class QL_LeadProductOfferViewControllerExtensions
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
