//
//@NOTE: replace this file with the file you want reverse engineered.
//@NOTE: make sure you:
//		- Change the namespace to SubsonicReverseMe.
//		- Change the provider name to 'ProviderName' or change the provider name in App.config to your provider name.
//

using System;
using System.ComponentModel;
using System.Linq;
using SubSonic;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;

namespace SubsonicReverseMe
{
	[DataContract]
	public partial class Example : ActiveRecord<Example>, INotifyPropertyChanged
	{

		#region Events
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, e);
		}
		#endregion Events

		#region .ctors and Default Settings

		public Example()
		{
			SetSQLProps();InitSetDefaults();MarkNew();
		}
		private void InitSetDefaults() { SetDefaults(); }
		protected static void SetSQLProps() { GetTableSchema(); }

		#endregion

		#region Schema and Query Accessor
		public static Query CreateQuery() { return new Query(Schema); }
		public static TableSchema.Table Schema
		{
			get {
				if (BaseSchema == null) SetSQLProps();
				return BaseSchema;
			}
		}
		private static void GetTableSchema()
		{
			if(!IsSchemaInitialized)
			{
				//Schema declaration
				TableSchema.Table schema = new TableSchema.Table("Examples", TableType.Table, DataService.GetInstance("ProviderName"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarExampleID = new TableSchema.TableColumn(schema);
				colvarExampleID.ColumnName = "ExampleID";
				colvarExampleID.DataType = DbType.Int64;
				colvarExampleID.MaxLength = 0;
				colvarExampleID.AutoIncrement = true;
				colvarExampleID.IsNullable = false;
				colvarExampleID.IsPrimaryKey = true;
				colvarExampleID.IsForeignKey = false;
				colvarExampleID.IsReadOnly = false;
				colvarExampleID.DefaultSetting = @"";
				colvarExampleID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarExampleID);

				TableSchema.TableColumn colvarEventId = new TableSchema.TableColumn(schema);
				colvarEventId.ColumnName = "EventId";
				colvarEventId.DataType = DbType.Int64;
				colvarEventId.MaxLength = 0;
				colvarEventId.AutoIncrement = false;
				colvarEventId.IsNullable = false;
				colvarEventId.IsPrimaryKey = false;
				colvarEventId.IsForeignKey = false;
				colvarEventId.IsReadOnly = false;
				colvarEventId.DefaultSetting = @"";
				colvarEventId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEventId);

				BaseSchema = schema;
				DataService.Providers["ProviderName"].AddSchema("Examples", schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static Example LoadFrom(Example item)
		{
			Example result = new Example();
			if (item.ExampleID != default(long)) {
				result.LoadByKey(item.ExampleID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long ExampleID {
			get { return GetColumnValue<long>(Columns.ExampleID); }
			set {
				SetColumnValue(Columns.ExampleID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ExampleID));
			}
		}
		[DataMember]
		public long EventId {
			get { return GetColumnValue<long>(Columns.EventId); }
			set {
				SetColumnValue(Columns.EventId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.EventId));
			}
		}

		#endregion //Properties

		#region ForeignKey Properties

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return ExampleID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn ExampleIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn EventIdColumn
		{
			get { return Schema.Columns[1]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string ExampleID = @"ExampleID";
			public static readonly string EventId = @"EventId";
		}
		#endregion Columns Struct
		/*
		public override object PrimaryKeyValue
		{
			get { return AdvertiserEventID; }
		}
		*/
	}
}

