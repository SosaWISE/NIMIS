using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.CentralStation;

namespace SOS.FunctionalServices.Models.CentralStation
{
	public class FnsMsDispatchAgencyType : IFnsMsDispatchAgencyType
	{
		#region .ctor

		public FnsMsDispatchAgencyType(MS_DispatchAgencyType agencyType)
		{
			DispatchAgencyTypeID = agencyType.DispatchAgencyTypeID;
			MonitoringStationOSId = agencyType.MonitoringStationOSId;
			DispatchAgencyType = agencyType.DispatchAgencyType;
			MsAgencyTypeNo = agencyType.MsAgencyTypeNo;
		}

		#endregion .ctor

		#region Properties
		public short DispatchAgencyTypeID { get; private set; }
		public string MonitoringStationOSId { get; private set; }
		public string DispatchAgencyType { get; private set; }
		public string MsAgencyTypeNo { get; private set; }
		#endregion Properties
	}
}
