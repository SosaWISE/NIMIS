using System;

namespace SOS.FOS.MonitoringStationServices.AvantGuard
{
	public static class AGExceptions
	{
		#region Session Authentication and Termination

		public class AGExceptionInvalidAuthentication : Exception
		{
			#region .ctor

			public AGExceptionInvalidAuthentication(int nErrorCode, string sMessage) :base(sMessage)
			{
				ErrorCode = nErrorCode;
			}

			#endregion .ctor

			#region Member Properties

			public int ErrorCode { get; private set; }

			#endregion Member Properties
		}

		public class AGExceptionErrorTerminationSession : Exception
		{
			#region .ctor

			public AGExceptionErrorTerminationSession() : base("Termination call to CS failed") {}

			#endregion .ctor
		}

		public class AGExceptionInvalidSession : Exception
		{
			#region .ctor
			public AGExceptionInvalidSession(string szMessage) : base(szMessage) {}
			#endregion .ctor
		}

		public class AGExceptionDeviceCopy : Exception
		{
			#region .ctor
			public AGExceptionDeviceCopy(int nErrorTypeNum, bool bSuccess, string szMessage) : base(szMessage)
			{
				ErrorTypeNum = nErrorTypeNum;
				Success = bSuccess;
			}
			#endregion .ctor

			#region Member Variables

			public bool Success { get; private set; }
			public int ErrorTypeNum { get; private set; }
			#endregion Member Variables
		}

		#endregion Session Authentication and Termination
	}
}
