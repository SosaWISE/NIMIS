using SSE.Lib.SseGpsDeviceAPI.Commands;
using SSE.Lib.SseGpsDeviceAPI.Commands.Interface;
using SSE.Lib.SseGpsDeviceAPI.ExceptionHandling;
using SSE.Lib.SseGpsDeviceAPI.Helper;

namespace SSE.Lib.SseGpsDeviceAPI.Processor
{
	public class ClientRequestParser
	{
		#region .ctor

		public ClientRequestParser(string rawSentence, Device device)
		{
			RawSentence = rawSentence;
			DeviceObject = device;
		}

		#endregion .ctor

		#region Member Variables

		public string RawSentence { get; private set; }
		public string ResponseSentence { get; private set; }
		public CommandDef ResponseType { get; private set; }
		private Device DeviceObject { get; set; }

		#endregion Member Variables

		#region Memeber Functions

		public object FindResponseObject()
		{
			/** Inititalize. */
			ResponseType = SentenceParser.GetCommandName(RawSentence);

			switch (ResponseType)
			{
				case CommandDef.SIR:
					return new ClientResponseSIR(DeviceObject.AccountID, DeviceObject.IMEI, DeviceObject.SIM, DeviceObject.PPM, DeviceObject.Password, DeviceObject.LowBatteryAlert, DeviceObject.SpeedAlert, DeviceObject.GForceAlert);
				default:
					throw new SseInvalidCommandName(ResponseType.ToString());
			}

		}

		public string GetResponseSentence()
		{
			// ** Initialize.
			object responseObj = FindResponseObject();

			// ** Return result. 
			switch (ResponseType)
			{
				case CommandDef.SIR:
					var responseSIR = (ClientResponseSIR) responseObj;
					ResponseSentence = responseSIR.GetSentence();
					break;

				default:
					throw new SseInvalidCommandName(ResponseType.ToString());
			}

			// ** Return result.
			return ResponseSentence;
		}

		#endregion Memeber Functions
	}
}
