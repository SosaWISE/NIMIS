using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.HumanResources
{
    public class RuUsersConnextAccountList : IRuUsersConnextAccountList
	{
        public int UserID { get; set; }
        public long CustomerID { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerMiddleName { get; set; }
        public string CustomerLastName { get; set; }
        public DateTime ContractDate { get; set; }
        public string CreditQuality { get; set; }
        public decimal ActivationFee { get; set; }
        public int ContractLength { get; set; }
        public string ServiceType { get; set; }
        public decimal MonthlyPayment { get; set; }
        public string PaymentMethod { get; set; }
        public decimal TotalCommission { get; set; }
        public bool IsActive { get; set; }
    }

	public interface IRuUsersConnextAccountList
	{
        [DataMember]
        int UserID { get; set; }
        [DataMember]
        long CustomerID { get; set; }
        [DataMember]
        string CustomerFirstName { get; set; }
        [DataMember]
        string CustomerMiddleName { get; set; }
        [DataMember]
        string CustomerLastName { get; set; }
        [DataMember]
        DateTime ContractDate { get; set; }
        [DataMember]
        string CreditQuality { get; set; }
        [DataMember]
        decimal ActivationFee { get; set; }
        [DataMember]
        int ContractLength { get; set; }
        [DataMember]
        string ServiceType { get; set; }
        [DataMember]
        decimal MonthlyPayment { get; set; }
        [DataMember]
        string PaymentMethod { get; set; }
        [DataMember]
        decimal TotalCommission { get; set; }
        [DataMember]
        bool IsActive { get; set; }
    }
}
