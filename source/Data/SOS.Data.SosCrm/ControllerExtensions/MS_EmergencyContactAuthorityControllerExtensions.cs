using AR = SOS.Data.SosCrm.MS_EmergencyContactAuthority;
using ARCollection = SOS.Data.SosCrm.MS_EmergencyContactAuthorityCollection;
using ARController = SOS.Data.SosCrm.MS_EmergencyContactAuthorityController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
    // ReSharper disable once InconsistentNaming
    public static class MS_EmergencyContactAuthorityControllerExtensions
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
