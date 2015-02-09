using System;

namespace SOS.Services.Interfaces.Models.AccountingEngine
{
	#region AeDealer
	public interface IAeDealer
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
		// ReSharper disable InconsistentNaming
		DateTime DEX_ROW_TS { get; set; }
		// ReSharper restore InconsistentNaming
	}

	public class AeDealer : IAeDealer
	{
		#region Implementation of IAeDealer

		public int DealerID { get; set; }
		public string DealerName { get; set; }
		public string ContactFirstName { get; set; }
		public string ContactLastName { get; set; }
		public string ContactEmail { get; set; }
		public string PhoneWork { get; set; }
		public string PhoneMobile { get; set; }
		public string PhoneFax { get; set; }
		public string Address { get; set; }
		public string Address2 { get; set; }
		public string City { get; set; }
		public string StateAB { get; set; }
		public string PostalCode { get; set; }
		public string PlusFour { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
		public DateTime DEX_ROW_TS { get; set; }

		#endregion Implementation of IAeDealer
	}

	#endregion AeDealer
}
