using System.Xml.Serialization;

namespace NXS.Logic.MonitoringStations.Models
{

	public class SiteOption
	{
		#region Properties

		[XmlAttribute(DataType = "string", AttributeName = "option_id")]
		public string OptionId { get; set; }


		[XmlAttribute(DataType = "string", AttributeName = "option_value")]
		public string OptionValue { get; set; }

		#endregion Properties
	}
}
