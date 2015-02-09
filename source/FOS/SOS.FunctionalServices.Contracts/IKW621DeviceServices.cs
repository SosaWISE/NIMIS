using System;
using System.Collections.Generic;
using System.Net;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.GpsTracking;
using SOS.FunctionalServices.Contracts.Models.GpsTracking.KW621;

namespace SOS.FunctionalServices.Contracts
{
	public interface IKW621DeviceServices : IFunctionalService
	{
		IFnsResult<List<IFnsKwRequest>> QueueItemsGet(int attemptNumberPerCmd);
		IFnsResult<bool> QueueItemIncrementAttempt(long requestID, int incrementBy);
		IFnsResult<bool> QueueItemAttemptSuccessfull(long requestID);
		IFnsResult<string> ExecuteSentence(string sentence, EndPoint remoteEndPoint, Func<IPAddress, long?, IFnsDeviceInfo> returnDeviceInfo = null);
		IFnsResult<string> SendRequestSystemInfo(EndPoint remoteEndPoint);
	}
}