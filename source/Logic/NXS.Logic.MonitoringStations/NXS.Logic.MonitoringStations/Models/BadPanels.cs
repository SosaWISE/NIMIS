using System.Xml.Serialization;

namespace NXS.Logic.MonitoringStations.Models
{
	public class BadPanels : Base
	{
		#region Properties
		public BadPanelInfo BadPanel { get; set; }
		#endregion Properties

		#region Methods

		public string Serialize()
		{
			return Serialize<BadPanels>();
		}
		#endregion Methods
	}


	public class BadPanelInfo
	{
		#region Properties

		[XmlAttribute(DataType = "string", AttributeName = "new_cs_no")]
		public string NewCsNo { get; set; }

		#endregion Properties
	}
}
