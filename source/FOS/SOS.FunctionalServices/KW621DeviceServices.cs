using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Activation;
using SOS.Data.GpsTracking;
using SOS.Data.GpsTracking.ControllerExtensions;
using SOS.Data.Logging;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Helper;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.GpsTracking;
using SOS.FunctionalServices.Contracts.Models.GpsTracking.KW621;
using SOS.FunctionalServices.Models;
using SOS.FunctionalServices.Models.GpsTracking.KW621;
using SOS.Lib.KW621API;
using SOS.Lib.KW621API.Models;

namespace SOS.FunctionalServices
{
	[ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single,ConcurrencyMode = ConcurrencyMode.Multiple)]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.NotAllowed)]
	public class KW621DeviceServices : BaseService, IKW621DeviceServices
	{

		public IFnsResult<string> ExecuteSentence(string sentence, EndPoint remoteEndPoint, Func<IPAddress, long?, IFnsDeviceInfo> returnDeviceInfo = null)
		{
			/** Initialize tranlation function. */
			Func<IPAddress, long?, DeviceInfo> translateReturnDeviceInfo = (oUnitIPAddress, lUnitID) =>
			{
				if (returnDeviceInfo != null)
				{
					IFnsDeviceInfo oFnsDeviceInfo = returnDeviceInfo(oUnitIPAddress, lUnitID);
					return new DeviceInfo { UnitID = oFnsDeviceInfo.UnitID, UnitIPAddress = oFnsDeviceInfo.UnitIPAddress };
				}

				/** Return null. */
				return null;
			};

			/** Call Handle for Execute Sentence. */
			return HandleExecuteSentence(sentence, remoteEndPoint, translateReturnDeviceInfo, ClientApi.Instance.ExecuteSentence);
		}

		public IFnsResult<string> SendRequestSystemInfo(EndPoint remoteEndPoint)
		{
			return HandleSendRequest(remoteEndPoint, ClientApi.Instance.SendSystemInfoRequest);
		}

		/// <summary>
		/// This method returns the commands that are placed in the database that will be pushed to a device.
		/// </summary>
		/// <param name="attemptNumberPerCmd">int.  Number of attemps</param>
		/// <returns></returns>
		public IFnsResult<List<IFnsKwRequest>> QueueItemsGet(int attemptNumberPerCmd)
		{
			var result = GenericServiceWrapper("QueueItemGet", () =>
			{
				/** Locals. */
				var oResult = new FnsResult<List<IFnsKwRequest>> { Code = (int)ErrorCodes.ExecutionInProg, Message = "On Deck for QueueItemsGet." };

			    /** Build list. */
				KW_RequestCollection oCol = GpsTrackingDataContext.Instance.KW_Requests.GetQueue(attemptNumberPerCmd);
				var listRequest = oCol.Select(kwRequest => new FnsKwRequest(kwRequest)).Cast<IFnsKwRequest>().ToList();

				/** Build response. */
				oResult.Code = (int) ErrorCodes.Success;
				oResult.Message = "Success";
				oResult.Value = listRequest;

				/** Return result. */
				return oResult;
			}) as IFnsResult<List<IFnsKwRequest>>;

			/** Return result. */
			return result;
		}

		public IFnsResult<bool> QueueItemIncrementAttempt(long requestID, int incrementBy)
		{
			var result = GenericServiceWrapper("QueueItemIncrementAttempt", () =>
			{
				/** Locals. */
				var oResult = new FnsResult<bool> { Code = (int)ErrorCodes.ExecutionInProg, Message = "On Deck for QueueItemIncrementAttempt." };

				/** Execute increment. */
				KW_Request original = GpsTrackingDataContext.Instance.KW_Requests.LoadByPrimaryKey(requestID);
				KW_Request item = GpsTrackingDataContext.Instance.KW_Requests.IncrementAttempt(requestID, incrementBy);

				/** Set return result. */
				if (original.Attempts == item.Attempts)
				{
					oResult.Code = (int)ErrorCodes.Success;
					oResult.Message = "Success";
					oResult.Value = true;
				}
				else
				{
					oResult.Code = (int)ErrorCodes.GeneralError;
					oResult.Message = string.Format("Increment failed to work for RequestID {0}.", requestID);
					oResult.Value = false;
				}

				/** Return result. */
				return oResult;
			}) as IFnsResult<bool>;

			/** Return result. */
			return result;
		}

		public IFnsResult<bool> QueueItemAttemptSuccessfull(long requestID)
		{
			var result = GenericServiceWrapper("QueueItemAttemptSuccessfull", () =>
			{
				/** Locals. */
				var oResult = new FnsResult<bool> { Code = (int)ErrorCodes.ExecutionInProg, Message = "On Deck for QueueItemAttemptSuccessfull." };

				/** Execute Process. */
				KW_Request original = GpsTrackingDataContext.Instance.KW_Requests.LoadByPrimaryKey(requestID);
				KW_Request item = GpsTrackingDataContext.Instance.KW_Requests.Process(requestID);

				/** Set return result. */
				if (original.ProcessDate == null && item.ProcessDate != null)
				{
					oResult.Code = (int)ErrorCodes.Success;
					oResult.Message = "Success";
					oResult.Value = true;
				}
				else
				{
					oResult.Code = (int)ErrorCodes.GeneralError;
					oResult.Message = string.Format("Process failed to work for RequestID {0}.", requestID);
					oResult.Value = false;
				}

				/** Return result. */
				return oResult;
			}) as IFnsResult<bool>;

			/** Return result. */
			return result;
		}

		#region HandleExecuteSentence

		private IFnsResult<string> HandleExecuteSentence(string sentence, EndPoint remoteEndPoint, Func<IPAddress, long?, DeviceInfo> returnDeviceInfo,  Func<string, EndPoint, Func<IPAddress, long?, DeviceInfo>, Func<string, decimal, decimal, string, string, bool?, bool>, string> action)
		{
			/** Initialize. */
			#region INITIALIZATION

			var oResult = new FnsResult<string>
			{
				Code = (int)ErrorCodes.GeneralMessage
				, Message = string.Format("Initializing {0}", action)
			};

			#endregion INITIALIZATION

			#region TRY

			try
			{
				var sResponse = action(sentence, remoteEndPoint, returnDeviceInfo, DispatchToCs);

				oResult.Code = (int)ErrorCodes.Success;
				oResult.Message = "Success";
				oResult.Value = sResponse;
			}

			#endregion TRY

			#region CATCH

			catch (Exception oEx)
			{
				var sMsg = string.Format("Exception thrown at ExecuteSentence: {0}", oEx.Message);
				DBErrorManager.Instance.AddCriticalMessage(oEx,
					string.Format("****Exception in {0}", action)
					, sMsg);
				oResult = new FnsResult<string>
				{
					Code = (int)ErrorCodes.UnexpectedException
					, Message = sMsg
				};
			}

			#endregion CATCH

			/** Return result. */
			return oResult;
		}

		/// <summary>
		/// This is a very important method since it is the one that calls the Central station.
		/// </summary>
		/// <param name="eventCodeId"></param>
		/// <param name="latitude"></param>
		/// <param name="longitude"></param>
		/// <param name="dispatchMessage"></param>
		/// <param name="csid"></param>
		/// <param name="testFlag"></param>
		/// <returns></returns>
		private bool DispatchToCs(string eventCodeId, decimal latitude, decimal longitude, string dispatchMessage, string csid, bool? testFlag)
		{
			/*
			var oReceiverServices = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();
			// ReSharper disable PossibleInvalidOperationException
			var oResult = oReceiverServices.DispatchFromLaipacDeviceToAGCentralStation(eventCodeId, latitude, longitude, dispatchMessage, csid, testFlag);
			// ReSharper restore PossibleInvalidOperationException

			return oResult.Code == (int)ErrorCodes.Success;
			 * */
			throw new NotImplementedException("The DispatchToCs method has not yet been implemented.");
		}

		#endregion HandleExecuteSentence

		#region HandleSendRequest
		private IFnsResult<string> HandleSendRequest(EndPoint remoteEndPoint, Func<EndPoint, string> action)
		{
			/** Initialize. */
			#region INITIALIZATION

			var oResult = new FnsResult<string>
			{
				Code = (int)ErrorCodes.GeneralMessage
				, Message = string.Format("Initializing {0}", action)
			};

			#endregion INITIALIZATION

			#region TRY

			try
			{
				var sResponse = action(remoteEndPoint);

				oResult.Code = (int)ErrorCodes.Success;
				oResult.Message = "Success";
				oResult.Value = sResponse;
			}

			#endregion TRY

			#region CATCH

			catch (Exception oEx)
			{
				var sMsg = string.Format("Exception thrown at ExecuteSentence: {0}", oEx.Message);
				DBErrorManager.Instance.AddCriticalMessage(oEx,
					string.Format("****Exception in {0}", action)
					, sMsg);
				oResult = new FnsResult<string>
				{
					Code = (int)ErrorCodes.UnexpectedException
					, Message = sMsg
				};
			}

			#endregion CATCH

			/** Return result. */
			return oResult;
		}
		#endregion HandleSendRequest

	}
}
