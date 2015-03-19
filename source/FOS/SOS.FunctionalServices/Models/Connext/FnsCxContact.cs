using System;
using NXS.Data.Connext;
using SOS.FunctionalServices.Contracts.Models.Connext;

namespace SOS.FunctionalServices.Models.Connext
{
	public class FnsCxContact : IFnsCxContact
	{
		#region .ctor

		public FnsCxContact(CX_Contact contact)
		{
			ContactID = contact.ContactID;
			ContactTypeId = contact.ContactTypeId;
			AddressId = contact.AddressId;
			DealerId = contact.DealerId;
			LocalizationId = contact.LocalizationId;
			TeamLocationId = contact.TeamLocationId;
			SeasonId = contact.SeasonId;
			SalesRepId = contact.SalesRepId;
			Salutation = contact.Salutation;
			FirstName = contact.FirstName;
			MiddleName = contact.MiddleName;
			LastName = contact.LastName;
			Suffix = contact.Suffix;
			Gender = contact.Gender;
			SSN = contact.SSN;
			DOB = contact.DOB;
			Email = contact.Email;
			PhoneHome = contact.PhoneHome;
			PhoneWork = contact.PhoneWork;
			PhoneMobile = contact.PhoneMobile;
			CurrentSystem = contact.CurrentSystem;
			IsActive = contact.IsActive;
			IsDeleted = contact.IsDeleted;
			CreatedOn = contact.CreatedOn;
			CreatedBy = contact.CreatedBy;
		}

		public FnsCxContact() { }

		#endregion .ctor

		#region Properties
		public long ContactID { get; set; }
		public string ContactTypeId { get; set; }
		public long AddressId { get; set; }
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
		public string Gender { get; set; }
		public string SSN { get; set; }
		public DateTime? DOB { get; set; }
		public string Email { get; set; }
		public string PhoneHome { get; set; }
		public string PhoneWork { get; set; }
		public string PhoneMobile { get; set; }
		public string CurrentSystem { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
		#endregion Properties
	}
}
