using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NXS.Framework.Wpf.Controls
{
	/// <summary>
	/// Interaction logic for EditableTextBox.xaml
	/// </summary>
	public partial class EditableTextBox : UserControl
	{
		bool _isEditing;
		string _originalText;

		public IEditableValue Context
		{
			get { return (IEditableValue)DataContext; ;}
		}

		public EditableTextBox()
		{
			InitializeComponent();

			DataContextChanged += new DependencyPropertyChangedEventHandler(EditableTextBox_DataContextChanged);
		}

		private void txtSelectAll_GotFocus(object sender, RoutedEventArgs e)
		{
			ViewHelper.TextBoxSelectAll(sender);
		}

		void EditableTextBox_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			if (_isEditing)
				throw new Exception("DataContext cannot be changed during edit.");

			if (e.OldValue != null) {
				IEditableValue ev = (IEditableValue)e.OldValue;
				if (ev != null) {
					ev.EditingChanged -= ev_EditingChanged;
				}
			}
			if (e.NewValue != null) {
				IEditableValue ev = (IEditableValue)e.NewValue;
				if (ev != null) {
					ev.EditingChanged += ev_EditingChanged;
				}
			}
		}
		void ev_EditingChanged(object sender, EventArgs e)
		{
			IEditableValue ev = Context;

			_isEditing = ev.IsEditing;
			_originalText = string.Empty;

			if (_isEditing) {

				_originalText = txtValue.Text;

				//txtValue.

				//select all of text box if we just started editing
				txtValue.Focus();
				txtValue.SelectAll();
			}
		}
		private void txtValue_LostFocus(object sender, RoutedEventArgs e)
		{
			Context.IsEditing = false;
		}
		private void txtValue_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter) {

				e.Handled = true;

				EndFocus();
			}
			else if (e.Key == Key.Escape) {

				//reset value to original
				txtValue.Text = _originalText;

				EndFocus();
			}
		}
		private void EndFocus()
		{
			txtValue_LostFocus(null, new RoutedEventArgs());
			btnEdit.Focus();
		}

		private void TextBox_MouseDown(object sender, MouseButtonEventArgs e)
		{

		}

		public void Connect(int connectionId, object target)
		{
			throw new NotImplementedException();
		}
	}
}
