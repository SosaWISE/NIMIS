using NXS.Data.Crm;
using System;

namespace NXS.DataServices.Crm.Models
{
	public class AeItem
	{
		//public string ItemID { get; set; }
		public string ID { get; set; }
		public string ItemTypeId { get; set; }
		public string TaxOptionId { get; set; }
		public string AccountZoneTypeId { get; set; }
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
		//public DateTime ModifiedOn { get; set; }
		//public string ModifiedBy { get; set; }
		//public DateTime CreatedOn { get; set; }
		//public string CreatedBy { get; set; }

		internal static AeItem FromDb(AE_Item item, bool nullable = false)
		{
			if (item == null)
			{
				if (nullable)
					return null;
				else
					throw new Exception("item is null");
			}

			var result = new AeItem();
			result.ID = item.ID.Trim(); // ensure no trailing space since sql doesn't use it when comparing
			result.ItemTypeId = item.ItemTypeId.Trim();
			result.TaxOptionId = item.TaxOptionId;
			result.AccountZoneTypeId = item.AccountZoneTypeId;
			result.VerticalId = item.VerticalId;
			result.ModelNumber = item.ModelNumber;
			result.ItemSKU = item.ItemSKU;
			result.ItemDesc = item.ItemDesc;
			result.Price = item.Price;
			result.Cost = item.Cost;
			result.SystemPoints = item.SystemPoints;
			result.IsCatalogItem = item.IsCatalogItem;
			result.IsActive = item.IsActive;
			result.IsDeleted = item.IsDeleted;
			//result.ModifiedOn = item.ModifiedOn;
			//result.ModifiedBy = item.ModifiedBy;
			//result.CreatedOn = item.CreatedOn;
			//result.CreatedBy = item.CreatedBy;
			return result;
		}
	}
}
