namespace SSE.Services.CmsCORS.Helpers
{
	public enum CmsResultCodes
	{
		Success = 0
		, Initializing = 1
		, ImplementationMissing = 2

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
		, NotAuthorized = 20220

		, UnexpectedException = 20300

		, SqlExceptions = 30000

		, MerchantServices = 40000

		, GPSDeviceCommError = 50000
		, GPSDeviceNoResponse = 50100
		, GPSDeviceNoLocation = 50200
	}

	public static class CmsErrorExtensions
	{
		public static string Message(this CmsResultCodes code)
		{
			switch (code)
			{
				case CmsResultCodes.Success:
					return "Success";
				case CmsResultCodes.Initializing:
					return "Initializing";
				case CmsResultCodes.LoginFailure:
					return "Login Failure";
				case CmsResultCodes.UsernameInvalid:
					return "Invalid Username";
				case CmsResultCodes.PasswordInvalid:
					return "Invalid Password";
				case CmsResultCodes.ArgumentValidationFailed:
					return "Invalid Arguments";

				default:
					return "Unknown Error";
			}
		}
	}
}