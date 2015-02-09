using System.Runtime.Serialization;

namespace SOS.Data.HumanResource.Models
{
	[DataContract]
	public class OfficeSearchInfo
	{
		[DataMember]
		public bool SearchLike { get; set; }
		[DataMember]
		public int? Top { get; set; }

		[DataMember]
		public int? TeamLocationID { get; set; }

		[DataMember]
		public string OfficeName { get; set; }

		[DataMember]
		public int? SeasonID { get; set; }

		[DataMember]
		public string SeasonName { get; set; }

		[DataMember]
		public int? MarketID { get; set; }

		[DataMember]
		public string MarketName { get; set; }

		[DataMember]
		public string City { get; set; }

		[DataMember]
		public string StateAB { get; set; }

		[DataMember]
		public int? TimeZoneID { get; set; }

		[DataMember]
		public string TimeZoneName { get; set; }
	}

}
