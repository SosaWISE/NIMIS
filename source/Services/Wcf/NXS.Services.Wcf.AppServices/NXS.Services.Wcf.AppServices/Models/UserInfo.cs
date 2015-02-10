using System.Collections.Generic;
using System.Runtime.Serialization;
using NXS.Data.Licensing;
using SOS.Data.HumanResource;

namespace NXS.Services.Wcf.AppServices.Models
{
	[DataContract]
	public class UserInfo
	{
		[DataMember]
		public RU_User User { get; set; }
		[DataMember]
		public List<LM_Note> Notes { get; set; }
	}
}
