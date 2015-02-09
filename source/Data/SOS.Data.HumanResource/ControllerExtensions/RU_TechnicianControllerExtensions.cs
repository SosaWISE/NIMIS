using AR = SOS.Data.HumanResource.RU_TechniciansView;
using ARCollection = SOS.Data.HumanResource.RU_TechniciansViewCollection;
using ARController = SOS.Data.HumanResource.RU_TechniciansViewController;

namespace SOS.Data.HumanResource.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class RU_TechniciansViewControllerExtensions
	{
        public static AR GetRuTechnicianByTechnicianId(this ARController cntlr, string technicianId)
        {
            var qry = AR.Query()
             .WHERE(AR.Columns.TechnicianId, technicianId);

            return cntlr.LoadSingle(qry);
        }
        public static ARCollection GetRuTechnicianList(this ARController cntlr)
        {
            var qry = AR.Query();
               // .WHERE(AR.Columns.IsDeleted, false);

            return cntlr.LoadCollection(qry);
        }

	}
}
