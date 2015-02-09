namespace NXS.Framework.Wpf.Validation
{
	public class ZipCodeValidator : RegexValidator
	{
		public static readonly string Mask = "00000";
		public static readonly string NullValue = "_____";

		public ZipCodeValidator()
			: base(@"^\d{5}$")
		{
		}

		public static ZipCodeValidator Create()
		{
			return new ZipCodeValidator();
		}
	}
}