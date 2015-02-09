using System.Windows.Controls;

namespace NXS.Framework.Wpf
{
	public static class ViewHelper
	{
		public static void TextBoxSelectAll(object sender)
		{
			TextBox txt = sender as TextBox;
			if (txt != null)
				txt.SelectAll();
		}
		public static void PasswordBoxSelectAll(object sender)
		{
			PasswordBox txt = sender as PasswordBox;
			if (txt != null)
				txt.SelectAll();
		}
		public static void PasswordBoxFocus(object sender)
		{
			PasswordBox txt = sender as PasswordBox;
			if (txt != null)
				txt.Focus();
		}
	}
}
