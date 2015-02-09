using System.Runtime.Serialization;

namespace SOS.Data.HumanResource.Models
{
	[DataContract]
	public class SeasonsMap
	{
		[DataMember]
		public int FromID { get; set; }
		[DataMember]
		public int ToID { get; set; }
	}
}
