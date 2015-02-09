using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.Swing;

namespace SOS.FunctionalServices.Models.Swing
{
    public class FnsCustomerSwungInfo : IFnsCustomerSwungInfo
    {
        #region .ctor

        public FnsCustomerSwungInfo() { }

        public FnsCustomerSwungInfo(Action<IFnsCustomerSwungInfo, object> fxBindData, object value)
        {
            if (fxBindData == null)
                throw new NotImplementedException();

            fxBindData(this, value);
        }

        public FnsCustomerSwungInfo(AE_CustomerSWUNGInfoView item)
        {
            InterimAccountID = item.InterimAccountID;
            MsAccountID = item.MsAccountID;
	        CustomerMasterFileID = item.CustomerMasterFileID;
	        CustomerMasterFile = item.CustomerMasterFile;
	        PremiseAddress = item.PremiseAddress;
	        McAccount = item.McAccount;
	        MsAccount = item.MsAccount;
	        QlLead = item.QlLead;
	        QlCreditReport = item.QlCreditReport;
	        AeCustomer = item.AeCustomer;
	        AeCustomerAccount = item.AeCustomerAccount;
	        MsEmergencyContact = item.MsEmergencyContact;
	        EquipmentSwung = item.EquipmentSwung;
            CreatedOn = item.CreatedOn;
            CreatedBy = item.CreatedBy;
        }

        #endregion .ctor

        #region Properties

	    public int InterimAccountID { get; set; }
	    public long? MsAccountID { get; set; }
	    public long? CustomerMasterFileID { get; set; }
	    public DateTime? CustomerMasterFile { get; set; }
	    public DateTime? PremiseAddress { get; set; }
	    public DateTime? McAccount { get; set; }
	    public DateTime? MsAccount { get; set; }
	    public DateTime? QlLead { get; set; }
	    public DateTime? QlCreditReport { get; set; }
	    public DateTime? AeCustomer { get; set; }
	    public DateTime? AeCustomerAccount { get; set; }
	    public DateTime? MsEmergencyContact { get; set; }
	    public DateTime? EquipmentSwung { get; set; }
	    public DateTime? CreatedOn { get; set; }
	    public string CreatedBy { get; set; }

		#endregion Properties
	}
}
