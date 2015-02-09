using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.GpsTracking
{
	public interface IGsAccountGeoFence
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
		short? ZoomLevel { get; set; }

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
		string GeoFenceType { get; set; }

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
		List<IGsAccountGeoFencePolygons> PolyPointsList { get; set; }

		[DataMember]
		bool IsActive { get; set; }

		[DataMember]
		bool IsDeleted { get; set; }

		[DataMember]
		DateTime ModifiedOn { get; set; }
	}

	#region Model

	public class GsAccountGeoFence : IGsAccountGeoFence
	{
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
		public short? ZoomLevel { get; set; }
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
		public List<IGsAccountGeoFencePolygons> PolyPointsList { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime ModifiedOn { get; set; }
	}

	#endregion Model
}
