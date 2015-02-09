using System;
using System.Collections.Generic;
using SOS.Lib.Util;
using AR = SOS.Data.SosCrm.Models.CustomerSearchResult;
using ARCollection = System.Collections.Generic.IList<SOS.Data.SosCrm.Models.CustomerSearchResult>;
using ARController = SOS.Data.SosCrm.Controllers.CustomerSearchController;
using System.Data;

namespace SOS.Data.SosCrm.Controllers
{
	public class CustomerSearchController
	{
		public ARCollection SearchCustomers(string szCompanyID)
		{
			// Locals
			var lResult = new List<AR>();

			var dsResults =
				SosCrmDataStoredProcedureManager.AE_CustomerMasterFileSearchCustomersByCompanyID(szCompanyID).GetDataSet();

			// Transform the results
			foreach (DataRow currRow in dsResults.Tables[0].Rows)
			{
				lResult.Add(
					new AR
					{
						CustomerMFID = (long)currRow[0],
						PremisePhone = currRow.IsNull(1) ? null : StringUtility.FormatPhoneNumber((string)currRow[1]),
						Customer1Name = currRow.IsNull(2) ? null : (string)currRow[2],
						Customer2Name = currRow.IsNull(3) ? null : (string)currRow[3],
						Email = currRow.IsNull(4) ? null : (string)currRow[4],
						City = currRow.IsNull(5) ? null : StringUtility.RevertUppercase((string)currRow[5]),
						State = currRow.IsNull(6) ? null : (string)currRow[6],
						StreetAddress = currRow[7] as string
					});
			}

			// Return result
			return lResult;
		}

		public ARCollection SearchCustomers(string szFirstName, string szLastName, string szPremisePhone,
			DateTime? dDateOfBirth, string szCsid, string szEmail, string szStreetAddress, string szCity, string szPostalCode, string szStateAB)
		{
			var lResult = new List<AR>();

			// Clean up data
			bool bFirstNameExact = true, bLastNameExact = true, bStreetExact = true;

			if (!string.IsNullOrEmpty(szFirstName))
			{
				int mark = szFirstName.IndexOf("*", StringComparison.Ordinal);
				if (mark >= 0)
				{
					szFirstName = szFirstName.Replace("*", "%");
					bFirstNameExact = false;
				}
			}
			if (!string.IsNullOrEmpty(szLastName))
			{
				int mark = szLastName.IndexOf("*", StringComparison.Ordinal);
				if (mark >= 0)
				{
					szLastName = szLastName.Replace("*", "%");
					bLastNameExact = false;
				}
			}
			if (!string.IsNullOrEmpty(szStreetAddress))
			{
				int mark = szStreetAddress.IndexOf("*", StringComparison.Ordinal);
				if (mark >= 0)
				{
					szStreetAddress = szStreetAddress.Replace("*", "%");
					bStreetExact = false;
				}
			}
			if (!string.IsNullOrEmpty(szPremisePhone))
			{
				szPremisePhone = StringUtility.TrimPhoneNumber(szPremisePhone);
			}

			// Execute the search
			using (DataSet dsResults = SosCrmDataStoredProcedureManager.AE_CustomerMasterFileSearchCustomers(
				StringUtility.NullIfWhiteSpace(szFirstName)
				, bFirstNameExact
				, StringUtility.NullIfWhiteSpace(szLastName)
				, bLastNameExact
				, StringUtility.NullIfWhiteSpace(szPremisePhone)
				, dDateOfBirth
				, StringUtility.NullIfWhiteSpace(szCsid)
				, StringUtility.NullIfWhiteSpace(szEmail)
				, StringUtility.NullIfWhiteSpace(szStreetAddress)
				, bStreetExact
				, StringUtility.NullIfWhiteSpace(szCity)
				, StringUtility.NullIfWhiteSpace(szPostalCode)
				, StringUtility.NullIfWhiteSpace(szStateAB)).GetDataSet())
			{
				// Transform the results
				foreach (DataRow currRow in dsResults.Tables[0].Rows)
				{
					lResult.Add(
						new AR
						{
							CustomerMFID = (long)currRow[0],
							PremisePhone = currRow.IsNull(1) ? null : StringUtility.FormatPhoneNumber((string)currRow[1]),
							Customer1Name = currRow.IsNull(2) ? null : (string)currRow[2],
							Customer2Name = currRow.IsNull(3) ? null : (string)currRow[3],
							Email = currRow.IsNull(4) ? null : (string)currRow[4],
							City = currRow.IsNull(5) ? null : StringUtility.RevertUppercase((string)currRow[5]),
							State = currRow.IsNull(6) ? null : (string)currRow[6],
							StreetAddress = currRow[7] as string
						});
				}
			}

			return lResult;
		}
	}
}
