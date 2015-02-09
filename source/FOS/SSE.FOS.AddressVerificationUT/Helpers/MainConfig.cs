using System;
using System.Configuration;
using SOS.Data;
using ConfigurationSettings = SOS.Lib.Util.Configuration.ConfigurationSettings;

namespace SSE.FOS.AddressVerificationUT.Helpers
{
	public class MainConfig
	{
		#region .ctor

		private MainConfig() {}

		#endregion .ctor

		#region Properties

		volatile private static MainConfig _instance;
		private static readonly object _instanceSync = new object();
		public static MainConfig Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_instanceSync)
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

		private static readonly object _isInitSyn = new object();
		volatile private bool _isInit;

		#endregion Properties

		#region Methods

		public void Initialize()
		{
			if (!_isInit)
			{
				lock (_isInitSyn)
				{
					if (!_isInit)
					{
						string environment = ConfigurationManager.AppSettings["Environment"] ?? Environment.MachineName;
						ConfigurationSettings.Current.SetProperties("Preferences", environment);

						// Setup SubSonic Connections
						SubSonicConfigHelper.SetupConnectionStrings();

						// ** Set the init on so it does not do this again.
						_isInit = true;
					}
				}
			}
		}

		#endregion Methods
	}
}
