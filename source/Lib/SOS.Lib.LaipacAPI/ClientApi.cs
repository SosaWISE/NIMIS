using System;
using System.Collections.Generic;
using System.Net;
using SOS.Data.GpsTracking;
using SOS.Data.GpsTracking.ControllerExtensions;
using SOS.Data.Logging;
using SOS.Data.SosCrm;
using SOS.Lib.LaipacAPI.Commands;
using SOS.Lib.LaipacAPI.ExceptionHandling;
using SOS.Lib.LaipacAPI.Models;
using NXS.Lib;

namespace SOS.Lib.LaipacAPI
{
	public class ClientApi
	{
		#region singleton .ctor
		
		private ClientApi()
		{
		}

		private static volatile ClientApi _instance;
		private static readonly object _instanceSync = new object();
		public static ClientApi Instance
		{
			get
			{
				if (_instance == null)
				{
					lock(_instanceSync)
					{
						if (_instance == null)
						{
							_instance = new ClientApi();
						}
					}
				}

				/** Return instance. */
				return _instance;
			}
		}


		#endregion singleton .ctor

		#region Member Variables
		private const int _DEFAULT_NUMBER_OF_GEOFENCES = 5;
		private const int _NUM_OF_SQLPINGS = 3;
		#endregion Member Variables

		#region Member Functions

		#region HandleRequestSentence and ReadSentence

		/// <summary>
		/// This handles all the exceptions.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="sentence">string</param>
		/// <param name="remoteEndPoint">EndPoint</param>
		/// <param name="returnDeviceInfo">function</param>
		/// <param name="dispatchToCs">function</param>
		/// <param name="action">Func<T />Function</param>
		/// <returns>T</returns>
		public T HandleRequestSentence<T>(string sentence, EndPoint remoteEndPoint, Func<IPAddress, long?, DeviceInfo> returnDeviceInfo, Func<string, decimal, decimal, string, string, bool?, bool> dispatchToCs, Func<string, EndPoint, Func<IPAddress, long?, DeviceInfo>, Func<string, decimal, decimal, string, string, bool?, bool>, T> action)
		{
			try
			{
				// ** Validate CheckSum. */
				string checkSum = Helper.SentenceParser.GetCheckSumRawSentence(sentence);
				string[] sentenceObj = Helper.SentenceParser.RawSentenceToSentence(sentence);
				bool resultValue = checkSum.Equals(sentenceObj[Helper.SentenceParser.Fields.ChkSum]);

				if (resultValue)
					return action(sentence, remoteEndPoint, returnDeviceInfo, dispatchToCs);

				/** Default path of execution. */
				throw new LaipacChkSumFailed(checkSum, sentenceObj[Helper.SentenceParser.Fields.ChkSum]
					, "Failed checksum.");
			}
			catch (Exception oEx)
			{
				DBErrorManager.Instance.AddCriticalMessage(oEx
					, "Critical Error"
					, string.Format("Exception thrown in HandleRequestSentence on {0}"
						, action));
				throw;
			}
		}

		private string ReadSentence(string sentence, EndPoint remoteEndPoint, Func<IPAddress, long?, DeviceInfo> returnDeviceInfo = null, Func<string, decimal, decimal, string, string, bool?, bool> dispatchToCs = null)
		{
			/** Initialize. */
			var oResponse = (IResponse) Response.Factory(sentence, remoteEndPoint, dispatchToCs);
			IPAddress unitIPAddress = ((IPEndPoint) (remoteEndPoint)).Address;

			if (oResponse != null)
			{
				switch (oResponse.CommandName)
				{
					case CommandDef.AVSYS:
						var oAVSYSResponse = (AVSYSResponse)oResponse;
						if (returnDeviceInfo != null) returnDeviceInfo(unitIPAddress, oAVSYSResponse.UnitID);
						return oAVSYSResponse.GetResponseBack();
					case CommandDef.AVRMC:
						var oAVRMCResponse = (AVRMCResponse) oResponse;
						if (returnDeviceInfo != null) returnDeviceInfo(unitIPAddress, oAVRMCResponse.UnitID);
						return oAVRMCResponse.GetResponseBack();
					case CommandDef.ECHK:
						var oECHKResponse = (ECHKResponse) oResponse;
						if (returnDeviceInfo != null) returnDeviceInfo(unitIPAddress, oECHKResponse.UnitID);
						return oECHKResponse.GetResponseBack();

				}
			}


			/** Default path of execution. */
			return oResponse != null 
				? oResponse.GetResponseBack()
				: string.Empty;
		}

		#endregion HandleRequestSentence and ReadSentence

		public string ExecuteSentence(string sentence, EndPoint remoteEndPoint, Func<IPAddress, long?, DeviceInfo> returnDeviceInfo = null, Func<string, decimal, decimal, string, string, bool?, bool> dispatchToCs = null)
		{
			return HandleRequestSentence(sentence, remoteEndPoint, returnDeviceInfo, dispatchToCs, ReadSentence);
		}

		public string SendSystemInfoRequest(EndPoint remoteEndPoint)
		{
			/** Validate Client Table. */
			DBErrorManager.Instance.AddSuccessMessage("****Sending Initial Handshake to Device"
				, string.Format("Remote End Point: {0}", remoteEndPoint));

			/** Return request. */
			var systemInfoRequest = new SystemInfoRequest(remoteEndPoint);
			var responseString = systemInfoRequest.GetRequest();

			/** Display Message to output device. */
			DBErrorManager.Instance.AddSuccessMessage("**** ->System Info Request:", string.Format("-->{0}", responseString));

			/** Return string. */
			return responseString;
		}

		public GS_AccountGeoFencesView CreateFence(long lAccountId, string reportMode, string geoFenceName, string geoFenceDescription, double dMaxLattitude, double dMaxLongitude, double dMinLattitude, double dMinLongitude, short? zZoomLevel, string sUser)
		{
			/** Initialize. */
			var msAccount = SosCrmDataContext.Instance.GS_Accounts.LoadByPrimaryKey(lAccountId);
			long lUnitID = Convert.ToInt64(msAccount.GpsWatchUnitID);
			// ** Check to see that there is no more than 5 geofences already present.
			int numberOfFences;
			if (!int.TryParse(WebConfig.Instance.GetConfig("LAIPAC.S911MaxFenceNumber"), out numberOfFences)) 
				numberOfFences = _DEFAULT_NUMBER_OF_GEOFENCES;
			LP_GsGeoFence newFence = GpsTrackingDataContext.Instance.LP_GsGeoFences.GetNewFence(lAccountId, lUnitID, numberOfFences);

			#region Create Sentence and queue it.
			/** Create sentence and place in queue */
			var fenceItem = new List<EAVGOFRequest.FenceItem>
			                	{
			                		new EAVGOFRequest.FenceItem
			                			{
			                				GeoFence = newFence.GeoFenceI,
			                				ReportMode = Convert.ToChar(reportMode),
			                				Latitude1 = dMaxLattitude,
			                				Longtitude1 = dMinLongitude,
			                				Latitude2 = dMinLattitude,
			                				Longtitude2 = dMaxLongitude
			                			}
			                	};
			var oRequest = new EAVGOFRequest(msAccount.GpsWatchPassword, lUnitID, fenceItem);
			oRequest.QueueRequest(lAccountId);
			#endregion Create Sentence and queue it.

			#region Catch device's response
			/** Catch device's response. */
			LP_CommandMessageEAVRSP3 response;
			var tries = 0;
			do
			{
				/** Wait for a response from the device. */
				System.Threading.Thread.Sleep(5000);

				/** Get Response back. */
				response = GpsTrackingDataContext.Instance.LP_CommandMessageEAVRSP3s.ProcessByUnitID(lUnitID);
				// ** Check
				if (response != null) break;

				/** Increment counter. */
				tries++;
			} while (tries <= _NUM_OF_SQLPINGS);

			/** Check if command was returned successfully. */
			if (response == null || !response.RESPCode.Equals(Response.RESPCode.OK)) return null;
			#endregion Catch device's response

			#region Build the fence infrasturcture
			/** Build the fence infrasturcture. */
			GS_AccountGeoFenceRectangle rectangleFence =
				GpsTrackingDataContext.Instance.GS_AccountGeoFenceRectangles.Create(lAccountId, reportMode, geoFenceName, geoFenceDescription, dMaxLattitude, dMinLongitude, dMinLattitude, dMaxLongitude, zZoomLevel, sUser);
			var gsGeoFence = new LP_GsGeoFence
			                 	{
			                 		UnitID = lUnitID,
									GsGeoFenceId = rectangleFence.GeoFenceID,
									GeoFenceI = newFence.GeoFenceI,
									ReportModeI = Convert.ToByte(reportMode),
									LattitudeI1 = dMaxLattitude,
									LongitudeI1 = dMinLongitude,
									LattitudeI2 = dMinLattitude,
									LongitudeI2 = dMaxLongitude,
									IsActive =  true,
									IsDeleted = false,
									DEX_ROW_TS = DateTime.UtcNow
			                 	};
			gsGeoFence.Save(sUser);
			var result = GpsTrackingDataContext.Instance.GS_AccountGeoFencesViews.LoadByPrimaryKey(rectangleFence.GeoFenceID);
			#endregion Build the fence infrasturcture

			/** Return result. */
			return result;
		}

		public GS_AccountGeoFencesView UpdateFence(long lGeoFenceID, long lAccountId, string reportMode, string geoFenceName, string geoFenceDescription, double dMaxLattitude, double dMaxLongitude, double dMinLattitude, double dMinLongitude, short? zZoomLevel, string sUser)
		{
			/** Initialize. */
			var msAccount = SosCrmDataContext.Instance.GS_Accounts.LoadByPrimaryKey(lAccountId);
			long lUnitID = Convert.ToInt64(msAccount.GpsWatchUnitID);
			LP_GsGeoFence gsGeoFence = GpsTrackingDataContext.Instance.LP_GsGeoFences.GetByGsGeoFenceID(lGeoFenceID);

			#region Create Sentence and queue it.
			/** Create sentence and place in queue */
			var fenceItem = new List<EAVGOFRequest.FenceItem>
			                	{
			                		new EAVGOFRequest.FenceItem
			                			{
			                				GeoFence = gsGeoFence.GeoFenceI,
			                				ReportMode = Convert.ToChar(reportMode),
			                				Latitude1 = dMaxLattitude,
			                				Longtitude1 = dMinLongitude,
			                				Latitude2 = dMinLattitude,
			                				Longtitude2 = dMaxLongitude
			                			}
			                	};
			var oRequest = new EAVGOFRequest(msAccount.GpsWatchPassword, lUnitID, fenceItem);
			oRequest.QueueRequest(lAccountId);
			#endregion Create Sentence and queue it.

			#region Catch device's response
			/** Catch device's response. */
			LP_CommandMessageEAVRSP3 response;
			var tries = 0;
			do
			{
				/** Wait for a response from the device. */
				System.Threading.Thread.Sleep(5000);

				/** Get Response back. */
				response = GpsTrackingDataContext.Instance.LP_CommandMessageEAVRSP3s.ProcessByUnitID(lUnitID);
				// ** Check
				if (response != null) break;

				/** Increment counter. */
				tries++;
			} while (tries <= _NUM_OF_SQLPINGS);

			/** Check if command was returned successfully. */
			if (response == null || !response.RESPCode.Equals(Response.RESPCode.OK)) return null;
			#endregion Catch device's response

			#region Build the fence infrasturcture
			/** Build the fence infrasturcture. */
			GS_AccountGeoFenceRectangle rectangleFence =
				GpsTrackingDataContext.Instance.GS_AccountGeoFenceRectangles.Update(lGeoFenceID, lAccountId, reportMode, geoFenceName, geoFenceDescription, dMaxLattitude, dMinLongitude, dMinLattitude, dMaxLongitude, zZoomLevel, sUser);
	
			gsGeoFence.UnitID = lUnitID;
			gsGeoFence.GsGeoFenceId = rectangleFence.GeoFenceID;
			gsGeoFence.ReportModeI = Convert.ToByte(reportMode);
			gsGeoFence.LattitudeI1 = dMaxLattitude;
			gsGeoFence.LongitudeI1 = dMinLongitude;
			gsGeoFence.LattitudeI2 = dMinLattitude;
			gsGeoFence.LongitudeI2 = dMaxLongitude;
			gsGeoFence.Save(sUser);

			var result = GpsTrackingDataContext.Instance.GS_AccountGeoFencesViews.LoadByPrimaryKey(rectangleFence.GeoFenceID);
			#endregion Build the fence infrasturcture

			/** Return result. */
			return result;
		}

		#endregion Member Functions
	}
}