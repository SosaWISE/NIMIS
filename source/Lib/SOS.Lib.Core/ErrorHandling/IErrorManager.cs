using System;
using System.Collections.Generic;

namespace SOS.Lib.Core.ErrorHandling
{
	public interface IErrorManager
	{
		/// <summary>
		/// Gets a collection of error messages that have been added.
		/// </summary>
		ICollection<IErrorMessage> ErrorMessages { get; }

		/// <summary>
		/// Gets a dictionary of error messages
		/// </summary>
		Dictionary<ErrorMessageType, List<IErrorMessage>> GetErrorMessageDictionary();

		/// <summary>
		/// Gets the number of messages that have been added.
		/// </summary>
		int MessageCount { get; }

		/// <summary>
		/// Gets or sets the source of the messages that are being logged.
		/// </summary>
		LogSource Source { get; set; }

		/// <summary>
		/// Clears Messages
		/// </summary>
		void ClearMessages();

		/// <summary>
		/// Adds a warning message - will be persisted.
		/// </summary>
		/// <param name="szHeader">The message header.</param>
		/// <param name="szMessage">The message text.</param>
		void AddWarningMessage(string szHeader, string szMessage);

		/// <summary>
		/// Adds a warning message - will be persisted.
		/// </summary>
		/// <param name="szHeader">The message header.</param>
		/// <param name="szMessage">The message text.</param>
		/// <param name="databaseName">The name of the database that contains the associated table.</param>
		/// <param name="schemaName">The name of the schema for the associated table.</param>
		/// <param name="tableName">The associated table for the warning message.</param>
		/// <param name="primaryKey">The primary key value of the row in the associated table for the warning message.</param>
		void AddWarningMessage(string szHeader, string szMessage, string databaseName, string schemaName,
							   string tableName, string primaryKey);

		/// <summary>
		/// Adds a warning message.
		/// </summary>
		/// <param name="szHeader">The message header.</param>
		/// <param name="szMessage">The message text.</param>
		/// <param name="shouldPersist">Whether to persist the message when the PersistMessages method is called.</param>
		void AddWarningMessage(string szHeader, string szMessage, bool shouldPersist);

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
		void AddWarningMessage(string szHeader, string szMessage, bool shouldPersist, string databaseName,
							   string schemaName,
							   string tableName, string primaryKey);

		/// <summary>
		/// Adds a success message - will NOT be persisted.
		/// </summary>
		/// <param name="szHeader">The message header.</param>
		/// <param name="szMessage">The message text.</param>
		void AddSuccessMessage(string szHeader, string szMessage);

		/// <summary>
		/// Adds a success message - will NOT be persisted.
		/// </summary>
		/// <param name="szHeader">The message header.</param>
		/// <param name="szMessage">The message text.</param>
		/// <param name="databaseName">The name of the database that contains the associated table.</param>
		/// <param name="schemaName">The name of the schema for the associated table.</param>
		/// <param name="tableName">The associated table for the warning message.</param>
		/// <param name="primaryKey">The primary key value of the row in the associated table for the warning message.</param>
		void AddSuccessMessage(string szHeader, string szMessage, string databaseName, string schemaName,
							   string tableName, string primaryKey);

		/// <summary>
		/// Adds a success message.
		/// </summary>
		/// <param name="szHeader">The message header.</param>
		/// <param name="szMessage">The message text.</param>
		/// <param name="shouldPersist">Whether to persist the message when the PersistMessages method is called.</param>
		void AddSuccessMessage(string szHeader, string szMessage, bool shouldPersist);

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
		void AddSuccessMessage(string szHeader, string szMessage, bool shouldPersist, string databaseName,
							   string schemaName,
							   string tableName, string primaryKey);

		/// <summary>
		/// Adds a critical message - will be persisted.
		/// </summary>
		/// <param name="szHeader">The message header.</param>
		/// <param name="szMessage">The message text.</param>
		void AddCriticalMessage(string szHeader, string szMessage);

		/// <summary>
		/// Adds a critical message - will be persisted.
		/// </summary>
		/// <param name="szHeader">The message header.</param>
		/// <param name="szMessage">The message text.</param>
		/// <param name="databaseName">The name of the database that contains the associated table.</param>
		/// <param name="schemaName">The name of the schema for the associated table.</param>
		/// <param name="tableName">The associated table for the warning message.</param>
		/// <param name="primaryKey">The primary key value of the row in the associated table for the warning message.</param>
		void AddCriticalMessage(string szHeader, string szMessage, string databaseName, string schemaName,
								string tableName, string primaryKey);

		/// <summary>
		/// Adds a critical message.
		/// </summary>
		/// <param name="szHeader">The message header.</param>
		/// <param name="szMessage">The message text.</param>
		/// <param name="shouldPersist">Whether to persist the message when the PersistMessages method is called.</param>
		void AddCriticalMessage(string szHeader, string szMessage, bool shouldPersist);

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
		void AddCriticalMessage(string szHeader, string szMessage, bool shouldPersist, string databaseName,
								string schemaName,
								string tableName, string primaryKey);

		/// <summary>
		/// Adds a critical message - will be persisted.
		/// </summary>
		/// <param name="ex">The exception that forms the basis for the message.</param>
		void AddCriticalMessage(Exception ex);

		/// <summary>
		/// Adds a critical message - will be persisted.
		/// </summary>
		/// <param name="ex">The exception that forms the basis for the message.</param>
		/// <param name="databaseName">The name of the database that contains the associated table.</param>
		/// <param name="schemaName">The name of the schema for the associated table.</param>
		/// <param name="tableName">The associated table for the warning message.</param>
		/// <param name="primaryKey">The primary key value of the row in the associated table for the warning message.</param>
		void AddCriticalMessage(Exception ex, string databaseName, string schemaName,
								string tableName, string primaryKey);

		/// <summary>
		/// Adds a critical message - will be persisted.
		/// </summary>
		/// <param name="ex">The exception that forms the basis for the message.</param>
		/// <param name="szHeader">The message header.</param>
		/// <param name="szMessage">The message text.</param>
		void AddCriticalMessage(Exception ex, string szHeader, string szMessage);

		/// <summary>
		/// Adds a critical message.
		/// </summary>
		/// <param name="ex">The exception that forms the basis for the message.</param>
		/// <param name="szHeader">The message header.</param>
		/// <param name="szMessage">The message text.</param>
		/// <param name="shouldPersist">Whether to persist the message when the PersistMessages method is called.</param>
		void AddCriticalMessage(Exception ex, string szHeader, string szMessage, bool shouldPersist);

		/// <summary>
		/// Adds a licensing message.
		/// </summary>
		/// <param name="szHeader">The message header.</param>
		/// <param name="szMessage">The message text.</param>
		/// <param name="shouldPersist">Wheter to persist the message when the PersitMessages method is called.</param>
		void AddLicensingMessage(string szHeader, string szMessage, bool shouldPersist);

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
		void AddCriticalMessage(Exception ex, string szHeader, string szMessage, string databaseName, string schemaName,
								string tableName, string primaryKey);

		/// <summary>
		/// Adds a message.
		/// </summary>
		/// <param name="type">The message type.</param>
		/// <param name="szHeader">The message header.</param>
		/// <param name="szMessage">The message text.</param>
		void AddMessage(ErrorMessageType type, string szHeader, string szMessage);

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
		void AddMessage(ErrorMessageType type, string szHeader, string szMessage, string databaseName, string schemaName,
						string tableName, string primaryKey);

		/// <summary>
		/// Adds a message.
		/// </summary>
		/// <param name="type">Tye type of the message.</param>
		/// <param name="oEx">The exception that forms the basis for the message.</param>
		/// <param name="szHeader">The message header.</param>
		/// <param name="szMessage">The message text.</param>
		void AddMessage(ErrorMessageType type, Exception oEx, string szHeader, string szMessage);

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
		void AddMessage(ErrorMessageType type, Exception oEx, string szHeader, string szMessage, string databaseName,
						string schemaName, string tableName, string primaryKey);

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
		void AddMessage(ErrorMessageType type, Exception oEx, string szHeader, string szMessage, bool shouldPersist,
						string databaseName, string schemaName, string tableName, string primaryKey);

		/// <summary>
		/// Persists all messages to the underlying data repository.
		/// </summary>
		void PersistMessages();
	}
}
