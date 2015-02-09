using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using SOS.Lib.Util;

namespace NXS.Framework.Wpf.Converters
{
	public class NonNullValueVisibilityConverter : IValueConverter
	{
		#region Singleton Implementation

		private NonNullValueVisibilityConverter()
		{
		}

		public static NonNullValueVisibilityConverter Instance
		{
			get { return Nested.ConverterInstance; }
		}

		private class Nested
		{
			internal static readonly NonNullValueVisibilityConverter ConverterInstance = new NonNullValueVisibilityConverter();

			static Nested()
			{
			}
		}

		#endregion Singleton Implementation

		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var result = Visibility.Visible;
			if (parameter != null)
			{
				if (StringUtility.AreEqual(parameter.ToString(), Visibility.Visible.ToString(), false))
				{
					result = Visibility.Visible;
				}
				if (StringUtility.AreEqual(parameter.ToString(), Visibility.Hidden.ToString(), false))
				{
					result = Visibility.Hidden;
				}
				if (StringUtility.AreEqual(parameter.ToString(), Visibility.Collapsed.ToString(), false))
				{
					result = Visibility.Collapsed;
				}
			}

			if (value != null && value != DBNull.Value)
			{
				// If the value is null, return the value
				return result;
			}
			// Return the opposite of the value
			switch (result)
			{
				case Visibility.Hidden:
				case Visibility.Collapsed:
					return Visibility.Visible;
				default:
					return Visibility.Collapsed;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}

		#endregion
	}
}