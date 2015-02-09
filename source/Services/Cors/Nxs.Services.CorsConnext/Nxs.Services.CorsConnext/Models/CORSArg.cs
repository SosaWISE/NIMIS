using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Nxs.Services.CorsConnext.Helpers;
using SOS.Lib.Core;

namespace Nxs.Services.CorsConnext.Models
{
	public class CORSArg
	{
		#region .ctor
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="argument"></param>
		/// <param name="falsePredicate"></param>
		/// <param name="message"></param>
		public CORSArg(object argument, bool falsePredicate, string message)
		{
			Argument = argument;
			FalsePredicate = falsePredicate;
			Message = message;
			BindValues(() => argument);
		}

		#endregion .ctor

		#region Public Props
		public object Argument { get; private set; }
		public bool FalsePredicate { get; private set; }
		public string Message { get; private set; }
		public string Name { get; private set; }
		public object Value { get; private set; }
		private void BindValues(Expression<Func<object>> expr)
		{
			var body = ((MemberExpression)expr.Body);
			Name = body.Member.Name;
			Value = ((FieldInfo)body.Member)
							.GetValue(((ConstantExpression)body.Expression).Value);
		}
		#endregion Public Props

		#region Public Methods

		/// <summary>
		/// Given a list of CORSArg it will validate them each.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="argArray"></param>
		/// <param name="result"></param>
		/// <param name="sourceMethods"></param>
		/// <returns>bool</returns>
		public static bool ArgumentValidation<T>(List<CORSArg> argArray, out Result<T> result, string sourceMethods = null) where T : new()
		{
			/** Initialize. */
			var defaultMessage = string.IsNullOrEmpty(sourceMethods) ? "Validating Arguments" : "Validating Arguments for " + sourceMethods;
			result = new Result<T>((int)CmsResultCodes.ArgumentValidating, defaultMessage);

			return ArgumentValidation(argArray, result, sourceMethods);
		}

		public static bool ArgumentValidation<T>(List<CORSArg> argArray, Result<T> result, string sourceMethods = null)
		{
			/** Initialize. */
			var sb = new StringBuilder();
			var passesValidation = true;
			foreach (CORSArg corsArg in argArray)
			{
				if (!corsArg.FalsePredicate) continue;

				/** Default path of execution. */
				sb.AppendFormat(corsArg.Message, corsArg.Name, corsArg.Value);
				passesValidation = false;
			}

			if (!passesValidation)
			{
				result.Code = (int)CmsResultCodes.ArgumentValidationFailed;
				result.Message = sb.ToString();
			}

			/** Return validation result. */
			return passesValidation;
		}
		#endregion Public Methods


	}
}