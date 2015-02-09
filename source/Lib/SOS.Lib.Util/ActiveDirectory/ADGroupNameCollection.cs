using System.Collections.Generic;

namespace SOS.Lib.Util.ActiveDirectory
{
	public class ADGroupNameCollection
	{
		public const string ALL_GROUPS = "*";
		private const string _IT_SHARES_TEXT = ADGroup.GroupNames.ITShares;

		private readonly Dictionary<string, string> _groupNameDict;

		public ADGroupNameCollection(bool bAddITShares)
		{
			_groupNameDict = new Dictionary<string, string>();

			if (bAddITShares)
			{
				Add(_IT_SHARES_TEXT);
			}
		}

		public ADGroupNameCollection()
			: this(true)
		{
		}

		public ADGroupNameCollection(ADGroupNameCollection adGroupNameCollection)
			: this()
		{
			Add(adGroupNameCollection);
		}

		public ADGroupNameCollection(params string[] groupNames)
			: this()
		{
			Add(groupNames);
		}

		public ADGroupNameCollection(IEnumerable<string> groupNames)
			: this()
		{
			Add(groupNames);
		}

		public IEnumerable<string> GroupNames
		{
			get { return _groupNameDict.Keys; }
		}

		public ADGroupNameCollection Add(params string[] groupNames)
		{
			return Add((IEnumerable<string>) groupNames);
		}

		public ADGroupNameCollection Add(IEnumerable<string> groupNames)
		{
			foreach (string groupName in groupNames)
			{
				Add(groupName);
			}

			return this;
		}

		public ADGroupNameCollection Add(string groupName)
		{
			if (!_groupNameDict.ContainsKey(groupName))
				_groupNameDict.Add(groupName, groupName);

			return this;
		}

		public ADGroupNameCollection Add(ADGroupNameCollection adGroupNameCollection)
		{
			foreach (var kvp in adGroupNameCollection._groupNameDict)
			{
				Add(kvp.Key);
			}

			return this;
		}

		public ADGroupNameCollection AddGroups(List<ADGroup> list)
		{
			foreach (ADGroup group in list)
			{
				AddGroup(group);
			}

			return this;
		}

		public ADGroupNameCollection AddGroup(ADGroup group)
		{
			Add(group.Name);
			return this;
		}

		public bool UserHasRights(ADUser user)
		{
			List<string> adGroupNames = user.Groups.ConvertAll(delegate(ADGroup group) { return group.Name; });
			return ContainsGroupName(adGroupNames);
		}

		public bool ContainsGroupName(IEnumerable<string> adGroupNames)
		{
			bool retVal = _groupNameDict.ContainsKey(ALL_GROUPS); //anonymous

			if (!retVal)
			{
				foreach (string groupName in adGroupNames)
				{
					//user has atleast one group that is in the dictionary
					if (_groupNameDict.ContainsKey(groupName))
					{
						retVal = true;
						break;
					}
				}
			}

			return retVal;
		}

		public override string ToString()
		{
			return StringUtility.Join(_groupNameDict.Values, " || ");
		}

		public static ADGroupNameCollection Create(params string[] groupNames)
		{
			return new ADGroupNameCollection(groupNames);
		}

		public static ADGroupNameCollection Create(IEnumerable<string> groupNames)
		{
			return new ADGroupNameCollection(groupNames);
		}

		public static bool UserHasRights(ADUser user, params string[] groupNames)
		{
			return Create(groupNames).UserHasRights(user);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="adGroupNames">User's AD Group Names</param>
		/// <param name="groupNames">Group names the user will be checked against</param>
		/// <returns></returns>
		public static bool ContainsGroupName(IEnumerable<string> adGroupNames, params string[] groupNames)
		{
			return Create(groupNames).ContainsGroupName(adGroupNames);
		}

		public bool IsValidLogin(string username, string password, out string message)
		{
			ADUser user = ADManager.Instance.LoadUser(username, password);
			if (user != null)
			{
				if (!UserHasRights(user))
				{
					message = "User doesn't have sufficient rights";
				}
				else
				{
					message = null;
				}
			}
			else
			{
				message = "Login Failed";
			}

			return message == null;
		}

		public ADGroupNameCollection Clear()
		{
			return Clear(true);
		}

		public ADGroupNameCollection Clear(bool bKeepITSharesIfExists)
		{
			bool addITShares = _groupNameDict.ContainsKey(_IT_SHARES_TEXT);

			_groupNameDict.Clear();

			if (addITShares)
			{
				Add(_IT_SHARES_TEXT);
			}

			return this;
		}
	}
}