using System;
using System.Collections;
using System.Collections.Generic;

namespace SOS.Lib.Core
{
	public static class CacheManager
	{
		#region Structs

		private struct CacheKey
		{
			public string KeyTypeName { get; set; }
			public string ValueTypeName { get; set; }
		}

		#endregion Structs

		#region Nested Classes

		private interface ICacheContainer : IDictionary
		{
			Guid CacheID { get; set; }
		}

		private class CacheContainer<TKey, TValue> : Dictionary<TKey, TValue>, ICacheContainer
		{
			public Guid CacheID { get; set; }

			public CacheContainer(Guid cacheID)
			{
				CacheID = cacheID;
			}
		}

		#endregion Nested Classes

		#region Properties

		#region Private

		private static readonly Dictionary<CacheKey, ICacheContainer> Caches;
		private static readonly Dictionary<CacheKey, object> SyncRoots;

		private static readonly object SyncRootCaches = new object();

		#endregion Private

		#endregion Properties

		#region Constructors

		static CacheManager()
		{
			Caches = new Dictionary<CacheKey, ICacheContainer>();
			SyncRoots = new Dictionary<CacheKey, object>();
		}

		#endregion Constructors

		#region Methods

		#region Private

		private static CacheKey GetCacheKey<TKey, TValue>()
		{
			return new CacheKey
			{
				KeyTypeName = typeof(TKey).FullName,
				ValueTypeName = typeof(TValue).FullName
			};
		}

		private static CacheContainer<TKey, TValue> GetCacheManager<TKey, TValue>()
		{
			return GetCacheManager<TKey, TValue>(GetCacheKey<TKey, TValue>());
		}

		private static CacheContainer<TKey, TValue> GetCacheManager<TKey, TValue>(CacheKey ckey)
		{
			if (Caches.ContainsKey(ckey))
			{
				return Caches[ckey] as CacheContainer<TKey, TValue>;
			}

			// Default execution path
			return null;
		}

		#endregion Private

		#region Public

		public static void RegisterCache<TKey, TValue>(Guid cacheID)
		{
			CacheKey ckey = GetCacheKey<TKey, TValue>();
			if (Caches.ContainsKey(ckey))
			{
				throw new InvalidOperationException(string.Format("A CacheManager has already been registered for that Key Type ({0}) and Value Type ({1})", typeof(TKey), typeof(TValue)));
			}

			lock (SyncRootCaches)
			{
				Caches.Add(ckey, new CacheContainer<TKey, TValue>(cacheID));
				SyncRoots.Add(ckey, new object());
			}
		}

		public static void AddToCache<TKey, TValue>(TKey key, TValue value)
		{
			CacheKey ckey = GetCacheKey<TKey, TValue>();
			CacheContainer<TKey, TValue> cache = GetCacheManager<TKey, TValue>(ckey);
			if (cache != null)
			{
				lock (SyncRoots[ckey])
				{
					if (!cache.ContainsKey(key))
					{
						cache.Add(key, value);
					}
					else
					{
						cache[key] = value;
					}
				}
			}
		}

		public static void RemoveFromCache<TKey, TValue>(TKey key, TValue value)
		{
			CacheKey ckey = GetCacheKey<TKey, TValue>();
			CacheContainer<TKey, TValue> cache = GetCacheManager<TKey, TValue>(ckey);
			if (cache != null)
			{
				lock (SyncRoots[ckey])
				{
					if (cache.ContainsKey(key))
					{
						cache.Remove(key);
					}
				}
			}
		}

		public static TValue GetFromCache<TKey, TValue>(TKey key)
		{
			CacheContainer<TKey, TValue> cache = GetCacheManager<TKey, TValue>();
			if (cache != null && cache.ContainsKey(key))
			{
				return cache[key];
			}

			// Default execution path
			return default(TValue);
		}

		public static List<string> ClearCacheByID(Guid cacheID)
		{
			var result = new List<string>();

			foreach (CacheKey currKey in Caches.Keys)
			{
				ICacheContainer currContainer = Caches[currKey];
				if (currContainer.CacheID == cacheID)
				{
					lock (SyncRoots[currKey])
					{
						currContainer.Clear();
					}

					result.Add(string.Format("{0} : {1}", currKey.KeyTypeName, currKey.ValueTypeName));
				}
			}

			return result;
		}

		#endregion Public

		#endregion Methods
	}
}
