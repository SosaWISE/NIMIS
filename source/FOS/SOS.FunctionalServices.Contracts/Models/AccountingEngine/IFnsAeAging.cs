using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.AccountingEngine
{
	public interface IFnsAeAging
	{
		#region Properties
		[DataMember]
		long CustomerMasterFileID { get; }

		[DataMember]
		string AgingStepID { get; }

		[DataMember]
		string AgingStep { get; }

		[DataMember]
		decimal Value { get; }

		[DataMember]
		short StepOrder { get; }
		#endregion Properties
	}
}