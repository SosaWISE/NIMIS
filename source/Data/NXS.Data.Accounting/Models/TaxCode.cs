using System.Runtime.Serialization;

namespace NXS.Data.Accounting.Models	
{
	[DataContract]
	public class TaxCode
	{
		[DataMember]
		public string ClassID { get; set; }
		[DataMember]
		public string Description { get; set; }
		[DataMember]
		public string Code { get; set; }
		[DataMember]
		public decimal Rate { get; set; }
	}
}