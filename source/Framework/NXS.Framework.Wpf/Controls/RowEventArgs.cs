using System;
using System.Windows.Controls;

namespace NXS.Framework.Wpf.Controls
{
	public class RowEventArgs : EventArgs
	{
		#region Properties

		#region Private
		#endregion Private

		#region Public

		public ListViewItem SelectedItem { get; set; }

		#endregion Public

		#endregion Properties

		#region Constructors

		public RowEventArgs()
		{
		}

		public RowEventArgs(ListViewItem selectedItem)
		{
			SelectedItem = selectedItem;
		}

		#endregion Constructors

		#region Methods

		#region Private
		#endregion Private

		#region Public
		#endregion Public

		#endregion Methods
	}
}