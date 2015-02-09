using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.MonitoringStation
{
	public class MsAccountDslSeisureTypes : IMsAccountDslSeisureTypes
	{
		public short DslSeizureID { get; set; }
		public string DslSeizure { get; set; }
	}

	public interface IMsAccountDslSeisureTypes
	{
		[DataMember]
		short DslSeizureID { get; set; }

		[DataMember]
		string DslSeizure { get; set; }
	}
}
