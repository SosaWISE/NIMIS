using AR = SLS.Data.Cms.XN_CustomerHoldDetail;
using ARCollection = SLS.Data.Cms.XN_CustomerHoldDetailCollection;
using ARController = SLS.Data.Cms.XN_CustomerHoldDetailController; 

namespace SLS.Data.Cms.ControllerExtensions
{
	public static class XN_CustomerHoldDetailControllerExtensions
	{
		public static ARCollection GetAllCustomerByUserId(this ARController oCntlr, int nUserID)
		{
			/** Return result */
			return oCntlr.LoadCollection(AR.Query()
			                      	.WHERE(AR.Columns.UserID, nUserID)
			                      	.ORDER_BY(AR.Columns.sfFundingCreated, "DESC"));
		}
	}
}
