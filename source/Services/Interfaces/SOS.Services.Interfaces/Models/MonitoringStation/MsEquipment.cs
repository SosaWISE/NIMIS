using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.MonitoringStation
{
	public class MsEquipment : IMsEquipment
	{
		public string EquipmentID { get; set; }
		public string ShortName { get; set; }
		public int? EquipmentTypeId { get; set; }
	}

    public interface IMsEquipment
	{
		[DataMember]
        string EquipmentID { get; set; }

		[DataMember]
		string ShortName { get; set; }

		[DataMember]
		int? EquipmentTypeId { get; set; }
	}
}
