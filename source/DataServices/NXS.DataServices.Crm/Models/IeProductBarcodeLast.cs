using NXS.Data.Crm;
using System;

namespace NXS.DataServices.Crm.Models
{
	public class IeProductBarcodeLast
	{
		public string ProductBarcodeID { get; set; }
		public string ProductBarcodeTrackingTypeId { get; set; }
		public string LocationTypeId { get; set; }
		public string LocationId { get; set; }
		public int? AuditId { get; set; }
		public string ItemSKU { get; set; }
		public string ItemDesc { get; set; }

		internal static IeProductBarcodeLast FromDb(IE_ProductBarcodeLast item, bool nullable = false)
		{
			if (item == null)
			{
				if (nullable)
					return null;
				else
					throw new Exception("ProductBarcodeLast is null");
			}

			var result = new IeProductBarcodeLast();
			result.ProductBarcodeID = item.ProductBarcodeID;
			result.ProductBarcodeTrackingTypeId = item.ProductBarcodeTrackingTypeId;
			result.LocationTypeId = item.LocationTypeId;
			result.LocationId = item.LocationId;
			result.AuditId = item.AuditId;
			result.ItemSKU = item.ItemSKU;
			result.ItemDesc = item.ItemDesc;
			return result;
		}
	}
}
