using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.CentralStation
{
	public interface IFnsMsEquipmentsView
	{

		[DataMember]
		 string EquipmentID { get;set;}
	
        [DataMember]
		int? EquipmentMonitoredTypeId { get;set;}
		
        [DataMember]
		int? EquipmentTypeId { get;set; }
		
        [DataMember]
	    string AccountZoneTypeId { get;set;}
		
        [DataMember]
		int? EquipmentPanelTypeId { get;set;}
		
        [DataMember]
		 string GPItemNmbr { get;set;}
		

		[DataMember]
		 string ItemDescription { get;set;}
		
		[DataMember]
		 string ShortName { get;set;}
		
        [DataMember]
		 string GenDescription { get;set;}

		[DataMember]
		 string FullName { get;set;}

		[DataMember]
		 bool ShowInInventory { get;set;}

		[DataMember]
		 byte Points { get;set;}


		[DataMember]
		double? ActualPoints { get;set;}

		[DataMember]
		 decimal RetailPrice { get;set;}

		[DataMember]
		 bool? IsCellUnit { get;set;}


		[DataMember]
		 int? AuditDay { get;set;}


		[DataMember]
		 decimal? EmployeeCost { get;set;}
		
        [DataMember]
		 int? DefaultTechStockLevel { get;set;}

		[DataMember]
		 bool? IsHighlighted {get;set; }
		
        [DataMember]
		 bool? IsWireless { get;set;}


		[DataMember]
		 bool IsActive { get;set; }
		
		[DataMember]
        bool IsDeleted { get; set; }

	
	}
}