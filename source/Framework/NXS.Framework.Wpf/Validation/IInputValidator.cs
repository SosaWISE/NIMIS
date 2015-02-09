namespace NXS.Framework.Wpf.Validation
{
	public interface IInputValidator<T>
	{
		bool Validate(T value);
	}
}
