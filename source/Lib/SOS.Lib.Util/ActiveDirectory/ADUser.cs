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
	[Serializable]
	public class ADUser
	{
		#region Private Property Variables

		private string _fullName; //Name
		private string _displayName; //Name
		private string _distinguishedName;
		private string _email; //mail
		private string _fax; //FacsimileTelephoneNumber
		private string _firstName; //givenName
		private Guid _guid; //Guid
		private List<ADGroup> _groups;
		private string _homePhone;
		private bool _isAccountActive; //userAccountControl
		private string _lastName; //sn
		private string _mailingAddress; //StreetAddress
		private string _middleInitial; //initials
		private string _mobile;
		private string _officePhone; //TelephoneNumber
		private string _password;
		private string _postalAddress;
		private string _residentialAddress; //HomePostalAddress
		private string _ssid; //NativeGuid
		private string _title;
		private string _url;
		private string _userName; //sAMAccountName
		private string _userPrincipalName; //userPrincipalName (e.g. user@domain.local)

		#endregion

		#region Public Properties

		public string FirstName
		{
			get { return _firstName; }
			set { _firstName = value; }
		}

		public string MiddleInitial
		{
			get { return _middleInitial; }
			set
			{
				if (value.Length > 6)
				{
					throw (new Exception("MiddleInitial cannot be more than six characters"));
				}
				
				// Default execution path
				_middleInitial = value;
			}
		}

		public string LastName
		{
			get { return _lastName; }
			set { _lastName = value; }
		}

		public string FullNameWithInitial
		{
			get 
			{
				_fullName = string.Format("{0}{1}"
					, _firstName
					, string.Format("{0} {1}"
						, _middleInitial.Equals(string.Empty)
							? string.Empty
							: " " + _middleInitial.Trim() + "."
						, _lastName));
				return _fullName;
			}
		}

		public string DisplayName
		{
			get
			{
				_displayName = _firstName + _middleInitial + "." + _lastName;
				return _displayName;
			}
		}

		public string FullName
		{
			get { return string.Format("{0} {1}", FirstName, LastName); }
		}

		public string UserPrincipalName
		{
			get { return _userPrincipalName; }
			set { _userPrincipalName = value; }
		}

		public string PostalAddress
		{
			get { return _postalAddress; }
			set { _postalAddress = value; }
		}

		public string MailingAddress
		{
			get { return _mailingAddress; }
			set { _mailingAddress = value; }
		}

		public string ResidentialAddress
		{
			get { return _residentialAddress; }
			set { _residentialAddress = value; }
		}

		public string Title
		{
			get { return _title; }
			set { _title = value; }
		}

		public string HomePhone
		{
			get { return _homePhone; }
			set { _homePhone = value; }
		}

		public string OfficePhone
		{
			get { return _officePhone; }
			set { _officePhone = value; }
		}

		public string Mobile
		{
			get { return _mobile; }
			set { _mobile = value; }
		}

		public string Fax
		{
			get { return _fax; }
			set { _fax = value; }
		}

		public string Email
		{
			get { return _email; }
			set { _email = value; }
		}

		public string Url
		{
			get { return _url; }
			set { _url = value; }
		}

		public string UserName
		{
			get { return _userName; }
			set { _userName = value; }
		}

		public string Password
		{
			get { return _password; }
			set { _password = value; }
		}

		public string DistinguishedName
		{
			get { return _distinguishedName; }
			set { _distinguishedName = value; }
		}

		public bool IsAccountActive
		{
			get { return _isAccountActive; }
			set { _isAccountActive = value; }
		}

		public List<ADGroup> Groups
		{
			get { return _groups ?? (_groups = ADGroup.LoadByUser(DistinguishedName)); }
			set { _groups = value; }
		}

		public string SSID
		{
			get { return _ssid; }
			set { _ssid = value; }
		}

		public Guid GUID
		{
			get { return _guid; }
			set { _guid = value; }
		}

		#endregion

		#region Static Functions

		internal static List<ADUser> LoadByGroup(string szDistinguishedName)
		{
			return GetUsers(szDistinguishedName);
		}

		#endregion

		#region Public Functions

		public void Update()
		{
			try
			{
				DirectoryEntry de = GetDirectoyrObject(UserName);
				ADUtility.SetProperty(de, "givenName", FirstName);
				ADUtility.SetProperty(de, "initials", MiddleInitial);
				ADUtility.SetProperty(de, "sn", LastName);
				ADUtility.SetProperty(de, "UserPrincipalName", UserPrincipalName);
				ADUtility.SetProperty(de, "PostalAddress", PostalAddress);
				ADUtility.SetProperty(de, "StreetAddress", MailingAddress);
				ADUtility.SetProperty(de, "HomePostalAddress", ResidentialAddress);
				ADUtility.SetProperty(de, "Title", Title);
				ADUtility.SetProperty(de, "HomePhone", HomePhone);
				ADUtility.SetProperty(de, "TelephoneNumber", OfficePhone);
				ADUtility.SetProperty(de, "Mobile", Mobile);
				ADUtility.SetProperty(de, "FacsimileTelephoneNumber", Fax);
				ADUtility.SetProperty(de, "mail", Email);
				ADUtility.SetProperty(de, "Url", Url);
				if (IsAccountActive)
				{
					de.Properties["userAccountControl"][0] = ADUtility.UserStatus.Enable;
				}
				else
				{
					de.Properties["userAccountControl"][0] = ADUtility.UserStatus.Disable;
				}
				de.CommitChanges();
			}
			catch (Exception ex)
			{
				throw (new Exception("User cannot be updated" + ex.Message));
			}
		}

		public void SetPassword(string newPassword)
		{
			try
			{
				DirectoryEntry de = GetDirectoyrObject(UserName);
				ADUtility.SetUserPassword(de, newPassword);
				de.CommitChanges();
			}
			catch (Exception ex)
			{
				throw (new Exception("User Password cannot be set" + ex.Message));
			}
		}

		public bool IsInGroup(string szGroup)
		{
			// Locals
			var bResult = false;

			// Loop through the groups
			foreach (ADGroup grpItem in Groups)
			{
				if (grpItem.Name.Equals(szGroup))
				{
					bResult = true;
					break;
				}
			}

			// Return result
			return bResult;
		}

		#endregion

		#region Private Functions

		private static DirectoryEntry GetDirectoyrObject(string szUserName)
		{
			DirectoryEntry de = ADUtility.GetDirectoryObject();
			var deSearch = new DirectorySearcher
			               	{
			               		SearchRoot = de,
			               		Filter = "(&(objectClass=user)(sAMAccountName=" + szUserName + "))",
			               		SearchScope = SearchScope.Subtree
			               	};
			SearchResult results = deSearch.FindOne();
			if (results != null)
			{
				de = new DirectoryEntry(results.Path, ADUtility.ADUser, ADUtility.ADPassword, AuthenticationTypes.Secure);
				return de;
			}
			
			// Default execution path
			return null;
		}

		private static List<ADUser> GetUsers(string szDistinguishedName)
		{
			DirectoryEntry oDe = ADUtility.GetDirectoryObjectByDistinguishedName(szDistinguishedName);
			int index;
			var list = new List<ADUser>();
			for (index = 0; index <= oDe.Properties["member"].Count - 1; index++)
			{
				list.Add(
					Load(ADUtility.GetDirectoryObjectByDistinguishedName(ADUtility.ADPath + "/" +
						                                                oDe.Properties["member"][index])));
			}
			return list;
		}

		private static ADUser Load(DirectoryEntry de)
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
			            		GUID = de.Guid,
			            		SSID = de.NativeGuid
			            	};
			return oUser;
		}

		#endregion
	}
}