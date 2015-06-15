using System;
using System.Configuration;
using SOS.Data;
using NXS.Lib;
using System.IO;

namespace NSE.Lib.Hart.UT
{
	public class MainConfig
	{
		#region .ctor

		private MainConfig() { }

		#endregion .ctor

		#region Properties

		volatile private static MainConfig _instance;
		private static readonly object InstanceSync = new object();
		public static MainConfig Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (InstanceSync)
					{
						if (_instance == null)
						{
							_instance = new MainConfig();
						}
					}
				}

				// ** Return object
				return _instance;
			}
		}

		private static readonly object IsInitSyn = new object();
		volatile private bool _isInit;

		#endregion Properties

		#region Methods

		public void Initialize()
		{
			if (!_isInit)
			{
				lock (IsInitSyn)
				{
					if (!_isInit)
					{
						// Load webconfig from SSE.Services.CmsCORS
						WebConfig.Init(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../../Services/Cors/SSE.Services.CmsCORS/SSE.Services.CmsCORS"), val =>
						{
							var decryptedVal = SOS.Lib.Util.Cryptography.TripleDES.DecryptString(val, null);
							// if decryption failed, return passed in value
							return decryptedVal.StartsWith("Error: ") ? val : decryptedVal;
						});
						// Setup SubSonic Connections
						var host = WebConfig.Instance.GetConfig("DBHost");
						var username = WebConfig.Instance.GetConfig("DBUsername");
						var password = WebConfig.Instance.GetConfig("DBPassword");
						var appName = WebConfig.Instance.GetConfig("ApplicationName");
						SubSonicConfigHelper.SetupConnectionStrings(host, username, password, appName);

						/** Initialize Fos Engine. */
						SOS.FunctionalServices.SosServiceEngine.Instance.Initialize();

						// ** Set the init on so it does not do this again.
						_isInit = true;
					}
				}
			}
		}

		#endregion Methods
	}
}
