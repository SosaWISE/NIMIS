using Newtonsoft.Json;
using NXS.Data;
using NXS.Data.Sales;
using System;

namespace NXS.DataServices.Sales.Models
{
	public class SlArea
	{
		public int ID { get; set; }
		public string Name { get; set; }
		//public decimal MinLatitude { get; set; }
		//public decimal MinLongitude { get; set; }
		//public decimal MaxLatitude { get; set; }
		//public decimal MaxLongitude { get; set; }
		//public string PointData { get; set; }
		public AreaPoint[][] Paths { get; set; }
		public int TeamId { get; set; }
		public string RepCompanyID { get; set; }
		public DateTime? StartTime { get; set; }
		public DateTime? EndTime { get; set; }
		//public bool IsActive { get; set; }
		//public bool IsDeleted { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		//public DateTime CreatedOn { get; set; }
		//public string CreatedBy { get; set; }

		public void ToDb(SL_Area item)
		{
			if (this.Paths.Length == 0)
				throw new Exception("Empty Paths");

			decimal minLat, maxLat, minLng, maxLng;
			minLat = minLng = decimal.MaxValue;
			maxLat = maxLng = decimal.MinValue;
			foreach (var path in this.Paths)
			{
				foreach (var point in path)
				{
					if (point.lat < minLat) minLat = point.lat;
					if (maxLat < point.lat) maxLat = point.lat;
					if (point.lng < minLng) minLng = point.lng;
					if (maxLng < point.lng) maxLng = point.lng;
				}
			}
			if (minLat == decimal.MaxValue || minLng == decimal.MaxValue ||
				maxLat == decimal.MinValue || maxLng == decimal.MinValue)
			{
				throw new Exception("Invalid Paths");
			}

			item.Name = DatabaseHelper.db_null_or_string(this.Name);
			item.PointData = JsonConvert.SerializeObject(this.Paths);
			item.MinLatitude = minLat;
			item.MaxLatitude = maxLat;
			item.MinLongitude = minLng;
			item.MaxLongitude = maxLng;
			item.TeamId = this.TeamId;
			item.RepCompanyID = this.RepCompanyID;
			item.StartTime = this.StartTime;
			item.EndTime = this.EndTime;
			item.IsActive = true;
		}

		internal static SlArea FromDb(SL_Area item, bool nullable = false)
		{
			if (item == null)
			{
				if (nullable)
					return null;
				else
					throw new Exception("AreaAssignment is null");
			}

			var result = new SlArea();
			result.ID = item.ID;
			result.Name = item.Name;
			result.Paths = JsonConvert.DeserializeObject<AreaPoint[][]>(item.PointData);
			result.TeamId = item.TeamId;
			result.RepCompanyID = item.RepCompanyID;
			result.StartTime = item.StartTime;
			result.EndTime = item.EndTime;
			//result.IsActive = item.IsActive;
			//result.IsDeleted = item.IsDeleted;
			result.ModifiedOn = item.ModifiedOn;
			result.ModifiedBy = item.ModifiedBy;
			//result.CreatedOn = item.CreatedOn;
			//result.CreatedBy = item.CreatedBy;
			return result;
		}
	}
	public class AreaPoint
	{
		public decimal lat { get; set; }
		public decimal lng { get; set; }
	}
}
