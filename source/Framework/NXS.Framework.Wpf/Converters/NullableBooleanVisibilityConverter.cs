using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace NXS.Framework.Wpf.Converters
{
	public class NullableBooleanVisibilityConverter : IValueConverter
	{
		#region Singleton Implementation

		private NullableBooleanVisibilityConverter()
		{
		}

		public static NullableBooleanVisibilityConverter Instance
		{
			get { return Nested.ConverterInstance; }
		}

		private class Nested
		{
			internal static readonly NullableBooleanVisibilityConverter ConverterInstance =
				new NullableBooleanVisibilityConverter();
		}

		#endregion Singleton Implementation

		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			Visibility visibility;

			if (System.Convert.IsDBNull(value))
			{
				value = null;
			}
			bool val = (value == null) ? false : (bool) value;

			bool convertParam;
			bool.TryParse(parameter as string, out convertParam);

			if (!convertParam)
			{
				visibility = val ? Visibility.Visible : Visibility.Collapsed;
			}
			else
			{
				visibility = val ? Visibility.Collapsed : Visibility.Visible;
			}

			return visibility;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}

		#endregion
	}
}