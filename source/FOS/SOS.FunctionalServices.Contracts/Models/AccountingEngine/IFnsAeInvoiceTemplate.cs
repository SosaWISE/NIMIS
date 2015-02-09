using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.AccountingEngine
{
	public interface IFnsAeInvoiceTemplate
	{
		[DataMember]
		long InvoiceTemplateID { get; set; }
		[DataMember]
		int DealerId { get; set; }
		[DataMember]
		string ActivationItemId { get; set; }
		[DataMember]
		string ActivationDiscountItemId { get; set; }
		[DataMember]
		string MMRItemId { get; set; }
		[DataMember]
		string MMRDiscountItemId { get; set; }
		[DataMember]
		string ActivationOverThreeMonthsId { get; set; }
		[DataMember]
		string TemplateName { get; set; }
		[DataMember]
		decimal ActivationDiscountAmount { get; set; }
		[DataMember]
		decimal MMRDiscountAmount { get; set; }
		[DataMember]
		decimal SystemPoints { get; set; }
 
	}
}