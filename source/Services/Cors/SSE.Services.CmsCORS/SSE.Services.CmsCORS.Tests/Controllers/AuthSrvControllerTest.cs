using Microsoft.VisualStudio.TestTools.UnitTesting;
using SOS.FunctionalServices.Contracts.Helper;
using SOS.Lib.Util;
using SOS.Services.Interfaces.Models;
using SSE.Services.CmsCORS.Controllers;
using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;

namespace SSE.Services.CmsCORS.Tests.Controllers
{
	[TestClass]
	public class AuthSrvControllerTest
	{
		#region Helper Methods

		public CmsCORSResult<SosSessionInfo<SseCmsUser>> GetASessionID()
		{
			return null;

			//// ** Init
			//var cntlr = new AuthSrvController();

			//var appInfo = new AuthSrvController.AppInfo
			//{
			//	AppToken = CORSSecurity.APP_TOKEN
			//};
			//CmsCORSResult<SosSessionInfo<SseCmsUser>> result = cntlr.SessionStart(appInfo);

			//// ** Return result.
			//return result;
		}

		#endregion Helper Methods

		[ClassInitialize]
		public static void ClassInit(TestContext context)
		{
			UnitTestingHelper.CmsCORS.IsActive = true;
			UnitTestingHelper.CmsCORS.IPAddress = "0.0.0.0";
			UnitTestingHelper.CmsCORS.Username = "PrivetteANDY";
			UnitTestingHelper.CmsCORS.Password = "Freedom!SOS";
		}

		[TestMethod]
		public void AuthSrvContollerSessionStart()
		{
			CmsCORSResult<SosSessionInfo<SseCmsUser>> result = GetASessionID();

			Assert.IsTrue(result.Code == (int)ErrorCodes.Success, "SessionStart Failed.");
		}

		//[TestMethod]
		//public void AuthSrvControllerAuthenticate()
		//{
		//	// ** Init
		//	CmsCORSResult<SosSessionInfo<SseCmsUser>> session = GetASessionID();
		//	var cntlr = new AuthSrvController();

		//	// ** Authenticate
		//	var userAuthInfo = new AuthSrvController.UserAuthInfo
		//		{
		//			SessionID = session.Value.SessionId,
		//			Username = UnitTestingHelper.CmsCORS.Username,
		//			Password = UnitTestingHelper.CmsCORS.Password
		//		};
		//	CmsCORSResult<SseCmsUser> authResult = cntlr.UserAuth(userAuthInfo);

		//	// ** Assert result.
		//	Assert.IsTrue(authResult.Code == (int)ErrorCodes.Success, "Authentication failed.");

		//}
	}
}
