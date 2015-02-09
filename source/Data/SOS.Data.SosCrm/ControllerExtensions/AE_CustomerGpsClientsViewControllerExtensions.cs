using SubSonic;
using System;
using AR = SOS.Data.SosCrm.AE_CustomerGpsClientsView;
using ARController = SOS.Data.SosCrm.AE_CustomerGpsClientsViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable InconsistentNaming
	public static class AE_CustomerGpsClientsViewControllerExtensions
// ReSharper restore InconsistentNaming
	{
		public static AR Authenticate(this ARController oCntlr, string sUsername, string sPassword)
		{
			/** Initialize. */
			var oQry = ReadOnlyRecord<AR>.Query()
				.WHERE(AR.Columns.Username, sUsername)
				.WHERE(AR.Columns.Password, sPassword);

			/** Execute. */
			var oResult = oCntlr.LoadSingle(oQry);

			if (oResult.IsLoaded)
				oCntlr.LoadSingle(SosCrmDataStoredProcedureManager.AE_CustomerGpsClientUpateLastLogin(oResult.CustomerID));

			/** Return result. */
			return oResult;
		}

		public static AR GpsClientSignup(this ARController oCntlr, int? nDealerId, string sSalesRepId, string sLocalizationId, string sFirstName, string sLastName, string sGender, string sPhoneHome, string sPhoneWork, string sPhoneMobile, string sEmail, string sUsername, string sPassword, int? nLeadSourceId, int? nLeadDispositionId)
		{
			/** Init. */
			AR oResult =
				oCntlr.LoadSingle(SosCrmDataStoredProcedureManager.AE_CustomerGpsClientSignup(nDealerId, sSalesRepId,
																							  sLocalizationId, sFirstName, sLastName,
																							  sGender, sPhoneHome, sPhoneWork,
																							  sPhoneMobile, sEmail, sUsername,
																							  sPassword, nLeadSourceId,
																							  nLeadDispositionId));
			/** Return result. */
			return oResult;
		}

        public static AR  GpsClientDelete(this ARController oCntlr, long customerID)
	    {
            /** Initialize. */
	        AR oResult = oCntlr.LoadSingle(SosCrmDataStoredProcedureManager.AE_CustomerGPSClientDelete(customerID));

            /** Return result. */
	        return oResult;
	    }

	    public static AE_CustomerGpsClientsView GpsClientUpdate(this AE_CustomerGpsClientsViewController oCntlr, string localizationId, string prefix, string firstName, string lastName, string gender, string phoneHome, string phoneWork, string phoneMobile, string email, DateTime? dob, string ssn, string username, string password, string stateId, string countryId, int? timezoneId, string streetAddress, string streetAddress2, string county, string city, string postalCode, string plusFour, string phone)
	    {
            /** Initialize. */
            AR oResult = oCntlr.LoadSingle(SosCrmDataStoredProcedureManager.AE_CustomerGPSClientUpdate(localizationId, prefix, firstName, lastName, gender,phoneHome, phoneWork, phoneMobile, email, dob, ssn, username, password, stateId, countryId, timezoneId, streetAddress, streetAddress2, county, city, postalCode, plusFour, phone));

            /** Return result. */
	        return oResult;
	    }

	    public static AE_CustomerGpsClientsView GpsClientRead(this AE_CustomerGpsClientsViewController oCntlr, long? customerID)
	    {
            /** Initialize. */
            AR oResult = oCntlr.LoadSingle(SosCrmDataStoredProcedureManager.AE_CustomerGPSClientRead(customerID));

            /**Return result. */
	        return oResult;

	    }
	}
}