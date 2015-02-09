using System;
using System.Windows;
using System.Windows.Data;

namespace NXS.Framework.Wpf.Converters
{
	public class BooleanVisibilityConverter : IValueConverter
	{
		#region Singleton Implementation

		private BooleanVisibilityConverter()
		{
		}

		public static BooleanVisibilityConverter Instance
		{
			get { return Nested.ConverterInstance; }
		}

		private class Nested
		{
			internal static readonly BooleanVisibilityConverter ConverterInstance = new BooleanVisibilityConverter();
		}

		#endregion Singleton Implementation

		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			bool match = ConverterHelper.AsBoolean(parameter);

			if (value is bool)
			{
				if ((bool) value == match)
					return Visibility.Visible;
				else
					return Visibility.Collapsed;
			}
			else
			{
				bool valueAsBool;
				if (bool.TryParse(value as string, out valueAsBool))
				{
					if (valueAsBool == match)
						return Visibility.Visible;
					else
						return Visibility.Collapsed;
				}
				else
					return Visibility.Visible;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return null;
		}

		#endregion
	}
}