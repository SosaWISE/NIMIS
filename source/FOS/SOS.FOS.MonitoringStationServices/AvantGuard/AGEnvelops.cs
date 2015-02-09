using SOS.FOS.MonitoringStationServices.Contracts.Models;
using Stages;

namespace SOS.FOS.MonitoringStationServices.AvantGuard
{
	public static class AGEnvelops
	{
		public static FosResult<T> GetResult<T>(FosResult<T> fosResult, BaseQueryResult response)
		{
			if (!response.Success && response.ErrorTypeNum != (int) AGErrorCodes.SuccessPlus)
			{
				fosResult.Code = (int) AGErrorCodes.GeneralError;  // This means failure
			}
			else
			{
				fosResult.Code = (int) AGErrorCodes.Success;
			}
			fosResult.Message = response.UserErrorMessage;

			// Return result
			return fosResult;
		}

		//public static FosResult<T> GetResult<T>(FosResult<T> fosResult, XtAdvancedSearchQueryResult response)
		//{
		//	// Set Codes
		//	fosResult.Code = response.ErrorTypeNum == (int)AGErrorCodes.Success || response.ErrorTypeNum == (int)AGErrorCodes.SuccessPlus
		//		? 0
		//		: response.ErrorTypeNum;
		//	fosResult.Message = response.UserErrorMessage;

		//	// Return result
		//	return fosResult;
		//}
	}
}
