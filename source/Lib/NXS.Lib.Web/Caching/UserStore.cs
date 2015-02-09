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
		ReadUserFunc _readUser;
		TimeSpan _hardExpiration;

		LockBy<string> _idlocker;

		static int _count = 0;
		MemoryCache _cache;

		public UserStore(ReadUserFunc readUser, TimeSpan hardExpiration,
			int? memoryLimitMb = null, int? physicalMemoryLimitPercent = null, TimeSpan? pollingInterval = null)
		{
			_readUser = readUser;
			_hardExpiration = hardExpiration;

			_idlocker = new LockBy<string>();

			var cacheSettings = new NameValueCollection();
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
						var p = new CacheItemPolicy();
						p.AbsoluteExpiration = DateTimeOffset.UtcNow.Add(_hardExpiration);
						p.Priority = CacheItemPriority.Default;
						p.RemovedCallback = this.OnRemoved;

						_cache.Set(username, user, p);
					}
				}
			});
			return user;
		}
		private void OnRemoved(CacheEntryRemovedArguments arguments)
		{
		}
	}
}
