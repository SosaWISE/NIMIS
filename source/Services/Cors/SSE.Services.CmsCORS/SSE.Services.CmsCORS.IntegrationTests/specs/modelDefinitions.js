/** modelDefinitions.js */
module.exports.salesRepAndTechInfo = function(repInfo) {
  expect(repInfo.UserID).toBeDefined("UserID must be defined.");
  expect(repInfo.UserID).not.toBeNull("UserID can not be null.");
  expect(repInfo.CompanyID).toBeDefined("CompanyID must be defined.");
  expect(repInfo.CompanyID).not.toBeNull("CompanyID can not be null.");
  expect(repInfo.TeamLocationId).toBeDefined("TeamLocationId must be defined.");
  expect(repInfo.TeamLocationId).not.toBeNull("TeamLocationId can not be null.");
  expect(repInfo.DefaultOfficeName).toBeDefined("DefaultOfficeName must be defined.");
  expect(repInfo.DefaultOfficeName).not.toBeNull("DefaultOfficeName can not be null.");
  expect(repInfo.FirstName).toBeDefined("FirstName must be defined.");
  expect(repInfo.FirstName).not.toBeNull("FirstName can not be null.");
  expect(repInfo.LastName).toBeDefined("LastName must be defined.");
  expect(repInfo.LastName).not.toBeNull("LastName can not be null.");
  expect(repInfo.CompanyName).toBeDefined("CompanyName must be defined.");
  expect(repInfo.CompanyName).not.toBeNull("CompanyName can not be null.");
  expect(repInfo.UserName).toBeDefined("UserName must be defined.");
  expect(repInfo.UserName).not.toBeNull("UserName can not be null.");
  expect(repInfo.BirthDate).toBeDefined("BirthDate must be defined.");
  expect(repInfo.BirthDate).not.toBeNull("BirthDate can not be null.");
  expect(repInfo.HomeTown).toBeDefined("HomeTown must be defined.");
  expect(repInfo.HomeTown).not.toBeNull("HomeTown can not be null.");
  expect(repInfo.Sex).toBeDefined("Sex must be defined.");
  expect(repInfo.Sex).not.toBeNull("Sex can not be null.");
  expect(repInfo.ShirtSize).toBeDefined("ShirtSize must be defined.");
  expect(repInfo.ShirtSize).not.toBeNull("ShirtSize can not be null.");
  expect(repInfo.HatSize).toBeDefined("HatSize must be defined.");
  expect(repInfo.HatSize).not.toBeNull("HatSize can not be null.");
  expect(repInfo.PhoneHome).toBeDefined("PhoneHome must be defined.");
  expect(repInfo.PhoneHome).not.toBeNull("PhoneHome can not be null.");
  expect(repInfo.PhoneCell).toBeDefined("PhoneCell must be defined.");
  expect(repInfo.PhoneCell).not.toBeNull("PhoneCell can not be null.");
  expect(repInfo.Email).toBeDefined("Email must be defined.");
  expect(repInfo.Email).not.toBeNull("Email can not be null.");
  expect(repInfo.SSN).toBeDefined("SSN must be defined.");
  expect(repInfo.SSN).not.toBeNull("SSN can not be null.");
  expect(repInfo.ImagePath).toBeDefined("ImagePath must be defined.");
  expect(repInfo.ImagePath).not.toBeNull("ImagePath can not be null.");
  expect(repInfo.Seasons).toBeDefined("Seasons must be defined.");
  expect(repInfo.Seasons.length).toBeDefined("Seasons is an array of seasons so it should be defined even if there are no seasons.");
};

module.exports.lead = function(leadData) {
  expect(leadData.CustomerMasterFileId).toBeDefined("CustomerMasterFileId must be defined.");
  expect(leadData.CustomerMasterFileId).not.toBeNull("CustomerMasterFileId can not be null.");

  expect(leadData.LeadId).toBeDefined("LeadID must be defined.");
  expect(leadData.LeadId).not.toBeNull("LeadID can not be null.");

  expect(leadData.AccountId).toBeDefined("AccountId must be defined.");

  expect(leadData.BureauId).toBeDefined("BureauId must be defined.");
  expect(leadData.BureauId).not.toBeNull("BureauId can not be null.");

  expect(leadData.SeasonId).toBeDefined("SeasonId must be defined.");
  expect(leadData.SeasonId).not.toBeNull("SeasonId can not be null.");

  expect(leadData.CreditReportVendorid).toBeDefined("CreditReportVendorid must be defined.");
  expect(leadData.CreditReportVendorid).not.toBeNull("CreditReportVendorid can not be null.");

  expect(leadData.CreditReportVendorAbaraId).toBeDefined("CreditReportVendorAbaraId must be defined.");

  expect(leadData.CreditReportVendorMicrobiltId).toBeDefined("CreditReportVendorMicrobiltId must be defined.");

  expect(leadData.CreditVendorEasyAccessId).toBeDefined("CreditVendorEasyAccessId must be defined.");

  expect(leadData.CreditReportVendorManualId).toBeDefined("CreditReportVendorManualId must be defined.");

  expect(leadData.Score).toBeDefined("Score must be defined.");
  expect(leadData.Score).not.toBeNull("Score can not be null.");

  expect(leadData.IsScored).toBeDefined("IsScored must be defined.");
  expect(leadData.IsScored).not.toBeNull("IsScored can not be null.");

  expect(leadData.IsHit).toBeDefined("IsHit must be defined.");
  expect(leadData.IsHit).not.toBeNull("IsHit can not be null.");

  expect(leadData.CreditGroup).toBeDefined("CreditGroup must be defined.");
  expect(leadData.CreditGroup).not.toBeNull("CreditGroup can not be null.");
};

module.exports.localization = function(localData) {
  expect(localData.LocalizationID).toBeDefined("LocalizationID must be defined.");
  expect(localData.LocalizationID).not.toBeNull("LocalizationID can not be null.");

  expect(localData.MSLocalId).toBeDefined("MSLocalId must be defined.");
  expect(localData.MSLocalId).not.toBeNull("MSLocalId can not be null.");

  expect(localData.LocalizationName).toBeDefined("LocalizationName must be defined.");
  expect(localData.LocalizationName).not.toBeNull("LocalizationName can not be null.");
};

module.exports.address = function(addrData) {
  expect(addrData.AddressID).toBeDefined("AddressID must be defined.");
  expect(addrData.AddressID).not.toBeNull("AddressID can not be null.");

  expect(addrData.DealerId).toBeDefined("DealerId must be defined.");
  expect(addrData.DealerId).not.toBeNull("DealerId can not be null.");
  expect(addrData.DealerId).toBeGreaterThan(0, "DealerId must have a value greater than 0.");

  expect(addrData.TimeZoneId).toBeDefined("TimeZoneId must be defined.");
  expect(addrData.TimeZoneId).not.toBeNull("TimeZoneId can not be null.");
  expect(addrData.TimeZoneId).toBeGreaterThan(0, "TimeZoneId must have a value greater than 0.");

  expect(addrData.StreetAddress).toBeDefined("StreetAddress must be defined.");
  expect(addrData.StreetAddress).not.toBeNull("StreetAddress can not be null.");

  expect(addrData.StreetAddress2).toBeDefined("StreetAddress2 must be defined.");

  expect(addrData.City).toBeDefined("City must be defined.");
  expect(addrData.City).not.toBeNull("City can not be null.");

  expect(addrData.StateId).toBeDefined("StateId must be defined.");
  expect(addrData.StateId).not.toBeNull("StateId can not be null.");

  expect(addrData.PostalCode).toBeDefined("PostalCode must be defined.");
  expect(addrData.PostalCode).not.toBeNull("PostalCode can not be null.");

  expect(addrData.PlusFour).toBeDefined("PlusFour must be defined.");

  expect(addrData.County).toBeDefined("County must be defined.");
  expect(addrData.County).not.toBeNull("County can not be null.");

  expect(addrData.PhoneNumber).toBeDefined("PhoneNumber must be defined.");
  expect(addrData.PhoneNumber).not.toBeNull("PhoneNumber can not be null.");

  expect(addrData.Latitude).toBeDefined("Latitude must be defined.");

  expect(addrData.Longitude).toBeDefined("Longitude must be defined.");

  expect(addrData.Validated).toBeDefined("Validated must be defined.");

  expect(addrData.SalesRepId).toBeDefined("SalesRepId must be defined.");
  expect(addrData.SalesRepId).not.toBeNull("SalesRepId can not be null.");

  expect(addrData.SeasonId).toBeDefined("SeasonId must be defined.");
  expect(addrData.SeasonId).not.toBeNull("SeasonId can not be null.");

  expect(addrData.TeamLocationId).toBeDefined("TeamLocationId must be defined.");
  expect(addrData.TeamLocationId).not.toBeNull("TeamLocationId can not be null.");
};

module.exports.msAccountLeadInfos = function(data) {
  expect(data.AccountID).toBeDefined("AccountID must be defined.");
  expect(data.AccountID).not.toBeNull("AccountID can not be null.");

  expect(data.LeadId).toBeDefined("LeadId must be defined.");
  expect(data.LeadId).not.toBeNull("LeadId can not be null.");

  expect(data.CustomerId).toBeDefined("CustomerId must be defined.");
  expect(data.CustomerId).not.toBeNull("CustomerId can not be null.");

  expect(data.CustomerMasterFileId).toBeDefined("CustomerMasterFileId must be defined.");
  expect(data.CustomerMasterFileId).not.toBeNull("CustomerMasterFileId can not be null.");

  expect(data.IndustryAccountId).toBeDefined("IndustryAccountId must be defined.");

  expect(data.SystemTypeId).toBeDefined("SystemTypeId must be defined.");

  expect(data.CellularTypeId).toBeDefined("CellularTypeId must be defined.");

  expect(data.PanelTypeId).toBeDefined("PanelTypeId must be defined.");

  expect(data.PanelItemId).toBeDefined("PanelItemId must be defined.");

  expect(data.CellPackageItemId).toBeDefined("CellPackageItemId must be defined.");

  expect(data.ContractTemplateId).toBeDefined("ContractTemplateId must be defined.");
};

module.exports.msEmergencyContact = function(emcData) {
  expect(emcData.EmergencyContactID).toBeGreaterThan(0, "The EmergencyContactID must be returned.");
  expect(emcData.CustomerId).toBeDefined("The CustomerId must be defined.");
  expect(emcData.AccountId).toBeGreaterThan(0, "The AccountId must be returned.");
  expect(emcData.RelationshipId).toBeGreaterThan(0, "The RelationshipId must be returned.");
  expect(emcData.OrderNumber).toBeGreaterThan(0, "The OrderNumber must be returned.");
  expect(emcData.Allergies).toBeDefined("This can be null");
  expect(emcData.MedicalConditions).toBeDefined("This can be null");
  expect(emcData.HasKey).not.toBeNull("This is a boolean field.");
  expect(emcData.DOB).toBeDefined("This can be null.");
  expect(emcData.Prefix).toBeDefined("This can be null.");
  expect(emcData.FirstName).not.toBeNull("This is a required field");
  expect(emcData.MiddleName).toBeDefined("This can be null.");
  expect(emcData.LastName).not.toBeNull("This is a required field");
  expect(emcData.Postfix).toBeDefined("This can be null.");
  expect(emcData.Email).toBeDefined("This can be null.");
  expect(emcData.Password).toBeDefined("This can be null.");
  expect(emcData.Phone1).not.toBeNull("This is a required field");
  expect(emcData.Phone1TypeId).not.toBeNull("This is a required field");
  expect(emcData.Phone2).toBeDefined("This can be null.");
  expect(emcData.Phone2TypeId).toBeDefined("This can be null.");
  expect(emcData.Phone3).toBeDefined("This can be null.");
  expect(emcData.Phone3TypeId).toBeDefined("This can be null.");
  expect(emcData.Comment1).toBeDefined("This can be null.");
};

module.exports.msEmergencyContactPhoneType = function(phoneType) {
  expect(phoneType.PhoneTypeID).toBeDefined("PhoneTypeID must be defined.");
  expect(phoneType.PhoneTypeID).not.toBeNull("PhoneTypeID can not be null.");

  expect(phoneType.MonitoringStationOSId).toBeDefined("MonitoringStationOSId must be defined.");
  expect(phoneType.MonitoringStationOSId).not.toBeNull("MonitoringStationOSId can not be null.");

  expect(phoneType.MsPhoneTypeId).toBeDefined("MsPhoneTypeId must be defined.");
  expect(phoneType.MsPhoneTypeId).not.toBeNull("MsPhoneTypeId can not be null.");

  expect(phoneType.PhoneTypeDescription).toBeDefined("PhoneTypeDescription must be defined.");
  expect(phoneType.PhoneTypeDescription).not.toBeNull("PhoneTypeDescription can not be null.");
};

module.exports.msEmergencyContactRelationship = function(relationship) {
  expect(relationship.RelationshipID).toBeDefined("RelationshipID must be defined.");
  expect(relationship.RelationshipID).not.toBeNull("RelationshipID can not be null.");

  expect(relationship.MonitoringStationOSId).toBeDefined("MonitoringStationOSId must be defined.");
  expect(relationship.MonitoringStationOSId).not.toBeNull("MonitoringStationOSId can not be null.");

  expect(relationship.MsRelationshipId).toBeDefined("MsRelationshipId must be defined.");
  expect(relationship.MsRelationshipId).not.toBeNull("MsRelationshipId can not be null.");

  expect(relationship.RelationshipDescription).toBeDefined("RelationshipDescription must be defined.");
  expect(relationship.RelationshipDescription).not.toBeNull("RelationshipDescription can not be null.");

  expect(relationship.IsEVC).toBeDefined("IsEVC must be defined.");
  expect(relationship.IsEVC).not.toBeNull("IsEVC can not be null.");

  expect(relationship.IsActive).toBeDefined("IsActive must be defined.");
  expect(relationship.IsActive).not.toBeNull("IsActive can not be null.");

  expect(relationship.IsDeleted).toBeDefined("IsDeleted must be defined.");
  expect(relationship.IsDeleted).not.toBeNull("IsDeleted can not be null.");

  expect(relationship.CreatedOn).toBeDefined("CreatedOn must be defined.");
  expect(relationship.CreatedOn).not.toBeNull("CreatedOn can not be null.");

  expect(relationship.CreatedBy).toBeDefined("CreatedBy must be defined.");
  expect(relationship.CreatedBy).not.toBeNull("CreatedBy can not be null.");

  expect(relationship.ModifiedOn).toBeDefined("ModifiedOn must be defined.");
  expect(relationship.ModifiedOn).not.toBeNull("ModifiedOn can not be null.");

  expect(relationship.ModifiedBy).toBeDefined("ModifiedBy must be defined.");
  expect(relationship.ModifiedBy).not.toBeNull("ModifiedBy can not be null.");
};

module.exports.aeInvoice = function(invModule, invoiceId, conditionalArgs) {
  expect(invModule.Header).not.toBeNull("Header was not returned.");
  expect(invModule.Header.InvoiceID).toBe(invoiceId, "The invoice id returned did not match the passed invoiceID.");
  expect(invModule.Items).not.toBeNull("There are no invoice items associated with this invoice.");
  expect(invModule.Items.length).toBeGreaterThan(0, "The invoice items list should at least have one item.");
  // console.log("Items: ", invModule.Items);

  /** Check items returned. */
  if (invModule.Items.length > 0) {
    var item = invModule.Items[0];
    // console.log("AeInvoice Items: ", item);
    expect(item.InvoiceItemID).not.toBeNull("Should not be null");
    expect(item.InvoiceId).toBe(invoiceId, "Invoice ID should match the item");
    expect(item.ItemId).not.toBeNull("Should not be null.");
    expect(item.ItemSKU).not.toBeNull("The ItemSKU should not be null.");
    expect(item.ItemDesc).not.toBeNull("The ItemDesc should not be null.");
    expect(item.TaxOptionId).toBe("TAX");
    expect(item.Qty).toBe(1);
    expect(item.Cost).toBeGreaterThan(0);
    expect(item.RetailPrice).toBeGreaterThan(0);
    expect(item.SalesmanID).toBeDefined("SalesmanID should be defined in the object.");
    expect(item.TechnicianID).toBeDefined("TechnicianID should be defined in the object.");
    if (conditionalArgs && conditionalArgs.salesmanIdIsRequired) {
      expect(item.SalesmanID).toBe(conditionalArgs.salesmanId, "TechnicianID should have been passed as " + conditionalArgs.salesmanId);
    }
    if (conditionalArgs && conditionalArgs.salesmanIdIsRequired === false) {
      expect(item.SalesmanID).toBeNull("This should be null since it is not passed on Create Invoice Item call.");
    }
    if (conditionalArgs && conditionalArgs.technicianIdIsRequired) {
      expect(item.TechnicianID).toBe(conditionalArgs.technicianId, "TechnicianID should have been passed as " + conditionalArgs.technicianId);
    }
    if (conditionalArgs && conditionalArgs.technicianIdIsRequired === false) {
      expect(item.TechnicianID).toBeNull("This should be null since it is not passed on Create Invoice Item call.");
    }
  }
};

module.exports.aeInvoiceItem = function(invItem, invoiceId, itemId, itemSku, qty) {
  expect(invItem.InvoiceItemID).toBeGreaterThan(0, "The InvoiceItemID should be greater than zero.");
  expect(invItem.InvoiceId).toBe(invoiceId, "The InvoiceID's should match.");
  if (itemId) {
    expect(invItem.ItemId).toBe(itemId, "The ItemId should be " + itemId + ".");
  }
  expect(invItem.ItemSKU).not.toBeNull("The ItemSKU should not be null.");
  if (itemSku) {
    expect(invItem.ItemSKU).toBe(itemSku, "The ItemSku should be " + itemSku + ".");
  }
  expect(invItem.ItemDesc).not.toBeNull("The ItemDesc should not be null.");
  expect(invItem.TaxOptionId).toBe("TAX", "The TaxOptionId should be , in this case, 'TAX'.");
  expect(invItem.Qty).toBe(qty, "The Qty field should have returned 1.");
  expect(invItem.Cost).toBeGreaterThan(0, "The Cost field should greater than zero.");
  expect(invItem.RetailPrice).toBeGreaterThan(0, "The RetailPrice field should greater than zero.");
  expect(invItem.PriceWithTax).toBeGreaterThan(0, "The PriceWithTax field should greater than zero.");
};

module.exports.serviceType = function(item) {
  expect(item.SystemTypeID).toBeDefined("SystemTypeID has to be defined.");
  expect(item.SystemTypeID).not.toBeNull("SystemTypeID can not be null.");

  expect(item.SystemTypeName).toBeDefined("SystemTypeName has to be defined.");
  expect(item.SystemTypeName).not.toBeNull("SystemTypeName can not be null.");
};

module.exports.panelType = function(item) {
  expect(item.PanelTypeID).toBeDefined("PanelTypeID has to be defined.");
  expect(item.PanelTypeID).not.toBeNull("PanelTypeID can not be null.");

  expect(item.PanelTypeName).toBeDefined("PanelTypeName has to be defined.");
  expect(item.PanelTypeName).not.toBeNull("PanelTypeName can not be null.");
};

module.exports.dslSeizureType = function(item) {
  expect(item.DslSeizureID).toBeDefined("DslSeizureID has to be defined.");
  expect(item.DslSeizureID).not.toBeNull("DslSeizureID can not be null.");

  expect(item.DslSeizure).toBeDefined("DslSeizure has to be defined.");
  expect(item.DslSeizure).not.toBeNull("DslSeizure can not be null.");
};

module.exports.zoneEventType = function(item) {
  expect(item.ZoneEventTypeID).toBeDefined("ZoneEventTypeID has to be defined.");
  expect(item.ZoneEventTypeID).not.toBeNull("ZoneEventTypeID can not be null.");

  expect(item.MonitoringStationOSID).toBeDefined("MonitoringStationOSID has to be defined.");
  expect(item.MonitoringStationOSID).not.toBeNull("MonitoringStationOSID can not be null.");

  expect(item.Descrption).toBeDefined("Descrption has to be defined.");
  expect(item.Descrption).not.toBeNull("Descrption can not be null.");
};

module.exports.equipmentLocation = function(item) {
  expect(item.EquipmentLocaitonID).toBeDefined("EquipmentLocaitonID has to be defined.");
  expect(item.EquipmentLocaitonID).not.toBeNull("EquipmentLocaitonID can not be null.");

  expect(item.EquipmentLocationDesc).toBeDefined("EquipmentLocationDesc has to be defined.");
  expect(item.EquipmentLocationDesc).not.toBeNull("EquipmentLocationDesc can not be null.");

  expect(item.MonitronicsCode).toBeDefined("MonitronicsCode has to be defined.");

  expect(item.CriticomCode).toBeDefined("CriticomCode has to be defined.");

  expect(item.AvantGuardCode).toBeDefined("AvantGuardCode has to be defined.");

  expect(item.LocationCode).toBeDefined("LocationCode has to be defined.");
};

module.exports.accountZoneType = function(item) {
  expect(item.AccountZoneTypeID).toBeDefined("AccountZoneTypeID has to be defined.");
  expect(item.AccountZoneTypeID).not.toBeNull("AccountZoneTypeID can not be null.");

  expect(item.AccountZoneType).toBeDefined("AccountZoneType has to be defined.");
  expect(item.AccountZoneType).not.toBeNull("AccountZoneType can not be null.");
};

module.exports.industryAccountNumber = function(item) {
  expect(item.IndustryAccountID).toBeDefined("IndustryAccountID has to be defined.");
  expect(item.IndustryAccountID).not.toBeNull("IndustryAccountID can not be null.");

  expect(item.AccountId).toBeDefined("AccountId has to be defined.");
  expect(item.AccountId).not.toBeNull("AccountId can not be null.");

  expect(item.ReceiverLineId).toBeDefined("ReceiverLineId has to be defined.");
  expect(item.ReceiverLineId).not.toBeNull("ReceiverLineId can not be null.");

  expect(item.ReceiverLineBlockId).toBeDefined("ReceiverLineBlockId has to be defined.");
  expect(item.ReceiverLineBlockId).not.toBeNull("ReceiverLineBlockId can not be null.");

  expect(item.IndustryAccount).toBeDefined("IndustryAccount has to be defined.");
  expect(item.IndustryAccount).not.toBeNull("IndustryAccount can not be null.");

  expect(item.Designator).toBeDefined("Designator has to be defined.");
  expect(item.Designator).not.toBeNull("Designator can not be null.");

  expect(item.SubscriberNumber).toBeDefined("SubscriberNumber has to be defined.");
  expect(item.SubscriberNumber).not.toBeNull("SubscriberNumber can not be null.");

  expect(item.ReceiverNumber).toBeDefined("ReceiverNumber has to be defined.");
  expect(item.ReceiverNumber).not.toBeNull("ReceiverNumber can not be null.");
};

module.exports.msAccountEquipment = function(item) {
  expect(item.AccountZoneAssignmentID).toBeDefined("AccountZoneAssignmentID has to be defined.");
  expect(item.AccountZoneAssignmentID).not.toBeNull("AccountZoneAssignmentID can not be null.");

  expect(item.AccountEquipmentID).toBeDefined("AccountEquipmentID has to be defined.");
  expect(item.AccountEquipmentID).not.toBeNull("AccountEquipmentID can not be null.");

  expect(item.AccountId).toBeDefined("AccountId has to be defined.");
  expect(item.AccountId).not.toBeNull("AccountId can not be null.");

  expect(item.ItemId).toBeDefined("ItemId has to be defined.");
  expect(item.ItemId).not.toBeNull("ItemId can not be null.");

  expect(item.ItemDesc).toBeDefined("ItemDesc has to be defined.");
  expect(item.ItemDesc).not.toBeNull("ItemDesc can not be null.");

  expect(item.Zone).toBeDefined("Zone has to be defined.");
  expect(item.Zone).not.toBeNull("Zone can not be null.");

  expect(item.AccountZoneTypeId).toBeDefined("AccountZoneTypeId has to be defined.");
  expect(item.AccountZoneTypeId).not.toBeNull("AccountZoneTypeId can not be null.");

  expect(item.EquipmentLocationId).toBeDefined("EquipmentLocationId has to be defined.");

  expect(item.GPEmployeeId).toBeDefined("GPEmployeeId has to be defined.");
  expect(item.GPEmployeeId).not.toBeNull("GPEmployeeId can not be null.");

  expect(item.AccountEquipmentUpgradeTypeId).toBeDefined("AccountEquipmentUpgradeTypeId has to be defined.");
  expect(item.AccountEquipmentUpgradeTypeId).not.toBeNull("AccountEquipmentUpgradeTypeId can not be null.");

  expect(item.Price).toBeDefined("Price has to be defined.");
  expect(item.Price).not.toBeNull("Price can not be null.");

  expect(item.IsExistingWiring).toBeDefined("IsExistingWiring has to be defined.");
  expect(item.IsExistingWiring).not.toBeNull("IsExistingWiring can not be null.");

  expect(item.IsExisting).toBeDefined("IsExisting has to be defined.");
  expect(item.IsExisting).not.toBeNull("IsExisting can not be null.");

  expect(item.IsMainPanel).toBeDefined("IsMainPanel has to be defined.");
  expect(item.IsMainPanel).not.toBeNull("IsMainPanel can not be null.");
};

module.exports.msAccountSystemDetails = function(item) {
  expect(item.AccountID).toBeDefined("AccountID has to be defined.");
  expect(item.AccountID).not.toBeNull("AccountID can not be null.");

  expect(item.SystemTypeId).toBeDefined("SystemTypeId has to be defined.");
  expect(item.CellularTypeId).toBeDefined("CellularTypeId has to be defined.");
  expect(item.PanelTypeId).toBeDefined("PanelTypeId has to be defined.");
  expect(item.AccountPassword).toBeDefined("AccountPassword has to be defined.");
  expect(item.DslSeizureId).toBeDefined("DslSeizureId has to be defined.");
};

module.exports.aeCustomerGeneralSearchDetails = function(item) {
  expect(item.CustomerMasterFileID).toBeDefined("CustomerMasterFileID has to be defined.");
  expect(item.CustomerMasterFileID).not.toBeNull("CustomerMasterFileID can not be null.");

  expect(item.AccountTypes).toBeDefined("AccountTypes has to be defined.");
  expect(item.AccountTypes).not.toBeNull("AccountTypes can not be null.");

  /** Loop through the account types. */
  item.AccountTypes.map(function(accountType) {
    expect(accountType).toBeDefined("accountType has to be defined.");
    expect(accountType).not.toBeNull("accountType can not be null.");
  });

  expect(item.Fullname).toBeDefined("Fullname has to be defined.");
  expect(item.Fullname).not.toBeNull("Fullname can not be null.");

  expect(item.City).toBeDefined("City has to be defined.");
  expect(item.City).not.toBeNull("City can not be null.");

  expect(item.Phone).toBeDefined("Phone has to be defined.");
  expect(item.Phone).not.toBeNull("Phone can not be null.");

  expect(item.Email).toBeDefined("Email has to be defined.");
};

module.exports.msCellularTypes = function(item) {
  expect(item.CellularTypeID).toBeDefined("CellularTypeID has to be defined.");
  expect(item.CellularTypeID).not.toBeNull("CellularTypeID can not be null.");

  expect(item.CellularTypeName).toBeDefined("CellularTypeName has to be defined.");
  expect(item.CellularTypeName).not.toBeNull("CellularTypeName can not be null.");
};

module.exports.msPanelTypes = function(item) {
  expect(item.PanelTypeID).toBeDefined("PanelTypeID has to be defined.");
  expect(item.PanelTypeID).not.toBeNull("PanelTypeID can not be null.");

  expect(item.PanelTypeName).toBeDefined("PanelTypeName has to be defined.");
  expect(item.PanelTypeName).not.toBeNull("PanelTypeName can not be null.");

  expect(item.UIName).toBeDefined("UIName has to be defined.");
  expect(item.UIName).not.toBeNull("UIName can not be null.");
};

module.exports.aeCustomerCardInfo = function(item) {
  expect(item.CustomerID).toBeDefined("CustomerID has to be defined.");
  expect(item.CustomerID).not.toBeNull("CustomerID can not be null.");

  expect(item.ResultType).toBeDefined("ResultType has to be defined.");
  expect(item.ResultType).not.toBeNull("ResultType can not be null.");

  expect(item.CustomerMasterFileID).toBeDefined("CustomerMasterFileID has to be defined.");
  expect(item.CustomerMasterFileID).not.toBeNull("CustomerMasterFileID can not be null.");

  expect(item.Prefix).toBeDefined("Prefix has to be defined.");

  expect(item.FirstName).toBeDefined("FirstName has to be defined.");
  expect(item.FirstName).not.toBeNull("FirstName can not be null.");

  expect(item.MiddleName).toBeDefined("MiddleName has to be defined.");

  expect(item.LastName).toBeDefined("LastName has to be defined.");
  expect(item.LastName).not.toBeNull("LastName can not be null.");

  expect(item.PostFix).toBeDefined("PostFix has to be defined.");

  expect(item.FullName).toBeDefined("FullName has to be defined.");
  expect(item.FullName).not.toBeNull("FullName can not be null.");

  expect(item.Gender).toBeDefined("Gender has to be defined.");
  expect(item.Gender).not.toBeNull("Gender can not be null.");

  expect(item.PhoneHome).toBeDefined("PhoneHome has to be defined.");
  expect(item.PhoneHome).not.toBeNull("PhoneHome can not be null.");

  expect(item.PhoneWork).toBeDefined("PhoneWork has to be defined.");

  expect(item.PhoneMobile).toBeDefined("PhoneMobile has to be defined.");

  expect(item.Email).toBeDefined("Email has to be defined.");
  expect(item.Email).not.toBeNull("Email can not be null.");

  expect(item.DOB).toBeDefined("DOB has to be defined.");

  expect(item.SSN).toBeDefined("SSN has to be defined.");

  expect(item.Username).toBeDefined("Username has to be defined.");

  expect(item.Password).toBeDefined("Password has to be defined.");

  expect(item.AddressID).toBeDefined("AddressID has to be defined.");
  expect(item.AddressID).not.toBeNull("AddressID can not be null.");

  expect(item.StreetAddress).toBeDefined("StreetAddress has to be defined.");
  expect(item.StreetAddress).not.toBeNull("StreetAddress can not be null.");

  expect(item.StreetAddress2).toBeDefined("StreetAddress2 has to be defined.");

  expect(item.City).toBeDefined("City has to be defined.");
  expect(item.City).not.toBeNull("City can not be null.");

  expect(item.StateId).toBeDefined("StateId has to be defined.");
  expect(item.StateId).not.toBeNull("StateId can not be null.");

  expect(item.PostalCode).toBeDefined("PostalCode has to be defined.");
  expect(item.PostalCode).not.toBeNull("PostalCode can not be null.");

  expect(item.PlusFour).toBeDefined("PlusFour has to be defined.");

  expect(item.CityStateZip).toBeDefined("CityStateZip has to be defined.");
  expect(item.CityStateZip).not.toBeNull("CityStateZip can not be null.");
};

module.exports.qlAddress = function(item) {
  expect(item.AddressID).toBeDefined("AddressID has to be defined.");
  expect(item.AddressID).not.toBeNull("AddressID can not be null.");

  expect(item.DealerId).toBeDefined("DealerId has to be defined.");
  expect(item.DealerId).not.toBeNull("DealerId can not be null.");

  expect(item.AddressTypeId).toBeDefined("AddressTypeId has to be defined.");
  expect(item.AddressTypeId).not.toBeNull("AddressTypeId can not be null.");

  expect(item.AddressValidationStateId).toBeDefined("AddressValidationStateId has to be defined.");
  expect(item.AddressValidationStateId).not.toBeNull("AddressValidationStateId can not be null.");

  expect(item.CarrierRoute).toBeDefined("CarrierRoute has to be defined.");

  expect(item.City).toBeDefined("City has to be defined.");
  expect(item.City).not.toBeNull("City can not be null.");

  expect(item.CongressionalDistric).toBeDefined("CongressionalDistric has to be defined.");

  expect(item.CountryId).toBeDefined("CountryId has to be defined.");
  expect(item.CountryId).not.toBeNull("CountryId can not be null.");

  expect(item.County).toBeDefined("County has to be defined.");

  expect(item.CountyCode).toBeDefined("CountyCode has to be defined.");

  expect(item.DeliveryPoint).toBeDefined("DeliveryPoint has to be defined.");

  expect(item.DPV).toBeDefined("DPV has to be defined.");
  expect(item.DPV).not.toBeNull("DPV can not be null.");

  expect(item.DPVFootnote).toBeDefined("DPVFootnote has to be defined.");

  expect(item.DPVResponse).toBeDefined("DPVResponse has to be defined.");

  expect(item.Extension).toBeDefined("Extension has to be defined.");

  expect(item.ExtensionNumber).toBeDefined("ExtensionNumber has to be defined.");

  expect(item.Latitude).toBeDefined("Latitude has to be defined.");
  expect(item.Latitude).not.toBeNull("Latitude can not be null.");

  expect(item.Longitude).toBeDefined("Longitude has to be defined.");
  expect(item.Longitude).not.toBeNull("Longitude can not be null.");

  expect(item.Phone).toBeDefined("Phone has to be defined.");

  expect(item.PlusFour).toBeDefined("PlusFour has to be defined.");

  expect(item.PostalCode).toBeDefined("PostalCode has to be defined.");
  expect(item.PostalCode).not.toBeNull("PostalCode can not be null.");

  expect(item.PostalCodeFull).toBeDefined("PostalCodeFull has to be defined.");

  expect(item.PostDirectional).toBeDefined("PostDirectional has to be defined.");

  expect(item.PreDirectional).toBeDefined("PreDirectional has to be defined.");

  expect(item.SalesRepId).toBeDefined("SalesRepId has to be defined.");
  expect(item.SalesRepId).not.toBeNull("SalesRepId can not be null.");

  expect(item.SeasonId).toBeDefined("SeasonId has to be defined.");
  expect(item.SeasonId).not.toBeNull("SeasonId can not be null.");

  expect(item.StateId).toBeDefined("StateId has to be defined.");
  expect(item.StateId).not.toBeNull("StateId can not be null.");

  expect(item.StreetAddress).toBeDefined("StreetAddress has to be defined.");
  expect(item.StreetAddress).not.toBeNull("StreetAddress can not be null.");

  expect(item.StreetAddress2).toBeDefined("StreetAddress2 has to be defined.");

  expect(item.StreetName).toBeDefined("StreetName has to be defined.");

  expect(item.StreetNumber).toBeDefined("StreetNumber has to be defined.");

  expect(item.StreetType).toBeDefined("StreetType has to be defined.");

  expect(item.TeamLocationId).toBeDefined("TeamLocationId has to be defined.");
  expect(item.TeamLocationId).not.toBeNull("TeamLocationId can not be null.");

  expect(item.TimeZoneId).toBeDefined("TimeZoneId has to be defined.");
  expect(item.TimeZoneId).not.toBeNull("TimeZoneId can not be null.");

  expect(item.Urbanization).toBeDefined("Urbanization has to be defined.");

  expect(item.UrbanizationCode).toBeDefined("UrbanizationCode has to be defined.");

  expect(item.ValidationVendorId).toBeDefined("ValidationVendorId has to be defined.");
  expect(item.ValidationVendorId).not.toBeNull("ValidationVendorId can not be null.");
};

module.exports.qlQualifyCustomerInfo = function(item) {
  expect(item.LeadID).toBeDefined("LeadID has to be defined.");
  expect(item.LeadID).not.toBeNull("LeadID can not be null.");

  expect(item.SeasonId).toBeDefined("SeasonId has to be defined.");
  expect(item.SeasonId).not.toBeNull("SeasonId can not be null.");

  expect(item.CustomerName).toBeDefined("CustomerName has to be defined.");
  expect(item.CustomerName).not.toBeNull("CustomerName can not be null.");

  expect(item.CustomerEmail).toBeDefined("CustomerEmail has to be defined.");

  expect(item.AddressID).toBeDefined("AddressID has to be defined.");
  expect(item.AddressID).not.toBeNull("AddressID can not be null.");

  expect(item.StreetAddress).toBeDefined("StreetAddress has to be defined.");
  expect(item.StreetAddress).not.toBeNull("StreetAddress can not be null.");

  expect(item.StreetAddress2).toBeDefined("StreetAddress2 has to be defined.");

  expect(item.City).toBeDefined("City has to be defined.");
  expect(item.City).not.toBeNull("City can not be null.");

  expect(item.StateId).toBeDefined("StateId has to be defined.");
  expect(item.StateId).not.toBeNull("StateId can not be null.");

  expect(item.County).toBeDefined("County has to be defined.");
  expect(item.County).not.toBeNull("County can not be null.");

  expect(item.TimeZoneId).toBeDefined("TimeZoneId has to be defined.");
  expect(item.TimeZoneId).not.toBeNull("TimeZoneId can not be null.");

  expect(item.TimeZoneName).toBeDefined("TimeZoneName has to be defined.");
  expect(item.TimeZoneName).not.toBeNull("TimeZoneName can not be null.");

  expect(item.PostalCode).toBeDefined("PostalCode has to be defined.");
  expect(item.PostalCode).not.toBeNull("PostalCode can not be null.");

  expect(item.Phone).toBeDefined("Phone has to be defined.");

  expect(item.CreditReportID).toBeDefined("CreditReportID has to be defined.");
  expect(item.CreditReportID).not.toBeNull("CreditReportID can not be null.");

  expect(item.CRStatus).toBeDefined("CRStatus has to be defined.");
  expect(item.CRStatus).not.toBeNull("CRStatus can not be null.");

  expect(item.Score).toBeDefined("Score has to be defined.");
  expect(item.Score).not.toBeNull("Score can not be null.");

  expect(item.BureauName).toBeDefined("BureauName has to be defined.");
  expect(item.BureauName).not.toBeNull("BureauName can not be null.");

  expect(item.UserID).toBeDefined("UserID has to be defined.");
  expect(item.UserID).not.toBeNull("UserID can not be null.");

  expect(item.CompanyID).toBeDefined("CompanyID has to be defined.");
  expect(item.CompanyID).not.toBeNull("CompanyID can not be null.");

  expect(item.FirstName).toBeDefined("FirstName has to be defined.");
  expect(item.FirstName).not.toBeNull("FirstName can not be null.");

  expect(item.MiddleName).toBeDefined("MiddleName has to be defined.");

  expect(item.LastName).toBeDefined("LastName has to be defined.");
  expect(item.LastName).not.toBeNull("LastName can not be null.");

  expect(item.PreferredName).toBeDefined("PreferredName has to be defined.");

  expect(item.RepEmail).toBeDefined("RepEmail has to be defined.");

  expect(item.PhoneCell).toBeDefined("PhoneCell has to be defined.");

  expect(item.PhoneCellCarrierID).toBeDefined("PhoneCellCarrierID has to be defined.");

  expect(item.PhoneCellCarrier).toBeDefined("PhoneCellCarrier has to be defined.");

  expect(item.SeasonName).toBeDefined("SeasonName has to be defined.");
  expect(item.SeasonName).not.toBeNull("SeasonName can not be null.");
};

module.exports.mcNoteFull = function(item) {
  expect(item.NoteID).toBeDefined("NoteID has to be defined.");
  expect(item.NoteID).not.toBeNull("NoteID can not be null.");

  expect(item.NoteTypeId).toBeDefined("NoteTypeId has to be defined.");
  expect(item.NoteTypeId).not.toBeNull("NoteTypeId can not be null.");

  expect(item.NoteType).toBeDefined("NoteType has to be defined.");
  expect(item.NoteType).not.toBeNull("NoteType can not be null.");

  expect(item.CustomerMasterFileId).toBeDefined("CustomerMasterFileId has to be defined.");
  expect(item.CustomerMasterFileId).not.toBeNull("CustomerMasterFileId can not be null.");

  expect(item.CustomerId).toBeDefined("CustomerId has to be defined.");
  // expect(item.CustomerId).not.toBeNull("CustomerId can not be null.");

  expect(item.LeadId).toBeDefined("LeadId has to be defined.");
  // expect(item.LeadId).not.toBeNull("LeadId can not be null.");

  expect(item.NoteCategory1Id).toBeDefined("NoteCategory1Id has to be defined.");
  expect(item.NoteCategory1Id).not.toBeNull("NoteCategory1Id can not be null.");

  expect(item.Category1).toBeDefined("Category1 has to be defined.");
  expect(item.Category1).not.toBeNull("Category1 can not be null.");

  expect(item.Desc1).toBeDefined("Desc1 has to be defined.");
  expect(item.Desc1).not.toBeNull("Desc1 can not be null.");

  expect(item.NoteCategory2Id).toBeDefined("NoteCategory2Id has to be defined.");
  expect(item.NoteCategory2Id).not.toBeNull("NoteCategory2Id can not be null.");

  expect(item.Category2).toBeDefined("Category2 has to be defined.");
  expect(item.Category2).not.toBeNull("Category2 can not be null.");

  expect(item.Desc2).toBeDefined("Desc2 has to be defined.");
  expect(item.Desc2).not.toBeNull("Desc2 can not be null.");

  expect(item.Note).toBeDefined("Note has to be defined.");
  expect(item.Note).not.toBeNull("Note can not be null.");

  expect(item.CreatedBy).toBeDefined("CreatedBy has to be defined.");
  expect(item.CreatedBy).not.toBeNull("CreatedBy can not be null.");

  expect(item.CreatedOn).toBeDefined("CreatedOn has to be defined.");
  expect(item.CreatedOn).not.toBeNull("CreatedOn can not be null.");

};

module.exports.verifyAddress = function(item) {
  expect(item.AddressID).toBeDefined("AddressID has to be defined.");
  expect(item.AddressID).not.toBeNull("AddressID can not be null.");
};

module.exports.msAccountSalesInformation = function(item) {
  expect(item.AccountID).toBeDefined("AccountID has to be defined.");
  expect(item.AccountID).not.toBeNull("AccountID can not be null.");

  expect(item.AccountID).toBeDefined("PaymentTypeId has to be defined.");

  expect(item.AccountID).toBeDefined("BillingDay has to be defined.");

  expect(item.AccountID).toBeDefined("PanelTypeId has to be defined.");

  expect(item.AccountID).toBeDefined("PanelItemId has to be defined.");

  expect(item.AccountID).toBeDefined("IsTakeOver has to be defined.");

  expect(item.AccountID).toBeDefined("IsOwner has to be defined.");

  expect(item.AccountID).toBeDefined("CellPackageItemId has to be defined.");

  expect(item.AccountID).toBeDefined("CellType has to be defined.");

  expect(item.AccountID).toBeDefined("CellServicePackage has to be defined.");

  expect(item.AccountID).toBeDefined("SetupFee has to be defined.");

  expect(item.AccountID).toBeDefined("Setup1stMonth has to be defined.");

  expect(item.AccountID).toBeDefined("MMR has to be defined.");

  expect(item.Over3Months).toBeDefined("Over3Months has to be defined.");
  expect(item.Over3Months).not.toBeNull("Over3Months can not be null.");

  expect(item.ContractLength).toBeDefined("ContractLength has to be defined.");
};