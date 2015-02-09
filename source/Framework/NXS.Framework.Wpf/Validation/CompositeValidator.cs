using System.Collections.Generic;

namespace NXS.Framework.Wpf.Validation
{
	public class CompositeValidator<T> : List<IInputValidator<T>>, IInputValidator<T>
	{
		#region IInputValidator<T> Members

		public bool Validate(T value)
		{
			bool valid = true;
			foreach (IInputValidator<T> validator in this) {
				if (!validator.Validate(value)) {
					valid = false;
					break;
				}
			}
			return valid;
		}

		#endregion

		public static CompositeValidator<T> Create()
		{
			return new CompositeValidator<T>();
		}

		public new CompositeValidator<T> Add(IInputValidator<T> validator)
		{
			base.Add(validator);
			return this;
		}
	}
}