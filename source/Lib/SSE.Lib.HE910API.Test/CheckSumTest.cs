using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSE.Lib.HE910API.ExceptionHandling;

namespace SSE.Lib.HE910API.Test
{
	[TestClass]
	public class CheckSumTest
	{
		[TestMethod]
		public void RawSentenceToSentenceTest()
		{
			/** Inititalize. */
			const string SENTENCE = "alsdkf alskf alkdfjalksdfjal dklakj";

			try
			{
				Helper.SentenceParser.RawSentenceToSentence(String.Format("4343"));
				Assert.IsFalse(true, "Validation failed.  The sentence should not be less than 3 characters long.");
			}
			catch (HE910SentenceLengthException oEx)
			{
				Assert.IsTrue(true, oEx.Message);	
			}

			try
			{
				var temp = Helper.SentenceParser.RawSentenceToSentence(String.Format("${0}*34343", SENTENCE));
				Assert.IsTrue(temp[0].Equals(SENTENCE), "The result does not match the sentence.");
			}
			catch (Exception oEx)
			{
				Assert.IsFalse(true, oEx.Message);
			}

			/** Check for the $ sign. */
			try
			{
				Helper.SentenceParser.RawSentenceToSentence(String.Format("{0}*34343", SENTENCE));
			}
			catch (HE910SentenceMissingDollarSign oEx)
			{
				Assert.IsTrue(true, oEx.Message);
			}

			/** Check for the * sign. */
			try
			{
				Helper.SentenceParser.RawSentenceToSentence(String.Format("${0}34343", SENTENCE));
			}
			catch (HE910SentenceMissingChkSum oEx)
			{
				Assert.IsTrue(true, oEx.Message);
			}

		}

		[TestMethod]
		public void GetCheckSumRawSentenceTestGood()
		{
			/** Test 001. */
			string sentance = "AVRMC,90007200,005142,r,4019.1472,N,11140.5435,W,0.00,0.00,151112,0,3774,27,1,0,0";
			string chkSum = "14";
			var chkSumVal = Helper.SentenceParser.GetCheckSumRawSentence(string.Format("${0}*14", sentance));
			Assert.IsTrue(chkSumVal.Equals(chkSum), "'GetCheckSumRawSentence Test 001' failed checksum test.");

			/** Test 002. */
			sentance = "AVRSP,99999999,00";
			chkSum = "46";
			chkSumVal = Helper.SentenceParser.GetCheckSumRawSentence(string.Format("${0}*46", sentance));
			Assert.IsTrue(chkSumVal.Equals(chkSum), "'GetCheckSumRawSentence Test 002' failed checksum test.");

			/** Test 003. */
			sentance = "AVSYS,99999999,V1.17,SN0000103,32768";
			chkSum = "16";
			chkSumVal = Helper.SentenceParser.GetCheckSumRawSentence(string.Format("${0}*16", sentance));
			Assert.IsTrue(chkSumVal.Equals(chkSum), "'GetCheckSumRawSentence Test 003' failed checksum test.");

			/** Test 004. */
			sentance = "AVREQ,00000000,0";
			chkSum = "61";
			chkSumVal = Helper.SentenceParser.GetCheckSumRawSentence(string.Format("${0}*61", sentance));
			Assert.IsTrue(chkSumVal.Equals(chkSum), "'GetCheckSumrRawSentence Test 004' failed checksum test.");

			/** Test 005. */
			sentance = "AVALL,99999999,00,3";
			chkSum = "49";
			chkSumVal = Helper.SentenceParser.GetCheckSumRawSentence(string.Format("${0}*49", sentance));
			Assert.IsTrue(chkSumVal.Equals(chkSum), "'GetCheckSumrRawSentence Test 005' failed checksum test.");

		}

		[TestMethod]
		public void GetCheckSumRawSentenceTestBad()
		{
			/** Initialize. */
			const string SENTANCE = "A,90007200,005142,r,4019.1472,N,11140.5435,W,0.00,0.00,151112,0,3774,27,1,0,0";

			var chkSum = Helper.SentenceParser.GetCheckSumRawSentence(string.Format("${0}*14", SENTANCE));

			Assert.IsFalse(chkSum.Equals("14"), "'GetCheckSumRawSentence' failed checksum test.");
		}

		[TestMethod]
		public void GetCheckSumTestGood()
		{
			/** Test 001. */
			string sentance = "AVRMC,90007200,005142,r,4019.1472,N,11140.5435,W,0.00,0.00,151112,0,3774,27,1,0,0";
			string chkSum = "14";
			var chkSumVal = Helper.SentenceParser.GetCheckSum(sentance);
			Assert.IsTrue(chkSumVal.Equals(chkSum), "'GetCheckSumRawSentence Test 001' failed checksum test.");

			/** Test 002. */
			sentance = "AVRSP,99999999,00";
			chkSum = "46";
			chkSumVal = Helper.SentenceParser.GetCheckSum(sentance);
			Assert.IsTrue(chkSumVal.Equals(chkSum), "'GetCheckSumRawSentence Test 002' failed checksum test.");

			/** Test 003. */
			sentance = "AVSYS,99999999,V1.17,SN0000103,32768";
			chkSum = "16";
			chkSumVal = Helper.SentenceParser.GetCheckSum(sentance);
			Assert.IsTrue(chkSumVal.Equals(chkSum), "'GetCheckSumRawSentence Test 003' failed checksum test.");

			/** Test 004. */
			sentance = "AVREQ,00000000,0";
			chkSum = "61";
			chkSumVal = Helper.SentenceParser.GetCheckSum(sentance);
			Assert.IsTrue(chkSumVal.Equals(chkSum), "'GetCheckSumrRawSentence Test 004' failed checksum test.");

			/** Test 005. */
			sentance = "AVALL,99999999,00,3";
			chkSum = "49";
			chkSumVal = Helper.SentenceParser.GetCheckSum(sentance);
			Assert.IsTrue(chkSumVal.Equals(chkSum), "'GetCheckSumrRawSentence Test 005' failed checksum test.");
		}
	}
}
