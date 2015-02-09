using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace SOS.Lib.Core.ErrorHandling
{
	public class SimpleErrorManager : BaseErrorManager
	{
		private readonly List<IErrorMessage> _errorMessages = new List<IErrorMessage>();

		public override ICollection<IErrorMessage> ErrorMessages
		{
			get { return _errorMessages; }
		}

		public override int MessageCount
		{
			get { return _errorMessages.Count; }
		}

		public override void ClearMessages()
		{
			_errorMessages.Clear();
		}

		public override void AddMessage(ErrorMessageType type, Exception ex, string szHeader, string szMessage, bool shouldPersist, string databaseName, string schemaName, string tableName, string primaryKey)
		{
			var oMessage = new SimpleErrorMessage { Type = type };
			oMessage.Header = szHeader;
			oMessage.Message = szMessage;
			oMessage.TargetDatabase = databaseName;
			oMessage.TargetSchema = schemaName;
			oMessage.TargetTable = tableName;
			oMessage.TargetPrimaryKey = primaryKey;
			oMessage.ShouldPersist = shouldPersist;
			oMessage.Source = (Source != LogSource.Default) ? Source : LogSource.Default;

			var oCurrentTrace = new StackTrace(true);
			MethodBase oCaller = null;
			foreach (StackFrame currFrame in oCurrentTrace.GetFrames())
			{
				oCaller = currFrame.GetMethod();
				// Make sure it wasn't a helper method in this class
				if (oCaller.ReflectedType != GetType())
					break;
			}
			oMessage.MethodCall = string.Format("{0}.{1}", oCaller.ReflectedType.FullName, oCaller.Name);
			
			if (ex != null)
			{
				var oTrace = new StackTrace(ex, true);
				foreach (StackFrame currFrame in oTrace.GetFrames())
				{
					var oFrame = new SimpleErrorStackFrame
					{
						FileName = currFrame.GetFileName(),
						LineNumber = currFrame.GetFileLineNumber(),
						ColumnNumber = currFrame.GetFileColumnNumber()
					};

					MethodBase oMethod = currFrame.GetMethod();
					oFrame.MethodName = oMethod.ReflectedType != null 
						? string.Format("{0}.{1}", oMethod.ReflectedType.FullName, oMethod.Name)
						: oMethod.Name;

					oMessage.StackFrames.Add(oFrame);
				}
			}

			_errorMessages.Add(oMessage);

			OnMessageAdded();
		}

		public override void PersistMessages()
		{
		}
	}
}