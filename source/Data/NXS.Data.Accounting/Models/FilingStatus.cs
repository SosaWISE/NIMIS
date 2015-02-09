using System.Runtime.Serialization;

namespace NXS.Data.Accounting.Models
{
	[DataContract]
	public class FilingStatus
	{
		[DataMember]
		public string ID { get; set; }
		[DataMember]
		public string Description { get; set; }
	}
}
