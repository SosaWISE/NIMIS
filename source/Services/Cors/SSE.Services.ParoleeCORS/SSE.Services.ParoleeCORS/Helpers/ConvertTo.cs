﻿using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.Data;
using SOS.FunctionalServices.Contracts.Models.Parolee;
using SSE.Services.ParoleeCORS.Models;
using CmsModels = SOS.Services.Interfaces.Models.CmsModels;

namespace SSE.Services.ParoleeCORS.Helpers
{
	public static class ConvertTo
	{
		#region AeCustomer
		public static CmsModels.AeCustomer CastFnsToAeCustomer(IFnsAeCustomerGpsClientsViewModel oItem)
		{
			/** Initialize. */
			var oResult = new CmsModels.AeCustomer();

			/** Bind data. */
			oResult.CustomerID = oItem.CustomerID;
			oResult.CustomerTypeId = oItem.CustomerTypeId;
			oResult.CustomerMasterFileId = oItem.CustomerMasterFileId;
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

		public static CmsModels.AeCustomer CastFnsToAeCustomer(IFnsCustomerFullDataModel oAeCustomerModel)
		{
			/** Initialize. */
			var oResult = new CmsModels.AeCustomer();

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
		#endregion AeCustomer

		#region McOfficer
		
		public static SosOfficer CastFnsToMcOfficer(IFnsParoleeOfficer oMcOfficerModel)
		{
			var oResult = new SosOfficer();

			/** Bind data. */
			oResult.OfficerID = oMcOfficerModel.OfficerID;
			oResult.LocalizationId = oMcOfficerModel.LocalizationId;
			oResult.UserName = oMcOfficerModel.UserName;
			oResult.Password = oMcOfficerModel.Password;
			oResult.Title = oMcOfficerModel.Title;
			oResult.Department = oMcOfficerModel.Department;
			oResult.Salutation = oMcOfficerModel.Salutation;
			oResult.FirstName = oMcOfficerModel.FirstName;
			oResult.MiddleName = oMcOfficerModel.MiddleName;
			oResult.LastName = oMcOfficerModel.LastName;
			oResult.Suffix = oMcOfficerModel.Suffix;
			oResult.OfficePhone = oMcOfficerModel.OfficePhone;
			oResult.MobilePhone = oMcOfficerModel.MobilePhone;
			oResult.HomePhone = oMcOfficerModel.HomePhone;
			oResult.Pager = oMcOfficerModel.Pager;
			oResult.Fax = oMcOfficerModel.Fax;
			oResult.Email1 = oMcOfficerModel.Email1;
			oResult.Email2 = oMcOfficerModel.Email2;
			oResult.EmailPasswordReset = oMcOfficerModel.EmailPasswordReset;
			oResult.SmsGateway = oMcOfficerModel.SmsGateway;
			oResult.SmsAddress = oMcOfficerModel.SmsAddress;
			oResult.SessionTimeOut = oMcOfficerModel.SessionTimeOut;
			
			//oResult.Password = oMcOfficerModel.Password;
			//oResult.IsActive = oMcOfficerModel.IsActive;
			//oResult.ModifiedOn = oMcOfficerModel.ModifiedOn;
			//oResult.ModifiedBy = oMcOfficerModel.ModifiedBy;
			//oResult.CreatedOn = oMcOfficerModel.CreatedOn;
			//oResult.CreatedBy = oMcOfficerModel.CreatedBy;

			/** Return result. */
			return oResult;

		}

		#endregion McOfficer
	}
}