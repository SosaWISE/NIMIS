using System;
using System.Configuration;
using ConfigurationSettings = SOS.Lib.Util.Configuration.ConfigurationSettings;

namespace SOS.Clients.Console.TxtWire
{
	class Program
	{
		/**
		 * login

			username: 8018229323
			password: andres123

		 * To access the API please do the following:
			API username: wisearchitects
			API password: letmein123!
			API key:  79c1a6306719a6ac24e418eda76533e8c50a0b60
			shortcode is 88588
			group keyword is: t4pnhw

*/
		#region ENTRY POINT

		static void Main(string[] args)
		{
			/** Initialize. */
			string environment = ConfigurationManager.AppSettings["Environment"] ?? Environment.MachineName;
			ConfigurationSettings.Current.SetProperties("Preferences", environment);

			/** Check that there are two arguments. */
			if (args.Length > 1)
			{
				_listOfNumbers = args[0].Split(',');

				_message = args[1];

				foreach (var sPhoneNumber in _listOfNumbers)
				{
					SendMessage(sPhoneNumber, _message);
				}

				System.Console.Write("Hit Enter to end.");
				System.Console.ReadLine();
			}
			else
			{
				System.Console.WriteLine(@"Please enter the write argumetns in the following formation:

[Comma Separated list of phone numbers] [Message to send]");
			}
		}

		#endregion ENTRY POINT

		#region Memeber Properties

		private static string[] _listOfNumbers;
		private static string _message;

		#endregion Memeber Properties

		#region Member Functions

		public static void SendMessage (string sPhoneNumber, string sMessage)
		{
			System.Console.WriteLine("Number: {0} | Message: {1} | Success: {2}", sPhoneNumber, sMessage,
				Lib.TxtWire.TxtWireService.Instance.SendMessage(sPhoneNumber, sMessage, null, null));
		}

		public static void UnitTest()
		{
			var someMessage = "$AVERQ,?*"; // 42
			System.Console.WriteLine("{0}:{1}", someMessage, Lib.LaipacAPI.Helper.SentenceParser.GetCheckSumRawSentence(someMessage));
			someMessage = "$AVSYS,99999999,V1.17,SN0000103,32768*"; // 16
			System.Console.WriteLine("{0}:{1}", someMessage, Lib.LaipacAPI.Helper.SentenceParser.GetCheckSumRawSentence(someMessage));
			someMessage = "$AVREQ,00000000,0*"; // 16
			System.Console.WriteLine("{0}:{1}", someMessage, Lib.LaipacAPI.Helper.SentenceParser.GetCheckSumRawSentence(someMessage));
			someMessage = "$AVRMC,80000551,144811,A,4351.3789,N,07923.4712,W,0.00,153.45,091107,A,,161,1*"; // 64
			System.Console.WriteLine("{0}:{1}", someMessage, Lib.LaipacAPI.Helper.SentenceParser.GetCheckSumRawSentence(someMessage));
			someMessage = "$AVRMC,80000551,144811,A,4351.3789,N,07923.4712,W,0.00,153.45,091107,A,,161,1,0,0*"; // 64
			System.Console.WriteLine("{0}:{1}", someMessage, Lib.LaipacAPI.Helper.SentenceParser.GetCheckSumRawSentence(someMessage));
			someMessage = "$AVRMC,99999999,164339,A,4351.0542,N,07923.5445,W,0.29,78.66,180703,X,3.727,17,1,0,0*"; // 5F
			System.Console.WriteLine("{0}:{1}", someMessage, Lib.LaipacAPI.Helper.SentenceParser.GetCheckSumRawSentence(someMessage));

		}

		#endregion Member Functions
	}
}