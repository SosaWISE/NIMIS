using Newtonsoft.Json;
using NXS.Data.Sales;
using System;

namespace NXS.DataServices.Sales.Models
{
	public class AreaInput
	{
		public AreaData[][] areaData { get; set; }
		public int areaId { get; set; }
		public string areaName { get; set; }
		public int officeId { get; set; }
		public int salesRepId { get; set; }
		public DateTime? startTimestamp { get; set; }

		public void ToDb(SalesArea item)
		{
			if (this.areaData.Length == 0)
				throw new Exception("Empty areaData");

			double minLat, maxLat, minLng, maxLng;
			minLat = minLng = double.MaxValue;
			maxLat = maxLng = double.MinValue;
			foreach (var path in this.areaData)
			{
				foreach (var point in path)
				{
					if (point.lat < minLat) minLat = point.lat;
					if (maxLat < point.lat) maxLat = point.lat;
					if (point.lng < minLng) minLng = point.lng;
					if (maxLng < point.lng) maxLng = point.lng;
				}
			}
			if (minLat == double.MaxValue || minLng == double.MaxValue ||
				maxLat == double.MinValue || maxLng == double.MinValue)
			{
				throw new Exception("Invalid areaData");
			}

			item.areaName = ContactsService.db_null_or_string(this.areaName);
			item.pointData = JsonConvert.SerializeObject(this.areaData);
			item.minLatitude = minLat;
			item.maxLatitude = maxLat;
			item.minLongitude = minLng;
			item.maxLongitude = maxLng;
			item.status = "A";
		}

		public void ToDb(SalesAreaAssignment item)
		{
			item.salesAreaId = this.areaId;
			item.officeId = this.officeId;
			item.salesRepId = this.salesRepId;
			item.startTimestamp = this.startTimestamp.HasValue ? this.startTimestamp.Value : DateTime.UtcNow;
			item.endTimestamp = null;
			item.status = "A";
		}
	}
	public class AreaData
	{
		public double lat { get; set; }
		public double lng { get; set; }
	}
}
