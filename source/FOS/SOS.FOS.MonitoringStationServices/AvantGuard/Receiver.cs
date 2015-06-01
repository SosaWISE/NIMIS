using System;
using SOS.FOS.MonitoringStationServices.AGSignalService;
using NXS.Lib;

namespace SOS.FOS.MonitoringStationServices.AvantGuard
{
	public class Receiver
	{
		#region .ctor
		public Receiver()
		{
			_username = WebConfig.Instance.GetConfig("AG_USERNAME");
			_password = WebConfig.Instance.GetConfig("AG_PASSWORD");
			//_applname = WebConfig.Instance.GetConfig("AG_APPLNAME");
			//_clenplat = WebConfig.Instance.GetConfig("AG_CLIENTPLATFORM");
			//_appvrson = WebConfig.Instance.GetConfig("AG_APPLIC_VERISON");
			_urledpnt = WebConfig.Instance.GetConfig("AG_SIGNALR_ENDPNT");

			_stagesAPIClient = new ReceiverSoapClient(_urledpnt);
		}

		#endregion .ctor

		#region Member Variables

		//public SessionInfo SessionInfo { get; private set; }

		private readonly string _username;
		private readonly string _password;
		//private readonly string _applname;
		//private readonly string _clenplat;
		//private readonly string _appvrson;
		private readonly string _urledpnt;

		private readonly ReceiverSoapClient _stagesAPIClient;

		#endregion Member Variables

		#region Member Functions

		public Result SendSignal(bool? bPollMessageFlag, string sTransmitterCode, string sSignalFormat
			, string sSignalCode, string sPoint, string sArea, string sUserId, string sText, DateTime? dDate
			, string sANIPhone, decimal? mLongitude, decimal? mLatitude, string sFileName, string sURL
			, string sVideoType, bool? bTestSignalFlag)
		{
			/** Execute. */
			Result oResponse = _stagesAPIClient.Signal(
				bPollMessageFlag
				, _username
				, _password
				, null
				, null
				, sTransmitterCode
				, sSignalFormat
				, sSignalCode
				, sPoint
				, sArea
				, sUserId
				, sText
				, dDate
				, sANIPhone
				, mLongitude
				, mLatitude
				, sFileName
				, sURL
				, sVideoType
				, bTestSignalFlag);

			/** Return result. */
			return oResponse;
		}

		#endregion Member Functions
	}
}