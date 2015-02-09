using SOS.Lib.Util;
using System;
using AR = SOS.Data.SosCrm.SE_ScheduleBlocksView;
using ARCollection = SOS.Data.SosCrm.SE_ScheduleBlocksViewCollection;
using ARController = SOS.Data.SosCrm.SE_ScheduleBlocksViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
    public static class ScheduleBlocksViewControllerExtensions
	{
        public static ARCollection GetScheduleBlockList(this ARController oCntlr, DateTime dateFrom, DateTime dateTo)
        {
            /** Initialize. */
            var oQry = AR.Query()
                .WHERE(AR.Columns.IsDeleted, false);
               // .BETWEEN_AND(AR.Columns.StartTime, dateFrom, dateTo);

            /** Return result. */
            return oCntlr.LoadCollection(oQry);
        }

        public static AR GetByBlockID(this ARController oCntlr, long blockId)
        {
            /** Initialize. */
            var oQuery = AR.Query()
                .WHERE(AR.Columns.BlockID, blockId)
                .WHERE(AR.Columns.IsDeleted, false);   //do not include deleted ProductBarcode


            /** Execute. */
            var oResult = oCntlr.LoadSingle(oQuery);

            /** REturn result. */
            return oResult;
        }


	}
}
