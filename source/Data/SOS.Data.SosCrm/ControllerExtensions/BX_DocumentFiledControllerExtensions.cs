using System;
using System.Collections.Generic;
using System.Data;
using SubSonic;
using AR = SOS.Data.SosCrm.BX_DocumentField;
using ARCollection = SOS.Data.SosCrm.BX_DocumentFieldCollection;
using ARController = SOS.Data.SosCrm.BX_DocumentFieldController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class BX_DocumentFiledControllerExtensions
	{
		public static Dictionary<string, DocumentFieldAndValue> GetDictByBarcodeTypeId(this ARController cntlr,
			string barcodeTypeId,
			long accountId)
		{
			// ** Initlialize
			var keyPair = new Dictionary<string, DocumentFieldAndValue>();
			var barcodeType = SosCrmDataContext.Instance.BX_BarcodeTypes.LoadByPrimaryKey(barcodeTypeId);

			// ** Call the DocumentFields 
			var qry = AR.Query().AND(AR.Columns.BarcodeTypeId, barcodeTypeId);
			var col = cntlr.LoadCollection(qry);

			// ** Check that there is something in the collection
			if (col.Count == 0) return null;
			
			// ** Read Values
			using (var rdr = new CodingHorror().ExecuteReader(barcodeType.SqlStatement, accountId))
			{
				if (rdr.Read())
				{
					// ** Loop through result and build dictionary.
					foreach (BX_DocumentField bxDocumentField in col)
					{
						// ** Init
						object value = null;

						// ** Get value if there is one, or use a default value, or leave it empty.
						if (ColumnExists(rdr, bxDocumentField.FieldKey))
						{
							value = FindColumnReturnValue(rdr, bxDocumentField.FieldKey);
						}
						// ** add to dictionary
						keyPair.Add(bxDocumentField.FieldKey, new DocumentFieldAndValue(bxDocumentField, value, 
							bxDocumentField.DataType,
							bxDocumentField.Justification,
							bxDocumentField.IsEncrypted,
							bxDocumentField.Mask));
					}
				}
			}
			// ** Return result
			return keyPair;
		}

		private static object FindColumnReturnValue(IDataReader rdr, string fieldKey)
		{
			// ** Init find column with the fieldKey we are looking for.
			var index = rdr.GetOrdinal(fieldKey);

			if (index == -1)
				return null; // throw new Exception(string.Format("The FieldKey '{0}' was not found in the SQL Statement.", fieldKey));

			var value = rdr.GetValue(index);

			// ** Return result
				return value;
		}

		public static bool GetSubDataSource(this ARController cntlr, Dictionary<string, object> keyFields, string sql, long accountId, Func<DocumentFieldAndValue, object> fxGetData, out string message)
		{
			// ** Initialize
			message = "Initializing...";
			var result = false;

			// ** Call SQL Statement
			try
			{
				using (var rdr = new CodingHorror().ExecuteReader(sql, accountId))
				{
					while (rdr.Read())
					{
						for(int i = 0; i < rdr.FieldCount; i++)
						{
// TODO: Andres							var dataValue = fxGetData(new DocumentFieldAndValue());
						}
					}
				}

				result = true;
				message = "Successful import of datasource.";
			}
			catch (Exception ex)
			{
				message = string.Format("The following error was thrown generating the datasource: {0}", ex.Message);
			}

			// ** return result;
			return result;
		}

		private static bool ColumnExists(IDataReader reader, string columnName)
		{
			// ** Init
			var schemaTable = reader.GetSchemaTable();
			if (schemaTable == null) return false;

				schemaTable.DefaultView.RowFilter = "ColumnName= '" + columnName + "'";
			return (schemaTable.DefaultView.Count > 0);
		}
	}

	#region Supporting Objects

	public class DocumentFieldAndValue
	{
		public DocumentFieldAndValue(BX_DocumentField documentField, object value, string fieldType, string justification, bool isEncrypted, string mask)
		{
			DocumentField = documentField;
			Value = value;
			FieldType = fieldType;
			Justification = justification;
			IsEncrypted = isEncrypted;
			Mask = mask;
		}
		public AR DocumentField { get; private set; }
		public object Value { get; private set; }
		public string FieldType { get; private set; }
		public string Justification { get; private set; }
		public bool IsEncrypted { get; private set; }
		public string Mask { get; private set; }
	}
	#endregion Supporting Objects
}
