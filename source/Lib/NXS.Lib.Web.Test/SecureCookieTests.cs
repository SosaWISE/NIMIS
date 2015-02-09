using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Collections.Generic;

namespace NXS.Lib.Web.Test
{
	[TestClass]
	public class SecureCookieTests
	{
		[TestMethod]
		public void TestSecureCookie()
		{
			var encoding = Encoding.UTF8;

			var s1 = new SecureCookie(encoding.GetBytes("12345123451234512345123451234512"), encoding.GetBytes("12345678901234561234567890123456"));
			var s2 = new SecureCookie(encoding.GetBytes("54321543215432154321543215432154"), encoding.GetBytes("65432109876543216543210987654321"));
			var value = new Dictionary<string, object>(){
				{"baz", (object)(long)128},
				{"foo", "bar"}
			};

			s1.MinAge = 2;
			for (var i = 0; i < 50; i++)
			{
				// Running this multiple times to check if any special character
				// breaks encoding/decoding.
				var encoded = s1.Encode("sid", value);
				if (s1.MinAge > 0)
				{
					System.Threading.Thread.Sleep(s1.MinAge);
				}
				var dst = s1.Decode<Dictionary<string, object>>("sid", encoded);
				Assert.IsTrue(dst != null);

				Assert.AreEqual(dst.Count, value.Count);
				foreach (var kvp in value)
					Assert.AreEqual(dst[kvp.Key], kvp.Value);

				// s2 should not be able to decrypt cookie encrypted by s1
				var dst2 = s2.Decode<Dictionary<string, object>>("sid", encoded);
				Assert.IsFalse(dst2 != null);
			}
		}
	}
}
