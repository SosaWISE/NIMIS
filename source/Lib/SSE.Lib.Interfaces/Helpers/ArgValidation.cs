using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using SSE.Lib.Interfaces.FOS;

namespace SSE.Lib.Interfaces.Helpers
{
	public class ArgValidation
	{
		#region .ctor

		public ArgValidation(object argument, bool falsePredicate, string message)
		{
			Argument = argument;
			FalsePredicate = falsePredicate;
			Message = message;
			BindValues(() => argument);
		}

		#endregion .ctor

		#region Properties

		public object Argument { get; private set; }
		public bool FalsePredicate { get; private set; }
		public string Message { get; private set; }
		public string Name { get; private set; }
		public object Value { get; private set; }

		#endregion Properties

		#region Methods

		/// <summary>
		/// This is how you get an expression or a object and be able to parse out its name and value.
		/// </summary>
		/// <param name="expr"></param>
		private void BindValues(Expression<Func<object>> expr)
		{
			var body = ((MemberExpression)expr.Body);
			Name = body.Member.Name;
			Value = ((FieldInfo)body.Member)
							.GetValue(((ConstantExpression)body.Expression).Value);
		}

		public static bool ValidateArguments<T>(List<ArgValidation> argArray, out IFosResult<T> result)
		{
			// ** Initialize
			var passesValidation = true;
			var sb = new StringBuilder();
			result = new FosResult<T>(ResultCodes.ArgumentValidating, "Validating Arguments");

			// ** Loop through each argument and validate eachone.
			foreach (var argValidation in argArray)
			{
				// ** Check that the validation passes.
				if (!argValidation.FalsePredicate) continue;
				
				// ** Default path of execution.  Validation failed.
				sb.AppendFormat(argValidation.Message, argValidation.Name, argValidation.Value);
				passesValidation = false;
			}

			// ** Check if validation failed if so reconstruct the result 
			if (!passesValidation)
			{
				result = new FosResult<T>(ResultCodes.ArgumentValidationFailed, sb.ToString());
			}

			// ** Return result
			return passesValidation;
		}

		#endregion Methods
	}
}
