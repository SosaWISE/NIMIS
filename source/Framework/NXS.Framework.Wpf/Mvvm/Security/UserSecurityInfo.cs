using System;
using System.Collections.Generic;
using SOS.Lib.Util;
using SOS.Lib.Util.ActiveDirectory;

namespace NXS.Framework.Wpf.Mvvm.Security
{
	public class UserSecurityInfo
	{
		#region Properties

		public string Username { get; private set; }
		public UniqueStringCollection ADGroups { get; private set; }

		public bool IsITShares
		{
			get { return ADGroups.ContainsValue(ADGroup.GroupNames.ITShares); }
		}

		#endregion //Properties

		public UserSecurityInfo(string username, List<string> adGroups)
			: this(username, new UniqueStringCollection(adGroups))
		{
		}
		public UserSecurityInfo(string username, UniqueStringCollection adGroups)
		{
			if (username == null)
				throw new ArgumentNullException("username");
			if (adGroups == null)
				throw new ArgumentNullException("adGroups");

			this.Username = username;
			this.ADGroups = adGroups;
		}

		public override int GetHashCode()
		{
			return Username.GetHashCode();
		}
		public override bool Equals(object obj)
		{
			if (obj is UserSecurityInfo) {
				return this == (UserSecurityInfo)obj;
			}
			return false;
		}
		public static bool operator ==(UserSecurityInfo left, UserSecurityInfo right)
		{
			// If both are null, or both are same instance, return true.
			if (Object.ReferenceEquals(left, right)) {
				return true;
			}

			// If one is null, but not both, return false.
			if (((object)left == null) || ((object)right == null)) {
				return false;
			}

			return string.Compare(left.Username, right.Username, true) == 0;
		}
		public static bool operator !=(UserSecurityInfo left, UserSecurityInfo right)
		{
			return !(left == right);
		}
	}
}
