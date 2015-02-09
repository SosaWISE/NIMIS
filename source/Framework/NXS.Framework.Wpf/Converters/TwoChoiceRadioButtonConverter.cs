using System;
using System.Windows.Data;

namespace NXS.Framework.Wpf.Converters
{
	public class TwoChoiceRadioButtonConverter : IValueConverter
	{
		#region Singleton Implementation

		private TwoChoiceRadioButtonConverter()
		{
		}

		public static TwoChoiceRadioButtonConverter Instance
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

			internal static readonly TwoChoiceRadioButtonConverter ConverterInstance = new TwoChoiceRadioButtonConverter();
		}

		#endregion Singleton Implementation

		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			bool param = bool.Parse(parameter.ToString());
			if (value == null)
			{
				return false;
			}
			return !((bool)value ^ param);
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			bool param = bool.Parse(parameter.ToString());
			return !((bool)value ^ param);

		}

		#endregion
	}
}
