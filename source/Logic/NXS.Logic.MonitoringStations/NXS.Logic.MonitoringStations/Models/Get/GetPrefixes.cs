using System.Xml.Serialization;

namespace NXS.Logic.MonitoringStations.Models.Get
{
	public class GetPrefixes : Base
	{
		#region Properties
		public Prefix GetPrefix { get; set; }
		#endregion Properties

		#region Methods

		public string Serialize()
		{
			return Serialize<GetPrefixes>();
		}

		#endregion Methods
	}

	public class Prefix
	{
		#region Properties

		[XmlAttribute(DataType = "string", AttributeName = "cs_no")]
		public string CsNo { get; set; }

		#endregion Properties
	}
}
