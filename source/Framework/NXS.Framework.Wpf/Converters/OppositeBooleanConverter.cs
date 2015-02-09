using System;
using System.Windows.Data;

namespace NXS.Framework.Wpf.Converters
{
	public class OppositeBooleanConverter : IValueConverter
	{
		#region Singleton Implementation

		private OppositeBooleanConverter()
		{
		}

		public static OppositeBooleanConverter Instance
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

			internal static readonly OppositeBooleanConverter ConverterInstance = new OppositeBooleanConverter();
		}

		#endregion Singleton Implementation

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
			if (value != null)
				return !((bool)value);
			else
				return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Convert(value, targetType, parameter, culture);
        }

        #endregion
	}
}