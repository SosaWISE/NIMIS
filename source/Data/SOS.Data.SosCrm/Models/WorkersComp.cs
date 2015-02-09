using System.Runtime.Serialization;

namespace SOS.Data.SosCrm.Models
{
	[DataContract]
	public class WorkersComp
	{
		[DataMember]
		public string WrkrComp { get; set; }
		[DataMember]
		public string Dscriptn { get; set; }
	}
}
