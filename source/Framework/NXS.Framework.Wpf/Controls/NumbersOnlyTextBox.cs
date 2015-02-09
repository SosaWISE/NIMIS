using System.Windows.Controls;
using System.Windows.Input;
using SOS.Lib.Util;

namespace NXS.Framework.Wpf.Controls
{
	public class NumbersOnlyTextBox : TextBox
	{
		public bool DecimalAllowed { get; set; }

		/// <summary>
		/// override this method to only allow numbers
		/// </summary>
		/// <param name="e">Arguments for event</param>
		protected override void OnPreviewTextInput(TextCompositionEventArgs e)
		{
			e.Handled = !StringUtility.IsValidNumber(e.Text, DecimalAllowed);
			base.OnPreviewTextInput(e);
		}

		/// <summary>
		/// override this method to catch space characters
		/// </summary>
		/// <param name="e">Arguments for event</param>
		protected override void OnPreviewKeyDown(KeyEventArgs e)
		{
			base.OnPreviewKeyDown(e);
			e.Handled = e.Key == Key.Space;
		}
	}
}
