using SOS.Data.GpsTracking;

namespace SSE.Lib.SseGpsDeviceAPI.Commands.Interface
{
	public interface IResponse
	{
		 /** Methods */
		string GetResponseBack();

		/** Properties. */
		CommandDef CommandName { get; }
		SS_CommandMessage CommandMessage { get; }
		string RawSentence { get; }
	}

	public enum CommandDef
	{
		Undefined = 0,
		PDE = 1,
		SOS = 2,
		LBA = 3,
		BTA = 4,
		BSA = 5,
		EZB = 6,
		IZB = 7,
		FDA = 8,
		SIR = 9,
		ERR = 10
	}
}