using System;
using System.ComponentModel;

namespace SOS.Lib.Util
{
	public static class ConversionHelper
	{
		public static CT ChangeType<CT>(object value)
		{
			if (value is CT)
			{
				return (CT) value;
			}
			// Default path
			return (CT) ChangeType(value, typeof (CT));
		}

		public static object ChangeType(object value, Type conversionType)
		{
			// Note: This if block was taken from Convert.ChangeType as is, and is needed here since we're
			// checking properties on conversionType below.
			if (conversionType == null)
				throw new ArgumentNullException("conversionType");

			if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof (Nullable<>)))
			{
				// It's a nullable type, so instead of calling Convert.ChangeType directly which would throw a
				// InvalidCastException (per http://weblogs.asp.net/pjohnson/archive/2006/02/07/437631.aspx),
				// determine what the underlying type is
				// If it's null, it won't convert to the underlying type, but that's fine since nulls don't really
				// have a type--so just return null
				// Note: We only do this check if we're converting to a nullable type, since doing it outside
				// would diverge from Convert.ChangeType's behavior, which throws an InvalidCastException if
				// value is null and conversionType is a value type.
				if (value == null)
					return null;

				// It's a nullable type, and not null, so that means it can be converted to its underlying type,
				// so overwrite the passed-in conversion type with this underlying type
				var nullableConverter = new NullableConverter(conversionType);
				conversionType = nullableConverter.UnderlyingType;
			}
			else if (conversionType == typeof (Guid))
			{
				return new Guid(value.ToString());
			}

			// Now that we've guaranteed conversionType is something Convert.ChangeType can handle (i.e. not a
			// nullable type), pass the call on to Convert.ChangeType
			return Convert.ChangeType(value, conversionType);
		}


		public static T ConvertType<T>(object oVal)
		{
			return (T) ConvertType(oVal, typeof (T));
		}

		public static object ConvertType(object oVal, Type type)
		{
			if (IsNullable(type) || type == typeof (object))
			{
				if (Convert.IsDBNull(oVal))
				{
// && type != typeof(DBNull)) {
					return null;
				}
				else
				{
					return oVal;
				}
			}

			Type valType = oVal.GetType();
			if (valType == typeof (Byte[]))
			{
				return Convert.ChangeType(oVal, valType);
			}

			return Convert.ChangeType(oVal, type);
		}

		public static bool IsNullable(Type objType)
		{
			if (!objType.IsGenericType)
			{
				return false;
			}
			return objType.GetGenericTypeDefinition().Equals(typeof (Nullable<>));
		}


		public static bool? GetTernaryBooleanValue(string value)
		{
			bool result;
			if (bool.TryParse(value, out result))
				return result;
			return null;
		}

		public static string SetTernaryBooleanValue(bool? value)
		{
			if (value != null)
				return value.ToString().ToLower();
			else
				return null;
		}

		public static int? GetTernaryIntegerValue(string value)
		{
			int result;
			if (int.TryParse(value, out result))
				return result;
			return null;
		}

		public static string SetTernaryIntegerValue(int? value)
		{
			if (value != null)
				return value.ToString();
			else
				return null;
		}

		public static decimal? GetTernaryDecimalValue(string value)
		{
			decimal result;
			if (decimal.TryParse(value, out result))
				return result;
			return null;
		}

		public static string SetTernaryDecimalValue(decimal? value)
		{
			if (value != null)
				return value.ToString();
			else
				return null;
		}
	}
}