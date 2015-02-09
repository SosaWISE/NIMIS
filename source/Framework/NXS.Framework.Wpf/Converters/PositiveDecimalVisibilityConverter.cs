using System;
using System.Windows.Data;
using System.Windows;

namespace NXS.Framework.Wpf.Converters
{
	public sealed class PositiveDecimalVisibilityConverter : IValueConverter
	{
		#region Singleton Implementation

		private PositiveDecimalVisibilityConverter()
		{
		}

		public static PositiveDecimalVisibilityConverter Instance
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

			internal static readonly PositiveDecimalVisibilityConverter ConverterInstance = new PositiveDecimalVisibilityConverter();
		}

		#endregion Singleton Implementation

		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			bool match = ConverterHelper.AsBoolean(parameter);

			decimal? test = null;

			if (value is decimal) {

				test = (decimal)value;
			}
			else if (value != null) {

				decimal converted;
				if (decimal.TryParse(value.ToString(), out converted)) {
					test = converted;
				}
			}

			if (test != null)
			{
				if (test.Value > 0M == match)
					return Visibility.Visible;
				return Visibility.Collapsed; //Visibility.Hidden;
			}
			return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return null;
		}

		#endregion
	}
}