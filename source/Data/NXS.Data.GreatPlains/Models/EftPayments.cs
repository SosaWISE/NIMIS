using System;
using System.Data.SqlClient;
using SOS.Lib.Core.ErrorHandling;
using SOS.Lib.Util.Configuration;
using SOS.Lib.Util.Cryptography;

namespace NXS.Data.GreatPlains.Models
{
	public class EftPayments
	{
		#region .ctor
		public EftPayments(IErrorManager oErrorManager) : this(null, oErrorManager) { }
		public EftPayments(int? nHeaderID, IErrorManager oErrorManager)
		{
			HeaderID = nHeaderID != null
				? nHeaderID.Value
				: 0;
			_errorManager = oErrorManager;

			// Check if we have to load the header
			if (HeaderID > 0)
				LoadHeader();
		}

		#endregion .ctor

		#region Properties

		private const string LOAD_BY_BATCHNAME = @"
SELECT [HeaderID]
	,[BatchNumber]
	,[AccountNickname]
	,[Customer]
	,[Company]
	,[EffectiveEntryDate]
	,[ScheduleSendDate]
	,[CustTrace]
	,[BankTrace]
	,[TransactionType]
	,[Frequency]
	,[Status]
	,[CrDrMixed]
	,[EntryDescription]
	,[OffsetCreationLevel]
	,[ActivatedBy]
	,[ActivatedDate]
	,[ApprovedBy]
	,[ApprovedDate]
	,[ModifiedBy]
	,[ModifiedDate]
	,[CreatedBy]
	,[CreatedDate]
FROM [PPROT].[dbo].[EFT_ImportHeaders]
WHERE
	([BatchNumber] = '{0}')";

		private const string LOAD_HEADER = @"
/****** Script for SelectTopNRows command from SSMS  ******/
SELECT [HeaderID]
      ,[BatchNumber]
      ,[Customer]
      ,[Company]
      ,[EffectiveEntryDate]
      ,[CustTrace]
      ,[BankTrace]
      ,[TransactionType]
      ,[EntryDescription]
      ,[ActivatedOn]
      ,[ActivatedBy]
      ,[ApprovedOn]
      ,[ApprovedBy]
      ,[CreatedOn]
  FROM [PPROT].[dbo].[EFT_ImportHeaders]
  WHERE
	(HeaderID = {0})
";
		private const string INSERT_HEADER = @"
INSERT INTO [PPROT].[dbo].[EFT_ImportHeaders] (
		{0}
) VALUES (
		{1})
;SELECT @@IDENTITY AS 'Identity'
";

		private readonly IErrorManager _errorManager;

		public int HeaderID { get; private set; }
		public string BatchNumber { get; set; }
		public string AccountNickname { get; set; }
		public string Customer { get; set; }
		public string Company { get; set; }
		public DateTime? EffectiveEntryDate { get; set; }
		public DateTime? ScheduleSendDate { get; set; }
		public int? CustTrace { get; set; }
		public int? BankTrace { get; set; }
		public string TransactionType { get; set; }
		public string Frequency { get; set; }
		public string Status { get; set; }
		public string CrDrMixed { get; set; }
		public string EntryDescription { get; set; }
		public string OffsetCreationLevel { get; set; }
		public DateTime? ActivatedOn { get; set; }
		public string ActivatedBy { get; set; }
		public DateTime? ApprovedOn { get; set; }
		public string ApprovedBy { get; set; }
		public DateTime? CreatedOn { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }

		#endregion Properties

		#region Methods
		#region Private

		private void LoadHeader()
		{
			// Locals

			try
			{
				using (var conn =
						new SqlConnection(
							TripleDES.DecryptString(
								ConfigurationSettings.Current.GetConfig("ConnStringGPScheduleA"), null)))
				{
					using (var cmd = new SqlCommand(string.Format(LOAD_HEADER, HeaderID), conn))
					{
						conn.Open();
						using (var oReader = cmd.ExecuteReader())
						{
							if (oReader != null)
							{
								if (!oReader.HasRows) throw new Exception(string.Format("The HeaderID of '{0}' returned no rows.", HeaderID));

								BatchNumber = oReader.GetString(oReader.GetOrdinal("BatchNumber"));
								AccountNickname = oReader.GetString(oReader.GetOrdinal("AccountNickName"));
								Customer = oReader.GetString(oReader.GetOrdinal("Customer"));
								Company = oReader.GetString(oReader.GetOrdinal("Company"));
								EffectiveEntryDate = oReader.GetDateTime(oReader.GetOrdinal("EffectiveEntryDate"));
								CustTrace = oReader.GetInt32(oReader.GetOrdinal("CustTrace"));
								BankTrace = oReader.GetInt32(oReader.GetOrdinal("BankTrace"));
								TransactionType = oReader.GetString(oReader.GetOrdinal("TransactionType"));
								Frequency = oReader.GetString(oReader.GetOrdinal("Frequency"));
								Status = oReader.GetString(oReader.GetOrdinal("Status"));
								CrDrMixed = oReader.GetString(oReader.GetOrdinal("CrDrMixed"));
								EntryDescription = oReader.GetString(oReader.GetOrdinal("EntryDescription"));
								OffsetCreationLevel = oReader.GetString(oReader.GetOrdinal("OffsetCreationLevel"));
								ActivatedOn = oReader.GetDateTime(oReader.GetOrdinal("ActivatedOn"));
								ActivatedBy = oReader.GetString(oReader.GetOrdinal("ActivatedBy"));
								ApprovedOn = oReader.GetDateTime(oReader.GetOrdinal("ApprovedOn"));
								ApprovedBy = oReader.GetString(oReader.GetOrdinal("ApprovedBy"));
								CreatedOn = oReader.GetDateTime(oReader.GetOrdinal("CreatedOn"));
							}
						}
						conn.Close();
					}
				}
			}
			catch (Exception oEx)
			{
				_errorManager.AddCriticalMessage(oEx, "Error On Loading Header"
					, string.Format("The following error occured:\r\n{0}", oEx.Message));
			}
		}

		private bool UpdateHeader()
		{
			_errorManager.AddCriticalMessage("No Implemented", "This UpdateHeader method has not been implemented yet.");
			return false;
		}

		private bool InsertHeader()
		{
			// Locals
			var bResult = true;

			// Make sure all is good
			try
			{
				// Locals
				// // Batch Number
				var szFields = "BatchNumber";
				var szValues = string.Format("'{0}'", BatchNumber);

				/*
				 *  Build query **/
				// AccountNickname
				if (!string.IsNullOrEmpty(AccountNickname))
				{
					szFields += "\r\n\t\t, AccountNickname";
					szValues += string.Format("\r\n\t\t, '{0}'", AccountNickname);
				}

				// Customer
				if (!string.IsNullOrEmpty(Customer))
				{
					szFields += "\r\n\t\t, Customer";
					szValues += string.Format("\r\n\t\t, '{0}'", Customer);
				}

				// Company
				if (!string.IsNullOrEmpty(Company))
				{
					szFields += "\r\n\t\t, Company";
					szValues += string.Format("\r\n\t\t, '{0}'", Company);
				}

				// EffectiveEntryDate
				if (EffectiveEntryDate != null)
				{
					szFields += "\r\n\t\t, EffectiveEntryDate";
					szValues += string.Format("\r\n\t\t, '{0}'", EffectiveEntryDate.Value);
				}

				// CustTrace
				if (CustTrace != null)
				{
					szFields += "\r\n\t\t, CustTrace";
					szValues += string.Format("\r\n\t\t, {0}", CustTrace.Value);
				}

				// BankTrace
				if (BankTrace != null)
				{
					szFields += "\r\n\t\t, BankTrace";
					szValues += string.Format("\r\n\t\t, {0}", BankTrace.Value);
				}

				// TransactionType
				if (!string.IsNullOrEmpty(TransactionType))
				{
					szFields += "\r\n\t\t, TransactionType";
					szValues += string.Format("\r\n\t\t, '{0}'", TransactionType);
				}

				// Frequency
				if (!string.IsNullOrEmpty(Frequency))
				{
					szFields += "\r\n\t\t, Frequency";
					szValues += string.Format("\r\n\t\t, '{0}'", Frequency);
				}

				// Status
				if (!string.IsNullOrEmpty(Status))
				{
					szFields += "\r\n\t\t, Status";
					szValues += string.Format("\r\n\t\t, '{0}'", Status);
				}

				// CrDrMixed
				if (!string.IsNullOrEmpty(CrDrMixed))
				{
					szFields += "\r\n\t\t, CrDrMixed";
					szValues += string.Format("\r\n\t\t, '{0}'", CrDrMixed);
				}

				// EntryDescription
				if (!string.IsNullOrEmpty(EntryDescription))
				{
					szFields += "\r\n\t\t, EntryDescription";
					szValues += string.Format("\r\n\t\t, '{0}'", EntryDescription);
				}

				// OffsetCreationLevel
				if (!string.IsNullOrEmpty(OffsetCreationLevel))
				{
					szFields += "\r\n\t\t, OffsetCreationLevel";
					szValues += string.Format("\r\n\t\t, '{0}'", OffsetCreationLevel);
				}

				// ActivatedOn
				if (ActivatedOn != null)
				{
					szFields += "\r\n\t\t, ActivatedDate";
					szValues += string.Format("\r\n\t\t, '{0}'", ActivatedOn.Value);
				}

				// ActivatedBy
				if (!string.IsNullOrEmpty(ActivatedBy))
				{
					szFields += "\r\n\t\t, ActivatedBy";
					szValues += string.Format("\r\n\t\t, '{0}'", ActivatedBy);
				}

				// ApprovedOn
				if (ApprovedOn != null)
				{
					szFields += "\r\n\t\t, ApprovedDate";
					szValues += string.Format("\r\n\t\t, '{0}'", ApprovedOn.Value);
				}

				// ApprovedBy
				if (!string.IsNullOrEmpty(ApprovedBy))
				{
					szFields += "\r\n\t\t, ApprovedBy";
					szValues += string.Format("\r\n\t\t, '{0}'", ApprovedBy);
				}

				// ModifiedOn
				if (ModifiedOn != null)
				{
					szFields += "\r\n\t\t, ModifiedDate";
					szValues += string.Format("\r\n\t\t, '{0}'", ModifiedOn.Value);
				}

				// ModifiedBy
				if (ModifiedBy != null)
				{
					szFields += "\r\n\t\t, ModifiedBy";
					szValues += string.Format("\r\n\t\t, '{0}'", ModifiedBy);
				}

				// CreatedOn
				if (CreatedOn != null)
				{
					szFields += "\r\n\t\t, CreatedDate";
					szValues += string.Format("\r\n\t\t, '{0}'", CreatedOn.Value);
				}

				// CreatedBy
				if (CreatedBy != null)
				{
					szFields += "\r\n\t\t, CreatedBy";
					szValues += string.Format("\r\n\t\t, '{0}'", CreatedBy);
				}

				// Build Final Statement
				var szSQLStmt = string.Format(INSERT_HEADER, szFields, szValues);

				// Execute
				using (var oCon =
						new SqlConnection(
							TripleDES.DecryptString(
								ConfigurationSettings.Current.GetConfig("ConnStringGPScheduleA"), null)))
				{
					using (var oCmd = new SqlCommand(szSQLStmt, oCon))
					{
						oCon.Open();
						var oReader = oCmd.ExecuteScalar();
						HeaderID = int.Parse(oReader.ToString());
					}
				}
			}
			catch (Exception oEx)
			{
				_errorManager.AddCriticalMessage(oEx, "Error Creating a Header"
					, string.Format("The following error occurred:\r\n{0}", oEx.Message));
				bResult = false;
			}

			// Return result
			return bResult;
		}

		#endregion Private

		#region Public

		public bool SaveHeader()
		{
			// Check that there is a headerid
			return HeaderID > 0 ? UpdateHeader() : InsertHeader();
		}

		public void LoadByBatchName(string szBatchName)
		{
			// Locals
			var szSQLStmt = string.Format(LOAD_BY_BATCHNAME, szBatchName);

			try
			{
				// Execute
				using (var oCon =
						new SqlConnection(
							TripleDES.DecryptString(
								ConfigurationSettings.Current.GetConfig("ConnStringGPScheduleA"), null)))
				{
					using (var oCmd = new SqlCommand(szSQLStmt, oCon))
					{
						oCon.Open();
						var oReader = oCmd.ExecuteReader();

						if (oReader != null && oReader.HasRows)
						{
							// Get Row
							oReader.Read();

							HeaderID = oReader.GetInt32(oReader.GetOrdinal("HeaderID"));

							if (oReader[oReader.GetOrdinal("BatchNumber")] != null)
								BatchNumber = oReader.GetString(oReader.GetOrdinal("BatchNumber"));
							if (oReader[oReader.GetOrdinal("AccountNickname")] != DBNull.Value)
								AccountNickname = oReader.GetString(oReader.GetOrdinal("AccountNickname"));
							if (oReader[oReader.GetOrdinal("Customer")] != DBNull.Value)
								Customer = oReader.GetString(oReader.GetOrdinal("Customer"));
							if (oReader[oReader.GetOrdinal("Company")] != DBNull.Value)
								Company = oReader.GetString(oReader.GetOrdinal("Company"));
							if (oReader[oReader.GetOrdinal("EffectiveEntryDate")] != DBNull.Value)
								EffectiveEntryDate = oReader.GetDateTime(oReader.GetOrdinal("EffectiveEntryDate"));
							if (oReader[oReader.GetOrdinal("ScheduleSendDate")] != DBNull.Value)
								ScheduleSendDate = oReader.GetDateTime(oReader.GetOrdinal("ScheduleSendDate"));
							if (oReader[oReader.GetOrdinal("CustTrace")] != DBNull.Value)
								CustTrace = oReader.GetInt32(oReader.GetOrdinal("CustTrace"));
							if (oReader[oReader.GetOrdinal("BankTrace")] != DBNull.Value)
								BankTrace = oReader.GetInt32(oReader.GetOrdinal("BankTrace"));
							if (oReader[oReader.GetOrdinal("TransactionType")] != DBNull.Value)
								TransactionType = oReader.GetString(oReader.GetOrdinal("TransactionType"));
							if (oReader[oReader.GetOrdinal("Frequency")] != DBNull.Value)
								Frequency = oReader.GetString(oReader.GetOrdinal("Frequency"));
							if (oReader[oReader.GetOrdinal("Status")] != DBNull.Value)
								Status = oReader.GetString(oReader.GetOrdinal("Status"));
							if (oReader[oReader.GetOrdinal("CrDrMixed")] != DBNull.Value)
								CrDrMixed = oReader.GetString(oReader.GetOrdinal("CrDrMixed"));
							if (oReader[oReader.GetOrdinal("EntryDescription")] != DBNull.Value)
								EntryDescription = oReader.GetString(oReader.GetOrdinal("EntryDescription"));
							if (oReader[oReader.GetOrdinal("OffsetCreationLevel")] != DBNull.Value)
								OffsetCreationLevel = oReader.GetString(oReader.GetOrdinal("OffsetCreationLevel"));
							if (oReader[oReader.GetOrdinal("ActivatedBy")] != DBNull.Value)
								ActivatedBy = oReader.GetString(oReader.GetOrdinal("ActivatedBy"));
							if (oReader[oReader.GetOrdinal("ActivatedDate")] != DBNull.Value)
								ActivatedOn = oReader.GetDateTime(oReader.GetOrdinal("ActivatedDate"));
							if (oReader[oReader.GetOrdinal("ApprovedBy")] != DBNull.Value)
								ApprovedBy = oReader.GetString(oReader.GetOrdinal("ApprovedBy"));
							if (oReader[oReader.GetOrdinal("ApprovedDate")] != DBNull.Value)
								ApprovedOn = oReader.GetDateTime(oReader.GetOrdinal("ApprovedDate"));
							if (oReader[oReader.GetOrdinal("ModifiedBy")] != DBNull.Value)
								ModifiedBy = oReader.GetString(oReader.GetOrdinal("ModifiedBy"));
							if (oReader[oReader.GetOrdinal("ModifiedDate")] != DBNull.Value)
								ModifiedOn = oReader.GetDateTime(oReader.GetOrdinal("ModifiedDate"));
							if (oReader[oReader.GetOrdinal("CreatedBy")] != DBNull.Value)
								CreatedBy = oReader.GetString(oReader.GetOrdinal("CreatedBy"));
							if (oReader[oReader.GetOrdinal("CreatedDate")] != DBNull.Value)
								CreatedOn = oReader.GetDateTime(oReader.GetOrdinal("CreatedDate"));

						}
						else
							HeaderID = 0;
					}
				}
			}
			catch (Exception oEx)
			{
				_errorManager.AddCriticalMessage(oEx, "Error Loading Header"
					, string.Format("The following error occurred:{0}", oEx.Message));
			}
		}

		#endregion Public
		#endregion Methods
	}
}
