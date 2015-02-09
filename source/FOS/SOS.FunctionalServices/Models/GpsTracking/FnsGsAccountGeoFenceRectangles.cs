using System;
using SOS.Data.GpsTracking;
using SOS.FunctionalServices.Contracts.Models.GpsTracking;

namespace SOS.FunctionalServices.Models.GpsTracking
{
	public class FnsGsAccountGeoFenceRectangles : IFnsGsAccountGeoFenceRectangles
	{
		#region .ctor

		public FnsGsAccountGeoFenceRectangles (GS_AccountGeoFencesView oRectangleView)
		{
			GeoFenceID = oRectangleView.GeoFenceID;
			AccountId = oRectangleView.AccountId;
			if (oRectangleView.MeanLattitude != null) MeanLattitude = (double) oRectangleView.MeanLattitude;
			if (oRectangleView.MeanLongitude != null) MeanLongitude = (double) oRectangleView.MeanLongitude;
			if (oRectangleView.MaxLattitude != null) MaxLattitude = (double) oRectangleView.MaxLattitude;
			if (oRectangleView.MinLongitude != null) MinLongitude = (double) oRectangleView.MinLongitude;
			if (oRectangleView.MinLattitude != null) MinLattitude = (double) oRectangleView.MinLattitude;
			if (oRectangleView.MaxLongitude != null) MaxLongitude = (double) oRectangleView.MaxLongitude;
			Area = oRectangleView.Area;
			ZoomLevel = oRectangleView.ZoomLevel;
			ModifiedOn = oRectangleView.ModifiedOn;
			ModifiedBy = oRectangleView.ModifiedBy;
			CreatedOn = oRectangleView.CreatedOn;
			CreatedBy = oRectangleView.CreatedBy;
		}

		#endregion .ctor

		#region Properties

		public long GeoFenceID { get; set; }
		public long AccountId { get; set; }
		public double MeanLattitude { get; set; }
		public double MeanLongitude { get; set; }
		public double MaxLattitude { get; set; }
		public double MinLongitude { get; set; }
		public double MinLattitude { get; set; }
		public double MaxLongitude { get; set; }
		public double? Area { get; set; }
		public short? ZoomLevel { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }

		#endregion Properties
	}
}
