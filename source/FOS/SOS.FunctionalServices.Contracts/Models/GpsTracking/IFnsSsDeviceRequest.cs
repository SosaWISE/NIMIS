using System;

namespace SOS.FunctionalServices.Contracts.Models.GpsTracking
{
	public interface IFnsSsDeviceRequest
	{
		long DeviceRequestID { get; }
		long? CommandMessageId { get; }
		long AccountId { get; }
		string Sentence { get; }
		int? Attempts { get; }
		DateTime? LastAttemptDate { get; }
		DateTime? ProcessDate { get; }
		DateTime CreatedOn { get; }
	}
}