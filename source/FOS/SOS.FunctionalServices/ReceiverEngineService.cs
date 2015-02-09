using System;
using System.Collections.Generic;
using System.Web;
using SOS.Data.ReceiverEngine;
using SOS.Data.SosCrm;
using SOS.Data.SosCrm.ControllerExtensions;
using SOS.FOS.MonitoringStationServices.AGSignalService;
using SOS.FOS.ReceiverEngineServices;
using SOS.FOS.ReceiverEngineServices.DeviceProtocols;
using SOS.FOS.ReceiverEngineServices.SmsGateways;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.SmsGateways;

namespace SOS.FunctionalServices
{
	public class ReceiverEngineService : IReceiverEngineService
	{

		#region Methods

		/// <summary>
		/// Given a caller id it will return the MSOID.
		/// 
		/// </summary>
		/// <param name="szCallerId">string</param>
		/// <returns>string</returns>
		public MS_IndustryNumberByCallerIdView GetMonitoringStations(string szCallerId)
		{
			/** Initialize. */
			MS_IndustryNumberByCallerIdViewCollection oMsInfo = SosCrmDataContext.Instance.MS_IndustryNumberByCallerIdViews.GetInfo(szCallerId);

			/** Check that we got result back. */
			if (oMsInfo == null || oMsInfo.Count == 0)
				return null;

			switch (oMsInfo[0].MonitoringStationsID)
			{
				case MS_MonitoringStation.MetaData.Avant_GuardID:
					return oMsInfo[0];
				default:
					return null;
			}
		}

		public bool ProcessTxtWireEvent(RE_TxtWireRequest oTxtRequest)
		{
			/** Initialize. */
			bool bResult = false;

			/** Identify Source. */
			if (oTxtRequest.Message.IndexOf(LaipacDevice.Devices.S911_BL, StringComparison.Ordinal) == 0)
			{
				/** This means that this is a LAIPAC S911 BL device. */
				LaipacDeviceEvent oEvent = LaipacDevice.ParseMessageVersion002(oTxtRequest.Message);

				if (oEvent.Event.Equals(LaipacDeviceEvent.EventTypes.PANIC_ALERT)
					|| oEvent.Event.Equals(LaipacDeviceEvent.EventTypes.G_SENCER))
				{
					DispatchToCentralStation(oTxtRequest, oEvent);
				}

				bResult = true;
			}
			else if (oTxtRequest.Message.IndexOf(LaipacDevice.Devices.ALERT_FROM_S911_BL, StringComparison.Ordinal) == 0)
			{
				/** This means that this is a LAIPAC S911 BL device. */
				LaipacDeviceEvent oEvent = LaipacDevice.ParseMessageVersion001(oTxtRequest.Message);

				/** Check if this is a panic alert. */
				if (oEvent.Event.Equals(LaipacDeviceEvent.EventTypes.PANIC_ALERT)
					|| oEvent.Event.Equals(LaipacDeviceEvent.EventTypes.G_SENCER))
				{
					DispatchToCentralStation(oTxtRequest, oEvent);
					//** Acquired Monitoring Station id based on the callerid. */
					//MS_IndustryNumberByCallerIdView oMsInfo = GetMonitoringStations(oTxtRequest.Phone);

					//** Call the appropriate MS. */
					//switch (oMsInfo.MonitoringStationsID)
					//{
					//    case MS_MonitoringStation.MetaData.AvantGuardID:
					//        var oAGService = new FOS.MonitoringStationServices.AvantGuard.Receiver();
					//        string szDisplayFrase = oEvent.Event.Equals(LaipacDeviceEvent.EventTypes.PANIC_ALERT)
					//                                    ? "Emergency Signal"
					//                                    : "Fall Sensor";
					//        string szCode = oEvent.Event.Equals(LaipacDeviceEvent.EventTypes.PANIC_ALERT)
					//                            ? "E100"
					//                            : "E101";
					//        /** SENDS SIGNAL TO AVANT GUARD. */
					//        Result oSignalResult = oAGService.SendSignal(null
					//            , oMsInfo.Csid
					//            , "CID"
					//            , szCode
					//            , "1"
					//            , null
					//            , null
					//            , oEvent.Event
					//            , DateTime.Now
					//            , null
					//            , oEvent.Longitude
					//            , oEvent.Latitude
					//            , null
					//            , string.Format("http://maps.google.com/maps?q={0},+{1}({2})&hl=en&z=19", oEvent.Latitude, oEvent.Longitude, szDisplayFrase)
					//            , null
					//            , oMsInfo.InTestMode);

					//        /** Check signal result. */
					//        if (oSignalResult.ErrorNum > 0)
					//            throw new Exception(oSignalResult.ErrorMessage);

					//        break;
					//    default:
					//        System.Diagnostics.Debug.WriteLine(string.Format("The following Monitoring Station ID does not support Panic Alerts: {0}", oMsInfo));
					//        break;
					//}
				}

				bResult = true;
			}
			else if (oTxtRequest.Message.IndexOf(LaipacDevice.SocketMessageTypes.AVRMC, StringComparison.Ordinal) == 0)
			{
				/** Get Parsed event. */
				LaipacDeviceEvent oSEvent = LaipacDevice.ParseSocketMessageAVRMC(oTxtRequest.Message);

				switch (oSEvent.Event)
				{
					case LaipacDeviceEvent.EventTypes.PANIC_ALERT:
					case LaipacDeviceEvent.EventTypes.SOS_BUTTON_PRESS_ALERT:
					case LaipacDeviceEvent.EventTypes.TAMPER_DETECT_OPEN:
					case LaipacDeviceEvent.EventTypes.G_SENSOR_ALERT_1:
					case LaipacDeviceEvent.EventTypes.G_SENCER:
						DispatchToCentralStation(oTxtRequest, oSEvent);
						break;
				}

				bResult = true;
			}

			/** Return result. */
			return bResult;
		}

		private void DispatchToCentralStation(RE_TxtWireRequest oTxtRequest, LaipacDeviceEvent oEvent)
		{
			/** Acquired Monitoring Station id based on the callerid. */
			MS_IndustryNumberByCallerIdView oMsInfo = GetMonitoringStations(oTxtRequest.Phone);

			/** Call the appropriate MS. */
			switch (oMsInfo.MonitoringStationsID)
			{
				case MS_MonitoringStation.MetaData.Avant_GuardID:
					var oAGService = new FOS.MonitoringStationServices.AvantGuard.Receiver();
					string szDisplayFrase = oEvent.Event;
					switch (oEvent.Event)
					{
						case LaipacDeviceEvent.EventTypes.PANIC_ALERT:
							szDisplayFrase = "Panic Alert !";
							break;
						case LaipacDeviceEvent.EventTypes.SOS_BUTTON_PRESS_ALERT:
							szDisplayFrase = "SOS Button Pressed Alert !";
							break;
						case LaipacDeviceEvent.EventTypes.TAMPER_DETECT_OPEN:
							szDisplayFrase = "Tamper Detection Band has been opened !";
							break;
						case LaipacDeviceEvent.EventTypes.G_SENSOR_ALERT_1:
							szDisplayFrase = "G Sensor 1 was fired !";
							break;
						case LaipacDeviceEvent.EventTypes.G_SENCER:
							szDisplayFrase = "G Sensor was fired.  Possible fall occurred";
							break;
					}

					string szCode = (oEvent.Event.Equals(LaipacDeviceEvent.EventTypes.PANIC_ALERT)
						|| oEvent.Event.Equals(LaipacDeviceEvent.EventTypes.SOS_BUTTON_PRESS_ALERT)
						|| oEvent.Event.Equals(LaipacDeviceEvent.EventTypes.TAMPER_DETECT_OPEN)
						|| oEvent.Event.Equals(LaipacDeviceEvent.EventTypes.G_SENSOR_ALERT_1)
						|| oEvent.Event.Equals(LaipacDeviceEvent.EventTypes.G_SENCER))
										? "E100"
										: "E101";
					/** SENDS SIGNAL TO AVANT GUARD. */
					Result oSignalResult = oAGService.SendSignal(null
						, oMsInfo.Csid
						, "CID"
						, szCode
						, "1"
						, null
						, null
						, oEvent.Event
						, DateTime.Now
						, null
						, oEvent.Longitude
						, oEvent.Latitude
						, null
						, string.Format("http://maps.google.com/maps?q={0},+{1}({2})&hl=en&z=19", oEvent.Latitude, oEvent.Longitude, szDisplayFrase)
						, null
						, oMsInfo.InTestMode);

					/** Check signal result. */
					if (oSignalResult.ErrorNum > 0)
						throw new Exception(oSignalResult.ErrorMessage);

					break;
				default:
					System.Diagnostics.Debug.WriteLine(string.Format("The following Monitoring Station ID does not support Panic Alerts: {0}", oMsInfo));
					break;
			}
		}

		#endregion Methods

		#region Implementation of IReceiverEngineService

		public IFnsREResult SaveRequest(HttpContext oContext)
		{
			/** Initialize. */
			var oRE = new Main();

			REResult oResult = oRE.SaveRequest(oContext);

			var oReturnResult = new Models.FnsREResult(oResult);

			/** Return result. */
			return oReturnResult;
		}

		public IFnsREResult SaveTxtWireSignal(string szTitle, string szCode, string szShortCode, string szMessage, string szPhone,
						string szCarrier, string szKeyword, string szGroupName, string szCustomTicket, string szDefaultKeyword,
						string szUsername, string szPassword, string szApiKey)
		{
			/** Initialize. */
			var oRE = new Main();

			REResult oResult = oRE.SaveSignal(szTitle, szCode, szShortCode, szMessage, szPhone,
						szCarrier, szKeyword, szGroupName, szCustomTicket, szDefaultKeyword,
						szUsername, szPassword, szApiKey);

			/** Initialize returning value. */
			var oReturnResult = new Models.FnsREResult(oResult);

			if (!ProcessTxtWireEvent((RE_TxtWireRequest)oResult.Value))
				return new Models.FnsREResult((int)REErrorCodes.GeneralError, "Error processing TxtWire event", false);

			/** Return result. */
			return oReturnResult;
		}

		/// <summary>
		/// Given the arguments it will save the signal in the txtwire receiver table and send the signal to AvantGuard.
		/// </summary>
		/// <param name="oModelEnv">IFnsSmsGwyTxtEnvModel</param>
		/// <returns>IFnsREResult</returns>
		public IFnsResult ExecuteSignalToAvantGuard(IFnsSmsGwyTxtEnvModel oModelEnv)
		{
			/** Initialize. */
			var oModel = new TxtWireEnvModel
				{
					Title = oModelEnv.Title,
					Code = oModelEnv.Code,
					ShortCode = oModelEnv.ShortCode,
					Message = oModelEnv.Message,
					Phone = oModelEnv.Phone,
					Carrier = oModelEnv.Carrier,
					Keyword = oModelEnv.Keyword,
					GroupName = oModelEnv.GroupName,
					CustomTicket = oModelEnv.CustomTicket,
					DefaultKeyword = oModelEnv.DefaultKeyword,
					Username = oModelEnv.Username,
					Password = oModelEnv.Password,
					ApiKey = oModelEnv.ApiKey,
				};
			/*******
			 * Function that will call 
			 ******/
			Func<TxtWireEnvModel, List<REResult>, REResult> fxSuccess = delegate(TxtWireEnvModel oTxtWireModel, List<REResult> oList)
				{
					/** Initialize. */
					var oResultNew = new REResult(REErrorCodes.GeneralError, "Initializing the submission to AvantGuard.", null);
					var oAGService = new FOS.MonitoringStationServices.AvantGuard.Receiver();

					/** Execute the signal to AvantGuard. */


					/** Add to the list of results. */
					oList.Add(oResultNew);

					/** Return result. */
					return oResultNew;
				};

			/** Execute the ExecuteSignalToAvantGuard. */
			var oResult = (IFnsREResult)TxtWire.ReceiveSignal(oModel, fxSuccess, null);

			/** Return result. */
			return (IFnsResult) oResult;
		}

		#endregion Implementation of IReceiverEngineService
	}
}