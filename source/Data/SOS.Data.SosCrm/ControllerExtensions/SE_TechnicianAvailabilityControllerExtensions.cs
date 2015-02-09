using SOS.Lib.Util;
using System;
using AR = SOS.Data.SosCrm.SE_TechnicianAvailability;
using ARCollection = SOS.Data.SosCrm.SE_TechnicianAvailabilityCollection;
using ARController = SOS.Data.SosCrm.SE_TechnicianAvailabilityController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
    public static class TechnicianAvailabilityControllerExtensions
	{


        public static AR SeTechnicianAvailabilityCreate(this ARController cntlr,
                                  string technicianId,
                                  DateTime startDateTime,
                                  DateTime endDateTime
                             

           )
        {
            return
                cntlr.LoadSingle(SosCrmDataStoredProcedureManager.SE_TechnicianAvailabilityCreate(
                                   technicianId,
                                   startDateTime,
                                   endDateTime
                    )
                );
        }

        public static ARCollection GetTechnicianAvailabilityList(this ARController oCntlr)
        {
            /** Initialize. */
            var oQry = AR.Query()
                .WHERE(AR.Columns.IsDeleted, false);
              //  .BETWEEN_AND(AR.Columns.StartTime, dateFrom, dateTo);

            /** Return result. */
            return oCntlr.LoadCollection(oQry);
        }

        public static ARCollection GetTechnicianAvailabilityByTechId(this ARController oCntlr, string technicianId)
        {
            /** Initialize. */
            var oQry = AR.Query()
                .WHERE(AR.Columns.TechnicianId, technicianId)
                .WHERE(AR.Columns.IsDeleted, false)
                .ORDER_BY(AR.Columns.StartDateTime, "Descending");
                
            /** Return result. */
            return oCntlr.LoadCollection(oQry);
        }


       
	}
}
