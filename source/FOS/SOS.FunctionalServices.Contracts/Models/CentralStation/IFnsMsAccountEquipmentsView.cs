using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.CentralStation
{
	public interface IFnsMsAccountEquipmentsView
	{
		//long AccountZoneAssignmentID { get; set; }
		//long AccountEquipmentID { get; set; }
		//long AccountId { get; set; }
		//string EquipmentId { get; set; }
		//string ItemSKU { get; set; }
		//string ItemDesc { get; set; }
		//string Zone { get; set; }
		//string AccountZoneTypeId { get; set; }
		//int? EquipmentLocationId { get; set; }
		//string GPEmployeeId { get; set; }
		//string AccountEquipmentUpgradeTypeId { get; set; }
		//double? ActualPoints { get; set; }
		//decimal Price { get; set; }
		//bool IsExisting { get; set; }
		//string BarcodeId { get; set; }
		//bool IsServiceUpgrade { get; set; }
		//bool IsExistingWiring { get; set; }
		//bool IsMainPanel { get; set; }
		//string EquipmentLocationDesc { get; set; }
		//string AccountZoneType { get; set; }

		// MS_AccountEquipment
		long AccountEquipmentID { get; set; }
		long AccountId { get; set; }
		string EquipmentId { get; set; }
		int? EquipmentLocationId { get; set; }
		string GPEmployeeId { get; set; }
		//string OfficeReconciliationItemId {get;set;}
		string AccountEquipmentUpgradeTypeId { get; set; }
		//string CustomerLocation { get; set; }
		int Points { get; set; }
		double? ActualPoints { get; set; }
		decimal Price { get; set; }
		bool IsExisting { get; set; }
		string BarcodeId { get; set; }
		bool IsServiceUpgrade { get; set; }
		bool IsExistingWiring { get; set; }
		bool IsMainPanel { get; set; }
		// MS_AccountZoneAssignment
		long AccountZoneAssignmentID { get; set; }
		string AccountZoneTypeId { get; set; }
		int? AccountEventId { get; set; }
		string Zone { get; set; }
		string Comments { get; set; }
		//bool IsExisting { get; set; }
		// readonly 
		string ItemSKU { get; set; }
		string ItemDesc { get; set; }
		string AccountZoneType { get; set; }
		string EquipmentLocationDesc { get; set; }
	}
}