using System.ComponentModel;

namespace NXS.Framework.Wpf.Validation
{
	public interface IValidatedInput : INotifyPropertyChanged
	{
		void Clean();
		string Name { get; set; }
		bool IsDirty { get; }
		bool IsValid { get; }
		object InputValue { get; }
		string InvalidMessage { get; set; }
		string NullValueString { get; set; }
		void RunValidation();
		void Reset();
		void RemoveValidation();

		object GetInputValue();
		void SetInputValue(object obj);

	}
}