using System.Windows.Controls;

namespace NXS.Framework.Wpf.Controls
{
	public partial class CopyBox : UserControl
	{
		#region Properties

		/// <summary>
		/// Sets the Text of the CopyBox.
		/// </summary>        
		public string Text
		{
			set { txt_value.Text = value; }
		}

		#endregion //Properties

		public CopyBox()
		{
			InitializeComponent();
		}

		public void Connect(int connectionId, object target)
		{
			throw new System.NotImplementedException();
		}
	}
}
