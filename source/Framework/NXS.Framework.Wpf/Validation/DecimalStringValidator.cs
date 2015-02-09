using SOS.Lib.Util;

namespace NXS.Framework.Wpf.Validation
{
	public class DecimalStringValidator : IInputValidator<string>
	{
		public DecimalStringValidator()
		{
		}

		public static DecimalStringValidator Create()
		{
			return new DecimalStringValidator();
		}

		#region IInputValidator<string> Members

		public bool Validate(string value)
		{
			value = StringUtility.NullIfWhiteSpace(value);

			decimal t = decimal.Zero;
			bool result = (value == null) ? true : decimal.TryParse(value, out t);
			if (result && t.ToString() != value)
			{
				result = false;
			}
			return result;
		}

		#endregion
	}
}