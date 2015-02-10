namespace SSE.Services.ParoleeCORS.Models
{
	public class JsonParamBase
	{
		public long SessionID { get; set; }
	}

	public  class CustomerParam : JsonParamBase
	{
		public int OfficerID { get; set; }
		public string LocalizationID { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public string Title { get; set; }
		public string Department { get; set; }
		public string Salutation { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string Suffix { get; set; }
		public string OfficePhone { get; set; }
		public string MobilePhone { get; set; }
		public string HomePhone { get; set; }
		public string Pager { get; set; }
		public string Fax { get; set; }
		public string Email1 { get; set; }
		public string Email2 { get; set; }
		public string EmailPasswordReset { get; set; }
		public string SmsGateway { get; set; }
		public string SmsAddress { get; set; }
		public string SessionTimeOut { get; set; }
		public string LocalizationId { get; set; }
	}

	public class UserRead : JsonParamBase
	{
		
	}



	public class UserDelete : JsonParamBase
	{
		public long OffenderID { get; set; }
	}

}