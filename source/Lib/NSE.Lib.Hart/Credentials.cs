using System;
using SOS.Lib.Util.Cryptography;
using NXS.Lib;

namespace NSE.Lib.Hart
{
	public static class Credentials
	{
		#region Fields

		#region EndPointUri
		private static readonly object SyncEndPointUri = new object();
		private static string _endPointUri;
		public static string EndPointUri
		{
			get
			{
				if (String.IsNullOrEmpty(_endPointUri))
				{
					lock (SyncEndPointUri)
					{
						if (String.IsNullOrEmpty(_endPointUri))
						{
							_endPointUri = WebConfig.Instance.GetConfig("CreditReport_Vendor_HART_ENDPOINT");
						}
					}
				}

				// Return result
				return _endPointUri;
			}
		}

		#endregion EndPointUri

		#region CrUsername
		private static readonly object SyncCrUsernameCredentials = new object();
		private static string _crUsername;
		public static string CrUsername
		{
			get
			{
				if (String.IsNullOrEmpty(_crUsername))
				{
					lock (SyncCrUsernameCredentials)
					{
						if (String.IsNullOrEmpty(_crUsername))
						{
							_crUsername = WebConfig.Instance.GetConfig("CreditReport_Vendor_HART_CRP_UID");
						}
					}
				}

				// Return result
				return _crUsername;
			}
		}
		#endregion CrUsername

		#region CrPassword
		private static readonly object SyncCrPasswordCredentials = new object();
		private static string _crPassword;
		public static string CrPassword
		{
			get
			{
				if (String.IsNullOrEmpty(_crPassword))
				{
					lock (SyncCrPasswordCredentials)
					{
						if (String.IsNullOrEmpty(_crPassword))
						{
							_crPassword = WebConfig.Instance.GetConfig("CreditReport_Vendor_HART_CRP_PWD");
						}
					}
				}

				// Return result
				return _crPassword;
			}
		}
		#endregion CrPassword

		#endregion Fields
	}
}
