using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.CentralStation;

namespace SOS.FunctionalServices.Models.CentralStation
{
	public class FnsMsEquipmentsView : IFnsMsEquipmentsView
	{
		#region .ctor

		public FnsMsEquipmentsView(Action<IFnsMsEquipmentsView, object> fxBindData, object value)
		{
			if (fxBindData == null)
				throw new NotImplementedException();

			fxBindData(this, value);
		}

        public FnsMsEquipmentsView(MS_EquipmentsView view)
		{
		    EquipmentID = view.EquipmentID;
            EquipmentMonitoredTypeId = view.EquipmentMonitoredTypeId;
            EquipmentTypeId= view.EquipmentTypeId;
            AccountZoneTypeId = view.AccountZoneTypeId;
            EquipmentPanelTypeId = view.EquipmentPanelTypeId;
            GPItemNmbr = view.GPItemNmbr;
            ItemDescription = view.ItemDescription;
            ShortName  = view.ShortName;
            GenDescription= view.GenDescription;
            FullName= view.FullName;
            ShowInInventory = view.ShowInInventory;
            ActualPoints = view.ActualPoints;
            RetailPrice=view.RetailPrice;
            IsCellUnit=view.IsCellUnit;
            AuditDay=view.AuditDay;
            EmployeeCost=view.EmployeeCost;
            DefaultTechStockLevel=view.DefaultTechStockLevel;
            IsHighlighted=view.IsHighlighted;
            IsWireless=view.IsWireless;
            IsActive= view.IsActive;
            IsDeleted = view.IsDeleted;

		}

		#endregion .ctor

		#region Properties

        public string EquipmentID { get; set; }


        public int? EquipmentMonitoredTypeId { get; set; }


        public int? EquipmentTypeId { get; set; }


        public string AccountZoneTypeId { get; set; }


        public int? EquipmentPanelTypeId { get; set; }


        public string GPItemNmbr { get; set; }



        public string ItemDescription { get; set; }


        public string ShortName { get; set; }


        public string GenDescription { get; set; }


        public string FullName { get; set; }


        public bool ShowInInventory { get; set; }


        public byte Points { get; set; }



        public double? ActualPoints { get; set; }


        public decimal RetailPrice { get; set; }


        public bool? IsCellUnit { get; set; }



        public int? AuditDay { get; set; }



        public decimal? EmployeeCost { get; set; }


        public int? DefaultTechStockLevel { get; set; }


        public bool? IsHighlighted { get; set; }


        public bool? IsWireless { get; set; }



        public bool IsActive { get; set; }


        public  bool IsDeleted { get; set; }

	
		#endregion Properties
	}
}
