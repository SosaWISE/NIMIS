using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.Funding
{
	public class FePacketItem : IFePacketItem
	{
		public long PacketItemID { get; set; }
		public int PacketId { get; set; }
		public long CustomerNumber { get; set; }
		public long CustomerId { get; set; }
		public long AccountId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public long? ReturnAccountFundingStatusId { get; set; }
		public string AccountFundingShortDesc { get; set; }
		public string AccountStatusNote { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime? ModifiedOn { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? CreatedOn { get; set; }
	}

	public interface IFePacketItem
	{
		[DataMember]
		long PacketItemID { get; set; }

		[DataMember]
		int PacketId { get; set; }

		[DataMember]
		long CustomerNumber { get; set; }

		[DataMember]
		long CustomerId { get; set; }

		[DataMember]
		long AccountId { get; set; }

		[DataMember]
		string FirstName { get; set; }

		[DataMember]
		string LastName { get; set; }

		[DataMember]
		long? ReturnAccountFundingStatusId { get; set; }

		[DataMember]
		string AccountFundingShortDesc { get; set; }

		[DataMember]
		string AccountStatusNote { get; set; }

		[DataMember]
		string ModifiedBy { get; set; }

		[DataMember]
		DateTime? ModifiedOn { get; set; }

		[DataMember]
		string CreatedBy { get; set; }

		[DataMember]
		DateTime? CreatedOn { get; set; }
 		
	}
}
