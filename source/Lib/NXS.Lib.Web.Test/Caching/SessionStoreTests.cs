using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Text;

namespace NXS.Lib.Web.Caching.Test
{
	[TestClass]
	public class SessionStoreTests
	{
		[TestMethod]
		public void TestMethod1()
		{
			var _db = new Dictionary<string, Session>();

			var store = new SessionStore(
				createSession: (s) =>
				{
					_db.Add(s.SessionKey, s);
					return s;
				},
				readSession: (sessionKey) =>
				{
					if (_db.ContainsKey(sessionKey))
					{
						return _db[sessionKey];
					}
					return default(Session);
				},
				updateSession: (s) =>
				{
					if (_db.ContainsKey(s.SessionKey))
					{
						_db[s.SessionKey] = s;
					}
					else
					{
						_db.Add(s.SessionKey, s);
					}
				},
				terminateSession: (sessionKey) =>
				{
					_db.Remove(sessionKey);
				},
				slidingExpiration: TimeSpan.FromSeconds(20),
				memoryLimitMb: 1,
				physicalMemoryLimitPercent: 20,
				pollingInterval: TimeSpan.FromSeconds(5)
			);
			//string.Intern(
			//string.IsInterned(
			//var longString = "0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789" +
			//	"0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789" +
			//	"0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789" +
			//	"0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789" +
			//	"0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789" +
			//	"0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789" +
			//	"0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789" +
			//	"0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789" +
			//	"0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789" +
			//	"0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789";

			var count = 0;
			var t = new System.Threading.Thread(() =>
			{
				for (var i = 0; i < 100000; i++)
				{
					var storedCount = store.GetCount();

					store.Create("user" + (++count));
				};
			});
			t.Start();

			while (true)
			{
				System.Threading.Thread.Sleep(1);
			}
		}
	}
}
