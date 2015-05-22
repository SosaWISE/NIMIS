using NXS.Data.Crm;
using System;
using System.Collections.Generic;

namespace NXS.DataServices.Crm.Models
{
	public class IeAudit
	{
		public int ID { get; set; }
		public string LocationId { get; set; }
		public string LocationTypeId { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }

		public bool IsClosed { get; set; }

		internal static IeAudit FromDb(IE_Audit item, bool nullable = false)
		{
			if (item == null)
			{
				if (nullable)
					return null;
				else
					throw new Exception("Audit is null");
			}

			var result = new IeAudit();
			result.ID = item.ID;
			result.LocationId = item.LocationId;
			result.LocationTypeId = item.LocationTypeId;
			result.IsActive = item.IsActive;
			result.IsDeleted = item.IsDeleted;
			result.ModifiedOn = item.ModifiedOn;
			result.ModifiedBy = item.ModifiedBy;
			result.CreatedOn = item.CreatedOn;
			result.CreatedBy = item.CreatedBy;
			return result;
		}
	}
	public class IeAuditSave
	{
		public int ID { get; set; }
		public string LocationId { get; set; }
		public string LocationTypeId { get; set; }
		public DateTime ModifiedOn { get; set; }
		public List<string> Barcodes { get; set; }

		internal void ToDb(IE_Audit item)
		{
			if (item.ID != 0)
				throw new Exception("ID should be 0");

			item.LocationId = this.LocationId;
			item.LocationTypeId = this.LocationTypeId;
		}
	}
}
