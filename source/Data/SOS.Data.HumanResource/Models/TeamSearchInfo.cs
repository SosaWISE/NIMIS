using System.Runtime.Serialization;

namespace SOS.Data.HumanResource.Models
{
	[DataContract]
	public class TeamSearchInfo
	{
		[DataMember]
		public bool SearchLike { get; set; }
		[DataMember]
		public int? Top { get; set; }

		[DataMember]
		public int? TeamID { get; set; }

		[DataMember]
		public string TeamName { get; set; }

		[DataMember]
		public string OfficeName { get; set; }

		[DataMember]
		public int? SeasonID { get; set; }

		[DataMember]
		public string SeasonName { get; set; }

		[DataMember]
		public string City { get; set; }

		[DataMember]
		public string StateAB { get; set; }

		[DataMember]
		public int? RoleLocationID { get; set; }
	}
}
