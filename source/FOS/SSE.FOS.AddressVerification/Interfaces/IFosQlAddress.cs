namespace SSE.FOS.AddressVerification.Interfaces
{
	public interface IFosQlAddress
	{
		long AddressID { get; }
		string AddressLine1 { get; set; }
		string AddressLine2 { get; set; }
		string StreetNumber { get; set; }
		string StreetName { get; set; }
		string City { get; set; }
		string StateId { get; set; }
		string PostalCode { get; set; }
		string County { get; set; }
		string PreDirectional { get; set; }
		string PostDirectional { get; set; }
		string StreetType { get; set; }
		string Extension { get; set; }
		string ExtensionNumber { get; set; }
		string CarrierRoute { get; set; }
		string DPVResponse { get; set; }
int TimeZoneId { get; set; }
		string Phone { get; set; }
	}
}