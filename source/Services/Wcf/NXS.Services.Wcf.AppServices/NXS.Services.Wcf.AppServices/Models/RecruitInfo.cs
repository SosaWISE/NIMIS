using System.Runtime.Serialization;
using SOS.Data.HumanResource;

namespace NXS.Services.Wcf.AppServices.Models
{
	[DataContract]
	public class RecruitInfo
	{
		public RecruitInfo(HumanResourceDataContext oResourceDB, RU_Recruit recruit)
		{
			Recruit = recruit;
			Address = recruit.CurrentAddress;
		}

		[DataMember]
		public RU_Recruit Recruit { get; set; }

		[DataMember]
		public RU_RecruitAddress Address { get; set; }
	}
}
