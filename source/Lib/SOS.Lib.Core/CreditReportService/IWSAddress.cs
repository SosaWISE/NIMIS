namespace SOS.Lib.Core.CreditReportService
{
	public interface IWSAddress
	{
		long AddressID { get; set; }
		string AddressType { get; set; }
		string Address1 { get; set; }
		string StreetName { get; set; }
		string HouseNumber { get; set; }
		string AptNumber { get; set; }
		string Direction { get; set; }
		string StreetType { get; set; }
		string City { get; set; }
		string State { get; set; }
		string PostalCode { get; set; }
		bool IsVerified { get; set; }
		 
	}
}