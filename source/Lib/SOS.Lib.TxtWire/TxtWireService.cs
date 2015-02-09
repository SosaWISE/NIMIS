using System;
using SOS.Lib.TxtWire.com.txtwire.www;
using SOS.Lib.Util.Configuration;
using System.Collections.Generic;

namespace SOS.Lib.TxtWire
{
	public class TxtWireService
	{
		#region .ctor

		static TxtWireService()
		{
			APIUsername = ConfigurationSettings.Current.GetConfig("TxtWireAPI_username");
			APIUsername = Util.Cryptography.TripleDES.DecryptString(APIUsername, null);
	
			APIPassword = ConfigurationSettings.Current.GetConfig("TxtWireAPI_password");
			APIPassword = Util.Cryptography.TripleDES.DecryptString(APIPassword, null);

			APIKey = ConfigurationSettings.Current.GetConfig("TxtWireAPI_key");
			APIKey = Util.Cryptography.TripleDES.DecryptString(APIKey, null);

			APIShortCode = ConfigurationSettings.Current.GetConfig("TxtWireShortcode");
			APIShortCode = Util.Cryptography.TripleDES.DecryptString(APIShortCode, null);

			APIGroupKeyword = ConfigurationSettings.Current.GetConfig("TxtWireGroup_keyword");
			APIGroupKeyword = Util.Cryptography.TripleDES.DecryptString(APIGroupKeyword, null);
		}

		protected static string APIPassword { get; set; }
		protected static string APIUsername { get; set; }
		protected static string APIKey { get; set; }
		protected static string APIShortCode { get; set; }
		protected static string APIGroupKeyword { get; set; }

		#endregion .ctor

		#region Member Properties

		private static TxtWireService _instance;

		public static TxtWireService Instance
		{
			get { return _instance ?? (_instance = new TxtWireService()); }
		}

		#endregion Member Properties

		#region Member Functions

		public bool SendMessage(string phoneString, string messageString, string customTicketsString, string statusUrlString)
		{
			/** Initialize. */
			var oBind = new WS_MessageBinding();
			var oRecipients = new List<WSRecipient>
			                  	{
			                  		new WSRecipient
			                  			{
			                  				sendTo = phoneString,
			                  				type = 1
			                  			}
			                  	};
			var list = new WSRecipientList {recipients = oRecipients.ToArray()};
			var auth = new WSAuthentication
			           	{
			           		username = APIUsername,
			           		password = APIPassword,
			           		api_key = APIKey,
			           		code = APIShortCode,
			           		keyword = APIGroupKeyword
			           	};

			WSMessageResponse oResponse = oBind.sendMessage(auth, messageString, list, customTicketsString, statusUrlString);

			/** Check for errors. */
			if (oResponse.result) return true;

			Console.WriteLine("Error Code: {0} | Message: {1}", oResponse.error_code, oResponse.message);

			/* Return result. */
			return false;
		}

		#endregion Member Functions
	}
}