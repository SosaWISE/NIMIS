using System;
using System.Collections.Generic;
using System.Globalization;
using SOS.Data.SosCrm;
using SOS.Data.SosCrm.ControllerExtensions;

namespace NXS.FOS.DocumentServices
{
    public class DocumentManager
	{
		#region .ctor

	    public DocumentManager()
	    {
		    KeyValues = new Dictionary<string, object>();
	    }
		#endregion .ctor

		#region Properties

		public Dictionary<string, DocumentFieldAndValue> FieldsInfo { get; private set; }
		public Dictionary<string, object> KeyValues { get; private set; } 
		#endregion Properties

		#region Methods

	    public bool InitDocumentTry(string documentName, long accountId, out string message)
	    {
			// ** Initialize
		    message = string.Empty;

			// Get FieldsInfo
		    FieldsInfo = SosCrmDataContext.Instance.BX_DocumentFields.GetDictByBarcodeTypeId(documentName, accountId);

			// ** Check result
		    if (FieldsInfo == null)
			    message =
				    string.Format("The document name or BarcodeType '{0}' was either not found or some other major error occurred.", documentName);

		    try
		    {
				// ** Setup the KeyFields that are Arrays
				// ReSharper disable once PossibleNullReferenceException
				foreach (KeyValuePair<string, DocumentFieldAndValue> documentFieldAndValue in FieldsInfo)
				{
					// ** Get value
					var valueToBind = GetValueFromDocFieldAndValueObject(documentFieldAndValue.Value);

					if (!string.IsNullOrEmpty(documentFieldAndValue.Value.DocumentField.SubDataSource))
					{
						if (!SosCrmDataContext.Instance.BX_DocumentFields.GetSubDataSource(KeyValues,
							documentFieldAndValue.Value.DocumentField.SubDataSource,
							accountId,
							GetValueFromDocFieldAndValueObject,
							out message))
						{
							return false;
						}
					}
					else if (documentFieldAndValue.Value.DocumentField.Slots > 1)
					{
						if (valueToBind == null) valueToBind = string.Empty;

						BindArrayToDict(KeyValues,
							documentFieldAndValue.Value.DocumentField.DataType.Equals("decimal")
								? string.Format("{0:F2}", valueToBind).ToCharArray()
								: valueToBind.ToString().ToCharArray(),
							documentFieldAndValue.Key,
							documentFieldAndValue.Value.DocumentField.Slots,
							documentFieldAndValue.Value.DocumentField.DefaultValue,
							documentFieldAndValue.Value.DocumentField.Justification,
							documentFieldAndValue.Value.DocumentField.DataType.Equals("int") || documentFieldAndValue.Value.DocumentField.DataType.Equals("decimal"));
					}
					else
					{
						KeyValues.Add(documentFieldAndValue.Key, valueToBind);
					}
				}

		    }
		    catch (Exception ex)
		    {
			    message = string.Format("The following exception was thrown while initializing a document template: {0}",
				    ex.Message);
			    FieldsInfo = null;
		    }

			// ** Return result.
			return FieldsInfo != null;
		}

	    private static object GetValueFromDocFieldAndValueObject(DocumentFieldAndValue docFieldAndValue)
	    {
			// ** Init
			object result = docFieldAndValue.Value;

			// ** Check to see if value is empty or null
			if (docFieldAndValue.Value == null || string.IsNullOrEmpty(docFieldAndValue.Value.ToString()))
				result = docFieldAndValue.DocumentField.DefaultValue;
		    if (result == null) return string.Empty;

			// ** Check for date formating
		    switch (docFieldAndValue.DocumentField.DataType)
		    {
				case "longDate":
					result = string.Format("{0:dddd, MMMM d, yyyy}", result);
				    break;
			    case "shortDate":
					result = string.Format("{0:ddd, MMM d, yyyy}", result);
				    break;
		    }

		    // ** Check for Encryption
		    if (docFieldAndValue.DocumentField.IsEncrypted)
			    result = SOS.Lib.Util.Cryptography.TripleDES.DecryptString(result.ToString());

			// ** Check for a substring
		    if (!string.IsNullOrEmpty(docFieldAndValue.DocumentField.SubString))
		    {
			    var subStrParams = docFieldAndValue.DocumentField.SubString.Split(',');
			    var startIndex = int.Parse(subStrParams[0]);
			    var length = int.Parse(subStrParams[1]);
			    result = result.ToString().Substring(startIndex, length);
		    }

			// ** Check for a mask
		    if (!string.IsNullOrEmpty(docFieldAndValue.DocumentField.Mask)
				&& !string.IsNullOrEmpty(result.ToString()))
		    {
// ReSharper disable once PossibleNullReferenceException
			    result = Int64.Parse(result.ToString()).ToString(docFieldAndValue.DocumentField.Mask);
		    } 
			
			// ** Return Result
		    return result;
	    }

	    public static void BindArrayToDict(Dictionary<string, object> keyPairs, char[] arrayOfChars, string keyWord, int numberOfSlots, object defaultValue = null, string justification = "LEFT", bool isNumeric = false)
	    {
			// ** Check for justification
		    if (justification.Equals("Left"))
		    {
				// ** Check if this is a numeric type or a string
			    if (isNumeric)
			    {
					// ** Loop through and add dictionary
					for (int i = numberOfSlots - 1; i >=0; i--)
					{
						var charIndex = (numberOfSlots - 1) - i;
						keyPairs.Add(string.Format("{0}{1}", keyWord, i), charIndex >= 0 ? arrayOfChars[charIndex].ToString(CultureInfo.InvariantCulture) : defaultValue);
					}
				}
				else
				{
					// ** Loop through and add dictionary
					for (int i = 0; i < numberOfSlots; i++)
					{
						keyPairs.Add(string.Format("{0}{1}", keyWord, i), i < arrayOfChars.Length ? arrayOfChars[i].ToString(CultureInfo.InvariantCulture) : defaultValue);
					}
				}
		    }
		    else
		    {
				// ** CHeck that the number of slots is greater than or equal to the length of the array.
				if (arrayOfChars.Length > numberOfSlots) 
					throw new Exception(string.Format("The number of characters ({0}) is greater than the number of slots ({1}).", arrayOfChars.Length, numberOfSlots));

				// ** Check if this is a numeric type or a string.
			    if (isNumeric)
			    {
					for (int i = numberOfSlots - 1; i >= 0; i--)
					{
						// ** Init 
						int pos = arrayOfChars.Length - i - 1;
//						int pos = i - (numberOfSlots - arrayOfChars.Length);
						keyPairs.Add(string.Format("{0}{1}", keyWord, i), pos < 0 ? defaultValue : arrayOfChars[pos].ToString(CultureInfo.InvariantCulture));
					}
			    }
				else
				{
					for (int i = 0; i < numberOfSlots; i++)
					{
						int pos = i - (numberOfSlots - arrayOfChars.Length);
						keyPairs.Add(string.Format("{0}{1}", keyWord, i), pos < 0 ? defaultValue : arrayOfChars[pos].ToString(CultureInfo.InvariantCulture));
					}
				}
		    }
	    }



	    #endregion Methods
	}
}