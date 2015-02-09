using System;
using System.ComponentModel;
using System.Windows.Input;
using NXS.Framework.Wpf.Mvvm;
using SOS.Lib.Util;

namespace NXS.Framework.Wpf.Validation
{
	public class EditableValidatedInput<T> : ValidatedInput<T>, IEditableValue
	{
		readonly static PropertyChangedEventArgs IsEditingChangeArgs = ObservableHelper.CreateArgs<EditableValidatedInput<T>>(a => a.IsEditing);
		private bool _isEditing;
		public bool IsEditing
		{
			get { return _isEditing; }
			set
			{
				if (_isEditing == value) return;

				_isEditing = value;

				OnPropertyChanged(IsEditingChangeArgs);
				OnEditingChanged(EventArgs.Empty);
			}
		}
		readonly static PropertyChangedEventArgs CanEditChangeArgs = ObservableHelper.CreateArgs<EditableValidatedInput<T>>(a => a.CanEdit);
		private bool _CanEdit;
		public bool CanEdit
		{
			get { return _CanEdit; }
			set
			{
				if (_CanEdit == value) return;

				_CanEdit = value;

				OnPropertyChanged(CanEditChangeArgs);
			}
		}

		private RelayCommand _editCommand;
		public ICommand EditCommand
		{
			get
			{
				if (_editCommand == null) {
					_editCommand = new RelayCommand((param) => IsEditing = true, (param) => CanEdit);
				}
				return _editCommand;
			}
		}

		public event EventHandler<EventArgs> EditingChanged;
		protected virtual void OnEditingChanged(EventArgs e)
		{
			if (EditingChanged != null) {
				EditingChanged(this, e);
			}
		}

		public string GetTextValue()
		{
			if (Value == null) {
				return null;
			}

			if (Value is string) {
				return Value as string;
			}
			else {
				return Value.ToString();
			}
		}
	}
}
