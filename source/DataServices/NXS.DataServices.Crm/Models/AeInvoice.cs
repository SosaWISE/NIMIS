using NXS.Data.Crm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NXS.DataServices.Crm.Models
{
	public class AeInvoice
	{
		//public long InvoiceID { get; set; }
		public long ID { get; set; }
		public long AccountId { get; set; }
		public string InvoiceTypeId { get; set; }
		public int? ContractId { get; set; }
		public int? TaxScheduleId { get; set; }
		public int? PaymentTermId { get; set; }
		public DateTime DocDate { get; set; }
		public DateTime? PostedDate { get; set; }
		public DateTime? DueDate { get; set; }
		public DateTime? GLPostDate { get; set; }
		public decimal? CurrentTransactionAmount { get; set; }
		public decimal SalesAmount { get; set; }
		public decimal OriginalTransactionAmount { get; set; }
		public decimal CostAmount { get; set; }
		public decimal TaxAmount { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }

		public List<AeInvoiceItem> InvoiceItems { get; set; }

		internal static AeInvoice FromDb(AE_Invoice item, bool nullable = false)
		{
			if (item == null)
			{
				if (nullable)
					return null;
				else
					throw new Exception("invoice is null");
			}

			var result = new AeInvoice();
			result.ID = item.ID;
			result.AccountId = item.AccountId;
			result.InvoiceTypeId = item.InvoiceTypeId;
			result.ContractId = item.ContractId;
			result.TaxScheduleId = item.TaxScheduleId;
			result.PaymentTermId = item.PaymentTermId;
			result.DocDate = item.DocDate;
			result.PostedDate = item.PostedDate;
			result.DueDate = item.DueDate;
			result.GLPostDate = item.GLPostDate;
			result.CurrentTransactionAmount = item.CurrentTransactionAmount;
			result.SalesAmount = item.SalesAmount;
			result.OriginalTransactionAmount = item.OriginalTransactionAmount;
			result.CostAmount = item.CostAmount;
			result.TaxAmount = item.TaxAmount;
			result.IsActive = item.IsActive;
			result.IsDeleted = item.IsDeleted;
			result.ModifiedOn = item.ModifiedOn;
			result.ModifiedBy = item.ModifiedBy;
			result.CreatedOn = item.CreatedOn;
			result.CreatedBy = item.CreatedBy;

			result.InvoiceItems = (item.InvoiceItems == null)
				? new List<AeInvoiceItem>() : item.InvoiceItems.ConvertAll(a => AeInvoiceItem.FromDb(a));

			return result;
		}
	}
}
