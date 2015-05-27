using NXS.Data.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NXS.DataServices.Sales.Models
{
	public class SlAreaAssignment
	{
		public int ID { get; set; }
		public int AreaId { get; set; }
		public int OfficeId { get; set; }
		public string RepCompanyID { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime? EndTime { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }

		internal static SlAreaAssignment FromDb(SL_AreaAssignment item, bool nullable = false)
		{
			if (item == null)
			{
				if (nullable)
					return null;
				else
					throw new Exception("AreaAssignment is null");
			}

			var result = new SlAreaAssignment();
			result.ID = item.ID;
			result.AreaId = item.AreaId;
			result.OfficeId = item.OfficeId;
			result.RepCompanyID = item.RepCompanyID;
			result.StartTime = item.StartTime;
			result.EndTime = item.EndTime;
			result.IsActive = item.IsActive;
			result.IsDeleted = item.IsDeleted;
			result.ModifiedOn = item.ModifiedOn;
			result.ModifiedBy = item.ModifiedBy;
			result.CreatedOn = item.CreatedOn;
			result.CreatedBy = item.CreatedBy;
			return result;
		}
	}
}
