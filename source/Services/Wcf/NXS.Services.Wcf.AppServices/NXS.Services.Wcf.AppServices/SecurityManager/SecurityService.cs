using System;
using NXS.Services.Wcf.AppServices.SecurityManager.Model;
using SOS.Lib.Util.ActiveDirectory;

namespace NXS.Services.Wcf.AppServices.SecurityManager
{
	public class SecurityService : ISecurityService
	{
		/// <summary>
		/// Given the authentication credentials it will return a Model of user information
		/// </summary>
		/// <param name="szUsername">string</param>
		/// <param name="szPassword">string</param>
		/// <param name="szDomainName">string</param>
		/// <returns>UserInfoModel</returns>
		public UserInfoModel Authenticate(string szUsername, string szPassword, string szDomainName)
		{
			// Locals
			UserInfoModel oUserModel;
			var oResult = ADManager.Login(szUsername, szPassword, szDomainName);

			switch(oResult)
			{
				case ADUtility.LoginResult.LOGIN_OK:
					var oADUser = ADManager.Instance.LoadUser(szUsername, szPassword);
					oUserModel = new UserInfoModel
			                 	{
									FullName = oADUser.FullName
			                 		, DisplayName = oADUser.DisplayName
									, DistinguishedName = oADUser.DistinguishedName
									, Email = oADUser.Email
									, Fax = oADUser.Fax
									, FirstName = oADUser.FirstName
									, GUID = oADUser.GUID
									, Groups = oADUser.Groups
									, HomePhone = oADUser.HomePhone
									, IsAccountActive = oADUser.IsAccountActive
									, LastName = oADUser.LastName
									, MailingAddress = oADUser.MailingAddress
									, MiddleInitial = oADUser.MiddleInitial
									, Mobile = oADUser.Mobile
									, OfficePhone = oADUser.OfficePhone
									, Password = oADUser.Password
									, PostalAddress = oADUser.PostalAddress
									, ResidentialAddress = oADUser.ResidentialAddress
									, Ssid = oADUser.SSID
									, Title = oADUser.Title
									, Url = oADUser.Url
									, UserName = oADUser.UserName
									, UserPrincipalName = oADUser.UserPrincipalName
			                 	};
					break;
				default:
					oUserModel = new UserInfoModel
					             	{
					             		UserName = szUsername
										, LoginResult = oResult
					             	};
					break;
			}

			return oUserModel;
		}

		public UserInfoModel Impersonate(string szUsername)
		{
			throw new NotImplementedException();
		}
	}
}