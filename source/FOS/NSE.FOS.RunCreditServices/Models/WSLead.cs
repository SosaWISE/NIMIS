using SOS.Data.SosCrm;
using SOS.Lib.Core.CreditReportService;

namespace NSE.FOS.RunCreditServices.Models
{
	public class WSLead : IWSLead
	{
		#region .ctor

		public WSLead(QL_Lead lead)
		{
			LeadID = lead.LeadID;
			Prefix = lead.Salutation;
			FirstName = lead.FirstName;
			MiddleName = lead.MiddleName;
			LastName = lead.LastName;
			Suffix = lead.Suffix;
			SocialSecurity = (lead.SSN == null) ? null : SOS.Lib.Util.Cryptography.TripleDES.DecryptString(lead.SSN, null);
			DOB = lead.DOB.ToString();
			Generation = lead.Suffix;
			HomePhone = lead.PhoneHome;
		}
		#endregion .ctor

		#region Properties
		public long LeadID { get; set; }
		public string Prefix { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string Suffix { get; set; }
		public string SocialSecurity { get; set; }
		public string DOB { get; set; }
		public string Generation { get; set; }
		public string HomePhone { get; set; }
		#endregion Properties
	}
}