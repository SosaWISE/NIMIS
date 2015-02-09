using System.Xml.Serialization;

namespace NXS.Logic.MonitoringStations.Models.Get
{
	public class GetZipCodes : Base
	{
		public ZipCode GetZipCode { get; set; }
	}

	public class ZipCode
	{
		#region Properties

		[XmlAttribute(DataType = "string", AttributeName = "zip_code")]
		public string PostalCode { get; set; }

		[XmlAttribute(DataType = "string", AttributeName = "city_name")]
		public string CityName { get; set; }

		[XmlAttribute(DataType = "string", AttributeName = "county_name")]
		public string CountyName { get; set; }

		[XmlAttribute(DataType = "string", AttributeName = "state_id")]
		public string StateId { get; set; }

		#endregion Properties
	}
}
