using System.Runtime.Serialization;

namespace SOS.Data.HumanResource.Models
{
	[DataContract]
	public class UserSearchInfo
	{
		[DataMember]
		public bool SearchLike { get; set; }
		[DataMember]
		public int? Top { get; set; }

		[DataMember]
		public int? UserID { get; set; }
		[DataMember]
		public int? RecruitID { get; set; }
		[DataMember]
		public int? SeasonID { get; set; }
		[DataMember]
		public short? UserTypeID { get; set; }

		[DataMember]
		public string FirstName { get; set; }
		[DataMember]
		public string LastName { get; set; }
		[DataMember]
		public string CompanyID { get; set; }
		[DataMember]
		public string SSN { get; set; }
		[DataMember]
		public string CellPhone { get; set; }
		[DataMember]
		public string HomePhone { get; set; }
		[DataMember]
		public string Email { get; set; }
		[DataMember]
		public string UserName { get; set; }
	}
}
