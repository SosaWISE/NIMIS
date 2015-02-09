using System;
using System.Windows.Data;

namespace NXS.Framework.Wpf.Converters
{
	public sealed class PositiveDecimalBooleanConverter : IValueConverter
	{
		#region Singleton Implementation

		private PositiveDecimalBooleanConverter()
		{
		}

		public static PositiveDecimalBooleanConverter Instance
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

			internal static readonly PositiveDecimalBooleanConverter ConverterInstance = new PositiveDecimalBooleanConverter();
		}

		#endregion Singleton Implementation

		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
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
				if (test.Value > 0M)
					return true;
				return false;
			}
			return false;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return null;
		}

		#endregion
	}
}