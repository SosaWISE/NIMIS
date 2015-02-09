using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.GpsTracking
{
	public interface IFnsGsAccountGeoFenceRectangles
	{
		[DataMember]
		long GeoFenceID { get; set; }

		[DataMember]
		long AccountId { get; set; }

		[DataMember]
		double MeanLattitude { get; set; }

		[DataMember]
		double MeanLongitude { get; set; }

		[DataMember]
		double MaxLattitude { get; set; }

		[DataMember]
		double MinLongitude { get; set; }

		[DataMember]
		double MinLattitude { get; set; }

		[DataMember]
		double MaxLongitude { get; set; }

		[DataMember]
		double? Area { get; set; }

		[DataMember]
		short? ZoomLevel { get; set; }

		[DataMember]
		DateTime ModifiedOn { get; set; }

		[DataMember]
		string ModifiedBy { get; set; }

		[DataMember]
		DateTime CreatedOn { get; set; }

		[DataMember]
		string CreatedBy { get; set; }
	}
}