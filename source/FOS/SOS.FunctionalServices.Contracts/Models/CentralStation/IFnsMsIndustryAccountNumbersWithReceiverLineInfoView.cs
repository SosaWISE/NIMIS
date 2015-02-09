using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.CentralStation
{
	public interface IFnsMsIndustryAccountNumbersWithReceiverLineInfoView
	{
		[DataMember]
		long IndustryAccountID { get; }
		[DataMember]
		long AccountId { get; }
		[DataMember]
		string ReceiverNumber { get; }
		[DataMember]
		string Designator { get; }
		[DataMember]
		string SubscriberNumber { get; }
		[DataMember]
		string IndustryAccount { get; }
		[DataMember]
		string MonitoringStationOSID { get; }
		[DataMember]
		string OSDescription { get; }
		[DataMember]
		string MonitoringStationName { get; }
		[DataMember]
		string PrimaryCSID { get; }
		[DataMember]
		string SecondaryCSID { get; }
	}
}