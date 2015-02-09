
namespace NXS.Framework.Wpf.Validation
{
	public class PhoneNumberValidator : RegexValidator
	{
		public static readonly string Mask = "(000)000-0000";
		public static readonly string NullValue = "(___)___-____";

		public static readonly string MaskWithExtension = Mask + "x999999";
		public static readonly string NullValueWithExtension = NullValue + "x______";

		public PhoneNumberValidator()
			: base(@"\A\(?[0-9]{3}\)?[-. ]?[0-9]{3}[-. ]?[0-9]{4}(?:x\d*)?\z")
		{
		}

		public static PhoneNumberValidator Create()
		{
			return new PhoneNumberValidator();
		}

		public override bool Validate(string value)
		{
			if (!string.IsNullOrEmpty(value)) {
				return base.Validate(value.Replace("_", " ").Trim());
			}
			else {
				return base.Validate(value);
			}
		}
	}
}
