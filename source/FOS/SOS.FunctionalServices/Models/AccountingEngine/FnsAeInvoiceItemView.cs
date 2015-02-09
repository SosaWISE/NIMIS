using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.AccountingEngine;

namespace SOS.FunctionalServices.Models.AccountingEngine
{
	public class FnsAeInvoiceItemView : IFnsAeInvoiceItemView
	{
		#region .ctor

		public FnsAeInvoiceItemView(long invoiceID, string itemId, short qty, string salesmanID, string technicianID)
		{
			InvoiceId = invoiceID;
			ItemId = itemId;
			Qty = qty;
			SalesmanID = salesmanID;
			TechnicianID = technicianID;

		}

		public FnsAeInvoiceItemView(long invoiceItemID, string itemId, short qty, decimal retailPrice, decimal systemPoints, string salesmanID, string technicianID)
		{
			InvoiceItemID = invoiceItemID;
			ItemId = itemId;
			Qty = qty;
			RetailPrice = retailPrice;
			PriceWithTax = retailPrice;
			SystemPoints = systemPoints;
			SalesmanID = salesmanID;
			TechnicianID = technicianID;
		}

		public FnsAeInvoiceItemView(AE_InvoiceItemsView item)
		{
			InvoiceItemID = item.InvoiceItemID;
			InvoiceId = item.InvoiceId;
			ItemId = item.ItemId;
			ProductBarcodeId = item.ProductBarcodeId;
			ItemSKU = item.ItemSKU;
			ItemDesc = item.ItemDesc;
			TaxOptionId = item.TaxOptionId;
			Qty = item.Qty;
			Cost = item.Cost;
			RetailPrice = item.RetailPrice;
			PriceWithTax = item.PriceWithTax;
			SystemPoints = item.SystemPoints;
			SalesmanID = item.SalesmanId;
			TechnicianID = item.TechnicianId;
			ModifiedOn = item.ModifiedOn;
			ModifiedBy = item.ModifiedBy;
			CreatedOn = item.CreatedOn;
			CreatedBy = item.CreatedBy;
		}

		#endregion .ctor

		#region Properties
		public long InvoiceItemID { get; set; }
		public long InvoiceId { get; set; }
		public string ItemId { get; set; }
		public string ProductBarcodeId { get; set; }
		public string ItemSKU { get; set; }
		public string ItemDesc { get; set; }
		public string TaxOptionId { get; set; }
		public short Qty { get; set; }
		public decimal Cost { get; set; }
		public decimal RetailPrice { get; set; }
		public decimal? PriceWithTax { get; set; }
		public decimal SystemPoints { get; set; }
		public string SalesmanID { get; set; }
		public string TechnicianID { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
	    public string BarcodeId { get; set; }

	    #endregion Properties
	}
}
