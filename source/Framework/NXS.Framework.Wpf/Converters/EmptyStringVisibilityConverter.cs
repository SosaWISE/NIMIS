using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using SOS.Lib.Util;

namespace NXS.Framework.Wpf.Converters
{
	public class EmptyStringVisibilityConverter : IValueConverter
	{
		#region Singleton Implementation

		private EmptyStringVisibilityConverter()
		{
		}

		public static EmptyStringVisibilityConverter Instance
		{
			get { return Nested.ConverterInstance; }
		}

		private class Nested
		{
			internal static readonly EmptyStringVisibilityConverter ConverterInstance = new EmptyStringVisibilityConverter();
		}

		#endregion Singleton Implementation

		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var input = value as string;
			if (StringUtility.NullIfWhiteSpace(input) == null)
			{
				return Visibility.Collapsed;
			}
			return Visibility.Visible;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}

		#endregion
	}
}