using NXS.Data.Crm;
using System;

namespace NXS.DataServices.Crm.Models
{
	public class MsPackageItem
	{
		public int ID { get; set; } // AccountPackageId
		public int AccountPackageId { get; set; }
		public string AccountPackageItemTypeId { get; set; }
		public string PackegeItemName { get; set; }
		public string ItemId { get; set; }
		public string ModelNumber { get; set; }
		public bool IsUpgrade { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public string CreatedBy { get; set; }
		public DateTime CreatedOn { get; set; }

		internal static MsPackageItem FromDb(MS_AccountPackageItem item, bool nullable = false)
		{
			if (item == null)
			{
				if (nullable)
					return null;
				else
					throw new Exception("team is null");
			}

			var result = new MsPackageItem();
			result.ID = item.ID;
			result.AccountPackageId = item.AccountPackageId;
			result.AccountPackageItemTypeId = item.AccountPackageItemTypeId;
			result.PackegeItemName = item.PackegeItemName;
			result.ItemId = item.ItemId;
			result.ModelNumber = item.ModelNumber;
			result.IsUpgrade = item.IsUpgrade;
			result.IsActive = item.IsActive;
			result.IsDeleted = item.IsDeleted;
			result.CreatedBy = item.CreatedBy;
			result.CreatedOn = item.CreatedOn;
			return result;
		}
	}
}
