using AR = SOS.Data.SosCrm.MS_EmergencyContactRelationship;
using ARCollection = SOS.Data.SosCrm.MS_EmergencyContactRelationshipCollection;
using ARController = SOS.Data.SosCrm.MS_EmergencyContactRelationshipController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
	// ReSharper disable once InconsistentNaming
	public static class MS_EmergencyContactRelationshipControllerExtensions
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
