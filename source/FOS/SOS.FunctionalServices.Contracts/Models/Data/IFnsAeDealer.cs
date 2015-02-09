using System;

namespace SOS.FunctionalServices.Contracts.Models.Data
{
	public interface IFnsAeDealer
	{
		int DealerID { get; set; }
		string DealerName { get; set; }
		string ContactFirstName { get; set; }
		string ContactLastName { get; set; }
		string ContactEmail { get; set; }
		string PhoneWork { get; set; }
		string PhoneMobile { get; set; }
		string PhoneFax { get; set; }
		string Address { get; set; }
		string Address2 { get; set; }
		string City { get; set; }
		string StateAB { get; set; }
		string PostalCode { get; set; }
		string PlusFour { get; set; }
		string Username { get; set; }
		string Password { get; set; }
		bool IsActive { get; set; }
		bool IsDeleted { get; set; }
		DateTime ModifiedOn { get; set; }
		string ModifiedBy { get; set; }
		DateTime CreatedOn { get; set; }
		string CreatedBy { get; set; }
		DateTime DEX_ROW_TS { get; set; }
	}
}