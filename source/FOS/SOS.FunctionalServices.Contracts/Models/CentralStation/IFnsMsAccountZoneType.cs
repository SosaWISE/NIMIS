using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.CentralStation
{
	public interface IFnsMsAccountZoneType
	{
		[DataMember]
		string AccountZoneTypeID { get; }

		[DataMember]
		string AccountZoneType { get; }
	}
}