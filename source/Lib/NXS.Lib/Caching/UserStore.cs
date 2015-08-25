using System;
using System.Globalization;
using System.Runtime.Caching;

namespace NXS.Lib.Caching
{
	public delegate User ReadUserFunc(string username);

	public class UserStore : IDisposable
	{
		#region .ctor
		public UserStore(ReadUserFunc readUser, TimeSpan hardExpirationLength,
			int? memoryLimitMb = null, int? physicalMemoryLimitPercent = null, TimeSpan? pollingInterval = null)
		{
			_idlocker = new LockBy<string>();

			_readUser = readUser;
			_hardExpirationLength = hardExpirationLength;

// ReSharper disable once UseObjectOrCollectionInitializer
			var cacheSettings = new System.Collections.Specialized.NameValueCollection();
			cacheSettings.Add("CacheMemoryLimitMegabytes", memoryLimitMb.HasValue ? memoryLimitMb.ToString() : "10");
			cacheSettings.Add("physicalMemoryLimitPercentage", physicalMemoryLimitPercent.HasValue ? physicalMemoryLimitPercent.Value.ToString(CultureInfo.InvariantCulture) : "49");  //set % here
			cacheSettings.Add("pollingInterval", pollingInterval.HasValue ? pollingInterval.ToString() : "00:02:30");
			_cache = new MemoryCache("UserStore" + (++_count), cacheSettings);
		}
		#endregion .ctor

		#region Properties
		static int _count;

		readonly LockBy<string> _idlocker;

		readonly ReadUserFunc _readUser;
		readonly TimeSpan _hardExpirationLength;
		readonly MemoryCache _cache;

// ReSharper disable once RedundantDefaultFieldInitializer
		bool _disposed = false;
		#endregion Properties

		#region Methods
		public void Dispose()
		{
			if (_disposed)
				return;
			_disposed = true;

			_idlocker.Dispose();
			_cache.Dispose();
		}

		public User Get(string username)
		{
			if (_disposed)
				throw new Exception("UserStore is disposed");

			User user = default(User);
			_idlocker.Lock(username, () =>
			{
				if (_cache.Contains(username))
				{
					// get from cache
					user = (User)_cache.Get(username);
				}
				else
				{
					// load from disk
					user = _readUser(username);
					if (user != default(User))
					{
						var p = new CacheItemPolicy
						{
							AbsoluteExpiration = DateTimeOffset.UtcNow.Add(_hardExpirationLength),
							Priority = CacheItemPriority.Default,
							RemovedCallback = OnRemoved,
						};
						_cache.Set(username, user, p);
					}
				}
			});
			return user;
		}
		private void OnRemoved(CacheEntryRemovedArguments arguments)
		{
		}

		public void RemoveCached(string username)
		{
			if (_disposed)
				throw new Exception("UserStore is disposed");

			_idlocker.Lock(username, () =>
			{
// ReSharper disable once ConvertToLambdaExpression
				_cache.Remove(username);
			});
		}
		#endregion Methods
	}
}
