using System;
using SOS.FunctionalServices.Contracts.Models.Parolee;

namespace SSE.Services.ParoleeCORS.Models
{
	public class FnsParoleeOfficer : IFnsParoleeOfficer
	{
		#region .ctor
		public FnsParoleeOfficer() {}
		public FnsParoleeOfficer(SosOfficer oItem)
		{
			OfficerID = oItem.OfficerID;
			LocalizationId = oItem.LocalizationId;
			UserName = oItem.UserName;
			Password = oItem.Password;
			Title = oItem.Title;
			Department = oItem.Department;
			Salutation = oItem.Salutation;
			FirstName = oItem.FirstName;
			MiddleName = oItem.MiddleName;
			LastName = oItem.LastName;
			Suffix = oItem.Suffix;
			OfficePhone = oItem.OfficePhone;
			MobilePhone = oItem.MobilePhone;
			HomePhone = oItem.HomePhone;
			Pager = oItem.Pager;
			Fax = oItem.Fax;
			Email1 = oItem.Email1;
			Email2 = oItem.Email2;
			EmailPasswordReset = oItem.EmailPasswordReset;
			SmsGateway = oItem.SmsGateway;
			SmsAddress = oItem.SmsAddress;
			SessionTimeOut = oItem.SessionTimeOut;
		}

		#endregion .ctor

		#region Properties
		public int OfficerID { get; set; }
		public string LocalizationId { get; set; }
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
	    public bool IsActive { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }

		#endregion Properties
	}
}
