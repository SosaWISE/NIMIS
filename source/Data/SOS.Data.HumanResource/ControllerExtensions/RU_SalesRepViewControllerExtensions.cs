using AR = SOS.Data.HumanResource.RU_SalesRepsView;
using ARCollection = SOS.Data.HumanResource.RU_SalesRepsViewCollection;
using ARController = SOS.Data.HumanResource.RU_SalesRepsViewController;

namespace SOS.Data.HumanResource.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class RU_SalesRepViewControllerExtensions
	{
        public static AR GetRuSalesRepBySalesRepId(this ARController cntlr, string salesRepId)
        {
            var qry = AR.Query()
			 .WHERE(AR.Columns.SalesRepId, salesRepId);

            return cntlr.LoadSingle(qry);
        }
        public static ARCollection GetRuSalesRepList(this ARController cntlr)
        {
            var qry = AR.Query();
               // .WHERE(AR.Columns.IsDeleted, false);

            return cntlr.LoadCollection(qry);
        }

	}
}
