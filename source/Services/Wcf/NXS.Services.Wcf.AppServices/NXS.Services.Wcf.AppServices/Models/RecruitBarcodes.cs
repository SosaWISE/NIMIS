
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using SOS.Data.SosCrm;

namespace NXS.Services.Wcf.AppServices.Models
{
	[DataContract]
	public class RecruitBarcodes
	{
		[DataMember]
		public int RecruitID { get; set; }

		[DataMember]
		public Dictionary<BE_DocType.DocTypeEnum, string> BarcodeDocumentDictionary { get; set; }

		public Dictionary<BE_DocType.DocTypeEnum, string> GetMissingOrDifferent(RecruitBarcodes recruitBarcodes)
		{
			var dict = new Dictionary<BE_DocType.DocTypeEnum, string>();
			foreach (var kvp in BarcodeDocumentDictionary)
			{
				if (!recruitBarcodes.BarcodeDocumentDictionary.ContainsKey(kvp.Key)
					||
					string.Compare(recruitBarcodes.BarcodeDocumentDictionary[kvp.Key], kvp.Value,
								   StringComparison.InvariantCultureIgnoreCase) != 0)
				{
					dict.Add(kvp.Key, kvp.Value);
				}
			}

			return dict;
		}

		public void Set(BE_DocType.DocTypeEnum docType, string barcode)
		{
			if (BarcodeDocumentDictionary.ContainsKey(docType))
			{
				BarcodeDocumentDictionary[docType] = barcode;
			}
			else
			{
				BarcodeDocumentDictionary.Add(docType, barcode);
			}
		}
	}

}
