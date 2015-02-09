namespace SOS.FOS.MerchantServices.Helper
{
	public enum ErrorCodes
	{
		Success = 0
		, GeneralMerchant = 40000
		, Initializing = 40010
		, MerchantReturnedNull = 40100
		, MerchantTransFailed = 40200
	}

	public static class FosErrorExtensions
	{
		public static string Message(this ErrorCodes code)
		{
			switch (code)
			{
				case ErrorCodes.Success:
					return "Success";
				case ErrorCodes.GeneralMerchant:
					return "General Merchant Message";
				case ErrorCodes.Initializing:
					return "Intializing method '{0}'.";
				case ErrorCodes.MerchantReturnedNull:
					return "The merchant '{0}' returned a null.  This could mean that it timed out.";
				case ErrorCodes.MerchantTransFailed:
					return "The merchant '{0}' processed a transaction and it failed with the following message: \"{1}\"";

				default:
					return "Unknown Error";
			}
		}
	}
}
