using AR = SOS.Data.SosCrm.MS_EmergencyContactPhoneType;
using ARCollection = SOS.Data.SosCrm.MS_EmergencyContactPhoneTypeCollection;
using ARController = SOS.Data.SosCrm.MS_EmergencyContactPhoneTypeController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class MS_EmergencyContactPhoneTypeControllerExtensions
	{
		public static ARCollection LoadAllSafe(this ARController cntlr)
		{
			var qry = AR.Query()
				.WHERE(AR.Columns.IsActive, true)
				.WHERE(AR.Columns.IsDeleted, false);

			return cntlr.LoadCollection(qry);
		}

		public static ARCollection LoadSafeByMsosId(this ARController cntlr, string msosid)
		{
			var qry = AR.Query()
				.WHERE(AR.Columns.MonitoringStationOSId, msosid)
				.WHERE(AR.Columns.IsActive, true)
				.WHERE(AR.Columns.IsDeleted, false);

			return cntlr.LoadCollection(qry);
		}
	}
}
