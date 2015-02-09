using SOS.Data.GpsTracking;
using SOS.Data.GpsTracking.ControllerExtensions;
using SOS.Data.Logging;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Helper;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.GpsTracking;
using SOS.FunctionalServices.Models;
using SOS.FunctionalServices.Models.GpsTracking;
using SOS.Lib.LaipacAPI;
using SOS.Lib.LaipacAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace SOS.FunctionalServices
{
	[ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.NotAllowed)]
	public class LaipacDeviceServices : BaseService, ILaipacDeviceServices
	{
		public IFnsResult<bool> ValidateResponseSentence(string sentence)
		{
			/** Initialize. */
			#region INITIALIZATION

			var oResult = new FnsResult<bool>
			{
				Code = (int)ErrorCodes.GeneralMessage
				, Message = "Initializing ValidateResponseSentence"
			};

			#endregion INITIALIZATION

			#region TRY

			try
			{
				string checkSum = Lib.LaipacAPI.Helper.SentenceParser.GetCheckSumRawSentence(sentence);
				string[] sentenceObj = Lib.LaipacAPI.Helper.SentenceParser.RawSentenceToSentence(sentence);
				bool resultValue = checkSum.Equals(sentenceObj[Lib.LaipacAPI.Helper.SentenceParser.Fields.ChkSum]);

				oResult.Code = (int)ErrorCodes.Success;
				oResult.Message = "Success";
				oResult.Value = resultValue;
			}

			#endregion TRY

			#region CATCH

			catch (Exception oEx)
			{
				var sMsg = string.Format("Exception thrown at ValidateResponseSentence: {0}", oEx.Message);
				DBErrorManager.Instance.AddCriticalMessage(oEx,
					"Exception in ValidateResponseSentence"
					, sMsg);
				oResult = new FnsResult<bool>
				{
					Code = (int)ErrorCodes.UnexpectedException
					, Message = sMsg
				};
			}

			#endregion CATCH

			/** Return result. */
			return oResult;
		}

		public IFnsResult<string> ExecuteSentence(string sentence, EndPoint remoteEndPoint, Func<IPAddress, long?, IFnsDeviceInfo> returnDeviceInfo = null)
		{
			/** Initialize translation function. */
			Func<IPAddress, long?, DeviceInfo> translateReturnDeviceInfo = (oUnitIPAddress, lUnitID) =>
			{
				if (returnDeviceInfo != null)
				{
					IFnsDeviceInfo oFnsDeviceInfo = returnDeviceInfo(oUnitIPAddress, lUnitID);
					return new DeviceInfo{UnitID = oFnsDeviceInfo.UnitID, UnitIPAddress = oFnsDeviceInfo.UnitIPAddress};
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

		public IFnsResult<List<IFnsLpRequest>> QueueItemsGet(int attemptNumberPerCmd)
		{
			/** Initialize. */
			var result = GenericServiceWrapper("QueueItemsGet", () =>
			{
			    /** Locals. */
			    var oResult = new FnsResult<List<IFnsLpRequest>> { Code = (int)ErrorCodes.ExecutionInProg, Message = "On Deck for QueueItemsGet." };

				/** Build list. */
				LP_RequestCollection oCol = GpsTrackingDataContext.Instance.LP_Requests.GetQueue(attemptNumberPerCmd);
				var listRequests = oCol.Select(lpRequest => new FnsLpRequest(lpRequest)).Cast<IFnsLpRequest>().ToList();

				oResult.Code = (int) ErrorCodes.Success;
				oResult.Message = "Success";
				oResult.Value = listRequests;

				/** Return result. */
			    return oResult;
			}) as IFnsResult<List<IFnsLpRequest>>;

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
				LP_Request original = GpsTrackingDataContext.Instance.LP_Requests.LoadByPrimaryKey(requestID);
				LP_Request item = GpsTrackingDataContext.Instance.LP_Requests.IncrementAttempt(requestID, incrementBy);

				/** Set return result. */
				if (original.Attempts == item.Attempts)
				{
					oResult.Code = (int) ErrorCodes.Success;
					oResult.Message = "Success";
					oResult.Value = true;
				}
				else
				{
					oResult.Code = (int) ErrorCodes.GeneralError;
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
				LP_Request original = GpsTrackingDataContext.Instance.LP_Requests.LoadByPrimaryKey(requestID);
				LP_Request item = GpsTrackingDataContext.Instance.LP_Requests.Process(requestID);

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
			var oResult = oReceiverServices.DispatchFromLaipacDeviceToAGCentralStation(eventCodeId, latitude, longitude, dispatchMessage, csid, testFlag);
// ReSharper restore PossibleInvalidOperationException

			return oResult.Code == (int) ErrorCodes.Success;
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