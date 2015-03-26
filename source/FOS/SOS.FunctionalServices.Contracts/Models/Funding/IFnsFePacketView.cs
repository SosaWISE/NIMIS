using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.Funding
{
	public interface IFnsFePacketView
	{
		[DataMember]
		int PacketID { get; }

		[DataMember]
		string CriteriaName { get; }

		[DataMember]
		int? CriteriaId { get; }

		[DataMember]
		string PurchaserID { get; }

		[DataMember]
		string PurchaserName { get; }

		[DataMember]
		DateTime? SubmittedOn { get; }

		[DataMember]
		string SubmittedBy { get; }

		[DataMember]
		bool IsDeleted { get; }

		[DataMember]
		DateTime? CreatedOn { get; }

		[DataMember]
		string CreatedBy { get; }
	}
}