using System.Xml.Serialization;

namespace NXS.Logic.MonitoringStations.Models
{
	public class SiteAgencyPermit
	{
		#region Properties

		[XmlAttribute(DataType = "string", AttributeName = "agencytype_id")]
		public string AgencyTypeId { get; set; }


		[XmlAttribute(DataType = "string", AttributeName = "phone1")]
		public string Phone1 { get; set; }


		[XmlAttribute(DataType = "string", AttributeName = "permit_no")]
		public string PermitNo { get; set; }


		[XmlAttribute(DataType = "string", AttributeName = "permtype_id")]
		public string PermTypeId { get; set; }


		[XmlAttribute(DataType = "string", AttributeName = "effective_date")]
		public string EffectiveDate { get; set; }


		[XmlAttribute(DataType = "string", AttributeName = "expire_date")]
		public string ExpireDate { get; set; }

        [XmlAttribute(DataType = "string", AttributeName = "agency_no")]
        public string AgencyNo { get; set; }

		#endregion Properties
	}
}
