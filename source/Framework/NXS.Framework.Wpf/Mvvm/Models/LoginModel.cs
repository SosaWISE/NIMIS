using NXS.Framework.Wpf.Validation;

namespace NXS.Framework.Wpf.Mvvm.Models
{
	public class LoginModel : ModelBase
	{
		#region Inputs

		public ValidatedInput<string> Username { get; private set; }
		public ValidatedInput<string> Password { get; private set; }

		#endregion //Inputs

		public LoginModel()
		{
			StringLengthValidator requiredString = StringLengthValidator.Create(1, 0);

			#region Add Inputs

			AddInput(Username = new ValidatedInput<string>
			{
				Name = "Username",
				Validator = requiredString,
			});
			AddInput(Password = new ValidatedInput<string>
			{
				Name = "Password",
				Validator = requiredString,
			});

			#endregion //Add Inputs

			RunValidation();
		}
	}
}
