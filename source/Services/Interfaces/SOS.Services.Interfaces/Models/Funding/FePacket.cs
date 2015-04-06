using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.Funding
{
	public class FePacket : IFePacket
	{
		#region Properties
		public int PacketID { get; set; }
		public string CriteriaName { get; set; }
		public int? CriteriaId { get; set; }
		public string PurchaserID { get; set; }
		public string PurchaserName { get; set; }
		public DateTime? SubmittedOn { get; set; }
		public string SubmittedBy { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime? CreatedOn { get; set; }
		public string CreatedBy { get; set; }
		#endregion Properties
	}

	public interface IFePacket
	{
		[DataMember]
		int PacketID { get; set; }

		[DataMember]
		string CriteriaName { get; set; }

		[DataMember]
		int? CriteriaId { get; set; }

		[DataMember]
		string PurchaserID { get; set; }

		[DataMember]
		string PurchaserName { get; set; }

		[DataMember]
		DateTime? SubmittedOn { get; set; }

		[DataMember]
		string SubmittedBy { get; set; }

		[DataMember]
		bool IsDeleted { get; set; }

		[DataMember]
		DateTime? CreatedOn { get; set; }

		[DataMember]
		string CreatedBy { get; set; }
		
	}
}
