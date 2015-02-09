using System.Xml.Serialization;

namespace NXS.Logic.MonitoringStations.Models
{
	public class OutOfServices : Base
	{
		#region Properties

		public OutOfServiceInfo OutOfService { get; set; }

		#endregion Properties

		#region Methods

		public string Serialize()
		{
			return Serialize<OutOfServices>();
		}

		#endregion Methods
	}

	public class OutOfServiceInfo
	{
		#region Properties

		[XmlAttribute(DataType = "string", AttributeName = "status_flag")]
		public string StatusFlag { get; set; }

		[XmlAttribute(DataType = "string", AttributeName = "ooscat_id")]
		public string OosCatId { get; set; }

		#endregion Properties
	}
}