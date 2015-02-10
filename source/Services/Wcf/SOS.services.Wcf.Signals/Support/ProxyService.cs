using System;
using System.Collections.Generic;
using System.Configuration;
using SOS.Lib.RestCake;
using SOS.Services.Interfaces;

namespace SOS.services.Wcf.Signals.Support
{
	public class ProxyService<TServiceInterface> : RestHttpHandler where TServiceInterface : class
	{
		#region .ctor

		static ProxyService()
		{
			Initialize();
		}

		protected ProxyService(bool isSecure)
		{
			IsSecure = isSecure;
			var serviceEndpointBase = ConfigurationManager.AppSettings[IsSecure ? "SecureServiceEndpointBase" : "ServiceEndpointBase"];
			if (string.IsNullOrEmpty(serviceEndpointBase))
			{
				throw new InvalidOperationException("Missing [Secure]ServiceEndpointBase setting");
			}

//			var endpt = serviceEndpointBase + _serviceUrl.BaseUrl;

//			ServiceEndpoint = new Uri(endpt);
		}

		#endregion .ctor

		#region Member Functions
		private static void Initialize()
		{
		}

		private string[] GetParameters(object[] args)
		{
			if (args == null)
			{
				return null;
			}

			var parms = new string[args.Length];
			for (var idx = 0; idx < args.Length; idx++)
			{
				if (args[idx] == null)
				{
					continue;
				}

				parms[idx] = Convert.ToString(args[idx]);
			}

			return parms;
		}

		#endregion Member Functions

		#region Member Fields

		private static readonly string _typeName = typeof(TServiceInterface).Name;

		private static Dictionary<string, CallParameters> _methodCallParameters;
		private static ServiceUrlAttribute _serviceUrl;

		public bool IsSecure { get; private set; }
//		public Uri ServiceEndpoint { get; private set; }

		#endregion Member Fields
	}
}