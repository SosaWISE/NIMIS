using System;
using System.Collections.Generic;

namespace SOS.Lib.Core.ErrorHandling
{
	public abstract class BaseErrorManager : IErrorManager
	{
		#region Event Declarations

		#region Delegates

		public delegate void DisplayMessagesHandler();

		public delegate void MessageAddedHandler();

		#endregion

		public event MessageAddedHandler MessageAdded;

		public event DisplayMessagesHandler DisplayMessages;

		#endregion Event Declarations

		#region Constants

		public const string LogSourceKey = "LogSourceID";

		#endregion Constants

		#region Properties

		#region Private

		#endregion Private

		#region Public

		public abstract ICollection<IErrorMessage> ErrorMessages { get; }
		public abstract int MessageCount { get; }
		public abstract void ClearMessages();

		public virtual Dictionary<ErrorMessageType, List<IErrorMessage>> GetErrorMessageDictionary()
		{
			var result = new Dictionary<ErrorMessageType, List<IErrorMessage>>();

			List<IErrorMessage> tempList;
			foreach (IErrorMessage item in ErrorMessages)
			{
				if (!result.TryGetValue(item.Type, out tempList))
				{
					tempList = new List<IErrorMessage>();
					result.Add(item.Type, tempList);
				}
				tempList.Add(item);
			}
			return result;
		}

		public LogSource Source { get; set; }

		#endregion Public

		#endregion Properties

		#region Constructors

		protected BaseErrorManager() : this(LogSource.Default)
		{
		}

		protected BaseErrorManager(LogSource source)
		{
			Source = source;
		}

		#endregion Constructors

		#region Methods

		#region Public

		/// <summary>
		/// Adds a warning message - will be persisted.
		/// </summary>
		/// <param name="szHeader">The message header.</param>
		/// <param name="szMessage">The message text.</param>
		public void AddWarningMessage(string szHeader, string szMessage)
		{
			AddMessage(ErrorMessageType.Warning, null, szHeader, szMessage, null, null, null, null);
		}

		/// <summary>
		/// Adds a warning message - will be persisted.
		/// </summary>
		/// <param name="szHeader">The message header.</param>
		/// <param name="szMessage">The message text.</param>
		/// <param name="databaseName">The name of the database that contains the associated table.</param>
		/// <param name="schemaName">The name of the schema for the associated table.</param>
		/// <param name="tableName">The associated table for the warning message.</param>
		/// <param name="primaryKey">The primary key value of the row in the associated table for the warning message.</param>
		public void AddWarningMessage(string szHeader, string szMessage, string databaseName, string schemaName,
									  string tableName, string primaryKey)
		{
			AddMessage(ErrorMessageType.Warning, null, szHeader, szMessage, databaseName, schemaName, tableName,
					   primaryKey);
		}

		/// <summary>
		/// Adds a warning message.
		/// </summary>
		/// <param name="szHeader">The message header.</param>
		/// <param name="szMessage">The message text.</param>
		/// <param name="shouldPersist">Whether to persist the message when the PersistMessages method is called.</param>
		public void AddWarningMessage(string szHeader, string szMessage, bool shouldPersist)
		{
			AddMessage(ErrorMessageType.Warning, null, szHeader, szMessage, shouldPersist, null, null, null, null);
		}

		/// <summary>
		/// Adds a warning message.
		/// </summary>
		/// <param name="szHeader">The message header.</param>
		/// <param name="szMessage">The message text.</param>
		/// <param name="shouldPersist">Whether to persist the message when the PersistMessages method is called.</param>
		/// <param name="databaseName">The name of the database that contains the associated table.</param>
		/// <param name="schemaName">The name of the schema for the associated table.</param>
		/// <param name="tableName">The associated table for the warning message.</param>
		/// <param name="primaryKey">The primary key value of the row in the associated table for the warning message.</param>
		public void AddWarningMessage(string szHeader, string szMessage, bool shouldPersist, string databaseName,
									  string schemaName, string tableName, string primaryKey)
		{
			AddMessage(ErrorMessageType.Warning, null, szHeader, szMessage, shouldPersist, databaseName, schemaName,
					   tableName, primaryKey);
		}

		/// <summary>
		/// Adds a success message - will NOT be persisted.
		/// </summary>
		/// <param name="szHeader">The message header.</param>
		/// <param name="szMessage">The message text.</param>
		public void AddSuccessMessage(string szHeader, string szMessage)
		{
			AddMessage(ErrorMessageType.Success, null, szHeader, szMessage, null, null, null, null);
		}

		/// <summary>
		/// Adds a success message - will NOT be persisted.
		/// </summary>
		/// <param name="szHeader">The message header.</param>
		/// <param name="szMessage">The message text.</param>
		/// <param name="databaseName">The name of the database that contains the associated table.</param>
		/// <param name="schemaName">The name of the schema for the associated table.</param>
		/// <param name="tableName">The associated table for the warning message.</param>
		/// <param name="primaryKey">The primary key value of the row in the associated table for the warning message.</param>
		public void AddSuccessMessage(string szHeader, string szMessage, string databaseName, string schemaName,
									  string tableName, string primaryKey)
		{
			AddMessage(ErrorMessageType.Success, null, szHeader, szMessage, databaseName, schemaName, tableName,
					   primaryKey);
		}

		/// <summary>
		/// Adds a success message.
		/// </summary>
		/// <param name="szHeader">The message header.</param>
		/// <param name="szMessage">The message text.</param>
		/// <param name="shouldPersist">Whether to persist the message when the PersistMessages method is called.</param>
		public void AddSuccessMessage(string szHeader, string szMessage, bool shouldPersist)
		{
			AddMessage(ErrorMessageType.Success, null, szHeader, szMessage, shouldPersist, null, null, null, null);
		}

		/// <summary>
		/// Adds a success message.
		/// </summary>
		/// <param name="szHeader">The message header.</param>
		/// <param name="szMessage">The message text.</param>
		/// <param name="shouldPersist">Whether to persist the message when the PersistMessages method is called.</param>
		/// <param name="databaseName">The name of the database that contains the associated table.</param>
		/// <param name="schemaName">The name of the schema for the associated table.</param>
		/// <param name="tableName">The associated table for the warning message.</param>
		/// <param name="primaryKey">The primary key value of the row in the associated table for the warning message.</param>
		public void AddSuccessMessage(string szHeader, string szMessage, bool shouldPersist, string databaseName,
									  string schemaName, string tableName, string primaryKey)
		{
			AddMessage(ErrorMessageType.Success, null, szHeader, szMessage, shouldPersist, databaseName, schemaName,
					   tableName, primaryKey);
		}

		/// <summary>
		/// Adds a critical message - will be persisted.
		/// </summary>
		/// <param name="szHeader">The message header.</param>
		/// <param name="szMessage">The message text.</param>
		public void AddCriticalMessage(string szHeader, string szMessage)
		{
			AddMessage(ErrorMessageType.Critical, null, szHeader, szMessage, null, null, null, null);
		}

		/// <summary>
		/// Adds a critical message - will be persisted.
		/// </summary>
		/// <param name="szHeader">The message header.</param>
		/// <param name="szMessage">The message text.</param>
		/// <param name="databaseName">The name of the database that contains the associated table.</param>
		/// <param name="schemaName">The name of the schema for the associated table.</param>
		/// <param name="tableName">The associated table for the warning message.</param>
		/// <param name="primaryKey">The primary key value of the row in the associated table for the warning message.</param>
		public void AddCriticalMessage(string szHeader, string szMessage, string databaseName, string schemaName,
									   string tableName, string primaryKey)
		{
			AddMessage(ErrorMessageType.Critical, null, szHeader, szMessage, databaseName, schemaName, tableName,
					   primaryKey);
		}

		/// <summary>
		/// Adds a critical message.
		/// </summary>
		/// <param name="szHeader">The message header.</param>
		/// <param name="szMessage">The message text.</param>
		/// <param name="shouldPersist">Whether to persist the message when the PersistMessages method is called.</param>
		public void AddCriticalMessage(string szHeader, string szMessage, bool shouldPersist)
		{
			AddMessage(ErrorMessageType.Critical, null, szHeader, szMessage, shouldPersist, null, null, null, null);
		}

		/// <summary>
		/// Adds a critical message.
		/// </summary>
		/// <param name="szHeader">The message header.</param>
		/// <param name="szMessage">The message text.</param>
		/// <param name="shouldPersist">Whether to persist the message when the PersistMessages method is called.</param>
		/// <param name="databaseName">The name of the database that contains the associated table.</param>
		/// <param name="schemaName">The name of the schema for the associated table.</param>
		/// <param name="tableName">The associated table for the warning message.</param>
		/// <param name="primaryKey">The primary key value of the row in the associated table for the warning message.</param>
		public void AddCriticalMessage(string szHeader, string szMessage, bool shouldPersist, string databaseName,
									   string schemaName, string tableName, string primaryKey)
		{
			AddMessage(ErrorMessageType.Critical, null, szHeader, szMessage, shouldPersist, databaseName, schemaName,
					   tableName, primaryKey);
		}

		/// <summary>
		/// Adds a critical message - will be persisted.
		/// </summary>
		/// <param name="ex">The exception that forms the basis for the message.</param>
		public void AddCriticalMessage(Exception ex)
		{

			AddMessage(ErrorMessageType.Critical, ex, ex.Message, ex.StackTrace, null, null, null, null);
		}

		/// <summary>
		/// Adds a critical message - will be persisted.
		/// </summary>
		/// <param name="ex">The exception that forms the basis for the message.</param>
		/// <param name="databaseName">The name of the database that contains the associated table.</param>
		/// <param name="schemaName">The name of the schema for the associated table.</param>
		/// <param name="tableName">The associated table for the warning message.</param>
		/// <param name="primaryKey">The primary key value of the row in the associated table for the warning message.</param>
		public void AddCriticalMessage(Exception ex, string databaseName, string schemaName, string tableName,
									   string primaryKey)
		{
			AddMessage(ErrorMessageType.Critical, ex, ex.Message, ex.StackTrace, databaseName, schemaName, tableName,
					   primaryKey);
		}

		/// <summary>
		/// Adds a critical message - will be persisted.
		/// </summary>
		/// <param name="ex">The exception that forms the basis for the message.</param>
		/// <param name="szHeader">The message header.</param>
		/// <param name="szMessage">The message text.</param>
		public void AddCriticalMessage(Exception ex, string szHeader, string szMessage)
		{
			AddMessage(ErrorMessageType.Critical, ex, szHeader, szMessage, null, null, null, null);
		}

		/// <summary>
		/// Adds a critical message.
		/// </summary>
		/// <param name="ex">The exception that forms the basis for the message.</param>
		/// <param name="szHeader">The message header.</param>
		/// <param name="szMessage">The message text.</param>
		/// <param name="shouldPersist">Whether to persist the message when the PersistMessages method is called.</param>
		public void AddCriticalMessage(Exception ex, string szHeader, string szMessage, bool shouldPersist)
		{
			AddMessage(ErrorMessageType.Critical, ex, szHeader, szMessage, shouldPersist, null, null, null, null);
		}

		/// <summary>
		/// Adds a critical message - will be persisted.
		/// </summary>
		/// <param name="ex">The exception that forms the basis for the message.</param>
		/// <param name="szHeader">The message header.</param>
		/// <param name="szMessage">The message text.</param>
		/// <param name="databaseName">The name of the database that contains the associated table.</param>
		/// <param name="schemaName">The name of the schema for the associated table.</param>
		/// <param name="tableName">The associated table for the warning message.</param>
		/// <param name="primaryKey">The primary key value of the row in the associated table for the warning message.</param>
		public void AddCriticalMessage(Exception ex, string szHeader, string szMessage, string databaseName,
									   string schemaName, string tableName, string primaryKey)
		{
			AddMessage(ErrorMessageType.Critical, ex, szHeader, szMessage, databaseName, schemaName, tableName,
					   primaryKey);
		}

		/// <summary>
		/// Adds a licensing message.
		/// </summary>
		/// <param name="szHeader">The message header.</param>
		/// <param name="szMessage">The message text.</param>
		/// <param name="shouldPersist">Wheter to persist the message when the PersitMessages method is called.</param>
		public void AddLicensingMessage(string szHeader, string szMessage, bool shouldPersist)
		{
			AddMessage(ErrorMessageType.Licensing, null, szHeader, szMessage, shouldPersist, null, null, null, null);
		}

		/// <summary>
		/// Adds a message.
		/// </summary>
		/// <param name="type">The message type.</param>
		/// <param name="szHeader">The message header.</param>
		/// <param name="szMessage">The message text.</param>
		public void AddMessage(ErrorMessageType type, string szHeader, string szMessage)
		{
			AddMessage(type, null, szHeader, szMessage, null, null, null, null);
		}

		/// <summary>
		/// Adds a message.
		/// </summary>
		/// <param name="type">The type of the message.</param>
		/// <param name="szHeader">The message header.</param>
		/// <param name="szMessage">The message text.</param>
		/// <param name="databaseName">The name of the database that contains the associated table.</param>
		/// <param name="schemaName">The name of the schema for the associated table.</param>
		/// <param name="tableName">The associated table for the warning message.</param>
		/// <param name="primaryKey">The primary key value of the row in the associated table for the warning message.</param>
		public void AddMessage(ErrorMessageType type, string szHeader, string szMessage, string databaseName,
							   string schemaName, string tableName, string primaryKey)
		{
			AddMessage(type, null, szHeader, szMessage, databaseName, schemaName, tableName, primaryKey);
		}

		/// <summary>
		/// Adds a message.
		/// </summary>
		/// <param name="type">Tye type of the message.</param>
		/// <param name="oEx">The exception that forms the basis for the message.</param>
		/// <param name="szHeader">The message header.</param>
		/// <param name="szMessage">The message text.</param>
		public void AddMessage(ErrorMessageType type, Exception oEx, string szHeader, string szMessage)
		{
			AddMessage(type, oEx, szHeader, szMessage, null, null, null, null);
		}

		/// <summary>
		/// Adds a message.
		/// </summary>
		/// <param name="type">Tye type of the message.</param>
		/// <param name="oEx">The exception that forms the basis for the message.</param>
		/// <param name="szHeader">The message header.</param>
		/// <param name="szMessage">The message text.</param>
		/// <param name="databaseName">The name of the database that contains the associated table.</param>
		/// <param name="schemaName">The name of the schema for the associated table.</param>
		/// <param name="tableName">The associated table for the warning message.</param>
		/// <param name="primaryKey">The primary key value of the row in the associated table for the warning message.</param>
		public void AddMessage(ErrorMessageType type, Exception oEx, string szHeader, string szMessage,
							   string databaseName, string schemaName, string tableName, string primaryKey)
		{
			bool bPersist = true;
			if (type == ErrorMessageType.Success)
				bPersist = false;

			AddMessage(type, oEx, szHeader, szMessage, bPersist, databaseName, schemaName, tableName, primaryKey);
		}

		public void OnDisplayMessages()
		{
			if (DisplayMessages != null)
				DisplayMessages.Invoke();
		}

		#endregion Public

		/// <summary>
		/// Adds a message.
		/// </summary>
		/// <param name="type">Tye type of the message.</param>
		/// <param name="oEx">The exception that forms the basis for the message.</param>
		/// <param name="szHeader">The message header.</param>
		/// <param name="szMessage">The message text.</param>
		/// <param name="shouldPersist">Whether to persist the message when the PersistMessages method is called.</param>
		/// <param name="databaseName">The name of the database that contains the associated table.</param>
		/// <param name="schemaName">The name of the schema for the associated table.</param>
		/// <param name="tableName">The associated table for the warning message.</param>
		/// <param name="primaryKey">The primary key value of the row in the associated table for the warning message.</param>
		public abstract void AddMessage(ErrorMessageType type, Exception oEx, string szHeader, string szMessage,
										bool shouldPersist, string databaseName, string schemaName, string tableName,
										string primaryKey);

		public abstract void PersistMessages();

		protected void OnMessageAdded()
		{
			if (MessageAdded != null)
				MessageAdded.Invoke();
		}

		#endregion Methods
	}
}