using SubSonic;
using AR = SLS.Data.Cms.XN_CustomersMasterFileCompanyStat;
using ARCollection = SLS.Data.Cms.XN_CustomersMasterFileCompanyStatCollection;
using ARController = SLS.Data.Cms.XN_CustomersMasterFileCompanyStatController; 

namespace SLS.Data.Cms.ControllerExtensions
{
	public static class XN_CustomersMasterFileCompanyStatsControllerExtensions
	{
		public static ARCollection GetAll(this ARController oCntlr)
		{
			return oCntlr.LoadCollection(AR.Query());
		}

		public static ARCollection GetAllByUserId(this ARController oCntlr, int nUserId)
		{
			/** Locals */
			var oQuery = AR.Query()
				.WHERE(AR.Columns.UserID, Comparison.Equals, nUserId);
			return oCntlr.LoadCollection(oQuery);
		}

		public static ARCollection GetAllCustomersByUserId(this ARController oCntlr, int nUserId)
		{
			/** Locals */
			var oQuery = AR.Query()
				.WHERE(AR.Columns.UserID, nUserId)
				.WHERE(AR.Columns.CustomerID, Comparison.GreaterThan, 0)
				.ORDER_BY(AR.Columns.SaleDate, "DESC");

			return oCntlr.LoadCollection(oQuery);
		}

		public static ARCollection GetBySeasonID(this ARController oCntlr, int nSeasonID)
		{
			return oCntlr.LoadCollection(AR.Query().WHERE(AR.Columns.SeasonID, nSeasonID));
		}
	}
}
