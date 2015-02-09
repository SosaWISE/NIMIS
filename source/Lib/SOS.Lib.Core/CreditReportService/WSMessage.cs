using System;
using System.Collections.Generic;
using System.Linq;
using SOS.Lib.Core.ErrorHandling;

namespace SOS.Lib.Core.CreditReportService
{
	public class WSMessage
	{
		#region Fields
		
		public Exception Ex { get; set; }
		public string DisplayMessage { get; set; }
		public string Title { get; set; }
		public ErrorMessageType MessageType { get; set; }

		#endregion Fields

		#region Methods
		public static bool HasCriticalError(List<WSMessage> list)
		{
			if (list.Any(wsMessage => (wsMessage.MessageType == ErrorMessageType.Critical) || (wsMessage.MessageType == ErrorMessageType.Exception)))
			{
				return true;
			}

			return false;
		}

		#endregion Methods
	}
}
