using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using NXS.Lib.PDFs.Interfaces;
using org.pdfclown.documents;
using org.pdfclown.documents.interaction.forms;
using org.pdfclown.files;

namespace NXS.Lib.PDFs
{
    public class AMA : PDFBase
	{
		#region .ctor

	    public AMA(IAMAInfo info, string pdfInputPath, string pdfOutputPath) : base(pdfOutputPath)
	    {
		    Info = info;
		    PdfInputPath = pdfInputPath;
		    PdfOutputPath = pdfOutputPath;
			_keyPairs = new Dictionary<string, object>();

			BuildKeyPairs();
	    }

	    public AMA(Dictionary<string, object> keyPairs, string pdfInputPath, string pdfOutputPath) : base(pdfOutputPath)
	    {
			PdfInputPath = pdfInputPath;
			PdfOutputPath = pdfOutputPath;
		    _keyPairs = keyPairs;
	    }

	    #endregion .ctor

		#region Propertie

	    public readonly IAMAInfo Info;
	    public readonly string PdfInputPath;
	    public readonly string PdfOutputPath;

		//private struct KeyPair
		//{
		//	public string Key { get; set; }
		//	public object Value { get; set; }
		//}

		//private readonly List<KeyPair> _keyPairs;
		private readonly Dictionary<string, object> _keyPairs;  

	    #endregion Propertie

		#region Methods

	    private void BuildKeyPairs()
	    {
			// ** Init
		    var firstName = new StringBuilder();
		    var lastName = new StringBuilder();

			// ** Build Owner FirstName **
			if (!string.IsNullOrEmpty(Info.OwnerPreName)) firstName.AppendFormat("{0} ", Info.OwnerPreName);
			// _keyPairs.Add("OwnerFirstName", Info.OwnerFirstName);
		    firstName.Append(Info.OwnerFirstName);
			if(!string.IsNullOrEmpty(Info.OwnerMiddleName)) firstName.AppendFormat(" {0}", Info.OwnerMiddleName);
			//_keyPairs.Add("OwnerLastName", Info.OwnerLastName);
		    lastName.Append(Info.OwnerLastName);
		    if (!string.IsNullOrEmpty(Info.OwnerPostName)) lastName.AppendFormat(" {0}", Info.OwnerPostName);
			// * Assign values to KeyPair values
			_keyPairs.Add("OwnerFirstName", firstName.ToString());
			_keyPairs.Add("OwnerLastName", lastName.ToString());

			// * Clear
		    firstName.Clear();
		    lastName.Clear();

			// ** Build Spouse FirstName
		    if (!string.IsNullOrEmpty(Info.SpousePreName)) firstName.AppendFormat("{0} ", Info.SpousePreName);
		    firstName.Append(Info.SpouseFirstName);
		    if (!string.IsNullOrEmpty(Info.SpouseMiddleName)) firstName.AppendFormat(" {0}", Info.SpouseMiddleName);
		    lastName.Append(Info.SpouseLastName);
			if(!string.IsNullOrEmpty(Info.SpousePostName)) lastName.AppendFormat(" {0}", Info.SpousePostName);
			// * Assign values to KeyPair values
			_keyPairs.Add("SpouseFirstName", firstName.ToString());
			_keyPairs.Add("SpouseLastName", lastName.ToString());

			// ** Set Effective Date
		    var mm = Info.EffectiveDate.Month.ToString("D2");
			_keyPairs.Add("EffDateMM", mm);

		    var dd = Info.EffectiveDate.Day.ToString("D2");
			_keyPairs.Add("EffDateDD", dd);

		    var yy = Info.EffectiveDate.Year.ToString("D2");
			_keyPairs.Add("EffDateYY", yy);

			if (!string.IsNullOrEmpty(Info.NameOfBusiness)) _keyPairs.Add("NameOfBusiness", Info.NameOfBusiness);

			//// ** Premise Address
			//_keyPairs.Add("PremiseAddress", Info.PremiseAddress);
			//_keyPairs.Add("PremiseCity", Info.PremiseCity);
			//_keyPairs.Add("PremiseState", Info.PremiseState);
			//_keyPairs.Add("PremiseZip", Info.PremiseZip);
			//_keyPairs.Add("PremiseZip4", Info.PremiseZip4);
			//if (!string.IsNullOrEmpty(Info.PremiseZip4)) _keyPairs.Add("PremiseZip4", Info.PremiseZip4);
			//// ** Billing Address
			//if (!string.IsNullOrEmpty(Info.BillingAddress)) _keyPairs.Add("BillingAddress", Info.BillingAddress);
			//if (!string.IsNullOrEmpty(Info.BillingCity)) _keyPairs.Add("BillingCity", Info.BillingCity);
			//if (!string.IsNullOrEmpty(Info.BillingState)) _keyPairs.Add("BillingState", Info.BillingState);
			//if (!string.IsNullOrEmpty(Info.BillingZip)) _keyPairs.Add("BillingZip", Info.BillingZip);
			//if (!string.IsNullOrEmpty(Info.BillingZip4)) _keyPairs.Add("BillingZip4", Info.BillingZip4);

			//// ** SSOwner
			//if (!string.IsNullOrEmpty(Info.SSO4)) _keyPairs.Add("SSO4", Info.SSO4);

			//// ** SS Spouse / Resident
			//if (!string.IsNullOrEmpty(Info.SSR4)) _keyPairs.Add("SSO4", Info.SSR4);

			//// ** Extended Service Option
			//_keyPairs.Add(Info.ExtendedServiceOption ? "ExtendedServiceOption_Yes" : "ExtendedServiceOption_No", true);
	    }

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
			
			foreach (var field in form.Fields.Values)
			{
				var key = field.Name;
				Debug.WriteLine("Key: {0}", key);
				if (field.FullName != key)
				{
					Debug.WriteLine(field.FullName);
				}
				object value;
				if (_keyPairs.TryGetValue(key, out value))
				{
					field.Value = value;
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
