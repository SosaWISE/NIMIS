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
using System.Collections.Generic;
using System.DirectoryServices;

namespace SOS.Lib.Util.ActiveDirectory
{
	public class ADManager
	{
		private static ADManager _instance;
		private static readonly object _syncRootInstance = new object();

		private ADManager()
		{
		}

		public static ADManager Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_syncRootInstance)
					{
						if (_instance == null)
						{
							_instance = new ADManager();
						}
					}
				}
				return _instance;
			}
		}

		#region Private Functions

		private static List<ADUser> LoadUsers(SearchResultCollection seCollection)
		{
			var list = new List<ADUser>();
			foreach (SearchResult se in seCollection)
				list.Add(Load(new DirectoryEntry(se.Path, ADUtility.ADUser, ADUtility.ADPassword, AuthenticationTypes.Secure)));
			return list;
		}

		private List<ADGroup> LoadGroups(SearchResultCollection seCollection)
		{
			var list = new List<ADGroup>();
			foreach (SearchResult se in seCollection)
				list.Add(LoadGroup(new DirectoryEntry(se.Path, ADUtility.ADUser, ADUtility.ADPassword, AuthenticationTypes.Secure)));
			return list;
		}

		private ADGroup LoadGroup(DirectoryEntry de)
		{
			var oADGroup = new ADGroup
							{
								Name = ADUtility.GetProperty(de, "cn"),
								DisplayName = ADUtility.GetProperty(de, "DisplayName"),
								DistinguishedName = ADUtility.ADPath + "/" + ADUtility.GetProperty(de, "DistinguishedName"),
								Description = ADUtility.GetProperty(de, "Description")
							};
			return oADGroup;
		}

		private static SearchResultCollection GetUsers()
		{
			DirectoryEntry de = ADUtility.GetDirectoryObject();
			var deSearch = new DirectorySearcher
							{
								SearchRoot = de,
								Filter = "(&(objectClass=user)(objectCategory=person))",
								SearchScope = SearchScope.Subtree
							};
			return deSearch.FindAll();
		}

		private static SearchResultCollection GetGroups()
		{
			DirectoryEntry de = ADUtility.GetDirectoryObject();
			var deSearch = new DirectorySearcher { SearchRoot = de, Filter = "(&(objectClass=group))" };
			return deSearch.FindAll();
		}

		private static DirectoryEntry GetUser(string szUserName)
		{
			DirectoryEntry oDe = ADUtility.GetDirectoryObject();
			Console.Write("Test: {0}", oDe);
			var deSearch = new DirectorySearcher
							{
								SearchRoot = oDe,
								Filter = "(&(objectClass=user)(samAccountName=" + szUserName + "))",
								SearchScope = SearchScope.Subtree
							};
			//			deSearch.Filter = "(&(objectClass=user)(CN=" + szUserName+ "))";
			try
			{
				SearchResult results = deSearch.FindOne();
				if (results != null)
				{
					oDe = new DirectoryEntry(results.Path, ADUtility.ADUser, ADUtility.ADPassword, AuthenticationTypes.Secure);
					return oDe;
				}
			}
			catch (Exception oEx)
			{
				Console.WriteLine("The following Error occurred: {0}", oEx.Message);
			}

			// Default path meaning that nothing was found.
			return null;
		}

		/// <summary>
		/// Given a directory Entry it reutrns an ADUser if the entry is not null otherwise it returns null.
		/// </summary>
		/// <param name="de">DirectoryEntry</param>
		/// <returns>User</returns>
		private static ADUser Load(DirectoryEntry de)
		{
			if (de != null)
			{
				var oUser = new ADUser
								{
									FirstName = ADUtility.GetProperty(de, "givenName"),
									MiddleInitial = ADUtility.GetProperty(de, "initials"),
									LastName = ADUtility.GetProperty(de, "sn"),
									UserPrincipalName = ADUtility.GetProperty(de, "UserPrincipalName"),
									PostalAddress = ADUtility.GetProperty(de, "PostalAddress"),
									MailingAddress = ADUtility.GetProperty(de, "StreetAddress"),
									ResidentialAddress = ADUtility.GetProperty(de, "HomePostalAddress"),
									Title = ADUtility.GetProperty(de, "Title"),
									HomePhone = ADUtility.GetProperty(de, "HomePhone"),
									OfficePhone = ADUtility.GetProperty(de, "TelephoneNumber"),
									Mobile = ADUtility.GetProperty(de, "Mobile"),
									Fax = ADUtility.GetProperty(de, "FacsimileTelephoneNumber"),
									Email = ADUtility.GetProperty(de, "mail"),
									Url = ADUtility.GetProperty(de, "Url"),
									UserName = ADUtility.GetProperty(de, "sAMAccountName"),
									DistinguishedName = ADUtility.ADPath + "/" + ADUtility.GetProperty(de, "DistinguishedName"),
									IsAccountActive =
										ADUtility.IsAccountActive(Convert.ToInt32(ADUtility.GetProperty(de, "userAccountControl"))),
									SSID = de.NativeGuid,
									GUID = de.Guid
								};
				return oUser;
			}

			// Defautl result
			return null;
		}

		private static DirectoryEntry AddGroup(string szName, string szDisplayName, string szDistinguishedName, string szDescription)
		{
			string szRootDSE;
			var oDSESearcher = new DirectorySearcher();
			try
			{
				szRootDSE = oDSESearcher.SearchRoot.Path;
				szRootDSE = szRootDSE.Insert(7, ADUtility.ADUsersPath);
				var myDE = new DirectoryEntry(szRootDSE);
				DirectoryEntries myEntries = myDE.Children;
				DirectoryEntry myDirectoryEntry = myEntries.Add("CN=" + szName, "Group");
				ADUtility.SetProperty(myDirectoryEntry, "cn", szName);
				ADUtility.SetProperty(myDirectoryEntry, "DisplayName", szDisplayName);
				ADUtility.SetProperty(myDirectoryEntry, "Description", szDescription);
				ADUtility.SetProperty(myDirectoryEntry, "sAMAccountName", szName);
				ADUtility.SetProperty(myDirectoryEntry, "groupType",
									  Convert.ToString(ADUtility.GroupScope.ADS_GROUP_TYPE_GLOBAL_GROUP));

				myDirectoryEntry.CommitChanges();
				myDirectoryEntry = ADUtility.GetGroup(szName);
				return myDirectoryEntry;
			}
			catch (Exception oEx)
			{
				System.Diagnostics.Debug.WriteLine("The following error occurred in AddGroup: {0}", oEx.Message);
				// ReSharper disable PossibleIntendedRethrow
				throw (oEx);
				// ReSharper restore PossibleIntendedRethrow
			}
		}

		private DirectoryEntry Add(string FirstName, string MiddleInitial, string LastName, string UserPrincipalName,
								   string PostalAddress, string MailingAddress, string ResidentialAddress, string Title,
								   string HomePhone, string OfficePhone, string Mobile, string Fax, string Email, string Url,
								   string UserName, string Password, string DistinguishedName, bool IsAccountActive)
		{
			string szRootDSE;
			var oDSESearcher = new DirectorySearcher();
			try
			{
				szRootDSE = oDSESearcher.SearchRoot.Path;
				szRootDSE = szRootDSE.Insert(7, ADUtility.ADUsersPath);
				var myDE = new DirectoryEntry(szRootDSE);
				DirectoryEntries myEntries = myDE.Children;
				DirectoryEntry myDirectoryEntry = myEntries.Add("CN=" + UserName, "user");
				ADUtility.SetProperty(myDirectoryEntry, "givenName", FirstName);
				ADUtility.SetProperty(myDirectoryEntry, "initials", MiddleInitial);
				ADUtility.SetProperty(myDirectoryEntry, "sn", LastName);
				ADUtility.SetProperty(myDirectoryEntry, "UserPrincipalName", UserPrincipalName ?? UserName);
				ADUtility.SetProperty(myDirectoryEntry, "PostalAddress", PostalAddress);
				ADUtility.SetProperty(myDirectoryEntry, "StreetAddress", MailingAddress);
				ADUtility.SetProperty(myDirectoryEntry, "HomePostalAddress", ResidentialAddress);
				ADUtility.SetProperty(myDirectoryEntry, "Title", Title);
				ADUtility.SetProperty(myDirectoryEntry, "HomePhone", HomePhone);
				ADUtility.SetProperty(myDirectoryEntry, "TelephoneNumber", OfficePhone);
				ADUtility.SetProperty(myDirectoryEntry, "Mobile", Mobile);
				ADUtility.SetProperty(myDirectoryEntry, "FacsimileTelephoneNumber", Fax);
				ADUtility.SetProperty(myDirectoryEntry, "mail", Email);
				ADUtility.SetProperty(myDirectoryEntry, "Url", Url);
				ADUtility.SetProperty(myDirectoryEntry, "sAMAccountName", UserName);
				ADUtility.SetProperty(myDirectoryEntry, "UserPassword", Password);
				myDirectoryEntry.Properties["userAccountControl"].Value = ADUtility.UserStatus.Enable;
				myDirectoryEntry.CommitChanges();
				myDirectoryEntry = GetUser(UserName);
				ADUtility.SetUserPassword(myDirectoryEntry, Password);
				return myDirectoryEntry;
			}
			catch (Exception ex)
			{
				throw (ex);
			}
		}

		#endregion

		#region Public Functions

		public ADUser LoadUser(string userName)
		{
			return Load(GetUser(StringUtility.FormatUsername(userName)));
		}

		public ADUser LoadUser(string userName, string password)
		{
			DirectoryEntry deUser;
			try
			{
				deUser = ADUtility.GetUser(StringUtility.FormatUsername(userName), password);
			}
			catch (Exception)
			{
				deUser = null;
			}

			return Load(deUser);
		}

		public ADGroup LoadGroup(string szGroupName)
		{
			return LoadGroup(ADUtility.GetGroup(szGroupName));
		}

		public List<ADUser> LoadAllUsers()
		{
			return LoadUsers(GetUsers());
		}

		public List<ADGroup> LoadAllGroups()
		{
			return LoadGroups(GetGroups());
		}

		public ADUser CreateADUser(ADUser User)
		{
			return
				Load(
					Add(User.FirstName, User.MiddleInitial, User.LastName, User.UserPrincipalName, User.PostalAddress,
						User.MailingAddress, User.ResidentialAddress, User.Title, User.HomePhone, User.OfficePhone,
						User.Mobile, User.Fax, User.Email, User.Url, User.UserName, User.Password, User.DistinguishedName,
						User.IsAccountActive));
		}

		public ADGroup CreateADGroup(ADGroup Group)
		{
			return LoadGroup(AddGroup(Group.Name, Group.DisplayName, Group.DistinguishedName, Group.Description));
		}

		public static void AddUserToGroup(string UserDistinguishedName, string GroupDistinguishedName)
		{
			DirectoryEntry oGroup = ADUtility.GetDirectoryObjectByDistinguishedName(GroupDistinguishedName);
			oGroup.Invoke("Add", new object[] { UserDistinguishedName });
			oGroup.Close();
		}

		public static void RemoveUserFromGroup(string UserDistinguishedName, string GroupDistinguishedName)
		{
			DirectoryEntry oGroup = ADUtility.GetDirectoryObjectByDistinguishedName(GroupDistinguishedName);
			oGroup.Invoke("Remove", new object[] { UserDistinguishedName });
			oGroup.Close();
		}

		public static void DisableUserAccount(string UserName)
		{
			ADUtility.DisableUserAccount(GetUser(UserName));
		}

		public static void EnableUserAccount(string UserName)
		{
			ADUtility.EnableUserAccount(GetUser(UserName));
		}

		public static void DeleteUserAccount(string UserName)
		{
			DirectoryEntry de = GetUser(UserName);
			de.DeleteTree();
			de.CommitChanges();
		}

		public static void DeleteGroupAccount(string GroupName)
		{
			DirectoryEntry de = ADUtility.GetGroup(GroupName);
			de.DeleteTree();
			de.CommitChanges();
		}

		public static bool IsUserValid(string szUsername, string szPassword, string szDomainName)
		{
			try
			{
				DirectoryEntry deUser = ADUtility.GetUser(szUsername, szPassword, szDomainName);
				deUser.Close();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public static bool IsUserValid(string szUsername, string szPassword)
		{
			return IsUserValid(szUsername, szPassword, null);
		}

		public static bool UserExists(string szUserName)
		{
			DirectoryEntry de = ADUtility.GetDirectoryObject();
			var deSearch = new DirectorySearcher {SearchRoot = de, Filter = "(&(objectClass=user) (cn=" + szUserName + "))"};
			SearchResultCollection results = deSearch.FindAll();
			if (results.Count == 0)
			{
				return false;
			}

			// Default execution path
			return true;
		}

		public static bool GroupExists(string szGroupName)
		{
			DirectoryEntry de = ADUtility.GetDirectoryObject();
			var deSearch = new DirectorySearcher {SearchRoot = de, Filter = "(&(objectClass=group) (cn=" + szGroupName + "))"};
			SearchResultCollection results = deSearch.FindAll();
			if (results.Count == 0)
			{
				return false;
			}

			// Default execution path
			return true;
		}

		public static ADUtility.LoginResult Login(string szUserName, string szPassword)
		{
			return Login(szUserName, szPassword, null);
		}

		public static ADUtility.LoginResult Login(string szUserName, string szPassword, string szDomainName)
		{
			if (IsUserValid(szUserName, szPassword, szDomainName))
			{
				DirectoryEntry oDe = GetUser(szUserName);
				if (oDe != null)
				{
					if (ADUtility.UserStatus.Enable == (ADUtility.UserStatus)(oDe.Properties["userAccountControl"][0]))
					{
						oDe.Close();
						return ADUtility.LoginResult.LOGIN_OK;
					}
					else
					{
						// Check if the account is inactive
						if (ADUtility.IsAccountActive((int)oDe.Properties["userAccountControl"][0]))
						{
							oDe.Close();
							return ADUtility.LoginResult.LOGIN_OK;
						}
						else
						{
							oDe.Close();
							return ADUtility.LoginResult.LOGIN_USER_ACCOUNT_INACTIVE;
						}
					}
				}
				else
				{
					return ADUtility.LoginResult.LOGIN_USER_DOESNT_EXIST;
				}
			}
			else
			{
				return ADUtility.LoginResult.LOGIN_USER_DOESNT_EXIST;
			}
		}

		#endregion
	}
}