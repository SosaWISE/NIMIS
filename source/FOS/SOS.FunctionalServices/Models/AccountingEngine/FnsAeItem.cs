using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.AccountingEngine;

namespace SOS.FunctionalServices.Models.AccountingEngine
{
	public class FnsAeItem : IFnsAeItem
	{
		#region .ctor
		public FnsAeItem(AE_Item item)
		{
			ItemID = item.ItemID;
			ItemTypeId = item.ItemTypeId;
			TaxOptionId = item.TaxOptionId;
			ModelNumber = item.ModelNumber;
			VerticalId = item.VerticalId;
			ItemSKU = item.ItemSKU;
			ItemDesc = item.ItemDesc;
			Price = item.Price;
			Cost = item.Cost;
			SystemPoints = item.SystemPoints;
			IsCatalogItem = item.IsCatalogItem;
			IsActive = item.IsActive;
			IsDeleted = item.IsDeleted;
			ModifiedOn = item.ModifiedOn;
			ModifiedBy = item.ModifiedBy;
			CreatedOn = item.CreatedOn;
			CreatedBy = item.CreatedBy;
		}
		#endregion .ctor

		#region Properties
		public string ItemID { get; set; }
		public string ItemTypeId { get; set; }
		public string TaxOptionId { get; set; }
		public string VerticalId { get; set; }
		public string ModelNumber { get; set; }
		public string ItemSKU { get; set; }
		public string ItemDesc { get; set; }
		public decimal Price { get; set; }
		public decimal Cost { get; set; }
		public decimal SystemPoints { get; set; }
		public bool IsCatalogItem { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
		#endregion Properties
	}
}
