using System;
using System.Windows.Data;

namespace NXS.Framework.Wpf.Converters
{
	public class ValidBrushConverter : IValueConverter
	{
		#region Singleton Implementation

		private ValidBrushConverter()
		{
		}

		public static ValidBrushConverter Instance
		{
			get { return Nested.ConverterInstance; }
		}

		private class Nested
		{
			internal static readonly ValidBrushConverter ConverterInstance = new ValidBrushConverter();
		}

		#endregion Singleton Implementation

		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return (bool)value ? "#00FF00" : "#FF0000";  //Brushes.Green : Brushes.Red;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return value;
		}

		#endregion
	}
}
