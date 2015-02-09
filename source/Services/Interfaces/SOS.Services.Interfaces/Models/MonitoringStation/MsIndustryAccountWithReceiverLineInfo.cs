using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.MonitoringStation
{
	public class MsIndustryAccountWithReceiverLineInfo : IMsIndustryAccountWithReceiverLineInfo
	{
		#region Properties
		public long IndustryAccountID { get; set; }
		public long AccountId { get; set; }
		public string ReceiverNumber { get; set; }
		public string Designator { get; set; }
		public string SubscriberNumber { get; set; }
		public string IndustryAccount { get; set; }
		public string MonitoringStationOSID { get; set; }
		public string OSDescription { get; set; }
		public string MonitoringStationName { get; set; }
		public string PrimaryCSID { get; set; }
		public string SecondaryCSID { get; set; }
		#endregion Properties
	}

	public interface IMsIndustryAccountWithReceiverLineInfo
	{
		[DataMember]
		long IndustryAccountID { get; set; }
		[DataMember]
		long AccountId { get; set; }
		[DataMember]
		string ReceiverNumber { get; set; }
		[DataMember]
		string Designator { get; set; }
		[DataMember]
		string SubscriberNumber { get; set; }
		[DataMember]
		string IndustryAccount { get; set; }
		[DataMember]
		string MonitoringStationOSID { get; set; }
		[DataMember]
		string OSDescription { get; set; }
		[DataMember]
		string MonitoringStationName { get; set; }
		[DataMember]
		string PrimaryCSID { get; set; }
		[DataMember]
		string SecondaryCSID { get; set; }
	}
}