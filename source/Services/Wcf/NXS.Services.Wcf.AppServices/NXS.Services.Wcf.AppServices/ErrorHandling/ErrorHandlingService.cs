using System;
using System.Collections.Generic;
using System.Configuration;
using NXS.Services.Wcf.AppServices.ErrorHandling.Models;
using SOS.Data.Logging;
using ConfigurationSettings = SOS.Lib.Util.Configuration.ConfigurationSettings;

namespace NXS.Services.Wcf.AppServices.ErrorHandling
{
	public class ErrorHandlingService : IErrorHandlingService
	{
		#region .ctor
		public ErrorHandlingService()
		{
			if (!IsSetup) SetupSystem();
		}

		#endregion .ctor

		#region Fields

		private static readonly object _syncSetup = new object();
		private static object _setup;
		private static bool _isSetup;
		public static bool IsSetup
		{
			get
			{
				if (_setup == null)
					lock (_syncSetup)
					{
						if (_setup == null)
						{
							// Initialize connection strings
							ConfigurationSettings.Current.SetProperties("Preferences", ConfigurationManager.AppSettings["Environment"]);
							SOS.Data.SubSonicConfigHelper.SetupConnectionStrings();

							_setup = new object();
							_isSetup = true;
						}
					}
				// Return value
				return _isSetup;
			}
		}


		#endregion Fields

		#region Methods
		private static void SetupSystem()
		{
			ConfigurationSettings.Current.SetProperties("Preferences", ConfigurationManager.AppSettings["Environment"]);
			Console.WriteLine("Hopefully this is never called.");
		}

		#endregion Methods

		public void PersistMessages(List<ErrorMessage> messages, int logSourceID)
		{
			foreach (ErrorMessage currMessage in messages)
			{
				// Save each message record
				var dbMessage = new LG_Message
				{
					MessageTypeId = currMessage.ErrorMessageTypeID,
					LogSourceID = logSourceID,
					Header = currMessage.Header,
					Message = currMessage.Message,
					TargetDatabase = currMessage.TargetDatabase,
					TargetSchema = currMessage.TargetSchema,
					TargetTable = currMessage.TargetTable,
					TargetPrimaryKey = currMessage.TargetPrimaryKey.ToString(),
					MethodCall = currMessage.MethodCall,
					ComputerName = currMessage.ComputerName,
					SourceView = currMessage.SourceUrl,
					CreatedBy = currMessage.CreatedById,
					CreatedOn = DateTime.Now
				};
				dbMessage.Save();

				// Save each of the stack frames
				if (currMessage.MessageStackFrames != null && currMessage.MessageStackFrames.Count > 0)
				{
					foreach (MessageStackFrame currFrame in currMessage.MessageStackFrames)
					{
						var dbFrame = new LG_MessageStackFrame
						{
							MessageId = dbMessage.MessageID,
							Method = currFrame.MethodName,
							FileName = currFrame.FileName,
							LineNumber = currFrame.LineNumber,
							ColumnNumber = currFrame.ColumnNumber
						};
						dbFrame.Save();
					}
				}
			}
		}

	}

}
