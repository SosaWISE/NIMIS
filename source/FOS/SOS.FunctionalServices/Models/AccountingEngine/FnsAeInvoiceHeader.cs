using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.AccountingEngine;

namespace SOS.FunctionalServices.Models.AccountingEngine
{
	public class FnsAeInvoiceHeader : IFnsAeInvoiceHeader
	{
		#region .ctor

		public FnsAeInvoiceHeader(long accountId, string invoiceTypeId)
		{
			AccountId = accountId;
			InvoiceTypeId = invoiceTypeId;
		}

		public FnsAeInvoiceHeader(AE_Invoice invoice)
		{
			InvoiceID = invoice.InvoiceID;
			AccountId = invoice.AccountId;
			InvoiceTypeId = invoice.InvoiceTypeId;
			ContractId = invoice.ContractId;
			TaxScheduleId = invoice.TaxScheduleId;
			PaymentTermId = invoice.PaymentTermId;
			DocDate = invoice.DocDate;
			PostedDate = invoice.PostedDate;
			DueDate = invoice.DueDate;
			GLPostDate = invoice.GLPostDate;
			CurrentTransactionAmount = invoice.CurrentTransactionAmount;
			SalesAmount = invoice.SalesAmount;
			OriginalTransactionAmount = invoice.OriginalTransactionAmount;
			CostAmount = invoice.CostAmount;
			TaxAmount = invoice.TaxAmount;
			IsActive = invoice.IsActive;
			IsDelete = invoice.IsDeleted;
			ModifiedOn = invoice.ModifiedDate;
			ModifiedBy = invoice.ModifiedById;
		}

		#endregion .ctor

		#region Properties
		public long InvoiceID { get; set; }
		public long AccountId { get; set; }
		public string InvoiceTypeId { get; set; }
		public int? ContractId { get; set; }
		public int? TaxScheduleId { get; set; }
		public int? PaymentTermId { get; set; }
		public string DocDate { get; set; }
		public string PostedDate { get; set; }
		public string DueDate { get; set; }
		public string GLPostDate { get; set; }
		public decimal? CurrentTransactionAmount { get; set; }
		public decimal SalesAmount { get; set; }
		public decimal OriginalTransactionAmount { get; set; }
		public decimal CostAmount { get; set; }
		public decimal TaxAmount { get; set; }
		public bool IsActive { get; set; }
		public bool IsDelete { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
		//public string BarcodeId { get; set; }

	    #endregion Properties
	}
}