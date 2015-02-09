using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.Data;

namespace SOS.FunctionalServices.Models.Cms
{
	public class FnsCustomerFullDataModel : IFnsCustomerFullDataModel
	{
		#region .ctor
		public FnsCustomerFullDataModel(AE_Customer oCustomer)
		{
			CustomerID = oCustomer.CustomerID;
			AddressId = oCustomer.AddressId;
			//Address = new FnsCustomerAddressModel(oCustomer.Address);
			CustomerType = new FnsAeCustomerType(oCustomer.CustomerType);
			LeadId = oCustomer.LeadId;
			CustomerTypeId = oCustomer.CustomerTypeId;
			CustomerMasterFileId = oCustomer.CustomerMasterFileId;
			DealerId = oCustomer.DealerId;
			Dealer = new FnsAeDealer(oCustomer.Dealer);
			LocalizationId = oCustomer.LocalizationId;
			Localization = new FnsMcLocalization(oCustomer.Localization);
			Salutation = oCustomer.Prefix;
			FirstName = oCustomer.FirstName;
			MiddleName = oCustomer.MiddleName;
			LastName = oCustomer.LastName;
			Suffix = oCustomer.Postfix;
			Gender = oCustomer.Gender;
			SSN = oCustomer.SSN;
			DOB = oCustomer.DOB;
			Email = oCustomer.Email;
			PhoneHome = oCustomer.PhoneHome;
			PhoneWork = oCustomer.PhoneWork;
			PhoneMobile = oCustomer.PhoneMobile;
			Username = oCustomer.Username;
			IsActive = oCustomer.IsActive;
			IsDeleted = oCustomer.IsDeleted;
			ModifiedOn = oCustomer.ModifiedOn;
			ModifiedBy = oCustomer.ModifiedBy;
			CreatedOn = oCustomer.CreatedOn;
			CreatedBy = oCustomer.CreatedBy;
		}


		#endregion .ctor

		#region Properties

		public long CustomerID { get; set; }
		public long AddressId { get; set; }
		public IFnsCustomerAddressModel Address { get; set; }
		public IFnsAeCustomerType CustomerType { get; set; }
		public long LeadId { get; set; }
		public string CustomerTypeId { get; set; }
		public long CustomerMasterFileId { get; set; }
		public int DealerId { get; set; }
		public IFnsAeDealer Dealer { get; set; }
		public string LocalizationId { get; set; }
		public IFnsMcLocalization Localization { get; set; }
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
		public string Username { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }

		#endregion Properties
	}
}
