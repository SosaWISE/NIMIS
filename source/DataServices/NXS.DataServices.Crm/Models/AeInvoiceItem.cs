using NXS.Data.Crm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NXS.DataServices.Crm.Models
{
	public class AeInvoiceItem
	{
		//public long InvoiceItemID { get; set; }
		public long ID { get; set; }
		public long InvoiceId { get; set; }
		public string ItemId { get; set; }
		public string ProductBarcodeId { get; set; }
		public long? AccountEquipmentId { get; set; }
		public string TaxOptionId { get; set; }
		public short Qty { get; set; }
		public decimal Cost { get; set; }
		public decimal RetailPrice { get; set; }
		public decimal? PriceWithTax { get; set; }
		public decimal SystemPoints { get; set; }
		public string SalesmanId { get; set; }
		public string TechnicianId { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }

		internal static AeInvoiceItem FromDb(AE_InvoiceItem item, bool nullable = false)
		{
			if (item == null)
			{
				if (nullable)
					return null;
				else
					throw new Exception("invoice item is null");
			}

			var result = new AeInvoiceItem();
			result.ID = item.ID;
			result.InvoiceId = item.InvoiceId;
			result.ItemId = item.ItemId;
			result.ProductBarcodeId = item.ProductBarcodeId;
			result.AccountEquipmentId = item.AccountEquipmentId;
			result.TaxOptionId = item.TaxOptionId;
			result.Qty = item.Qty;
			result.Cost = item.Cost;
			result.RetailPrice = item.RetailPrice;
			result.PriceWithTax = item.PriceWithTax;
			result.SystemPoints = item.SystemPoints;
			result.SalesmanId = item.SalesmanId;
			result.TechnicianId = item.TechnicianId;
			result.IsActive = item.IsActive;
			result.IsDeleted = item.IsDeleted;
			result.ModifiedOn = item.ModifiedOn;
			result.ModifiedBy = item.ModifiedBy;
			result.CreatedOn = item.CreatedOn;
			result.CreatedBy = item.CreatedBy;
			return result;
		}

		internal void ToDb(AE_InvoiceItem item)
		{
			if (item.ID != this.ID)
				throw new Exception("IDs don't match");
			NXS.Data.VersionException.CheckModifiedOn(item.ModifiedOn, this.ModifiedOn);

			item.ID = this.ID;
			item.InvoiceId = this.InvoiceId;
			item.ItemId = this.ItemId;
			item.ProductBarcodeId = this.ProductBarcodeId;
			item.AccountEquipmentId = this.AccountEquipmentId;
			item.TaxOptionId = this.TaxOptionId;
			item.Qty = this.Qty;
			item.Cost = this.Cost;
			item.RetailPrice = this.RetailPrice;
			item.PriceWithTax = this.PriceWithTax;
			item.SystemPoints = this.SystemPoints;
			item.SalesmanId = this.SalesmanId;
			item.TechnicianId = this.TechnicianId;
			item.IsActive = this.IsActive;
			item.IsDeleted = this.IsDeleted;
			//item.ModifiedOn = this.ModifiedOn;
			//item.ModifiedBy = this.ModifiedBy;
			//item.CreatedOn = this.CreatedOn;
			//item.CreatedBy = this.CreatedBy;
		}
	}
}
