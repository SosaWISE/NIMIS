using System;
using System.Runtime.Serialization;

namespace SOS.Data.HumanResource.Models
{
	[DataContract]
	public class ScannedDocument
	{
		[DataMember]
		public int DocumentId { get; set; }
		[DataMember]
		public DateTime Created { get; set; }
		[DataMember]
		public DateTime Modified { get; set; }
		[DataMember]
		public int ModifiedBy { get; set; }
		[DataMember]
		public string DocumentType { get; set; }
		[DataMember]
		public string DocPath { get; set; }
		[DataMember]
		public string Filename { get; set; }
		[DataMember]
		public string ScannedBarcode { get; set; }
	}
}
