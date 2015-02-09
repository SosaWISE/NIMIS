using System.Data;
using System.IO;
using NXS.Logic.MonitoringStations.Schemas;

namespace NXS.Logic.MonitoringStations.Helpers
{
	public abstract class Utils
	{
		#region Properties

		private const string _CONFIRM_NUMBER = "Confirm#";
		private const string _SITE_OPTION = "SiteOption";

		#endregion Properties

		#region DataSet Conversions

		public static TDs ConvertDataSet<TDs>(DataSet from) where TDs : DataSet, new()
		{
			// ** Initialize
			var retValue = new TDs();

			// ** Convert
			using (var ms = new MemoryStream())
			{
				from.WriteXml(ms);

				ms.Position = 0;
				if (ms.CanRead)
				{
					retValue.ReadXml(ms, XmlReadMode.InferTypedSchema);
				}
			}

			// ** Return result
			return retValue;
		}

		#endregion DataSet Conversions

		#region Error Handler

		public static bool ErrorsTry(DataSet ds, out Errors dsErrors, out string firstErrorMsg)
		{
			// ** Initialize
			dsErrors = null;
			firstErrorMsg = string.Empty;

			// ** Check that there is a column err_msg
			var columns = ds.Tables[0].Columns;
			if (!columns.Contains("err_msg")) return false;

			// ** Check to see if this is an error message
			dsErrors = ConvertDataSet<Errors>(ds);
			if (dsErrors != null && dsErrors.Tables.Count > 0)
			{

				if (dsErrors.Tables[0].Rows.Count > 0)
				{
					// ** Check the first error message
					if (!string.IsNullOrEmpty(((Errors.TableRow) dsErrors.Tables[0].Rows[0]).err_msg))
					{
						firstErrorMsg = ((Errors.TableRow) dsErrors.Tables[0].Rows[0]).err_msg;
						return true;
					}
				}
			}

			// ** Return result
			return false;
		}

		public static bool ErrorsOnBoardAccountTry(DataSet ds, out ErrorsOnBoardAccount dsErrorsOnBoardAccount)
		{
			string firstErrorMsg;
			string confirmationNumber;
			return ErrorsOnBoardAccountTry(ds, out dsErrorsOnBoardAccount, out firstErrorMsg, out confirmationNumber);
		}

		public static bool ErrorsOnBoardAccountTry(DataSet ds, out ErrorsOnBoardAccount dsErrorsOnBoardAccount,
			out string firstErrorMsg, out string confirmationNumber)
		{
			// ** Initialize
			dsErrorsOnBoardAccount = null;
			firstErrorMsg = string.Empty;
			confirmationNumber = null;

			// ** Check that there is a column err_msg
			var columns = ds.Tables[0].Columns;
			if (!columns.Contains("err_text")) return false;

			// ** Check to see if this is an error message
			dsErrorsOnBoardAccount = ConvertDataSet<ErrorsOnBoardAccount>(ds);
			if (dsErrorsOnBoardAccount != null && dsErrorsOnBoardAccount.Tables.Count > 0)
			{

				if (dsErrorsOnBoardAccount.Tables[0].Rows.Count > 0)
				{
					// ** Check the first error message
					foreach (ErrorsOnBoardAccount.TableRow row in dsErrorsOnBoardAccount.Tables[0].Rows)
					{
						if (row.msg_type == 0)
						{
							// ** Get Confirmation Number
							if (row.entry_id.Equals(_CONFIRM_NUMBER) && row.table_name.Equals(_SITE_OPTION))
							{
								confirmationNumber = GetOnlineConfirmationNumber(row.err_text);
							}
							continue;
						}
						firstErrorMsg = row.err_text;
						return true;
					}
				}
			}

			// ** Return result
			return false;
		}

		public static string GetOnlineConfirmationNumber(string errText)
		{
			// ** Initilize
			var start = errText.IndexOf(',');
			var end = errText.IndexOf(';') - 1;
			var result = errText.Substring(start + 1, end - start);
			
			// ** Clean result
			result = result.Replace(" ", string.Empty);

			// ** Return result
			return result;

		}

		#endregion Error Handler
	}
}
