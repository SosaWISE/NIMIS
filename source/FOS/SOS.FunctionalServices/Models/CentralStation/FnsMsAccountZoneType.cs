using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.CentralStation;

namespace SOS.FunctionalServices.Models.CentralStation
{
	public class FnsMsAccountZoneType : IFnsMsAccountZoneType
	{
		#region .ctor

		public FnsMsAccountZoneType(MS_AccountZoneType zoneType)
		{
			AccountZoneTypeID = zoneType.AccountZoneTypeID;
			AccountZoneType = zoneType.AccountZoneType;
		}

		#endregion .ctor

		#region Properties

		public string AccountZoneTypeID { get; private set; }
		public string AccountZoneType { get; private set; }

		#endregion Properties
	}
}
