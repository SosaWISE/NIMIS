using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SOS.Lib.LaipacAPI.Tests
{
	[TestClass]
	public class CheckSumTest1
	{
		[TestMethod]
		public void CheckSum()
		{
			var sSentence = "AVREQ,?";
			var sTestValue = "42";

			/** Basic Request test. */
			var result = Helper.SentenceParser.GetCheckSum(sSentence);
			Assert.IsTrue(result.Equals(sTestValue)
				, string.Format("Sorry ChkSum returned {0} it should have been {1}"
				, result, sTestValue));

			/** Basic Response test. */
			sSentence = "AVSYS,99999999,V1.17,SN0000103,32768";
			sTestValue = "16";
			result = Helper.SentenceParser.GetCheckSum(sSentence);
			Assert.IsTrue(result.Equals(sTestValue)
				, string.Format("Sorry ChkSum returned {0} it should have been {1}"
				, result, sTestValue));

			/** Add-hock tests 001. */
			sSentence = "AVREQ,00000000,1";
			sTestValue = "60";
			result = Helper.SentenceParser.GetCheckSum(sSentence);
			Assert.IsTrue(result.Equals(sTestValue)
				, string.Format("Sorry ChkSum returned {0} it should have been {1}"
				, result, sTestValue));

			/** Add-hock tests 002. */
			sSentence = "AVRMC,99999999,164339,A,4351.0542,N,07923.5445,W,0.29,78.66,180703,X,3.727,17,1,0,0";
			sTestValue = "5F";
			result = Helper.SentenceParser.GetCheckSum(sSentence);
			Assert.IsTrue(result.Equals(sTestValue)
				, string.Format("Sorry ChkSum returned {0} it should have been {1}"
				, result, sTestValue));
		}
	}
}
