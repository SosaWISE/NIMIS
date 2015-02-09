using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using SOS.Lib.Core;
using SOS.Lib.Core.ErrorHandling;
using SOS.Lib.Util;

namespace SOS.Data.Logging
{
	public class DBErrorManager : BaseErrorManager
	{
		#region .ctor

		public DBErrorManager()
		{
		}

		private string UserID { get; set; }

		public DBErrorManager(LogSource source, string szUserID) : base(source)
		{
			/** Check that szUserID is not null. */
			if (string.IsNullOrWhiteSpace(szUserID))
				szUserID = "10000000-1000-1000-1000-100000000001";
			UserID = szUserID;
		}

		#endregion .ctor

		#region Memeber Properties

		#region Private

		private readonly LG_MessageCollection _errorMessages = new LG_MessageCollection();

		#endregion Private

		#region Public

		public static DBErrorManager Instance { get; private set; }

		public override ICollection<IErrorMessage> ErrorMessages
		{
			get
			{
				return _errorMessages.Cast<IErrorMessage>().ToList();
			}
		}

		public override int MessageCount
		{
			get { return _errorMessages.Count; }
		}

		#endregion Public

		#endregion Memeber Properties

		#region Member Functions

		public override void ClearMessages()
		{
			_errorMessages.Clear();
		}

		public override void AddMessage(ErrorMessageType oType, Exception ex, string szHeader, string szMessage, bool shouldPersist, string databaseName, string schemaName, string tableName, string primaryKey)
		{
			var oMessage = new LG_Message { MessageTypeId = (int)oType, Type = oType };
			if (!string.IsNullOrEmpty(szHeader) && szHeader.Length > LG_Message.MaxHeaderLength)
				oMessage.Header = szHeader.Substring(0, LG_Message.MaxHeaderLength);
			else
				oMessage.Header = szHeader;
			oMessage.Message = szMessage;
			oMessage.ComputerName = Environment.MachineName;
			oMessage.TargetDatabase = databaseName;
			oMessage.TargetSchema = schemaName;
			oMessage.TargetTable = tableName;
			oMessage.TargetPrimaryKey = primaryKey;
			oMessage.ShouldPersist = shouldPersist;
			oMessage.CreatedOn = DateTime.Now;
			oMessage.Source = (Source != LogSource.Default) ? Source : SubSonicConfigHelper.GetLogSourceFromConfig();
			oMessage.LogSourceID = (int) oMessage.Source;

			var oCurrentTrace = new StackTrace(true);
			StackFrame oCurrentFrame = null;
			MethodBase oCaller = null;
			foreach (StackFrame currFrame in oCurrentTrace.GetFrames())
			{
				oCurrentFrame = currFrame;
				oCaller = currFrame.GetMethod();
				// Make sure it wasn't a helper method in this class
				if (oCaller.ReflectedType != GetType())
					break;
			}
			oMessage.MethodCall = string.Format("{0}.{1}", oCaller.ReflectedType.FullName, oCaller.Name);
			if (!string.IsNullOrEmpty(oMessage.MethodCall) && oMessage.MethodCall.Length > LG_Message.MaxMethodLength)
				oMessage.MethodCall = oMessage.MethodCall.Substring(0, LG_Message.MaxMethodLength);

			#region For Windows Clients
			oMessage.SourceView = oCurrentFrame.GetFileName();
			if (!string.IsNullOrEmpty(oMessage.SourceView) &&
				oMessage.SourceView.Length > LG_Message.MaxSourceViewLength)
				oMessage.SourceView = oMessage.SourceView.Substring(0, LG_Message.MaxSourceViewLength);

			/*
				* TODO:  We need to make sure that this is pulling the logged in user not the machine user.
				*/
			// Get the Guid of the currently logged in user
			oMessage.CreatedBy = UserID;
			oMessage.Save(UserID);

			#endregion For Windows Clients

			if (!string.IsNullOrEmpty(oMessage.ComputerName) &&
				oMessage.ComputerName.Length > LG_Message.MaxComputerNameLength)
				oMessage.ComputerName = oMessage.ComputerName.Substring(0, LG_Message.MaxComputerNameLength);

			if (ex != null)
			{
				//var oTrace = new StackTrace(ex, true);
				//foreach (StackFrame currFrame in oTrace.GetFrames())
				//{
				//	var oFrame = new LG_MessageStackFrame();
				//	oFrame.MessageId = oMessage.MessageID;
				//	oFrame.FileName = currFrame.GetFileName();
				//	if (!string.IsNullOrEmpty(oFrame.FileName) &&
				//		oFrame.FileName.Length > LG_MessageStackFrame.MaxFileNameLength)
				//		oFrame.FileName = oFrame.FileName.Substring(0, LG_MessageStackFrame.MaxFileNameLength);
				//	oFrame.LineNumber = currFrame.GetFileLineNumber();
				//	oFrame.ColumnNumber = currFrame.GetFileColumnNumber();

				//	MethodBase oMethod = currFrame.GetMethod();
				//	if (oMethod.ReflectedType != null)
				//	{
				//		oFrame.Method = string.Format("{0}.{1}", oMethod.ReflectedType.FullName, oMethod.Name);
				//	}
				//	else
				//	{
				//		//for lambda expressions the ReflectedType is null
				//		oFrame.Method = oMethod.Name;
				//	}
				//	if (!string.IsNullOrEmpty(oFrame.Method) &&
				//		oFrame.Method.Length > LG_MessageStackFrame.MaxMethodLength)
				//		oFrame.Method = oFrame.Method.Substring(0, LG_MessageStackFrame.MaxMethodLength);

				//	oFrame.Save(UserID);
				//	oMessage.MessageStackFrames.Add(oFrame);
				//}
				SaveStack(ex, oMessage);
			}

			_errorMessages.Add(oMessage);

			OnMessageAdded();
		}

		private void SaveStack(Exception ex, LG_Message message)
		{
			var oTrace = new StackTrace(ex, true);
			foreach (StackFrame currFrame in oTrace.GetFrames())
			{
				var oFrame = new LG_MessageStackFrame();
				oFrame.MessageId = message.MessageID;
				oFrame.FileName = currFrame.GetFileName();
				if (!string.IsNullOrEmpty(oFrame.FileName) &&
					oFrame.FileName.Length > LG_MessageStackFrame.MaxFileNameLength)
					oFrame.FileName = oFrame.FileName.Substring(0, LG_MessageStackFrame.MaxFileNameLength);
				oFrame.LineNumber = currFrame.GetFileLineNumber();
				oFrame.ColumnNumber = currFrame.GetFileColumnNumber();

				MethodBase oMethod = currFrame.GetMethod();
				if (oMethod.ReflectedType != null)
				{
					oFrame.Method = string.Format("{0}.{1}", oMethod.ReflectedType.FullName, oMethod.Name);
				}
				else
				{
					//for lambda expressions the ReflectedType is null
					oFrame.Method = oMethod.Name;
				}
				if (!string.IsNullOrEmpty(oFrame.Method) &&
					oFrame.Method.Length > LG_MessageStackFrame.MaxMethodLength)
					oFrame.Method = oFrame.Method.Substring(0, LG_MessageStackFrame.MaxMethodLength);

				oFrame.Save(UserID);
				message.MessageStackFrames.Add(oFrame);
			}
		}

		public void AddSqlExceptionMessage(SqlException sqlEx, string szHeader, string szMessage, bool shouldPersist = false)
		{
			// ** Initialize 
			var oMessage = new LG_Message { MessageTypeId = (int)ErrorMessageType.Critical, Type = ErrorMessageType.Critical };
			var sqlParse = MsSqlExceptionUtil.Parse(sqlEx.Message, sqlEx);

			if (!string.IsNullOrEmpty(szHeader) && szHeader.Length > LG_Message.MaxHeaderLength)
				oMessage.Header = szHeader.Substring(0, LG_Message.MaxHeaderLength);
			else
				oMessage.Header = szHeader;
			oMessage.Message = szMessage;
			oMessage.ComputerName = Environment.MachineName;
			oMessage.TargetDatabaseServer = sqlParse.ServerName;
			oMessage.TargetDatabase = sqlParse.DatabaseName;
			oMessage.TargetSchema = sqlParse.SchemaName;
			oMessage.TargetTable = sqlParse.TableName;
			oMessage.TargetPrimaryKey = sqlParse.PrimaryKey;
			oMessage.ShouldPersist = shouldPersist;
			oMessage.CreatedOn = DateTime.Now;
			oMessage.Source = (Source != LogSource.Default) ? Source : SubSonicConfigHelper.GetLogSourceFromConfig();
			oMessage.LogSourceID = (int)oMessage.Source;

			var oCurrentTrace = new StackTrace(true);
			StackFrame oCurrentFrame = null;
			MethodBase oCaller = null;
			foreach (StackFrame currFrame in oCurrentTrace.GetFrames())
			{
				oCurrentFrame = currFrame;
				oCaller = currFrame.GetMethod();
				// Make sure it wasn't a helper method in this class
				if (oCaller.ReflectedType != GetType())
					break;
			}
			oMessage.MethodCall = string.Format("{0}.{1}", oCaller.ReflectedType.FullName, oCaller.Name);
			if (!string.IsNullOrEmpty(oMessage.MethodCall) && oMessage.MethodCall.Length > LG_Message.MaxMethodLength)
				oMessage.MethodCall = oMessage.MethodCall.Substring(0, LG_Message.MaxMethodLength);

			#region For Windows Clients
			oMessage.SourceView = oCurrentFrame.GetFileName();
			if (!string.IsNullOrEmpty(oMessage.SourceView) &&
				oMessage.SourceView.Length > LG_Message.MaxSourceViewLength)
				oMessage.SourceView = oMessage.SourceView.Substring(0, LG_Message.MaxSourceViewLength);

			/*
				* TODO:  We need to make sure that this is pulling the logged in user not the machine user.
				*/
			// Get the Guid of the currently logged in user
			oMessage.CreatedBy = UserID;
			oMessage.Save(UserID);

			#endregion For Windows Clients

			if (!string.IsNullOrEmpty(oMessage.ComputerName) &&
				oMessage.ComputerName.Length > LG_Message.MaxComputerNameLength)
				oMessage.ComputerName = oMessage.ComputerName.Substring(0, LG_Message.MaxComputerNameLength);

		}

		public override void PersistMessages()
		{
			// Save all the messages
			foreach (LG_Message curr in _errorMessages)
			{
				if (curr.ShouldPersist)
				{
					curr.Save();
					// Save associated stack frames
					foreach (LG_MessageStackFrame currFrame in curr.MessageStackFrames)
					{
						if (currFrame.MessageStackFrameID == 0)
						{
							currFrame.MessageId = curr.MessageID;
							currFrame.Save();
						}
					}
				}
			}
		}

		public static void SetSingletonInstance(DBErrorManager oInstance)
		{
			Instance = oInstance;
		}

		#endregion Member Functions
	}
}
