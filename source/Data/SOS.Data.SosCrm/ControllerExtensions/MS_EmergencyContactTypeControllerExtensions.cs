using AR = SOS.Data.SosCrm.MS_EmergencyContactType;
using ARCollection = SOS.Data.SosCrm.MS_EmergencyContactTypeCollection;
using ARController = SOS.Data.SosCrm.MS_EmergencyContactTypeController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
    // ReSharper disable once InconsistentNaming
    public static class MS_EmergencyContactTypeControllerExtensions
    {
        public static ARCollection LoadAllSafe(this ARController cntlr)
        {
            /** Init. */
            var qry = AR.Query()
                .WHERE(AR.Columns.IsActive, true)
                .WHERE(AR.Columns.IsDeleted, false);

            /** Return result. */
            return cntlr.LoadCollection(qry);
        }
        public static ARCollection ByMonitoringStation(this ARController cntlr, string monitoringStationOSId)
        {
            var qry = AR.Query()
                .WHERE(AR.Columns.MonitoringStationOSId, monitoringStationOSId)
                .WHERE(AR.Columns.IsActive, true)
                .WHERE(AR.Columns.IsDeleted, false);
            return cntlr.LoadCollection(qry);
        }
    }
}
