using System;
using System.Collections;
using System.Collections.Generic;

namespace SOS.Lib.Util
{
	public class UniqueStringCollection : IEnumerable<string>
	{
		private readonly Dictionary<string, int> _dict;

		public UniqueStringCollection()
		{
			_dict = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);
		}

		public UniqueStringCollection(params string[] list)
			: this()
		{
			Add(list);
		}

		public UniqueStringCollection(IEnumerable<string> list)
			: this()
		{
			Add(list);
		}

		#region IEnumerable<string> Members

		public IEnumerator<string> GetEnumerator()
		{
			return _dict.Keys.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion

		public void Add(IEnumerable<string> list)
		{
			foreach (string value in list)
			{
				Add(value);
			}
		}

		public void Add(string value)
		{
			if (!_dict.ContainsKey(value))
			{
				_dict.Add(value, 0);
			}
		}

		public bool ContainsValue(string value)
		{
			return _dict.ContainsKey(value);
		}

		public override string ToString()
		{
			return "Count:" + _dict.Count + "-> " + StringUtility.Join(_dict.Keys, "|");
		}
	}
}