
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using NXS.Lib.PDFs;
using org.pdfclown.documents;
using org.pdfclown.documents.interaction.forms;
using org.pdfclown.files;

namespace NXS.FOS.DocumentServices
{
	public class PDFManager : PDFBase
	{
		#region .ctor
		public PDFManager(Dictionary<string, object> keyPairs, string pdfInputPath, string pdfOutputPath)
			: base(pdfOutputPath)
		{
			PdfInputPath = pdfInputPath;
			PdfOutputPath = pdfOutputPath;
			_keyPairs = keyPairs;
		}
		#endregion .ctor

		#region Properties

		public readonly string PdfInputPath;
		public readonly string PdfOutputPath;
		private readonly Dictionary<string, object> _keyPairs;  

		#endregion Properties

		#region Methods

		public bool TryBindData(out string message)
		{
			/** Initialize. */
			var file = new File(PdfInputPath);
			Document document = file.Document;
			Form form = document.Form;
			message = string.Format("Initializing...");

			// ** Check to see if there is a form
			if (!form.Exists())
			{
				message = string.Format("This PDF template does not have a form.");
				return false;
			}

			var i = 0;
			foreach (var field in form.Fields.Values)
			{
				var key = field.Name;
				Debug.Write(string.Format("Index: {0,3} | Key: {1,30}", i++, key));
			if (field.FullName != key)
				{
					Debug.WriteLine(field.FullName);
				}
				object value;
				if (_keyPairs.TryGetValue(key, out value))
				{
					Debug.WriteLine(" | Value: {0}", value);
					
					// ** Init
					if (value == null)
					{
						// field.Value = string.Empty;
						continue;
					}

					// ** Assign the correct value.
					var varType = value.GetType().ToString();
					switch (varType)
					{
						case "System.Decimal":
							field.Value = ((decimal) value).ToString("F2");
							break;
                        case "System.Int64":
                            field.Value = ((Int64)value).ToString(CultureInfo.InvariantCulture);
					        break;
						case "System.Int16":
							field.Value = ((Int16) value).ToString(CultureInfo.InvariantCulture);
							break;
						case "System.DateTime":
							field.Value = string.Format("{0}", value);
							break;
						default:
							if (!value.Equals(".")) // TODL:  For some reason MMR2 breaks the library.
								field.Value = value;
							break;
					}
				}
				else
				{
					Debug.WriteLine(" | was not found in the _keyPairs");
				}
			}

			// ** Serialize
			Serialize(file);

			// ** Return result.
			return true;
		}

		#endregion Methods
	}
}