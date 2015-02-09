using System;
using System.Configuration;
using SOS.Data;

namespace NXS.FOS.DocumentServicesUT.Helpers
{
	public class InitializeAndConfigure
	{
		#region .ctor

		private InitializeAndConfigure()
		{
			/** Init block. */
			string sEnvironmentToUse = ConfigurationManager.AppSettings["Environment"] ?? Environment.MachineName;
			SOS.Lib.Util.Configuration.ConfigurationSettings.Current.SetProperties("Preferences", sEnvironmentToUse);

			// Setup SubSonic Connections
			SubSonicConfigHelper.SetupConnectionStrings();

			/** Initialize Fos Engine. */
//			FunctionalServices.SosServiceEngine.Instance.Initialize();

		}

		#endregion .ctor

		#region Properties

		private static readonly object _instanceSync = new object();
		private static volatile InitializeAndConfigure _instance;
		public static InitializeAndConfigure Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_instanceSync)
					{
						if (_instance == null)
							return _instance = new InitializeAndConfigure();
					}
				}

				/** Return result. */
				return _instance;
			}
		}
		#endregion Properties

		#region Methods

		public void Initialize()
		{
		}

		#endregion Methods
	}
}
