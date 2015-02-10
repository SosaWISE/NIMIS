using System.Collections.Generic;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.Data;
using SOS.FunctionalServices.Models;
using SOS.FunctionalServices.Models.Cms;
using SOS.Services.Interfaces.Models;

namespace SOS.Services.Wcf.Crm.Helper
{
	public static class CmsSvcHelper
	{

		public static CmsModels.QlLeadFullData BindToQlLeadFullData(IFnsLeadFullDataModel oItem)
		{
			var oResultValue = new CmsModels.QlLeadFullData
			{
				Address = new CmsModels.QlAddress
				{
					AddressID = oItem.Address.AddressID,
					DealerId = oItem.Address.DealerId,
					ValidationVendorId = oItem.Address.ValidationVendorId,
					AddressValidationStateId = oItem.Address.AddressValidationStateId,
					StateId = oItem.Address.StateId,
					CountryId = oItem.Address.CountryId,
					TimeZoneId = oItem.Address.TimeZoneId,
					TimeZone = new CmsModels.McPoliticalTimeZone
					{
						TimeZoneID = oItem.Address.TimeZone.TimeZoneID,
						TimeZoneName = oItem.Address.TimeZone.TimeZoneName,
						TimeZoneAB = oItem.Address.TimeZone.TimeZoneAB,
						CentralTime = oItem.Address.TimeZone.CentralTime,
						HourDifference = oItem.Address.TimeZone.HourDifference,
						IsActive = oItem.Address.TimeZone.IsActive,
						IsDeleted = oItem.Address.TimeZone.IsDeleted
					},
					AddressTypeId = oItem.Address.AddressTypeId,
					StreetAddress = oItem.Address.StreetAddress,
					StreetAddress2 = oItem.Address.StreetAddress2,
					StreetNumber = oItem.Address.StreetNumber,
					StreetName = oItem.Address.StreetName,
					StreetType = oItem.Address.StreetType,
					PreDirectional = oItem.Address.PreDirectional,
					PostDirectional = oItem.Address.PostDirectional,
					Extension = oItem.Address.Extension,
					ExtensionNumber = oItem.Address.ExtensionNumber,
					County = oItem.Address.County,
					CountyCode = oItem.Address.CountyCode,
					Urbanization = oItem.Address.Urbanization,
					UrbanizationCode = oItem.Address.UrbanizationCode,
					City = oItem.Address.City,
					PostalCode = oItem.Address.PostalCode,
					PlusFour = oItem.Address.PlusFour,
					Phone = oItem.Address.Phone,
					DeliveryPoint = oItem.Address.DeliveryPoint,
					Latitude = oItem.Address.Latitude,
					Longitude = oItem.Address.Longitude,
					CongressionalDistric = oItem.Address.CongressionalDistric,
					DPV = oItem.Address.DPV,
					DPVResponse = oItem.Address.DPVResponse,
					DPVFootnote = oItem.Address.DPVFootnote,
					CarrierRoute = oItem.Address.CarrierRoute,
					IsActive = oItem.Address.IsActive,
					IsDeleted = oItem.Address.IsDeleted,
					CreatedBy = oItem.Address.CreatedBy,
					CreatedOn = oItem.Address.CreatedOn
				},
				CustomerType = new CmsModels.AeCustomerType
				{
					CustomerTypeID = oItem.CustomerType.CustomerTypeID,
					CustomerType = oItem.CustomerType.CustomerType
				},
				LeadID = oItem.LeadID,
				//ProductSkwIdList = oItem.ProductSkwIdList,
				CustomerTypeId = oItem.CustomerTypeId,
				CustomerMasterFileId = oItem.CustomerMasterFileId,
				DealerId = oItem.DealerId,
				Dealer = new CmsModels.AeDealer
				{
					DealerID = oItem.Dealer.DealerID,
					DealerName = oItem.Dealer.DealerName,
					ContactFirstName = oItem.Dealer.ContactFirstName,
					ContactLastName = oItem.Dealer.ContactLastName,
					ContactEmail = oItem.Dealer.ContactEmail,
					PhoneWork = oItem.Dealer.PhoneWork,
					PhoneMobile = oItem.Dealer.PhoneMobile,
					PhoneFax = oItem.Dealer.PhoneFax,
					Address = oItem.Dealer.Address,
					Address2 = oItem.Dealer.Address2,
					City = oItem.Dealer.City,
					StateAB = oItem.Dealer.StateAB,
					PostalCode = oItem.Dealer.PostalCode,
					PlusFour = oItem.Dealer.PlusFour,
					Username = oItem.Dealer.Username,
					Password = oItem.Dealer.Password,
					IsActive = oItem.Dealer.IsActive,
					IsDeleted = oItem.Dealer.IsDeleted,
					ModifiedOn = oItem.Dealer.ModifiedOn,
					ModifiedBy = oItem.Dealer.ModifiedBy,
					CreatedOn = oItem.Dealer.CreatedOn,
					CreatedBy = oItem.Dealer.CreatedBy,
					DEX_ROW_TS = oItem.Dealer.DEX_ROW_TS
				},
				LocalizationId = oItem.LocalizationId,
				Localization = new CmsModels.McLocalization
				{
					LocalizationID = oItem.Localization.LocalizationID,
					MSLocalId = oItem.Localization.MSLocalId,
					LocalizationName = oItem.Localization.LocalizationName,
					IsActive = oItem.Localization.IsActive,
					IsDeleted = oItem.Localization.IsDeleted
				},
				TeamLocationId = oItem.TeamLocationId,
				SeasonId = oItem.SeasonId,
				SalesRepId = oItem.SalesRepId,
				SalesRep = new RuModels.RuUser
				{
					UserID = oItem.SalesRep.UserID,
					RecruitedByID = oItem.SalesRep.RecruitedByID,
					GPEmployeeID = oItem.SalesRep.GPEmployeeID,
					UserEmployeeTypeId = oItem.SalesRep.UserEmployeeTypeId,
					PermanentAddressID = oItem.SalesRep.PermanentAddressID,
					SSN = oItem.SalesRep.SSN,
					FirstName = oItem.SalesRep.FirstName,
					MiddleName = oItem.SalesRep.MiddleName,
					LastName = oItem.SalesRep.LastName,
					PreferredName = oItem.SalesRep.PreferredName,
					CompanyName = oItem.SalesRep.CompanyName,
					MaritalStatus = oItem.SalesRep.MaritalStatus,
					SpouseName = oItem.SalesRep.SpouseName,
					UserName = oItem.SalesRep.UserName,
					Password = oItem.SalesRep.Password,
					BirthDate = oItem.SalesRep.BirthDate,
					HomeTown = oItem.SalesRep.HomeTown,
					BirthCity = oItem.SalesRep.BirthCity,
					BirthState = oItem.SalesRep.BirthState,
					BirthCountry = oItem.SalesRep.BirthCountry,
					Sex = oItem.SalesRep.Sex,
					ShirtSize = oItem.SalesRep.ShirtSize,
					HatSize = oItem.SalesRep.HatSize,
					DLNumber = oItem.SalesRep.DLNumber,
					DLState = oItem.SalesRep.DLState,
					DLCountry = oItem.SalesRep.DLCountry,
					DLExpiration = oItem.SalesRep.DLExpiration,
					Height = oItem.SalesRep.Height,
					Weight = oItem.SalesRep.Weight,
					EyeColor = oItem.SalesRep.EyeColor,
					HairColor = oItem.SalesRep.HairColor,
					PhoneHome = oItem.SalesRep.PhoneHome,
					PhoneCell = oItem.SalesRep.PhoneCell,
					PhoneCellCarrierID = oItem.SalesRep.PhoneCellCarrierID,
					PhoneFax = oItem.SalesRep.PhoneFax,
					Email = oItem.SalesRep.Email,
					CorporateEmail = oItem.SalesRep.CorporateEmail,
					TreeLevel = oItem.SalesRep.TreeLevel,
					HasVerifiedAddress = oItem.SalesRep.HasVerifiedAddress,
					RightToWorkExpirationDate = oItem.SalesRep.RightToWorkExpirationDate,
					RightToWorkNotes = oItem.SalesRep.RightToWorkNotes,
					RightToWorkStatusID = oItem.SalesRep.RightToWorkStatusID,
					IsLocked = oItem.SalesRep.IsLocked,
					IsActive = oItem.SalesRep.IsActive,
					IsDeleted = oItem.SalesRep.IsDeleted,
					RecruitedDate = oItem.SalesRep.RecruitedDate,
					CreatedByID = oItem.SalesRep.CreatedByID,
					CreatedDate = oItem.SalesRep.CreatedDate,
					ModifiedByID = oItem.SalesRep.ModifiedByID,
					ModifiedDate = oItem.SalesRep.ModifiedDate,
				},
				LeadDispositionId = oItem.LeadDispositionId,
				LeadDisposition = oItem.LeadDisposition,
				LeadDispositionDateChange = oItem.LeadDispositionDateChange,
				LeadSourceId = oItem.LeadSourceId,
				LeadSource = oItem.LeadSource,
				Salutation = oItem.Salutation,
				FirstName = oItem.FirstName,
				MiddleName = oItem.MiddleName,
				LastName = oItem.LastName,
				Suffix = oItem.Suffix,
				Gender = oItem.Gender,
				SSN = oItem.SSN,
				DOB = oItem.DOB,
				DL = oItem.DL,
				DLStateID = oItem.DLStateID,
				Email = oItem.Email,
				PhoneHome = oItem.PhoneHome,
				PhoneWork = oItem.PhoneWork,
				PhoneMobile = oItem.PhoneMobile,
				IsActive = oItem.IsActive,
				IsDeleted = oItem.IsDeleted,
				CreatedOn = oItem.CreatedOn,
				CreatedBy = oItem.CreatedBy
			};
			// ** Build Product List
			if (oItem.ProductSkwIdList != null && oItem.ProductSkwIdList.Count > 0)
			{
				oResultValue.ProductSkwIdList = new List<CmsModels.QlLeadProductOffer>();
				foreach (var oLeadProductOffer in oItem.ProductSkwIdList)
				{
					oResultValue.ProductSkwIdList.Add(new CmsModels.QlLeadProductOffer
					{
						LeadProductOfferedId = oLeadProductOffer.LeadProductOfferedId,
						LeadId = oLeadProductOffer.LeadId,
						ProductSkwId = oLeadProductOffer.ProductSkwId,
						ProductName = oLeadProductOffer.ProductName,
						ShortName = oLeadProductOffer.ShortName,
						ProductTypeName = oLeadProductOffer.ProductTypeName,
						ProductImageName = oLeadProductOffer.ProductImageName,
						SalesRepId = oLeadProductOffer.SalesRepId,
						SalesRepFullName = oLeadProductOffer.SalesRepFullName,
						OfferDate = oLeadProductOffer.OfferDate
					});
				}
			}

			/** Return result. */
			return oResultValue;
		}

		public static FnsLeadFullDataModel BindToLeadFullDataModel(CmsModels.QlLeadFullData oItem)
		{
			var oResultValue = new FnsLeadFullDataModel
			{
				Address = oItem.Address != null ? new FnsLeadAddressModel
				{
					AddressID = oItem.Address.AddressID,
					DealerId = oItem.Address.DealerId,
					ValidationVendorId = oItem.Address.ValidationVendorId,
					AddressValidationStateId = oItem.Address.AddressValidationStateId,
					StateId = oItem.Address.StateId,
					CountryId = oItem.Address.CountryId,
					TimeZoneId = oItem.Address.TimeZoneId,
					TimeZone = oItem.Address.TimeZone != null ? new FnsMcPoliticalTimeZone
					{
						TimeZoneID = oItem.Address.TimeZone.TimeZoneID,
						TimeZoneName = oItem.Address.TimeZone.TimeZoneName,
						TimeZoneAB = oItem.Address.TimeZone.TimeZoneAB,
						CentralTime = oItem.Address.TimeZone.CentralTime,
						HourDifference = oItem.Address.TimeZone.HourDifference,
						IsActive = oItem.Address.TimeZone.IsActive,
						IsDeleted = oItem.Address.TimeZone.IsDeleted
					}
					: null,
					AddressTypeId = oItem.Address.AddressTypeId,
					StreetAddress = oItem.Address.StreetAddress,
					StreetAddress2 = oItem.Address.StreetAddress2,
					StreetNumber = oItem.Address.StreetNumber,
					StreetName = oItem.Address.StreetName,
					StreetType = oItem.Address.StreetType,
					PreDirectional = oItem.Address.PreDirectional,
					PostDirectional = oItem.Address.PostDirectional,
					Extension = oItem.Address.Extension,
					ExtensionNumber = oItem.Address.ExtensionNumber,
					County = oItem.Address.County,
					CountyCode = oItem.Address.CountyCode,
					Urbanization = oItem.Address.Urbanization,
					UrbanizationCode = oItem.Address.UrbanizationCode,
					City = oItem.Address.City,
					PostalCode = oItem.Address.PostalCode,
					PlusFour = oItem.Address.PlusFour,
					Phone = oItem.Address.Phone,
					DeliveryPoint = oItem.Address.DeliveryPoint,
					Latitude = oItem.Address.Latitude,
					Longitude = oItem.Address.Longitude,
					CongressionalDistric = oItem.Address.CongressionalDistric,
					DPV = oItem.Address.DPV,
					DPVResponse = oItem.Address.DPVResponse,
					DPVFootnote = oItem.Address.DPVFootnote,
					CarrierRoute = oItem.Address.CarrierRoute,
					IsActive = oItem.Address.IsActive,
					IsDeleted = oItem.Address.IsDeleted,
					CreatedBy = oItem.Address.CreatedBy,
					CreatedOn = oItem.Address.CreatedOn
				}
				: null,
				CustomerTypeId = oItem.CustomerTypeId,
				CustomerType = oItem.CustomerType != null
						? new FnsAeCustomerType
						  {
							CustomerTypeID = oItem.CustomerType.CustomerTypeID,
							CustomerType = oItem.CustomerType.CustomerType
						  }
						: null,
				LeadID = oItem.LeadID,
				//ProductSkwIdList = oItem.ProductSkwIdList,
				CustomerMasterFileId = oItem.CustomerMasterFileId,
				DealerId = oItem.DealerId,
				Dealer = oItem.Dealer != null ? new FnsAeDealer
						{
							DealerID = oItem.Dealer.DealerID,
							DealerName = oItem.Dealer.DealerName,
							ContactFirstName = oItem.Dealer.ContactFirstName,
							ContactLastName = oItem.Dealer.ContactLastName,
							ContactEmail = oItem.Dealer.ContactEmail,
							PhoneWork = oItem.Dealer.PhoneWork,
							PhoneMobile = oItem.Dealer.PhoneMobile,
							PhoneFax = oItem.Dealer.PhoneFax,
							Address = oItem.Dealer.Address,
							Address2 = oItem.Dealer.Address2,
							City = oItem.Dealer.City,
							StateAB = oItem.Dealer.StateAB,
							PostalCode = oItem.Dealer.PostalCode,
							PlusFour = oItem.Dealer.PlusFour,
							Username = oItem.Dealer.Username,
							Password = oItem.Dealer.Password,
							IsActive = oItem.Dealer.IsActive,
							IsDeleted = oItem.Dealer.IsDeleted,
							ModifiedOn = oItem.Dealer.ModifiedOn,
							ModifiedBy = oItem.Dealer.ModifiedBy,
							CreatedOn = oItem.Dealer.CreatedOn,
							CreatedBy = oItem.Dealer.CreatedBy,
							DEX_ROW_TS = oItem.Dealer.DEX_ROW_TS
						}
						: null,
				LocalizationId = oItem.LocalizationId,
				Localization = oItem.Localization != null ? new FnsMcLocalization
						{
							LocalizationID = oItem.Localization.LocalizationID,
							MSLocalId = oItem.Localization.MSLocalId,
							LocalizationName = oItem.Localization.LocalizationName,
							IsActive = oItem.Localization.IsActive,
							IsDeleted = oItem.Localization.IsDeleted
						}
						: null,
				TeamLocationId = oItem.TeamLocationId,
				LeadSourceId = oItem.LeadSourceId,
				LeadDispositionId = oItem.LeadDispositionId,
				SeasonId = oItem.SeasonId,
				SalesRepId = oItem.SalesRepId,
				SalesRep = oItem.SalesRep != null ? new FnsRuUser
					{
						UserID = oItem.SalesRep.UserID,
						RecruitedByID = oItem.SalesRep.RecruitedByID,
						GPEmployeeID = oItem.SalesRep.GPEmployeeID,
						UserEmployeeTypeId = oItem.SalesRep.UserEmployeeTypeId,
						PermanentAddressID = oItem.SalesRep.PermanentAddressID,
						SSN = oItem.SalesRep.SSN,
						FirstName = oItem.SalesRep.FirstName,
						MiddleName = oItem.SalesRep.MiddleName,
						LastName = oItem.SalesRep.LastName,
						PreferredName = oItem.SalesRep.PreferredName,
						CompanyName = oItem.SalesRep.CompanyName,
						MaritalStatus = oItem.SalesRep.MaritalStatus,
						SpouseName = oItem.SalesRep.SpouseName,
						UserName = oItem.SalesRep.UserName,
						Password = oItem.SalesRep.Password,
						BirthDate = oItem.SalesRep.BirthDate,
						HomeTown = oItem.SalesRep.HomeTown,
						BirthCity = oItem.SalesRep.BirthCity,
						BirthState = oItem.SalesRep.BirthState,
						BirthCountry = oItem.SalesRep.BirthCountry,
						Sex = oItem.SalesRep.Sex,
						ShirtSize = oItem.SalesRep.ShirtSize,
						HatSize = oItem.SalesRep.HatSize,
						DLNumber = oItem.SalesRep.DLNumber,
						DLState = oItem.SalesRep.DLState,
						DLCountry = oItem.SalesRep.DLCountry,
						DLExpiration = oItem.SalesRep.DLExpiration,
						Height = oItem.SalesRep.Height,
						Weight = oItem.SalesRep.Weight,
						EyeColor = oItem.SalesRep.EyeColor,
						HairColor = oItem.SalesRep.HairColor,
						PhoneHome = oItem.SalesRep.PhoneHome,
						PhoneCell = oItem.SalesRep.PhoneCell,
						PhoneCellCarrierID = oItem.SalesRep.PhoneCellCarrierID,
						PhoneFax = oItem.SalesRep.PhoneFax,
						Email = oItem.SalesRep.Email,
						CorporateEmail = oItem.SalesRep.CorporateEmail,
						TreeLevel = oItem.SalesRep.TreeLevel,
						HasVerifiedAddress = oItem.SalesRep.HasVerifiedAddress,
						RightToWorkExpirationDate = oItem.SalesRep.RightToWorkExpirationDate,
						RightToWorkNotes = oItem.SalesRep.RightToWorkNotes,
						RightToWorkStatusID = oItem.SalesRep.RightToWorkStatusID,
						IsLocked = oItem.SalesRep.IsLocked,
						IsActive = oItem.SalesRep.IsActive,
						IsDeleted = oItem.SalesRep.IsDeleted,
						RecruitedDate = oItem.SalesRep.RecruitedDate,
						CreatedByID = oItem.SalesRep.CreatedByID,
						CreatedDate = oItem.SalesRep.CreatedDate,
						ModifiedByID = oItem.SalesRep.ModifiedByID,
						ModifiedDate = oItem.SalesRep.ModifiedDate,
					}
					: null,
				Salutation = oItem.Salutation,
				FirstName = oItem.FirstName,
				MiddleName = oItem.MiddleName,
				LastName = oItem.LastName,
				Suffix = oItem.Suffix,
				Gender = oItem.Gender,
				SSN = oItem.SSN,
				DOB = oItem.DOB,
				DL = oItem.DL,
				DLStateID = oItem.DLStateID,
				Email = oItem.Email,
				PhoneHome = oItem.PhoneHome,
				PhoneWork = oItem.PhoneWork,
				PhoneMobile = oItem.PhoneMobile,
				IsActive = oItem.IsActive,
				IsDeleted = oItem.IsDeleted,
				CreatedOn = oItem.CreatedOn,
				CreatedBy = oItem.CreatedBy
			};
			// ** Build Product List
			if (oItem.ProductSkwIdList.Count > 0)
			{
				oResultValue.ProductSkwIdList = new List<IFosLeadProductOffer>();
				foreach (var oLeadProductOffer in oItem.ProductSkwIdList)
				{
					oResultValue.ProductSkwIdList.Add(new FosLeadProductOffer
					{
						LeadProductOfferedId = oLeadProductOffer.LeadProductOfferedId,
						LeadId = oLeadProductOffer.LeadId,
						ProductSkwId = oLeadProductOffer.ProductSkwId,
						SalesRepId = oLeadProductOffer.SalesRepId,
						OfferDate = oLeadProductOffer.OfferDate
					});
				}
			}

			/** Return result. */
			return oResultValue;
		}

		public static FnsMcAccountNoteModel BindToFosMcAccountNoteModel(CmsModels.McAccountNote oNote)
		{
			/** Initialize. */
			var oFosNote = new FnsMcAccountNoteModel
				{
					NoteTypeID = oNote.NoteTypeId
					, CustomerMasterFileId = oNote.CustomerMasterFileId
					, CustomerId =  oNote.CustomerId
					, LeadId = oNote.LeadId
					, NoteCategory1ID = oNote.NoteCategory1Id
					, NoteCategory2ID = oNote.NoteCategory2Id
					, Note = oNote.Note
				};

			/** Return result. */
			return oFosNote;
		}

		public static FnsAePaymentInformationCreateAccountModel BindToFnsAePaymentInformationCreateAccountModel(CmsModels.AePaymentInformationCreateAccountModel oPaymentInformationCreateAccountModel)
		{
			/** Initialize. */
			var oResult = new FnsAePaymentInformationCreateAccountModel
					{
						LeadId = oPaymentInformationCreateAccountModel.LeadId
						, CustomerMasterFileId = oPaymentInformationCreateAccountModel.CustomerMasterFileId
						, DealerId = oPaymentInformationCreateAccountModel.DealerId
						, ProductSkws = oPaymentInformationCreateAccountModel.ProductSkws.Split(',')
						, DealerAccountID = oPaymentInformationCreateAccountModel.DealerAccountID
						, ContractTemplateID = oPaymentInformationCreateAccountModel.ContractTemplateID
					};

			oResult.BillingInfo = new FnsBillingInfoModel
					{
						SameAsCustomer = oPaymentInformationCreateAccountModel.BillingInfo.SameAsCustomer
						, Salutation = oPaymentInformationCreateAccountModel.BillingInfo.Salutation
						, FirstName = oPaymentInformationCreateAccountModel.BillingInfo.FirstName
						, MiddleName = oPaymentInformationCreateAccountModel.BillingInfo.MiddleName
						, LastName = oPaymentInformationCreateAccountModel.BillingInfo.LastName
						, Suffix = oPaymentInformationCreateAccountModel.BillingInfo.Suffix
						, DOB = oPaymentInformationCreateAccountModel.BillingInfo.DOB
						, SSN = oPaymentInformationCreateAccountModel.BillingInfo.SSN
						, Gender = oPaymentInformationCreateAccountModel.BillingInfo.Gender
						, Email = oPaymentInformationCreateAccountModel.BillingInfo.Email
						, Language = oPaymentInformationCreateAccountModel.BillingInfo.Language
						, PhoneHome = oPaymentInformationCreateAccountModel.BillingInfo.PhoneHome
						, PhoneMobile = oPaymentInformationCreateAccountModel.BillingInfo.PhoneMobile
						, PhoneWork = oPaymentInformationCreateAccountModel.BillingInfo.PhoneWork
					};

			oResult.BillingAddress = new FnsBillingAddressModel
					{
						SameAsCustomer = oPaymentInformationCreateAccountModel.BillingAddress.SameAsCustomer
						, ValidationVendorId = oPaymentInformationCreateAccountModel.BillingAddress.ValidationVendorId
						, AddressValidationStateId= oPaymentInformationCreateAccountModel.BillingAddress.AddressValidationStateId
						, Street = oPaymentInformationCreateAccountModel.BillingAddress.Street
						, AddressTypeId = oPaymentInformationCreateAccountModel.BillingAddress.AddressTypeId
						, Street2 = oPaymentInformationCreateAccountModel.BillingAddress.Street2
						, City = oPaymentInformationCreateAccountModel.BillingAddress.City
						, StateId = oPaymentInformationCreateAccountModel.BillingAddress.StateId
						, PostalCode = oPaymentInformationCreateAccountModel.BillingAddress.PostalCode
						, CountryId = oPaymentInformationCreateAccountModel.BillingAddress.CountryId
						, TimeZoneId = oPaymentInformationCreateAccountModel.BillingAddress.TimeZoneId
						, Latitude = oPaymentInformationCreateAccountModel.BillingAddress.Latitude
						, Longitude = oPaymentInformationCreateAccountModel.BillingAddress.Longitude
						, DPV = oPaymentInformationCreateAccountModel.BillingAddress.DPV
					};

			oResult.PaymentInformation = new FnsPaymentInfoModel
					{
						PONumber = oPaymentInformationCreateAccountModel.PaymentInformation.PONumber
						, PaymentMethod = (EnumFnsPaymentMethod) oPaymentInformationCreateAccountModel.PaymentInformation.PaymentMethod
						, NameOnCard = oPaymentInformationCreateAccountModel.PaymentInformation.NameOnCard
						, CCNumber = oPaymentInformationCreateAccountModel.PaymentInformation.CCNumber
						, CCV = oPaymentInformationCreateAccountModel.PaymentInformation.CCV
						, ExpMonth = oPaymentInformationCreateAccountModel.PaymentInformation.ExpMonth
						, ExpYear = oPaymentInformationCreateAccountModel.PaymentInformation.ExpYear
						, RoutingNumber = oPaymentInformationCreateAccountModel.PaymentInformation.RoutingNumber
						, AccountNumber = oPaymentInformationCreateAccountModel.PaymentInformation.AccountNumber
						, CheckNumber = oPaymentInformationCreateAccountModel.PaymentInformation.CheckNumber
					};

			/** Return result. */
			return oResult;
		}
	}
}