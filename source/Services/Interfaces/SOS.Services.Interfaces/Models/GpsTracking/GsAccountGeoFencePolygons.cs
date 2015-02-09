﻿using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.GpsTracking
{

	public interface IGsAccountGeoFencePolygons
	{
		[DataMember]
		long GeoFencePolygonID { get; set; }

		[DataMember]
		long GeoFenceId { get; set; }

		[DataMember]
		int Sequence { get; set; }

		[DataMember]
		double Lattitude { get; set; }

		[DataMember]
		double Longitude { get; set; }

		[DataMember]
		DateTime CreatedOn { get; set; }

		[DataMember]
		string CreatedBy { get; set; }
	}

	#region Model

	public class GsAccountGeoFencePolygons : IGsAccountGeoFencePolygons
	{
		public long GeoFencePolygonID { get; set; }
		public long GeoFenceId { get; set; }
		public int Sequence { get; set; }
		public double Lattitude { get; set; }
		public double Longitude { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
	}

	public class GsSimplePoly
	{
		public long GeoFencePolygonID { get; set; }
		public long GeoFenceId { get; set; }
	}

	#endregion Model

}
