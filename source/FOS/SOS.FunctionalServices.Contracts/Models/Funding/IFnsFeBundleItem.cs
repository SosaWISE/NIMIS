using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.Funding
{
	public interface IFnsFeBundleItem
	{
		[DataMember]
		int BundleItemID { get; }

		[DataMember]
		int BundleId { get; }

		[DataMember]
		int PacketId { get; }

		[DataMember]
		bool IsDeleted { get; }

		[DataMember]
		DateTime CreatedOn { get; }

		[DataMember]
		string CreatedBy { get; }

		[DataMember]
		DateTime? PSubmittedOn { get; }

		[DataMember]
		string PSubmittedBy { get; }

		[DataMember]
		DateTime PCreatedOn { get; }

		[DataMember]
		string PCreatedBy { get; }
		 
	}
}