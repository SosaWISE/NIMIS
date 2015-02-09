using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.CentralStation
{
	public interface IFnsMsAccountDslSeizureType
	{
		[DataMember]
		short DslSeizureID { get; }

		[DataMember]
		string DslSeizure { get; }
	}
}