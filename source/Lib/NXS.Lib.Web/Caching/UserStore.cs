using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace NXS.Lib.Web.Caching
{
	public delegate User ReadUserFunc(string username);

	public class UserStore : IDisposable
	{
		static int _count = 0;

		LockBy<string> _idlocker;

		ReadUserFunc _readUser;
		TimeSpan _hardExpirationLength;
		MemoryCache _cache;

		public UserStore(ReadUserFunc readUser, TimeSpan hardExpirationLength,
			int? memoryLimitMb = null, int? physicalMemoryLimitPercent = null, TimeSpan? pollingInterval = null)
		{
			_idlocker = new LockBy<string>();

			_readUser = readUser;
			_hardExpirationLength = hardExpirationLength;

			var cacheSettings = new System.Collections.Specialized.NameValueCollection();
			cacheSettings.Add("CacheMemoryLimitMegabytes", memoryLimitMb.HasValue ? memoryLimitMb.ToString() : "10");
			cacheSettings.Add("physicalMemoryLimitPercentage", physicalMemoryLimitPercent.HasValue ? physicalMemoryLimitPercent.Value.ToString() : "49");  //set % here
			cacheSettings.Add("pollingInterval", pollingInterval.HasValue ? pollingInterval.ToString() : "00:02:30");
			_cache = new MemoryCache("UserStore" + (++_count), cacheSettings);
		}

		bool _disposed = false;
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
						var p = new CacheItemPolicy()
						{
							AbsoluteExpiration = DateTimeOffset.UtcNow.Add(_hardExpirationLength),
							Priority = CacheItemPriority.Default,
							RemovedCallback = this.OnRemoved,
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
				_cache.Remove(username);
			});
		}
	}
}
