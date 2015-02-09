using System.Collections.Specialized;
using System.Configuration;

namespace SOS.Lib.Util.Configuration
{
	public sealed class ConfigurationSettings
	{
		#region Constructors

		public ConfigurationSettings()
		{
			Initialize();
		}

		public ConfigurationSettings(string szSectionName, string szMachineName)
		{
			Initialize(szSectionName, szMachineName);
		}

		#endregion Constructors

		#region Constants

		private const string _DEFAULT_SECTION = "Preferences";

		private const string _DEFAULT_MACHINE_NAME = "PROD";

		#endregion Constants

		#region Properties

		#region Private

		#region Static

		private static volatile ConfigurationSettings _instance;
		private static readonly object _syncRoot = new object();
		private static NameValueCollection _keyValueColl;
		private static string _sectionName = string.Empty;
		private static string _environment = string.Empty;

		#endregion Static

		#endregion Private

		#region Public

		#region Static

		public static ConfigurationSettings Current
		{
			get
			{
				if (_instance == null)
				{
					lock (_syncRoot)
					{
						if (_instance == null)
							_instance = new ConfigurationSettings();
					}
				}

				// Return result
				return _instance;
			}
		}

		public string Section
		{
			get { return _sectionName; }
			set { _sectionName = value; }
		}

		public string Environment
		{
			get { return _environment; }
			set { _environment = value; }
		}

		#endregion Static

		#endregion Public

		#endregion Properties

		#region Methods

		#region Private Static Methods

		private static void Initialize()
		{
			if (_sectionName.Equals(string.Empty))
				_sectionName = _DEFAULT_SECTION;

			if (_environment.Equals(string.Empty))
				_environment = _DEFAULT_MACHINE_NAME;

			Initialize(_sectionName, _environment);
		}

		private static void Initialize(string szSection, string szEnvironment)
		{
			// Set the properties to the correct values.
			_sectionName = szSection;
			_environment = szEnvironment;

			// Get the Collection Values
			_keyValueColl = (NameValueCollection) ConfigurationManager.GetSection(szSection + "/" + szEnvironment);
		}

		public static bool DoesSectionExists(string szSection, string szEnvironment)
		{
			// Check 
			var oKeyValue = ConfigurationManager.GetSection(szSection + "/" + szEnvironment);

			// Return result
			return oKeyValue != null;
		}

		#endregion Private Static Methods

		#region Public Static Methods

		public bool HasConfigValue(string szName)
		{
			if (_keyValueColl == null)
				Initialize();
			return _keyValueColl[szName] != null;
		}

		public string GetConfig(string szName)
		{
			if (_keyValueColl == null)
				Initialize();
			return _keyValueColl[szName];
		}

		public void SetProperties()
		{
			SetProperties(_sectionName, _environment);
		}

		public void SetProperties(string szSectionName, string szMachineName)
		{
			_sectionName = szSectionName;
			_environment = szMachineName;

			Initialize(_sectionName, _environment);
		}

		#endregion Public Static Methods

		#endregion Methods
	}
}