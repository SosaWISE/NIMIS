using SOS.Lib.Util;

namespace NXS.Framework.Wpf.Validation
{
	public class IntStringValidator : IInputValidator<string>
	{
		public IntStringValidator()
		{
		}

		public static IntStringValidator Create()
		{
			return new IntStringValidator();
		}

		#region IInputValidator<string> Members

		public bool Validate(string value)
		{
			value = StringUtility.NullIfWhiteSpace(value);

			int t;
			bool result = (value == null) ? true : int.TryParse(value, out t);
			return result;
		}

		#endregion
	}
}
