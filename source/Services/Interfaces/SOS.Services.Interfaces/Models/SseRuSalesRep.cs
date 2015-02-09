using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models
{
	public class SseRuSalesRep
	{
		[DataMember]
		public string GPEmployeeID { get; set; }

		[DataMember]
		public int SeasonId { get; set; }

		[DataMember]
		public string SeasonName { get; set; }

		[DataMember]
		public int TeamLocationId { get; set; }

		[DataMember]
		public string TeamLocation { get; set; }

		[DataMember]
		public string FirstName { get; set; }

		[DataMember]
		public string LastName { get; set; }

		[DataMember]
		public DateTime? BirthDate { get; set; }

		[DataMember]
		public string UserName { get; set; }

		[DataMember]
		public int Sex { get; set; }

		[DataMember]
		public string PhoneCell { get; set; }

		[DataMember]
		public string PhoneHome { get; set; }

		[DataMember]
		public string Email { get; set; }
		
	}
}