using System.Collections.Generic;
using System.Linq;
using SOS.Lib.Util;

namespace NXS.Framework.Wpf.Mvvm.Security
{
	public class PermissionList
	{
		List<Permission> _list;

		public PermissionList()
		{
			_list = new List<Permission>();
		}
		public void Add(Permission permission)
		{
			_list.Add(permission);
			//sort after each add to keep in correct order
			_list.Sort(Permission.PermissionComparer.Instance);
		}
		public bool Remove(Permission permission)
		{
			return _list.Remove(permission);
		}

		public bool HasPermission(UserSecurityInfo usi)
		{
			Permission foundPermission = FindPermission(usi);

			return foundPermission != null && foundPermission.AllowAccess;
		}

		private Permission FindPermission(UserSecurityInfo usi)
		{
			return _list.FirstOrDefault(a =>
				(a.PermissionType == Permission.PermissionTypes.Username && string.Compare(a.PrincipalName, usi.Username, true) == 0)
				|| (a.PermissionType == Permission.PermissionTypes.ADGroup && usi.ADGroups.ContainsValue(a.PrincipalName))
			);
		}

		public override string ToString()
		{
			return "Count:" + _list.Count + "-> " + StringUtility.Join(_list, "|");
		}
	}

	public class Permission
	{
		public enum PermissionTypes
		{
			ADGroup = 1,
			Username = 2,
		}

		public PermissionTypes PermissionType { get; set; }
		public string PrincipalName { get; set; }
		public bool AllowAccess { get; set; }

		public override string ToString()
		{
			return string.Format("{2}-{0}-{1}", PermissionType, PrincipalName, (AllowAccess ? "Allow" : "Deny"));
		}

		public class PermissionComparer : IComparer<Permission>
		{
			#region Singleton Implementation

			private PermissionComparer() { }
			public static PermissionComparer Instance
			{
				get { return Nested.Instance; }
			}
			private class Nested
			{
				internal static readonly PermissionComparer Instance = new PermissionComparer();
			}

			#endregion Singleton Implementation

			public int Compare(Permission x, Permission y)
			{
				int result = 0;

				if (result == 0) {
					// Deny has precedence, false comes before true
					result = x.AllowAccess.CompareTo(y.AllowAccess);
				}

				if (result == 0) {
					result = x.PermissionType.CompareTo(y.PermissionType);
				}

				if (result == 0) {
					result = string.Compare(x.PrincipalName, y.PrincipalName, true);
				}

				return result;
			}
		}
	}
}
