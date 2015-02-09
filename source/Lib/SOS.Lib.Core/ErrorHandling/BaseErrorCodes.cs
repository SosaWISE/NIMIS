/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 11/26/14
 * Time: 10:54
 * 
 * Description:  Error codes for all of applications.
 *********************************************************************************************************************/
namespace SOS.Lib.Core.ErrorHandling
{
	public static class BaseErrorCodes
	{
		//#region Internal Instance

		//private static BaseErrorCodes _instance;
		//private static readonly object SyncRootContextInstance = new object();

		//public static BaseErrorCodes Instance
		//{
		//	get
		//	{
		//		if (_instance == null)
		//		{
		//			lock (SyncRootContextInstance)
		//			{
		//				if (_instance == null)
		//				{
		//					_instance = new BaseErrorCodes();
		//				}
		//			}
		//		}
		//		return _instance;
		//	}
		//}

		//#endregion  Internal Instance

		#region Properties
		public enum ErrorCodes
		{
			#region General Warnings
			NotImplemented = -1
			, Success = 0
			, Initializing = 1
			, GeneralWarning = 2
			, ExecutionInProg = 5
			, GeneralMessage = 10
			, DuplicateItem = 20
			, GeneralException = 100
			#endregion General Warnings

			#region General Methods
			, CallInitialization = 1000
			, ArgumentValidation = 1100
			#endregion General Methods

			#region Authentication Messages
			, LoginFailure = 10000
			, LogoutFailure = 10001
			, UsernameInvalid = 10010
			, PasswordInvalid = 10020
			, DealerIdInvalid = 10030
			, CookieInvalid = 10040
			#endregion Authentication Messages

			#region General Errors and Sessions
			, GeneralError = 20000
			, SessionExp = 20100
			, SessionAlreadyInUse = 20110
			, ExceptionThrown = 20200
			, InvalidCredentials = 20210
			, InvalidPrimaryKeyId = 20220
			#endregion General Errors and Sessions

			#region General Exceptions
			, UnexpectedException = 20300
			#endregion General Exceptions

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

			#region Monitoring Station Errors
			, MSServices = 80000
			, MSAccountOnboardError = 80100
			, MSAccountOnboardMissingConfNumber = 80110
			, MSAccountNoSignalHistoryFound = 80200
			, MSAccountInitTwoWayFailed = 80210
			, MSAccountOnboardSuccessful = 80300
			#endregion Monitoring Station Errors

			#region Central Station Messages
			, CSServices = 90000
			, CSLookupNotFound = 90100
			, CSLookupFailed = 90200
			, CSDispatchAgencyNotFound = 90300
			#endregion Central Station Messages
		}

		#endregion Properties

		#region Methods

		public static int Code(this ErrorCodes code)
		{
			return (int) code;
		}

		public static string Message(this ErrorCodes code)
		{
			switch (code)
			{
			#region General Warnings
				case ErrorCodes.NotImplemented:
					return "'{0}' not implemented yet....";
				case ErrorCodes.Initializing:
					return "Initializing '{0}'.";
				case ErrorCodes.Success:
					return "Success";
				case ErrorCodes.GeneralMessage:
					return "General PurchaseMessageDescription";
			#endregion General Warnings

			#region General Methods
				case ErrorCodes.ArgumentValidation:
					return "Argument Validation failed for the following arguments passed to '{0}' method: \r\n{1}";
			#endregion General Methods

			#region Authentication Messages
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
					return "Exception thrown at '{1}': {0}";
			#endregion Authentication Messages

			#region General Errors and Sessions
				case ErrorCodes.ExceptionThrown:
					return "The following exception was thrown in '{0}' method:\r\n{1}";
				case ErrorCodes.InvalidPrimaryKeyId:
					return "The primary key passed ('{0}') did not return a value.";
			#endregion General Errors and Sessions

				case ErrorCodes.MSAccountOnboardError:
					return "While onboarding AccountID '{0}' with CSID '{1}' the following error(s) was generated:\r\n{2}";
				case ErrorCodes.MSAccountOnboardMissingConfNumber:
					return "While onboarding AccountID '{0}' with CSID '{1}' the confirmation # was not found.  The following error was generated:\r\n{2}";
				case ErrorCodes.MSAccountInitTwoWayFailed:
					return "While trying to initiate two-way test with CSID '{0}' the following error was generated:\r\n{1}";
				case ErrorCodes.MSAccountOnboardSuccessful:
					return "SUCCESFULL Onboarding Account.  Confirmation #{0}";

			#region Central Station Messages
				case ErrorCodes.CSLookupNotFound:
					return "The central station lookup method '{0}' did not return anything.";
				case ErrorCodes.CSLookupFailed:
					return "The central station lookup method '{0}' falied with the following error message:\r\n{1}";
				case ErrorCodes.CSDispatchAgencyNotFound:
					return "The central station '{0}' with arguments \"{1}\" was not able to find a Dispatch Agency.  Please call Central Station at this number '{2}' to add the agency to their system.";
				case ErrorCodes.MSAccountNoSignalHistoryFound:
					return "The central station '{0}' did not return any signal history for CSID '{1}'.";

					#endregion Central Station Messages
				default:
					return "Unknown Error";
			}
		}

		#endregion Methods
	}
}
