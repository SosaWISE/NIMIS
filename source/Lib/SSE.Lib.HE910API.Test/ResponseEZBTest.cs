using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSE.Lib.HE910API.Commands;
using SSE.Lib.HE910API.Helper;

namespace SSE.Lib.HE910API.Test
{
	/// <summary>
	/// Summary description for ResponseEZBTest
	/// </summary>
	[TestClass]
	public class ResponseEZBTest
	{
		#region Properties
		private static readonly ClientResponseEZB Command1 = new ClientResponseEZB();
		private const string _COMMAND1_RESULT = "$EZB,00100231,{UTCTime},4542.8106N,01344.2720W,2.25,338.0,3,0.0,0.02,0.01,{UTCDate},04,01,100,100,100,R*{CheckSum}";
		#endregion Properties

		#region Private Methods

		private static void InitializeCommand1()
		{
			DateTime utcDateTime = DateTime.UtcNow;
			Command1.DeviceID = "00100231";
			Command1.Lattitude = "4542.8106";
			Command1.NSIndicator = GPSIndicator.InstanceN;
			Command1.Longitude = "01344.2720";
			Command1.EWIndicator = GPSIndicator.InstanceW;
			Command1.HDOP = "2.25";
			Command1.Altitude = "338.0";
			Command1.Fix = "3";
			Command1.COG = "0.0";
			Command1.SpKm = "0.02";
			Command1.SpKn = "0.01";
			Command1.UTCEventTime = utcDateTime; // 240613
			Command1.NSat = "04";
			Command1.GForce = "01";
			Command1.Battery = "100";
			Command1.CellStrength = "100";
			Command1.GpsStrength = "100";
			Command1.MessageState = MessageState.RealTime;
		}

		private static string GetSentenceWithTimeStamp(ClientResponseEZB command)
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
		public void ResponseSOS_CheckSentenceDeviceID()
		{
			try
			{
				/** Get Sentence. */
				Command1.DeviceID = string.Empty;
				Command1.GetSentence();

				Assert.Fail("The execution should not have made it this far.");
			}
			catch (ResponsePDEMissingProperty oEx)
			{
				Assert.IsTrue(oEx.PropertyName.Equals("DeviceID"), string.Format("The property that should have been tested is 'DeviceID', the property that threw the exception was '{0}'."
					, oEx.PropertyName));
			}
		}

		[TestMethod]
		public void ResponseSOS_CheckResult()
		{
			var result = Command1.GetSentence();
			var compTo = GetSentenceWithTimeStamp(Command1);

			Assert.IsTrue(result.Equals(compTo), "The sentences do not match.");
		}

		#endregion Unit Tests

	}
}
