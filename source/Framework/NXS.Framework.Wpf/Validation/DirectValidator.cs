namespace NXS.Framework.Wpf.Validation
{
	public class DirectValidator<T> : IInputValidator<T>
	{
		public DirectValidator()
		{
		}

		public static DirectValidator<T> Create()
		{
			return new DirectValidator<T>();
		}

		public static DirectValidator<T> Create(bool isValid)
		{
			return new DirectValidator<T>()
			{
				IsValid = isValid
			};
		}

		public bool IsValid { get; set; }

		public bool Validate(T value)
		{
			return IsValid;
		}
	}
}
