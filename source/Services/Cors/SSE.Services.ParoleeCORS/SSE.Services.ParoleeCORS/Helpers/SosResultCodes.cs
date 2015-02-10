namespace SSE.Services.ParoleeCORS.Helpers
{
	public enum SosResultCodes
	{
		Success = 0
		, Initializing = 1

		, LoginFailure = 10000
		, UsernameInvalid = 10010
		, PasswordInvalid = 10020
		, DealerIdInvalid = 10030
		, CookieInvalid = 10040

		, ArgumentValidating = 10300
		, ArgumentValidationFailed = 10301

		, GeneralError = 20000
		, SessionExp = 20100
		, SessionAlreadyInUse = 20110
		, ExceptionThrown = 20200
		, InvalidCredentials = 20210

		, UnexpectedException = 20300

		, SqlExceptions = 30000

		, MerchantServices = 40000

		, GPSDeviceCommError = 50000
		, GPSDeviceNoResponse = 50100
		, GPSDeviceNoLocation = 50200
	}

	public static class SosErrorExtensions
	{
		public static string Message(this SosResultCodes code)
		{
			switch (code)
			{
				case SosResultCodes.Success:
					return "Success";
				case SosResultCodes.Initializing:
					return "Initializing";
				case SosResultCodes.LoginFailure:
					return "Login Failure";
				case SosResultCodes.UsernameInvalid:
					return "Invalid Username";
				case SosResultCodes.PasswordInvalid:
					return "Invalid Password";
				case SosResultCodes.ArgumentValidationFailed:
					return "Invalid Arguments";

				default:
					return "Unknown Error";
			}
		}
	}
}