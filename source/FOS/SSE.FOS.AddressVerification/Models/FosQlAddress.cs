using SSE.FOS.AddressVerification.Interfaces;

namespace SSE.FOS.AddressVerification.Models
{
	public class FosQlAddress : IFosQlAddress
	{
		public long AddressID { get; private set; }
		public string AddressLine1 { get; set; }
		public string AddressLine2 { get; set; }
		public string StreetNumber { get; set; }
		public string StreetName { get; set; }
		public string City { get; set; }
		public string StateId { get; set; }
		public string PostalCode { get; set; }
		public string County { get; set; }
		public string PreDirectional { get; set; }
		public string PostDirectional { get; set; }
		public string StreetType { get; set; }
		public string Extension { get; set; }
		public string ExtensionNumber { get; set; }
		public string CarrierRoute { get; set; }
		public string DPVResponse { get; set; }
		public int TimeZoneId { get; set; }
		public string Phone { get; set; }
	}
}
