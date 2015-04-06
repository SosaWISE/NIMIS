using System;
using NXS.Data.Funding;
using SOS.FunctionalServices.Contracts.Models.Funding;

namespace SOS.FunctionalServices.Models.Funding
{
	public class FnsFeBundle : IFnsFeBundle
	{
		#region .ctor

		public FnsFeBundle(FE_BundlesView item)
		{
			BundleID = item.BundleID;
			PurchaserID = item.PurchaserID;
			PurchaserName = item.PurchaserName;
			TrackingNumberID = item.TrackingNumberID;
			TrackingNumber = item.TrackingNumber;
			DeliveryDate = item.DeliveryDate;
			SubmittedOn = item.SubmittedOn;
			SubmittedBy = item.SubmittedBy;
			CreatedOn = item.CreatedOn;
			CreatedBy = item.CreatedBy;
		}
		#endregion .ctor

		#region Properties
		public int BundleID { get; private set; }

		public string PurchaserID { get; private set; }

		public string PurchaserName { get; private set; }

		public long? TrackingNumberID { get; private set; }

		public string TrackingNumber { get; private set; }

		public DateTime? DeliveryDate { get; private set; }

		public DateTime? SubmittedOn { get; private set; }

		public string SubmittedBy { get; private set; }

		public DateTime CreatedOn { get; private set; }

		public string CreatedBy { get; private set; }

		#endregion Properties
	}
}
