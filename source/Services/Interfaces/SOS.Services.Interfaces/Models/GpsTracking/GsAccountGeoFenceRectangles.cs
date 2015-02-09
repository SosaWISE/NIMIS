using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.GpsTracking
{
	public interface IGsAccountGeoFenceRectangles
	{
		[DataMemberAttribute]
		long GeoFenceID { get; set; }

		[DataMemberAttribute]
		long AccountId { get; set; }

		[DataMemberAttribute]
		double MeanLattitude { get; set; }

		[DataMemberAttribute]
		double MeanLongitude { get; set; }

		[DataMemberAttribute]
		double MaxLattitude { get; set; }

		[DataMemberAttribute]
		double MinLongitude { get; set; }

		[DataMemberAttribute]
		double MinLattitude { get; set; }

		[DataMemberAttribute]
		double MaxLongitude { get; set; }

		[DataMemberAttribute]
		double? Area { get; set; }

		[DataMemberAttribute]
		short? ZoomLevel { get; set; }

		[DataMemberAttribute]
		DateTime ModifiedOn { get; set; }

		[DataMemberAttribute]
		string ModifiedBy { get; set; }

		[DataMemberAttribute]
		DateTime CreatedOn { get; set; }

		[DataMemberAttribute]
		string CreatedBy { get; set; }
	}

	public class GsAccountGeoFenceRectangles : IGsAccountGeoFenceRectangles
	{
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
	}
}
