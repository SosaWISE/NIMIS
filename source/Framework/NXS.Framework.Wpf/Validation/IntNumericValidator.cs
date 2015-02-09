namespace NXS.Framework.Wpf.Validation
{
	public class IntNumericValidator : IInputValidator<string>
	{
		public IntNumericValidator()
		{
		}

		public static IntNumericValidator Create()
		{
			return new IntNumericValidator();
		}

		#region IInputValidator<string> Members

		public bool Validate(string value)
		{
			if (!string.IsNullOrEmpty(value)) {

				int nResult;
				return int.TryParse(value, out nResult);
			}
			return true;
		}

		#endregion
	}
}