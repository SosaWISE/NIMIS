namespace SOS.Lib.KW621API.Commands
{
	public interface IResponse
	{
		string GetResponseBack();
		CommandDef CommandName { get; }
	}

	public enum CommandDef
	{
		Undefined = 0,
		HE910 = 910
	}
}