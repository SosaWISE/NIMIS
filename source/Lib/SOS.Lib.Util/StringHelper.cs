using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SOS.Lib.Util
{
	public class StringHelper
	{
		public static string Join(IEnumerable input, string separator)
		{
			if (input == null)
			{
				return string.Empty;
			}
			else
			{
				var result = new StringBuilder();

				foreach (object currItem in input)
				{
					if (currItem != null)
					{
						result.Append(currItem.ToString());
						result.Append(separator ?? "");
					}
				}

				if (result.Length > 0 && separator != null && separator.Length > 0)
				{
					result.Remove(result.Length - separator.Length, separator.Length);
				}
				return result.ToString();
			}
		}
		public static string Join(IEnumerable<string> input, string separator)
		{
			if (string.IsNullOrWhiteSpace(separator)) return null;

			var result = new StringBuilder();
			foreach (string curr in input)
			{
				result.Append(curr);
				if (!string.IsNullOrEmpty(separator))
				{
					result.Append(separator);
				}
			}
			if (!string.IsNullOrEmpty(separator) && result.Length > separator.Length)
			{
				result.Remove(result.Length - separator.Length, separator.Length);
			}
			return result.ToString();
		}

		public static bool AreEqual(string s1, string s2, bool ignoreCase)
		{
			StringComparer comparer = (ignoreCase) ? StringComparer.CurrentCultureIgnoreCase : StringComparer.CurrentCulture;
			return comparer.Equals(s1, s2);
		}

		private static readonly char[] ValidChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890".ToCharArray();
		public static string GenerateRandomString(int length)
		{
			if (length <= 0)
			{
				throw new ArgumentOutOfRangeException("length", "Parameter must be greater than 0");
			}

			var result = new StringBuilder();
			var r = new Random();

			while (result.Length < length)
			{
				result.Append(ValidChars[r.Next(0, ValidChars.Length)]);
			}

			return result.ToString();
		}

		public static string NullIfWhiteSpace(string value)
		{
			if (value == null || value.Trim().Length == 0)
				return null;
			return value.Trim();
		}

		public static string Right(string input, int nChars)
		{
			if (string.IsNullOrEmpty(input))
			{
				return input;
			}
			else if (input.Length < nChars)
			{
				return input;
			}
			else
			{
				return input.Substring(input.Length - nChars, nChars);
			}
		}

		public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
		{
			// Unix timestamp is seconds past epoch
			var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
			dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
			return dtDateTime;
		}
	}
}