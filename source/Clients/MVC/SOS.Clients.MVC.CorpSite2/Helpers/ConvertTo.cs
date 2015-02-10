using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.Data;
using SOS.Services.Interfaces.Models;

namespace SOS.Clients.MVC.CorpSite2.Helpers
{
	public static class ConvertTo
	{
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
	}
}