using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace NXS.Framework.Wpf.Converters
{
	public class IsValidToBrushConverter : IValueConverter
	{
		#region Singleton Implementation

		public static IsValidToBrushConverter Instance
		{
			get { return Nested.ConverterInstance; }
		}

		private class Nested
		{
			internal static readonly IsValidToBrushConverter ConverterInstance = new IsValidToBrushConverter();

			static Nested()
			{
			}
		}

		#endregion Singleton Implementation

		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (targetType != typeof (Brush))
			{
				throw new Exception("Target type is not Brush");
			}

			var isValid = (bool) value;

			bool match = ConverterHelper.AsBoolean(parameter);
			if (!match)
			{
				isValid = !isValid;
			}

			// Return result
			return isValid ? Brushes.Transparent : new SolidColorBrush(Color.FromRgb(255, 221, 221));
		}

		public object ConvertBack(object value, Type targetType, object parameter,
			CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		#endregion IValueConverter Members
	}
}