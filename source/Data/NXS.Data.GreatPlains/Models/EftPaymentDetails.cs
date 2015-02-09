using System;
using System.Data.SqlClient;
using SOS.Lib.Util.Configuration;
using SOS.Lib.Util.Cryptography;
using SOS.Lib.Core.ErrorHandling;

namespace NXS.Data.GreatPlains.Models
{
	public class EftPaymentDetails
	{
		#region .ctor

		public EftPaymentDetails(EftPaymentProofDetailStruct oItem, IErrorManager oErrorManager)
		{
			_item = oItem;
			_errorManager = oErrorManager;
		}

		#endregion .ctor

		#region Properties
		private const string LOAD_DETAIL = @"
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
		private const string INSERT_DETAIL = @"
INSERT INTO [PPROT].[dbo].[EFT_ImportDetails] (
		{0}
) VALUES (
		{1})
;SELECT @@IDENTITY AS 'Identity'
";
		private EftPaymentProofDetailStruct _item;
		private readonly IErrorManager _errorManager;

		public int EftImportID { get; private set; }
		#endregion Properties

		#region Methods

		public bool SaveDetails(IErrorManager oErrorManager)
		{
			// Local
			var bResult = true;

			// Make sure all is good
			try
			{
				// Locals
				// // Batch Number
				var szFields = "HeaderID";
				var szValues = string.Format("{0}", _item.HeaderID);

				/*
				 *  Build query **/
				// BatchNumber
				if (!string.IsNullOrEmpty(_item.BatchNumber))
				{
					szFields += "\r\n\t\t, BatchNumber";
					szValues += string.Format("\r\n\t\t, '{0}'", _item.BatchNumber);
				}

				// DOCNUMBR
				if (!string.IsNullOrEmpty(_item.Docnumbr))
				{
					szFields += "\r\n\t\t, DOCNUMBR";
					szValues += string.Format("\r\n\t\t, '{0}'", _item.Docnumbr);
				}

				// CUSTNMBR
				if (!string.IsNullOrEmpty(_item.Custnmbr))
				{
					szFields += "\r\n\t\t, CUSTNMBR";
					szValues += string.Format("\r\n\t\t, '{0}'", _item.Custnmbr);
				}

				// RecipientID
				if (!string.IsNullOrEmpty(_item.RecipientID))
				{
					szFields += "\r\n\t\t, RecipientID";
					szValues += string.Format("\r\n\t\t, '{0}'", _item.RecipientID);
				}

				// CustName
				if (!string.IsNullOrEmpty(_item.RecipientName))
				{
					szFields += "\r\n\t\t, CustName";
					szValues += string.Format("\r\n\t\t, '{0}'", _item.RecipientName);
				}

				// BatchEntry
				szFields += "\r\n\t\t, BatchEntry";
				szValues += string.Format("\r\n\t\t, {0}", _item.BatchEntry);

				// BankNumber
				if (_item.BankNumber > 0)
				{
					szFields += "\r\n\t\t, BankNumber";
					szValues += string.Format("\r\n\t\t, {0}", _item.BankNumber);
				}

				// AccountNumber
				if (!string.IsNullOrEmpty(_item.AccountNo))
				{
					szFields += "\r\n\t\t, AccountNumber";
					szValues += string.Format("\r\n\t\t, '{0}'", _item.AccountNo);
				}

				// Type
				if (!string.IsNullOrEmpty(_item.Type))
				{
					szFields += "\r\n\t\t, TypeID";
					szValues += string.Format("\r\n\t\t, {0}", _GetTypeID(_item.Type));
				}

				// Amount
				if (!string.IsNullOrEmpty(_item.OrgAmount))
				{
					szFields += "\r\n\t\t, Amount";
					szValues += string.Format("\r\n\t\t, {0}", _item.OrgAmount);
				}

				// DateCreated
				szFields += "\r\n\t\t, DateCreated";
				szValues += "\r\n\t\t, GetDate()";

				// Build Final Statement
				var szSQLStmt = string.Format(INSERT_DETAIL, szFields, szValues);
				Console.WriteLine(szSQLStmt);

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
						EftImportID = int.Parse(oReader.ToString());
					}
				}
			}
			catch (Exception oEx)
			{
				_errorManager.AddCriticalMessage(oEx, "Error Creating a Details"
					, string.Format("The following error occurred:\r\n{0}", oEx.Message));
				bResult = false;
			}


			// Return result
			return bResult;
		}

		private static int _GetTypeID(string szType)
		{
			switch (szType)
			{
				case "D":
					return (int)EnumDetailTypes.Debit;
				case "C":
					return (int)EnumDetailTypes.Credit;
				default:
					return (int)EnumDetailTypes.Undefined;
			}
		}

		#endregion Methods
	}

	public enum EnumDetailTypes
	{
		Debit = 0,
		Credit = 1,
		Undefined = 10
	}

	public struct EftPaymentProofDetailStruct
	{
		#region Fields

		public int HeaderID { get; set; }
		public string BatchNumber { get; set; }
		public int BatchEntry { get; set; }
		public string Docnumbr { get; set; }
		public string Custnmbr { get; set; }
		public string RecipientName { get; set; }
		public int AccountID { get; set; }
		public string RecipientID { get; set; }
		public int BankNumber { get; set; }
		public string AccountNo { get; set; }
		public string Type { get; set; }
		public string DiscData { get; set; }
		public string Status { get; set; }
		public string OrgAmount { get; set; }

		#endregion Fields
	}
}
