using System;
using System.Windows.Controls;

namespace NXS.Framework.Wpf.Mvvm.Views
{
	/// <summary>
	/// Interaction logic for ErrorMessageDisplayView.xaml
	/// </summary>
	public partial class ErrorMessageDisplayView : UserControl
	{
		public ErrorMessageDisplayView()
		{
			InitializeComponent();
		}

		public void Connect(int connectionId, object target)
		{
			throw new NotImplementedException();
		}
	}
}
