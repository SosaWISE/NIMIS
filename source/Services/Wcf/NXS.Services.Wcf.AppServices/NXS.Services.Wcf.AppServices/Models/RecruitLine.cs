using System.Runtime.Serialization;
using SOS.Data.HumanResource;

namespace NXS.Services.Wcf.AppServices.Models
{
	[DataContract]
	public class RecruitLine
	{
		[DataMember]
		public RU_Season Season { get; set; }
		[DataMember]
		public RU_RoleLocation RoleLocation { get; set; }

		[DataMember]
		public RecruitUserView NationalRegional { get; set; }
		[DataMember]
		public RecruitUserView Regional { get; set; }
		[DataMember]
		public RU_TeamLocation Office { get; set; }
		[DataMember]
		public RU_Team Team { get; set; }
		[DataMember]
		public RecruitUserView Manager { get; set; }
		[DataMember]
		public RecruitUserView Rep { get; set; }
	}
}
