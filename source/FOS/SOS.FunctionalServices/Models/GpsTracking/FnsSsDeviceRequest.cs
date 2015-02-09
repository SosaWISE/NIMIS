using System;
using SOS.Data.GpsTracking;
using SOS.FunctionalServices.Contracts.Models.GpsTracking;

namespace SOS.FunctionalServices.Models.GpsTracking
{
	public class FnsSsDeviceRequest : IFnsSsDeviceRequest
	{
		#region .ctor

		public FnsSsDeviceRequest(SS_DeviceRequest request)
		{
			DeviceRequestID = request.DeviceRequestID;
			CommandMessageId = request.CommandMessageId;
			AccountId = request.AccountId;
			Sentence = request.Sentence;
			Attempts = request.Attempts;
			LastAttemptDate = request.LastAttemptDate;
			ProcessDate = request.ProcessDate;
			CreatedOn = request.CreatedOn;
		}

		#endregion .ctor

		#region Member Variables
		public long DeviceRequestID { get; private set; }
		public long? CommandMessageId { get; private set; }
		public long AccountId { get; private set; }
		public string Sentence { get; private set; }
		public int? Attempts { get; private set; }
		public DateTime? LastAttemptDate { get; private set; }
		public DateTime? ProcessDate { get; private set; }
		public DateTime CreatedOn { get; private set; }
		#endregion Member Variables
	}
}
