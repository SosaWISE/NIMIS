using System.Collections.Generic;
using System.Runtime.Serialization;
using SOS.Data.HumanResource;

namespace NXS.Services.Wcf.AppServices.Models
{
	[DataContract]
	public class NewSeasonInfo
	{
		[DataMember]
		public List<RU_Season> ExistingSeasons { get; set; }
		[DataMember]
		public List<RU_Season> NonExistingSeasons { get; set; }
	}

}
