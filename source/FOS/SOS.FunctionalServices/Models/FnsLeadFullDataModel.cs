using System;
using System.Collections.Generic;
using SOS.Data.HumanResource;
using SOS.Data.HumanResource.ControllerExtensions;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.Data;

namespace SOS.FunctionalServices.Models
{
	public class FnsLeadFullDataModel : IFnsLeadFullDataModel
	{
		#region .ctor

		public FnsLeadFullDataModel(){}
		public FnsLeadFullDataModel(QL_Lead oLead)
		{
			Address = new FnsLeadAddressModel(oLead.Address);
			CustomerType = new FnsAeCustomerType(oLead.CustomerType);
			Dealer = new FnsAeDealer(oLead.Dealer);
			LeadID = oLead.LeadID;
			CustomerTypeId = oLead.CustomerTypeId;
			CustomerMasterFileId = oLead.CustomerMasterFileId;
			DealerId = oLead.DealerId;
			LocalizationId = oLead.LocalizationId;
			Localization = new FnsMcLocalization(oLead.Localization); 
			TeamLocationId = oLead.TeamLocationId;
			LeadSourceId = oLead.LeadSourceId;
			LeadSource = oLead.LeadSource.LeadSource;
			LeadDispositionId = oLead.LeadDispositionId;
			LeadDisposition = oLead.LeadDisposition.LeadDisposition;
			LeadDispositionDateChange = oLead.LeadDispositionDateChange;
//			TeamLocation = new RuTeamLocation(HumanResourceDataContext.Instance.RU_TeamLocations.LoadByPrimaryKey(oLead.TeamLocationId));
			SeasonId = oLead.SeasonId;
//			Season = new RuSeason(HumanResourceDataContext.Instance.RU_Seasons.LoadByPrimaryKey(oLead.SeasonId));
			SalesRepId = oLead.SalesRepId;
			SalesRep = new FnsRuUser(HumanResourceDataContext.Instance.RU_Users.LoadBySalesRepId(oLead.SalesRepId));
			Salutation = oLead.Salutation;
			FirstName = oLead.FirstName;
			MiddleName = oLead.MiddleName;
			LastName = oLead.LastName;
			Suffix = oLead.Suffix;
			Gender = oLead.Gender;
			SSN = oLead.SSN;
			DOB = oLead.DOB;
			DL = oLead.DL;
			DLStateID = oLead.DLStateID;
			Email = oLead.Email;
			PhoneHome = oLead.PhoneHome;
			PhoneWork = oLead.PhoneWork;
			PhoneMobile = oLead.PhoneMobile;
			IsActive = oLead.IsActive;
			IsDeleted = oLead.IsDeleted;
			CreatedOn = oLead.CreatedOn.ToUniversalTime();
			CreatedBy = oLead.CreatedBy;
		}

		#endregion .ctor

		#region Implementation of IFnsLeadFullDataModel

		public IFnsLeadAddressModel Address { get; set; }
		public IFnsAeCustomerType CustomerType { get; set; }
		public IFnsAeDealer Dealer { get; set; }
		public long LeadID { get; set; }
		public List<IFosLeadProductOffer> ProductSkwIdList { get; set; }
		public string CustomerTypeId { get; set; }
		public long CustomerMasterFileId { get; set; }
		public int DealerId { get; set; }
		public string LocalizationId { get; set; }
		public int LeadSourceId { get; set; }
		public string LeadSource { get; set; }
		public int LeadDispositionId { get; set; }
		public string LeadDisposition { get; set; }
		public DateTime? LeadDispositionDateChange { get; set; }
		public IFnsMcLocalization Localization { get; set; }
		public int TeamLocationId { get; set; }
		public IFnsRuTeamLocation TeamLocation { get; set; }
		public int SeasonId { get; set; }
		public IFnsRuSeason Season { get; set; }
		public string SalesRepId { get; set; }
		public IFnsRuUser SalesRep { get; set; }
		public string Salutation { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string Suffix { get; set; }
		public string Gender { get; set; }
		public string SSN { get; set; }
		public DateTime? DOB { get; set; }
		public string DL { get; set; }
		public string DLStateID { get; set; }
		public string Email { get; set; }
		public string PhoneHome { get; set; }
		public string PhoneWork { get; set; }
		public string PhoneMobile { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }

		#endregion Implementation of IFnsLeadFullDataModel
	}

	public class FosLeadProductOffer : IFosLeadProductOffer
	{
		#region .ctor
		public FosLeadProductOffer() {}
		public FosLeadProductOffer(QL_LeadProductOffersView oLeadProductOffer)
		{
			LeadProductOfferedId = oLeadProductOffer.LeadProductOfferedId;
			LeadId = oLeadProductOffer.LeadId;
			ProductSkwId = oLeadProductOffer.ProductSkwID;
			ProductName = oLeadProductOffer.ProductName;
			ShortName = oLeadProductOffer.ShortName;
			ProductTypeName = oLeadProductOffer.ProductTypeName;
			ProductImageName = oLeadProductOffer.ProductImageName;
			SalesRepId = oLeadProductOffer.SalesRepId;
			SalesRepFullName = oLeadProductOffer.SalesRepFullName;
			OfferDate = oLeadProductOffer.OfferDate;
		}

		#endregion .ctor

		#region Implementation of IFosLeadProductOffer

		public long LeadProductOfferedId { get; set; }
		public long LeadId { get; set; }
		public string ProductSkwId { get; set; }
		public string ProductName { get; set; }
		public string ShortName { get; set; }
		public string ProductTypeName { get; set; }
		public string ProductImageName { get; set; }
		public string SalesRepId { get; set; }
		public string SalesRepFullName { get; set; }
		public DateTime OfferDate { get; set; }

		#endregion Implementation of IFosLeadProductOffer
	}

	public class FnsLeadAddressModel : IFnsLeadAddressModel
	{
		#region .ctor
		public FnsLeadAddressModel() {}
		public FnsLeadAddressModel (QL_Address oAddress)
		{
			AddressID = oAddress.AddressID;
			DealerId = oAddress.DealerId;
			ValidationVendorId = oAddress.ValidationVendorId;
			AddressValidationStateId = oAddress.AddressValidationStateId;
			StateId = oAddress.StateId;
			State = new FnsMcPoliticalState(oAddress.State);
			CountryId = oAddress.CountryId;
			Country = new FnsMcPoliticalCountry(oAddress.Country);
			TimeZoneId = oAddress.TimeZoneId;
			TimeZone = new FnsMcPoliticalTimeZone(oAddress.TimeZone);
			AddressTypeId = oAddress.AddressTypeId;
			StreetAddress = oAddress.StreetAddress;
			StreetAddress2 = oAddress.StreetAddress2;
			StreetNumber = oAddress.StreetNumber;
			StreetName = oAddress.StreetName;
			StreetType = oAddress.StreetType;
			PreDirectional = oAddress.PreDirectional;
			PostDirectional = oAddress.PostDirectional;
			Extension = oAddress.Extension;
			ExtensionNumber = oAddress.ExtensionNumber;
			County = oAddress.County;
			CountyCode = oAddress.CountyCode;
			Urbanization = oAddress.Urbanization;
			UrbanizationCode = oAddress.UrbanizationCode;
			City = oAddress.City;
			PostalCode = oAddress.PostalCode;
			PlusFour = oAddress.PlusFour;
			Phone = oAddress.Phone;
			DeliveryPoint = oAddress.DeliveryPoint;
			Latitude = oAddress.Latitude;
			Longitude = oAddress.Longitude;
			CongressionalDistric = oAddress.CongressionalDistric;
			DPV = oAddress.DPV;
			DPVResponse = oAddress.DPVResponse;
			DPVFootnote = oAddress.DPVFootnote;
			CarrierRoute = oAddress.CarrierRoute;
			IsActive = oAddress.IsActive;
			IsDeleted = oAddress.IsDeleted;
			CreatedBy = oAddress.CreatedBy;
			CreatedOn = oAddress.CreatedOn.ToUniversalTime();

		}

		#endregion .ctor

		#region Implementation of IFnsLeadAddressModel

		public long AddressID { get; set; }
		public int DealerId { get; set; }
		public string ValidationVendorId { get; set; }
		public string AddressValidationStateId { get; set; }
		public string StateId { get; set; }
		public IFnsMcPoliticalState State { get; set; }
		public string CountryId { get; set; }
		public IFnsMcPoliticalCountry Country { get; set; }
		public int TimeZoneId { get; set; }
		public IFnsMcPoliticalTimeZone TimeZone { get; set; }
		public string AddressTypeId { get; set; }
		public string StreetAddress { get; set; }
		public string StreetAddress2 { get; set; }
		public string StreetNumber { get; set; }
		public string StreetName { get; set; }
		public string StreetType { get; set; }
		public string PreDirectional { get; set; }
		public string PostDirectional { get; set; }
		public string Extension { get; set; }
		public string ExtensionNumber { get; set; }
		public string County { get; set; }
		public string CountyCode { get; set; }
		public string Urbanization { get; set; }
		public string UrbanizationCode { get; set; }
		public string City { get; set; }
		public string PostalCode { get; set; }
		public string PlusFour { get; set; }
		public string Phone { get; set; }
		public string DeliveryPoint { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public int? CongressionalDistric { get; set; }
		public bool DPV { get; set; }
		public string DPVResponse { get; set; }
		public string DPVFootnote { get; set; }
		public string CarrierRoute { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public string CreatedBy { get; set; }
		public DateTime CreatedOn { get; set; }

		#endregion
	}
}