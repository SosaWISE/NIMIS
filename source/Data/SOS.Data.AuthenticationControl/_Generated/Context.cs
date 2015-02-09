


using System;
using SubSonic;
using SOS.Data;

namespace SOS.Data.AuthenticationControl
{
	public partial class SosAuthControlDataContext
	{
		#region Internal Instance

		private static SosAuthControlDataContext _contextInstance;
		private static readonly object _syncRootContextInstance = new object();

		public static SosAuthControlDataContext Instance
		{
			get
			{
				if (_contextInstance == null)
				{
					lock (_syncRootContextInstance)
					{
						if (_contextInstance == null)
						{
							_contextInstance = new SosAuthControlDataContext();
						}
					}
				}
				return _contextInstance;
			}
		}

		#endregion // Internal Instance

		#region Controllers Properties

		AC_ActionController _AC_Actions;
		public AC_ActionController AC_Actions
		{
			get
			{
				if (_AC_Actions == null) _AC_Actions = new AC_ActionController();
				return _AC_Actions;
			}
		}

		AC_ApplicationController _AC_Applications;
		public AC_ApplicationController AC_Applications
		{
			get
			{
				if (_AC_Applications == null) _AC_Applications = new AC_ApplicationController();
				return _AC_Applications;
			}
		}

		AC_AuthenticationController _AC_Authentications;
		public AC_AuthenticationController AC_Authentications
		{
			get
			{
				if (_AC_Authentications == null) _AC_Authentications = new AC_AuthenticationController();
				return _AC_Authentications;
			}
		}

		AC_GroupActionController _AC_GroupActions;
		public AC_GroupActionController AC_GroupActions
		{
			get
			{
				if (_AC_GroupActions == null) _AC_GroupActions = new AC_GroupActionController();
				return _AC_GroupActions;
			}
		}

		AC_GroupApplicationController _AC_GroupApplications;
		public AC_GroupApplicationController AC_GroupApplications
		{
			get
			{
				if (_AC_GroupApplications == null) _AC_GroupApplications = new AC_GroupApplicationController();
				return _AC_GroupApplications;
			}
		}

		AC_SessionController _AC_Sessions;
		public AC_SessionController AC_Sessions
		{
			get
			{
				if (_AC_Sessions == null) _AC_Sessions = new AC_SessionController();
				return _AC_Sessions;
			}
		}

		AC_UserACLController _AC_UserACLs;
		public AC_UserACLController AC_UserACLs
		{
			get
			{
				if (_AC_UserACLs == null) _AC_UserACLs = new AC_UserACLController();
				return _AC_UserACLs;
			}
		}

		AC_UserActionController _AC_UserActions;
		public AC_UserActionController AC_UserActions
		{
			get
			{
				if (_AC_UserActions == null) _AC_UserActions = new AC_UserActionController();
				return _AC_UserActions;
			}
		}

		AC_UserController _AC_Users;
		public AC_UserController AC_Users
		{
			get
			{
				if (_AC_Users == null) _AC_Users = new AC_UserController();
				return _AC_Users;
			}
		}

		AC_UserSessionController _AC_UserSessions;
		public AC_UserSessionController AC_UserSessions
		{
			get
			{
				if (_AC_UserSessions == null) _AC_UserSessions = new AC_UserSessionController();
				return _AC_UserSessions;
			}
		}

		#endregion //Controllers Properties

		#region View Controllers Properties

		AC_DateTimeViewController _AC_DateTimeViews;
		public AC_DateTimeViewController AC_DateTimeViews
		{
			get
			{
				if (_AC_DateTimeViews == null) _AC_DateTimeViews = new AC_DateTimeViewController();
				return _AC_DateTimeViews;
			}
		}

		AC_UserGeneralAuthenticationViewController _AC_UserGeneralAuthenticationViews;
		public AC_UserGeneralAuthenticationViewController AC_UserGeneralAuthenticationViews
		{
			get
			{
				if (_AC_UserGeneralAuthenticationViews == null) _AC_UserGeneralAuthenticationViews = new AC_UserGeneralAuthenticationViewController();
				return _AC_UserGeneralAuthenticationViews;
			}
		}

		AC_UsersAppAuthenticationViewController _AC_UsersAppAuthenticationViews;
		public AC_UsersAppAuthenticationViewController AC_UsersAppAuthenticationViews
		{
			get
			{
				if (_AC_UsersAppAuthenticationViews == null) _AC_UsersAppAuthenticationViews = new AC_UsersAppAuthenticationViewController();
				return _AC_UsersAppAuthenticationViews;
			}
		}

		AC_UsersDealerUsersAuthenticateViewController _AC_UsersDealerUsersAuthenticateViews;
		public AC_UsersDealerUsersAuthenticateViewController AC_UsersDealerUsersAuthenticateViews
		{
			get
			{
				if (_AC_UsersDealerUsersAuthenticateViews == null) _AC_UsersDealerUsersAuthenticateViews = new AC_UsersDealerUsersAuthenticateViewController();
				return _AC_UsersDealerUsersAuthenticateViews;
			}
		}

		#endregion //View Controllers Properties
	}

	#region Controllers

	public class AC_ActionController : BaseTableController<AC_Action, AC_ActionCollection> { }
	public class AC_ApplicationController : BaseTableController<AC_Application, AC_ApplicationCollection> { }
	public class AC_AuthenticationController : BaseTableController<AC_Authentication, AC_AuthenticationCollection> { }
	public class AC_GroupActionController : BaseTableController<AC_GroupAction, AC_GroupActionCollection> { }
	public class AC_GroupApplicationController : BaseTableController<AC_GroupApplication, AC_GroupApplicationCollection> { }
	public class AC_SessionController : BaseTableController<AC_Session, AC_SessionCollection> { }
	public class AC_UserACLController : BaseTableController<AC_UserACL, AC_UserACLCollection> { }
	public class AC_UserActionController : BaseTableController<AC_UserAction, AC_UserActionCollection> { }
	public class AC_UserController : BaseTableController<AC_User, AC_UserCollection> { }
	public class AC_UserSessionController : BaseTableController<AC_UserSession, AC_UserSessionCollection> { }

	#endregion //Controllers

	#region View Controllers

	public class AC_DateTimeViewController : BaseViewController<AC_DateTimeView, AC_DateTimeViewCollection> { }
	public class AC_UserGeneralAuthenticationViewController : BaseViewController<AC_UserGeneralAuthenticationView, AC_UserGeneralAuthenticationViewCollection> { }
	public class AC_UsersAppAuthenticationViewController : BaseViewController<AC_UsersAppAuthenticationView, AC_UsersAppAuthenticationViewCollection> { }
	public class AC_UsersDealerUsersAuthenticateViewController : BaseViewController<AC_UsersDealerUsersAuthenticateView, AC_UsersDealerUsersAuthenticateViewCollection> { }

	#endregion //View Controllers
}
