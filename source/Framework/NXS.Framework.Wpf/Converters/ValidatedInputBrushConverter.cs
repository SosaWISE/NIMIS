#region Using Directives

using System;
using System.Windows.Data;
using System.Windows.Media;

#endregion

namespace NXS.Framework.Wpf.Converters
{
	//0 - IsValid
	//1 - IsDirty
	public class ValidatedInputBrushConverter : IMultiValueConverter
	{
		#region Singleton Implementation

		public static ValidatedInputBrushConverter Instance
		{
			get { return Nested.ConverterInstance; }
		}

		private class Nested
		{
			internal static readonly ValidatedInputBrushConverter ConverterInstance = new ValidatedInputBrushConverter();
		}

		#endregion Singleton Implementation

		private static readonly Brush _red = new SolidColorBrush(Color.FromRgb(255, 115, 115));
		private static readonly Brush _yellow = new SolidColorBrush(Color.FromRgb(255, 209, 102));

		#region IMultiValueConverter Members

		public object Convert(object[] value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (targetType != typeof (Brush))
			{
				throw new Exception("Target type is not Brush");
			}

			Brush result;
			if (!GetBoolean(value, 0))
			{
				//!IsValid
				result = _red;
			}
			else if (GetBoolean(value, 1))
			{
				//IsDirty
				result = _yellow;
			}
			else
			{
				result = Brushes.Transparent;
			}
			return result;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter,
		                            System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		#endregion

		private bool GetBoolean(object[] ray, int index)
		{
			if (index < ray.Length)
			{
				object obj = ray[index];
				if (obj != null && obj.GetType() == typeof (bool))
				{
					return (bool) obj;
				}
				//else {
				//    int t = 0;
				//}
			}
			return false;
		}
	}
}