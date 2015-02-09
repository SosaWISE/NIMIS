using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.Swing
{
    public class CustomerSwungInfo : ICustomerSwungInfo
    {
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

    public interface ICustomerSwungInfo
    {
		#region Properties

		[DataMember]
		int InterimAccountID { get; set; }

		[DataMember]
		long? MsAccountID { get; set; }

		[DataMember]
		long? CustomerMasterFileID { get; set; }

		[DataMember]
		DateTime? CustomerMasterFile { get; set; }

		[DataMember]
		DateTime? PremiseAddress { get; set; }

		[DataMember]
		DateTime? McAccount { get; set; }

		[DataMember]
		DateTime? MsAccount { get; set; }

		[DataMember]
		DateTime? QlLead { get; set; }

		[DataMember]
		DateTime? QlCreditReport { get; set; }

		[DataMember]
		DateTime? AeCustomer { get; set; }

		[DataMember]
		DateTime? AeCustomerAccount { get; set; }

		[DataMember]
		DateTime? MsEmergencyContact { get; set; }

		[DataMember]
		DateTime? EquipmentSwung { get; set; }

		[DataMember]
		DateTime? CreatedOn { get; set; }

		[DataMember]
		string CreatedBy { get; set; }

		#endregion Properties
	}
}