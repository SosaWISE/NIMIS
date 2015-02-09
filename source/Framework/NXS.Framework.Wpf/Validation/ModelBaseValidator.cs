using NXS.Framework.Wpf.Mvvm;

namespace NXS.Framework.Wpf.Validation
{
	public class ModelBaseValidator : IInputValidator<ModelBase>
	{
		public ModelBaseValidator()
		{
		}

		public static ModelBaseValidator Create()
		{
			return new ModelBaseValidator();
		}

		#region IInputValidator<ModelBase> Members

		public bool Validate(ModelBase value)
		{
			bool valid = true;
			foreach (IValidatedInput input in value.Inputs) {
				if (!input.IsValid) {
					valid = false;
					break;
				}
			}
			return valid;
		}

		#endregion
	}
}
