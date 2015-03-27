using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NXS.Lib.Web;

namespace SOS.FunctionalServices.Test
{
	[TestClass]
	public class AuthServiceTests
	{
		[TestMethod]
		public void TestBob()
		{
			var service = new AuthService(null, null);
			//var userSession = new SystemUserIdentity();
			//service.RenewOrStartSession(ref userSession, "ipaddress");
		}
	}
}
