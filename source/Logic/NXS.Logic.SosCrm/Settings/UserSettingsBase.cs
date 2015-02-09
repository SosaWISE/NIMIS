using System;
using System.Collections.Generic;
using NXS.Framework.Wpf.Settings;
using SOS.Data.SosCrm;
using SOS.Data.SosCrm.ControllerExtensions;
using SOS.Lib.Util;

namespace NXS.Logic.SosCrm.Settings
{
	public class UserSettingsBase
	{
		#region Fields

		protected UI_UserSettingsContainer _settings;
		protected UserSettingContainer _container;

		bool _initialized;

		#endregion //Fields

		#region Properties

		public int ApplicationID { get; private set; }
		public string Username { get; private set; }

		#endregion //Properties

		#region .ctors

		public UserSettingsBase(int applicationID, string username)
		{
			if (username == null)
				throw new Exception("username is null");
			username = StringUtility.NullIfWhiteSpace(username);
			if (username == null)
				throw new Exception("username is whitespace");

			this.ApplicationID = applicationID;
			this.Username = username;
		}

		public void Init()
		{
			_initialized = true;

			Exception exception = null;

			try
			{
				_settings = SosCrmDataContext.Instance.UI_UserSettingsContainers.GetUserSettingsForApp(this.ApplicationID, this.Username);
				if (_settings != null)
				{
					this._container = UserSettingContainer.Deserialize(_settings.SerialziedValue, GetSerializedTypes(), null);
				}
			}
			catch (Exception ex)
			{
				exception = ex;
			}

			if (this._container == null)
			{
				this._container = new UserSettingContainer();
			}

			if (this._settings == null)
			{
				_settings = new UI_UserSettingsContainer()
				{
					ApplicationID = this.ApplicationID,
					UserID = this.Username,
				};
			}

			if (exception != null)
			{
				throw new Exception("There was an error loading user settings", exception);
			}
		}

		protected virtual IEnumerable<Type> GetSerializedTypes()
		{
			return null;
		}

		#endregion //.ctors

		#region Public Methods

		public void SaveSettings()
		{
			if (!_initialized)
				throw new Exception("UserSettings is not initialized. Init must be called be saving.");

			_settings.SerialziedValue = _container.Serialize(null);
			_settings.Save();
		}

		#endregion //Public Methods

		#region Setting Properties

		#endregion //Setting Properties

		#region Setting Names

		#endregion //Setting Names
	}
}
