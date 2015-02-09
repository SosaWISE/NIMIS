namespace SOS.Lib.LaipacAPI.Commands
{
	public interface IResponse
	{
		string GetResponseBack();
		CommandDef CommandName { get; } 
	}

	public enum CommandDef
	{
		Undefined = 0,
		AVSYS = 1,
		AVRMC = 2,
		ECHK = 3,
		EAVRSP = 4
	}
}