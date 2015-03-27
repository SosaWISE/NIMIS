using NXS.Lib.Web.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NXS.Lib.Web.Test.Caching
{
	public class SessionStoreTests
	{
		void RunThreads(int n, ParameterizedThreadStart fn, int sleep = 0)
		{
			for (int i = 0; i < n; i++)
			{
				var t = new Thread(fn);
				t.Start(i);
			}
			if (sleep > 0)
			{
				Thread.Sleep(sleep);
			}
		}

		SessionStore CreateStore(int saveSleepMs, Func<int> incSaveCount, int readSleepMs, Func<int> incReadCount,
			int touchSleepMs = 0, Func<int> incTouchCount = null)
		{
			var _dict = new Dictionary<string, Session>();

			var store = new SessionStore((sess) =>
			{
				var n = incSaveCount();
				Thread.Sleep(saveSleepMs);
				Console.Out.WriteLine("username: " + sess.Username + " n: " + n);
				if (!_dict.ContainsKey(sess.SessionKey))
				{
					// new
					_dict.Add(sess.SessionKey, sess);
				}
				else
				{
					// existing
					_dict[sess.SessionKey] = sess;
				}
				return sess;
			}, (sessionKey) =>
			{
				var n = incReadCount();
				Thread.Sleep(readSleepMs);
				Console.Out.WriteLine("read sessionKey: " + sessionKey + " n: " + n);
				if (!_dict.ContainsKey(sessionKey))
				{
					return default(Session);
				}
				return _dict[sessionKey];
			}, (sessionKey) =>
			{
				if (incTouchCount != null) incTouchCount();
				Thread.Sleep(touchSleepMs);
				// touch
				if (!_dict.ContainsKey(sessionKey))
				{
					throw new Exception("sessionKey does not exist. unable to touch...");
				}
			}, (sessionKey) =>
			{
				_dict.Remove(sessionKey);
			}, TimeSpan.FromMinutes(10), TimeSpan.FromMinutes(10),
			pollingInterval: TimeSpan.FromMilliseconds(10 * 1000));

			return store;
		}

		[Fact]
		public void TestCreateSession()
		{
			var saveCount = 0;
			var readCount = 0;
			var store = CreateStore(1, () =>
			{
				return Interlocked.Increment(ref saveCount);
			}, 1, () =>
			{
				return Interlocked.Increment(ref readCount);
			});

			Assert.Equal(0, saveCount);
			Assert.Equal(0, readCount);
			var sessionNum = store.Create("bob", "ipaddress");
			Assert.Equal(1, saveCount);
			Assert.Equal(0, readCount);

			Session sess;
			Assert.True(store.Access(sessionNum, out sess));
			Assert.Equal(1, saveCount);
			Assert.Equal(0, readCount); // value should be cached
		}

		//[Fact]
		//public void TestSetUsername()
		//{
		//	var saveCount = 0;
		//	var readCount = 0;
		//	var store = CreateStore(1, () =>
		//	{
		//		return Interlocked.Increment(ref saveCount);
		//	}, 1, () =>
		//	{
		//		return Interlocked.Increment(ref readCount);
		//	});
		//
		//	Session sess;
		//
		//	var sessionNum = store.Create("bob", "ipaddress");
		//	var ex = Record.Exception(() => store.SetUsername(sessionNum, "bob"));
		//	Assert.NotNull(ex);
		//
		//	sessionNum = store.Create(null);
		//	Assert.True(store.Access(sessionNum, out sess));
		//	Assert.Equal(null, sess.Username);
		//	ex = Record.Exception(() => store.SetUsername(sessionNum, "bob"));
		//	Assert.Null(ex);
		//	Assert.True(store.Access(sessionNum, out sess));
		//	Assert.Equal("bob", sess.Username);
		//}

		[Fact]
		public void TestTerminateSession()
		{
			var saveCount = 0;
			var readCount = 0;
			var store = CreateStore(1, () =>
			{
				return Interlocked.Increment(ref saveCount);
			}, 1, () =>
			{
				return Interlocked.Increment(ref readCount);
			});

			var sessionNum = store.Create("bob", "ipaddress");
			store.Terminate(sessionNum);
			Session sess;
			Assert.False(store.Access(sessionNum, out sess));
		}

		[Fact]
		public void TestConcurrentAccess()
		{
			var sleepMs = 100;
			byte[][] nums = null;

			var saveCount = 0;
			var readCount = 0;
			var touchCount = 0;
			var afterLockCount = 0;
			new Thread(() =>
			{
				var store = CreateStore(0, () =>
				{
					return Interlocked.Increment(ref saveCount);
				}, 10 * 1000, () =>
				{
					return Interlocked.Increment(ref readCount);
				}, sleepMs, () =>
				{
					return Interlocked.Increment(ref touchCount);
				});

				nums = new byte[][] {
					store.Create("bob", "ipaddress"),
					store.Create("bob", "ipaddress"),
					store.Create("bob", "ipaddress"),
					store.Create("bob", "ipaddress"),
					store.Create("bob", "ipaddress"),
					store.Create("bob", "ipaddress"),
				};

				int length = nums.Length * 3;
				int hitCount = 0;
				ParameterizedThreadStart func = (obj) =>
				{
					Interlocked.Increment(ref hitCount);

					var n = (int)obj;
					var num = nums[n % nums.Length];
					var tryNum = (n / nums.Length);
					Console.Out.WriteLine(num + " try: " + tryNum + " " + DateTime.Now);
					Session session;
					Assert.True(store.Access(num, out session));
					Interlocked.Increment(ref afterLockCount);
					Console.Out.WriteLine(num + " try: " + tryNum + " done " + DateTime.Now);
				};

				RunThreads(nums.Length * 3, func);
			}).Start();

			Assert.Equal(0, afterLockCount);

			// wait for first round to go through
			Thread.Sleep(sleepMs + sleepMs / 10);
			Assert.Equal(nums.Length, afterLockCount);

			// wait for second round to go through
			Thread.Sleep(sleepMs + sleepMs / 10);
			Assert.Equal(nums.Length * 2, afterLockCount);

			// wait for third round to go through
			Thread.Sleep(sleepMs + sleepMs / 10);
			Assert.Equal(nums.Length * 3, afterLockCount);
		}
	}
}