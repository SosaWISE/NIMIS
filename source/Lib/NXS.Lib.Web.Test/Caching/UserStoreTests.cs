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
	public class UserStoreTests
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

		[Fact]
		public void TestConcurrentRequests()
		{
			var sleepMs = 1000;
			var names = new string[] { "a", "b", "c", "d", "e", "f" };

			var readCount = 0;
			var afterLockCount = 0;
			new Thread(() =>
			{
				var store = new UserStore((username) =>
				{
					var n = ++readCount; // should be locking by username //Interlocked.Increment(ref readCount);
					Console.Out.WriteLine("username: " + username + " n: " + n);
					Thread.Sleep(sleepMs);
					return new User()
					{
						Username = username + "-" + n.ToString(),
					};
				}, TimeSpan.FromMinutes(10), pollingInterval: TimeSpan.FromMilliseconds(10 * 1000));

				ParameterizedThreadStart func = (obj) =>
				{
					var n = (int)obj;
					var name = names[n % names.Length];
					var tryNum = (n / names.Length);
					Console.Out.WriteLine(name + " try: " + tryNum + " " + DateTime.Now);
					var user = store.Get(name);
					Interlocked.Increment(ref afterLockCount);
					Console.Out.WriteLine(name + " try: " + tryNum + " done " + DateTime.Now);
				};

				RunThreads(names.Length * 3, func);
			}).Start();

			Assert.Equal(0, afterLockCount);

			// wait for first round to go through
			Thread.Sleep(sleepMs / 2);
			Console.Out.WriteLine("waiting");
			Assert.Equal(0, afterLockCount);

			Thread.Sleep(sleepMs);
			Console.Out.WriteLine("done waiting");
			Assert.Equal(names.Length * 3, afterLockCount);
		}
	}
}