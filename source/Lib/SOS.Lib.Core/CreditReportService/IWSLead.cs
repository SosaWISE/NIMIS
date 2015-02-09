namespace SOS.Lib.Core.CreditReportService
{
	public interface IWSLead
	{
		long LeadID { get; set; }
		string Prefix { get; set; }
		string FirstName { get; set; }
		string MiddleName { get; set; }
		string LastName { get; set; }
		string Suffix { get; set; }
		string SocialSecurity { get; set; }
		string DOB { get; set; }
		string Generation { get; set; }
		string HomePhone { get; set; }
	}
}