#region Using Directives

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

#endregion

namespace NXS.Framework.Wpf.ParentChildService
{
	[DataContract]
	public class ParameterDictionary
	{
		#region Properties

		#region Private

		[DataMember] private Dictionary<string, string> _container =
			new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

		#endregion Private

		#region Public

		public IEnumerable<string> ParameterKeys
		{
			get { return _container.Keys; }
		}

		public static ParameterDictionary Empty
		{
			get { return Create(); }
		}

		public int Count
		{
			get { return _container.Count; }
		}

		#endregion Public

		#endregion Properties

		#region Constructors

		public ParameterDictionary()
		{
		}

		public ParameterDictionary(Dictionary<string, string> initialValues)
		{
			if (initialValues != null)
			{
				foreach (string currKey in initialValues.Keys)
				{
					_container.Add(currKey, initialValues[currKey]);
				}
			}
		}

		#endregion Constructors

		#region Methods

		#region Public

		public static ParameterDictionary Create()
		{
			return new ParameterDictionary();
		}

		public ParameterDictionary AddParameter(string parameterKey, string parameterValue)
		{
			if (!_container.ContainsKey(parameterKey))
				_container.Add(parameterKey, parameterValue);
			return this;
		}

		public ParameterDictionary SetParameter(string parameterKey, string parameterValue)
		{
			if (_container.ContainsKey(parameterKey))
				_container[parameterKey] = parameterValue;
			return this;
		}

		public string GetParameterValue(string parameterKey)
		{
			string result;
			_container.TryGetValue(parameterKey, out result);
			return result;
		}

		public bool HasParameter(string parameterKey)
		{
			return _container.ContainsKey(parameterKey);
		}

		public bool TryGetParameter(string parameterKey, out string parameterValue)
		{
			return _container.TryGetValue(parameterKey, out parameterValue);
		}

		/// <summary>
		/// Returns true if all values for both ParameterDictionaries are equal
		/// </summary>
		public bool IsMatch(ParameterDictionary other)
		{
			if (other == null)
				throw new ArgumentNullException("other");

			//lengths have to match
			if (Count != other.Count)
				return false;

			//match on keys from this ParameterDictionary
			//	this should give us an exact match since the lengths are also equal
			return IsMatch(other, ParameterKeys.ToArray());
		}

		/// <summary>
		/// Returns true if all values for both ParameterDictionaries are equal, only comparing those values of the passed in keys 
		/// </summary>
		public bool IsMatch(ParameterDictionary other, params string[] keys)
		{
			if (other == null)
				throw new ArgumentNullException("other");
			if (keys == null)
				throw new ArgumentNullException("keys");

			foreach (string key in keys)
			{
				if (!HasParameter(key)
				    || !other.HasParameter(key)
				    || string.Compare(GetParameterValue(key), other.GetParameterValue(key), true) != 0)
				{
					return false;
				}
			}

			return true;
		}

		#endregion Public

		#endregion Methods
	}

	public struct ParameterNames
	{
		public const string AccountID = "AccountID";
		public const string ActionName = "ActionName";
		public const string ApplicationID = "ApplicationID";
		public const string CaseID = "CaseID";
		public const string ChildWindowID = "ChildWindowID";
		public const string UserID = "UserID";
		public const string RecruitID = "RecruitID";
		public const string Version = "Version";
	}
}
