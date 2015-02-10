namespace SOS.Services.Wcf.Crm.Helper
{
	public enum SosResultCodes
	{
		Success = 0
		, GeneralMessage = 10
		, DuplicateItem = 20

		, CallInitialization = 1000
		, ArgumentValidation = 1100

		, LoginFailure = 10000
		, UsernameInvalid = 10010
		, PasswordInvalid = 10020
		, DealerIdInvalid = 10030
		, CookieInvalid = 10040

		, GeneralError = 20000
		, SessionExp = 20100
		, SessionAlreadyInUse = 20110
		, LogoutFailure = 20201
		, InvalidCredentials = 20210

		, UnexpectedException = 20300

		, SqlExceptions = 30000

		, MerchantServices = 40000
	}

	public static class SosErrorExtensions
	{
		public static string Message (this SosResultCodes code)
		{
			switch (code)
			{
				case SosResultCodes.Success:
					return "Success";
				case SosResultCodes.LoginFailure:
					return "Login Failure";
				case SosResultCodes.UsernameInvalid:
					return "Invalid Username";
				case SosResultCodes.PasswordInvalid:
					return "Invalid Password";
				default:
					return "Unknown Error";
			}
		}
	}
}