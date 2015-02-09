using System;

namespace NXS.Framework.Wpf.Validation
{
	public class StringToIntConversionValidator : IInputValidator<string>
	{
		IInputValidator<int> _intValidator;

		public StringToIntConversionValidator(IInputValidator<int> intValidator)
		{
			if (intValidator == null)
				throw new ArgumentNullException("intValidator");

			_intValidator = intValidator;
		}

		public static StringToIntConversionValidator Create(IInputValidator<int> intValidator)
		{
			return new StringToIntConversionValidator(intValidator);
		}

		#region IInputValidator<string> Members

		public bool Validate(string value)
		{
			int i;
			if (int.TryParse(value, out i)) {
				return _intValidator.Validate(i);
			}
			return false;
		}

		#endregion
	}
}
