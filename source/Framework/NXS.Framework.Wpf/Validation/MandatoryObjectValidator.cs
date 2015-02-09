using SOS.Lib.Util;

namespace NXS.Framework.Wpf.Validation
{
	public class MandatoryObjectValidator<T> : IInputValidator<T>
	{
		public static MandatoryObjectValidator<T> Create()
		{
			return new MandatoryObjectValidator<T>();
		}

		public bool Validate(T value)
		{
			var result = true;

			if (value == null)
			{
				result = false; // Not valid if the value is null
			}
			else
			{
				var sv = value as string;
				if (sv != null && StringUtility.NullIfWhiteSpace(sv) == null)
				{
					result = false; // Not valid if the value is an empty string
				}
			}

			return result;
		}
	}
}
