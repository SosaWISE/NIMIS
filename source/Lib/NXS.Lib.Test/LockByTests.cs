using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NXS.Lib.Test
{
	public class LockByTests
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
		public void TestLockBy()
		{
			var sleepMs = 100;
			var names = new string[] { "a", "b", "c", "d", "e", "f" };
			var nameLocker = new LockBy<string>();

			var afterLockCount = 0;
			ParameterizedThreadStart func = (obj) =>
			{
				var n = (int)obj;
				var name = names[n % names.Length];
				var tryNum = (n / names.Length);
				Console.Out.WriteLine(name + " try: " + tryNum + " " + DateTime.Now);
				nameLocker.Lock(name, () =>
				{
					Thread.Sleep(sleepMs);
				});
				Interlocked.Increment(ref afterLockCount);
				Console.Out.WriteLine(name + " try: " + tryNum + " done " + DateTime.Now);
			};

			RunThreads(names.Length * 3, func);
			Assert.Equal(0, afterLockCount);

			// wait for first round to go through
			Thread.Sleep(sleepMs + sleepMs / 10);
			Assert.Equal(names.Length, afterLockCount);

			// wait for second round to go through
			Thread.Sleep(sleepMs + sleepMs / 10);
			Assert.Equal(names.Length * 2, afterLockCount);

			// wait for third round to go through
			Thread.Sleep(sleepMs + sleepMs / 10);
			Assert.Equal(names.Length * 3, afterLockCount);
		}
	}
}