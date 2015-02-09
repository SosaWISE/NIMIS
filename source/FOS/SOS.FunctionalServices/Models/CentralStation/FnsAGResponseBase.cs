using System.Runtime.Serialization;
using SOS.FOS.MonitoringStationServices.AGSignalService;
using SOS.FunctionalServices.Contracts.Models.CentralStation;
using Stages;

namespace SOS.FunctionalServices.Models.CentralStation
{
	public class FnsAGResponseBase : IFnsAGResponseBase
	{
		#region .ctor

		public FnsAGResponseBase(int nErrorTypeNum, bool bSqlConnectionLost, bool bSuccess, string sUserErrorMessage)
		{
			ErrorTypeNum = nErrorTypeNum;
			SqlConnectionLost = bSqlConnectionLost;
			Success = bSuccess;
			UserErrorMessage = sUserErrorMessage;
		}

		public FnsAGResponseBase (DeviceCopyQueryResult oResult)
		{
			ErrorTypeNum = oResult.ErrorTypeNum;
			SqlConnectionLost = oResult.SqlConnectionLost;
			Success = oResult.Success;
			UserErrorMessage = oResult.UserErrorMessage;
		}

		public int ErrorTypeNum { get; private set; }
		public bool SqlConnectionLost { get; private set; }
		public bool Success { get; private set; }
		public string UserErrorMessage { get; private set; }

		#endregion .ctor
	}

	public class FnsAGResponseSignalBase : IFnsAGResponseSignalBase
	{
		#region .ctor

		public FnsAGResponseSignalBase(Result oResult)
		{
			ErrorNum = oResult.ErrorNum;
			ErrorMessage = oResult.ErrorMessage;
			ExtensionData = oResult.ExtensionData;
		}

		public FnsAGResponseSignalBase(ExtensionDataObject oExtensionData, int nErrorNum, string sErrorMessage)
		{
			ErrorNum = nErrorNum;
			ErrorMessage = sErrorMessage;
			ExtensionData = oExtensionData;
		}

		#endregion .ctor

		#region Implementation of IFnsAGResponseSignalBase

		public int ErrorNum { get; private set; }
		public string ErrorMessage { get; private set; }
		public ExtensionDataObject ExtensionData { get; private set; }

		#endregion Implementation of IFnsAGResponseSignalBase
	}
}
