using SOS.Lib.Core;
using SOS.Lib.Util.Configuration;
using SOS.Lib.Util.Cryptography;
using SubSonic;
using System;
using System.Collections.Generic;

namespace SOS.Data
{
	public static class SubSonicConfigHelper
	{
		#region Constants

		// Provider Keys
		public const string SOS_CRM_PROVIDER_NAME = "SosCrmProvider";
		public const string SOS_AUTH_CONTROL_PROVIDER_NAME = "SosAuthControlProvider";
		public const string LOGGING_PROVIDER_NAME = "SosLoggingProvider";
		public const string SOS_RECEIVER_ENGINE_PROVIDER_NAME = "SosReceiverEngineProvider";
		public const string SOS_HUMAN_RESOURCE_PROVIDER_NAME = "SosHumanResourceProvider";
		public const string SOS_GPS_TRACKING_PROVIDER_NAME = "SosGpsTrackingProvider";
		public const string SSE_SURVEY_ENGINE_PROVIDER_NAME = "SseSurveyEngineProvider";
		public const string NXS_FILE_STORE_PROVIDER_NAME = "NxsFileStoreProvider";
		public const string NXS_INVENTORY_PROVIDER_NAME = "NxsInventoryProvider";
		public const string NXS_LICENSING_PROVIDER_NAME = "NxsLicensingProvider";
		public const string NXS_ACCOUNTING_PROVIDER_NAME = "NxsAccountingProvider";
		public const string NXS_LETTERS_PROVIDER_NAME = "NxsLettersProvider";
		public const string NXS_GREATPLAINS_PROVIDER_NAME = "NxsGreatPlainsProvider";

		// Connection String Keys
		public const string SOS_CMS_CONN_STRING_KEY = "SosCrmConnString";
		public const string SOS_AUTH_CONTROL_CONN_STRING_KEY = "SosAuthControlConnString";
		public const string LOGGING_CONN_STRING_KEY = "SosLoggingConnString";
		public const string SOS_RECEIVER_LINE_CONN_STRING_KEY = "SosReceiverEngineConnString";
		public const string SOS_HUMAN_RESOURCE_CONN_STRING_KEY = "SosHumanResourceConnString";
		public const string SOS_GPS_TRACKING_CONN_STRING_KEY = "SosGpsTrackingConnString";
		public const string SSE_SURVEY_ENGINE_CONN_STRING_KEY = "SseSurveyEngineConnString";
		public const string NXS_FILE_STORE_CONN_STRING_KEY = "NxsFileStoreConnString";
		public const string NXS_INVENTORY_CONN_STRING_KEY = "NxsInventoryConnString";
		public const string NXS_LICENSING_CONN_STRING_KEY = "NxsLicensingConnString";
		public const string NXS_ACCOUNTING_CONN_STRING_KEY = "NxsAccountingConnString";
		public const string NXS_LETTERS_CONN_STRING_KEY = "NxsLettersConnString";
		public const string NXS_GREATPLAINS_CONN_STRING_KEY = "NxsGreatPlainsConnString";

		// Application Keys
		public const string LOG_SOURCE_KEY = "LogSourceID";
		public const string FILE_SOURCE_KEY = "FileSourceID";

		// Key to use when all connection strings are the same (except for the database name)
		public const string BASECONNECTIONSTRING_KEY = "BaseConnectionString";
		public static readonly Dictionary<string, string> ProviderDatabaseDict = new Dictionary<string, string>
		{
			{SOS_CRM_PROVIDER_NAME, "WISE_CRM"},
			{SOS_AUTH_CONTROL_PROVIDER_NAME, "WISE_AuthenticationControl"},
			{LOGGING_PROVIDER_NAME, "WISE_Logging"},
			{SOS_RECEIVER_ENGINE_PROVIDER_NAME, "WISE_Receiver"},
			{SOS_HUMAN_RESOURCE_PROVIDER_NAME, "WISE_HumanResource"},
			{SOS_GPS_TRACKING_PROVIDER_NAME, "WISE_GPSTRACKING"},
			{SSE_SURVEY_ENGINE_PROVIDER_NAME, "WISE_SurveyEngine"},
			{NXS_FILE_STORE_PROVIDER_NAME, "NXSE_FileStore"},
			{NXS_INVENTORY_PROVIDER_NAME, "NXSE_Inventory"},
			{NXS_LICENSING_PROVIDER_NAME, "NXSE_Licensing"},
			{NXS_ACCOUNTING_PROVIDER_NAME, "NXSE_Accounting"},
			{NXS_LETTERS_PROVIDER_NAME, "NXSE_Letters"},
			{NXS_GREATPLAINS_PROVIDER_NAME, "NXSE_GreatPlains"}
		};

		#endregion Constants

		#region Methods

		#region Public

		public static void SetupConnectionStrings()
		{
			DataService.LoadProviders();

			// check for a BaseConnectionString
			if (ConfigurationSettings.Current.HasConfigValue(BASECONNECTIONSTRING_KEY))
			{
				var baseConnectionString = GetConnectionString(BASECONNECTIONSTRING_KEY);
				// ensure it ends with a semicolon
				if (!baseConnectionString.EndsWith(";"))
				{
					baseConnectionString += ";";
				}
				foreach (var kvp in ProviderDatabaseDict)
				{
					var providerName = kvp.Key;
					if (DataService.Providers[providerName] != null)
					{
						DataService.Providers[providerName].DefaultConnectionString = string.Format("{0}Initial Catalog={1}", baseConnectionString, kvp.Value);
					}
				}

				// connection strings are set, so skip below code
				return;
			}

			SetProviderConnectionString(SOS_CRM_PROVIDER_NAME, SOS_CMS_CONN_STRING_KEY);
			SetProviderConnectionString(SOS_AUTH_CONTROL_PROVIDER_NAME, SOS_AUTH_CONTROL_CONN_STRING_KEY);
			SetProviderConnectionString(LOGGING_PROVIDER_NAME, LOGGING_CONN_STRING_KEY);
			SetProviderConnectionString(SOS_RECEIVER_ENGINE_PROVIDER_NAME, SOS_RECEIVER_LINE_CONN_STRING_KEY);
			SetProviderConnectionString(SOS_HUMAN_RESOURCE_PROVIDER_NAME, SOS_HUMAN_RESOURCE_CONN_STRING_KEY);
			SetProviderConnectionString(SOS_GPS_TRACKING_PROVIDER_NAME, SOS_GPS_TRACKING_CONN_STRING_KEY);
			SetProviderConnectionString(SSE_SURVEY_ENGINE_PROVIDER_NAME, SSE_SURVEY_ENGINE_CONN_STRING_KEY);
			SetProviderConnectionString(NXS_FILE_STORE_PROVIDER_NAME, NXS_FILE_STORE_CONN_STRING_KEY);
			SetProviderConnectionString(NXS_INVENTORY_PROVIDER_NAME, NXS_INVENTORY_CONN_STRING_KEY);
			SetProviderConnectionString(NXS_LICENSING_PROVIDER_NAME, NXS_LICENSING_CONN_STRING_KEY);
			SetProviderConnectionString(NXS_ACCOUNTING_PROVIDER_NAME, NXS_ACCOUNTING_CONN_STRING_KEY);
		}

		static void SetProviderConnectionString(string providerName, string key)
		{
			if (DataService.Providers[providerName] != null && ConfigurationSettings.Current.HasConfigValue(key))
			{
				DataService.Providers[providerName].DefaultConnectionString = GetConnectionString(key);
			}
		}

		static string GetConnectionString(string key)
		{
			string configValue = ConfigurationSettings.Current.GetConfig(key);
			if (configValue.Contains("Data Source=") || configValue.Contains("Server="))
			{
				return configValue;
			}
			// Default path of execution.
			return TripleDES.DecryptString(configValue, null);
		}

		public static LogSource GetLogSourceFromConfig()
		{
			var szSourceValue = ConfigurationSettings.Current.GetConfig(LOG_SOURCE_KEY);
			if (!string.IsNullOrEmpty(szSourceValue))
				return (LogSource)Enum.Parse(typeof(LogSource), szSourceValue);

			// Default path
			return LogSource.Default;
		}

		public static FileSource GetFileSourceFromConfig()
		{
			var szSourceValue = ConfigurationSettings.Current.GetConfig(FILE_SOURCE_KEY);

			if (!string.IsNullOrEmpty(szSourceValue))
				return (FileSource)Enum.Parse(typeof(FileSource), szSourceValue);

			// Default path
			return FileSource.Default;
		}

		#endregion Public

		#endregion Methods
	}
}
