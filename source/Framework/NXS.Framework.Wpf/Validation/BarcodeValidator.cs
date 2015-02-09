using SOS.Lib.Util;

namespace NXS.Framework.Wpf.Validation
{
	public class BarcodeValidator : IInputValidator<string>
	{
		#region Properties

		public string Prefix { get; set; }

		#endregion //Properties

		public virtual bool Validate(string value)
		{
			bool result;
			value = StringUtility.NullIfWhiteSpace(value);
			if (value == null) {
				result = true;
			}
			else {
				result = value.Length >= 2 && string.Compare(value.Substring(0, 2), Prefix, true) == 0;
			}
			return result;
		}

		public static BarcodeValidator Create(string prefix)
		{
			return new BarcodeValidator() { Prefix = prefix, };
		}
	}
}
