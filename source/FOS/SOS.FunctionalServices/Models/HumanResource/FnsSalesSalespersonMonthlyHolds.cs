using System;
using SOS.Data.HumanResource;
using SOS.FunctionalServices.Contracts.Models.HumanResource;
using SOS.Services.Interfaces.Models.HumanResources;

namespace SOS.FunctionalServices.Models.HumanResource
{
    public class FnsSalesSalespersonMonthlyHolds : IFnsSalesSalespersonMonthlyHolds
    {
        #region .ctor

        public FnsSalesSalespersonMonthlyHolds(SAE_SalesSalespersonMonthlyHoldsView view)
        {
	    UserID = view.UserID;
	    ContractDate = view.ContractDate;
	    SalesMonth = view.SalesMonth;
	    SalesYear = view.SalesYear;
	    CustomerMasterFileID = view.CustomerMasterFileID;
	    AccountID = view.AccountID;
	    CustomerFirstName = view.CustomerFirstName;
	    CustomerMiddleName = view.CustomerMiddleName;
	    CustomerLastName = view.CustomerLastName;
	    HoldName = view.HoldName;
	    HoldDescription = view.HoldDescription;
	    HoldAmt = view.HoldAmt;
        }
        #endregion .ctor

        #region Properties
        public int UserID { get; private set; }
        public DateTime ContractDate { get; private set; }
        public int SalesMonth { get; private set; }
        public int SalesYear { get; private set; }
        public long CustomerMasterFileID { get; private set; }
        public long AccountID { get; private set; }
        public string CustomerFirstName { get; private set; }
        public string CustomerMiddleName { get; private set; }
        public string CustomerLastName { get; private set; }
        public string HoldName { get; private set; }
        public string HoldDescription { get; private set; }
        public decimal HoldAmt { get; private set; }

        #endregion Properties   
    }

}