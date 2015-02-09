using SSE.Lib.HE910API.Commands.Interface;

namespace SSE.Lib.HE910API.Commands
{
	public class Response : IResponse
	{
		#region .ctor

		public Response(CommandDef commandName)
		{
			CommandName = commandName;
		}

		#endregion .ctor

		#region Methods
		public string GetResponseBack()
		{
			return string.Empty;
		}
		#endregion Methods

		#region Properties
		public CommandDef CommandName { get; private set; }
		#endregion Properties
	}
}
