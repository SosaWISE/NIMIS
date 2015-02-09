namespace SOS.FunctionalServices.Contracts.Models.Parolee
{
	public interface IFnsParoleeOfficer
	{
		int OfficerID { get; set; }
		string LocalizationId { get; set; }
		string UserName { get; set; }
		string Password { get; set; }
		string Title { get; set; }
		string Department { get; set; }
		string Salutation { get; set; }
		string FirstName { get; set; }
		string MiddleName { get; set; }
		string LastName { get; set; }
		string Suffix { get; set; }
		string OfficePhone { get; set; }
		string MobilePhone { get; set; }
		string HomePhone { get; set; }
		string Pager { get; set; }
		string Fax { get; set; }
		string Email1 { get; set; }
		string Email2 { get; set; }
		string EmailPasswordReset { get; set; }
		string SmsGateway { get; set; }
		string SmsAddress { get; set; }
		string SessionTimeOut { get; set; }

	}
}

