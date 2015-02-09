using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.Data;

namespace SOS.FunctionalServices.Models.Cms
{
	public class FnsQlDealerLeadModel : IFnsQlDealerLeadModel
	{
		#region .ctor

		public FnsQlDealerLeadModel () {}
		public FnsQlDealerLeadModel (QL_DealerLead oDealerLead)
		{
			DealerLeadID = oDealerLead.DealerLeadID;
			DealerLeadTypeId = oDealerLead.DealerLeadTypeId;
			DealerName = oDealerLead.DealerName;
			ContactFirstName = oDealerLead.ContactFirstName;
			ContactLastName = oDealerLead.ContactLastName;
			ContactEmail = oDealerLead.ContactEmail;
			PhoneWork = oDealerLead.PhoneWork;
			PhoneMobile = oDealerLead.PhoneMobile;
			PhoneFax = oDealerLead.PhoneFax;
			Address = oDealerLead.Address;
			Address2 = oDealerLead.Address2;
			City = oDealerLead.City;
			StateAB = oDealerLead.StateAB;
			PostalCode = oDealerLead.PostalCode;
			PlusFour = oDealerLead.PlusFour;
			Memo = oDealerLead.Memo;
			Username = oDealerLead.Username;
			Password = oDealerLead.Password;
			IsActive = oDealerLead.IsActive;
			IsDeleted = oDealerLead.IsDeleted;
			ModifiedOn = oDealerLead.ModifiedOn;
			ModifiedBy = oDealerLead.ModifiedBy;
			CreatedOn = oDealerLead.CreatedOn;
			CreatedBy = oDealerLead.CreatedBy;
			DEX_ROW_TS = oDealerLead.DEX_ROW_TS;
		}

		#endregion .ctor

		#region Properties

		public int DealerLeadID { get; set; }
		public string DealerLeadTypeId { get; set; }
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
		public string Memo { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
		public DateTime DEX_ROW_TS { get; set; }

		#endregion Properties

	}
}
