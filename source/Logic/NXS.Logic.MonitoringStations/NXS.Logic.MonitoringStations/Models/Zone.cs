using System.Xml.Serialization;

namespace NXS.Logic.MonitoringStations.Models
{
	public class Zone
	{
		#region Properties

		[XmlAttribute(DataType = "string", AttributeName = "zone_id")]
		public string ZoneId { get; set; }


		[XmlAttribute(DataType = "string", AttributeName = "zonestate_id")]
		public string ZoneStateId { get; set; }


		[XmlAttribute(DataType = "string", AttributeName = "event_id")]
		public string EventId { get; set; }


		[XmlAttribute(DataType = "string", AttributeName = "equiploc_id")]
		public string EquipmentLocationId { get; set; }


		[XmlAttribute(DataType = "string", AttributeName = "equiptype_id")]
		public string EquipmentTypeId { get; set; }

        [XmlAttribute(DataType = "string", AttributeName = "zone_comment")]
        public string ZoneComment { get; set; }

		#endregion Properties
	}
}