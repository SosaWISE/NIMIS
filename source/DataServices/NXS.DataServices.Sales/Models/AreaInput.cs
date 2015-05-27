using Newtonsoft.Json;
using NXS.Data;
using NXS.Data.Sales;
using System;

namespace NXS.DataServices.Sales.Models
{
	public class AreaInput
	{
		public AreaData[][] AreaData { get; set; }
		public int AreaId { get; set; }
		public string AreaName { get; set; }
		public int OfficeId { get; set; }
		public string RepCompanyID { get; set; }
		public DateTime? StartTime { get; set; }

		public void ToDb(SL_Area item)
		{
			if (this.AreaData.Length == 0)
				throw new Exception("Empty areaData");

			decimal minLat, maxLat, minLng, maxLng;
			minLat = minLng = decimal.MaxValue;
			maxLat = maxLng = decimal.MinValue;
			foreach (var path in this.AreaData)
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
				throw new Exception("Invalid areaData");
			}

			item.AreaName = DatabaseHelper.db_null_or_string(this.AreaName);
			item.PointData = JsonConvert.SerializeObject(this.AreaData);
			item.MinLatitude = minLat;
			item.MaxLatitude = maxLat;
			item.MinLongitude = minLng;
			item.MaxLongitude = maxLng;
			item.IsActive = true;
		}

		public void ToDb(SL_AreaAssignment item)
		{
			item.AreaId = this.AreaId;
			item.OfficeId = this.OfficeId;
			item.RepCompanyID = this.RepCompanyID;
			item.StartTime = this.StartTime.HasValue ? this.StartTime.Value : DateTime.UtcNow;
			item.EndTime = null;
			item.IsActive = true;
		}
	}
	public class AreaData
	{
		public decimal lat { get; set; }
		public decimal lng { get; set; }
	}
}
