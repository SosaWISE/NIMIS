using System.Collections.Generic;
using System.Linq;
using SOS.Data.Logging.ControllerExtensions;
using SOS.Lib.Core;
using SOS.Lib.Core.ErrorHandling;

namespace SOS.Data.Logging
{
	public partial class LG_Message : IErrorMessage
	{
		#region Constants

		public const int MaxHeaderLength = 1024;
		public const int MaxMethodLength = 255;
		public const int MaxComputerNameLength = 100;
		public const int MaxSourceViewLength = 255;

		#endregion Constants

		#region Implementation of IErrorMessage

		public ErrorMessageType Type { get; set; }
		public string SourceUrl { get; set; }
		public int? AccountId { get; set; }
		public bool ShouldPersist { get; set; }
		public LogSource Source { get; set; }
		public ICollection<IErrorStackFrame> StackFrames
		{
			get
			{
				return MessageStackFrames.Cast<IErrorStackFrame>().ToList();
			}
		}

		#endregion Implementation of IErrorMessage

		#region Properties

		#region Public

		private LG_MessageStackFrameCollection _messageStackFrames;
		public LG_MessageStackFrameCollection MessageStackFrames
		{
			get
			{
				if (_messageStackFrames == null)
				{
					var ctxt = new LoggingDataContext();
					_messageStackFrames = ctxt.LG_MessageStackFrames.LoadByMessageID(MessageID);
				}
				return _messageStackFrames;
			}
		}

		#endregion Public

		#endregion Properties
	}
}
