using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using NXS.Data.Letters;
using NXS.Data.Letters.ControllerExtensions;
using NXS.Data.Licensing;
using SOS.Data;
using SOS.Lib.Util.Cryptography;
using SubSonic;

namespace NXS.Logic.Letters
{
	public class TemplateHelper
	{
		/// <summary>
		/// This DataRow is filled by the query attached to the template.  pdf is populated with info in this row
		/// </summary>
		private static DataRow _masterRecord;

		/// <summary>
		/// AutoPopulates the template given by templateID
		/// </summary>
		public static byte[] CreateTemplatePdfBuffer(LettersDataContext lettersDB, int templateID, int primaryKey)
		{
			var templateIDList = new List<int>();
			templateIDList.Add(templateID);

			var primaryKeyList = new List<int>();
			primaryKeyList.Add(primaryKey);

			return CreateTemplatePdfBuffer(lettersDB, templateIDList, primaryKeyList, null);
		}

		/// <summary>
		/// AutoPopulates multiple templates given by templateIDList with one primaryKey
		/// </summary>
		public static byte[] WritePdfToFile(LettersDataContext lettersDB, List<int> templateIDList, int primaryKey)
		{
			var primaryKeyList = new List<int>();
			primaryKeyList.Add(primaryKey);

			return CreateTemplatePdfBuffer(lettersDB, templateIDList, primaryKeyList, null);
		}

		/// <summary>
		/// AutoPopulates multiple copies of one template given by PrimaryKeyList
		/// </summary>
		public static byte[] CreateTemplatePdfBuffer(LettersDataContext lettersDB, int templateID, List<int> primaryKeyList)
		{
			var templateIDList = new List<int>();
			templateIDList.Add(templateID);

			return CreateTemplatePdfBuffer(lettersDB, templateIDList, primaryKeyList, null);
		}

		/// <summary>
		/// AutoPopulates multiple copies templateIDList and primaryKeyList
		/// </summary>
		public static byte[] CreateTemplatePdfBuffer(LettersDataContext lettersDB, List<int> templateIDList,
													 List<int> primaryKeyList, Dictionary<int, Image> barcodeDict)
		{
			byte[] result;

// ReSharper disable once UnusedVariable
			var fileList = new List<string>();
			using (var stream = new MemoryStream())
			{
				var doc = new Document(PageSize.LETTER, 72, 72, 36, 36);
				PdfWriter pdfWriter = PdfWriter.GetInstance(doc, stream);
				doc.Open();

				foreach (int primaryKey in primaryKeyList)
				{
					foreach (int templateID in templateIDList)
					{
						AppendTemplateToPdf(lettersDB, templateID, primaryKey, pdfWriter, doc, barcodeDict);
					}
				}

				doc.Close();

				result = stream.ToArray();
			}

			return result;
		}

		public static void AppendTemplateToPdf(LettersDataContext lettersDB, int templateID, int primaryKey,
											   PdfWriter pdfWriter, Document doc, Dictionary<int, Image> barcodeDict)
		{
			LD_Template template = lettersDB.LD_Templates.LoadByPrimaryKey(templateID);
			DataRow row = GetDataRowForTemplate(lettersDB, primaryKey, template);

			using (var stream = new MemoryStream())
			{
				var pdfReader = new PdfReader(template.Template);
				var pdfStamp = new PdfStamper(pdfReader, stream);
				// *** Don't let iTextSharp close the stream - we'll close it when we're done with it
				pdfStamp.Writer.CloseStream = false;

				PopulateFields(lettersDB, template, row, pdfStamp.AcroFields);

				pdfStamp.FormFlattening = true;
				pdfStamp.Close();

				// Reset MemoryStream to start position
				stream.Position = 0;
				var buffer = new byte[stream.Length];
				stream.Read(buffer, 0, (int)stream.Length);

				var newPdfReader = new PdfReader(buffer);
				PdfContentByte cb = pdfWriter.DirectContent;

				int numPages = pdfReader.NumberOfPages;
				for (int i = 1; i <= numPages; i++)
				{
					doc.SetPageSize(PageSize.LETTER);
					doc.NewPage();
					PdfImportedPage page = pdfWriter.GetImportedPage(newPdfReader, i);
					cb.AddTemplate(page, 0, 0);

					if (barcodeDict != null)
					{
						Image oBarcode;
						if (barcodeDict.TryGetValue(i, out oBarcode))
						{
							doc.Add(oBarcode); //the key of the dictionary is the page to which the barcode belongs
						}
					}
				}
			}
		}

		/// <summary>
		/// Gets the db value and parses it depending on field
		/// </summary>
		private static string EntryValue(DataRow row, LD_Field field)
		{
			string szRtn;
			try
			{
				object value = row[field.DBColumnName];

				if (string.IsNullOrEmpty(field.PreprocessFormatString))
				{
					szRtn = value.ToString();
				}
				else
				{
					szRtn = string.Format("{0:" + field.PreprocessFormatString + "}", value);
				}
			}
			catch
			{
				//for hard-coded entries like company address
				szRtn = field.DBColumnName;
			}

			//deencrypt
			if (field.IsEncrypted)
				szRtn = TripleDES.DecryptString(szRtn, null);

			//substrings
			if (field.IsSubstring)
			{
				szRtn = szRtn.Replace("-", "").Replace("(", "").Replace(")", "").Replace(" ", "");
				if (field.SubstringLength + field.SubstringStart <= szRtn.Length)
				{
					//get substring
					szRtn = szRtn.Substring((int)field.SubstringStart, (int)field.SubstringLength);
				}
				else if (field.SubstringStart <= szRtn.Length)
				{
					//get the part of the string that we can
					szRtn = szRtn.Substring((int)field.SubstringStart, szRtn.Length - (int)field.SubstringStart);
				}
				else
				{
					szRtn = "";
				}
			}

			//checkboxes
			if (field.FieldName != null)
			{
				if (field.FieldName.Contains("="))
				{
					string split = field.FieldName.Split('=')[1];
					szRtn = szRtn == split ? "X" : "";
				}
			}

			//dates - only get short string 
			DateTime dDummy;
			if (DateTime.TryParse(szRtn, out dDummy))
			{
				szRtn = dDummy.ToString("MM/dd/yyyy");
			}
			return szRtn;
		}

		private static DataRow GetDataRowForTemplate(LettersDataContext lettersDB, int primaryKey, LD_Template template)
		{
			LD_DocType docType = lettersDB.LD_DocTypes.LoadByPrimaryKey(template.DocTypeID);
			var con = new SqlConnection();

			switch ((LD_DocType.DocTypeEnum)docType.DocTypeID)
			{
				case LD_DocType.DocTypeEnum.Account:
					con.ConnectionString = DataService.Providers[SubSonicConfigHelper.SOS_CRM_PROVIDER_NAME].DefaultConnectionString;
					break;
				case LD_DocType.DocTypeEnum.Recruit:
				case LD_DocType.DocTypeEnum.RecruitRegistration:
					con.ConnectionString = DataService.Providers[SubSonicConfigHelper.SOS_HUMAN_RESOURCE_PROVIDER_NAME].DefaultConnectionString;
					break;
			}

			var oCmd = new SqlCommand(string.Format(docType.Query, primaryKey), con);
			var oDt = new DataTable();
			var oDad = new SqlDataAdapter(oCmd);
			oDad.Fill(oDt);
			DataRow row = oDt.Rows[0];
			return row;
		}

		private static void PopulateFields(LettersDataContext lettersDB, LD_Template oTemplate, DataRow row, AcroFields oForm)
		{
			//populate fields
			foreach (LD_Field oField in lettersDB.LD_Fields.LoadAllForTemplate(oTemplate.TemplateID))
			{
				// Check if we skip this time through
				if (oField.FieldName == null || oField.IsDeleted) continue;

				string szTest = oField.FieldName;
				LD_Field field = oField;
				//string szCmd = oField.Path.Path;
				var lEntries = new List<string>();
				int i = 0;

				lEntries.Add(EntryValue(row, field));
				if (lEntries[0] != "")
				{
					bool bChildExists = true;
					while (bChildExists)
					{
						if (field.SubsequentField != null)
						{
							szTest = field.FieldName;
							field = field.SubsequentField;
							lEntries.Add(EntryValue(row, field));
							i = i + 1;
						}
						else
						{
							bChildExists = false;
						}
					}

					var szEntry = new string[i + 1];
					for (int j = 0; j <= i; j++)
					{
						szEntry[j] = lEntries[j];
					}

					oForm.SetField(oField.FieldName, string.Format(oField.FormatString, szEntry));
				}

				// Print for debuging reasons
				Debug.WriteLine(szTest);
			}
		}


		public static byte[] GetPDFBuffer(LettersDataContext lettersDB, LM_RequirementCollection oRequirements,
										  int nPrimaryKeyId)
		{
			// Locals
			byte[] oResult;

			using (var oMStr = new MemoryStream())
			{
				var oDoc = new Document(PageSize.LETTER, 72, 72, 36, 36);
				PdfWriter oPDFWriter = PdfWriter.GetInstance(oDoc, oMStr);
				oDoc.Open();
				int nCurrentPageNumber = 0;
				PdfContentByte cb = oPDFWriter.DirectContent;

				// Loop through the requirements
				foreach (LM_Requirement oReq in oRequirements)
				{
					// Check that the requirement has a template
					if (oReq.TemplateID == null) continue;
					LD_Template oTemplate = lettersDB.LD_Templates.LoadByPrimaryKey((int)oReq.TemplateID);

					#region Master Record

					LD_DocType oDocType = lettersDB.LD_DocTypes.LoadByPrimaryKey(oTemplate.DocTypeID);
					var oCon = new SqlConnection();

					// switch to the correct Document type
					switch ((LD_DocType.DocTypeEnum)oDocType.DocTypeID)
					{
						case LD_DocType.DocTypeEnum.Account:
							oCon.ConnectionString =
								DataService.Providers[SubSonicConfigHelper.SOS_CRM_PROVIDER_NAME].DefaultConnectionString;
							break;
						case LD_DocType.DocTypeEnum.Recruit:
						case LD_DocType.DocTypeEnum.RecruitRegistration:
							oCon.ConnectionString =
								DataService.Providers[SubSonicConfigHelper.SOS_HUMAN_RESOURCE_PROVIDER_NAME].DefaultConnectionString;
							break;
					}

					var oCmd = new SqlCommand(string.Format(oDocType.Query, nPrimaryKeyId), oCon);
					var oDt = new DataTable();
					var oDad = new SqlDataAdapter(oCmd);
					oDad.Fill(oDt);

					// Checked that there was data found.
					if (oDt.Rows.Count == 0) continue;
					DataRow row = oDt.Rows[0];

					#endregion Master Record

					#region Bind fields and add to Main PDF Document

					//                    using (var oDocStr = new FileStream("C:\\Temp.pdf", FileMode.CreateNew, FileAccess.Write))
					using (var oDocStr = new MemoryStream())
					{
						#region Populate Fields

						// Locals
						byte[] oSqlBytes = oTemplate.Template;
						var oReader = new PdfReader(oSqlBytes);
						//var oStamp = new PdfStamper(oReader, oStream);
						var oStamp = new PdfStamper(oReader, oDocStr);
						oStamp.Writer.CloseStream = false;
						AcroFields oForm = oStamp.AcroFields;

						//populate fields
						foreach (LD_Field oField in lettersDB.LD_Fields.LoadAllForTemplate(oTemplate.TemplateID))
						{
							// Check if we skip this time through
							if (oField.FieldName == null || oField.IsDeleted) continue;

							string szTest = oField.FieldName;
							LD_Field field = oField;
							//string szCmd = oField.Path.Path;
							var lEntries = new List<string>();
							int i = 0;

							lEntries.Add(EntryValue(row, field));
							if (lEntries[0] != "")
							{
								bool bChildExists = true;
								while (bChildExists)
								{
									if (field.SubsequentField != null)
									{
										szTest = field.FieldName;
										field = field.SubsequentField;
										lEntries.Add(EntryValue(row, field));
										i = i + 1;
									}
									else
									{
										bChildExists = false;
									}
								}

								var szEntry = new string[i + 1];
								for (int j = 0; j <= i; j++)
								{
									szEntry[j] = lEntries[j];
								}

								oForm.SetField(oField.FieldName, string.Format(oField.FormatString, szEntry));
							}

							// Print for debuging reasons
							Debug.WriteLine(szTest);
						}

						#endregion Populate Fields

						#region Add Pages to Document

						// Create new reader
						oStamp.FormFlattening = true;
						oStamp.Close();

						byte[] yBoundDoc = oDocStr.ToArray();
						//                        var oBoundReader = new PdfReader("C:\\Temp.pdf");
						oDocStr.Close();
						var oBoundReader = new PdfReader(yBoundDoc);

						int nLastPage = nCurrentPageNumber + oTemplate.Pages;
						while (nCurrentPageNumber < nLastPage)
						{
							// Increase the page number
							nCurrentPageNumber++;
							oDoc.SetPageSize(PageSize.LETTER);
							oDoc.NewPage();
							PdfImportedPage oPage = oPDFWriter.GetImportedPage(oBoundReader, nCurrentPageNumber);
							cb.AddTemplate(oPage, 0, 0);
						}

						#endregion Add Pages to Document
					}

					#endregion Bind fields and add to Main PDF Document
				}

				// Close document
				oDoc.Close();

				// Get result
				oResult = oMStr.ToArray();
			}

			// Return result
			return oResult;
		}

		public static void CreatePDFFromTemplate(int nTemplateID, int nPrimaryKey, PdfWriter oPDFWriter, Document oDoc,
												 string szTempFileName, Dictionary<int, Image> oBarcodes)
		{
			LD_Template oTemplate = LettersDataContext.Instance.LD_Templates.LoadByPrimaryKey(nTemplateID);

			#region Master Record

			LD_DocType oDocType = LettersDataContext.Instance.LD_DocTypes.LoadByPrimaryKey(oTemplate.DocTypeID);
			var oCon = new SqlConnection();

			switch ((LD_DocType.DocTypeEnum)oDocType.DocTypeID)
			{
				case LD_DocType.DocTypeEnum.Account:
					oCon.ConnectionString =
						DataService.Providers[SubSonicConfigHelper.SOS_CRM_PROVIDER_NAME].DefaultConnectionString;
					break;
				case LD_DocType.DocTypeEnum.Recruit:
				case LD_DocType.DocTypeEnum.RecruitRegistration:
					oCon.ConnectionString =
						DataService.Providers[SubSonicConfigHelper.SOS_HUMAN_RESOURCE_PROVIDER_NAME].DefaultConnectionString;
					break;
			}

			var oCmd = new SqlCommand(string.Format(oDocType.Query, nPrimaryKey), oCon);
			var oDt = new DataTable();
			var oDad = new SqlDataAdapter(oCmd);
			oDad.Fill(oDt);
			_masterRecord = oDt.Rows[0];

			#endregion Master Record

			using (Stream oStream = new FileStream(szTempFileName, FileMode.Create, FileAccess.Write))
			{
				var oReader = new PdfReader(oTemplate.Template);
				var oStamp = new PdfStamper(oReader, oStream);
				AcroFields oForm = oStamp.AcroFields;

				#region Populate Fields

				//Load Fields
				LD_FieldCollection fields = LettersDataContext.Instance.LD_Fields.LoadAllForTemplate(oTemplate.TemplateID);

				//populate fields
				foreach (LD_Field oField in fields)
				{
					// Check if we skip this time through
					if (oField.FieldName == null || oField.IsDeleted) continue;

					string szTest = oField.FieldName;
					LD_Field field = oField;
					//string szCmd = oField.Path.Path;
					var lEntries = new List<string>();
					int i = 0;

					lEntries.Add(EntryValue(field));
					if (lEntries[0] != "")
					{
						bool bChildExists = true;
						while (bChildExists)
						{
							if (field.SubsequentField != null)
							{
								szTest = field.FieldName;
								field = field.SubsequentField;
								lEntries.Add(EntryValue(field));
								i = i + 1;
							}
							else
							{
								bChildExists = false;
							}
						}

						var szEntry = new string[i + 1];
						for (int j = 0; j <= i; j++)
						{
							szEntry[j] = lEntries[j];
						}

						oForm.SetField(oField.FieldName, string.Format(oField.FormatString, szEntry));
					}
				}

				#endregion Populate Fields

				oStamp.FormFlattening = true;
				oStamp.Close();

				var r = new PdfReader(szTempFileName);
				PdfContentByte cb = oPDFWriter.DirectContent;

				for (int i = 1; i <= oTemplate.Pages; i++)
				{
					oDoc.NewPage();
					PdfImportedPage oPage = oPDFWriter.GetImportedPage(r, i);
					cb.AddTemplate(oPage, 0, 0);

					if (oBarcodes == null) continue;

					Image oBarcode;
					if (oBarcodes.TryGetValue(i, out oBarcode))
						oDoc.Add(oBarcode); //the key of the dictionary is the page to which the barcode belongs
				}
			}
		}

		/// <summary>
		/// Gets the db value and parses it depending on oField
		/// </summary>
		/// <param name="oField"></param>
		/// <returns></returns>
		private static string EntryValue(LD_Field oField)
		{
			string szRtn;
			try
			{
				object value = _masterRecord[oField.DBColumnName];

				if (string.IsNullOrEmpty(oField.PreprocessFormatString))
				{
					szRtn = value.ToString();
				}
				else
				{
					szRtn = string.Format("{0:" + oField.PreprocessFormatString + "}", value);
				}
			}
			catch
			{
				//for hard-coded entries like company address
				szRtn = oField.DBColumnName;
			}

			//deencrypt
			if (oField.IsEncrypted)
				szRtn = TripleDES.DecryptString(szRtn, null);

			//substrings
			if (oField.IsSubstring)
			{
				szRtn = szRtn.Replace("-", "").Replace("(", "").Replace(")", "").Replace(" ", "");
				if (oField.SubstringLength + oField.SubstringStart <= szRtn.Length)
				{
					//get substring
					szRtn = szRtn.Substring((int)oField.SubstringStart, (int)oField.SubstringLength);
				}
				else if (oField.SubstringStart <= szRtn.Length)
				{
					//get the part of the string that we can
					szRtn = szRtn.Substring((int)oField.SubstringStart, szRtn.Length - (int)oField.SubstringStart);
				}
				else
				{
					szRtn = "";
				}
			}

			//checkboxes
			if (oField.FieldName != null)
			{
				if (oField.FieldName.Contains("="))
				{
					string split = oField.FieldName.Split('=')[1];
					szRtn = szRtn == split ? "X" : "";
				}
			}

			//dates - only get short string 
			DateTime dDummy;
			if (DateTime.TryParse(szRtn, out dDummy))
			{
				szRtn = dDummy.ToString("MM/dd/yyyy");
			}
			return szRtn;
		}
	}
}
