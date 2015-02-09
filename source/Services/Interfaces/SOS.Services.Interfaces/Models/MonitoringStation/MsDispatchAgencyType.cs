namespace SOS.Services.Interfaces.Models.MonitoringStation
{
	public class MsDispatchAgencyType : IMsDispatchAgencyType
	{
		#region Properties
		public short DispatchAgencyTypeID { get; set; }
		public string MonitoringStationsOSId { get; set; }
		public string DispatchAgencyType { get; set; }
		public string MsAgencyTypeNo { get; set; }
		#endregion Properties
	}

	public interface IMsDispatchAgencyType
	{
		#region Properties
		short DispatchAgencyTypeID { get; set; }
		string MonitoringStationsOSId { get; set; }
		string DispatchAgencyType { get; set; }
		string MsAgencyTypeNo { get; set; }
		#endregion Properties
	}
}
