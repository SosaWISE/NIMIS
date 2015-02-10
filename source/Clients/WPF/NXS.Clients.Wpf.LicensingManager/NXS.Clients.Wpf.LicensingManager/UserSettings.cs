using NXS.Logic.SosCrm.Settings;

namespace NXS.Clients.Wpf.LicensingManager
{
	public class UserSettings : UserSettingsBase
	{
		#region .ctors

		public UserSettings(int applicationID, string username)
			: base(applicationID, username)
		{
		}

		#endregion //.ctors

		#region Setting Properties

		public bool IsMenuSliderOpen
		{
			get { return _container.GetSettingValue<bool>(SettingNames.IsMenuSliderOpen); }
			set { _container.SetSettingValue<bool>(SettingNames.IsMenuSliderOpen, value); }
		}

		#endregion //Setting Properties

		#region Setting Names

		public struct SettingNames
		{
			public static readonly string IsMenuSliderOpen = "IsMenuSliderOpen";
		}

		#endregion //Setting Names
	}
}
