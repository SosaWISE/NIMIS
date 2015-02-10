using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Windows;
using NXS.Clients.Wpf.LicensingManager.ViewModels;
using NXS.Data.GreatPlains;
using NXS.Data.Letters;
using NXS.Data.Licensing;
using NXS.Framework.Wpf.Mvvm;
using NXS.Framework.Wpf.Mvvm.Managers;
using NXS.Framework.Wpf.Mvvm.Security;
using NXS.Framework.Wpf.Mvvm.ViewModels;
using NXS.Framework.Wpf.ParentChildService;
using SOS.Data;
using SOS.Data.HumanResource;
using SOS.Data.Logging;
using SOS.Data.SosCrm;
using SOS.Data.SosCrm.Controllers;
using SOS.Data.SosCrm.Models;
using SOS.Lib.Core;
using SOS.Lib.Core.ErrorHandling;
using SOS.Lib.Util.ActiveDirectory;
using StructureMap;
using ConfigurationSettings = SOS.Lib.Util.Configuration.ConfigurationSettings;

namespace NXS.Clients.Wpf.LicensingManager
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
// ReSharper disable once RedundantExtendsListEntry
	public partial class App : Application
	{
		#region Properties
		private const string _STORAGE_DIRECTORY_NAME = "PPro.Clients.Wpf.Licensing";

		public static string StorageDirectoryPath
		{
			get
			{
				return System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), _STORAGE_DIRECTORY_NAME);
			}
		}

		public const int APPLICATION_ID = 5;

		private MainWindow _main;

		public static App CurrentApp
		{
			get { return (App)Current; }
		}

		private static ADUser _loggedInUser;
		private static readonly object _syncRootLoggedInUser = new object();

		private static List<string> _loggedInUserGroups;
		private static readonly object _syncRootLoggedInUserGroups = new object();

		public SecurityController Security { get; private set; }
		public UserSettings Settings { get; private set; }

		public static ADUser LoggedInUser
		{
			get
			{
				if (_loggedInUser == null)
				{
					lock (_syncRootLoggedInUser)
					{
						if (_loggedInUser == null)
						{
							//loggedInUser = ADManager.Instance.LoadUser("bkotter");
							_loggedInUser = ADManager.Instance.LoadUser(Environment.UserName);
						}
					}
				}
				return _loggedInUser;
			}
		}

		public static List<string> LoggedInUserGroups
		{
			get
			{
				if (_loggedInUserGroups == null)
				{
					lock (_syncRootLoggedInUserGroups)
					{
						if (_loggedInUserGroups == null)
						{
							_loggedInUserGroups = new List<string>();
							foreach (ADGroup currGroup in LoggedInUser.Groups)
								_loggedInUserGroups.Add(currGroup.Name);
						}
					}
				}
				return _loggedInUserGroups;
			}
		}
		#endregion Properties

		#region Methods
		protected override void OnStartup(StartupEventArgs e)
		{
			// Setup current environment
			ConfigurationSettings.Current.SetProperties("Preferences", ConfigurationManager.AppSettings["Environment"]);
			SubSonicConfigHelper.SetupConnectionStrings();

			//wire errors
// ReSharper disable once RedundantDelegateCreation
			Current.DispatcherUnhandledException += new System.Windows.Threading.DispatcherUnhandledExceptionEventHandler(Current_DispatcherUnhandledException);

			// Register ErrorManager and ErrorFormatter with StructurMap
			ObjectFactory.Initialize(x =>
			{
				// Register UI Managers
				x.ForRequestedType<IDialogManager>().TheDefaultIsConcreteType<DefaultDialogManager>();
				x.ForRequestedType<IMessageBoxManager>().TheDefaultIsConcreteType<DefaultMessageBoxManager>();
				x.ForRequestedType<IOpenFileManager>().TheDefaultIsConcreteType<DefaultOpenFileManager>();
				x.ForRequestedType<ISaveFileManager>().TheDefaultIsConcreteType<DefaultSaveFileManager>();

				// Register DB Contexts
				x.ForRequestedType<SosCrmDataContext>().TheDefaultIsConcreteType<SosCrmDataContext>();
				x.ForRequestedType<HumanResourceDataContext>().TheDefaultIsConcreteType<HumanResourceDataContext>();
				x.ForRequestedType<GreatPlainsContext>().TheDefaultIsConcreteType<GreatPlainsContext>();
				x.ForRequestedType<LicensingDataContext>().TheDefaultIsConcreteType<LicensingDataContext>();
				x.ForRequestedType<LettersDataContext>().TheDefaultIsConcreteType<LettersDataContext>();

				// Register error handling
				x.ForRequestedType<IErrorManager>().TheDefault.Is.OfConcreteType<DBErrorManager>()
														.WithCtorArg("source").EqualTo(LogSource.NXSClientsWpfLicensingManager);
			});

			// Initialize the main window
			Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
			StartupMainWindow(LoggedInUser.UserName, LoggedInUserGroups);

			// Let the base class do needed work
			base.OnStartup(e);
		}

		internal void StartupMainWindow(string username, List<string> adGroupNames)
		{
			//Initialize the Main Window
			_main = new MainWindow();

			// Set the window as the application's main window
			Current.MainWindow = _main;

			ParameterDictionary startupArgs = GlobalCache.ParentComm.GetStartupParameterDictionary();
			string version;
			if (GlobalCache.ParentComm.GetVersion(startupArgs, out version))
			{
				ViewModelBase.AppVersion = version;
			}

			var usi = new UserSecurityInfo(username, adGroupNames);

			// Load user settings
			Settings = new UserSettings(APPLICATION_ID, usi.Username);
			try
			{
				Settings.Init();
			}
			catch (Exception ex)
			{
// ReSharper disable once SuggestUseVarKeywordEvident
				IErrorManager errorManager = ObjectFactory.GetInstance<IErrorManager>();
				if (errorManager != null)
				{
					errorManager.AddCriticalMessage(ex);
					ViewModelBase.DisplayErrorMessages(errorManager, ObjectFactory.GetInstance<IDialogManager>(), false);
				}
			}

			Security = new SecurityController(
				usi
				, () =>
				{
					OverrideResult result;

					// Load the search control
					var loginViewModel =
						new DefaultLoginViewModel("Manager Override", AdManagerHelper.Instance.IsValidLogin, AdManagerHelper.Instance.GetGroupsForUser);

					if (ObjectFactory.GetInstance<IDialogManager>().ShowDialog(loginViewModel, null, 320, 200, ResizeMode.NoResize))
					{
						result = new OverrideResult(loginViewModel.User, false); //TODO:IsPermanent
					}
					else
					{
						result = null;
					}

					//dispose view model
					loginViewModel.Dispose();

					return result;
				}
				, GlobalCache.ParentComm
			);

			var actnPrmsnsCntrllr = new ActionPermissionController();
			//load permissions
			List<ActionPermission> appPermissions = actnPrmsnsCntrllr.GetCurrentApplicationPermissions(APPLICATION_ID).ToList();
			appPermissions.ForEach(item =>
				Security.AddPermission(item.ActionName, item.IsOverrideable, item.PrincipalName, (Permission.PermissionTypes)item.PermissionTypeID, item.AllowAccess));

			//subscribe to calls from parent
			GlobalCache.ParentComm.SubscribeToParentInvocations(Security);

			//Create the view model
			var mainViewModel = new LicensingMainViewModel(GlobalCache.ParentComm, ParameterDictionary.Create().AddParameter(ParameterNames.ApplicationID, APPLICATION_ID.ToString(CultureInfo.InvariantCulture)), LoggedInUser.UserName, LoggedInUserGroups);

			//Initialize the main window with it's view model
			_main.DataContext = mainViewModel;

			// When the ViewModel asks to be closed, close the window
			mainViewModel.WireWindowClose(_main);

			// Show the main window
			_main.Show();

			//run startup action if one exists
			string actionName;
			if (GlobalCache.ParentComm.GetStartupAction(startupArgs, out actionName))
			{
				GlobalCache.ParentComm.InvokeAction(actionName, startupArgs);
			}
		}

		private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
		{
			var builder = new System.Text.StringBuilder();
			builder.AppendLine(e.Exception.Message);
			builder.AppendLine(e.Exception.StackTrace);
			builder.AppendLine();
			if (e.Exception.TargetSite != null)
				builder.AppendLine(e.Exception.TargetSite.ToString());
			if (e.Exception.InnerException != null)
				builder.AppendLine(e.Exception.InnerException.ToString());

			//write file
			System.IO.Directory.CreateDirectory(StorageDirectoryPath);

			string path = System.IO.Path.Combine(StorageDirectoryPath, "DispatcherError.txt");
			string location = " locally.\r\n   " + path;
			System.IO.File.WriteAllText(path, builder.ToString());
			//}

			MessageBox.Show(
				"This exception has been logged" + location + "\r\n\r\nPlease notify a system administrator.\r\n\r\n" + builder
				, "An exception has occurred");

			Environment.Exit(Environment.ExitCode);
		}
		#endregion Methods
	}
}
