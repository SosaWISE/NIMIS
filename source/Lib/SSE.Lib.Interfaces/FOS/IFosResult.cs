namespace SSE.Lib.Interfaces.FOS
{
	public interface IFosResult
	{
		#region Properties

		int Code { get; }
		string Message { get; }

		object GetValue();

		#endregion Properties
	}

	public interface IFosResult<T> : IFosResult
	{
		T Value { get; set; }
	}

	public enum ResultCodes
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

		// ** Address Verification / Validation
		, AddressValidationSuccess = 60000
		, AddressValidationError = 60100
		, AddressValidationInuptError = 60200
		, AddressValidationUnexpectedError = 60300
	}
}
