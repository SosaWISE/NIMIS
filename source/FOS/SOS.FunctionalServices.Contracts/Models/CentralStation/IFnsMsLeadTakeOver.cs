using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.CentralStation
{
	public interface IFnsMsLeadTakeOver
	{
		[DataMember]
		long AccountId { get; }

		[DataMember]
		long LeadID { get; }

		[DataMember]
		string FullName { get; }

		[DataMember]
		string StreetAddress { get; }

		[DataMember]
		string CityStZip { get; }

		[DataMember]
		int AlarmCompanyId { get; }

		[DataMember]
		string AlarmCompanyName { get; }
 
	}
}