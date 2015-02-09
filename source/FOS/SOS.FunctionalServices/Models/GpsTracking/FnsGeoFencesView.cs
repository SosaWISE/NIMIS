using System;
using System.Collections.Generic;
using SOS.Data.GpsTracking;
using SOS.FunctionalServices.Contracts.Models.GpsTracking;

namespace SOS.FunctionalServices.Models.GpsTracking
{
	public class FnsGeoFencesView : IFnsGeoFencesView
	{
		public FnsGeoFencesView(GS_AccountGeoFencesView oFenceItem, List<IFnsGsAccountGeoFencePolygons> oPolyPointsList)
		{
			GeoFenceID = oFenceItem.GeoFenceID;
			GeoFenceTypeId = oFenceItem.GeoFenceTypeId;
			GeoFenceTypeUi = oFenceItem.GeoFenceTypeUi;
			ReportModeId = oFenceItem.ReportModeId;
			ReportModeUi = oFenceItem.ReportModeUi;
			GeoFenceType = oFenceItem.GeoFenceType;
			AccountId = oFenceItem.AccountId;
			GeoFenceName = oFenceItem.GeoFenceName;
			GeoFenceDescription = oFenceItem.GeoFenceDescription;
			GeoFenceNameUi = oFenceItem.GeoFenceNameUi;
			MeanLattitude = oFenceItem.MeanLattitude;
			MeanLongitude = oFenceItem.MeanLongitude;
			GoogleMapZoomLevel = oFenceItem.ZoomLevel;
			Area = oFenceItem.Area;
			MinLattitude = oFenceItem.MinLattitude;
			MinLongitude = oFenceItem.MinLongitude;
			MaxLattitude = oFenceItem.MaxLattitude;
			MaxLongitude = oFenceItem.MaxLongitude;
			PointLatitude = oFenceItem.PointLatitude;
			PointLongitude = oFenceItem.PointLongitude;
			CenterLattitude = oFenceItem.CenterLattitude;
			CenterLongitude = oFenceItem.CenterLongitude;
			Radius = oFenceItem.Radius;
			PolyPointsList = oPolyPointsList;
			IsActive = oFenceItem.IsActive;
			IsDeleted = oFenceItem.IsDeleted;
			ModifiedOn = oFenceItem.ModifiedOn;
		}

		public long GeoFenceID { get; set; }
		public string GeoFenceTypeId { get; set; }
		public string GeoFenceTypeUi { get; set; }
		public string ReportModeId { get; set; }
		public string ReportModeUi { get; set; }
		public long AccountId { get; set; }
		public string GeoFenceName { get; set; }
		public string GeoFenceDescription { get; set; }
		public string GeoFenceNameUi { get; set; }
		public double? MeanLattitude { get; set; }
		public double? MeanLongitude { get; set; }
		public short? GoogleMapZoomLevel { get; set; }
		public double? Area { get; set; }
		public double? MinLattitude { get; set; }
		public double? MinLongitude { get; set; }
		public double? MaxLattitude { get; set; }
		public double? MaxLongitude { get; set; }
		public string GeoFenceType { get; set; }
		public double? PointLatitude { get; set; }
		public double? PointLongitude { get; set; }
		public double? CenterLattitude { get; set; }
		public double? CenterLongitude { get; set; }
		public double? Radius { get; set; }
		public List<IFnsGsAccountGeoFencePolygons> PolyPointsList { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime ModifiedOn { get; set; }
	}
}
