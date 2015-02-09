using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Runtime.Serialization;
using SOS.Lib.Core;
using SOS.Lib.Util.Configuration;
using SubSonic;

namespace SOS.Data
{
	[DataContract]
	public abstract class BusinessEntity<T> : ActiveRecord<T>, IBusinessEntity where T : ActiveRecord<T>, new()
	{
		#region Properties

		#region Public

		private static readonly TableSchema.Table _currentSchema;
		private static readonly string _databaseName;
		private static int _installerCode = 0;
		private static readonly string _schemaName;
		private static readonly string _tableName;

		/// <summary>
		/// Gets the schema of the ActiveRecord.
		/// </summary>
		internal static TableSchema.Table CurrentSchema
		{
			get { return _currentSchema; }
		}

		public static string DatabaseName
		{
			get { return _databaseName; }
		}

		public static string SchemaName
		{
			get { return _schemaName; }
		}

		public new static string TableName
		{
			get { return _tableName; }
		}

		public IEnumerable<T> MockDataSource
		{
			get { yield return this as T; }
		}

		public static int InstallerCode
		{
			get
			{
				if (_installerCode == 0)
					_installerCode = int.Parse(ConfigurationSettings.Current.GetConfig("INSTALLER_CODE"));

				return _installerCode;
			}
		}

		/// <summary>
		/// Gets the value of the primary key field(s).  If the primary key is a single column,
		/// this returns the value of that column.  Otherwise, it returns an object array with
		/// the values of each primary key column.
		/// </summary>
		public abstract object PrimaryKeyValue { get; }

		public TableSchema.Table SchemaTable
		{
			get { return CurrentSchema; }
		}

		#endregion Public

		#region Protected

		private static readonly Dictionary<string, bool> _propertiesToIgnore;

		protected static Dictionary<string, bool> PropertiesToIgnore
		{
			get { return _propertiesToIgnore; }
		}

		#endregion Protected

		#endregion Properties

		#region Events

		/// <summary>
		/// Fires on a successful ActiveRecord Save()
		/// </summary>
		public event EventHandler Saved;


		/// <summary>
		/// Executes after all steps have been performed for a successful ActiveRecord Save()
		/// </summary>
		protected override void AfterCommit()
		{
			OnSaved(EventArgs.Empty);
			base.AfterCommit();
		}

		/// <summary>
		/// Fires the Saved event.
		/// </summary>
		protected virtual void OnSaved(EventArgs e)
		{
			if (Saved != null)
				Saved(this, e);
		}

		#endregion

		#region Constructors

		static BusinessEntity()
		{
			_propertiesToIgnore = new Dictionary<string, bool>(StringComparer.InvariantCultureIgnoreCase);
			// Add properties that belong to SubSonic
			_propertiesToIgnore.Add("BaseSchema", true);
			_propertiesToIgnore.Add("DirtyColumns", true);
			_propertiesToIgnore.Add("Errors", true);
			_propertiesToIgnore.Add("InvalidTypeExceptionMessage", true);
			_propertiesToIgnore.Add("IsDirty", true);
			_propertiesToIgnore.Add("IsLoaded", true);
			_propertiesToIgnore.Add("IsNew", true);
			_propertiesToIgnore.Add("IsSchemaInitialized", true);
			_propertiesToIgnore.Add("LengthExceptionMessage", true);
			_propertiesToIgnore.Add("NullExceptionMessage", true);
			_propertiesToIgnore.Add("ParameterPrefix", true);
			_propertiesToIgnore.Add("ProviderName", true);
			_propertiesToIgnore.Add("TableName", true);
			_propertiesToIgnore.Add("ValidateWhenSaving", true);

			Type reflectedType = typeof(T);
			foreach (PropertyInfo currProperty in reflectedType.GetProperties(BindingFlags.Static | BindingFlags.Public)
				)
			{
				if (currProperty.Name.Equals("schema", StringComparison.InvariantCultureIgnoreCase))
				{
					_currentSchema = currProperty.GetValue(default(T), null) as TableSchema.Table;
					break;
				}
			}

			if (_currentSchema != null)
			{
				_databaseName = _currentSchema.Provider is SosCrmProvider
									? ((SosCrmProvider)_currentSchema.Provider).DatabaseName
									: "UNKNOWN";
				_schemaName = _currentSchema.SchemaName;
				_tableName = _currentSchema.TableName;
			}
			else
				_databaseName = _schemaName = _tableName = "UNKNOWN";
		}

		#endregion Constructors

		#region Methods

		#region Public

		public T ExecuteFetchByID(int id)
		{
			return FetchByID(id);
		}

		/*/
		public virtual void SaveAndLogChanges(string userID, string requestedByID, string szSourceView)
		{
			long? changeLogID;
			SaveAndLogChanges(userID, requestedByID, szSourceView, out changeLogID);
		}

		public virtual void SaveAndLogChanges(string userID, string requestedByID, string szSourceView,
											  out long? changeLogID)
		{
			changeLogID = null;

			// We can only track changes on items w/ an integer primary key
			if (PrimaryKeyValue.GetType() == typeof(int)) {
				if (IsNew) {
					// Do the save so we can get a primary key
					Save();

					LG_ChangeLog oLog = new LG_ChangeLog();
					oLog.ChangeLogTypeID = (int)LG_ChangeLogType.TypeCodes.Insert;
					oLog.LogSourceID = 1;
					oLog.TargetDatabase = (CurrentSchema.Provider is PlatinumDataProvider)
											  ?
												  ((PlatinumDataProvider)CurrentSchema.Provider).DatabaseName
											  : "UNKNOWN";
					oLog.TargetSchema = CurrentSchema.SchemaName;
					oLog.TargetTable = CurrentSchema.TableName;
					oLog.TargetPrimaryKey = (int)PrimaryKeyValue;
					oLog.SourceView = szSourceView;
					oLog.RequestedByID = requestedByID;
					oLog.ModifiedByID = userID;
					oLog.ModifiedByDate = DateTime.Now;
					oLog.Save();

					Type reflectedType = typeof(T);
					object newVal;
					PropertyInfo currProperty;
					LG_Change oChange;
					foreach (TableSchema.TableColumn currColumn in CurrentSchema.Columns) {
						currProperty = reflectedType.GetProperty(currColumn.PropertyName);
						if (currProperty != null) {
							newVal = currProperty.GetValue(this, null);
							if (newVal == null)
								newVal = "[Empty]";

							oChange = new LG_Change();
							oChange.ChangeLogID = oLog.ChangeLogID;
							oChange.TargetColumn = currColumn.ColumnName;
							oChange.OldValue = null;
							oChange.NewValue = newVal.ToString();
							oChange.Save();
						}
					}

					changeLogID = oLog.ChangeLogID;
				}
				else {
					// Get the copy from the database
					T oldCopy = FetchByID((int)PrimaryKeyValue);
					Type reflectedType = typeof(T);

					// See what has changed
					LG_ChangeCollection oChanges = new LG_ChangeCollection();
					PropertyInfo currProperty;
					object oldVal, newVal;
					LG_Change oChange;
					foreach (TableSchema.TableColumn currColumn in CurrentSchema.Columns) {
						currProperty = reflectedType.GetProperty(currColumn.PropertyName);
						if (currProperty == null)
							currProperty = reflectedType.GetProperty(currColumn.ColumnName);
						if (currProperty != null) {
							oldVal = currProperty.GetValue(oldCopy, null);
							newVal = currProperty.GetValue(this, null);

							// Trim strings because we don't care about whitespace for the equality comparison
							if (oldVal is string) oldVal = ((string)oldVal).Trim();
							if (newVal is string) newVal = ((string)newVal).Trim();

							// If the values are different, log the change.
							if ((oldVal == null && newVal != null) || (oldVal != null && newVal == null) ||
								(oldVal != null && !oldVal.Equals(newVal))) {
								oChange = new LG_Change();
								oChange.TargetColumn = currColumn.ColumnName;
								oChange.OldValue = (oldVal == null) ? null : oldVal.ToString();
								oChange.NewValue = (newVal == null) ? null : newVal.ToString();
								oChanges.Add(oChange);
							}
						}
					}

					if (oChanges.Count > 0) {
						LG_ChangeLog oLog = new LG_ChangeLog();
						oLog.ChangeLogTypeID = (int)LG_ChangeLogType.TypeCodes.Update;
						oLog.LogSourceID = (int)SubSonicConfigHelper.GetLogSourceFromConfig();
						oLog.TargetDatabase = (CurrentSchema.Provider is PlatinumDataProvider)
												  ?
													  ((PlatinumDataProvider)CurrentSchema.Provider).DatabaseName
												  : "UNKNOWN";
						oLog.TargetSchema = CurrentSchema.SchemaName;
						oLog.TargetTable = CurrentSchema.TableName;
						oLog.TargetPrimaryKey = (int)PrimaryKeyValue;
						oLog.SourceView = szSourceView;
						oLog.RequestedByID = requestedByID;
						oLog.ModifiedByID = userID;
						oLog.ModifiedByDate = DateTime.Now;
						oLog.Save();

						foreach (LG_Change curr in oChanges) {
							curr.ChangeLogID = oLog.ChangeLogID;
							curr.Save();
						}

						changeLogID = oLog.ChangeLogID;
					}

					// Save now that we've tracked the changes
					Save();
				}
			}
			else // Not an int primary key - just save
				Save();
		}
		//*/

		public bool DiffEqual(BusinessEntity<T> right)
		{
			foreach (TableSchema.TableColumn column in BaseSchema.Columns)
			{
				bool equal;
				switch (column.DataType)
				{
					case DbType.AnsiString:
					case DbType.AnsiStringFixedLength:
					case DbType.String:
					case DbType.StringFixedLength:
					case DbType.Xml:
						equal = GetColumnValue<string>(column.ColumnName) ==
								right.GetColumnValue<string>(column.ColumnName);
						break;
					case DbType.Boolean:
						equal = GetColumnValue<bool>(column.ColumnName) == right.GetColumnValue<bool>(column.ColumnName);
						break;
					case DbType.Currency:
					case DbType.Decimal:
						equal = GetColumnValue<decimal>(column.ColumnName) ==
								right.GetColumnValue<decimal>(column.ColumnName);
						break;
					case DbType.Date:
					case DbType.DateTime:
					case DbType.DateTime2:
					case DbType.Time:
						equal = GetColumnValue<DateTime>(column.ColumnName) ==
								right.GetColumnValue<DateTime>(column.ColumnName);
						break;
					//equal = this.GetColumnValue<string>(column.ColumnName) == right.GetColumnValue<string>(column.ColumnName);
					//break;
					case DbType.Double:
						equal = GetColumnValue<double>(column.ColumnName) ==
								right.GetColumnValue<double>(column.ColumnName);
						break;
					case DbType.Guid:
						equal = GetColumnValue<Guid>(column.ColumnName) == right.GetColumnValue<Guid>(column.ColumnName);
						break;
					case DbType.Int16:
						equal = GetColumnValue<short>(column.ColumnName) ==
								right.GetColumnValue<short>(column.ColumnName);
						break;
					case DbType.Int32:
						equal = GetColumnValue<int>(column.ColumnName) == right.GetColumnValue<int>(column.ColumnName);
						break;
					case DbType.Int64:
						equal = GetColumnValue<long>(column.ColumnName) == right.GetColumnValue<long>(column.ColumnName);
						break;
					case DbType.Object:
						equal = GetColumnValue<object>(column.ColumnName) ==
								right.GetColumnValue<object>(column.ColumnName);
						break;
					case DbType.Single:
						equal = GetColumnValue<float>(column.ColumnName) ==
								right.GetColumnValue<float>(column.ColumnName);
						break;
					case DbType.UInt16:
						equal = GetColumnValue<ushort>(column.ColumnName) ==
								right.GetColumnValue<ushort>(column.ColumnName);
						break;
					case DbType.UInt32:
						equal = GetColumnValue<uint>(column.ColumnName) == right.GetColumnValue<uint>(column.ColumnName);
						break;
					case DbType.UInt64:
						equal = GetColumnValue<ulong>(column.ColumnName) ==
								right.GetColumnValue<ulong>(column.ColumnName);
						break;
					default:
						throw new NotImplementedException();
				}
				if (!equal) return false;
			}
			return true;
		}

		public void CopyTo<TConversion>(T oSource, List<string> szPrimaryKey) where TConversion : AbstractRecord<TConversion>, new()
		{
			TableSchema.TableColumnSettingCollection columnSettings = oSource.GetColumnSettings();
			foreach (TableSchema.TableColumnSetting setting in columnSettings)
			{
				if (!szPrimaryKey.Contains(setting.ColumnName))
					oSource.SetColumnValue(setting.ColumnName, GetColumnValue<object>(setting.ColumnName));
			}
		}

		#endregion Public

		#endregion Methods
	}
}
