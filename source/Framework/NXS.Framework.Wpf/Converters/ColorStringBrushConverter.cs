using System;
using System.Windows.Data;
using System.Windows.Media;

namespace NXS.Framework.Wpf.Converters
{
	public class ColorStringBrushConverter : IValueConverter
	{
		#region Singleton Implementation

		public static ColorStringBrushConverter Instance
		{
			get { return Nested._converterInstance; }
		}

		private class Nested
		{
			internal static readonly ColorStringBrushConverter _converterInstance = new ColorStringBrushConverter();
		}

		#endregion Singleton Implementation

		#region IMultiValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			// Locals
			Brush oResult;

			if (targetType != typeof(Brush)) throw new Exception("Target type is not Brush");
			if (value == null) throw new Exception("No value passed to ColorStringBrushConverter");
			switch (value.ToString())
			{
				case "Green":
					oResult = new SolidColorBrush(Colors.Green);
					break;
				case "GreenYellow":
					oResult = new SolidColorBrush(Colors.GreenYellow);
					break;
				case "Yellow":
					oResult = new SolidColorBrush(Colors.Yellow);
					break;
				case "YellowGreen":
					oResult = new SolidColorBrush(Colors.YellowGreen);
					break;
				case "Red":
					oResult = new SolidColorBrush(Colors.Red);
					break;
				case "Azure":
					oResult = new SolidColorBrush(Colors.Azure);
					break;
				default:
					throw new Exception(string.Format("Sorry the value '{0}' passed is not supported in the ColorStringBrushConverter.", value));
			}

			// Return result 
			return oResult;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return null;
		}

		#endregion IMultiValueConverter Members
	}
}
