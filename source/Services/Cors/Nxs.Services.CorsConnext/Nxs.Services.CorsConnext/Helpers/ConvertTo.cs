using SOS.FunctionalServices.Contracts.Models.Connext;
using SOS.FunctionalServices.Models.Connext;
using SOS.Services.Interfaces.Models.Connext;

namespace Nxs.Services.CorsConnext.Helpers
{
	public static class ConvertTo
	{
		#region Contacts

		public static CxAddress CastFnsToCxAddress(IFnsCxAddress fnsCxAddress)
		{
			return new CxAddress
			{
				AddressID = fnsCxAddress.AddressID,
				DealerId = fnsCxAddress.DealerId,
				AddressTypeId = fnsCxAddress.AddressTypeId,
				AddressValidationStateId = fnsCxAddress.AddressValidationStateId,
				CarrierRoute = fnsCxAddress.CarrierRoute,
				City = fnsCxAddress.City,
				CongressionalDistric = fnsCxAddress.CongressionalDistric,
				CountryId = fnsCxAddress.CountryId,
				County = fnsCxAddress.County,
				CountyCode = fnsCxAddress.CountyCode,
				DeliveryPoint = fnsCxAddress.DeliveryPoint,
				DPV = fnsCxAddress.DPV,
				DPVFootnote = fnsCxAddress.DPVFootnote,
				DPVResponse = fnsCxAddress.DPVResponse,
				Extension = fnsCxAddress.Extension,
				ExtensionNumber = fnsCxAddress.ExtensionNumber,
				Latitude = fnsCxAddress.Latitude,
				Longitude = fnsCxAddress.Longitude,
				Phone = fnsCxAddress.Phone,
				PlusFour = fnsCxAddress.PlusFour,
				PostalCode = fnsCxAddress.PostalCode,
				PostalCodeFull = fnsCxAddress.PostalCodeFull,
				PostDirectional = fnsCxAddress.PostDirectional,
				PreDirectional = fnsCxAddress.PreDirectional,
				SalesRepId = fnsCxAddress.SalesRepId,
				SeasonId = fnsCxAddress.SeasonId,
				StateId = fnsCxAddress.StateId,
				StreetAddress = fnsCxAddress.StreetAddress,
				StreetAddress2 = fnsCxAddress.StreetAddress2,
				StreetName = fnsCxAddress.StreetName,
				StreetNumber = fnsCxAddress.StreetNumber,
				StreetType = fnsCxAddress.StreetType,
				TeamLocationId = fnsCxAddress.TeamLocationId,
				TimeZoneId = fnsCxAddress.TimeZoneId,
				Urbanization = fnsCxAddress.Urbanization,
				UrbanizationCode = fnsCxAddress.UrbanizationCode,
				ValidationVendorId = fnsCxAddress.ValidationVendorId,
				IsActive = fnsCxAddress.IsActive,
				IsDeleted = fnsCxAddress.IsDeleted,
				CreatedBy = fnsCxAddress.CreatedBy,
				CreatedOn = fnsCxAddress.CreatedOn
			};
		}

		public static IFnsCxAddress CastToFnsCxAddress(CxAddress cxAddress)
		{
			return new FnsCxAddress
			{
				AddressID = cxAddress.AddressID,
				DealerId = cxAddress.DealerId,
				AddressTypeId = cxAddress.AddressTypeId,
				AddressValidationStateId = cxAddress.AddressValidationStateId,
				CarrierRoute = cxAddress.CarrierRoute,
				City = cxAddress.City,
				CongressionalDistric = cxAddress.CongressionalDistric,
				CountryId = cxAddress.CountryId,
				County = cxAddress.County,
				CountyCode = cxAddress.CountyCode,
				DeliveryPoint = cxAddress.DeliveryPoint,
				DPV = cxAddress.DPV,
				DPVFootnote = cxAddress.DPVFootnote,
				DPVResponse = cxAddress.DPVResponse,
				Extension = cxAddress.Extension,
				ExtensionNumber = cxAddress.ExtensionNumber,
				Latitude = cxAddress.Latitude,
				Longitude = cxAddress.Longitude,
				Phone = cxAddress.Phone,
				PlusFour = cxAddress.PlusFour,
				PostalCode = cxAddress.PostalCode,
				PostalCodeFull = cxAddress.PostalCodeFull,
				PostDirectional = cxAddress.PostDirectional,
				PreDirectional = cxAddress.PreDirectional,
				SalesRepId = cxAddress.SalesRepId,
				SeasonId = cxAddress.SeasonId,
				StateId = cxAddress.StateId,
				StreetAddress = cxAddress.StreetAddress,
				StreetAddress2 = cxAddress.StreetAddress2,
				StreetName = cxAddress.StreetName,
				StreetNumber = cxAddress.StreetNumber,
				StreetType = cxAddress.StreetType,
				TeamLocationId = cxAddress.TeamLocationId,
				TimeZoneId = cxAddress.TimeZoneId,
				Urbanization = cxAddress.Urbanization,
				UrbanizationCode = cxAddress.UrbanizationCode,
				ValidationVendorId = cxAddress.ValidationVendorId,
				IsActive = cxAddress.IsActive,
				IsDeleted = cxAddress.IsDeleted,
				CreatedBy = cxAddress.CreatedBy,
				CreatedOn = cxAddress.CreatedOn
			};
		}

		public static CxContact CastFnsToCxContact(IFnsCxContact fnsCxContact)
		{
			return new CxContact
			{
				ContactID = fnsCxContact.ContactID,
				ContactTypeId = fnsCxContact.ContactTypeId,
				AddressId = fnsCxContact.AddressId,
				DealerId = fnsCxContact.DealerId,
				LocalizationId = fnsCxContact.LocalizationId,
				TeamLocationId = fnsCxContact.TeamLocationId,
				SeasonId = fnsCxContact.SeasonId,
				SalesRepId = fnsCxContact.SalesRepId,
				Salutation = fnsCxContact.Salutation,
				FirstName = fnsCxContact.FirstName,
				MiddleName = fnsCxContact.MiddleName,
				LastName = fnsCxContact.LastName,
				Suffix = fnsCxContact.Suffix,
				Gender = fnsCxContact.Gender,
				SSN = fnsCxContact.SSN,
				DOB = fnsCxContact.DOB,
				Email = fnsCxContact.Email,
				PhoneHome = fnsCxContact.PhoneHome,
				PhoneWork = fnsCxContact.PhoneWork,
				PhoneMobile = fnsCxContact.PhoneMobile,
				CurrentSystem = fnsCxContact.CurrentSystem,
				IsActive = fnsCxContact.IsActive,
				IsDeleted = fnsCxContact.IsDeleted,
				CreatedOn = fnsCxContact.CreatedOn,
				CreatedBy = fnsCxContact.CreatedBy
			};
		}

		public static IFnsCxContact CastToFnsCxContact(CxContact cxContact)
		{
			return new FnsCxContact
			{
				ContactID = cxContact.ContactID,
				ContactTypeId = cxContact.ContactTypeId,
				AddressId = cxContact.AddressId,
				DealerId = cxContact.DealerId,
				LocalizationId = cxContact.LocalizationId,
				TeamLocationId = cxContact.TeamLocationId,
				SeasonId = cxContact.SeasonId,
				SalesRepId = cxContact.SalesRepId,
				Salutation = cxContact.Salutation,
				FirstName = cxContact.FirstName,
				MiddleName = cxContact.MiddleName,
				LastName = cxContact.LastName,
				Suffix = cxContact.Suffix,
				Gender = cxContact.Gender,
				SSN = cxContact.SSN,
				DOB = cxContact.DOB,
				Email = cxContact.Email,
				PhoneHome = cxContact.PhoneHome,
				PhoneWork = cxContact.PhoneWork,
				PhoneMobile = cxContact.PhoneMobile,
				CurrentSystem = cxContact.CurrentSystem,
				IsActive = cxContact.IsActive,
				IsDeleted = cxContact.IsDeleted,
				CreatedOn = cxContact.CreatedOn,
				CreatedBy = cxContact.CreatedBy
			};
		}

		#endregion Contacts
	}
}