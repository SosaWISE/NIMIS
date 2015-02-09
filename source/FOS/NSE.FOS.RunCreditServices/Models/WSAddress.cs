using SOS.Data.SosCrm;
using SOS.Lib.Core.CreditReportService;

namespace NSE.FOS.RunCreditServices.Models
{
	public class WSAddress : IWSAddress
	{
		#region .ctor

		public WSAddress(QL_Address address)
		{
			AddressID = address.AddressID;
			AddressType = address.AddressTypeId;
			Address1 = address.StreetAddress;
			StreetName = address.StreetName;
			HouseNumber = address.StreetNumber;
		//	AptNumber = address.St
			Direction = address.PostDirectional;
			StreetType = address.StreetType;
			City = address.City;
			State = address.State.StateAB;
			PostalCode = address.PostalCode;
		}
		#endregion .ctor

		#region Properties
		public long AddressID { get; set; }
		public string AddressType { get; set; }
		public string Address1 { get; set; }
		public string StreetName { get; set; }
		public string HouseNumber { get; set; }
		public string AptNumber { get; set; }
		public string Direction { get; set; }
		public string StreetType { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string PostalCode { get; set; }
		public bool IsVerified { get; set; }
		#endregion Properties
	}
}
