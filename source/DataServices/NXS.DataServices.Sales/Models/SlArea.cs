using NXS.Data.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NXS.DataServices.Sales.Models
{
	public class SlArea
	{
		public int ID { get; set; }
		public string AreaName { get; set; }
		public decimal MinLatitude { get; set; }
		public decimal MinLongitude { get; set; }
		public decimal MaxLatitude { get; set; }
		public decimal MaxLongitude { get; set; }
		public string PointData { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }

		internal static SlArea FromDb(SL_Area item, bool nullable = false)
		{
			if (item == null)
			{
				if (nullable)
					return null;
				else
					throw new Exception("Area is null");
			}

			var result = new SlArea();
			result.ID = item.ID;
			result.AreaName = item.AreaName;
			result.MinLatitude = item.MinLatitude;
			result.MinLongitude = item.MinLongitude;
			result.MaxLatitude = item.MaxLatitude;
			result.MaxLongitude = item.MaxLongitude;
			result.PointData = item.PointData;
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
