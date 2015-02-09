using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.MonitoringStation
{
	public class MsDispatchAgency : IMsDispatchAgency
	{
		#region Properties
		public long AccountId { get; set; }
		public int DispatchAgencyID { get; set; }
		public short DispatchAgencyTypeId { get; set; }
		public string MonitoringStationOSId { get; set; }
		public int DispatchAgencyOsId { get; set; }
		public string DispatchAgencyName { get; set; }
		public string MsAgencyNumber { get; set; }
		public string Address1 { get; set; }
		public string Address2 { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string ZipCode { get; set; }
		public string Phone1 { get; set; }
		public string Phone2 { get; set; }
		public string DispatchAgencyType { get; set; }
		#endregion Properties
	}

	public interface IMsDispatchAgency
	{
		[DataMember]
		long AccountId { get; set; }
		[DataMember]
		int DispatchAgencyID { get; set; }
		[DataMember]
		short DispatchAgencyTypeId { get; set; }
		[DataMember]
		string MonitoringStationOSId { get; set; }
		[DataMember]
		int DispatchAgencyOsId { get; set; }
		[DataMember]
		string DispatchAgencyName { get; set; }
		[DataMember]
		string MsAgencyNumber { get; set; }
		[DataMember]
		string Address1 { get; set; }
		[DataMember]
		string Address2 { get; set; }
		[DataMember]
		string City { get; set; }
		[DataMember]
		string State { get; set; }
		[DataMember]
		string ZipCode { get; set; }
		[DataMember]
		string Phone1 { get; set; }
		[DataMember]
		string Phone2 { get; set; }
		[DataMember]
		string DispatchAgencyType { get; set; }
	}
}
