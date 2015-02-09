using AR = SOS.Data.SosCrm.MC_PoliticalTimeZone;
using ARCollection = SOS.Data.SosCrm.MC_PoliticalTimeZoneCollection;
using ARController = SOS.Data.SosCrm.MC_PoliticalTimeZoneController;
using SubSonic;

namespace SOS.Data.SosCrm.ControllerExtensions
{
	// ReSharper disable InconsistentNaming
	public static class MC_PoliticalTimeZoneControllerExtensions
	// ReSharper restore InconsistentNaming
	{
		#region Methods
		public static ARCollection GetAllActive(this ARController oCntrl)
		{
			Query qry = AR.Query()
				.WHERE(AR.Columns.IsActive, Comparison.Equals, true)
				.ORDER_BY(AR.Columns.TimeZoneName, "ASC");

			return oCntrl.LoadCollection(qry);
		}
		#endregion Methods
	}
}
