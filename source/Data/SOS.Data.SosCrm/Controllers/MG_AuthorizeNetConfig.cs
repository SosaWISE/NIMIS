using System;
using SOS.Lib.Util.Configuration;

namespace SOS.Data.SosCrm
{
	public partial class MG_AuthorizeNetConfig
	{
		#region Methods

		public string GetLiveApiLogin()
		{
			return Lib.Util.Cryptography.TripleDES.DecryptString(LiveApiLogin, null);
		}
		public string GetLiveTransactionKey()
		{
			return Lib.Util.Cryptography.TripleDES.DecryptString(LiveTransactionKey, null);
		}

		public string GetTestApiLogin()
		{
			return Lib.Util.Cryptography.TripleDES.DecryptString(TestApiLogin, null);
		}
		public string GetTestTransactionKey()
		{
			return Lib.Util.Cryptography.TripleDES.DecryptString(TestTransactionKey, null);
		}

		public string ApiLogin
		{
			get
			{
				if (!IsTestMode)
					return GetLiveApiLogin();
				
				// Default path of execution.
				return GetTestApiLogin();
			}
		}

		public string TransactionKey
		{
			get
			{
				if (!IsTestMode)
					return GetLiveTransactionKey();

				// Default path of execution.
				return GetTestTransactionKey();
			}
		}

		public bool IsTestMode
		{
			get
			{
				try
				{
					var sMode = ConfigurationSettings.Current.GetConfig("MerchantStatus").Equals("TestMode");

					return sMode;
				}
				catch (Exception oEx)
				{
					System.Diagnostics.Debug.WriteLine(string.Format("In MG_AuthorizeNetConfig Controller threw exception looking for 'IsTestMode':{0}"
						, oEx.Message));
				}

				return false;
			}
		}

		#endregion Methods
	}
}
