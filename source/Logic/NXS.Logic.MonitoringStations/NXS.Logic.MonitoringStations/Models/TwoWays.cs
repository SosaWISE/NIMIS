using System.Xml.Serialization;

namespace NXS.Logic.MonitoringStations.Models
{
	public class Twoways : Base
	{
		#region .ctor

		public Twoways(TwowayInfo twoway)
		{
			Twoway = twoway;
		}

		public Twoways()
		{
		}

		#endregion .ctor

		#region Properties

		public TwowayInfo Twoway { get; set; }

		#endregion Properties

		#region Methods

		public string Serialize()
		{
			return Serialize<Twoways>();
		}

		#endregion Methods
	}

	public class TwowayInfo
	{
		#region Properties

		[XmlAttribute(DataType = "string", AttributeName = "twoway_device_id")]
		public string TwoWayDeviceId { get; set; }

		#endregion Properties

	}
}
