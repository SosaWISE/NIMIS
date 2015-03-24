
using System;

namespace SSE.Services.CmsCORS.Models
{
	public class JsonParamBase
	{
		public long SessionID { get; set; }
		public string SalesRepId { get; set; }
		public string TechnicnId { get; set; }
		public int SeasonId { get; set; }
		public int TeamLocationId { get; set; }
	}

	#region Address

	public class AddressParam : JsonParamBase
	{
		public long AddressId { get; set; }
		public int DealerId { get; set; }
		public int TimeZoneId { get; set; }
		public string StreetAddress { get; set; }
		public string StreetAddress2 { get; set; }
		public string City { get; set; }
		public string StateId { get; set; }
		public string PostalCode { get; set; }
		public string PhoneNumber { get; set; }
	}

	#endregion Address

	#region SalesRepRead

	public class SalesRepParam : JsonParamBase
	{
	}
	#endregion SalesRepRead

#region Run Credit

	public class LeadParam : JsonParamBase
	{
		public bool CreateMasterLead { get; set; }

		public long LeadID { get; set; }
		public long AddressId { get; set; }
		public string CustomerTypeId { get; set; }
		public long CustomerMasterFileId { get; set; }
		public int DealerId { get; set; }
		public string LocalizationId { get; set; }
		//public int TeamLocationId { get; set; }
		//public int SeasonId { get; set; }
		//public string SalesRepId { get; set; }
		public int LeadSourceId { get; set; }
		public int LeadDispositionId { get; set; }
		public DateTime? LeadDispositionDateChange { get; set; }
		public string Salutation { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string Suffix { get; set; }
		public string Gender { get; set; }
		public string SSN { get; set; }
		public DateTime? DOB { get; set; }
		public string DL { get; set; }
		public string DLStateID { get; set; }
		public string Email { get; set; }
		public string PhoneHome { get; set; }
		public string PhoneWork { get; set; }
		public string PhoneMobile { get; set; }
		public string ProductSkwId { get; set; }
	}
#endregion Run Credit
}