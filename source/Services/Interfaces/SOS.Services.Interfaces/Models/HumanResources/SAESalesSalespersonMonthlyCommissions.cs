using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.HumanResources
{
    public class SAESalesSalespersonMonthlyCommissions : ISAESalesSalespersonMonthlyCommissions
	{
        public int UserID { get; set; }
        public DateTime ContractDate { get; set; }
        public int SalesMonth { get; set; }
        public int SalesYear { get; set; }
        public long CustomerMasterFileID { get; set; }
        public long AccountId { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerMiddleName { get; set; }
        public string CustomerLastName { get; set; }
        public string CreditRating { get; set; }
        public decimal ActivationFeeAmt { get; set; }
        public int ContractLength { get; set; }
        public string ServiceType { get; set; }
        public decimal MonthlyPaymentAmt { get; set; }
        public string PaymentMethod { get; set; }
        public decimal SalesCommissionAmt { get; set; }
        public decimal RecurringCommissionAmt { get; set; }
        public bool IsActive { get; set; }
    }

	public interface ISAESalesSalespersonMonthlyCommissions

	{
        [DataMember]
        int UserID { get; set; }
        [DataMember]
        DateTime ContractDate { get; set; }
        [DataMember]
        int SalesMonth { get; set; }
        [DataMember]
        int SalesYear { get; set; }
        [DataMember]
        long CustomerMasterFileID { get; set; }
        [DataMember]
        string CustomerFirstName { get; set; }
        [DataMember]
        string CustomerMiddleName { get; set; }
        [DataMember]
        string CustomerLastName { get; set; }
        [DataMember]
        string CreditRating { get; set; }
        [DataMember]
        decimal ActivationFeeAmt { get; set; }
        [DataMember]
        int ContractLength { get; set; }
        [DataMember]
        string ServiceType { get; set; }
        [DataMember]
        decimal MonthlyPaymentAmt { get; set; }
        [DataMember]
        string PaymentMethod { get; set; }
        [DataMember]
        decimal SalesCommissionAmt { get; set; }
        [DataMember]
        decimal RecurringCommissionAmt { get; set; }
        [DataMember]
        bool IsActive { get; set; }
    }
}
