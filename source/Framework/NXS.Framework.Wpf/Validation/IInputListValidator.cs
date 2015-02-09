
namespace NXS.Framework.Wpf.Validation
{
    public interface IInputListValidator<T, ListItemType>
    {
        bool Validate(T value);
    }
}
