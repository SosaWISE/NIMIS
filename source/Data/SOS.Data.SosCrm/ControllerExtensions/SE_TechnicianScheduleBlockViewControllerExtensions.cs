using SOS.Lib.Util;
using System;
using AR = SOS.Data.SosCrm.SE_TechnicianScheduleBlocksView;
using ARCollection = SOS.Data.SosCrm.SE_TechnicianScheduleBlocksViewCollection;
using ARController = SOS.Data.SosCrm.SE_TechnicianScheduleBlocksViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
    public static class TechnicianScheduleBlocksViewControllerExtensions
	{
        public static ARCollection GetTechnicianScheduleBlockList(this ARController oCntlr, DateTime dateFrom, DateTime dateTo)
        {
            /** Initialize. */
            var oQry = AR.Query()
                .WHERE(AR.Columns.IsDeleted, false);
               // .BETWEEN_AND(AR.Columns.StartTime, dateFrom, dateTo);

            /** Return result. */
            return oCntlr.LoadCollection(oQry);
        }

      


	}
}
