using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.AccountingEngine;

namespace SOS.FunctionalServices.Models.AccountingEngine
{
	public class FnsAeInvoiceTemplate : IFnsAeInvoiceTemplate
	{
		#region .ctor

		public FnsAeInvoiceTemplate(AE_InvoiceTemplate item)
		{
			InvoiceTemplateID = item.InvoiceTemplateID;
			DealerId = item.DealerId;
			ActivationItemId = item.ActivationItemId;
			ActivationDiscountItemId = item.ActivationDiscountItemId;
			MMRItemId = item.MMRItemId;
			MMRDiscountItemId = item.MMRDiscountItemId;
			ActivationOverThreeMonthsId = item.ActivationOverThreeMonthsId;
			TemplateName = item.TemplateName;
			ActivationDiscountAmount = item.ActivationDiscountAmount;
			MMRDiscountAmount = item.MMRDiscountAmount;
			SystemPoints = item.SystemPoints;
		}

		#endregion .ctor

		#region Properties
		public long InvoiceTemplateID { get; set; }
		public int DealerId { get; set; }
		public string ActivationItemId { get; set; }
		public string ActivationDiscountItemId { get; set; }
		public string MMRItemId { get; set; }
		public string MMRDiscountItemId { get; set; }
		public string ActivationOverThreeMonthsId { get; set; }
		public string TemplateName { get; set; }
		public decimal ActivationDiscountAmount { get; set; }
		public decimal MMRDiscountAmount { get; set; }
		public decimal SystemPoints { get; set; }
		#endregion Properties
	}
}
