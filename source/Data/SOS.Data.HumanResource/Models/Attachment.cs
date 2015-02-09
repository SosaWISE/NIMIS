using System.Runtime.Serialization;

namespace SOS.Data.HumanResource.Models
{
	public class Attachment
	{
		[DataMember]
		public string FileName { get; set; }
		[DataMember]
		public byte[] File { get; set; }
	}
}
