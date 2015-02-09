using System;
using System.Collections.Generic;
using System.Linq;
using SOS.Lib.Core.ErrorHandling;

namespace SOS.Lib.Core.CreditReportService.Message
{
	public class MessageController
	{
		#region .ctor

		public MessageController()
		{
			MessageList = new List<IMessage>();
		}

		#endregion .ctor

		#region Properties
		public List<IMessage> MessageList { get; private set; }

		public delegate IMessage GetNewMessage(ErrorMessageType messageType, Exception ex, string title, string message);

		public event GetNewMessage OnNewMessage;
		public bool HasCriticalError { get { return MessageList.Any(oItem => oItem.MessageType == ErrorMessageType.Critical); } }
		#endregion Properties

		#region Methods

		#region Public
		public void ClearAllMessages()
		{
			MessageList.Clear();
		}

		#region Critical Message
		public void AddCriticalMessage(Exception ex, string szTitle, string szMessage)
		{
			AddMessage(ErrorMessageType.Critical, ex, szTitle, szMessage);
		}
		public void AddCriticalMessage(string szTitle, string szMessage)
		{
			AddCriticalMessage(null, szTitle, szMessage);
		}
		public void AddCriticalMessage(string szMessage)
		{
			AddCriticalMessage(null, null, szMessage);
		}
		#endregion Critical Message
		#region Warning Message
		public void AddWarningMessage(Exception ex, string szTitle, string szMessage)
		{
			AddMessage(ErrorMessageType.Warning, ex, szTitle, szMessage);
		}
		public void AddWarningMessage(string szTitle, string szMessage)
		{
			AddWarningMessage(null, szTitle, szMessage);
		}
		public void AddWarningMessage(string szMessage)
		{
			AddWarningMessage(null, null, szMessage);
		}
		#endregion Warning Message
		#region Successful Message
		public void AddSuccessMessage(Exception ex, string szTitle, string szMessage)
		{
			AddMessage(ErrorMessageType.Success, ex, szTitle, szMessage);
		}
		public void AddSuccessMessage(string szTitle, string szMessage)
		{
			AddSuccessMessage(null, szTitle, szMessage);
		}
		public void AddSuccessMessage(string szMessage)
		{
			AddSuccessMessage(null, null, szMessage);
		}
		#endregion Successful Message
		#region Normal Message
		public void AddNormalMessage(Exception ex, string szTitle, string szMessage)
		{
			AddMessage(ErrorMessageType.Normal, ex, szTitle, szMessage);
		}
		public void AddNormalMessage(string szTitle, string szMessage)
		{
			AddNormalMessage(null, szTitle, szMessage);
		}
		public void AddNormalMessage(string szMessage)
		{
			AddNormalMessage(null, null, szMessage);
		}
		#endregion Normal Message

		#endregion Public

		#region Private
		private void AddMessage(ErrorMessageType messageType, Exception ex, string title, string messageString)
		{
			if (OnNewMessage != null)
			{
				MessageList.Add(OnNewMessage(messageType, ex, title, messageString));
				return;
			}

			// ** Init 
			var message = ex == null
				? messageString
				: string.Format("{0} | with exception message of:\r\n{1}", messageString, ex.Message);

			MessageList.Add(new Message
			{
				Title = title,
				DisplayMessage = message,
				MessageType = messageType,
				Ex = ex
			});
		}

		#endregion Private
		#endregion Methods
	}
}
