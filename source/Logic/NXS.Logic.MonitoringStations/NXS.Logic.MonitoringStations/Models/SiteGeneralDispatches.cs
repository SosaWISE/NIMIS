using System.Xml.Serialization;

namespace NXS.Logic.MonitoringStations.Models
{
	public class SiteGeneralDispatches
	{
		#region Properties

		[XmlAttribute(DataType = "string", AttributeName = "instructions")]
		public string Instructions { get; set; }

		#endregion Properties
	}
}
