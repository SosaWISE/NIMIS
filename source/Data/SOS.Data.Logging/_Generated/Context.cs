


using System;
using SubSonic;
using SOS.Data;

namespace SOS.Data.Logging
{
	public partial class LoggingDataContext
	{
		#region Internal Instance

		private static LoggingDataContext _contextInstance;
		private static readonly object _syncRootContextInstance = new object();

		public static LoggingDataContext Instance
		{
			get
			{
				if (_contextInstance == null)
				{
					lock (_syncRootContextInstance)
					{
						if (_contextInstance == null)
						{
							_contextInstance = new LoggingDataContext();
						}
					}
				}
				return _contextInstance;
			}
		}

		#endregion // Internal Instance

		#region Controllers Properties

		LG_ChangeLogController _LG_ChangeLogs;
		public LG_ChangeLogController LG_ChangeLogs
		{
			get
			{
				if (_LG_ChangeLogs == null) _LG_ChangeLogs = new LG_ChangeLogController();
				return _LG_ChangeLogs;
			}
		}

		LG_ChangeLogTypeController _LG_ChangeLogTypes;
		public LG_ChangeLogTypeController LG_ChangeLogTypes
		{
			get
			{
				if (_LG_ChangeLogTypes == null) _LG_ChangeLogTypes = new LG_ChangeLogTypeController();
				return _LG_ChangeLogTypes;
			}
		}

		LG_ChangeController _LG_Changes;
		public LG_ChangeController LG_Changes
		{
			get
			{
				if (_LG_Changes == null) _LG_Changes = new LG_ChangeController();
				return _LG_Changes;
			}
		}

		LG_LogSourceController _LG_LogSources;
		public LG_LogSourceController LG_LogSources
		{
			get
			{
				if (_LG_LogSources == null) _LG_LogSources = new LG_LogSourceController();
				return _LG_LogSources;
			}
		}

		LG_MessageController _LG_Messages;
		public LG_MessageController LG_Messages
		{
			get
			{
				if (_LG_Messages == null) _LG_Messages = new LG_MessageController();
				return _LG_Messages;
			}
		}

		LG_MessageStackFrameController _LG_MessageStackFrames;
		public LG_MessageStackFrameController LG_MessageStackFrames
		{
			get
			{
				if (_LG_MessageStackFrames == null) _LG_MessageStackFrames = new LG_MessageStackFrameController();
				return _LG_MessageStackFrames;
			}
		}

		LG_MessageTypeController _LG_MessageTypes;
		public LG_MessageTypeController LG_MessageTypes
		{
			get
			{
				if (_LG_MessageTypes == null) _LG_MessageTypes = new LG_MessageTypeController();
				return _LG_MessageTypes;
			}
		}

		#endregion //Controllers Properties

		#region View Controllers Properties

		#endregion //View Controllers Properties
	}

	#region Controllers

	public class LG_ChangeLogController : BaseTableController<LG_ChangeLog, LG_ChangeLogCollection> { }
	public class LG_ChangeLogTypeController : BaseTableController<LG_ChangeLogType, LG_ChangeLogTypeCollection> { }
	public class LG_ChangeController : BaseTableController<LG_Change, LG_ChangeCollection> { }
	public class LG_LogSourceController : BaseTableController<LG_LogSource, LG_LogSourceCollection> { }
	public class LG_MessageController : BaseTableController<LG_Message, LG_MessageCollection> { }
	public class LG_MessageStackFrameController : BaseTableController<LG_MessageStackFrame, LG_MessageStackFrameCollection> { }
	public class LG_MessageTypeController : BaseTableController<LG_MessageType, LG_MessageTypeCollection> { }

	#endregion //Controllers

	#region View Controllers


	#endregion //View Controllers
}
