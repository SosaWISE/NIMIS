namespace SSE.Lib.HE910API.Commands.Interface
{
	public interface IResponse
	{
		 /** Methods */
		string GetResponseBack();

		/** Properties. */
		CommandDef CommandName { get; }
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
		FDA = 8
	}
}