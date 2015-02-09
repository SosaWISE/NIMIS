using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.CentralStation;

namespace SOS.FunctionalServices.Models.CentralStation
{
	public class FnsMsAccountEquipmentsView : IFnsMsAccountEquipmentsView
	{
		#region .ctor

		public FnsMsAccountEquipmentsView()
		{
		}
		public FnsMsAccountEquipmentsView(MS_AccountEquipmentsView view)
		{
			// MS_AccountEquipment
			AccountEquipmentID = view.AccountEquipmentID;
			AccountId = view.AccountId;
			EquipmentId = view.EquipmentId;
			EquipmentLocationId = view.EquipmentLocationId;
			GPEmployeeId = view.GPEmployeeId;
			//OfficeReconciliationItemId = view.OfficeReconciliationItemId;
			AccountEquipmentUpgradeTypeId = view.AccountEquipmentUpgradeTypeId;
			//CustomerLocation = view.CustomerLocation;
			Points = view.Points;
			ActualPoints = view.ActualPoints;
			Price = view.Price;
			IsExisting = view.IsExisting;
			BarcodeId = view.BarcodeId;
			IsServiceUpgrade = view.IsServiceUpgrade;
			IsExistingWiring = view.IsExistingWiring;
			IsMainPanel = view.IsMainPanel;
			// MS_AccountZoneAssignment
			AccountZoneAssignmentID = view.AccountZoneAssignmentID;
			AccountZoneTypeId = view.AccountZoneTypeId;
			AccountEventId = view.AccountEventId;
			Zone = view.Zone;
			Comments = view.Comments;
			//IsExisting = view.IsExisting;
			// readonly 
			ItemSKU = view.ItemSKU;
			ItemDesc = view.ItemDesc;
			AccountZoneType = view.AccountZoneType;
			EquipmentLocationDesc = view.EquipmentLocationDesc;
		}

		#endregion .ctor

		// MS_AccountEquipment
		public long AccountEquipmentID { get; set; }
		public long AccountId { get; set; }
		public string EquipmentId { get; set; }
		public int? EquipmentLocationId { get; set; }
		public string GPEmployeeId { get; set; }
		//public string OfficeReconciliationItemId {get;set;}
		public string AccountEquipmentUpgradeTypeId { get; set; }
		//public string CustomerLocation { get; set; }
		public int Points { get; set; }
		public double? ActualPoints { get; set; }
		public decimal Price { get; set; }
		public bool IsExisting { get; set; }
		public string BarcodeId { get; set; }
		public bool IsServiceUpgrade { get; set; }
		public bool IsExistingWiring { get; set; }
		public bool IsMainPanel { get; set; }
		// MS_AccountZoneAssignment
		public long AccountZoneAssignmentID { get; set; }
		public string AccountZoneTypeId { get; set; }
		public int? AccountEventId { get; set; }
		public string Zone { get; set; }
		public string Comments { get; set; }
		//public bool IsExisting { get; set; }
		// readonly 
		public string ItemSKU { get; set; }
		public string ItemDesc { get; set; }
		public string AccountZoneType { get; set; }
		public string EquipmentLocationDesc { get; set; }
	}
}
