using NXS.Clients.Wpf.LicensingManager.Annotations;
using NXS.Framework.Wpf.Mvvm;
using NXS.Framework.Wpf.Mvvm.Models;
using NXS.Framework.Wpf.Mvvm.Security;
using NXS.Framework.Wpf.Mvvm.ViewModels;
using NXS.Framework.Wpf.ParentChildService;
using SOS.Data.SosCrm;
using SOS.Data.SosCrm.ControllerExtensions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace NXS.Clients.Wpf.LicensingManager.ViewModels
{
	public class LicensingMainViewModel : MainWindowViewModel, ISecuritySubscriber
	{

		#region Properties
// ReSharper disable once NotAccessedField.Local
		private readonly ParentCommunicator _oParentComm;
		private WorkspaceController WorkspaceManager { get; set; }
		protected DataContextController ContextManager { get; private set; }

		private string UserName { [UsedImplicitly] get; set; }
		private List<string> UserGroups { get; set; }

		private int? _applicationID;
		private int ApplicationID
		{
			get
			{
				if (_applicationID == null && Arguments.HasParameter(ParameterNames.ApplicationID))
				{
					int appID;
					if (int.TryParse(Arguments.GetParameterValue(ParameterNames.ApplicationID), out appID))
						_applicationID = appID;
				}
				return _applicationID ?? 0;
			}
		}

		public MenuController MenuManager { get; set; }

		public ObservableCollection<MenuNode> MenuNodes
		{
			get { return MenuManager.MenuNodes; }
		}

		public ObservableCollection<WorkspaceViewModel> Workspaces
		{
			get { return WorkspaceManager.Workspaces; }
		}


		private ConcreteCommand _employeeDataReportCommand;
		public ConcreteCommand EmployeeDataReportCommand
		{
			get
			{
				if (_employeeDataReportCommand == null)
				{
					_employeeDataReportCommand = new ConcreteCommand(ActionNames.EmployeeDataReport, ShowEmployeeDataReport);
				}
				return _employeeDataReportCommand;
			}
		}

		private ConcreteCommand _purchasedAccountsReportCommand;
		public ConcreteCommand PurchasedAccountsReportCommand
		{
			get
			{
				if (_purchasedAccountsReportCommand == null)
				{
					_purchasedAccountsReportCommand = new ConcreteCommand(ActionNames.PurchasedAccountsReport, ShowPurchasedAccountsReport);
				}
				return _purchasedAccountsReportCommand;
			}
		}
		#endregion Properties

		#region Constructors
		public LicensingMainViewModel(ParentCommunicator oParentComm, ParameterDictionary args, string userName, List<string> userGroups)
			: base(args)
		{
			_oParentComm = oParentComm;
			DisplayName = GetVersionDisplayName("Nexsense Licensing");

			WorkspaceManager = new WorkspaceController();
			MenuManager = new MenuController();
			ContextManager = DataContextController.Instance;
			App.CurrentApp.Security.AddSubscriber(this);

			UserName = userName;
			UserGroups = new List<string>();
			if (userGroups != null)
			    UserGroups.AddRange(userGroups);

			BuildMenu();
		}

		#endregion Constructors

		#region Methods
		private void BuildMenu()
		{
			MenuManager.BuildMenuAsync(
				null
				, () => App.CurrentApp.Security.GetUserMenu(GetAppMenuViewList())
				, App.CurrentApp.Security.CreateOwnedAbstractAction
				, delegate { }
			);
		}

		private List<MenuItem> GetAppMenuViewList()
		{
			List<UI_ApplicationMenuView> appMenuViewList = DataContextController.Instance.InterimContext.UI_ApplicationMenuViews.GetCurrentApplicationMenu(ApplicationID, true).ToList(); //GetCurrentApplicationMenu(App.ApplicationID);

			List<MenuItem> list = appMenuViewList.ConvertAll(item => new MenuItem
			{
				ID = item.MenuItemID,
				ParentID = item.ParentItemID,
				Label = item.Label,
				ToolTip = item.ToolTip,
				ActionName = item.ActionName,
				IsOverrideable = item.IsOverrideable,
				IsVisible = item.IsVisible
			});

			return list;
		}

		protected override void OnDispose()
		{
			base.OnDispose();

			//unregister commands
			App.CurrentApp.Security.RemoveSubscriber(this);
		}

		private void ShowEmployeeDataReport(ExecutionArgs ea)
		{
// ReSharper disable once RedundantTypeArgumentsOfMethod
			WorkspaceManager.ShowSingletonWorkspace<EmployeeDataReportViewModel>(param => new EmployeeDataReportViewModel(param), ea);
		}

		private void ShowPurchasedAccountsReport(ExecutionArgs ea)
		{
// ReSharper disable once RedundantTypeArgumentsOfMethod
			WorkspaceManager.ShowSingletonWorkspace<PurchasedAccountsReportViewModel>(param => new PurchasedAccountsReportViewModel(param), ea);
		}
		#endregion Methods

		#region ISecuritySubscriber Members

		public void SetLocalCommands(SecurityController securityController)
		{
		}

		public void UnregisterConcreteCommands(SecurityController securityController)
		{
			securityController.UnregisterConcreteCommandDictionary(null);
		}

		public void RegisterConcreteCommands(SecurityController securityController)
		{
			securityController.RegisterConcreteCommandDictionary(null, GetCommandDictionary());
		}

		private ConcreteCommandDictionary _commandDictionary;
		private ConcreteCommandDictionary GetCommandDictionary()
		{
			if (_commandDictionary == null)
			{
				_commandDictionary = new ConcreteCommandDictionary();
			}
			_commandDictionary.Clear();
			_commandDictionary.Add(EmployeeDataReportCommand);
			_commandDictionary.Add(PurchasedAccountsReportCommand);

			return _commandDictionary;
		}
		#endregion ISecuritySubscriber Members
	}
}
