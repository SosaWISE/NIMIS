using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.MonitoringStation
{
	public class MsAccountEquipment : IMsAccountEquipment
	{
		public long? AccountZoneAssignmentID { get; set; }
		public long AccountEquipmentID { get; set; }
		public long AccountId { get; set; }
		public string EquipmentId { get; set; }
		public string ItemSKU { get; set; }
		public string ItemDesc { get; set; }
		public string Zone { get; set; }

        public string Comments { get; set; }
      

		public string AccountZoneTypeId { get; set; }
		public string ZoneEventTypeId { get; set; }

        public int? AccountEventId { get; set; }
		public int? EquipmentLocationId { get; set; }
		public string GPEmployeeId { get; set; }
		public string AccountEquipmentUpgradeTypeId { get; set; }

        public decimal Points { get; set; }
		public decimal? ActualPoints { get; set; }
		public decimal Price { get; set; }
		public bool IsExisting { get; set; }
		public string BarcodeId { get; set; }
		public bool IsServiceUpgrade { get; set; }
		public bool IsExistingWiring { get; set; }
		public bool IsMainPanel { get; set; }
		public string EquipmentLocationDesc { get; set; }
		public string AccountZoneType { get; set; }
	}

	public interface IMsAccountEquipment
	{
		long AccountEquipmentID { get; set; }
		long AccountId { get; set; }
		string EquipmentId { get; set; }
		string ItemSKU { get; set; }
		string ItemDesc { get; set; }
		string Zone { get; set; }
        string Comments { get; set; }
		string AccountZoneTypeId { get; set; }
		string ZoneEventTypeId { get; set; }
		int? EquipmentLocationId { get; set; }
		string GPEmployeeId { get; set; }
		string AccountEquipmentUpgradeTypeId { get; set; }
        decimal Points { get; set; }
		decimal? ActualPoints { get; set; }
		decimal Price { get; set; }
		bool IsExisting { get; set; }
		string BarcodeId { get; set; }
		bool IsServiceUpgrade { get; set; }
		bool IsExistingWiring { get; set; }
		bool IsMainPanel { get; set; }
		string EquipmentLocationDesc { get; set; }
		string AccountZoneType { get; set; }

		long? AccountZoneAssignmentID { get; set; }


	}
}