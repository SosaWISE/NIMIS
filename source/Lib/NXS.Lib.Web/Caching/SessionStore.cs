using NXS.Lib.Web.Security;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Caching;
using System.Text;
using System.Threading;
using System.Security.Cryptography;

namespace NXS.Lib.Web.Caching
{
	public delegate Session CreateSessionFunc(Session sess);
	public delegate Session ReadSessionFunc(string sessionKey);
	public delegate void UpdateSessionFunc(Session sess);
	public delegate void TerminateSessionFunc(string sessionKey);

	public class SessionStore : IDisposable
	{
		static int _count = 0;

		CreateSessionFunc _createSession;
		ReadSessionFunc _readSession;
		UpdateSessionFunc _updateSession;
		TerminateSessionFunc _terminateSession;

		TimeSpan _slidingExpiration;

		LockBy<string> _idlocker;

		MemoryCache _cache;
		CacheItemPolicy _policy;

		public SessionStore(CreateSessionFunc createSession, ReadSessionFunc readSession, UpdateSessionFunc updateSession, TerminateSessionFunc terminateSession,
			TimeSpan slidingExpiration,
			int? memoryLimitMb = null, int? physicalMemoryLimitPercent = null, TimeSpan? pollingInterval = null)
		{
			_createSession = createSession;
			_updateSession = updateSession;
			_readSession = readSession;
			_terminateSession = terminateSession;

			_slidingExpiration = slidingExpiration;

			_idlocker = new LockBy<string>();

			var cacheSettings = new NameValueCollection();
			cacheSettings.Add("CacheMemoryLimitMegabytes", memoryLimitMb.HasValue ? memoryLimitMb.ToString() : "10");
			cacheSettings.Add("physicalMemoryLimitPercentage", physicalMemoryLimitPercent.HasValue ? physicalMemoryLimitPercent.Value.ToString() : "49");  //set % here
			cacheSettings.Add("pollingInterval", pollingInterval.HasValue ? pollingInterval.ToString() : "00:02:30");
			_cache = new MemoryCache("SessionStore" + (++_count), cacheSettings);

			_policy = new CacheItemPolicy();
			_policy.SlidingExpiration = slidingExpiration;
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
			_rnd.Dispose();
			_sha1.Dispose();
		}

		// add new session
		public byte[] Create(string ipAddress)
		{
			if (_disposed)
				throw new Exception("SessionStore is disposed");

			var sessionNum = NewSessionNum();
			var sessionKey = SessionNumToKey(sessionNum);
			_idlocker.Lock(sessionKey, () =>
			{
				// save to disk
				var sess = new Session
				{
					Username = null,
					IPAddress = ipAddress,
					SessionKey = sessionKey,
					LastAccessedOn = DateTime.UtcNow,
				};
				sess = _createSession(sess);
				// add to cache
				_cache.Set(sessionKey, sess, _policy);
			});

			return sessionNum;
		}

		// re-new session
		public bool TryRenew(byte[] currSessionNum, string username, string ipAddress, out byte[] newSessionNum)
		{
			if (_disposed) throw new Exception("SessionStore is disposed");

			Session sess = default(Session);
			try
			{
				var sessionNum = NewSessionNum();
				var sessionKey = SessionNumToKey(sessionNum);
				_idlocker.Lock(sessionKey, () =>
				{
					// try to get the current session
					var currSessionKey = SessionNumToKey(currSessionNum);
					_idlocker.Lock(currSessionKey, () =>
					{
						if (!TryAccessInternal(currSessionNum, username, ipAddress, false, out sess)) // re-entrant lock
						{
							// current session is not valid
							sessionNum = null;
							return;
						}
						// remove current from cache
						_cache.Remove(currSessionKey);

						try
						{
							// update session key
							sess.SessionKey = sessionKey;
							// save all changes to disk
							_updateSession(sess);
							// add to cache using new key
							_cache.Set(sessionKey, sess, _policy);
						}
						catch (Exception ex)
						{
							// if there are any errors, assume the session is no longer valid
							sessionNum = null;
							_cache.Remove(sessionKey);

							throw ex; //???
						}
					});
				});
				newSessionNum = sessionNum;
				return newSessionNum != null;
			}
			catch
			{
				// failed to renew
				newSessionNum = null;
				return false;
			}
		}

		// get session
		public bool TryAccess(byte[] sessionNum, string username, string ipAddress, out Session result)
		{
			if (_disposed)
				throw new Exception("SessionStore is disposed");
			return TryAccessInternal(sessionNum, username, ipAddress, true, out result);
		}
		private bool TryAccessInternal(byte[] sessionNum, string username, string ipAddress, bool saveChanges, out Session result)
		{
			Session sess = default(Session);

			var sessionKey = SessionNumToKey(sessionNum);
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
					sess = _readSession(sessionKey);
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
				if (_slidingExpiration < DateTime.UtcNow.Subtract(sess.LastAccessedOn))
				{
					// session has expired
					sess = default(Session);
					_cache.Remove(sessionKey);
					return;
				}

				if (sess.Username != null && sess.Username != username)
				{
					throw new Exception("username cannot change");
				}
				// update access time
				try
				{
					sess.Username = username;
					sess.IPAddress = ipAddress;
					sess.LastAccessedOn = DateTime.UtcNow;
					if (saveChanges)
					{
						// make changes on disk
						_updateSession(sess);
					}
				}
				catch (Exception ex)
				{
					// if there are any errors, assume the session is no longer valid
					sess = default(Session);
					_cache.Remove(sessionKey);

					throw ex; //???
				}
			});
			result = sess;
			return result != default(Session);
		}

		// remove session
		public void Terminate(byte[] sessionNum)
		{
			if (_disposed)
				throw new Exception("SessionStore is disposed");
			var sessionKey = SessionNumToKey(sessionNum);
			_idlocker.Lock(sessionKey, () =>
			{
				// remove from cache
				_cache.Remove(sessionKey);
				// make changes on disk
				_terminateSession(sessionKey);
			});
		}

		public long GetCount()
		{
			return _cache.GetCount();
		}

		private RandomNumberGenerator _rnd = RandomNumberGenerator.Create();
		private byte[] NewSessionNum()
		{
			if (_disposed)
				throw new Exception("SessionStore is disposed");
			var key = new byte[16]; // 128 / 8
			_rnd.GetBytes(key);
			return key;
		}

		private SHA1Managed _sha1 = new SHA1Managed();
		public string SessionNumToKey(byte[] sessionNum)
		{
			if (_disposed)
				throw new Exception("SessionStore is disposed");
			return Convert.ToBase64String(_sha1.ComputeHash(sessionNum));
		}
	}
}
