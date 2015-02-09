using System;
using AR = SOS.Data.SosCrm.QL_LeadBasicInfoView;
using ARCollection = SOS.Data.SosCrm.QL_LeadBasicInfoViewCollection;
using ARController = SOS.Data.SosCrm.QL_LeadBasicInfoViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class QL_LeadBasicInfoViewControllerExtensions
	{
		public static AR CreateLeadBasicInfo(this ARController oCntlr, int nDealerId, string sSalutation
			, string sFirstName, string sMiddleName, string sLastName, string sSuffix, string sEmail
			, string sStreetAddress, string sCity, string sStateId, string sPostalCode, string sPremisePhone
			, int nLeadSourceId, int nLeadDispositionId)
		{
			/** Initialize. */
			var oResult = oCntlr.LoadSingle(SosCrmDataStoredProcedureManager.QL_LeadsCreateBasic(nDealerId
				, null
				, null
				, null
				, null
				, nLeadSourceId
				, nLeadDispositionId
				, sSalutation
				, sFirstName
				, sMiddleName
				, sLastName
				, sSuffix
				, null
				, null
				, null
				, null
				, sEmail
				, null
				, null
				, null
				, sStreetAddress
				, sCity
				, sStateId
				, sPostalCode
				, null
				, null
				, sPremisePhone));

			/** Return result. */
			return oResult;
		}

		public static AR Create(this QL_LeadBasicInfoViewController cntlr, long addressID, int dealerId, string salutation, string firstName, string middleName, string lastName, string suffix
			, int teamLocationId, int seasonId, string salesRepId, string localizationId, int leadDispostionId, int leadSourceId, string gender, string ssn, DateTime? dob, string dl, string dlStateId
			, string email, string phoneHome, string phoneMobile, string phoneWork, string productSkwId)
		{
			return
				cntlr.LoadSingle(SosCrmDataStoredProcedureManager.QL_LeadBasicInfoViewCreate(addressID, dealerId, localizationId,
					teamLocationId, seasonId, salesRepId, leadSourceId
					, leadDispostionId, salutation, firstName, middleName, lastName, suffix, ssn, dob, dl, dlStateId, email, phoneHome,
					phoneWork, phoneMobile, productSkwId));
		}
	}
}
