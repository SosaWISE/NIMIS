
namespace NXS.Framework.Wpf.Validation
{
	public class SocialSecurityNumberValidator : RegexValidator
	{
		public static readonly string Mask = "000-00-0000";
		public static readonly string NullValue = "___-__-____";

		public SocialSecurityNumberValidator()
			: base(@"\A[0-9]{3}-[0-9]{2}-[0-9]{4}\z")
		{
		}

		public static SocialSecurityNumberValidator Create()
		{
			return new SocialSecurityNumberValidator();
		}
	}
}