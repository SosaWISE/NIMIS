using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.Data;

namespace SOS.FunctionalServices.Models.Cms
{
	public static class Helper
	{

		#region Helper values
		#endregion Helper values

		public static void BindToQlAddress(out QL_Address oQlAddress, IFnsLeadAddressModel oAddress)
		{
			/** Instantiate QL_Address. */
			oQlAddress = oAddress.AddressID == 0 // If 0 means this is a new address.
				? new QL_Address()
				: SosCrmDataContext.Instance.QL_Addresses.LoadByPrimaryKey(oAddress.AddressID);

			/** Bind new data. */
			if (oAddress.DealerId != oQlAddress.DealerId) oQlAddress.DealerId = oAddress.DealerId;
			// ** Validate foreign key.
			if (oAddress.ValidationVendorId != oQlAddress.ValidationVendorId) oQlAddress.ValidationVendorId = oAddress.ValidationVendorId;
			if (string.IsNullOrWhiteSpace(oQlAddress.ValidationVendorId))
				oQlAddress.ValidationVendorId = MC_AddressValidationVendor.FKDefaultValues.VALIDATION_VENDOR_ID;
			if (oAddress.AddressValidationStateId != oQlAddress.AddressValidationStateId) oQlAddress.AddressValidationStateId = oAddress.AddressValidationStateId;
			if (String.IsNullOrWhiteSpace(oQlAddress.AddressValidationStateId))
				oQlAddress.AddressValidationStateId = MC_AddressValidationState.FKDefaultValues.ADDRESS_VALIDATION_STATE_ID;
			if (oAddress.StateId != oQlAddress.StateId) oQlAddress.StateId = oAddress.StateId;
			if (oAddress.CountryId != oQlAddress.CountryId) oQlAddress.CountryId = oAddress.CountryId;
			if (oAddress.TimeZoneId != oQlAddress.TimeZoneId) oQlAddress.TimeZoneId = oAddress.TimeZoneId;
			if (oAddress.AddressTypeId != oQlAddress.AddressTypeId) oQlAddress.AddressTypeId = oAddress.AddressTypeId;
			if (string.IsNullOrWhiteSpace(oQlAddress.AddressTypeId))
				oQlAddress.AddressTypeId = MC_AddressType.FKDefaultValues.ADDRESS_TYPE_ID;
			if (oAddress.StreetAddress != oQlAddress.StreetAddress) oQlAddress.StreetAddress = oAddress.StreetAddress;
			if (oAddress.StreetAddress2 != oQlAddress.StreetAddress2) oQlAddress.StreetAddress2 = oAddress.StreetAddress2;
			if (oAddress.StreetNumber != oQlAddress.StreetNumber) oQlAddress.StreetNumber = oAddress.StreetNumber;
			if (oAddress.StreetName != oQlAddress.StreetName) oQlAddress.StreetName = oAddress.StreetName;
			if (oAddress.StreetType != oQlAddress.StreetType) oQlAddress.StreetType = oAddress.StreetType;
			if (oAddress.PreDirectional != oQlAddress.PreDirectional) oQlAddress.PreDirectional = oAddress.PreDirectional;
			if (oAddress.PostDirectional != oQlAddress.PostDirectional) oQlAddress.PostDirectional = oAddress.PostDirectional;
			if (oAddress.Extension != oQlAddress.Extension) oQlAddress.Extension = oAddress.Extension;
			if (oAddress.ExtensionNumber != oQlAddress.ExtensionNumber) oQlAddress.ExtensionNumber = oAddress.ExtensionNumber;
			if (oAddress.County != oQlAddress.County) oQlAddress.County = oAddress.County;
			if (oAddress.CountyCode != oQlAddress.CountyCode) oQlAddress.CountyCode = oAddress.CountyCode;
			if (oAddress.Urbanization != oQlAddress.Urbanization) oQlAddress.Urbanization = oAddress.Urbanization;
			if (oAddress.UrbanizationCode != oQlAddress.UrbanizationCode) oQlAddress.UrbanizationCode = oAddress.UrbanizationCode;
			if (oAddress.City != oQlAddress.City) oQlAddress.City = oAddress.City;
			if (oAddress.PostalCode != oQlAddress.PostalCode) oQlAddress.PostalCode = oAddress.PostalCode;
			if (oAddress.PlusFour != oQlAddress.PlusFour) oQlAddress.PlusFour = oAddress.PlusFour;
			if (oAddress.Phone != oQlAddress.Phone) oQlAddress.Phone = oAddress.Phone;
			if (oAddress.DeliveryPoint != oQlAddress.DeliveryPoint) oQlAddress.DeliveryPoint = oAddress.DeliveryPoint;
			if (Math.Abs(oAddress.Latitude - oQlAddress.Latitude) > 0) oQlAddress.Latitude = oAddress.Latitude;
			if (Math.Abs(oAddress.Longitude - oQlAddress.Longitude) > 0) oQlAddress.Longitude = oAddress.Longitude;
			if (oAddress.CongressionalDistric != oQlAddress.CongressionalDistric) oQlAddress.CongressionalDistric = oAddress.CongressionalDistric;
			if (oAddress.DPV != oQlAddress.DPV) oQlAddress.DPV = oAddress.DPV;
			if (oAddress.DPVResponse != oQlAddress.DPVResponse) oQlAddress.DPVResponse = oAddress.DPVResponse;
			if (oAddress.DPVFootnote != oQlAddress.DPVFootnote) oQlAddress.DPVFootnote = oAddress.DPVFootnote;
			if (oAddress.CarrierRoute != oQlAddress.CarrierRoute) oQlAddress.CarrierRoute = oAddress.CarrierRoute;
			if (oAddress.IsActive != oQlAddress.IsActive) oQlAddress.IsActive = oAddress.IsActive;
			if (oAddress.IsDeleted != oQlAddress.IsDeleted) oQlAddress.IsDeleted = oAddress.IsDeleted;
		}

		/// <summary>
		/// Because this is one time save we do not check for disparate values.
		/// 
		/// </summary>
		/// <param name="oOffer">out QL_LeadProductOffer </param>
		/// <param name="fosLeadProductOffer">IFosLeadProductOffer</param>
		/// <param name="szUserId">string</param>
		public static void BindToQlLeadProductOffer(out QL_LeadProductOffer oOffer, IFosLeadProductOffer fosLeadProductOffer, string szUserId)
		{
			/** Bind values. */
			oOffer = new QL_LeadProductOffer
			{
				LeadId = fosLeadProductOffer.LeadId,
				ProductSkwId = fosLeadProductOffer.ProductSkwId,
				SalesRepId = szUserId,
				OfferDate = DateTime.Now
			};
		}

		public static void BindToQlLead(out QL_Lead oLead, IFnsLeadFullDataModel oFnsLeadFullDataModel, string szUserId)
		{
			/** Check to see if this is an existing record. */
			oLead = oFnsLeadFullDataModel.LeadID == 0
				? new QL_Lead()
				: SosCrmDataContext.Instance.QL_Leads.LoadByPrimaryKey(oFnsLeadFullDataModel.LeadID);

			/** Bind values. */
			if (oFnsLeadFullDataModel.Address.AddressID != oLead.AddressId) oLead.AddressId = oFnsLeadFullDataModel.Address.AddressID;
			if (oFnsLeadFullDataModel.CustomerTypeId != oLead.CustomerTypeId) oLead.CustomerTypeId = oFnsLeadFullDataModel.CustomerTypeId;
			if (oFnsLeadFullDataModel.CustomerMasterFileId != oLead.CustomerMasterFileId) oLead.CustomerMasterFileId = oFnsLeadFullDataModel.CustomerMasterFileId;
			if (oFnsLeadFullDataModel.DealerId != oLead.DealerId) oLead.DealerId = oFnsLeadFullDataModel.DealerId;
			if (oFnsLeadFullDataModel.LocalizationId != oLead.LocalizationId) oLead.LocalizationId = oFnsLeadFullDataModel.LocalizationId;
			if (oFnsLeadFullDataModel.TeamLocationId != oLead.TeamLocationId) oLead.TeamLocationId = oFnsLeadFullDataModel.TeamLocationId;
			if (oFnsLeadFullDataModel.LeadSourceId != oLead.LeadSourceId) oLead.LeadSourceId = oFnsLeadFullDataModel.LeadSourceId;
			if (oFnsLeadFullDataModel.LeadDispositionId != oLead.LeadDispositionId) oLead.LeadDispositionId = oFnsLeadFullDataModel.LeadDispositionId;
			if (oFnsLeadFullDataModel.SeasonId != oLead.SeasonId) oLead.SeasonId = oFnsLeadFullDataModel.SeasonId;
			if (oFnsLeadFullDataModel.SalesRepId != oLead.SalesRepId) oLead.SalesRepId = oFnsLeadFullDataModel.SalesRepId;
			if (oFnsLeadFullDataModel.Salutation != oLead.Salutation) oLead.Salutation = oFnsLeadFullDataModel.Salutation;
			if (oFnsLeadFullDataModel.FirstName != oLead.FirstName) oLead.FirstName = oFnsLeadFullDataModel.FirstName;
			if (oFnsLeadFullDataModel.MiddleName != oLead.MiddleName) oLead.MiddleName = oFnsLeadFullDataModel.MiddleName;
			if (oFnsLeadFullDataModel.LastName != oLead.LastName) oLead.LastName = oFnsLeadFullDataModel.LastName;
			if (oFnsLeadFullDataModel.Suffix != oLead.Suffix) oLead.Suffix = oFnsLeadFullDataModel.Suffix;
			if (oFnsLeadFullDataModel.Gender != oLead.Gender) oLead.Gender = oFnsLeadFullDataModel.Gender;
			if (oFnsLeadFullDataModel.SSN != oLead.SSN) oLead.SSN = oFnsLeadFullDataModel.SSN;
			if (oFnsLeadFullDataModel.DOB != oLead.DOB) oLead.DOB = oFnsLeadFullDataModel.DOB;
			if (oFnsLeadFullDataModel.DL != oLead.DL) oLead.DL = oFnsLeadFullDataModel.DL;
			if (oFnsLeadFullDataModel.DLStateID != oLead.DLStateID) oLead.DLStateID = oFnsLeadFullDataModel.DLStateID;
			if (oFnsLeadFullDataModel.Email != oLead.Email) oLead.Email = oFnsLeadFullDataModel.Email;
			if (oFnsLeadFullDataModel.PhoneHome != oLead.PhoneHome) oLead.PhoneHome = oFnsLeadFullDataModel.PhoneHome;
			if (oFnsLeadFullDataModel.PhoneWork != oLead.PhoneWork) oLead.PhoneWork = oFnsLeadFullDataModel.PhoneWork;
			if (oFnsLeadFullDataModel.PhoneMobile != oLead.PhoneMobile) oLead.PhoneMobile = oFnsLeadFullDataModel.PhoneMobile;
			if (oFnsLeadFullDataModel.IsActive != oLead.IsActive) oLead.IsActive = oFnsLeadFullDataModel.IsActive;
			if (oFnsLeadFullDataModel.IsDeleted != oLead.IsDeleted) oLead.IsDeleted = oFnsLeadFullDataModel.IsDeleted;
			//if (oFnsLeadFullDataModel.CreatedOn != oLead.CreatedOn) oLead.CreatedOn = oFnsLeadFullDataModel.CreatedOn;
			//if (oFnsLeadFullDataModel.CreatedBy != oLead.CreatedBy) oLead.CreatedBy = oFnsLeadFullDataModel.CreatedBy;

			// ** Check to see if this is a new record.
			if (oLead.IsNew)
			{
				oLead.DEX_ROW_TS = DateTime.UtcNow;
				// ** Generate a new CMFID.
				var oCMF = AE_CustomerMasterFile.CreateNew(oLead.DealerId, szUserId);
				oLead.CustomerMasterFileId = oCMF.CustomerMasterFileID;
			}

		}
	}
}
