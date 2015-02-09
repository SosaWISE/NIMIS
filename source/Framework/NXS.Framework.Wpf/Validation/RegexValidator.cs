using System.Text.RegularExpressions;
using SOS.Lib.Util;

namespace NXS.Framework.Wpf.Validation
{
	public class RegexValidator : IInputValidator<string>
	{
		#region Fields

		Regex _regex;
		string _pattern;

		#endregion //Fields

		#region Properties

		public string Pattern
		{
			get { return _pattern; }
			set
			{
				if (_pattern != value) {
					_pattern = value;
					_regex = new Regex(_pattern);
				}
			}
		}

		#endregion //Properties

		public RegexValidator(string pattern)
		{
			this.Pattern = pattern;
		}

		public static RegexValidator Create(string pattern)
		{
			return new RegexValidator(pattern);
		}

		#region IInputValidator<string> Members

		public virtual bool Validate(string value)
		{
			bool result;
			if (StringUtility.NullIfWhiteSpace(value) == null)
			{
				result = true; // Counts as valid if there is no value
			}
			else {
				result = _regex.IsMatch(value);
			}
			return result;
		}

		#endregion IInputValidator<string> Members
	}
}
