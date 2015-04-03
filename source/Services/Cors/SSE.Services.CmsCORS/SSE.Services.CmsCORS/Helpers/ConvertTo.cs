using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.AccountingEngine;
using SOS.FunctionalServices.Contracts.Models.CentralStation;
using SOS.FunctionalServices.Contracts.Models.Data;
using SOS.FunctionalServices.Contracts.Models.Funding;
using SOS.FunctionalServices.Contracts.Models.HumanResource;
using SOS.FunctionalServices.Contracts.Models.InventoryEngine;
using SOS.FunctionalServices.Contracts.Models.Licensing;
using SOS.FunctionalServices.Contracts.Models.QualifyLead;
using SOS.FunctionalServices.Contracts.Models.Reporting;
using SOS.FunctionalServices.Contracts.Models.Swing;
using SOS.FunctionalServices.Models.CentralStation;
using SOS.Services.Interfaces.Models.AccountingEngine;
using SOS.Services.Interfaces.Models.CmsModels;
using SOS.Services.Interfaces.Models.Funding;
using SOS.Services.Interfaces.Models.HumanResources;
using SOS.Services.Interfaces.Models.InventoryEngine;
using SOS.Services.Interfaces.Models.Licensing;
using SOS.Services.Interfaces.Models.MonitoringStation;
using SOS.Services.Interfaces.Models.QualifyLead;
using SOS.Services.Interfaces.Models.SalesHareReportSrv;
using SOS.Services.Interfaces.Models.Swing;
using System.Collections.Generic;
using System.Linq;
using QlAddress = SSE.Services.CmsCORS.Models.QlAddress;

namespace SSE.Services.CmsCORS.Helpers
{
	public static class ConvertTo
	{
		#region AeCustomer
		public static AeCustomer CastFnsToAeCustomer(IFnsAeCustomerGpsClientsViewModel oItem)
		{
			/** Initialize. */
			var oResult = new AeCustomer();

			/** Bind data. */
			oResult.SalesRepId = oItem.SalesRepId;
			oResult.SeasonId = oItem.SeasonId;
			oResult.TeamLocationId = oItem.TeamLocationId;
			oResult.DealerId = oItem.DealerId;
			oResult.AddressId = oItem.AddressId;
			oResult.LeadId = oItem.LeadId;
			oResult.LocalizationId = oItem.LocalizationId;
			oResult.Prefix = oItem.Prefix;
			oResult.FirstName = oItem.FirstName;
			oResult.MiddleName = oItem.MiddleName;
			oResult.LastName = oItem.LastName;
			oResult.Postfix = oItem.Postfix;
			oResult.Gender = oItem.Gender;
			oResult.PhoneHome = oItem.PhoneHome;
			oResult.PhoneWork = oItem.PhoneWork;
			oResult.PhoneMobile = oItem.PhoneMobile;
			oResult.Email = oItem.Email;
			oResult.DOB = oItem.DOB;
			oResult.SSN = oItem.SSN;
			oResult.Username = oItem.Username;
			//oResult.Password = oItem.Password;
			oResult.IsActive = oItem.IsActive;
			oResult.ModifiedOn = oItem.ModifiedOn;
			oResult.ModifiedBy = oItem.ModifiedBy;
			oResult.CreatedOn = oItem.CreatedOn;
			oResult.CreatedBy = oItem.CreatedBy;

			/** Return result. */
			return oResult;
		}

		public static AeCustomer CastFnsToAeCustomer(IFnsCustomerFullDataModel oAeCustomerModel)
		{
			/** Initialize. */
			var oResult = new AeCustomer();

			/** Bind data. */
			oResult.CustomerID = oAeCustomerModel.CustomerID;
			oResult.CustomerTypeId = oAeCustomerModel.CustomerTypeId;
			oResult.CustomerMasterFileId = oAeCustomerModel.CustomerMasterFileId;
			oResult.DealerId = oAeCustomerModel.DealerId;
			oResult.AddressId = oAeCustomerModel.AddressId;
			oResult.LeadId = oAeCustomerModel.LeadId;
			oResult.LocalizationId = oAeCustomerModel.LocalizationId;
			oResult.Prefix = oAeCustomerModel.Salutation;
			oResult.FirstName = oAeCustomerModel.FirstName;
			oResult.MiddleName = oAeCustomerModel.MiddleName;
			oResult.LastName = oAeCustomerModel.LastName;
			oResult.Postfix = oAeCustomerModel.Suffix;
			oResult.Gender = oAeCustomerModel.Gender;
			oResult.PhoneHome = oAeCustomerModel.PhoneHome;
			oResult.PhoneWork = oAeCustomerModel.PhoneWork;
			oResult.PhoneMobile = oAeCustomerModel.PhoneMobile;
			oResult.Email = oAeCustomerModel.Email;
			oResult.DOB = oAeCustomerModel.DOB;
			oResult.SSN = oAeCustomerModel.SSN;
			oResult.Username = oAeCustomerModel.Username;
			//oResult.Password = oAeCustomerModel.Password;
			oResult.IsActive = oAeCustomerModel.IsActive;
			oResult.ModifiedOn = oAeCustomerModel.ModifiedOn;
			oResult.ModifiedBy = oAeCustomerModel.ModifiedBy;
			oResult.CreatedOn = oAeCustomerModel.CreatedOn;
			oResult.CreatedBy = oAeCustomerModel.CreatedBy;

			/** Return result. */
			return oResult;
		}

		public static AeCustomerCardInfo CastFnsToAeCustomerCardInfo(IFnsAeCustomerCardInfo item)
		{
			/** Initialize. */
			var result = new AeCustomerCardInfo();

			/** Bind Data. */
			result.CustomerID = item.CustomerID;
			result.CustomerTypeId = item.CustomerTypeId;
			result.CustomerMasterFileID = item.CustomerMasterFileID;
			result.Prefix = item.Prefix;
			result.FirstName = item.FirstName;
			result.MiddleName = item.MiddleName;
			result.LastName = item.LastName;
			result.PostFix = item.PostFix;
			result.FullName = item.FullName;
			result.Gender = item.Gender;
			result.PhoneHome = item.PhoneHome;
			result.PhoneWork = item.PhoneWork;
			result.PhoneMobile = item.PhoneMobile;
			result.Email = item.Email;
			result.DOB = item.DOB;
			result.SSN = item.SSN;
			result.Username = item.Username;
			result.Password = item.Password;
			result.AddressID = item.AddressID;
			result.StreetAddress = item.StreetAddress;
			result.StreetAddress2 = item.StreetAddress2;
			result.City = item.City;
			result.StateId = item.StateId;
			result.PostalCode = item.PostalCode;
			result.PlusFour = item.PlusFour;
			result.CityStateZip = item.CityStateZip;
			result.CreditGroup = item.CreditGroup;

			/** Return result. */
			return result;
		}
		#endregion AeCustomer

		#region VerifyAddress

		//public static VerifyAddress CastFnsToVerifyAddress(IFnsVerifyAddress item)
		//{
		//	/** Init. */
		//	var result = new VerifyAddress();
		//	result.AddressID = item.AddressID;
		//	result.DealerId = item.DealerId;
		//	result.StreetAddress = item.StreetAddress;
		//	result.StreetAddress2 = item.StreetAddress2;
		//	result.StreetNumber = item.StreetNumber;
		//	result.StreetName = item.StreetName;
		//	result.City = item.City;
		//	result.StateId = item.StateId;
		//	result.PostalCode = item.PostalCode;
		//	result.PlusFour = item.PlusFour;
		//	result.County = item.County;
		//	result.PreDirectional = item.PreDirectional;
		//	result.PostDirectional = item.PostDirectional;
		//	result.StreetType = item.StreetType;
		//	result.Extension = item.Extension;
		//	result.ExtensionNumber = item.ExtensionNumber;
		//	result.CarrierRoute = item.CarrierRoute;
		//	result.DPVResponse = item.DPVResponse;
		//	result.PhoneNumber = item.Phone;
		//	result.Latitude = item.Latitude;
		//	result.Longitude = item.Longitude;
		//	result.Validated = item.DPV;
		//	result.SalesRepId = item.SalesRepId;
		//	result.SeasonId = item.SeasonId;
		//	result.TeamLocationId = item.TeamLocationId;
		//	result.TimeZoneId = item.TimeZoneId;
		//	//result.TimeZone = item.TimeZone;
		//	result.IsActive = item.IsActive;
		//	result.CreatedOn = item.CreatedOn;
		//	result.CreatedBy = item.CreatedBy;
		//	result.ModifiedOn = item.ModifiedOn;
		//	result.ModifiedBy = item.ModifiedBy;

		//	/** Return result. */
		//	return result;
		//}

		public static SOS.Services.Interfaces.Models.QualifyLead.QlAddress CastFnsToVerifyAddress(IFnsVerifyAddress item)
		{
			/** Init. */
			var result = new SOS.Services.Interfaces.Models.QualifyLead.QlAddress();
			result.AddressID = item.AddressID;
			result.DealerId = item.DealerId;
			result.StreetAddress = item.StreetAddress;
			result.StreetAddress2 = item.StreetAddress2;
			result.StreetNumber = item.StreetNumber;
			result.StreetName = item.StreetName;
			result.City = item.City;
			result.StateId = item.StateId;
			result.PostalCode = item.PostalCode;
			result.PlusFour = item.PlusFour;
			result.County = item.County;
			result.PreDirectional = item.PreDirectional;
			result.PostDirectional = item.PostDirectional;
			result.StreetType = item.StreetType;
			result.Extension = item.Extension;
			result.ExtensionNumber = item.ExtensionNumber;
			result.CarrierRoute = item.CarrierRoute;
			result.DPVResponse = item.DPVResponse;
			result.Phone = item.Phone;
			result.Latitude = item.Latitude;
			result.Longitude = item.Longitude;
			result.DPV = item.DPV;
			result.SalesRepId = item.SalesRepId;
			result.SeasonId = item.SeasonId;
			result.TeamLocationId = item.TeamLocationId;
			result.TimeZoneId = item.TimeZoneId;
			//result.TimeZone = item.TimeZone;
			result.IsActive = item.IsActive;
			result.CreatedOn = item.CreatedOn;
			result.CreatedBy = item.CreatedBy;
			result.ModifiedOn = item.ModifiedOn;
			result.ModifiedBy = item.ModifiedBy;

			/** Return result. */
			return result;
		}

		#endregion VerifyAddress

		public static QlAddress CastFnsToQlAddress(IFnsQlAddress fnsQlAddress)
		{
			/** Init. */
			var result = new QlAddress();
			result.AddressID = fnsQlAddress.AddressID;
			result.DealerId = fnsQlAddress.DealerId;
			result.AddressTypeId = fnsQlAddress.AddressTypeId;
			result.AddressValidationStateId = fnsQlAddress.AddressValidationStateId;
			result.CarrierRoute = fnsQlAddress.CarrierRoute;
			result.City = fnsQlAddress.City;
			result.CongressionalDistric = fnsQlAddress.CongressionalDistric;
			result.CountryId = fnsQlAddress.CountryId;
			result.County = fnsQlAddress.County;
			result.CountyCode = fnsQlAddress.CountyCode;
			result.CreatedBy = fnsQlAddress.CreatedBy;
			result.CreatedOn = fnsQlAddress.CreatedOn;
			result.DeliveryPoint = fnsQlAddress.DeliveryPoint;
			result.DPV = fnsQlAddress.DPV;
			result.DPVFootnote = fnsQlAddress.DPVFootnote;
			result.DPVResponse = fnsQlAddress.DPVResponse;
			result.Extension = fnsQlAddress.Extension;
			result.ExtensionNumber = fnsQlAddress.ExtensionNumber;
			result.IsActive = fnsQlAddress.IsActive;
			result.IsDeleted = fnsQlAddress.IsDeleted;
			result.Latitude = fnsQlAddress.Latitude;
			result.Longitude = fnsQlAddress.Longitude;
			result.Phone = fnsQlAddress.Phone;
			result.PlusFour = fnsQlAddress.PlusFour;
			result.PostalCode = fnsQlAddress.PostalCode;
			result.PostalCodeFull = fnsQlAddress.PostalCodeFull;
			result.PostDirectional = fnsQlAddress.PostDirectional;
			result.PreDirectional = fnsQlAddress.PreDirectional;
			result.SalesRepId = fnsQlAddress.SalesRepId;
			result.SeasonId = fnsQlAddress.SeasonId;
			result.StateId = fnsQlAddress.StateId;
			result.StreetAddress = fnsQlAddress.StreetAddress;
			result.StreetAddress2 = fnsQlAddress.StreetAddress2;
			result.StreetName = fnsQlAddress.StreetName;
			result.StreetNumber = fnsQlAddress.StreetNumber;
			result.StreetType = fnsQlAddress.StreetType;
			result.TeamLocationId = fnsQlAddress.TeamLocationId;
			result.TimeZoneId = fnsQlAddress.TimeZoneId;
			result.Urbanization = fnsQlAddress.Urbanization;
			result.UrbanizationCode = fnsQlAddress.UrbanizationCode;
			result.ValidationVendorId = fnsQlAddress.ValidationVendorId;

			/** Return result. */
			return result;
		}

		public static List<AeInvoiceTemplate> CastFnsToAeInvoiceTemplateList(List<IFnsAeInvoiceTemplate> valueList)
		{
			// ** Return result
			return valueList.Select(aeInvoice => new AeInvoiceTemplate
			{
				InvoiceTemplateID = aeInvoice.InvoiceTemplateID,
				DealerId = aeInvoice.DealerId,
				ActivationItemId = aeInvoice.ActivationItemId,
				ActivationDiscountItemId = aeInvoice.ActivationDiscountItemId,
				MMRItemId = aeInvoice.MMRItemId,
				MMRDiscountItemId = aeInvoice.MMRDiscountItemId,
				ActivationOverThreeMonthsId = aeInvoice.ActivationOverThreeMonthsId,
				TemplateName = aeInvoice.TemplateName,
				ActivationDiscountAmount = aeInvoice.ActivationDiscountAmount,
				MMRDiscountAmount = aeInvoice.MMRDiscountAmount,
				SystemPoints = aeInvoice.SystemPoints
			}).ToList();
		}

		public static AeItem CastFnsToAeItem(IFnsAeItem aeItem)
		{
			// ** Return result
			return new AeItem
			{
				ItemID = aeItem.ItemID,
				ItemTypeId = aeItem.ItemTypeId,
				TaxOptionId = aeItem.TaxOptionId,
				VerticalId = aeItem.VerticalId,
				ModelNumber = aeItem.ModelNumber,
				ItemSKU = aeItem.ItemSKU,
				ItemDesc = aeItem.ItemDesc,
				Price = aeItem.Price,
				Cost = aeItem.Cost,
				SystemPoints = aeItem.SystemPoints,
				IsCatalogItem = aeItem.IsCatalogItem,
				IsActive = aeItem.IsActive,
				IsDeleted = aeItem.IsDeleted
			};
		}
		public static List<AeItem> CastFnsToAeItemList(List<IFnsAeItem> fnsValueList)
		{
			// ** Return result
			return fnsValueList.ConvertAll(CastFnsToAeItem);
		}

		public static List<AeItem> CastFnsToAeItemList(List<IFnsMsEquipmentsView> fnsValueList)
		{
			// ** Return result
			return fnsValueList.Select(aeItem => new AeItem
			{
				ItemID = aeItem.EquipmentID,
				//ItemTypeId = aeItem.ItemTypeId,
				//TaxOptionId = aeItem.TaxOptionId,
				//ModelNumber = aeItem.ModelNumber,
				ItemSKU = aeItem.GPItemNmbr,
				ItemDesc = aeItem.GenDescription,
				Price = aeItem.RetailPrice,
				//Cost = aeItem.Cost,
				SystemPoints = aeItem.Points,
				//IsCatalogItem = aeItem.IsCatalogItem,
				IsActive = aeItem.IsActive,
				IsDeleted = aeItem.IsDeleted
			}).ToList();
		}

		public static List<MsAccountCellularType> CastFnsToMsCellularTypeList(List<IFnsMsAccountCellularType> fnsMsAccountCellularTypes)
		{
			return fnsMsAccountCellularTypes.Select(msItem => new MsAccountCellularType
			{
				CellularTypeID = msItem.CellularTypeID,
				CellularTypeName = msItem.CellularTypeName
			}).ToList();
		}

		public static List<MsAccountServiceType> CastFnsToMsServiceTypeList(List<IFnsMsAccountServiceType> fnsMsAccountServiceTypes)
		{
			return fnsMsAccountServiceTypes.Select(msItem => new MsAccountServiceType
			{
				SystemTypeID = msItem.SystemTypeID,
				SystemTypeName = msItem.SystemTypeName
			}).ToList();
		}

		public static List<MsAccountPanelType> CastFnsToMsPanelTypeList(List<IFnsMsAccountPanelType> fnsMsAccountPanelTypes)
		{
			return fnsMsAccountPanelTypes.Select(msItem => new MsAccountPanelType
			{
				PanelTypeID = msItem.PanelTypeID,
				PanelTypeName = msItem.PanelTypeName,
				UIName = msItem.UIName
			}).ToList();
		}

		public static List<MsVendorAlarmComPackage> CastFnsToMsVendorAlarmComPackageList(List<IFnsMsVendorAlarmComPackage> packageList)
		{
			return packageList.Select(msPackage => new MsVendorAlarmComPackage
			{
				AlarmComPackageID = msPackage.AlarmComPackageID,
				PackageName = msPackage.PackageName,
				DefaultValue = msPackage.DefaultValue
			}).ToList();
		}

		public static List<AE_ContractTemplate> CastFnsToAeContractTemplateList(List<IFnsAeContractTemplate> fnsAeContractTemplates)
		{
			return fnsAeContractTemplates.Select(template => new AE_ContractTemplate
			{
				ContractTemplateID = template.ContractTemplateID,
				ContractName = template.ContractName,
				ContractLength = template.ContractLength,
				MonthlyFee = template.MonthlyFee,
				ShortDesc = template.ShortDesc
			}).ToList();
		}

		public static List<MsEmergencyContactPhoneType> CastFnsToMsEmergencyPhoneTypeList(List<IFnsMsEmergencyContactPhoneType> fnsList)
		{
			return fnsList.Select(phoneType => new MsEmergencyContactPhoneType
			{
				PhoneTypeID = phoneType.PhoneTypeID,
				MonitoringStationOSId = phoneType.MonitoringStationOSId,
				MsPhoneTypeId = phoneType.MsPhoneTypeId,
				PhoneTypeDescription = phoneType.PhoneTypeDescription
			}).ToList();
		}

		public static List<MsEmergencyContactRelationship> CastFnsToMsEmergencyContactRelationshipList(List<IFnsMsEmergencyContactRelationship> fnsList)
		{
			return fnsList.Select(relationship => new MsEmergencyContactRelationship
			{
				RelationshipID = relationship.RelationshipID,
				MonitoringStationOSId = relationship.MonitoringStationOSId,
				MsRelationshipId = relationship.MsRelationshipId,
				RelationshipDescription = relationship.RelationshipDescription,
				IsEVC = relationship.IsEVC,
				IsActive = relationship.IsActive,
				IsDeleted = relationship.IsDeleted,
				ModifiedOn = relationship.ModifiedOn,
				ModifiedBy = relationship.ModifiedBy,
				CreatedOn = relationship.CreatedOn,
				CreatedBy = relationship.CreatedBy
			}).ToList();
		}

		public static List<MsEmergencyContactAuthority> CastFnsToMsEmergencyContactAuthorityList(List<IFnsMsEmergencyContactAuthority> fnsList)
		{
			return fnsList.Select(relationship => new MsEmergencyContactAuthority
			{
				AuthorityID = relationship.AuthorityID,
				MonitoringStationOSId = relationship.MonitoringStationOSId,
				MsAuthorityId = relationship.MsAuthorityId,
				AuthorityDescription = relationship.AuthorityDescription,
				IsActive = relationship.IsActive,
				IsDeleted = relationship.IsDeleted,
				ModifiedOn = relationship.ModifiedOn,
				ModifiedBy = relationship.ModifiedBy,
				CreatedOn = relationship.CreatedOn,
				CreatedBy = relationship.CreatedBy
			}).ToList();
		}

		public static List<MsEmergencyContactType> CastFnsToMsEmergencyContactTypeList(List<IFnsMsEmergencyContactType> fnsList)
		{
			return fnsList.Select(relationship => new MsEmergencyContactType
			{
				EmergencyContactTypeID = relationship.EmergencyContactTypeID,
				MonitoringStationOSId = relationship.MonitoringStationOSId,
				MsContactTypeId = relationship.MsContactTypeId,
				ContactTypeDescription = relationship.ContactTypeDescription,
				IsActive = relationship.IsActive,
				IsDeleted = relationship.IsDeleted,
				ModifiedOn = relationship.ModifiedOn,
				ModifiedBy = relationship.ModifiedBy,
				CreatedOn = relationship.CreatedOn,
				CreatedBy = relationship.CreatedBy
			}).ToList();
		}

		public static List<MsEmergencyContact> CastFnsToMsEmergencyContactList(List<IFnsMsEmergencyContact> fnsList)
		{
			return fnsList.Select(emc => new MsEmergencyContact
			{
				EmergencyContactID = emc.EmergencyContactID,
				CustomerId = emc.CustomerId,
				AccountId = emc.AccountId,
				RelationshipId = emc.RelationshipId,
				OrderNumber = emc.OrderNumber,
				Allergies = emc.Allergies,
				MedicalConditions = emc.MedicalConditions,
				HasKey = emc.HasKey,
				DOB = emc.DOB,
				Prefix = emc.Prefix,
				FirstName = emc.FirstName,
				MiddleName = emc.MiddleName,
				LastName = emc.LastName,
				Postfix = emc.Postfix,
				Email = emc.Email,
				Password = emc.Password,
				Phone1 = emc.Phone1,
				Phone1TypeId = emc.Phone1TypeId,
				Phone2 = emc.Phone2,
				Phone2TypeId = emc.Phone2TypeId,
				Phone3 = emc.Phone3,
				Phone3TypeId = emc.Phone3TypeId,
				Comment1 = emc.Comment1
			}).ToList();
		}


		public static List<MsAccountEquipment> CastFnsToMsAccountEquipmentList(List<IFnsMsAccountEquipmentsView> fnsList)
		{
			return fnsList.Select(ae => new MsAccountEquipment
			{
				AccountEquipmentID = ae.AccountEquipmentID,
				AccountId = ae.AccountId,
				EquipmentId = ae.EquipmentId,
				EquipmentLocationId = ae.EquipmentLocationId,
				GPEmployeeId = ae.GPEmployeeId,
				AccountEquipmentUpgradeTypeId = ae.AccountEquipmentUpgradeTypeId,
				Points = ae.Points,
				ActualPoints = ae.ActualPoints,
				Price = ae.Price,
				IsExisting = ae.IsExisting,
				BarcodeId = ae.BarcodeId,
				IsServiceUpgrade = ae.IsServiceUpgrade,
				IsExistingWiring = ae.IsExistingWiring,
				IsMainPanel = ae.IsMainPanel,
				AccountZoneAssignmentID = ae.AccountZoneAssignmentID,
				AccountZoneTypeId = ae.AccountZoneTypeId,
				AccountEventId = ae.AccountEventId,
				Zone = ae.Zone,
				Comments = ae.Comments,
				ItemSKU = ae.ItemSKU,
				ItemDesc = ae.ItemDesc,
				AccountZoneType = ae.AccountZoneType,
				EquipmentLocationDesc = ae.EquipmentLocationDesc
			}).ToList();
		}

		public static List<MsAccountDslSeisureTypes> CastFnsToMsDslSeizureTypeList(List<IFnsMsAccountDslSeizureType> typeList)
		{
			return typeList.Select(item => new MsAccountDslSeisureTypes
			{
				DslSeizureID = item.DslSeizureID,
				DslSeizure = item.DslSeizure
			}).ToList();
		}

		public static List<MsAccountEventZoneEventTypes> CastFnsToMsAccountZoneEventTypeList(List<IFnsMsAccountEventZoneEventTypes> typesList)
		{
			return typesList.Select(item => new MsAccountEventZoneEventTypes
			{
				ZoneEventTypeID = item.ZoneEventTypeId,
				MonitoringStationOSID = item.MonitoringStationOSID,
				Descrption = item.Description
			}).ToList();
		}

		public static List<MsEquipmentLocation> CastFnsToMsEquipmentLocationList(List<IFnsMsEquipmentLocation> locationList)
		{
			return locationList.Select(item => new MsEquipmentLocation
			{
				EquipmentLocationID = item.EquipmentLocationID,
				EquipmentLocationDesc = item.EquipmentLocationDesc,
				MonitronicsCode = item.MonitronicsCode,
				CriticomCode = item.CriticomCode,
				AvantGuardCode = item.AvantGuardCode,
				LocationCode = item.LocationCode,
				IsActive = item.IsActive,
				IsDeleted = item.IsDeleted
			}).OrderBy(q => q.EquipmentLocationDesc).ToList();
		}

		public static List<MsAccountZoneType> CastFnsToMsAccountZoneTypesList(List<IFnsMsAccountZoneType> zoneTypesList)
		{
			return zoneTypesList.Select(item => new MsAccountZoneType
			{
				AccountZoneTypeID = item.AccountZoneTypeID,
				AccountZoneType = item.AccountZoneType
			}).ToList();
		}

		public static List<MsEquipment> CastFnsToMsEquipmentList(List<IFnsMsEquipmentsView> equipmentList)
		{
			return equipmentList.Select(item => new MsEquipment
			{
				EquipmentID = item.EquipmentID,
				ShortName = item.ShortName,
				EquipmentTypeId = item.EquipmentTypeId,
			})/*.OrderBy(q=>q.ShortName)*/.ToList();
		}

		public static List<MsIndustryAccount> CastFnsToMsIndustryAccountList(List<IFnsMsIndustryAccount> industryList)
		{
			return industryList.Select(item => new MsIndustryAccount
			{
				IndustryAccountID = item.IndustryAccountID,
				AccountId = item.AccountId,
				ReceiverLineId = item.ReceiverLineId,
				ReceiverLineBlockId = item.ReceiverLineBlockId,
				IndustryAccount = item.IndustryAccount,
				Designator = item.Designator,
				SubscriberNumber = item.SubscriberNumber,
				ReceiverNumber = item.ReceiverNumber,
				IsActive = item.IsActive
			}).ToList();
		}

		public static List<MsIndustryAccountWithReceiverLineInfo> CastFnsToMsIndustryAccountList(List<IFnsMsIndustryAccountNumbersWithReceiverLineInfoView> industryList)
		{
			return industryList.Select(item => new MsIndustryAccountWithReceiverLineInfo
			{
				IndustryAccountID = item.IndustryAccountID,
				AccountId = item.AccountId,
				ReceiverNumber = item.ReceiverNumber,
				Designator = item.Designator,
				SubscriberNumber = item.SubscriberNumber,
				IndustryAccount = item.IndustryAccount,
				MonitoringStationOSID = item.MonitoringStationOSID,
				OSDescription = item.OSDescription,
				MonitoringStationName = item.MonitoringStationName,
				PrimaryCSID = item.PrimaryCSID,
				SecondaryCSID = item.SecondaryCSID
			}).ToList();
		}

		public static MsIndustryAccount CastFnsToMsIndustryAccount(IFnsMsIndustryAccount item)
		{
			return new MsIndustryAccount
			{
				IndustryAccountID = item.IndustryAccountID,
				AccountId = item.AccountId,
				ReceiverLineId = item.ReceiverLineId,
				ReceiverLineBlockId = item.ReceiverLineBlockId,
				IndustryAccount = item.IndustryAccount,
				Designator = item.Designator,
				SubscriberNumber = item.SubscriberNumber,
				ReceiverNumber = item.ReceiverNumber,
				IsActive = item.IsActive
			};
		}

		public static List<CustomerSwingEmergencyContact> CastFnsToCustomerSwingEmergencyContactList(List<IFnsCustomerSwingEmergencyContact> fnsCustomerSwingEmergencyContacts)
		{
			return fnsCustomerSwingEmergencyContacts.Select(msItem => new CustomerSwingEmergencyContact
			{
				FirstName = msItem.FirstName,
				LastName = msItem.LastName,
				MiddleInit = msItem.MiddleInit,
				PhoneNumber1 = msItem.PhoneNumber1,
				Relationship = msItem.Relationship
			}).ToList();
		}

		public static List<CustomerSwingEquipmentInfo> CastFnsToCustomerSwingEquipmentInfoList(List<IFnsCustomerSwingEquipmentInfo> fnsCustomerSwingEquipmentInfos)
		{
			return fnsCustomerSwingEquipmentInfos.Select(msItem => new CustomerSwingEquipmentInfo
			{
				RowNumber = msItem.RowNumber,
				ZoneNumber = msItem.ZoneNumber,
				EquipmentLocationDesc = msItem.EquipmentLocationDesc,
				FullName = msItem.FullName,
				ZoneTypeName = msItem.ZoneTypeName
			}).ToList();
		}

		public static QlQualifyCustomerInfo CastFnsToQlQualifyCustomerInfo(IFnsQlQualifyCustomerInfo item)
		{
			return new QlQualifyCustomerInfo
			{
				LeadID = item.LeadID,
				SeasonId = item.SeasonId,
				CustomerName = item.CustomerName,
				CustomerEmail = item.CustomerEmail,
				DOB = item.DOB,
				AddressID = item.AddressID,
				StreetAddress = item.StreetAddress,
				StreetAddress2 = item.StreetAddress2,
				City = item.City,
				StateId = item.StateId,
				County = item.County,
				TimeZoneId = item.TimeZoneId,
				TimeZoneName = item.TimeZoneName,
				PostalCode = item.PostalCode,
				Phone = item.Phone,
				CreditCreatedOn = item.CreditCreatedOn,
				CreditReportID = item.CreditReportID,
				IsHit = item.IsHit,
				CRStatus = item.CRStatus,
				Score = item.Score,
				CreditGroup = item.CreditGroup,
				BureauName = item.BureauName,
				UserID = item.UserID,
				CompanyID = item.CompanyID,
				FirstName = item.FirstName,
				MiddleName = item.MiddleName,
				LastName = item.LastName,
				PreferredName = item.PreferredName,
				RepEmail = item.RepEmail,
				PhoneCell = item.PhoneCell,
				PhoneCellCarrierID = item.PhoneCellCarrierID,
				PhoneCellCarrier = item.PhoneCellCarrier,
				SeasonName = item.SeasonName
			};
		}

		public static List<MsSignalHistoryItem> CastFnsToMsSignalHistoryList(List<IFnsSignalHistoryItemModel> itemsList)
		{
			var result = new List<MsSignalHistoryItem>();
			foreach (var item in itemsList)
			{
				var msSignal = new MsSignalHistoryItem();
				msSignal.AlarmNum = item.AlarmNum;
				msSignal.AreaNum = item.AreaNum;
				msSignal.Comment = item.Comment;
				msSignal.EventCode = item.EventCode;
				msSignal.EventCodeDescription = item.EventCodeDescription;
				msSignal.FullClearFlag = item.FullClearFlag;
				msSignal.HistoryDate = item.HistoryDate;
				msSignal.Latitude = item.Latitude;
				msSignal.Longitude = item.Longitude;
				msSignal.OpAct = item.OpAct;
				msSignal.OpActDescription = item.OpActDescription;
				msSignal.Phone = item.Phone;
				msSignal.Point = item.Point;
				msSignal.PointDescription = item.PointDescription;
				msSignal.RawMessage = item.RawMessage;
				msSignal.SignalCode = item.SignalCode;
				msSignal.SiteName = item.SiteName;
				msSignal.TestNum = item.TestNum;
				msSignal.TransmitterCode = item.TransmitterCode;
				msSignal.UserId = item.UserId;
				msSignal.UserName = item.UserName;
				msSignal.UTCDate = item.UTCDate;

				// ** Add to list
				result.Add(msSignal);
			}

			// ** Return result.
			return result;
		}

		public static MsAccountSalesInformation CastFnsToMsAccountSalesInformation(IFnsMsAccountSalesInformation info)
		{
			return new MsAccountSalesInformation
			{
				AccountID = info.AccountID,
				PaymentTypeId = info.PaymentTypeId,
				BillingDay = info.BillingDay,
				CurrentMonitoringStation = info.CurrentMonitoringStation,
				PanelTypeId = info.PanelTypeId,
				PanelItemId = info.PanelItemId,
				IsTakeOver = info.IsTakeOver,
				IsOwner = info.IsOwner,
				CellPackageItemId = info.CellPackageItemId,
				CellServicePackage = info.CellServicePackage,
				CellularTypeId = info.CellularTypeId,
				CellularTypeName = info.CellularTypeName,
				CellularVendor = info.CellularVendor,
				SetupFee = info.SetupFee,
				Setup1StMonth = info.SetupFee1StMonth,
				MMR = info.MMR,
				Over3Months = info.Over3Months,
				ContractLength = info.ContractLength,
				ContractId = info.ContractId,
				ContractTemplateId = info.ContractTemplateId,
				Email = info.Email,
				IsMoni = info.IsMoni,

				FriendsAndFamilyTypeId = info.FriendsAndFamilyTypeId,
				AccountSubmitId = info.AccountSubmitId,
				AccountCancelReasonId = info.AccountCancelReasonId,
				TechId = info.TechId,
				SalesRepId = info.SalesRepId,
				InstallDate = info.InstallDate,
				SubmittedToCSDate = info.SubmittedToCSDate,
				CsConfirmationNumber = info.CsConfirmationNumber,
				CsTwoWayConfNumber = info.CsTwoWayConfNumber,
				SubmittedToGPDate = info.SubmittedToGPDate,
				ContractSignedDate = info.ContractSignedDate,
				CancelDate = info.CancelDate,
				AMA = info.AMA,
				NOC = info.NOC,
				SOP = info.SOP,
				ApprovedDate = info.ApprovedDate,
				ApproverID = info.ApproverID,
				NOCDate = info.NOCDate,
			};
		}

		#region InventoryEngine

		public static IePurchaseOrder CastFnsToIePurchaseOrder(IFnsIePurchaseOrder oItem)
		{
			/** Initialize. */
			var oResult = new IePurchaseOrder();

			/** Bind data. */
			oResult.PurchaseOrderID = oItem.PurchaseOrderID;
			//oResult.WarehouseSiteId = oItem.WarehouseSiteId;
			oResult.VendorId = oItem.VendorId;
			//oResult.CloseDate = oItem.CloseDate;
			oResult.IsActive = oItem.IsActive;
			oResult.ModifiedOn = oItem.ModifiedOn;
			oResult.ModifiedBy = oItem.ModifiedBy;
			oResult.CreatedOn = oItem.CreatedOn;
			oResult.CreatedBy = oItem.CreatedBy;

			return oResult;
		}
		public static List<IePurchaseOrderItem> CastFnsToIePurchaseOrderItemList(List<IFnsIePurchaseOrderItem> purchaseOrderItemList)
		{
			return purchaseOrderItemList.Select(item => new IePurchaseOrderItem
			{
				PurchaseOrderItemID = item.PurchaseOrderItemID,
				PurchaseOrderId = item.PurchaseOrderId,
				//ProductSkwId = item.ProductSkwId,
				ProductSKU = item.ProductSKU,
				ItemId = item.ItemId,
				Quantity = item.Quantity,
				ItemDesc = item.ItemDesc,
				WithBarcodeCount = item.WithBarcodeCount,
				WithoutBarcodeCount = item.WithoutBarcodeCount
			}).ToList();
		}

		public static List<IePackingSlip> CastFnsToIePackingSlip(List<IFnsIePackingSlip> packingSlipList)
		{

			return packingSlipList.Select(item => new IePackingSlip
			{
				PackingSlipID = item.PackingSlipID,
				PurchaseOrderId = item.PurchaseOrderId,
				PackingSlipNumber = item.PackingSlipNumber,
				ArrivalDate = item.ArrivalDate,
				CloseDate = item.CloseDate,
				IsActive = item.IsActive,
				GPPONumber = item.GPPONumber

			}).ToList();
		}

		public static IePackingSlip CastFnsToIePackingSlip(IFnsIePackingSlip oItem)
		{
			/** Initialize. */
			var oResult = new IePackingSlip();

			/** Bind data. */
			oResult.PackingSlipID = oItem.PackingSlipID;
			oResult.PurchaseOrderId = oItem.PurchaseOrderId;
			oResult.PackingSlipNumber = oItem.PackingSlipNumber;
			oResult.ArrivalDate = oItem.ArrivalDate;
			oResult.CloseDate = oItem.CloseDate;
			oResult.IsActive = oItem.IsActive;
			oResult.ModifiedOn = oItem.ModifiedOn;
			oResult.ModifiedBy = oItem.ModifiedBy;
			oResult.CreatedOn = oItem.CreatedOn;
			oResult.CreatedBy = oItem.CreatedBy;

			return oResult;
		}

		public static IeProductBarcode CastFnsToIeProductBarcode(IFnsIeProductBarcode oItem)
		{
			/** Initialize. */
			var oResult = new IeProductBarcode();
			/** Bind data. */
			oResult.ProductBarcodeID = oItem.ProductBarcodeID;
			oResult.PurchaseOrderItemId = oItem.PurchaseOrderItemId;
			oResult.LastProductBarcodeTrackingId = oItem.LastProductBarcodeTrackingId;
			oResult.ProductBarcodeBundleId = oItem.ProductBarcodeBundleId;
			oResult.SimGUID = oItem.SimGUID;
			oResult.IsDeleted = oItem.IsDeleted;
			oResult.ModifiedOn = oItem.ModifiedOn;
			oResult.ModifiedBy = oItem.ModifiedBy;
			oResult.CreatedOn = oItem.CreatedOn;
			oResult.CreatedBy = oItem.CreatedBy;

			return oResult;
		}

		public static IeProductBarcodeTracking CastFnsToIeProductBarcodeTracking(IFnsIeProductBarcodeTracking oItem)
		{
			/** Initialize. */
			var oResult = new IeProductBarcodeTracking();

			/** Bind data. */
			oResult.ProductBarcodeTrackingID = oItem.ProductBarcodeTrackingID;
			oResult.ProductBarcodeTrackingTypeId = oItem.ProductBarcodeTrackingTypeId;
			oResult.ProductBarcodeId = oItem.ProductBarcodeId;
			oResult.LocationTypeID = oItem.LocationTypeID;
			oResult.LocationID = oItem.LocationID;
			/*oResult.TransferToWarehouseSiteId = oItem.TransferToWarehouseSiteId;
			oResult.ReturnToVendorId = oItem.ReturnToVendorId;
			oResult.AssignedToCustomerId = oItem.AssignedToCustomerId;
			oResult.AssignedToDealerId = oItem.AssignedToDealerId;
			oResult.RtmaNumberId  = oItem.RtmaNumberId;*/
			oResult.Comment = oItem.Comment;
			oResult.IsDeleted = oItem.IsDeleted;
			oResult.ModifiedOn = oItem.ModifiedOn;
			oResult.ModifiedBy = oItem.ModifiedBy;
			oResult.CreatedOn = oItem.CreatedOn;
			oResult.CreatedBy = oItem.CreatedBy;

			return oResult;
		}

		public static IeProductBarcodeTrackingView CastFnsToIeProductBarcodeTrackingView(IFnsIeProductBarcodeTrackingView oItem)
		{
			/** Initialize. */
			var oResult = new IeProductBarcodeTrackingView();

			/** Bind data. */
			oResult.ProductBarcodeTrackingID = oItem.ProductBarcodeTrackingID;
			oResult.ProductBarcodeTrackingTypeId = oItem.ProductBarcodeTrackingTypeId;
			oResult.ProductBarcodeId = oItem.ProductBarcodeId;
			oResult.LocationTypeID = oItem.LocationTypeID;
			oResult.LocationID = oItem.LocationID;
			/* oResult.TransferToWarehouseSiteId = oItem.TransferToWarehouseSiteId;
			 oResult.Location = oItem.Location;
			 oResult.ReturnToVendorId = oItem.ReturnToVendorId;
			 oResult.AssignedToCustomerId = oItem.AssignedToCustomerId;
			 oResult.AssignedToDealerId = oItem.AssignedToDealerId;
			 oResult.RtmaNumberId = oItem.RtmaNumberId;*/
			oResult.Comment = oItem.Comment;

			return oResult;
		}


		public static List<IeWarehouseSite> CastFnsToIeWarehouseSiteList(List<IFnsIeWarehouseSite> warehouseSiteList)
		{
			return warehouseSiteList.Select(item => new IeWarehouseSite
			{
				WarehouseSiteID = item.WarehouseSiteID,
				WarehouseSiteName = item.WarehouseSiteName

			}).ToList();
		}

		public static List<IeLocationType> CastFnsToIeLocationTypeList(List<IFnsIeLocationType> locationTypeList)
		{
			return locationTypeList.Select(item => new IeLocationType
			{
				LocationTypeID = item.LocationTypeID,
				LocationTypeName = item.LocationTypeName

			}).ToList();
		}

		public static List<IeLocation> CastFnsToIeLocationList(List<IFnsIeLocation> locationList)
		{
			return locationList.Select(item => new IeLocation
			{
				LocationID = item.LocationID,
				LocationName = item.LocationName

			}).ToList();
		}


		public static List<IeProductBarcodeLocation> CastFnsToIeProductBarcodeLocationList(List<IFnsIeProductBarcodeLocation> productBarcodeLocationList)
		{
			return productBarcodeLocationList.Select(item => new IeProductBarcodeLocation
			{
				ProductBarcodeId = item.ProductBarcodeId,
				ItemDesc = item.ItemDesc

			}).ToList();
		}

		public static IeProductBarcodeLocation CastFnsToIeProductBarcodeLocation(IFnsIeProductBarcodeLocation productBarcodeLocation)
		{
			var oResult = new IeProductBarcodeLocation();

			/** Bind data. */
			oResult.ProductBarcodeId = productBarcodeLocation.ProductBarcodeId;
			oResult.ItemDesc = productBarcodeLocation.ItemDesc;


			return oResult;
		}


		public static List<IeVendor> CastFnsToIeVendorList(List<IFnsIeVendor> vendorList)
		{
			return vendorList.Select(item => new IeVendor
			{
				VendorID = item.VendorID,
				VendorName = item.VendorName

			}).ToList();
		}

		public static List<IePurchaseOrder> CastFnsToIePurchaseOrderList(List<IFnsIePurchaseOrder> purchaseOrderList)
		{
			return purchaseOrderList.Select(item => new IePurchaseOrder
			{
				PurchaseOrderID = item.PurchaseOrderID,
				GPPONumber = item.GPPONumber

			}).ToList();
		}

		#endregion InventoryEngine

		#region HumanResource
		public static List<RuTeamLocation> CastFnsToRuTeamLocationList(List<SOS.FunctionalServices.Contracts.Models.HumanResource.IFnsRuTeamLocation> ruTeamLocationList)
		{
			return ruTeamLocationList.Select(item => new RuTeamLocation
			{
				TeamLocationID = item.TeamLocationID,
				City = item.City
			}).ToList();
		}


		public static List<RuTechnician> CastFnsToRuTechnicianList(List<IFnsRuTechnician> ruTechnicianList)
		{
			return ruTechnicianList.Select(item => new RuTechnician
			{
				TechnicianId = item.TechnicianId,
				FullName = item.FullName,
				TechFirstName = item.TechFirstName,
				TechLastName = item.TechLastName,
				TechBirthDate = item.TechBirthDate,
				TechSeasonId = item.TechSeasonId,
				TechSeasonName = item.TechSeasonName


			}).ToList();
		}

		public static RuTechnician CastFnsToRuTechnician(IFnsRuTechnician ruTechnician)
		{
			// ** Return result
			return new RuTechnician
			{
				TechnicianId = ruTechnician.TechnicianId,
				FullName = ruTechnician.FullName,
				TechFirstName = ruTechnician.TechFirstName,
				TechLastName = ruTechnician.TechLastName,
				TechBirthDate = ruTechnician.TechBirthDate,
				TechSeasonId = ruTechnician.TechSeasonId,
				TechSeasonName = ruTechnician.TechSeasonName
			};
		}

		#endregion HumanResource

		#region ScheduleEngine
		/*
		public static List<SeTicketStatusCode> CastFnsToSeTicketStatusCodeList(List<IFnsSeTicketStatusCode> seTicketStatusCodeList)
		{
			return seTicketStatusCodeList.Select(item => new SeTicketStatusCode
			{
				StatusCodeID = item.StatusCodeID,
				StatusCode = item.StatusCode

			}).ToList();
		}

		public static List<SeTicketType> CastFnsToSeTicketTypeList(List<IFnsSeTicketType> seTicketTypeList)
		{
			return seTicketTypeList.Select(item => new SeTicketType
			{
				TicketTypeID = item.TicketTypeID,
				TicketTypeName = item.TicketTypeName

			}).ToList();
		}


		public static SeTicket CastFnsToSeTicket(IFnsSeTicket fnsSeTicket)
		{
			return new SeTicket
			{
				TicketID = fnsSeTicket.TicketID,
				AccountId = fnsSeTicket.AccountId,
				CustomerMasterFileId = fnsSeTicket.CustomerMasterFileId,
				MonitoringStationNo = fnsSeTicket.MonitoringStationNo,
				TicketTypeId = fnsSeTicket.TicketTypeId,
				TicketTypeName = fnsSeTicket.TicketTypeName,
				Weight = fnsSeTicket.Weight,
				StatusCodeId = fnsSeTicket.StatusCodeId,
				StatusCode = fnsSeTicket.StatusCode,
				MoniConfirmation = fnsSeTicket.MoniConfirmation,
				TechConfirmation = fnsSeTicket.TechConfirmation,
				TechnicianId = fnsSeTicket.TechnicianId,
				TechnicianName = fnsSeTicket.TechnicianName,
				TripCharges = fnsSeTicket.TripCharges,
				Appointment = fnsSeTicket.Appointment,
				AgentConfirmation = fnsSeTicket.AgentConfirmation,
				ExpirationDate = fnsSeTicket.ExpirationDate,
				Notes = fnsSeTicket.Notes,
				IsTechEnRoute = fnsSeTicket.IsTechEnRoute,
				IsTechDelayed = fnsSeTicket.IsTechDelayed,
				IsTechCompleted = fnsSeTicket.IsTechCompleted,
				CustomerFullName = fnsSeTicket.CustomerFullName,
				Address = fnsSeTicket.Address,
				CompleteAddress = fnsSeTicket.CompleteAddress,
				StreetAddress = fnsSeTicket.StreetAddress,
				CityStateZip = fnsSeTicket.CityStateZip,
				County = fnsSeTicket.County,
				State = fnsSeTicket.State,
				PostalCode = fnsSeTicket.PostalCode,
				Latitude = fnsSeTicket.Latitude,
				Longitude = fnsSeTicket.Longitude,
				PhoneHome = fnsSeTicket.PhoneHome,
				PhoneMobile = fnsSeTicket.PhoneMobile,
				AppointmentDate = fnsSeTicket.AppointmentDate,
				BlockId = fnsSeTicket.BlockId,
				TravelTime = fnsSeTicket.TravelTime,
				ZipCode = fnsSeTicket.ZipCode,
				MaxRadius = fnsSeTicket.MaxRadius,
				Distance = fnsSeTicket.Distance,
				StartTime = fnsSeTicket.StartTime,
				EndTime = fnsSeTicket.EndTime,
				ScheduleTicketId = fnsSeTicket.ScheduleTicketId
			};
		}

		public static List<SeTicket> CastFnsToSeTicketList(List<IFnsSeTicket> seTicketList)
		{
			return seTicketList.Select(item => new SeTicket
			{
				TicketID = item.TicketID,
				AccountId = item.AccountId,
				CustomerMasterFileId = item.CustomerMasterFileId,
				MonitoringStationNo = item.MonitoringStationNo,
				TicketTypeId = item.TicketTypeId,
				TicketTypeName = item.TicketTypeName,
				Weight = item.Weight,
				StatusCodeId = item.StatusCodeId,
				StatusCode = item.StatusCode,
				MoniConfirmation = item.MoniConfirmation,
				TechConfirmation = item.TechConfirmation,
				TechnicianId = item.TechnicianId,
				TripCharges = item.TripCharges,
				Appointment = item.Appointment,
				AgentConfirmation = item.AgentConfirmation,
				ExpirationDate = item.ExpirationDate,
				Notes = item.Notes,
				IsTechEnRoute = item.IsTechEnRoute,
				IsTechDelayed = item.IsTechDelayed,
				IsTechCompleted = item.IsTechCompleted,
				CustomerFullName = item.CustomerFullName,
				Address = item.Address,
				CompleteAddress = item.CompleteAddress,
				StreetAddress = item.StreetAddress,
				CityStateZip = item.CityStateZip,
				County = item.County,
				State = item.State,
				PostalCode = item.PostalCode,
				Latitude = item.Latitude,
				Longitude = item.Longitude,
				PhoneHome = item.PhoneHome,
				PhoneMobile = item.PhoneMobile,
				AppointmentDate = item.AppointmentDate,
				BlockId = item.BlockId,
				TravelTime = item.TravelTime,
				ZipCode = item.ZipCode,
				MaxRadius = item.MaxRadius,
				Distance = item.Distance,
				StartTime = item.StartTime,
				EndTime = item.EndTime,
				ScheduleTicketId = item.ScheduleTicketId

			}).ToList();
		}


		public static List<SeScheduleBlock> CastFnsToSeScheduleBlockList(List<IFnsSeScheduleBlock> seScheduleBlockList)
		{
			return seScheduleBlockList.Select(scheduleBlock => new SeScheduleBlock
			{
				BlockID = scheduleBlock.BlockID,
				Block = scheduleBlock.Block,
				ZipCode = scheduleBlock.ZipCode,
				MaxRadius = scheduleBlock.MaxRadius,
				//Distance = scheduleBlock.Distance,
				Distance = scheduleBlock.Distance ?? CustomGeoCoordinate.GetDistance(scheduleBlock.BlockLatitude ?? 0,
														   scheduleBlock.BlockLongitude ?? 0,
														   scheduleBlock.TicketLatitude ?? 0,
														   scheduleBlock.TicketLongitude ?? 0
				),
				AvailableSlots = scheduleBlock.AvailableSlots,
				StartTime = scheduleBlock.StartTime,
				EndTime = scheduleBlock.EndTime,
				TechnicianId = scheduleBlock.TechnicianId,
				TechnicianName = scheduleBlock.TechnicianName,
				IsTechConfirmed = scheduleBlock.IsTechConfirmed,
				CurrentTicketId = scheduleBlock.CurrentTicketId,
				BlockLatitude = scheduleBlock.BlockLatitude,
				BlockLongitude = scheduleBlock.BlockLongitude,
				TicketLatitude = scheduleBlock.TicketLatitude,
				TicketLongitude = scheduleBlock.TicketLongitude,
				IsRed = scheduleBlock.IsRed,
				Color = scheduleBlock.Color,
				NoOfTickets = scheduleBlock.NoOfTickets,
				IsBlocked = scheduleBlock.IsBlocked,
				TicketList = scheduleBlock.TicketList.Select(CastFnsToSeTicket).ToList()

			}).ToList();
		}


		public static SeScheduleBlock CastFnsToSeScheduleBlock(IFnsSeScheduleBlock seScheduleBlock)
		{
			return new SeScheduleBlock
			{
				BlockID = seScheduleBlock.BlockID,
				Block = seScheduleBlock.Block,
				ZipCode = seScheduleBlock.ZipCode,
				MaxRadius = seScheduleBlock.MaxRadius,
				Distance = seScheduleBlock.Distance,
				AvailableSlots = seScheduleBlock.AvailableSlots,
				StartTime = seScheduleBlock.StartTime,
				EndTime = seScheduleBlock.EndTime,
				TechnicianId = seScheduleBlock.TechnicianId,
				IsTechConfirmed = seScheduleBlock.IsTechConfirmed,
				IsRed = seScheduleBlock.IsRed,
				Color = seScheduleBlock.Color,
				NoOfTickets = seScheduleBlock.NoOfTickets,
				IsBlocked = seScheduleBlock.IsBlocked,
				CurrentTicketId = seScheduleBlock.CurrentTicketId,
				BlockLatitude = seScheduleBlock.BlockLatitude,
				BlockLongitude = seScheduleBlock.BlockLongitude,
				TicketLatitude = seScheduleBlock.TicketLatitude,
				TicketLongitude = seScheduleBlock.TicketLongitude
			};
		}



		public static List<SeTechnicianAvailability> CastFnsToSeTechnicianAvailabilityList(List<IFnsSeTechnicianAvailability> seTechnicianAvailabilityList)
		{
			return seTechnicianAvailabilityList.Select(item => new SeTechnicianAvailability
			{
				TechnicianAvailabilityID = item.TechnicianAvailabilityID,
				TechnicianId = item.TechnicianId,
				StartDateTime = item.StartDateTime,
				EndDateTime = item.EndDateTime
			}).ToList();
		}


		public static List<SeScheduleTicket> CastFnsToSeScheduleTicketList(List<IFnsSeScheduleTicket> seScheduleTicketList)
		{
			return seScheduleTicketList.Select(item => new SeScheduleTicket
			{
				ScheduleTicketID = item.ScheduleTicketID,
				TicketId = item.TicketId,
				BlockId = item.BlockId,
				AppointmentDate = item.AppointmentDate,
				TravelTime = item.TravelTime
				//,AccountId = item.AccountId,
				//CustomerMasterFileId = item.CustomerMasterFileId,
				//MonitoringStationNo = item.MonitoringStationNo,
				//TicketTypeId = item.TicketTypeId,
				//TicketTypeName = item.TicketTypeName,
				//StatusCodeId = item.StatusCodeId,
				//StatusCode = item.StatusCode,
				//MoniConfirmation = item.MoniConfirmation,
				//TechConfirmation = item.TechConfirmation,
				//TechnicianId = item.TechnicianId,
				//TripCharges = item.TripCharges,
				//Appointment = item.Appointment,
				//AgentConfirmation = item.AgentConfirmation,
				//ExpirationDate = item.ExpirationDate,
				//Notes = item.Notes,
				//IsTechEnRoute = item.IsTechEnRoute,
				//IsTechDelayed = item.IsTechDelayed,
				//IsTechCompleted = item.IsTechCompleted,
				//CustomerFullName = item.CustomerFullName,
				//Address = item.Address,
				//CompleteAddress = item.CompleteAddress,
				//StreetAddress = item.StreetAddress,
				//CityStateZip = item.CityStateZip,
				//County = item.County,
				//State = item.State,
				//PostalCode = item.PostalCode,
				//Latitude = item.Latitude,
				//Longitude = item.Longitude,
				//PhoneHome = item.PhoneHome,
				//PhoneMobile = item.PhoneMobile,
				//AppointmentDate = item.AppointmentDate,
				//BlockId = item.BlockId,
				//TravelTime = item.TravelTime,
				//ZipCode = item.ZipCode,
				//MaxRadius = item.MaxRadius,
				//Distance = item.Distance,
				//StartTime = item.StartTime,
				//EndTime = item.EndTime,
				//ScheduleTicketId = item.ScheduleTicketId


			}).ToList();
		}


		public static SeScheduleTicket CastFnsToSeScheduleTicket(IFnsSeScheduleTicket seScheduleTicket)
		{
			return new SeScheduleTicket
			{
				ScheduleTicketID = seScheduleTicket.ScheduleTicketID,
				TicketId = seScheduleTicket.TicketId,
				BlockId = seScheduleTicket.BlockId,
				AppointmentDate = seScheduleTicket.AppointmentDate,
				TravelTime = seScheduleTicket.TravelTime

			};
		}
		public static SeZipCode CastFnsToSeZipCode(IFnsSeZipCode fnsSeZipCode)
		{
			return new SeZipCode
			{
				ZipCode = fnsSeZipCode.ZipCode,
				Latitude = fnsSeZipCode.Latitude,
				Longitude = fnsSeZipCode.Longitude,
				PrimaryCity = fnsSeZipCode.PrimaryCity,
				State = fnsSeZipCode.State

			};
		}


		*/
		#endregion ScheduleEngine


		public static MsAccountEquipment CastFnsToMsAccountEquipment(IFnsMsAccountEquipmentsView value)
		{
			return new MsAccountEquipment
			{
				AccountZoneAssignmentID = value.AccountZoneAssignmentID,
				AccountEquipmentID = value.AccountEquipmentID,
				AccountId = value.AccountId,
				EquipmentId = value.EquipmentId,
				ItemDesc = value.ItemDesc,
				Zone = value.Zone,
				//ZoneEventTypeId = value.ZoneEventTypeId,
				AccountZoneTypeId = value.AccountZoneTypeId,
				EquipmentLocationId = value.EquipmentLocationId,
				GPEmployeeId = value.GPEmployeeId,
				AccountEquipmentUpgradeTypeId = value.AccountEquipmentUpgradeTypeId,
				Price = value.Price,
				IsExistingWiring = value.IsExistingWiring,
				IsExisting = value.IsExisting,
				IsMainPanel = value.IsMainPanel,
			};
		}

		public static MsDispatchAgency CastFnsToMsDispatchAgenciesView(IFnsMsDispatchAgencyView value)
		{
			return new MsDispatchAgency
			{
				DispatchAgencyID = value.DispatchAgencyID,
				DispatchAgencyTypeId = value.DispatchAgencyTypeId,
				MonitoringStationOSId = value.MonitoringStationOSId,
				DispatchAgencyOsId = value.DispatchAgencyOsId,
				DispatchAgencyName = value.DispatchAgencyName,
				MsAgencyNumber = value.MsAgencyNumber,
				Address1 = value.Address1,
				Address2 = value.Address2,
				City = value.City,
				State = value.State,
				ZipCode = value.ZipCode,
				Phone1 = value.Phone1,
				Phone2 = value.Phone2,
				DispatchAgencyType = value.DispatchAgencyType
			};
		}

		public static MsDispatchAgencyType CastFnsToMsDispatchAgencyType(IFnsMsDispatchAgencyType value)
		{
			return new MsDispatchAgencyType
			{
				DispatchAgencyTypeID = value.DispatchAgencyTypeID,
				MonitoringStationsOSId = value.MonitoringStationOSId,
				DispatchAgencyType = value.DispatchAgencyType,
				MsAgencyTypeNo = value.MsAgencyTypeNo
			};
		}

		public static McAddressView CastFnsToMcAddressView(IFnsMcAddressView fmsMcPremiseAddress)
		{
			return new McAddressView
			{
				AddressID = fmsMcPremiseAddress.AddressID,
				DealerId = fmsMcPremiseAddress.DealerId,
				CountryId = fmsMcPremiseAddress.CountryId,
				CountryName = fmsMcPremiseAddress.CountryName,
				TimeZoneId = fmsMcPremiseAddress.TimeZoneId,
				TimeZoneAB = fmsMcPremiseAddress.TimeZoneAB,
				TimeZoneName = fmsMcPremiseAddress.TimeZoneName,
				StreetAddress = fmsMcPremiseAddress.StreetAddress,
				StreetAddress2 = fmsMcPremiseAddress.StreetAddress2,
				City = fmsMcPremiseAddress.City,
				StateId = fmsMcPremiseAddress.StateId,
				StateAB = fmsMcPremiseAddress.StateAB,
				PostalCode = fmsMcPremiseAddress.PostalCode,
				PlusFour = fmsMcPremiseAddress.PlusFour,
				County = fmsMcPremiseAddress.County,
				Phone = fmsMcPremiseAddress.Phone,
				Latitude = fmsMcPremiseAddress.Latitude,
				Longitude = fmsMcPremiseAddress.Longitude,
				CrossStreet = fmsMcPremiseAddress.CrossStreet,

			};
		}

		public static MsAccountDispatchAgencyAssignmentView CastFnsToMsAccountDispatchAgencyAssignmentView(
			IFnsMsAccountDispatchAgencyAssignmentView value)
		{
			return new MsAccountDispatchAgencyAssignmentView
			{
				DispatchAgencyAssignmentID = value.DispatchAgencyAssignmentID,
				DispatchAgencyId = value.DispatchAgencyId,
				DispatchAgencyTypeId = value.DispatchAgencyTypeId,
				DispatchAgencyTypeName = value.DispatchAgencyTypeName,
				AccountId = value.AccountId,
				IndustryAccountID = value.IndustryAccountID,
				CsNo = value.CsNo,
				DispatchAgencyName = value.DispatchAgencyName,
				Phone1 = value.Phone1,
				PermitNumber = value.PermitNumber,
				PermitEffectiveDate = value.PermitEffectiveDate,
				PermitExpireDate = value.PermitExpireDate,
				IsVerified = value.IsVerified,
				IsActive = value.IsActive
			};
		}

		public static FnsMsAccountDispatchAgencyAssignmentView CastMsToFnsMsAccountDispatchAgencyAssignmentView(
			MsAccountDispatchAgencyAssignmentView value)
		{
			return new FnsMsAccountDispatchAgencyAssignmentView
			{
				DispatchAgencyAssignmentID = value.DispatchAgencyAssignmentID,
				DispatchAgencyId = value.DispatchAgencyId,
				DispatchAgencyTypeId = value.DispatchAgencyTypeId,
				DispatchAgencyTypeName = value.DispatchAgencyTypeName,
				AccountId = value.AccountId,
				IndustryAccountID = value.IndustryAccountID,
				CsNo = value.CsNo,
				DispatchAgencyName = value.DispatchAgencyName,
				Phone1 = value.Phone1,
				CityName = value.CityName,
				StateAB = value.StateAB,
				ZipCode = value.ZipCode,
				PermitNumber = value.PermitNumber,
				PermitEffectiveDate = value.PermitEffectiveDate,
				PermitExpireDate = value.PermitExpireDate,
				IsVerified = value.IsVerified,
				IsActive = value.IsActive
			};
		}

		#region Reporting Services

		public static MsAccountOnlineStatusInfo CastFnsToMsAccountOnlineStatusInfo(IFnsMsAccountOnlineStatusInfo value)
		{
			return new MsAccountOnlineStatusInfo
			{
				KeyName = value.KeyName,
				Value = value.Value,
				Status = value.Status
			};
		}

		public static MsAccountCreditsAndInstalls CastFnsToMsAccountCreditsAndInstalls(IFnsMsAccountCreditsAndInstalls value)
		{
			return new MsAccountCreditsAndInstalls
			{
				LeadID = value.LeadID,
				OfficeId = value.OfficeId,
				OfficeName = value.OfficeName,
				TeamID = value.TeamID,
				TeamName = value.TeamName,
				SalesRepID = value.SalesRepID,
				Active = value.Active,
				NumInstalls = value.NumInstalls,
				NumCredits = value.NumCredits,
				InstallDate = value.InstallDate,
				CreditDate = value.CreditDate
			};
		}

		#endregion Reporting Services

		#region Funding Services

		public static FeCriteria CastFnsToFeCriteria(IFnsFeCriteria value)
		{
			return new FeCriteria
			{
				CriteriaID = value.CriteriaID,
				PurchaserId = value.PurchaserId,
				PurchaserName = value.PurchaserName,
				CriteriaName = value.CriteriaName,
				Description = value.Description,
				FilterString = value.FilterString,
				CreatedBy = value.CreatedBy,
				CreatedOn = value.CreatedOn
			};
		}

		public static FePacket CastFnsToFePacket(IFnsFePacketView item)
		{
			return new FePacket
			{
				PacketID = item.PacketID,
				CriteriaName = item.CriteriaName,
				CriteriaId = item.CriteriaId,
				PurchaserID = item.PurchaserID,
				PurchaserName = item.PurchaserName,
				SubmittedOn = item.SubmittedOn,
				SubmittedBy = item.SubmittedBy,
				IsDeleted = item.IsDeleted,
				CreatedOn = item.CreatedOn,
				CreatedBy = item.CreatedBy
			};
		}

		#endregion Funding Services

		public static FePacketItem CastFnsToFePacketItem(IFnsFePacketItemView viewItem)
		{
			return new FePacketItem
			{
				PacketItemID = viewItem.PacketItemID,
				PacketId = viewItem.PacketId,
				CustomerNumber = viewItem.CustomerNumber,
				CustomerId = viewItem.CustomerId,
				AccountId = viewItem.AccountId,
				Csid = viewItem.Csid,
				FirstName = viewItem.FirstName,
				LastName = viewItem.LastName,
				ReturnAccountFundingStatusId = viewItem.ReturnAccountFundingStatusId,
				AccountFundingShortDesc = viewItem.AccountFundingShortDesc,
				AccountStatusNote = viewItem.AccountStatusNote,
				TransactionID = viewItem.TransactionID,
				ReportGuid = viewItem.ReportGuid,
				Bureau = viewItem.Bureau,
				Gateway = viewItem.Gateway,
				ModifiedBy = viewItem.ModifiedBy,
				ModifiedOn = viewItem.ModifiedOn,
				CreatedBy = viewItem.CreatedBy,
				CreatedOn = viewItem.CreatedOn
			};
		}

		public static FeBundle CastFnsToFeBundle(IFnsFeBundle viewItem)
		{
			return new FeBundle
			{
				BundleID = viewItem.BundleID,
				PurchaserID = viewItem.PurchaserID,
				PurchaserName = viewItem.PurchaserName,
				TrackingNumberID = viewItem.TrackingNumberID,
				TrackingNumber = viewItem.TrackingNumber,
				DeliveryDate = viewItem.DeliveryDate,
				SubmittedOn = viewItem.SubmittedOn,
				SubmittedBy = viewItem.SubmittedBy,
				CreatedOn = viewItem.CreatedOn,
				CreatedBy = viewItem.CreatedBy
			};
		}

		public static FeBundleItems CastFnsToFeBundleItems(IFnsFeBundleItem viewItem)
		{
			return new FeBundleItems
			{
				BundleItemID = viewItem.BundleItemID,
				BundleId = viewItem.BundleId,
				PacketId = viewItem.PacketId,
				CreatedOn = viewItem.CreatedOn,
				CreatedBy = viewItem.CreatedBy,
				PSubmittedOn = viewItem.PSubmittedOn,
				PSubmittedBy = viewItem.PSubmittedBy,
				PCreatedOn = viewItem.PCreatedOn,
				PCreatedBy = viewItem.CreatedBy
			};
		}

		public static LmSalesRepRequirement CastFnsToLmSalesRepRequirement(IFnsLmSalesRepRequirementsView licItem)
		{
			return new LmSalesRepRequirement
			{
				RequirementID = licItem.RequirementID,
				RequirementTypeName = licItem.RequirementTypeName,
				LocationTypeName = licItem.LocationTypeName,
				RequirementName = licItem.RequirementName,
				LockID = licItem.LockID,
				LockTypeName = licItem.LockTypeName,
				CallCenterMessage = licItem.CallCenterMessage,
				Status = licItem.Status,
				LicenseID = licItem.LicenseID
			};
		}
	}
}