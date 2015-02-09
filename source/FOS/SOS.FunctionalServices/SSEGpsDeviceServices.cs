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
using SOS.FunctionalServices.Models;
using SOS.FunctionalServices.Models.GpsTracking;
using SSE.Lib.SseGpsDeviceAPI.Models;

//using SOS.Lib.LaipacAPI.Models;

namespace SOS.FunctionalServices
{
	[ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.NotAllowed)]
	public class SSEGpsDeviceServices : BaseService, ISSEGpsDeviceServices
	{
		/// <summary>
		/// Validates a sentece received.
		/// </summary>
		/// <param name="sentence"></param>
		/// <returns></returns>
		public IFnsResult<bool> ValidateResponseSentence(string sentence)
		{
			/** Initialize. */
			#region INITIALIZATION

			var oResult = new FnsResult<bool>
			{
				Code = (int)ErrorCodes.GeneralMessage
				,
				Message = "Initializing ValidateResponseSentence"
			};

			#endregion INITIALIZATION

			/** Return result. */
			return oResult;
		}

		/// <summary>
		/// Gets the next item in the queue that has not been processed.
		/// </summary>
		/// <param name="attemptNumberPerCmd"></param>
		/// <returns></returns>
		public IFnsResult<List<IFnsSsDeviceRequest>> QueueItemGet(int attemptNumberPerCmd)
		{
			/** Initialize and Execute. */
			var result = GenericServiceWrapper("QueueItemGet", () =>
				{
					/** Locals. */
					var oResult = new FnsResult<List<IFnsSsDeviceRequest>> { Code = (int)ErrorCodes.ExecutionInProg, Message = "On Deck for QueueItemsGet." };

					/** Build list. */
					SS_DeviceRequestCollection oCol = GpsTrackingDataContext.Instance.SS_DeviceRequests.GetQueue(attemptNumberPerCmd);
					var listRequests = oCol.Select(ssRequest => new FnsSsDeviceRequest(ssRequest)).Cast<IFnsSsDeviceRequest>().ToList();

					oResult.Code = (int)ErrorCodes.Success;
					oResult.Message = "Success";
					oResult.Value = listRequests;

					/** Return result. */
					return oResult;
				}) as IFnsResult<List<IFnsSsDeviceRequest>>;

			/** Return result. */
			return result;
		}

		public IFnsResult<string> SendRequestSystemInfo(EndPoint remoteEndPoint)
		{
			return HandleSendRequest(remoteEndPoint, SSE.Lib.SseGpsDeviceAPI.ClientAPI.Instance.SendSystemInfoRequest);
		}

		public IFnsResult<string> ExecuteSentence(string sentence, EndPoint remoteEndPoint, Func<IPAddress, long?, IFnsDeviceInfo> returnDeviceInfo = null)
		{
			// ** Initialize Translation function
			Func<IPAddress, long?, DeviceInfo> translateReturnDeviceInfo = (oUnitIPAddress, unitID) =>
				{
					// ** CHeck that there is a DeviceInfo
					if (returnDeviceInfo != null)
					{
						IFnsDeviceInfo fnsDeviceInfo = returnDeviceInfo(oUnitIPAddress, unitID);
						return new DeviceInfo{AccountID = fnsDeviceInfo.AccountID, UnitIPAddress = fnsDeviceInfo.UnitIPAddress };
					}

					// ** Default path of execution
					return null;
				};

			// ** Call handle for execute sentence.
			return HandleExecuteSentence(sentence, remoteEndPoint, translateReturnDeviceInfo, SSE.Lib.SseGpsDeviceAPI.ClientAPI.Instance.ExecuteSentence);
		}

		public IFnsResult<bool> QueueItemIncrementAttempt(long requestID, int incrementBy)
		{
			/** Initialize. */
			var result = GenericServiceWrapper("QueueItemIncrementAttempt", () =>
			{
				/** Locals. */
				var oResult = new FnsResult<bool> { Code = (int)ErrorCodes.ExecutionInProg, Message = "On Deck for QueueItemIncrementAttempt." };

				/** Execute increment. */
				SS_DeviceRequest original = GpsTrackingDataContext.Instance.SS_DeviceRequests.LoadByPrimaryKey(requestID);
				SS_DeviceRequest item = GpsTrackingDataContext.Instance.SS_DeviceRequests.IncrementAttempt(requestID, incrementBy);

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
			/** Initialize. */
			var result = GenericServiceWrapper("QueueItemAttemptSuccessfull", () =>
			{
				/** Locals. */
				var oResult = new FnsResult<bool> { Code = (int)ErrorCodes.ExecutionInProg, Message = "On Deck for QueueItemAttemptSuccessfull." };

				/** Execute process. */
				SS_DeviceRequest original = GpsTrackingDataContext.Instance.SS_DeviceRequests.LoadByPrimaryKey(requestID);
				SS_DeviceRequest item = GpsTrackingDataContext.Instance.SS_DeviceRequests.Process(requestID);

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

		private IFnsResult<string> HandleExecuteSentence(string sentence, EndPoint remoteEndPoint, Func<IPAddress, long?, DeviceInfo> returnDeviceInfo, Func<string, EndPoint, Func<IPAddress, long?, DeviceInfo>, Func<string, decimal, decimal, string, string, bool?, bool>, string> action)
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

		private bool DispatchToCs(string eventCodeId, decimal latitude, decimal longitude, string dispatchMessage, string csid, bool? testFlag)
		{
			var oReceiverServices = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();
			// ReSharper disable PossibleInvalidOperationException
			var oResult = oReceiverServices.DispatchFromSSEDeviceToAGCentralStation(eventCodeId, latitude, longitude, dispatchMessage, csid, testFlag);
			// ReSharper restore PossibleInvalidOperationException

			return oResult.Code == (int)ErrorCodes.Success;
		}

		#endregion HandleExecuteSentence

		#region HandleSendRequest

		private IFnsResult<string> HandleSendRequest(EndPoint remoteEndPoint, Func<EndPoint, string> action)
		{
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

				oResult.Code = (int) ErrorCodes.Success;
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