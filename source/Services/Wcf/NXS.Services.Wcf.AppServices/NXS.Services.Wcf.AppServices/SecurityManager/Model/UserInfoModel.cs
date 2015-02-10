using System;
using System.Collections.Generic;
using SOS.Lib.Util.ActiveDirectory;

namespace NXS.Services.Wcf.AppServices.SecurityManager.Model
{
	public class UserInfoModel
	{
		#region Fields

		public string FullName { get; set; } //Name
		public string DisplayName { get; set; } //Name
		public string DistinguishedName { get; set; }
		public string Email { get; set; } //mail
		public string Fax { get; set; } //FacsimileTelephoneNumber
		public string FirstName { get; set; } //givenName
		public Guid GUID { get; set; } //Guid
		public List<ADGroup> Groups { get; set; }
		public string HomePhone { get; set; }
		public bool IsAccountActive { get; set; } //userAccountControl
		public string LastName { get; set; } //sn
		public string MailingAddress { get; set; } //StreetAddress
		public string MiddleInitial { get; set; } //initials
		public string Mobile { get; set; }
		public string OfficePhone { get; set; } //TelephoneNumber
		public string Password { get; set; }
		public string PostalAddress { get; set; }
		public string ResidentialAddress { get; set; } //HomePostalAddress
		public string Ssid { get; set; } //NativeGuid
		public string Title { get; set; }
		public string Url { get; set; }
		public string UserName { get; set; } //sAMAccountName
		public string UserPrincipalName { get; set; } //userPrincipalName (e.g. user@domain.local)
		public ADUtility.LoginResult LoginResult { get; set; }

		#endregion Fields
	}
}