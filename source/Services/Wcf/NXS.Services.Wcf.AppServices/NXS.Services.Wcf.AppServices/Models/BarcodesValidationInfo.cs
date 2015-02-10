using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using SOS.Data.SosCrm;

namespace NXS.Services.Wcf.AppServices.Models
{
	[DataContract]
	public class BarcodesValidationInfo
	{
		[DataMember]
		public Dictionary<BE_PrefixDocument.PrefixDocEnum, string> PrefixValidationDictionary { get; set; }
	}
}
