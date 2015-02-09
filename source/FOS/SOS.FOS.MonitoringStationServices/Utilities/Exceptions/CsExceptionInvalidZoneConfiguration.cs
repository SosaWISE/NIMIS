using System;

namespace SOS.FOS.MonitoringStationServices.Utilities.Exceptions
{
	public class CsExceptionInvalidZoneConfiguration : Exception
	{
		#region .ctor

		public CsExceptionInvalidZoneConfiguration(long accountId, string msosid, long accountZoneAssignmentID, string accountZoneTypeId, string zone)
			: base(string.Format("Invalid Zone Configuration with the following values: AccountId: {0} | MoniStation: '{1}' | AccountZoneAssignmentID: {2} | AccountZoneTypeId: '{3}' | Zone: '{4}'", accountId, msosid, accountZoneAssignmentID, accountZoneTypeId, zone))
		{
			AccountID = accountId;
			MonitoringStationOSId = msosid;
			AccountZoneAssignmentID = accountZoneAssignmentID;
			AccountZoneTypeId = accountZoneTypeId;
			Zone = zone;
		}

		#endregion .ctor

		#region Properties
		public long AccountID { get; private set; }
		public string MonitoringStationOSId { get; private set; }
		public long AccountZoneAssignmentID { get; private set; }
		public string AccountZoneTypeId { get; private set; }
		public string Zone { get; private set; }
		#endregion Properties
	}
}
