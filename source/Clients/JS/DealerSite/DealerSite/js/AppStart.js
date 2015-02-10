/**
 * Created by JetBrains WebStorm.
 * User: Andres Sosa
 * Date: 12/18/11
 * Time: 5:38 PM
 * To change this template use File | Settings | File Templates.
 */

Ext.ns('SOS');

SOS.AppState =
{
	Unknown: 0
	, Started: 1
	, UserLoggedIn: 2
	, UserInitialized: 3
	, LoggedOut: 4
};

/** Declare App Events. */
SOS.AppEvents =
{
	AppStateChange: 'AppStateChange',
	AppLogin: 'AppLogin',
	UserInitialized: 'UserInitialized'
};

SOS.AppService = (function()
{
	/** Initialization. */
	var bus = new Ext.util.Observable();
	bus.addEvents('AppStateChange', 'AppLogin', 'AppLogout', 'AppNoLogin', 'LoginClick', 'UserInitialized');
	var loggedIn = false;
	var activeUser = null;
	var templateCache = null;
	var appState = SOS.AppState.Unknown;
	var started = false;
	var sessionId = 0;
	var _userInfo = {};

	/** Event Handlers. */
	var setState = function (newState)
	{
		appState = newState;
		/** Fire event. */
		SOS.AppService.DispatchEvent(SOS.AppEvents.AppStateChange, appState);
	};

	/** Return instance of AppService. */
	return	{
		get UserInfo()
		{
			return _userInfo;
		},
		get State()
		{
			return appState;
		},

		get IsLoggedIn()
		{
			return loggedIn;
		},

		get ActiveUser()
		{
			return activeUser;
		},

		get TemplateCache()
		{
			return templateCache;
		},

		DispatchEvent: function(eventName)
		{
			bus.fireEvent.apply(bus, arguments);
		},

		RegisterHandler: function(eventName, handler, scope)
		{
			bus.on(eventName, handler, scope);
		},

		UnregisterHandler: function(eventName, handler, scope)
		{
			bus.un(eventName, handler, scope);
		},

		OnAppLogin: function(userInfo)
		{
			setState(SOS.AppState.UserLoggedIn);
			this.OnUserInitialized(userInfo);

			/** Open start window. */
			window.MainWindow = Ext.create('SOS.Views.MainWindowViewport'
				, {startPanel: SOS.Views.MainWindowViewport.Panels.LeadsPanel});

			/** Check to see if we have deep linking. */
			var ldId = SOS.querystring.getValue("ld");
			if (ldId !== null && ldId !== undefined)
			{
				var oArgs = { Id: "ld-" + ldId, NoteAccount: true }
				SOS.Panels.LeadsPanel.OpenLead(oArgs);
				SOS.AppService.DispatchEvent("panelactivated", SOS.Views.MainWindowViewport.Panels.LeadsPanel);
			}
		},

		OnUserInitialized: function(userInfo)
		{
			/** Bind user info to the application. */
			console.log(userInfo);

			/** Save in cookie. */

			/** Initialize. */
//			var statusBarEl = Ext.getDom('status-bar-id');
//			statusBarEl.innerHTML = Ext.String.format("User: <strong>{0}</strong> (UID: <strong>{1}</strong>) | " +
//				"Last Login: <strong>{2}</strong>"
//				, userInfo.Fullname
//				, userInfo.UserId
//				, userInfo.Username);

			_userInfo = userInfo;
		},

		Start: function()
		{
			/** Check of the application has already started. */
			if (started)
			{
				return;
			}

			/** Mechanisim to deal with token authentication. */
			$.when(this.TokenAuth())
			.then(function(oResponse)
				{
					console.log(oResponse);
					if (oResponse !== null && oResponse.Code === 0)
					{
						document.location.href = "/";
					}
				});

			/** Set the started flag to true. */
			started = true;

			/** Set the state that the app has started. */
			setState(SOS.AppState.Started);

			/** Set Template Cache. */
			templateCache = SOS.Framework.TemplateCache;

			/** Check to see if logged in. */
			if (loggedIn) return;

			/** Get a SessionId */
			var _svc = SOS.ClientServices.AuthServices.Create();

			var request = _svc.CheckSessionStatus(this);
			request.pipe(function(result)
				{
					switch(result.Code)
					{
						case SOS.ClientServices.ServiceCodes.CookieInvalid:
							_svc.on('SessionStartSuccess', function(sosSessionInfo)
								{
									/** Show login screen. */
									this.sessionId = sosSessionInfo.SessionId;
									var oLoginForm = new SOS.Views.LoginView({SessionId: sosSessionInfo.SessionId});
									oLoginForm.show();
								});
								_svc.on('SessionStartFailure', function()
								{});
								_svc.SosStart(SOSConfig.APP_TOKEN, this);
							break;
						case SOS.ClientServices.ServiceCodes.Success:
							SOS.AppService.DispatchEvent(SOS.AppEvents.AppLogin, result.Value);
							break;
					}
				});

			/** Register handlers. */
			SOS.AppService.RegisterHandler(SOS.AppEvents.AppLogin, this.OnAppLogin, this);
			SOS.AppService.RegisterHandler(SOS.AppEvents.UserInitialized, this.OnUserInitialized, this);
		},

		TokenAuth: function()
		{
			/** Check to see if it was passed. */
			var oValue = SOS.Utils.Strings.getParameterByName('st');
			if (!oValue) { return {}; }

			/** Execute. */
			var svc = SOS.ClientServices.AuthServices.Create();
			return svc.TokenAuthentication(oValue, this);
		}
	};
})();