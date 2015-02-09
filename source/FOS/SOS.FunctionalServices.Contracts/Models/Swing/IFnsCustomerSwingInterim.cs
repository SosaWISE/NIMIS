using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.Swing
{
    public interface IFnsCustomerSwingInterim
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
		string SwingStatus { get; set; }

		[DataMember]
		DateTime? CreatedOn { get; set; }

		[DataMember]
		string CreatedBy { get; set; }

		#endregion Properties
    }
}
