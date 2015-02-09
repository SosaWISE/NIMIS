using SOS.Lib.Util.ActiveDirectory;
using SOS.Lib.Util;

namespace NXS.Framework.Wpf.Validation
{
	public class ADUserValidator : IInputValidator<string>
	{
		public ADUserValidator()
		{
		}

		public static ADUserValidator Create()
		{
			return new ADUserValidator();
		}

		public bool Validate(string value)
		{
			bool result = true;

			if (!string.IsNullOrEmpty(value))
			{
				try
				{
					ADUser user = ADManager.Instance.LoadUser(value);
					if (user == null || !StringUtility.AreEqual(user.UserName, value, false))
					{
						result = false;
					}
				}
				catch
				{
					result = false;
				}
			}

			return result;
		}
	}
}