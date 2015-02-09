using System.Runtime.Serialization;

namespace SSE.Services.CmsCORS.Models
{
	public class QlHomeOwner
	{
		#region Address properties

		[DataMember]
		public long AddressId { get; set; }

		[DataMember]
		public int DealerId { get; set; }

		[DataMember]
		public string StreetAddress { get; set; }

		[DataMember]
		public string StreetAddress2 { get; set; }

		[DataMember]
		public string City { get; set; }

		[DataMember]
		public string State { get; set; }

		[DataMember]
		public string PostalCode { get; set; }

		[DataMember]
		public string PhoneNumber { get; set; }

		#endregion Address properties
	}
}