using NXS.Logic.MonitoringStations.MoniBounceAPI;

namespace NXS.Logic.MonitoringStations.Models.Slammed
{
	public class MatchResultFields
	{
		#region .ctor

		public MatchResultFields(MatchResult inputValue)
		{
			MatchID = inputValue.MatchID;
			MatchCode = inputValue.MatchCode;
			SourceSystemID = inputValue.SourceSystemID;
			SourceSiteID = inputValue.SourceSiteID;
		}

		#endregion .ctor

		#region Properties

		public int MatchID { get; set; }
		public int MatchCode { get; set; }
		public int SourceSystemID { get; set; }
		public int SourceSiteID { get; set; }

		#endregion Properties
	}
}
