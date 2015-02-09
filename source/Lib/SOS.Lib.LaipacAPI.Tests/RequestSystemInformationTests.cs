using Microsoft.VisualStudio.TestTools.UnitTesting;
using SOS.Lib.LaipacAPI.Commands;

namespace SOS.Lib.LaipacAPI.Tests
{
	[TestClass]
	public class RequestSystemInformationTests
	{
		[TestMethod]
		public void TestRequests()
		{
			/** Initialize. */
			//const string S_PHONE_NUMBER = "15135107527"; // My device
			const string S_PHONE_NUMBER = "13134676579";
			string sPassword = "00000000";
			var sMessage = SystemInfoRequest.GetRequestStatic();
			Assert.IsTrue(TxtWire.TxtWireService.Instance.SendMessage(S_PHONE_NUMBER, sMessage, null, null), "Sending the message failed");

			sMessage = CurrentPhoneNumberRequest.GetRequest(sPassword);
			Assert.IsTrue(TxtWire.TxtWireService.Instance.SendMessage(S_PHONE_NUMBER, sMessage, null, null), "Sending the message failed");

			//sPassword = "00000";
			sMessage = CurrentPositionRequest.GetRequest(sPassword);
			Assert.IsTrue(TxtWire.TxtWireService.Instance.SendMessage(S_PHONE_NUMBER, sMessage, null, null), "Sending the message failed");
		}
	}
}