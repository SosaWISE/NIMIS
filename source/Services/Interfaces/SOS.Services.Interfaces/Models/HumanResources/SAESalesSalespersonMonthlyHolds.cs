using System;
using System.Data;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.HumanResources
{
    public class SAESalesSalespersonMonthlyHolds : ISAESalesSalespersonMonthlyHolds
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
        public string HoldName { get; set; }
        public string HoldDescription { get; set; }
        public decimal HoldAmt { get; set; }
    }

	public interface ISAESalesSalespersonMonthlyHolds
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
        long AccountId { get; set; }
        [DataMember]
        string CustomerFirstName { get; set; }
        [DataMember]
        string CustomerMiddleName { get; set; }
        [DataMember]
        string CustomerLastName { get; set; }
        [DataMember]
        string HoldName { get; set; }
        [DataMember]
        string HoldDescription { get; set; }
        [DataMember]
        decimal HoldAmt { get; set; }
    }
}
