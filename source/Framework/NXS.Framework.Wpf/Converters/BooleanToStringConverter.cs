using System;
using System.Windows.Data;

namespace NXS.Framework.Wpf.Converters
{
	public class BooleanToStringConverter : IValueConverter
	{
		#region Singleton Implementation

		private BooleanToStringConverter()
		{
		}

		public static BooleanToStringConverter Instance
		{
			get { return Nested.ConverterInstance; }
		}

		private class Nested
		{
			internal static readonly BooleanToStringConverter ConverterInstance = new BooleanToStringConverter();
		}

		#endregion Singleton Implementation

		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (!(value is bool) || value == null)
				return value;

			bool b = (bool)value;

			if (parameter != null && parameter is string) {

				string g = parameter as string;
				string[] ray = g.Split(new string[] { "|" }, StringSplitOptions.None);

				if (ray.Length > 1) {
					return b ? ray[0] : ray[1];
				}
			}

			return b.ToString();
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return value;
		}

		#endregion
	}
}
