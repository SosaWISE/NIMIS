using System;
using System.Windows.Data;
using SOS.Lib.Util;

namespace NXS.Framework.Wpf.Converters
{
	public sealed class PhoneNumberFormatterConverter : IValueConverter
	{
		#region Singleton

		private PhoneNumberFormatterConverter()
		{
		}

		public static PhoneNumberFormatterConverter Instance
		{
			get
			{
				return Nested.ConverterInstance;
			}
		}

		private class Nested
		{
			static Nested()
			{
			}

			internal static readonly PhoneNumberFormatterConverter ConverterInstance = new PhoneNumberFormatterConverter();
		}

		#endregion Singleton

		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			//if (targetType == typeof(string))
				return StringUtility.FormatPhoneNumber(value as string);
			//else
			//    return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			//if (targetType == typeof(string))
				return StringUtility.TrimPhoneNumber(value as string);
			//else
			//    return null;
		}

		#endregion
	}
}
