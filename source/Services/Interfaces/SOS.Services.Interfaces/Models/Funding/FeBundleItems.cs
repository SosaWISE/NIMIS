using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.Funding
{
	public class FeBundleItems : IFeBundleItems
	{
		#region Properties
		public int BundleItemID { get; set; }
		public int BundleId { get; set; }
		public int PacketId { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? PSubmittedOn { get; set; }
		public string PSubmittedBy { get; set; }
		public DateTime PCreatedOn { get; set; }
		public string PCreatedBy { get; set; }
		#endregion Properties
	}

	public interface IFeBundleItems
	{
		[DataMember]
		int BundleItemID { get; set; }

		[DataMember]
		int BundleId { get; set; }

		[DataMember]
		int PacketId { get; set; }

		[DataMember]
		DateTime CreatedOn { get; set; }

		[DataMember]
		string CreatedBy { get; set; }

		[DataMember]
		DateTime? PSubmittedOn { get; set; }

		[DataMember]
		string PSubmittedBy { get; set; }

		[DataMember]
		DateTime PCreatedOn { get; set; }

		[DataMember]
		string PCreatedBy { get; set; }
	}
}
