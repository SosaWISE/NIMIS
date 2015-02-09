using System.Xml.Serialization;

namespace NXS.Logic.MonitoringStations.Models
{
	public class SiteSystem
	{
		#region Properties
		[XmlAttribute(DataType = "string", AttributeName = "site_name")]
		public string SiteName { get; set; }

		[XmlAttribute(DataType = "string", AttributeName = "site_addr1")]
		public string SiteAddr1 { get; set; }

		[XmlAttribute(DataType = "string", AttributeName = "city_name")]
		public string CityName { get; set; }

		[XmlAttribute(DataType = "string", AttributeName = "state_id")]
		public string StateId { get; set; }

		[XmlAttribute(DataType = "string", AttributeName = "county_name")]
		public string CountyName { get; set; }

		[XmlAttribute(DataType = "string", AttributeName = "zip_code")]
		public string ZipCode { get; set; }

		[XmlAttribute(DataType = "string", AttributeName = "phone1")]
		public string Phone1 { get; set; }

		[XmlAttribute(DataType = "string", AttributeName = "sitetype_id")]
		public string SiteTypeId { get; set; }

		[XmlAttribute(DataType = "string", AttributeName = "sitestat_id")]
		public string SiteStateId { get; set; }


		[XmlAttribute(DataType = "string", AttributeName = "codeword1")]
		public string CodeWord1 { get; set; }


		[XmlAttribute(DataType = "string", AttributeName = "servco_no")]
		public string ServiceNo { get; set; }


		[XmlAttribute(DataType = "string", AttributeName = "install_servco_no")]
		public string InstallServiceNo { get; set; }


		[XmlAttribute(DataType = "string", AttributeName = "cspart_no")]
		public string CsPartNo { get; set; }


		[XmlAttribute(DataType = "string", AttributeName = "cross_street")]
		public string CrossStreet { get; set; }


		[XmlAttribute(DataType = "string", AttributeName = "systype_id")]
		public string SysTypeId { get; set; }

		[XmlAttribute(DataType = "string", AttributeName = "sec_systype_id")]
		public string SecSysTypeId { get; set; }


		[XmlAttribute(DataType = "string", AttributeName = "receiver_phone")]
		public string ReceiverPhone { get; set; }


		[XmlAttribute(DataType = "string", AttributeName = "panel_phone")]
		public string PanelPhone { get; set; }


		[XmlAttribute(DataType = "string", AttributeName = "panel_location")]
		public string PanelLocation { get; set; }


		[XmlAttribute(DataType = "string", AttributeName = "install_date")]
		public string InstallDate { get; set; }


		[XmlAttribute(DataType = "string", AttributeName = "panel_code")]
		public string PanelCode { get; set; }

		[XmlAttribute(DataType = "string", AttributeName = "twoway_device_id")]
		public string TwoWayDeviceId { get; set; }

		#endregion Properties
	}
}
