using System;

namespace NXS.Data.Crm
{
	public class IE_ProductBarcodeLast
	{
		public string ProductBarcodeID { get; set; }
		public string ProductBarcodeTrackingTypeId { get; set; }
		public string LocationTypeId { get; set; }
		public string LocationId { get; set; }
		public int? AuditId { get; set; }
		public DateTime CreatedOn { get; set; }
		public string EquipmentId { get; set; }
		public string ItemSKU { get; set; }
		public string ItemDesc { get; set; }
	}
}
