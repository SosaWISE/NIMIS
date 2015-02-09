using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.MonitoringStation
{
	public class MsIndustryAccountReq : IMsIndustryAccountReq
	{
		public long AccountId { get; set; }
		public string AccountType { get; set; }
	}

	public interface IMsIndustryAccountReq
	{
		[DataMember]
		long AccountId { get; set; }

		[DataMember]
		string AccountType { get; set; }
	}
}