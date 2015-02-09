using System;
using SOS.Lib.Util;

namespace NXS.Framework.Wpf.Validation
{
	public class DateStringValidator : IInputValidator<string>
	{
		public static readonly string DateMask = "00/00/0000";
		public static readonly string DateNullValue = "__/__/____";

		private DateStringValidator()
		{
		}
		
		public static DateStringValidator Create()
		{
			return new DateStringValidator();
		}

		#region IInputValidator<string> Members

		public bool Validate(string value)
		{
			bool result;

			if (StringUtility.NullIfWhiteSpace(value) == null)
			{
				result = true; // Counts as valid if there is no value
			}
			else
			{
				DateTime date;
				//result = DateTime.TryParse(value, out date);
				bool canParse = DateTime.TryParse(value, out date);

				if (canParse)
				{

					result = ((date >= new DateTime(1900, 01, 01)) && (date <= DateTime.Now));
				}
				else
				{
					result = false;
				}
			}

			return result;
		}

		#endregion
	}
}
