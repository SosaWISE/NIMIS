using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;


namespace SOS.Lib.RestCake.Util
{
	internal static class ReflectionHelper
	{
		public static IEnumerable<Type> GetTypesWithAttribute(Assembly assembly, Type attributeType)
		{
			return assembly.GetTypes().Where(type => type.GetCustomAttributes(attributeType, true).Length > 0);
		}

		public static IEnumerable<MethodInfo> GetMethodsWithAttribute(Type classType, Type attributeType)
		{
			return classType.GetMethods().Where(methodInfo => methodInfo.GetCustomAttributes(attributeType, true).Length > 0);
		}

		public static IEnumerable<MemberInfo> GetMembersWithAttribute(Type classType, Type attributeType)
		{
			return classType.GetMembers().Where(memberInfo => memberInfo.GetCustomAttributes(attributeType, true).Length > 0);
		}


		/// <summary>
		/// Retrieves the text contents of an embedded resource, starting at "RestCake.Clients", so if you passed in "Js.ServiceClient.js", you'd get that file's contents.
		/// </summary>
		/// <param name="filename">The name of the file relative to "RestCake.Clients"</param>
		/// <returns></returns>
		public static string GetTemplateContents(string filename)
		{
			Assembly curAsm = Assembly.GetExecutingAssembly();
			Stream stream = curAsm.GetManifestResourceStream("SOS.Lib.RestCake.Clients." + filename);
			if (stream == null)
				throw new ArgumentException("No embedded resource could be found with the filename " + filename);
			var reader = new StreamReader(stream);
			string contents = reader.ReadToEnd();
			reader.Close();
			return contents;
		}


		public static T GetAttribute<T>(MethodInfo method)
			where T : Attribute
		{
			T[] attribs = (T[])method.GetCustomAttributes(typeof(T), true);
			return attribs.SingleOrDefault();
		}


		public static T GetAttribute<T>(Type @class)
			where T : Attribute
		{
			T[] attribs = (T[])@class.GetCustomAttributes(typeof(T), true);
			return attribs.SingleOrDefault();
		}


		/// <summary>
		/// From http://stackoverflow.com/questions/401681/how-can-i-get-the-correct-text-definition-of-a-generic-type-using-reflection
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static string GetFriendlyTypeName(Type type)
		{
			// We replace + chars with . chars, because the type system uses a + instead of a . for nested classes for some reason.
			if (type.IsGenericParameter)
			{
				return type.Name.Replace("+", ".");
			}

			if (!type.IsGenericType)
			{
				if (type.Namespace == "System")
					return GetSystemTypeName(type);
				return type.FullName.Replace("+", ".");
			}

			var builder = new StringBuilder();
			string name = type.Name.Replace("+", ".");
			int index = name.IndexOf("`");
			builder.AppendFormat("{0}.{1}", type.Namespace, name.Substring(0, index));
			builder.Append('<');
			bool first = true;
			foreach (Type arg in type.GetGenericArguments())
			{
				if (!first)
				{
					builder.Append(',');
				}
				builder.Append(GetFriendlyTypeName(arg));
				first = false;
			}
			builder.Append('>');
			return builder.ToString();
		}

		private static string GetSystemTypeName(Type type)
		{
			switch(type.Name)
			{
				case "Double":
				case "String":
				case "SByte":
				case "Byte":
				case "Char":
				case "Decimal":
				case "Object":
				case "Void":
					return type.Name.ToLower();

				case "Boolean":
					return "bool";

				case "UInt16":
					return "ushort";

				case "UInt32":
					return "uint";

				case "UInt64":
					return "ulong";

				case "Int16":
					return "short";

				case "Int32":
					return "int";

				case "Int64":
					return "long";

				case "Single":
					return "float";

				default: return type.Name;
			}
		}

		/// <summary>
		/// This is used for the services "_help" pages, for generating documentation on service APIs.
		/// It shouldn't be used for generating code, because the types may not always include their fully qualified names, and may not compile.
		/// </summary>
		/// <param name="type"></param>
		/// <param name="nsBefore">A string to put before the namespace</param>
		/// <param name="nsAfter">A string to put after the namespace</param>
		/// <returns></returns>
		public static string GetTypeAsHtml(Type type, string nsBefore = "<span class='namespace'>", string nsAfter = "</span>")
		{
			if (type.IsGenericParameter)
				return type.Name;

			if (type.FullName != null && type.FullName.StartsWith("System.Nullable`1"))
				return GetTypeAsHtml(type.GetGenericArguments()[0], nsBefore, nsAfter) + "?";
			
			if (!type.IsGenericType)
			{
				if (type.Namespace == "System")
					return GetSystemTypeName(type);
				return nsBefore + type.Namespace + "." + nsAfter + type.Name;
			}

			var sb = new StringBuilder();
			var name = type.Name;
			var index = name.IndexOf("`");
			if (type.Namespace == "System")
				sb.AppendFormat("{0}", name.Substring(0, index));
			else
				sb.AppendFormat("{0}{1}", nsBefore + type.Namespace + "." + nsAfter, name.Substring(0, index));
			sb.Append("&lt;");
			var first = true;
			foreach (var arg in type.GetGenericArguments())
			{
				if (!first)
					sb.Append(',');
				sb.Append(GetTypeAsHtml(arg, nsBefore, nsAfter));
				first = false;
			}
			sb.Append("&gt;");
			return sb.ToString();
		}


		/// <summary>
		/// Recursively gets all generic type params in any given type definition.
		/// So if you had a List{Dictionary{Dictionary{string, int}, Dictionary{string, int}}}, it would get all of the type params.
		/// </summary>
		/// <param name="type"></param>
		/// <param name="list"></param>
		/// <returns></returns>
		public static Type[] GetAllTypeParams(Type type, List<Type> list)
		{
			if (list == null)
				list = new List<Type>();

			if (type.IsGenericType)
			{
				Type[] typeParams = type.GetGenericArguments();
				list.AddRange(typeParams);

				// Recursively process all types
				foreach (Type t in typeParams)
					GetAllTypeParams(t, list);
			}
			return list.ToArray();
		}


		public static object[] ConvertStringValuesToObjects(string[] stringValues, Type[] targetTypes, JsonSerializer serializer)
		{
			if (stringValues.Length != targetTypes.Length)
				throw new ArgumentException("stringValues and targetTypes must be the same length");

			object[] vals = new object[stringValues.Length];
			for (int i = 0; i < stringValues.Length; i++)
			{
				string sVal = stringValues[i];
				Type type = targetTypes[i];
				// If the value is null, use the default value for the target type (null for ref types, a new instance of value types, which will have the default value)
				if (sVal == null)
				{
					if (type.IsValueType)
						vals[i] = Activator.CreateInstance(type);
					else
						vals[i] = null;
				}
				else if (type == typeof(string))
				{
					// See if the string value is wrapped in " or ' chars
					// (Query string and URI segment params won't be.  Ones serialized as part of the request body will be)
					if (!(sVal.StartsWith("\"") && sVal.EndsWith("\"")) && !(sVal.StartsWith("'") && sVal.EndsWith("'")))
						// The string is not wrapped in " or ' chars, so just add it's value as-is.
						vals[i] = sVal;
					else
						// Wrapped, just deserialize it.
						vals[i] = serializer.Deserialize(new StringReader(sVal), type);
				}
				// check explicity for string[] and IList<string>
				// NOTE: These are in here based on my usages of RestCake.
				// I think that getMethodArgs() will end up being sort of a problem area.
				// What would really be handy is some kind of generic library that takes a bunch of string input values, and converts them
				// to a bunch of known target .NET types, in the smartest way possible.  This would be great for interoperability over http (or other
				// ways that communicate with only string data), and should be it's own library, not a part of RestCake.
				// For instance, "True" does not deserialize to a boolean.  It has to be lowercase: "true".  While you can code for that specific edge case,
				// you can't catch it for all composed types.  "bool" is fine, but what about boo[], List<bool>, Dictionary<string, bool>,
				// List<Dictionary<string, Dictionary<bool, List<bool>>>, etc?
				// Json.NET does this to some degree, but is oriented specifically towards Json and user types.
				// It would be nice to find something that handles all cases of .NET types specifically.
				else if (type == typeof(string[]))
				{
					string[] arStrings = StringUtil.ParseStringArray(sVal);
					vals[i] = arStrings;
				}
				else if (typeof(IList<string>).IsAssignableFrom(type))
				{
					string[] arStrings = StringUtil.ParseStringArray(sVal);
					vals[i] = new List<string>(arStrings);
				}
				// special handling for bool, bool[] and IList<bool>
				// (Json.NET can't deserialize unless the bools are all lowercase, so "True" fails, but "true" suceeds).
				else if (type == typeof(bool) || type == typeof(bool[]) || typeof(IList<bool>).IsAssignableFrom(type))
				{
					if (NeedsBrackets(type) && !sVal.StartsWith("["))
						sVal = "[" + sVal + "]";
					sVal = sVal.ToLower(); // force Json.NET to deserialize bool types successfully
					object obj = serializer.Deserialize(new StringReader(sVal), type);
					vals[i] = obj;
				}
				else
				{
					if (NeedsBrackets(type) && !sVal.StartsWith("["))
						sVal = "[" + sVal + "]";
					object obj = serializer.Deserialize(new StringReader(sVal), type);
					vals[i] = obj;
				}
			}
			return vals;
		}


		/// <summary>
		/// Whether or not a string representation of a type'd value requires wrapping "[]" brackets.
		/// True for most IEnumerable types, such as arrays, lists, etc.
		/// NOT true for Dictionaries...
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static bool NeedsBrackets(Type type)
		{
			return !typeof (IDictionary).IsAssignableFrom(type)
			       && typeof (IEnumerable).IsAssignableFrom(type);
		}

	}
}
