namespace SOS.Services.Interfaces.Models.AccountingEngine
{
	public class AeInvoiceTemplate
	{
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
