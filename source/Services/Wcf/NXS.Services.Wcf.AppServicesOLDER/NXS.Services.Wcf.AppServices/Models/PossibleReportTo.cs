using System.Runtime.Serialization;

namespace NXS.Services.Wcf.AppServices.Models
{
	[DataContract]
	public class PossibleReportTo
	{
		[DataMember]
		public int UserID { get; set; }
		[DataMember]
		public string FullName { get; set; }
		[DataMember]
		public int RecruitID { get; set; }
		[DataMember]
		public string FullDescription { get; set; }
	}
}
