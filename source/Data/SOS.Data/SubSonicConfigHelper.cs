using NXS.Data;
using NXS.Lib;
using SOS.Lib.Core;
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
		public const string NXS_CONNEXT_PROVIDER_NAME = "NxsConnextProvider";
		public const string NXS_FUNDING_PROVIDER_NAME = "NxsFundingProvider";

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
		public const string NXS_CONNEXT_CONN_STRING_KEY = "NxsConnextConnString";
		public const string NXS_FUNDING_CONN_STRING_KEY = "NxsFundingConnString";

		// Application Keys
		public const string LOG_SOURCE_KEY = "LogSourceID";
		public const string FILE_SOURCE_KEY = "FileSourceID";

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
			{NXS_GREATPLAINS_PROVIDER_NAME, "NXSE_GreatPlains"},
			{NXS_CONNEXT_PROVIDER_NAME, "NXSE_Connext"},
			{NXS_FUNDING_PROVIDER_NAME, "NXSE_Funding"}
		};

		#endregion Constants

		#region Methods

		#region Public

		public static void SetupConnectionStrings(string host = null, string username = null, string password = null, string appName = null)
		{
			DataService.LoadProviders();

			// when all connection strings are the same (except for the database name)
			if (!string.IsNullOrEmpty(host) && !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(appName))
			{
				foreach (var kvp in ProviderDatabaseDict)
				{
					var providerName = kvp.Key;
					if (DataService.Providers[providerName] != null)
					{
						DataService.Providers[providerName].DefaultConnectionString = DatabaseHelper.FormatConnectionString(kvp.Value, host, username, password, appName);
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
			SetProviderConnectionString(NXS_CONNEXT_PROVIDER_NAME, NXS_CONNEXT_CONN_STRING_KEY);
			SetProviderConnectionString(NXS_FUNDING_PROVIDER_NAME, NXS_FUNDING_CONN_STRING_KEY);
		}

		static void SetProviderConnectionString(string providerName, string key)
		{
			if (DataService.Providers[providerName] != null && WebConfig.Instance.HasConfigValue(key))
			{
				DataService.Providers[providerName].DefaultConnectionString = WebConfig.Instance.GetConfig(key);
			}
		}

		public static LogSource GetLogSourceFromConfig()
		{
			var szSourceValue = WebConfig.Instance.GetConfig(LOG_SOURCE_KEY);
			if (!string.IsNullOrEmpty(szSourceValue))
				return (LogSource)Enum.Parse(typeof(LogSource), szSourceValue);

			// Default path
			return LogSource.Default;
		}

		public static FileSource GetFileSourceFromConfig()
		{
			var szSourceValue = WebConfig.Instance.GetConfig(FILE_SOURCE_KEY);

			if (!string.IsNullOrEmpty(szSourceValue))
				return (FileSource)Enum.Parse(typeof(FileSource), szSourceValue);

			// Default path
			return FileSource.Default;
		}

		#endregion Public

		#endregion Methods
	}
}
