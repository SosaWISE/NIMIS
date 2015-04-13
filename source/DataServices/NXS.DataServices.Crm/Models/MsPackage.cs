using NXS.Data.Crm;
using System;
using System.Collections.Generic;

namespace NXS.DataServices.Crm.Models
{
	public class MsPackage
	{
		public int ID { get; set; } // AccountPackageID
		public string AccountPackageName { get; set; }
		public string ShortName { get; set; }
		public string Description { get; set; }
		public decimal BasePoints { get; set; }
		public decimal BaseRMR { get; set; }
		public decimal MinRMR { get; set; }
		public decimal MaxRMR { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public string CreatedBy { get; set; }
		public DateTime CreatedOn { get; set; }

		public List<MsPackageItem> PackageItems { get; set; }

		internal static MsPackage FromDb(MS_AccountPackage item, bool nullable = false)
		{
			if (item == null)
			{
				if (nullable)
					return null;
				else
					throw new Exception("team is null");
			}

			var result = new MsPackage();
			result.ID = item.ID;
			result.AccountPackageName = item.AccountPackageName;
			result.ShortName = item.ShortName;
			result.Description = item.Description;
			result.BasePoints = item.BasePoints;
			result.BaseRMR = item.BaseRMR;
			result.MinRMR = item.MinRMR;
			result.MaxRMR = item.MaxRMR;
			result.IsActive = item.IsActive;
			result.IsDeleted = item.IsDeleted;
			result.CreatedBy = item.CreatedBy;
			result.CreatedOn = item.CreatedOn;

			result.PackageItems = (item.PackageItems == null)
				? new List<MsPackageItem>() : item.PackageItems.ConvertAll(a => MsPackageItem.FromDb(a));

			return result;
		}
	}
}
