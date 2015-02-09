using System;
using System.Collections.Generic;

namespace SOS.Lib.Util
{
	public class CachedDictionary<TKey, TItem>
	{
		private readonly Dictionary<TKey, TItem> _dict;

		private readonly Func<TKey, TItem> _getItem;
		private readonly object _syncRoot = new object();

		public CachedDictionary(Func<TKey, TItem> getItem)
			: this(getItem, null)
		{
		}

		public CachedDictionary(Func<TKey, TItem> getItem, IEqualityComparer<TKey> comparer)
		{
			if (getItem == null)
				throw new ArgumentNullException("getItem");

			_getItem = getItem;
			_dict = new Dictionary<TKey, TItem>(comparer);
		}

		public TItem this[TKey key]
		{
			get
			{
				if (!_dict.ContainsKey(key))
				{
					lock (_syncRoot)
					{
						if (!_dict.ContainsKey(key))
						{
							_dict[key] = _getItem(key);
						}
					}
				}
				return _dict[key];
			}
		}

		public void ClearCache()
		{
			_dict.Clear();
		}

		public void ClearCache(TKey key)
		{
			_dict.Remove(key);
		}
	}
}