using SOS.Lib.Util;

namespace NXS.Framework.Wpf.Validation
{
	public class EmailValidator : RegexValidator
	{
		public EmailValidator()
			: base(MailUtility.EMAIL_REGEX)
		{
		}

		public static EmailValidator Create()
		{
			return new EmailValidator();
		}
	}
}
