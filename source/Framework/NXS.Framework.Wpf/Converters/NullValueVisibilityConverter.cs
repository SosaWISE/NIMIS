using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace NXS.Framework.Wpf.Converters
{
	public class NullValueVisibilityConverter : IValueConverter
	{
		#region Singleton Implementation

		private NullValueVisibilityConverter()
		{
		}

		public static NullValueVisibilityConverter Instance
		{
			get { return Nested.ConverterInstance; }
		}

		private class Nested
		{
			internal static readonly NullValueVisibilityConverter ConverterInstance = new NullValueVisibilityConverter();

			static Nested()
			{
			}
		}

		#endregion Singleton Implementation

		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var result = Visibility.Collapsed;
			if (parameter != null)
			{
				if (string.Compare(parameter.ToString(), Visibility.Visible.ToString(), true) == 0)
					result = Visibility.Visible;
				if (string.Compare(parameter.ToString(), Visibility.Hidden.ToString(), true) == 0)
					result = Visibility.Hidden;
				//if (string.Compare(parameter.ToString(), Visibility.Collapsed.ToString(), true) == 0)
				//    result = Visibility.Collapsed;
			}

			if (value == null || System.Convert.IsDBNull(value))
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