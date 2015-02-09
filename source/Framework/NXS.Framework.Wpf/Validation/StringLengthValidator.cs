namespace NXS.Framework.Wpf.Validation
{
	public class StringLengthValidator : IInputValidator<string>
	{
		public int MinimumLength { get; private set; }
		public int MaximumLength { get; private set; }

		public StringLengthValidator(int minimumLength, int maximumLength)
		{
			MinimumLength = minimumLength;
			MaximumLength = maximumLength;
		}

		public static StringLengthValidator Create(int minimumLength, int maximumLength)
		{
			return new StringLengthValidator(minimumLength, maximumLength);
		}

		#region IInputValidator<string> Members

		public bool Validate(string value)
		{
			int length = (value == null) ? 0 : value.Trim().Length;

			bool valid =
				(MinimumLength == 0 || length >= MinimumLength)
				&& (MaximumLength == 0 || length <= MaximumLength)
				;

			return valid;
		}

		#endregion
	}
}