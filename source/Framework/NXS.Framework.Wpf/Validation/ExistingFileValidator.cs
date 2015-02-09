using System.IO;

namespace NXS.Framework.Wpf.Validation
{
	public class ExistingFileValidator : IInputValidator<string>
	{
		public ExistingFileValidator()
		{
		}

		public static ExistingFileValidator Create()
		{
			return new ExistingFileValidator();
		}

		public bool Validate(string value)
		{
			try
			{
				return File.Exists(value);
			}
			catch
			{
				return false;
			}
		}
	}
}