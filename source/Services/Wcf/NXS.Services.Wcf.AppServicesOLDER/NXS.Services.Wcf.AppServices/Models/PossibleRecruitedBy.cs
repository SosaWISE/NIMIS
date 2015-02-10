using System.Runtime.Serialization;

namespace NXS.Services.Wcf.AppServices.Models
{
	[DataContract]
	public class PossibleRecruitedBy
	{
		public PossibleRecruitedBy(int userID, string fullName)
		{
			UserID = userID;
			FullName = fullName;
		}

		[DataMember]
		public int UserID { get; set; }

		[DataMember]
		public string FullName { get; set; }
	}
}
