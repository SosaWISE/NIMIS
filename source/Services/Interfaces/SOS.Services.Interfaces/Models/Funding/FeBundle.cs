using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.Funding
{
	public class FeBundle : IFeBundle
	{

		#region Properties
		public int BundleID { get; set; }

		public string PurchaserID { get; set; }

		public string PurchaserName { get; set; }

		public long? TrackingNumberID { get; set; }

		public string TrackingNumber { get; set; }

		public DateTime? DeliveryDate { get; set; }

		public DateTime? SubmittedOn { get; set; }

		public string SubmittedBy { get; set; }

		public DateTime CreatedOn { get; set; }

		public string CreatedBy { get; set; }

		#endregion Properties
	}

	public interface IFeBundle
	{
		#region Properties
		[DataMember]
		int BundleID { get; set; }

		[DataMember]
		string PurchaserID { get; set; }

		[DataMember]
		string PurchaserName { get; set; }

		[DataMember]
		long? TrackingNumberID { get; set; }

		[DataMember]
		string TrackingNumber { get; set; }

		[DataMember]
		DateTime? DeliveryDate { get; set; }

		[DataMember]
		DateTime? SubmittedOn { get; set; }

		[DataMember]
		string SubmittedBy { get; set; }

		[DataMember]
		DateTime CreatedOn { get; set; }

		[DataMember]
		string CreatedBy { get; set; }

		#endregion Properties
	}
}
