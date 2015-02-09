using System;
using System.Collections.Generic;
using System.Net;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.GpsTracking;

namespace SOS.FunctionalServices.Contracts
{
	public interface ILaipacDeviceServices : IFunctionalService
	{
		IFnsResult<bool> ValidateResponseSentence(string sentence);

		IFnsResult<string> ExecuteSentence(string sentence, EndPoint remoteEndPoint, Func<IPAddress, long?, IFnsDeviceInfo> returnDeviceInfo = null);

		IFnsResult<string> SendRequestSystemInfo(EndPoint remoteEndPoint);

		IFnsResult<List<IFnsLpRequest>> QueueItemsGet(int attemptNumberPerCmd);

		IFnsResult<bool> QueueItemIncrementAttempt(long requestID, int incrementBy);

		IFnsResult<bool> QueueItemAttemptSuccessfull(long requestID);
	}
}