using System.Xml.Serialization;

namespace NXS.Logic.MonitoringStations.Models.Get
{
	public class GetAgencies : Base
	{
		#region Properties
		public AgencyInfo GetAgency { get; set; } 
		#endregion Properties

		#region Methods

		public string Serialize()
		{
			return Serialize<GetAgencies>();
		}

		#endregion Methods
	}

	public class AgencyInfo
	{
		#region Properties
		[XmlAttribute(DataType = "string", AttributeName = "agency_type_id")]
		public string AgencyTypeId { get; set; }

		[XmlAttribute(DataType = "string", AttributeName = "phone1")]
		public string Phone1 { get; set; }

		[XmlAttribute(DataType = "string", AttributeName = "zip_code")]
		public string ZipCode { get; set; }

		#endregion Properties

	}
}
