using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSE.Lib.SseGpsDeviceAPI.Commands;
using SSE.Lib.SseGpsDeviceAPI.Helper;

namespace SSE.Lib.SseGpsDeviceAPI.Test
{
	[TestClass]
	public class ResponseERRTest
	{
		#region Properties
		private static ClientResponseERR _command1;
		private const string _COMMAND1_RESULT = "$ERR,00100231,{UTCTime},{Code},{Message},{UTCDate},R*{CheckSum}";
		#endregion Properties

		#region Private Methods

		private static void InitializeCommand1()
		{
			DateTime utcDateTime = DateTime.UtcNow;
			_command1 = new ClientResponseERR(string.Empty);
			_command1.AccountID = "00100231";
			_command1.MessageState = MessageState.RealTime;
			_command1.UTCEventDateTime = utcDateTime;
		}

		private static string GetSentenceWithTimeStamp(ClientResponseERR command)
		{
			/** Result. */
			var result = _COMMAND1_RESULT.Replace("{UTCTime}", command.GetUTCTime());
			result = result.Replace("{UTCDate}", command.GetUTCDate());
			result = result.Replace("{CheckSum}", command.CheckSum);

			/** Return result. */
			return result;
		}

		#endregion Private Methods

		#region Initialization Methods
		//[AssemblyInitialize]
		//public static void AssemblyInit(TestContext context)
		//{
		//	System.Diagnostics.Debug.WriteLine("ResponseSOSTest Assembly Init");
		//}

		[ClassInitialize]
		public static void ClassInit(TestContext context)
		{
			System.Diagnostics.Debug.WriteLine("ResponseSOSTest ClassInit");
			InitializeCommand1();

		}

		[TestInitialize]
		public void Initialize()
		{
			System.Diagnostics.Debug.WriteLine("ResponseSOSTest Initialize");
		}

		[TestCleanup]
		public void Cleanup()
		{
			System.Diagnostics.Debug.WriteLine("ResponseSOSTest TestMethodCleanup");
			InitializeCommand1();
		}

		[ClassCleanup]
		public static void ClassCleanup()
		{
			System.Diagnostics.Debug.WriteLine("ResponseSOSTest ClassCleanup");
		}

		//[AssemblyCleanup]
		//public static void AssemblyCleanup()
		//{
		//	System.Diagnostics.Debug.WriteLine("ResponseSOSTest AssemblyCleanup");
		//}

		#endregion Initialization Methods

		#region Unit Tests

		[TestMethod]
		public void ResponseERR_CheckSentenceDeviceID()
		{
			try
			{
				/** Get Sentence. */
				_command1.AccountID = string.Empty;
				_command1.GetSentence();

				Assert.Fail("The execution should not have made it this far.");
			}
			catch (ResponsePDEMissingProperty oEx)
			{
				Assert.IsTrue(oEx.PropertyName.Equals("DeviceID"), string.Format("The property that should have been tested is 'DeviceID', the property that threw the exception was '{0}'."
					, oEx.PropertyName));
			}
		}

		[TestMethod]
		public void ResponseERR_CheckResult()
		{
			var result = _command1.GetSentence();
			var compTo = GetSentenceWithTimeStamp(_command1);

			Assert.IsTrue(result.Equals(compTo), "The sentences do not match.");
		}

		#endregion Unit Tests
	}
}