/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 01/19/12
 * Time: 08:40
 * 
 * Description:  Error codes for services.
 *********************************************************************************************************************/
namespace SOS.FunctionalServices.Contracts.Helper
{
	public enum ErrorCodes
	{
		Success = 0
		, Initializing = 1
		, GeneralWarning = 2
		, ExecutionInProg = 5
		, GeneralMessage = 10
		, DuplicateItem = 20
		, GeneralException = 100

		, CallInitialization = 1000
		, ArgumentValidation = 1100

		, LoginFailure = 10000
		, LogoutFailure = 10001
		, UsernameInvalid = 10010
		, PasswordInvalid = 10020
		, DealerIdInvalid = 10030
		, CookieInvalid = 10040

		, GeneralError = 20000
		, SessionExp = 20100
		, SessionAlreadyInUse = 20110
		, ExceptionThrown = 20200
		, InvalidCredentials = 20210

		, UnexpectedException = 20300

		, MerchantServices = 40000

		, GPSDeviceCommError = 50000
		, GPSDeviceNoResponse = 50100
		, GPSDeviceNoLocation = 50200

		, CreditReportError = 60000

		, SqlExceptions = 70000
		, SqlResultIsEmpty = 70100
		, SqlItemNotFound = 70110
		, SqlItemDuplicate = 70120
		, SqlItemNullException = 70130
		, SqlArgValidationFailed = 70140
        , SqlInvalidEquipmentMoveToCustomer = 70150
	}

	public static class SosErrorExtensions
	{
		public static string Message(this ErrorCodes code)
		{
			switch (code)
			{
				case ErrorCodes.Success:
					return "Success";
				case ErrorCodes.GeneralMessage:
					return "General PurchaseMessageDescription";

				case ErrorCodes.SessionExp:
					return "Session Expired";
				case ErrorCodes.SessionAlreadyInUse:
					return "Session in use or exp";
				case ErrorCodes.LoginFailure:
					return "Login Failure";
				case ErrorCodes.InvalidCredentials:
					return "Invalid Credentials";
				case ErrorCodes.UsernameInvalid:
					return "Invalid Username";
				case ErrorCodes.PasswordInvalid:
					return "Invalid Password";
				case ErrorCodes.UnexpectedException:
					return "Unexpected Exception thrown.";

				default:
					return "Unknown Error";
			}
		}
	}
}
