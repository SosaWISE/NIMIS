using System;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.GpsTracking;
using System.Collections.Generic;
using System.Net;

namespace SOS.FunctionalServices.Contracts
{
	public interface ISSEGpsDeviceServices : IFunctionalService
	{
		IFnsResult<bool> ValidateResponseSentence(string sentence);

		IFnsResult<List<IFnsSsDeviceRequest>> QueueItemGet(int attemptNumberPerCmd);

		IFnsResult<string> SendRequestSystemInfo(EndPoint remoteEndPoint);

		IFnsResult<string> ExecuteSentence(string sentence, EndPoint remoteEndPoint, Func<IPAddress, long?, IFnsDeviceInfo> returnDeviceInfo = null);

		IFnsResult<bool> QueueItemIncrementAttempt(long requestID, int incrementBy);

		IFnsResult<bool> QueueItemAttemptSuccessfull(long requestID);
	}
}