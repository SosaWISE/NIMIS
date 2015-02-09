using System.Runtime.Serialization;

namespace NXS.Data.Accounting.Models
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
