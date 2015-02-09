using System;
using System.Windows.Controls;
using System.Windows.Data;

namespace NXS.Framework.Wpf.Converters
{
	public class BooleanScrollBarVisibilityConverter : IValueConverter
	{
		#region Singleton Implementation

		private BooleanScrollBarVisibilityConverter()
		{
		}

		public static BooleanScrollBarVisibilityConverter Instance
		{
			get { return Nested.ConverterInstance; }
		}

		private class Nested
		{
			internal static readonly BooleanScrollBarVisibilityConverter ConverterInstance = new BooleanScrollBarVisibilityConverter();
		}

		#endregion Singleton Implementation

		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			bool match = ConverterHelper.AsBoolean(parameter);

			if (value is bool)
			{
				if ((bool) value == match)
					return ScrollBarVisibility.Auto;
				else
					return ScrollBarVisibility.Disabled;
			}
			else
			{
				bool valueAsBool;
				if (bool.TryParse(value as string, out valueAsBool))
				{
					if (valueAsBool == match)
						return ScrollBarVisibility.Auto;
					else
						return ScrollBarVisibility.Disabled;
				}
				else
					return ScrollBarVisibility.Auto;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return null;
		}

		#endregion
	}
}
