using System;
using System.Collections.Generic;
using System.Data;
using AR = SOS.Data.SosCrm.Models.AccountSearchResult;
using ARCollection = System.Collections.Generic.IList<SOS.Data.SosCrm.Models.AccountSearchResult>;
using ARController = SOS.Data.SosCrm.Controllers.AccountSearchController;
using SOS.Lib.Util;

namespace SOS.Data.SosCrm.Controllers
{
	public class AccountSearchController
	{
		public ARCollection SearchAccounts(string firstName, string lastName, string premisePhone,
			DateTime? dateOfBirth, string csid, string email, string streetAddress, string city, string zip, string stateAB)
		{
			var result = new List<AR>();

			// Clean up data
			bool firstNameExact = true, lastNameExact = true, streetExact = true;
			if (!string.IsNullOrEmpty(firstName))
			{
				int mark = firstName.IndexOf("*", StringComparison.Ordinal);
				if (mark >= 0)
				{
					firstName = firstName.Replace("*", "%");
					firstNameExact = false;
				}
			}
			if (!string.IsNullOrEmpty(lastName))
			{
				int mark = lastName.IndexOf("*", StringComparison.Ordinal);
				if (mark >= 0)
				{
					lastName = lastName.Replace("*", "%");
					lastNameExact = false;
				}
			}
			if (!string.IsNullOrEmpty(streetAddress))
			{
				int mark = streetAddress.IndexOf("*", StringComparison.Ordinal);
				if (mark >= 0)
				{
					streetAddress = streetAddress.Replace("*", "%");
					streetExact = false;
				}
			}
			if (!string.IsNullOrEmpty(premisePhone))
			{
				premisePhone = StringUtility.TrimPhoneNumber(premisePhone);
			}

			// Execute the search
			using (DataSet dsResults = SosCrmDataStoredProcedureManager.MS_AccountSearchAccounts(StringUtility.NullIfWhiteSpace(firstName),
				firstNameExact, StringUtility.NullIfWhiteSpace(lastName), lastNameExact, StringUtility.NullIfWhiteSpace(premisePhone),
				string.Empty, dateOfBirth, StringUtility.NullIfWhiteSpace(csid), StringUtility.NullIfWhiteSpace(email), StringUtility.NullIfWhiteSpace(streetAddress),
				streetExact, StringUtility.NullIfWhiteSpace(city), StringUtility.NullIfWhiteSpace(zip), StringUtility.NullIfWhiteSpace(stateAB)).GetDataSet())
			{
				// Transform the results
				foreach (DataRow currRow in dsResults.Tables[0].Rows)
				{
					result.Add(
						new AR
						{
							AccountID = (int)currRow[0],
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

			return result;
		}
	}
}
