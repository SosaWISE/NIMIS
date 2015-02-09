using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SOS.Lib.RestCake.AuthRules
{
	public class SkipAuthorizationRulesModule : IHttpModule
	{
		private List<SkipAuthorizationRule> _mRules;


		public void Dispose()
		{ }

		public void Init(HttpApplication context)
		{
			// Get any rules specified in the web.config
			_mRules = (List<SkipAuthorizationRule>) ConfigurationManager.GetSection("SkipAuthorizationRules") ?? new List<SkipAuthorizationRule>();

			// We need to intercept before authorization
			context.AuthenticateRequest += OnAuthenticateRequest;
		}


		public void OnAuthenticateRequest(Object sender, EventArgs e)
		{
			var app = (HttpApplication) sender;

			if (_mRules.Any(rule => rule != null && rule.IsMatch(app.Request)))
				app.Context.SkipAuthorization = true;
		}

	}
}
