using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.CentralStation
{
    public interface IFnsMsIndustryAccount
    {
        #region Properties

        [DataMember]
        long IndustryAccountID { get; }

        [DataMember]
        long AccountId { get; }

        [DataMember]
        string ReceiverLineId { get; }

        [DataMember]
        string ReceiverLineBlockId { get; }

		[DataMember]
		string IndustryAccount { get; }

		[DataMember]
		string Designator { get; }

		[DataMember]
		string SubscriberNumber { get; }

		[DataMember]
		string ReceiverNumber { get; }

		[DataMember]
		bool IsActive { get; }

        #endregion Properties
    }
}
