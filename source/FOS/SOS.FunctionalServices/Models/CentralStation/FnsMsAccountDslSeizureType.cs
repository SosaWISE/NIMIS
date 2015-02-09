using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.CentralStation;

namespace SOS.FunctionalServices.Models.CentralStation
{
	public class FnsMsAccountDslSeizureType : IFnsMsAccountDslSeizureType
	{
		#region .ctor

		public FnsMsAccountDslSeizureType(MS_AccountDslSeizureType type)
		{
			DslSeizureID = type.DslSeizureID;
			DslSeizure = type.DslSeizure;
		}

		#endregion .ctor

		#region Properties
		public short DslSeizureID { get; private set; }
		public string DslSeizure { get; private set; }
		#endregion Properties
	}
}
