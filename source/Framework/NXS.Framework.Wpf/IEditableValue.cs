using System;
using System.Windows.Input;

namespace NXS.Framework.Wpf
{
	public interface IEditableValue
	{
		bool IsValid { get; }
		bool IsDirty { get; }
		bool IsEditing { get; set; }
		bool CanEdit { get; set; }
		ICommand EditCommand { get; }
		event EventHandler<EventArgs> EditingChanged;

		string GetTextValue();
	}
}
