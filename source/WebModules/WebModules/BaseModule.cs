﻿using Nancy;
using Nancy.Extensions;
using Nancy.ModelBinding;
using Nancy.Responses;
using Nancy.Security;
using NXS.Lib.Web;
using SOS.FunctionalServices;
using SOS.Lib.Core;
using System;
using System.Collections.Generic;

namespace WebModules
{
	public abstract class BaseModule : NancyModule
	{
		protected BaseModule(string modulePath)
			: base("/api" + modulePath)
		{
			//this.RequiresHttps();
			//this.RequiresAuthentication();
			//this.RequiresClaims(new [] { "Admin" });
		}

		static readonly BindingConfig _bodyBindingConfig = new BindingConfig();
		protected TModel BindBody<TModel>()
		{
			_bodyBindingConfig.BodyOnly = true;
			return this.Bind<TModel>(_bodyBindingConfig);
		}

		public SystemUserIdentity User
		{
			get { return this.Context.CurrentUser as SystemUserIdentity; }
		}

		public Result<T> Respond<T>(int code = 0, string message = "", T value = default(T))
		{
			return new Result<T>(code, message, value);
		}

		public void RequiresHttps()
		{
			this.Before.AddItemToEndOfPipeline(
				HttpStatusCodeIfNot(HttpStatusCode.Forbidden, ctx => ctx.Request.Url.IsSecure));
		}
		//public void RequiresAuthentication()
		//{
		//	this.AddBeforeHookOrExecute(
		//		HttpStatusCodeIfNot(HttpStatusCode.Unauthorized, ctx => ctx.CurrentUser.IsAuthenticated()), "Authentication Required");
		//}
		//public void RequiresClaims(IEnumerable<string> requiredClaims)
		//{
		//	this.AddBeforeHookOrExecute(
		//		HttpStatusCodeIfNot(HttpStatusCode.Forbidden, ctx => ctx.CurrentUser.HasClaims(requiredClaims)), "Claims Required");
		//}
		//public void RequiresAnyClaim(IEnumerable<string> requiredClaims)
		//{
		//	this.AddBeforeHookOrExecute(
		//		HttpStatusCodeIfNot(HttpStatusCode.Forbidden, ctx => ctx.CurrentUser.HasAnyClaim(requiredClaims)), "Any Claim Required");
		//}
		//public void RequiresValidatedClaims(Func<IEnumerable<string>, bool> isValid)
		//{
		//	this.AddBeforeHookOrExecute(
		//		HttpStatusCodeIfNot(HttpStatusCode.Forbidden, ctx => ctx.CurrentUser.HasValidClaims(isValid)), "Validated Claim Required");
		//}

		public void RequiresPermission(string applicationID, string actionID)
		{
			this.AddBeforeHookOrExecute(
				HttpStatusCodeIfNot(HttpStatusCode.Forbidden, ctx =>
				{
					var user = this.User;// ctx.CurrentUser;
					if (user == null) return false;

					var authService = SosServiceEngine.Instance.FunctionalServices.Instance<AuthService>();
					return authService.HasPermission(user.Claims, applicationID, actionID);
				}, () =>
				{
					string format = "";
					if (applicationID != null)
					{
						format += " application {0}";
					}
					if (actionID != null)
					{
						if (format.Length > 0)
						{
							format += " and";
						}
						format += " action {1}";
					}
					if (format.Length == 0)
					{
					}
					return "Not authorized for" + string.Format(format, applicationID, actionID);
				}), "Permission Required");
		}

		private Func<NancyContext, Response> HttpStatusCodeIfNot(HttpStatusCode statusCode, Func<NancyContext, bool> test, Func<string> getMessage = null)
		{
			return (ctx) =>
			{
				Response response = null;
				if (!test(ctx))
				{
					response = this.Response.AsJson(new Result<object>(code: (int)statusCode,
						message: getMessage == null ? statusCode.ToString() : getMessage()));
				}
				return response;
			};
		}
	}
}

