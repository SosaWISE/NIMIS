// Wrapper API for using Microsoft Active Directory Services version 1.0
// Copyright (c) 2004-2005
// by Syed Adnan Ahmed ( adnanahmed235@yahoo.com )
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
// the rights to use, copy, modify, merge, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions 
// of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.
using System;
using System.DirectoryServices;
using System.Text;
using SOS.Lib.Util.Configuration;
using SOS.Lib.Util.Cryptography;

namespace SOS.Lib.Util.ActiveDirectory
{
	public class ADUtility
	{
		#region Public Constant Variables

		public static readonly DateTime DefaultDate = DateTime.MinValue;
		public static string ADPath = TripleDES.DecryptString(ConfigurationSettings.Current.GetConfig("ADPAth"), null);
		public static string ADUser = TripleDES.DecryptString(ConfigurationSettings.Current.GetConfig("ADUser"), null);
		public static string ADPassword = TripleDES.DecryptString(ConfigurationSettings.Current.GetConfig("ADPassword"), null);

		public static string ADUsersPath = TripleDES.DecryptString(ConfigurationSettings.Current.GetConfig("ADUsersPath"),
																   null);

		#endregion

		#region Private Functions

		internal static DirectoryEntry GetDirectoryObject(string szUsername, string szPassword, string szPath)
		{
			var oDe = new DirectoryEntry(szPath, szUsername, szPassword, AuthenticationTypes.Secure);
			return oDe;
		}

		internal static DirectoryEntry GetDirectoryObject(string szUserName, string szPassword)
		{
			var oDe = GetDirectoryObject(szUserName, szPassword, ADPath);
			return oDe;
		}

		#endregion

		#region Public Enums

		public enum LoginResult
		{
			// ReSharper disable InconsistentNaming
			LOGIN_OK = 0,
			LOGIN_USER_DOESNT_EXIST,
			LOGIN_USER_ACCOUNT_INACTIVE
			// ReSharper restore InconsistentNaming
		}

		#region Nested type: ADAccountOptions

		internal enum ADAccountOptions
		{
			// ReSharper disable InconsistentNaming
			UF_TEMP_DUPLICATE_ACCOUNT = 256,
			UF_NORMAL_ACCOUNT = 512,
			UF_INTERDOMAIN_TRUST_ACCOUNT = 2048,
			UF_WORKSTATION_TRUST_ACCOUNT = 4096,
			UF_SERVER_TRUST_ACCOUNT = 8192,
			UF_DONT_EXPIRE_PASSWD = 65536,
			UF_SCRIPT = 1,
			UF_ACCOUNTDISABLE = 2,
			UF_HOMEDIR_REQUIRED = 8,
			UF_LOCKOUT = 16,
			UF_PASSWD_NOTREQD = 32,
			UF_PASSWD_CANT_CHANGE = 64,
			UF_ACCOUNT_LOCKOUT = 16,
			UF_ENCRYPTED_TEXT_PASSWORD_ALLOWED = 128
			// ReSharper restore InconsistentNaming
		}

		#endregion

		#region Nested type: GroupScope

		internal enum GroupScope
		{
			// ReSharper disable InconsistentNaming
			ADS_GROUP_TYPE_DOMAIN_LOCAL_GROUP = -2147483644,
			ADS_GROUP_TYPE_GLOBAL_GROUP = -2147483646,
			ADS_GROUP_TYPE_UNIVERSAL_GROUP = -2147483640
			// ReSharper restore InconsistentNaming
		}

		#endregion

		#region Nested type: UserStatus

		internal enum UserStatus
		{
			Enable = 544,
			Disable = 546
		}

		#endregion

		#endregion

		#region Functions

		internal static string GetPathFromDomainName(string szDomainName)
		{
			// Return default path
			if (string.IsNullOrEmpty(szDomainName)) return ADPath;

			// Default execution path
			/*Check to see if there is a period in the domain name
			 */
			var szCom = "COM";
			szDomainName = szDomainName.Trim().ToUpper();
			var nPointPosition = szDomainName.LastIndexOf(".");
			if (nPointPosition > -1)
			{
				szCom = szDomainName.Substring(nPointPosition + 1);
				szDomainName = szDomainName.Substring(0, nPointPosition);
			}

			var szResult = string.Format("LDAP://{0}.{1}", szDomainName, szCom);

			return szResult;
		}

		internal static DirectoryEntry GetUser(string szUserName, string szPassword)
		{
			return GetUser(szUserName, szPassword, null);
		}

		internal static DirectoryEntry GetUser(string szUserName, string szPassword, string szDomainName)
		{
			var szPath = GetPathFromDomainName(szDomainName);
			DirectoryEntry de = GetDirectoryObject(szUserName, szPassword, szPath);
			var deSearch = new DirectorySearcher
			{
				SearchRoot = de,
				Filter = "(&(objectClass=user)(sAMAccountName=" + szUserName + "))",
				SearchScope = SearchScope.Subtree
			};
			SearchResult results = deSearch.FindOne();
			if (results != null)
			{
				de = new DirectoryEntry(results.Path, ADUser, ADPassword, AuthenticationTypes.Secure);
				return de;
			}

			// Default path of execution
			return null;
		}

		internal static void EnableUserAccount(DirectoryEntry oDe)
		{
			oDe.Properties["userAccountControl"].Value = UserStatus.Enable;
			oDe.CommitChanges();
			oDe.Close();
		}

		internal static DirectoryEntry GetGroup(string szName)
		{
			DirectoryEntry de = GetDirectoryObject();
			var deSearch = new DirectorySearcher
			{
				SearchRoot = de,
				Filter = "(&(objectClass=group)(cn=" + szName + "))",
				SearchScope = SearchScope.Subtree
			};
			SearchResult results = deSearch.FindOne();
			if (results != null)
			{
				de = new DirectoryEntry(results.Path, ADUser, ADPassword, AuthenticationTypes.Secure);
				return de;
			}

			// Default path of execution
			return null;
		}

		internal static void DisableUserAccount(DirectoryEntry oDe)
		{
			oDe.Properties["userAccountControl"].Value = UserStatus.Disable;
			oDe.CommitChanges();
			oDe.Close();
		}

		internal static DirectoryEntry GetDirectoryObject(string szDomainReference)
		{
			var oDe = new DirectoryEntry(ADPath + szDomainReference, ADUser, ADPassword, AuthenticationTypes.Secure);
			return oDe;
		}

		internal static void SetUserPassword(DirectoryEntry oDe, string szPassword)
		{
			oDe.Invoke("SetPassword", new object[] { szPassword });
		}

		internal static bool IsAccountActive(int userAccountControl)
		{
			int nUserAccountControlDisabled = Convert.ToInt32(ADAccountOptions.UF_ACCOUNTDISABLE);
			int flagExists = userAccountControl & nUserAccountControlDisabled;
			if (flagExists > 0)
			{
				return false;
			}

			// Default path of execution
			return true;
		}

		internal static string GetLDAPDomain()
		{
			var sbLDAPDomain = new StringBuilder();
			const string szServerName = "k2mega.local";
			string[] szaLDAPDc = szServerName.Split(Convert.ToChar("."));
			var i = 0;
			while (i < szaLDAPDc.GetUpperBound(0) + 1)
			{
				sbLDAPDomain.Append("DC=" + szaLDAPDc[i]);
				if (i < szaLDAPDc.GetUpperBound(0))
				{
					sbLDAPDomain.Append(",");
				}
				i += 1;
			}
			return sbLDAPDomain.ToString();
		}

		internal static DirectoryEntry GetDirectoryObjectByDistinguishedName(string szObjectPath)
		{
			var oDe = new DirectoryEntry(szObjectPath, ADUser, ADPassword, AuthenticationTypes.Secure);
			return oDe;
		}

		internal static void SetProperty(DirectoryEntry oDe, string szPropertyName, string szPropertyValue)
		{
			if (string.IsNullOrEmpty(szPropertyValue)) return;

			// Default path of execution
			if (oDe.Properties.Contains(szPropertyName))
			{
				oDe.Properties[szPropertyName][0] = szPropertyValue;
			}
			else
			{
				oDe.Properties[szPropertyName].Add(szPropertyValue);
			}
		}

		internal static DirectoryEntry GetDirectoryObject()
		{
			var oDe = new DirectoryEntry(ADPath, ADUser, ADPassword, AuthenticationTypes.Secure);
			return oDe;
		}

		internal static string GetProperty(DirectoryEntry oDe, string szPropertyName)
		{
#if DEBUG
			//// Loop through Properties
			//PropertyCollection pc = oDe.Properties;
			//IDictionaryEnumerator ide = pc.GetEnumerator();
			//ide.Reset();
			//while(ide.MoveNext())
			//{
			//    var pvc = ide.Entry.Value as PropertyValueCollection;

			//    Debug.WriteLine(string.Format("Name: {0}", ide.Entry.Key));
			//    Debug.WriteLine(string.Format("Value: {0}", pvc.Value));                
			//}
#endif

			if (oDe.Properties.Contains(szPropertyName))
			{
				return oDe.Properties[szPropertyName][0].ToString();
			}

			// Default path of execution
			return string.Empty;
		}

		#endregion
	}
}