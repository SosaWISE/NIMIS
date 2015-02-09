using System.Xml.Serialization;

namespace NXS.Logic.MonitoringStations.Models
{
	public class OnTests : Base
	{
		#region Properties

		public MasterMindTest OnTest { get; set; }

		#endregion Properties

		#region Methods

		public string Serialize()
		{
			return Serialize<OnTests>();
		}

		#endregion Methods
	}

	public class MasterMindTest
	{
		#region Properties

		[XmlAttribute(DataType = "string", AttributeName = "onoff_flag")]
		public string OnOffFlag { get; set; }

		[XmlAttribute(DataType = "string", AttributeName = "testcat_id")]
		public string TestCategoryId { get; set; }

		[XmlAttribute(DataType = "string", AttributeName = "test_hours")]
		public string TestHours { get; set; }

		[XmlAttribute(DataType = "string", AttributeName = "test_minutes")]
		public string TestMinutes { get; set; }

		#endregion Properties
	}
}
