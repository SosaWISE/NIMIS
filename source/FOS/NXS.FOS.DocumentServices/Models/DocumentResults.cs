using SOS.Lib.Core.ErrorHandling;

namespace NXS.FOS.DocumentServices.Models
{
	public class DocumentResults
	{
		#region .ctor

		public DocumentResults(ErrorMessageType messageType, string message)
		{
			MessageType = messageType;
			Message = message;
		}

		#endregion .ctor

		#region Properties
		public ErrorMessageType MessageType { get; private set; }
		public string Message { get; private set; }
		#endregion Properties

	}
}
