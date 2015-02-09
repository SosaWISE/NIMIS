using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.Data;

namespace SOS.FunctionalServices.Models
{
	public class FnsLeadModel : IFnsLeadModel
	{
		public FnsLeadModel(QL_LeadBasicInfoView oView)
		{
			LeadID = oView.LeadID;
			AddressId = oView.AddressID;
			CustomerTypeId = oView.CustomerTypeId;
			CustomerMasterFileId = oView.CustomerMasterFileID;
			DealerId = oView.DealerId;
			LocalizationId = oView.LocalizationId;
			TeamLocationId = oView.TeamLocationId;
			SeasonId = oView.SeasonId;
			SalesRepId = oView.SalesRepId;
			Salutation = oView.Salutation;
			FirstName = oView.FirstName;
			MiddleName = oView.MiddleName;
			LastName = oView.LastName;
			Suffix = oView.Suffix;
			SSN = oView.SSN;
			DOB =  oView.DOB;
			DL = oView.DL;
			DLStateID = oView.DLStateID;
			Email = oView.Email;
			PhoneHome = oView.PhoneHome;
			PhoneMobile = oView.PhoneMobile;
			PhoneWork = oView.PhoneWork;
			PremisePhone = oView.PremisePhone;
			StreetAddress = oView.StreetAddress;
			City = oView.City;
			StateId = oView.StateId;
			Postal = oView.PostalCode;
			IsActive = oView.IsActive;
			IsDeleted = oView.IsDeleted;
		}

		#region Implementation of IFnsLeadModel

		public long LeadID { get; set; }
		public long AddressId { get; set; }
		public string CustomerTypeId { get; set; }
		public long CustomerMasterFileId { get; set; }
		public int DealerId { get; set; }
		public string LocalizationId { get; set; }
		public int TeamLocationId { get; set; }
		public int SeasonId { get; set; }
		public string SalesRepId { get; set; }
		public string Salutation { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string Suffix { get; set; }
		public string SSN { get; set; }
		public DateTime? DOB { get; set; }
		public string DL { get; set; }
		public string DLStateID { get; set; }
		public string Email { get; set; }
		public string PhoneHome { get; set; }
		public string PhoneWork { get; set; }
		public string PhoneMobile { get; set; }
		public string PremisePhone { get; set; }
		public string StreetAddress { get; set; }
		public string City { get; set; }
		public string StateId { get; set; }
		public string Postal { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }

		#endregion
	}
}
