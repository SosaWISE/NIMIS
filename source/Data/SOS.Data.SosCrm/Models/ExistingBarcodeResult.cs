using System;
using System.Runtime.Serialization;

namespace SOS.Data.SosCrm.Models
{
	[DataContract]
	public class ExistingBarcodeResult
	{
		[DataMember]
		public Guid QueryKey { get; set; }
		[DataMember]
		public int RecruitID { get; set; }
		[DataMember]
		public string RecruitsCurrentBarcode { get; set; }
		[DataMember]
		public int? ExistingBarcodeRecruitID { get; set; }
		[DataMember]
		public string ExistingBarcode { get; set; }
	}
}
