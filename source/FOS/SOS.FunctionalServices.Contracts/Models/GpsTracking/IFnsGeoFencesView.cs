using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.GpsTracking
{
	public interface IFnsGeoFencesView
	{
		[DataMember]
		long GeoFenceID { get; set; }

		[DataMember]
		string GeoFenceTypeId { get; set; }

		[DataMember]
		string GeoFenceTypeUi { get; set; }

		[DataMember]
		string ReportModeId { get; set; }

		[DataMember]
		string ReportModeUi { get; set; }

		[DataMember]
		string GeoFenceType { get; set; }
		
		[DataMember]
		long AccountId { get; set; }

		[DataMember]
		string GeoFenceName { get; set; }

		[DataMember]
		string GeoFenceDescription { get; set; }

		[DataMember]
		string GeoFenceNameUi { get; set; }

		[DataMember]
		double? MeanLattitude { get; set; }

		[DataMember]
		double? MeanLongitude { get; set; }

		[DataMember]
		short? GoogleMapZoomLevel { get; set; }

		[DataMember]
		double? Area { get; set; }

		[DataMember]
		double? MinLattitude { get; set; }

		[DataMember]
		double? MinLongitude { get; set; }

		[DataMember]
		double? MaxLattitude { get; set; }

		[DataMember]
		double? MaxLongitude { get; set; }
		
		[DataMember]
		double? PointLatitude { get; set; }
		
		[DataMember]
		double? PointLongitude { get; set; }
		
		[DataMember]
		double? CenterLattitude { get; set; }
		
		[DataMember]
		double? CenterLongitude { get; set; }

		[DataMember]
		double? Radius { get; set; }

		[DataMember]
		List<IFnsGsAccountGeoFencePolygons> PolyPointsList { get; set; }
		
		[DataMember]
		bool IsActive { get; set; }
		
		[DataMember]
		bool IsDeleted { get; set; }

		[DataMember]
		DateTime ModifiedOn { get; set; }
	}
}