using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.Funding
{
	public interface IFnsFePacketItemView
	{
		[DataMember]
		long PacketItemID { get; }

		[DataMember]
		int PacketId { get; }

		[DataMember]
		long CustomerNumber { get; }

		[DataMember]
		long CustomerId { get; }

		[DataMember]
		long AccountId { get; }

		[DataMember]
		string FirstName { get; }

		[DataMember]
		string LastName { get; }

		[DataMember]
		long? ReturnAccountFundingStatusId { get; }

		[DataMember]
		string AccountFundingShortDesc { get; }

		[DataMember]
		string AccountStatusNote { get; }

		[DataMember]
		string ModifiedBy { get; }

		[DataMember]
		DateTime? ModifiedOn { get; }

		[DataMember]
		string CreatedBy { get; }

		[DataMember]
		DateTime? CreatedOn { get; }
 
	}
}