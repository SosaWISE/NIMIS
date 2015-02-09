using System;
using System.Windows.Data;

namespace NXS.Framework.Wpf.Converters
{
	public class TextIndexConverter : IValueConverter
	{
		#region Singleton Implementation

		private TextIndexConverter()
		{
		}

		public static TextIndexConverter Instance
		{
			get { return Nested.ConverterInstance; }
		}

		private class Nested
		{
			internal static readonly TextIndexConverter ConverterInstance = new TextIndexConverter();
		}

		#endregion Singleton Implementation

		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			var index = (int)value;

			string[] ray = ((string)parameter).Split(':');
			string result = ray[index];

			return result;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return null;
		}

		#endregion
	}
}
