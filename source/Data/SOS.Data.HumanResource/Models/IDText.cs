using System.Runtime.Serialization;

namespace SOS.Data.HumanResource.Models
{
	[DataContract]
	public class IDText
	{
		[DataMember]
		public int ID { get; set; }
		[DataMember]
		public string Text { get; set; }
	}
}
