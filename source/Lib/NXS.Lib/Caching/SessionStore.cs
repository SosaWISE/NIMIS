using System;
using System.Collections.Specialized;
using System.Runtime.Caching;

namespace NXS.Lib.Caching
{
	public delegate Session SaveSessionFunc(Session sess);
	public delegate Session ReadSessionFunc(string sessionKey);
	public delegate void SessionAction(string sessionKey);

	public class SessionStore : IDisposable
	{
		static int _count = 0;

		LockBy<string> _idlocker;

		SaveSessionFunc _save;
		ReadSessionFunc _read;
		SessionAction _touch;
		SessionAction _terminate;

		TimeSpan _maxAge;
		MemoryCache _cache;
		CacheItemPolicy _policy;

		public SessionStore(SaveSessionFunc save, ReadSessionFunc read, SessionAction touch, SessionAction terminate,
			TimeSpan maxAge, TimeSpan slidingCacheExpiration,
			int? memoryLimitMb = null, int? physicalMemoryLimitPercent = null, TimeSpan? pollingInterval = null)
		{
			_idlocker = new LockBy<string>();

			_save = save;
			_read = read;
			_touch = touch;
			_terminate = terminate;

			_maxAge = maxAge;

			var cacheSettings = new NameValueCollection();
			cacheSettings.Add("CacheMemoryLimitMegabytes", memoryLimitMb.HasValue ? memoryLimitMb.ToString() : "10");
			cacheSettings.Add("physicalMemoryLimitPercentage", physicalMemoryLimitPercent.HasValue ? physicalMemoryLimitPercent.Value.ToString() : "49");  //set % here
			cacheSettings.Add("pollingInterval", pollingInterval.HasValue ? pollingInterval.ToString() : "00:02:30");
			_cache = new MemoryCache("SessionStore" + (++_count), cacheSettings);

			_policy = new CacheItemPolicy();
			_policy.SlidingExpiration = slidingCacheExpiration;
			_policy.Priority = CacheItemPriority.Default;
			_policy.RemovedCallback = this.OnRemoved;
		}
		private void OnRemoved(CacheEntryRemovedArguments arguments)
		{
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

		// add new session
		public byte[] Create(string username, string ipAddress)
		{
			if (_disposed) throw new Exception("SessionStore is disposed");

			var sessionNum = SystemUserIdentity.NewAuthNum();
			var sessionKey = SystemUserIdentity.AuthNumToKey(sessionNum);
			_idlocker.Lock(sessionKey, () =>
			{
				// save to disk
				var sess = new Session
				{
					Username = username,
					IPAddress = ipAddress,
					SessionKey = sessionKey,
					CreatedOn = DateTime.UtcNow,
				};
				sess = _save(sess);
				// add to cache
				_cache.Set(sessionKey, sess, _policy);
			});

			return sessionNum;
		}
		//// add new session
		//public bool SetUsername(byte[] sessionNum, string username)
		//{
		//	if (_disposed) throw new Exception("SessionStore is disposed");
		//
		//	Session sess = default(Session);
		//	var sessionKey = SessionNumToKey(sessionNum);
		//	_idlocker.Lock(sessionKey, () =>
		//	{
		//		if (!Access(sessionNum, out sess))
		//		{
		//			return;
		//		}
		//
		//		if (sess.Username != null)
		//		{
		//			throw new Exception("Username already set");
		//		}
		//		try
		//		{
		//			sess.Username = username;
		//			sess = _save(sess);
		//			// update cache item
		//			_cache.Set(sessionKey, sess, _policy);
		//		}
		//		catch (Exception ex)
		//		{
		//			// remove if there are any errors
		//			sess = default(Session);
		//			_cache.Remove(sessionKey);
		//
		//			throw ex; //???
		//		}
		//	});
		//
		//	return sess != default(Session);
		//}

		// get session
		public bool Access(byte[] sessionNum, out Session result)
		{
			if (_disposed) throw new Exception("SessionStore is disposed");

			Session sess = default(Session);
			var sessionKey = SystemUserIdentity.AuthNumToKey(sessionNum);
			_idlocker.Lock(sessionKey, () =>
			{
				if (_cache.Contains(sessionKey))
				{
					// get from cache
					sess = (Session)_cache.Get(sessionKey);
				}
				else
				{
					// load from disk
					sess = _read(sessionKey);
					if (sess == default(Session))
					{
						return;
					}
					else
					{
						_cache.Set(sessionKey, sess, _policy);
					}
				}

				// validate expiration
				if (DateTime.UtcNow.Subtract(sess.CreatedOn) > _maxAge)
				{
					// session has expired
					sess = default(Session);
					_cache.Remove(sessionKey);
					return;
				}

				try
				{
					// update access time
					_touch(sessionKey);
				}
				catch (Exception ex)
				{
					// swallow errors since this is non-essential
					//@TODO: log error

					//// remove if there are any errors
					//sess = default(Session);
					//_cache.Remove(sessionKey);
					//
					//throw ex; //???
				}
			});
			result = sess;
			return result != default(Session);
		}

		// remove session
		public void Terminate(byte[] sessionNum)
		{
			if (_disposed) throw new Exception("SessionStore is disposed");

			var sessionKey = SystemUserIdentity.AuthNumToKey(sessionNum);
			_idlocker.Lock(sessionKey, () =>
			{
				// remove from cache
				_cache.Remove(sessionKey);
				// make changes on disk
				_terminate(sessionKey);
			});
		}

		public long GetCount()
		{
			return _cache.GetCount();
		}
	}
}