using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.MonitoringStation
{
	public class MsLeadTakeOver : IMsLeadTakeOver
	{
		#region Properties
		public long AccountId { get; set; }
		public long LeadID { get; set; }
		public string FullName { get; set; }
		public string StreetAddress { get; set; }
		public string CityStZip { get; set; }
		public int AlarmCompanyId { get; set; }
		public string AlarmCompanyName { get; set; }
		#endregion Properties
	}

	public interface IMsLeadTakeOver
	{
		[DataMember]
		long AccountId { get; set; }

		[DataMember]
		long LeadID { get; set; }

		[DataMember]
		string FullName { get; set; }

		[DataMember]
		string StreetAddress { get; set; }

		[DataMember]
		string CityStZip { get; set; }

		[DataMember]
		int AlarmCompanyId { get; set; }

		[DataMember]
		string AlarmCompanyName { get; set; }
	}
}