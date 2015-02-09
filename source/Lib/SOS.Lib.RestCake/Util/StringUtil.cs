using System;
using System.Collections.Generic;
using System.Text;

namespace SOS.Lib.RestCake.Util
{
	public static class StringUtil
	{
		/// <summary>
		/// Can parse a string representing a string[] into an actual string[].
		/// You can delimit strings with nothing (though commas can't be in the string values), ' or ".
		/// Surrounding [] chars are optional.
		/// Examples:
		///		a,b,c
		///		'a','b','c'
		///		"a","b","c"
		///		[a,b,c]
		///		['a','b','c']
		///		["a","b","c"]
		/// A space after a comma is ok, but if you aren't using a string delimiter, a space will be added to the next string's beginning (" b").
		/// Escaped characters also work, such as ["foo, \"bar\"", "\"quoted string\"", "'single quoted with \" delimiter'"]
		/// You can't mix delimiters.  Use all nothing, all ', or all ".  The first delimiter found will be used (at char index 0 or 1, depending on the wrapping []s presence)
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static string[] ParseStringArray(string input)
		{
			if (String.IsNullOrWhiteSpace(input))
				return null;
			
			input = input.Trim();
			
			bool brackets = input[0] == '[' && input[input.Length - 1] == ']';
			
			// String delimeter can be ' or ", or nothing.
			// Note that without a string delimeter, you can't have commas in your string, cause that's what we'll split on.
			char delim = 'X';
			// Depending on if we're brackets in []s or not, we'll look for the string delimiter at index 0 or 1
			int delimIndex = brackets ? 1 : 0;
			
			if (input[delimIndex] == '\'')
				delim = '\'';
			else if (input[delimIndex] == '"')
				delim = '"';
				
			//Console.WriteLine("input: " + input);
			//Console.WriteLine("delim: " + delim);
			//Console.WriteLine("brackets: " + brackets);
			
			if (delim == 'X')
			{
				// easiest case, split on ,
				if (brackets)
					// Get rid of the wrapping []'s
					return input.Substring(1, input.Length - 2).Split(',');

				// Default path of execution.
				return input.Split(',');
			}
			// From here on, we KNOW we have a string delimiter of ' or "
			
			bool inString = false;
			bool escapeNext = false;
			StringBuilder sb = new StringBuilder();
			List<string> strings = new List<string>();
			
			for(int i = 0; i < input.Length; ++i)
			{
				// Skip any possible wrapping [] chars
				if (brackets && (i == 0 || i == input.Length - 1))
					continue;

				char c = input[i];

				if (escapeNext)
				{
					//Console.WriteLine("escaping char: " + c);
					escapeNext = false;
					sb.Append(c);
				}
				else if (c == '\\' && inString)
				{
					//Console.WriteLine("Will escape next");
					escapeNext = true;
					sb.Append(c);
				}
				else if (c == delim)
				{
					//Console.WriteLine("at delim");
					inString = !inString;
				}
				else if (c == ',')
				{
					Console.Write("at comma: ");
					if (inString)
					{
						//Console.WriteLine("instring");
						// This is a comma in the string
						sb.Append(c);
					}
					else
					{
						//Console.WriteLine("out of string");
						// This comma separates one string from another
						//Console.WriteLine("end string");
						strings.Add(sb.ToString());
						sb.Clear();
					}
				}
				else if (inString)
				{
					sb.Append(c);
				}
			}
			// The last string wasn't added, because it gets added when a comma is encountered, and there's no last comma
			strings.Add(sb.ToString());
			
			return strings.ToArray();
		}
	}
}
