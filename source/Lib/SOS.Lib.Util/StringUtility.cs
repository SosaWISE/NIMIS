using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using SOS.Lib.Util.Extensions;

namespace SOS.Lib.Util
{
	public class StringUtility
	{
		#region Constructors

		// Mark constructor as private since this class will provide only static methods
		private StringUtility()
		{
		}

		#endregion Constructors

		#region Methods

		#region Static

		#region Delegates

		public delegate Predicate<T> GetTPredicateDelegate<T>(T obj);

		public delegate T GetTValueDelegate<T>(string value);

		#endregion

		public const string WILD_CARD = "*";
		public const string LIKE = "%";
		public static readonly int PhoneNumberLength = 10;
		private static readonly Random rndGen = new Random();
		private static readonly string passwordChars = "abcdefghjkmnpqrstuvwxyz	ABCDEFGHJKLMNPQRSTUVWXYZ	23456789	!@#$%&*";
		private static char[] _passwordCharArray;

		private static readonly Regex guidRegex =
			new Regex(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$",
			          RegexOptions.Compiled);

		public static char[] PasswordCharArray
		{
			get
			{
				if (_passwordCharArray == null)
				{
					_passwordCharArray = passwordChars.Replace("\t", string.Empty).ToCharArray();
				}
				return _passwordCharArray;
			}
		}

		/// <summary>
		/// Tests whether two strings are equal.
		/// </summary>
		/// <param name="string1">The first string to compare.</param>
		/// <param name="string2">The second string to compare.</param>
		/// <param name="caseSensitive">Whether the equality comparison is case-sensitive.</param>
		/// <returns>True if the two strings are equal, false if not.</returns>
		public static bool AreEqual(string string1, string string2, bool caseSensitive)
		{
			if (string1 == null)
				return string2 == null;
			else if (string2 == null)
				return false;
			else
			{
				if (caseSensitive)
					return string1.Equals(string2);
				else
					return string1.Equals(string2, StringComparison.InvariantCultureIgnoreCase);
			}
		}

		/// <summary>
		/// Tests whether the given testString is found in the list of strings.
		/// </summary>
		/// <param name="list">The list of strings to check against.</param>
		/// <param name="testString">The string to test for.</param>
		/// <param name="caseSensitive">Whether the equality comparison is case-sensitive.</param>
		/// <returns>True if the testString is found in the list, false if not.</returns>
		public static bool IsInList(IEnumerable<string> list, string testString, bool caseSensitive)
		{
			foreach (string curr in list)
			{
				if (AreEqual(curr, testString, caseSensitive))
					return true;
			}
			return false;
		}

		/// <summary>
		/// Removes all non-digit characters from a phone number.
		/// </summary>
		/// <param name="szPhoneNumber">The phone number string to trim.</param>
		/// <returns>The phone number in the format: 0000000000</returns>
		public static string TrimPhoneNumber(string szPhoneNumber)
		{
			return string.IsNullOrEmpty(szPhoneNumber)
			       	? szPhoneNumber
			       	: Regex.Replace(szPhoneNumber, @"[^\d]", "");
		}

		public static bool TryParsePhoneNumber(string text, out string phoneNumber)
		{
			phoneNumber = TrimPhoneNumber(text);
			if (phoneNumber != null)
			{
				return (phoneNumber.Length == PhoneNumberLength);
			}
			phoneNumber = null;
			return false;
		}

		/// <summary>
		/// Given a USA/Canada phone number it will look for the area code by the size of the phone number.
		/// </summary>
		/// <param name="szPhoneNumber"></param>
		/// <returns>string.  The 3 digit area code.</returns>
		public static int GetAreaCode(string szPhoneNumber)
		{
			// ** Trim number.
			var phone = TrimPhoneNumber(szPhoneNumber);

			// ** validate phone number
			if (phone.Length > 11) throw new StringUtilityBadPhoneException(szPhoneNumber
				, string.Format("The number you passed ('{0}') is not a well formed US Phone Number.", szPhoneNumber)); 

			if (phone.Length == 11 & !phone.Substring(0,1).Equals("1"))
				throw new StringUtilityBadPhoneException(phone
					, string.Format("The first digit of the number must be a '1' since the number is 11 characters long."));

			// ** Remove the one if there is one.
			if (phone.Length == 11) phone = phone.Substring(1);

			// ** Check that this is a number
			long numberCheck;
			if (!long.TryParse(phone, out numberCheck))
				throw new StringUtilityBadPhoneException(phone
					, string.Format("The number '{0}' must not have alpha characters.  Only numbers are allowed"));

			// ** Get the area code
			return Convert.ToInt32(phone.Substring(0, 3));
		}

		/// <summary>
		/// Exception used to track issues with the StringUtility class.
		/// </summary>
		public class StringUtilityBadPhoneException : Exception
		{
			#region .ctor
			public StringUtilityBadPhoneException(string rawPhoneNumber, string message) : base(message)
			{
				PhoneNumberRaw = rawPhoneNumber;
			}
			#endregion .ctor

			#region Properties
			public string PhoneNumberRaw { get; set; }
			#endregion Properties
		}

		/// <summary>
		/// Formats the given string as a phone number in the format: (000) 000-0000 *[0]
		/// </summary>
		/// <param name="szPhoneNumber">The phone number string to format.</param>
		/// <returns>A phone number in the format: (000) 000-0000 *[0]</returns>
		public static string FormatPhoneNumber(string szPhoneNumber)
		{
			if (string.IsNullOrEmpty(szPhoneNumber))
				return szPhoneNumber;

			// Start out with a trimmed string
			string result = TrimPhoneNumber(szPhoneNumber);

			if (result.Length == 10)
			{
				return string.Format("({0}) {1}-{2}", result.Substring(0, 3), result.Substring(3, 3),
				                     result.Substring(6));
			}
			else if (result.Length > 10)
			{
				return string.Format("({0}) {1}-{2} x{3}", result.Substring(0, 3), result.Substring(3, 3),
				                     result.Substring(6, 4), result.Substring(10));
			}
			else // Not quite sure how to format this one
				return result;
		}

		public static string FormatSsn(string text)
		{
			string mask = "000-00-0000";
			return FormatWithMask(text, mask);
		}

		/// <summary>
		/// This grabs the last 4 digits of a SSN and padds them with x's.
		/// </summary>
		/// <param name="szValue">string</param>
		/// <returns>string</returns>
		public static string FormatSsnWithBlanks(string szValue)
		{
			// Check for empty string
			if (szValue.Equals(string.Empty)) return szValue;

			// Locals
			var szResult = "xxx-xx-" + szValue.Substring(7);  // This grabs the last 4 digits of a SSN and padds them with x's.

			// Return result
			return szResult;
		}

		public static string FormatWithMask(string text, string mask)
		{
			text = text ?? string.Empty;

			var provider = new MaskedTextProvider(mask);
			provider.Clear();

			int position;
			provider.SetTextInProvider(out position, 0, text.Length, text);

			bool includePrompt = false;
			bool includeLiterals = true;
			return provider.ToString(includePrompt, includeLiterals);
		}

		/// <summary>
		/// Removes the domain name, if it exists, from the given username string.
		/// </summary>
		/// <param name="username">The username string.</param>
		/// <returns>The username without the domain name.</returns>
		public static string FormatUsername(string username)
		{
			if (!string.IsNullOrEmpty(username))
			{
				int mark = username.IndexOf(@"\");
				if (mark >= 0 && mark < username.Length - 1)
					return username.Substring(mark + 1);
				else
				{
					mark = username.IndexOf("/");
					if (mark >= 0 && mark < username.Length - 1)
						return username.Substring(mark + 1);
					else
						return username;
				}
			}
			else
				return username;
		}

		/// <summary>
		/// Tests whether the given input string is a valid number.
		/// </summary>
		/// <param name="input">The string to test.</param>
		/// <param name="decimalAllowed">
		/// Whether to allow a decimal point in the number. 
		/// If this is false, the method will test if the given input string is an integer.
		/// </param>
		/// <returns>True if the input string is a valid number, false if it is not.</returns>
		public static bool IsValidNumber(string input, bool decimalAllowed)
		{
			if (string.IsNullOrEmpty(input))
				return false;
			else if (decimalAllowed)
				return Regex.IsMatch(input, @"^[-+]?[0-9]*\.?[0-9]+$");
			else
				return Regex.IsMatch(input, @"^[-+]?\d+$");
		}

		public static bool IsValidNumber(string input, bool decimalAllowed, int? numbersAfterDecimalAllowed)
		{
			if (string.IsNullOrEmpty(input))
				return false;
			else if (decimalAllowed)
				return Regex.IsMatch(input,
				                     string.Format(@"^[-+]?[0-9]*\.?[0-9]{0}$",
				                                   numbersAfterDecimalAllowed.HasValue
				                                   	? numbersAfterDecimalAllowed.Value.ToString()
				                                   	: "+"));
			else
				return Regex.IsMatch(input, @"^[-+]?\d+$");
		}

		/// <summary>
		/// Masks the input string starting at the left-hand side, leaving the specified number
		/// of unmasked characters on the right-hand side.
		/// </summary>
		/// <param name="input">The string to mask.</param>
		/// <param name="nUnmasked">The number of non-masked characters to leave on the right-hand end of the string.</param>
		/// <param name="maskChar">The mask character.</param>
		/// <returns>The masked string.</returns>
		public static string MaskLeft(string input, int nUnmasked, char maskChar)
		{
			if (string.IsNullOrEmpty(input) || input.Length <= nUnmasked)
				return input;
			else
			{
				var resultBuilder = new StringBuilder();
				for (int i = 0; i < input.Length - nUnmasked; i++)
					resultBuilder.Append(maskChar);
				resultBuilder.Append(input.Substring(input.Length - nUnmasked, nUnmasked));
				return resultBuilder.ToString();
			}
		}

		/// <summary>
		/// Masks the input string starting at the left-hand side, leaving the specified number
		/// of unmasked characters on the right-hand side.
		/// </summary>
		/// <param name="input">The string to mask.</param>
		/// <param name="nUnmasked">The number of non-masked characters to leave on the right-hand end of the string.</param>
		/// <param name="maskChar">The mask character.</param>
		/// <returns>The masked string.</returns>
		public static string MaskLeft(string input, int nUnmasked, char maskChar, int nMaxMaskChars)
		{
			if (string.IsNullOrEmpty(input) || input.Length <= nUnmasked)
				return input;
			else
			{
				var resultBuilder = new StringBuilder();
				for (int i = 0; i < input.Length - nUnmasked; i++)
				{
					if (nMaxMaskChars >= 0 && resultBuilder.Length < nMaxMaskChars)
					{
						resultBuilder.Append(maskChar);
					}
				}
				resultBuilder.Append(input.Substring(input.Length - nUnmasked, nUnmasked));
				return resultBuilder.ToString();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		/// <param name="maximumLength"></param>
		/// <returns></returns>
		public static string Right(string input, int maximumLength)
		{
			if (string.IsNullOrEmpty(input) || input.Length < maximumLength)
			{
				return input;
			}
			else
			{
				return input.Substring(input.Length - maximumLength, maximumLength);
			}
		}

		/// <summary>
		/// Returns the distance/number of changes between the two strings. Eg: dog - fog and fog - frog are both 1. Whereas swapping two letters eg: frog - forg is 2.
		/// </summary>
		/// <param name="s"></param>
		/// <param name="t"></param>
		/// <returns></returns>
		public static int LevenshteinDistance(string s, string t)
		{
			s = s ?? "";
			t = t ?? "";

			int n = s.Length; //length of s
			int m = t.Length; //length of t
			var d = new int[n + 1,m + 1]; // matrix
			int cost; // cost

			// Step 1
			if (n == 0) return m;
			if (m == 0) return n;

			// Step 2
			for (int i = 0; i <= n; d[i, 0] = i++) ;
			for (int j = 0; j <= m; d[0, j] = j++) ;

			// Step 3
			for (int i = 1; i <= n; i++)
			{
				//Step 4
				for (int j = 1; j <= m; j++)
				{
					// Step 5
					cost = (t.Substring(j - 1, 1) == s.Substring(i - 1, 1) ? 0 : 1);
					// Step 6
					d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + cost);
				}
			}

			// Step 7
			return d[n, m];
		}

		public static string Join(string separator, params string[] input)
		{
			return Join(input, separator);
		}

		public static string Join(IEnumerable<string> input, string separator)
		{
			if (input == null || string.IsNullOrEmpty(separator))
				return null;

			var result = new StringBuilder();
			foreach (string curr in input)
			{
				result.Append(curr);
				if (!string.IsNullOrEmpty(separator))
					result.Append(separator);
			}
			if (!string.IsNullOrEmpty(separator) && result.Length > separator.Length)
				result.Remove(result.Length - separator.Length, separator.Length);
			return result.ToString();
		}

		public static string Join(IEnumerable input, string separator)
		{
			if (input == null || string.IsNullOrEmpty(separator))
				return null;

			var result = new StringBuilder();
			foreach (object curr in input)
			{
				result.Append(curr.ToString());
				if (!string.IsNullOrEmpty(separator))
					result.Append(separator);
			}
			if (!string.IsNullOrEmpty(separator) && result.Length > separator.Length)
				result.Remove(result.Length - separator.Length, separator.Length);
			return result.ToString();
		}

		public static string Join(NameValueCollection nvc, string separator)
		{
			var sob = new StringBuilder();

			foreach (string key in nvc)
			{
				sob
					.Append(key).Append("=")
					.Append(nvc[key]).Append(separator);
			}

			return sob.ToString(0, sob.Length - 1);
		}

		public static string FormatFullName(params string[] fullNameList)
		{
			if (fullNameList.Length > 0)
			{
				var sob = new StringBuilder();
				foreach (string g in fullNameList)
				{
					if (!String.IsNullOrEmpty(g))
					{
						sob.Append(g).Append(" ");
					}
				}
				return sob.ToString().Trim();
			}
			return string.Empty;
		}

		public static string JoinIfNotEmpty(string separator, params string[] list)
		{
			var result = new StringBuilder();
			if (list.Length > 0)
			{
				foreach (string g in list)
				{
					string temp = NullIfWhiteSpace(g);
					if (temp != null)
					{
						result.Append(temp).Append(separator);
					}
				}
			}
			if (!string.IsNullOrEmpty(separator) && result.Length > separator.Length)
				result.Remove(result.Length - separator.Length, separator.Length);
			return result.ToString();
		}

		public static string GetRandomString(int length)
		{
			var result = new StringBuilder();

			while (result.Length < length)
			{
				int next = rndGen.Next(48, 123);
				while ((next >= 58 && next < 65) || (next >= 91 && next < 97))
					next = rndGen.Next(48, 123);

				result.Append((char) next);
			}

			return result.ToString();
		}

		//static Random rndGen = new Random();

		/// <summary>
		/// Basically the same as GetRandomString except that it excludes chars that look similar such as 1liO0o
		/// </summary>
		/// <param name="length"></param>
		/// <returns></returns>
		public static string GetRandomPassword(int length)
		{
			int rayLength = PasswordCharArray.Length;

			var result = new StringBuilder();

			while (result.Length < length)
			{
				char next = PasswordCharArray[rndGen.Next(rayLength)];
				result.Append(next);
			}

			return result.ToString();
		}


		public static bool TryParseGuid(string value, out Guid output)
		{
			if (!string.IsNullOrEmpty(value))
			{
				if (guidRegex.IsMatch(value))
				{
					output = new Guid(value);
					return true;
				}
			}
			output = Guid.Empty;
			return false;
		}

		public static string SurroundWithLike(string value)
		{
			if (value == null)
				return null;
			return LIKE + value + LIKE;
		}

		public static string ReplaceWildCard(string value, bool surroundWithLike)
		{
			value = ReplaceWildCard(value);
			if (surroundWithLike)
				return SurroundWithLike(value);
			else
				return value;
		}

		public static string ReplaceWildCard(string value)
		{
			if (value == null)
				return null;
			return value.Replace(WILD_CARD, LIKE);
		}

		public static string NullIfWhiteSpace(string value)
		{
			if (value == null)
				return null;

			value = value.Trim();

			if (value.Length == 0)
				return null;

			return value;
		}

		public static string FormatParameterString(string parameterName, string parameterValue)
		{
			return string.Format("{0}:{1}", parameterName, parameterValue);
		}

		public static string FormatParameters(Dictionary<string, string> parameters)
		{
			var result = new StringBuilder();
			foreach (string curr in parameters.Keys)
				result.AppendFormat("{0} ", FormatParameterString(curr, parameters[curr]));

			return result.ToString().Trim();
		}

		public static Dictionary<string, string> GetParameters(string[] args)
		{
			var argParamDict = new Dictionary<string, string>();
			foreach (string arg in args)
			{
				string[] ray = arg.Split(new[] {':'});
				if (ray.Length == 2)
				{
					argParamDict.Add(ray[0], ray[1]);
				}
			}
			return argParamDict;
		}

		public static string ParseCollToStringList<T>(List<T> oList, string cSeparator, Func<T,string> dGetValue)
		{
			// Check to make sure that the delegate is passed
			if (dGetValue == null)
				throw new Exception(string.Format("Sorry but the dGetValue GetTValueDelegate was passed a null"));

			/** Locals */
			var szResult = string.Empty;

			/** Loop through the list */
			foreach (var oItem in oList)
			{
				if (string.IsNullOrEmpty(szResult))
				{
					szResult = dGetValue(oItem);
					continue;
				}

				// Default path of execution
				szResult += cSeparator + dGetValue(oItem);
			}

			// Return result
			return szResult;
		}

		/// <summary>
		/// Adds values from string to a list
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <param name="values">string of values delimited by seperators</param>
		/// <param name="seperators">string seperators</param>
		/// <param name="startWith">Nullable. Each segment must start with this value</param>
		/// <param name="parseTValue">Method used to parse string into T</param>
		/// <param name="findTInListPred">Nullable. Method used to prevent duplicate T values</param>
		public static void ParseStringToList<T>(List<T> list, string values, char[] seperators, string startWith,
		                                        GetTValueDelegate<T> parseTValue, GetTPredicateDelegate<T> equalsPred)
		{
			if (!String.IsNullOrEmpty(values))
			{
				foreach (string str in values.Split(seperators, StringSplitOptions.RemoveEmptyEntries))
				{
					if (!String.IsNullOrEmpty(startWith) && !str.StartsWith(startWith))
					{
						throw new Exception(string.Format("Invalid value '{0}'. '{0}' must start with '{1}'", str, startWith));
					}

					try
					{
						T obj = parseTValue(str);
						if (equalsPred == null || list.Find(equalsPred(obj)) == null)
						{
							list.Add(obj);
						}
					}
					catch (Exception ex)
					{
						throw new Exception(string.Format("There was an error adding '{0}' to the list.", str), ex);
					}
				}
			}
		}

		public static int CountNewLines(string s)
		{
			return CountChar(s, '\n');
		}

		public static int CountChar(string s, char c)
		{
			if (s == null)
				return 0;

			int count = 1;
			int startIndex = 0;
			while ((startIndex = s.IndexOf(c, startIndex)) != -1)
			{
				startIndex++; //move ahead one character
				count++; //increment count
			}

			return count;
		}

		public static bool IsMatch(string stringA, string stringB)
		{
			return String.Equals(stringA, stringB, StringComparison.InvariantCultureIgnoreCase);
		}

		public static string InsertText(string text, int index, string insertionText)
		{
			if (text == null)
				throw new ArgumentNullException("text");

			if (index > text.Length)
				throw new IndexOutOfRangeException("index is greater than length of text");

			var sob = new StringBuilder();
			sob.Append(text.Substring(0, index));
			sob.Append(insertionText);
			sob.Append(text.Substring(index, text.Length - 1));

			return sob.ToString();
		}

		public static string Space(string text)
		{
			var sob = new StringBuilder();

			bool prevIsUpper = false;
			bool prevIsNumber = false;
			bool prevIsSpace = false;


			int length = text.Length;
			for (int i = 0; i < length; i++)
			{
				char c = text[i];

				bool isUpper = Char.IsUpper(c);
				bool isNumber = Char.IsNumber(c);
				bool isSpace = Char.IsWhiteSpace(c);

				if (
					(i > 0) //don't add space in front of first char
					&& (!isSpace && !prevIsSpace) //no double spaces
					&& (
					   	(isUpper && !prevIsUpper) //add space before uppercase, unless previous is uppercase
					   	|| (isNumber && !prevIsNumber) //add space before number, unless previous is number
					   )
					)
				{
					sob.Append(" ");
				}
				sob.Append(c);

				prevIsUpper = isUpper;
				prevIsNumber = isNumber;
				prevIsSpace = isSpace;
			}
			return sob.ToString();
		}

		public static int CountInstances(string s, string value, StringComparison comparisonType)
		{
			if (value == null)
				throw new ArgumentNullException("value");

			int result = 0;
			int index = 0;
			while ((index = s.IndexOf(value, index, comparisonType)) >= 0)
			{
				result++;
				index += value.Length;
			}
			return result;
		}

		public static MemoryStream GetMemoryStream(string value)
		{
			return new MemoryStream(Encoding.ASCII.GetBytes(value));
		}


		public static int ParseInt(string value)
		{
			//if null or empty return 0
			//also trim the value for the next part
			if (value == null || (value = value.Trim()).Length == 0)
				return 0;

			value = RemoveLeadingZeros(value);

			int length = Math.Min(10, value.Length); //10 is maximum length of an int

			bool stillZero = true;
			//go through all chars and stop on first non digit
			var sob = new StringBuilder();
			char c;
			for (int i = 0; i < length; i++)
			{
				c = value[i];
				if (Char.IsDigit(c) || (i == 0 && c == '-'))
				{
//allow negative numbers
					sob.Append(c);
					if (stillZero && (c != '-' || c != 0))
					{
					}
				}
				else
				{
					break;
				}
			}

			//this loop is only needed for numbers larger/smaller than int.MaxValue/int.MinValue, respectively
			//for all other numbers it should return the first value
			int n;
			for (; sob.Length > 0; sob.Length--)
			{
				if (int.TryParse(sob.ToString(), out n))
				{
					return n;
				}
			}
			return 0;
		}

		public static string RemoveLeadingZeros(string value)
		{
			if (string.IsNullOrEmpty(value))
				return value;

			bool isNegative = value[0] == '-';

			int index = 0;
			int length = value.Length;
			char c;
			for (; index < length; index++)
			{
				c = value[index];
				if (!(c == '0' || (index == 0 && c == '-')))
				{
					break;
				}
			}

			if (index == 0)
			{
				return value;
			}
			else
			{
				if (isNegative && length != index) //is negative and not all zero
					return '-' + value.Substring(index);
				else
					return value.Substring(index);
			}
		}

		/// <summary>
		/// Trims the input string so it is no more than the specified length.
		/// </summary>
		/// <param name="input">The string to trim.</param>
		/// <param name="maxLength">The maximum length of the output string.</param>
		/// <returns></returns>
		public static string TrimToMax(string input, int maxLength)
		{
			if (string.IsNullOrEmpty(input))
				return input;
			else if (input.Length > maxLength)
				return input.Substring(0, maxLength);
			else
				return input;
		}

		public static string GetOrdinalNumberString(int number)
		{
			string result = number.ToString();

			if (result.EndsWith("1") && number != 11)
				result = result + "st";
			else if (result.EndsWith("2") && number != 12)
				result = result + "nd";
			else if (result.EndsWith("3") && number != 13)
				result = result + "rd";
			else
				result = result + "th";

			return result;
		}

		/// <summary>
		/// Tries to parse an integer id from a within a formatted text id. For U16697 it would return 16697.
		/// </summary>
		/// <param name="text"></param>
		/// <param name="regexPattern">"^[Uu](?<groupName>[0-9]*)$"</param>
		/// <param name="groupName">Name of group</param>
		/// <param name="id"></param>
		/// <returns></returns>
		public static bool TryParseTextID(string text, string regexPattern, string groupName, out int id)
		{
			id = 0;

			if (string.IsNullOrEmpty(text)) return false;

			var reg = new Regex(regexPattern);
			if (reg.IsMatch(text))
			{
				Match m = reg.Match(text);
				text = m.Groups[groupName].Value;
			}

			return int.TryParse(text, out id);
		}

		public static string RevertUppercase(string input)
		{
			if (string.IsNullOrEmpty(input)) return input;

			var result = new StringBuilder();
			for (int index = 0; index < input.Length; index++)
			{
				if (index == 0 || input[index - 1] == ' ')
				{
					result.Append(input[index]);
				}
				else
				{
					result.Append(input[index].ToString().ToLower());
				}
			}
			return result.ToString();
		}

		public static bool IsValidVersion(string version)
		{
			return Regex.IsMatch(version, @"^(?:(\d+)\.)?(?:(\d+)\.)?(?:(\d+)\.)?(\*|\d+)$");
		}

		#region ModResult

		public static string EvenOdd(int count, string even, string odd)
		{
			return ModResult(count, even, odd);
		}

		public static string ModResult(int count, params string[] values)
		{
			if (count >= 0 && values != null)
			{
				int length = values.Length;
				if (length > 0)
				{
					int index = count%length;
					return values[index];
				}
			}
			return null;
		}

		#endregion //ModResult

		#region DeleteSegment

		public static string DeleteSegment(DeleteDirectionEnum deleteDirection, string txt, int initialStartIndex,
		                                   out int startIndex)
		{
			int textLength = txt.Length;
			startIndex = initialStartIndex;

			int offset;
			int move;
			switch (deleteDirection)
			{
				case DeleteDirectionEnum.Left:
					offset = -1;
					move = -1;
					break;
				case DeleteDirectionEnum.Right:
					offset = 0;
					move = 1;
					break;
				default:
					throw new NotImplementedException();
			}

			//if still in bounds
			if ((startIndex + offset) > -1 && (startIndex + offset) < textLength)
			{
				int whitespaceCount = 0;
				CharType firstCharType = GetCharType(txt[startIndex + offset]);

				//move once
				startIndex += move;

				char c;
				CharType currCharType;

				//while still in bounds
				while (startIndex > 0 && startIndex < textLength)
				{
					c = txt[startIndex + offset];
					currCharType = GetCharType(c);

					if (firstCharType == CharType.Alpha
					    && currCharType != CharType.Alpha)
					{
						break;
					}
					else if (firstCharType == CharType.Numer
					         && currCharType != CharType.Numer)
					{
						break;
					}
					else if (firstCharType == CharType.Other
					         && currCharType != CharType.Other)
					{
						break;
					}
					else if (firstCharType == CharType.White)
					{
						if (currCharType != CharType.White)
						{
							if (whitespaceCount == 0)
							{
								//first char can be whitespace before Alpha, Numer, and Other
								firstCharType = currCharType;
							}
							else
							{
								//stop
								break;
							}
						}
						whitespaceCount++;
					}
					startIndex += move;
				}
			}

			int length = Math.Abs(initialStartIndex - startIndex);
			switch (deleteDirection)
			{
				case DeleteDirectionEnum.Left:
					//do nothing, already set correctly
					break;

				case DeleteDirectionEnum.Right:
					startIndex = initialStartIndex;
					break;

				default:
					throw new NotImplementedException();
			}

			return txt.Remove(startIndex, length);
		}

		private static CharType GetCharType(char c)
		{
			if (97 <= c && c <= 122)
			{
				return CharType.Alpha;
			}
			else if (48 <= c && c <= 57)
			{
				return CharType.Numer;
			}
			else if ("\t\n\r ".IndexOf(c) != -1) //(c == 32)
			{
				return CharType.White;
			}
			else
			{
				return CharType.Other;
			}
		}

		#endregion //DeleteSegment

		#region Add Values to String List Helpers

		public static void AddPipeDelimitedValues(List<string> strList, string values, string startWith)
		{
			ParseStringToStringList(strList, values, new[] {'|'}, startWith);
		}

		public static void AddValues(List<string> strList, string values, char[] seperators)
		{
			ParseStringToStringList(strList, values, seperators, null);
		}

		public static void ParseStringToStringList(List<string> strList, string values, char[] seperators, string startWith)
		{
			ParseStringToList(strList, values, seperators, startWith, GetStringValue, GetStringPred);
		}

		public static string GetStringValue(string value)
		{
			return value;
		}

		public static Predicate<string> GetStringPred(string value)
		{
			return delegate(string other) { return IsMatch(other, value); };
		}

		#endregion

		#region IP Address
		/// <summary>
		/// Author: Faisal Khan (http://www.stardeveloper.com)
		/// </summary>
		public static long ConvertIPToLong(string ipAddress)
		{
			System.Net.IPAddress ip;

			if (System.Net.IPAddress.TryParse(ipAddress, out ip))
			{
				byte[] bytes = ip.GetAddressBytes();

				return (long)(((long)bytes[0] << 24) | (bytes[1] << 16) |
					(bytes[2] << 8) | bytes[3]);
			}
			else
				return 0;
		}
		/// <summary>
		/// Author: Faisal Khan (http://www.stardeveloper.com)
		/// </summary>
		public static string ConvertLongToIP(long ipLong)
		{
			var b = new StringBuilder();
			long tempLong, temp;

			tempLong = ipLong;
			temp = tempLong / (256 * 256 * 256);
			tempLong = tempLong - (temp * 256 * 256 * 256);
			b.Append(Convert.ToString(temp)).Append(".");
			temp = tempLong / (256 * 256);
			tempLong = tempLong - (temp * 256 * 256);
			b.Append(Convert.ToString(temp)).Append(".");
			temp = tempLong / 256;
			tempLong = tempLong - (temp * 256);
			b.Append(Convert.ToString(temp)).Append(".");
			temp = tempLong;
			tempLong = tempLong - temp;
			b.Append(Convert.ToString(temp));

			return b.ToString().ToLower();
		}
		#endregion IP Address

		#endregion Static

		#endregion Methods
	}

	#region DeleteSegment Enums

	public enum DeleteDirectionEnum
	{
		None,
		Left,
		Right,
	}

	public enum CharType
	{
		Alpha,
		Numer,
		Other,
		White,
	}

	#endregion //DeleteSegment Enums
}