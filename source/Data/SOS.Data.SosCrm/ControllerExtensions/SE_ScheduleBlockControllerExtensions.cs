using SOS.Lib.Util;
using System;
using AR = SOS.Data.SosCrm.SE_ScheduleBlock;
using ARCollection = SOS.Data.SosCrm.SE_ScheduleBlockCollection;
using ARController = SOS.Data.SosCrm.SE_ScheduleBlockController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
    public static class ScheduleBlockControllerExtensions
	{


        public static AR SeScheduleBlockCreate(this ARController cntlr,
                                  string block,
                                  string zipCode,
                                  double? maxRadius,
                                  double? distance,
                                  DateTime? startTime,
                                  DateTime? endTime,
                                  int? availableSlots,
                                  string technicianId,
                                  bool? isTechConfirmed,
                                  string color,
                                  bool isBlocked

           )
        {
            return
                cntlr.LoadSingle(SosCrmDataStoredProcedureManager.SE_ScheduleBlockCreate(
                                    block,
                                    zipCode,
                                    maxRadius,
                                    distance,
                                    startTime,
                                    endTime,
                                    availableSlots,
                                    technicianId,
                                    isTechConfirmed,
                                    color,
                                    isBlocked
                    )
                );
        }

        public static ARCollection GetScheduleBlockList(this ARController oCntlr, DateTime dateFrom, DateTime dateTo)
        {
            /** Initialize. */
            var oQry = AR.Query()
                .WHERE(AR.Columns.IsDeleted, false);
              //  .BETWEEN_AND(AR.Columns.StartTime, dateFrom, dateTo);

            /** Return result. */
            return oCntlr.LoadCollection(oQry);
        }



        public static AR SeScheduleTicketTechUpdate(this ARController cntlr,long blockId, string technicianId)
        {
            return
                cntlr.LoadSingle(SosCrmDataStoredProcedureManager.SE_ScheduleTicketTechUpdate(
                                    blockId,
                                    technicianId
                    )
                );
        }

       
	}
}
