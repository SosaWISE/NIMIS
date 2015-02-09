using System;
using SOS.Data.HumanResource;
using SOS.FunctionalServices.Contracts.Models.HumanResource;

namespace SOS.FunctionalServices.Models.HumanResource
{
    public class FnsConnextAccountList : IFnsConnextAccountList
    {
        #region .ctor

        public FnsConnextAccountList(RU_UsersAccountListConnextView view)
        {
            UserID = view.UserID;
	        CustomerID = view.CustomerID;
	        CustomerFirstName = view.CustomerFirstName;
	        CustomerMiddleName = view.CustomerMiddleName;
	        CustomerLastName = view.CustomerLastName;
	        ContractDate = view.ContractDate;
	        CreditQuality = view.CreditQuality;
	        ActivationFee = view.ActivationFee;
	        ContractLength = view.ContractLength;
	        ServiceType = view.ServiceType;
	        MonthlyPayment = view.MonthlyPayment;
	        PaymentMethod = view.PaymentMethod;
	        TotalCommission = view.TotalCommission;
	        IsActive = view.isActive;
        }
        #endregion .ctor

        #region Properties
        public int UserID { get; private set; }
        public long CustomerID { get; private set; }
        public string CustomerFirstName { get; private set; }
        public string CustomerMiddleName { get; private set; }
        public string CustomerLastName { get; private set; }
        public DateTime ContractDate { get; private set; }
        public string CreditQuality { get; private set; }
        public decimal ActivationFee { get; private set; }
        public int ContractLength { get; private set; }
        public string ServiceType { get; private set; }
        public decimal MonthlyPayment { get; private set; }
        public string PaymentMethod { get; private set; }
        public decimal TotalCommission { get; private set; }
        public bool IsActive { get; private set; }
        #endregion Properties   
    }

}