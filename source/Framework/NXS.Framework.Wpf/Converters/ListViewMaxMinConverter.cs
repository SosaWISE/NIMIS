using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace NXS.Framework.Wpf.Converters
{
	public class ListViewMaxMinConverter : IValueConverter
	{
		#region Singleton Implementation

		private ListViewMaxMinConverter()
		{
		}

		public static ListViewMaxMinConverter Instance
		{
			get { return Nested.ConverterInstance; }
		}

		private class Nested
		{
			internal static readonly ListViewMaxMinConverter ConverterInstance = new ListViewMaxMinConverter();
		}

		#endregion Singleton Implementation

		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool result = true;
			var item = (ListViewItem) value;
			var listView = ItemsControl.ItemsControlFromItemContainer(item) as ListView;
			var currentArrow = (string) parameter;

			if (currentArrow.Equals("Up"))
			{
				if (listView.ItemContainerGenerator.IndexFromContainer(item) == 0)
					result = false;
			}

			if (currentArrow.Equals("Down"))
			{
				if (listView.ItemContainerGenerator.IndexFromContainer(item) == (listView.Items.Count - 1))
					result = false;
			}

			return result;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}