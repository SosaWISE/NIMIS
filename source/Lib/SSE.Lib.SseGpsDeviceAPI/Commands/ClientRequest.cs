using SOS.Lib.Core.ExceptionHandling;
using SSE.Lib.SseGpsDeviceAPI.Commands.Interface;

namespace SSE.Lib.SseGpsDeviceAPI.Commands
{
	public class ClientRequest
	{
		#region .ctor

		public ClientRequest(CommandDef commandName)
		{
			CommandName = commandName;
		}

		#endregion .ctor

		#region Properties

		public CommandDef CommandName { get; private set; }
		public string CheckSum { get; private set; }

		#endregion Properties

		#region Methods

		protected void CheckSumSet(string chkSum)
		{
			CheckSum = chkSum;
		}

		#endregion Methods
	}

	#region Error Handlers

	public class ClientRequestBaseException : BaseException
	{
		public ClientRequestBaseException(string propertyName, string message)
			: base(message)
		{
			PropertyName = propertyName;
		}
		public string PropertyName { get; private set; }
	}

	public class ClientRequestMissingProperty : ClientRequestBaseException
	{
		#region .ctor
		public ClientRequestMissingProperty(string propertyName)
			: base(propertyName, string.Format("The property '{0}' did not have a valid value.", propertyName))
		{
		}
		#endregion .ctor
	}

	public class ClientRequestLengthProperty : ClientRequestBaseException
	{
		public ClientRequestLengthProperty(string propertyName, int requiredLength, int actualLength)
			: base(propertyName, string.Format("The property '{0}' has a value of length '{1}' when it should be '{2}'."
								 , propertyName, actualLength, requiredLength))
		{
			RequiredLength = requiredLength;
			ActualLength = actualLength;
		}

		protected int RequiredLength { get; private set; }
		protected int ActualLength { get; private set; }
	}
	#endregion Error Handlers
}
