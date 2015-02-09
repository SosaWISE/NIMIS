using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using NXS.Framework.Wpf.Validation;

namespace NXS.Framework.Wpf.Converters
{
	public class MaskedValidatedInputVisibilityConverter : IValueConverter
	{
		#region Singleton Implementation

		private MaskedValidatedInputVisibilityConverter()
		{
		}

		public static MaskedValidatedInputVisibilityConverter Instance
		{
			get { return Nested.ConverterInstance; }
		}

		private class Nested
		{
			internal static readonly MaskedValidatedInputVisibilityConverter ConverterInstance =
				new MaskedValidatedInputVisibilityConverter();

			static Nested()
			{
			}
		}

		#endregion Singleton Implementation

		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var input = (MaskedValidatedInput) value;
			if (!input.EmptyMask.Equals(input.Value))
				return Visibility.Visible;
			return Visibility.Collapsed;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Convert(value, targetType, parameter, culture);
		}

		#endregion
	}
}