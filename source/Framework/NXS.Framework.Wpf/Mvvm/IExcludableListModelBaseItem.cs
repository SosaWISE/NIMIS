using NXS.Framework.Wpf.Validation;

namespace NXS.Framework.Wpf.Mvvm
{
	public interface IExcludableListModelBaseItem : IValidatedInput
	{
		ObservableValueContainer<bool> IncludeInValidation { get; }
	}
}
