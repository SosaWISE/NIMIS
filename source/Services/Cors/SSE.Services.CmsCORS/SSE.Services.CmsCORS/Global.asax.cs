using Api.Core;
using NXS.Data;
using NXS.Lib;
using SOS.Data;
using SOS.Data.Logging;
using SOS.Lib.Core;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AuthDBase = NXS.Data.AuthenticationControl.DBase;
using CrmDBase = NXS.Data.Crm.DBase;
using HrDBase = NXS.Data.HumanResource.DBase;
using SalesDBase = NXS.Data.Sales.DBase;


namespace SSE.Services.CmsCORS
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class WebApiApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			// Load webconfig
			WebConfig.Init(Server.MapPath("~"), val =>
			{
				var decryptedVal = SOS.Lib.Util.Cryptography.TripleDES.DecryptString(val, null);
				// if decryption failed, return passed in value
				return decryptedVal.StartsWith("Error: ") ? val : decryptedVal;
			});
			// Initialize Active Directory
			var domain = WebConfig.Instance.GetConfig("Domain");
			var adPath = WebConfig.Instance.GetConfig("ADPAth");
			var adUser = WebConfig.Instance.GetConfig("ADUser");
			var adPassword = WebConfig.Instance.GetConfig("ADPassword");
			var adUsersPath = WebConfig.Instance.GetConfig("ADUsersPath");
			SOS.Lib.Util.ActiveDirectory.ADUtility.Init(domain, adPath, adUser, adPassword, adUsersPath);

			// set connection strings
			var host = WebConfig.Instance.GetConfig("DBHost");
			var username = WebConfig.Instance.GetConfig("DBUsername");
			var password = WebConfig.Instance.GetConfig("DBPassword");
			var appName = WebConfig.Instance.GetConfig("ApplicationName");
			HrDBase.ConnectionString = DatabaseHelper.FormatConnectionString(HrDBase.Database, host, username, password, appName);
			CrmDBase.ConnectionString = DatabaseHelper.FormatConnectionString(CrmDBase.Database, host, username, password, appName);
			AuthDBase.ConnectionString = DatabaseHelper.FormatConnectionString(AuthDBase.Database, host, username, password, appName);
			SalesDBase.ConnectionString = DatabaseHelper.FormatConnectionString(SalesDBase.Database, host, username, password, appName);
			// Setup SubSonic Connections
			SubSonicConfigHelper.SetupConnectionStrings(host, username, password, appName);

			/** Initialize Fos Engine. */
			SOS.FunctionalServices.SosServiceEngine.Instance.Initialize();
			//
			AuthService authService;
			var configuration = AuthServiceConfig.Configure(ctx =>
			{
				//@HACK: for integration with non Nancy requests
				if (ctx == null)
					return System.Web.HttpContext.Current.Request.Headers["User-Agent"];
				return ctx.Request.Headers.UserAgent;
			}, out authService);
			SOS.FunctionalServices.SosServiceEngine.Instance.FunctionalServices.Register(() => configuration);
			SOS.FunctionalServices.SosServiceEngine.Instance.FunctionalServices.Register(() => authService);

			/** Initialize Error Manager. */
			_errorManager = new DBErrorManager(LogSource.NXSServicesCmsCORS, null);
			_errorManager.MessageAdded += OnErrorManagerMessageAdded;
			DBErrorManager.SetSingletonInstance(_errorManager);

			AreaRegistration.RegisterAllAreas();
			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			GlobalConfiguration.Configuration.EnsureInitialized();
		}

		#region Error Handler Manager

		private DBErrorManager _errorManager;

		void OnErrorManagerMessageAdded()
		{
			if (_errorManager.MessageCount > 0)
			{
				_errorManager.ClearMessages();
			}
		}

		#endregion Error Handler Manager
	}
}