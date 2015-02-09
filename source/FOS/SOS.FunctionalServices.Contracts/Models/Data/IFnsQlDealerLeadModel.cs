using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.Data
{
	public interface IFnsQlDealerLeadModel
	{
		[DataMember]
		int DealerLeadID { get; set; }
		[DataMember]
		string DealerLeadTypeId { get; set; }
		[DataMember]
		string DealerName { get; set; }
		[DataMember]
		string ContactFirstName { get; set; }
		[DataMember]
		string ContactLastName { get; set; }
		[DataMember]
		string ContactEmail { get; set; }
		[DataMember]
		string PhoneWork { get; set; }
		[DataMember]
		string PhoneMobile { get; set; }
		[DataMember]
		string PhoneFax { get; set; }
		[DataMember]
		string Address { get; set; }
		[DataMember]
		string Address2 { get; set; }
		[DataMember]
		string City { get; set; }
		[DataMember]
		string StateAB { get; set; }
		[DataMember]
		string PostalCode { get; set; }
		[DataMember]
		string PlusFour { get; set; }
		[DataMember]
		string Memo { get; set; }
		[DataMember]
		string Username { get; set; }
		[DataMember]
		string Password { get; set; }
		[DataMember]
		bool IsActive { get; set; }
		[DataMember]
		bool IsDeleted { get; set; }
		[DataMember]
		DateTime ModifiedOn { get; set; }
		[DataMember]
		string ModifiedBy { get; set; }
		[DataMember]
		DateTime CreatedOn { get; set; }
		[DataMember]
		string CreatedBy { get; set; }
		[DataMember]
		DateTime DEX_ROW_TS { get; set; }
	}
}