using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.Funding
{
	public interface IFnsFeBundle
	{
		#region Properties
		[DataMember]
		int BundleID { get; }

		[DataMember]
		string PurchaserID { get; }

		[DataMember]
		string PurchaserName { get; }

		[DataMember]
		long? TrackingNumberID { get; }

		[DataMember]
		string TrackingNumber { get; }

		[DataMember]
		DateTime? DeliveryDate { get; }

		[DataMember]
		DateTime? SubmittedOn { get; }

		[DataMember]
		string SubmittedBy { get; }

		[DataMember]
		DateTime CreatedOn { get; }

		[DataMember]
		string CreatedBy { get; }

		#endregion Properties
		 
	}
}