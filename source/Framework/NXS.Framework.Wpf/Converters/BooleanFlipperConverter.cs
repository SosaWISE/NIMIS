using System;
using System.Windows.Data;

namespace NXS.Framework.Wpf.Converters
{
	public class BooleanFlipperConverter : IValueConverter
	{
		#region Singleton Implementation

		private BooleanFlipperConverter()
		{
		}

		public static BooleanFlipperConverter Instance
		{
			get { return Nested.ConverterInstance; }
		}

		private class Nested
		{
			internal static readonly BooleanFlipperConverter ConverterInstance = new BooleanFlipperConverter();
		}

		#endregion Singleton Implementation

		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (targetType != typeof(bool) || !(value is bool))
				return value;

			return !(bool)value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (targetType != typeof(bool) || !(value is bool))
				return value;

			return !(bool)value;
		}

		#endregion
	}
}
