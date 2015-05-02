using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NXS.Data.Tests
{
	class Db1 : Database<Db1>
	{
		public Table1Table Table1s { get; set; }
		public Table2Table Table2s { get; set; }
		//public Table<Table1Table, int> Table3 { get; set; }

		public class Table1Table : Table<object, int>
		{
			public Table1Table(Db1 db) : base(db, "t1", "Table1") { }
			public string ID { get { return _alias + "ID"; } }
			public string Name { get { return _alias + "Name"; } }
		}
		public class Table2Table : Table<object, int>
		{
			public Table2Table(Db1 db) : base(db, "t2", "Table2") { }
			public string Col1 { get { return _alias + "Col1"; } }
			public string T1ColId { get { return _alias + "T1ColId"; } }
			public string Col2 { get { return _alias + "Col2"; } }
		}
	}

	public class SequelTests
	{
		private Sequel GetSelectQuery1(bool prettyPrint)
		{
			var db = Db1.Init(null);
			var t1 = db.Table1s;//.As("t1");
			var t2 = db.Table2s;//.As("t2");

			var qry = Sequel.NewSelect(prettyPrint,
				t1.Star
				, t2.Col1
				, t2.Col2
			).From(t1).WithNoLock()
			.InnerJoin(t2).WithNoLock()
			.On(t1.ID, Comparison.Equals, t2.T1ColId, literalText: true)
			.And(a =>
			{
				a.Compare(t1.ID, Comparison.LessThan, 10)
				.Or(t1.ID, Comparison.GreaterThan, 100);
			})
			.Where(t2.Col2, Comparison.GreaterThan, 22);

			return qry;
		}

		[Fact]
		public void TestSelect_With_PrettyPrint()
		{
			var qry = GetSelectQuery1(true);
			Assert.Equal(
@"SELECT
	t1.*
	, t2.Col1
	, t2.Col2
FROM Table1 AS t1 WITH(NOLOCK)
INNER JOIN Table2 AS t2 WITH(NOLOCK)
ON
	(t1.ID = t2.T1ColId)
	AND (
		(t1.ID < @0)
		OR (t1.ID > @1)
	)
WHERE
	(t2.Col2 > @2)", qry.Sql);
		}

		[Fact]
		public void TestSelect_No_PrettyPrint()
		{
			var qry = GetSelectQuery1(false);
			Assert.Equal(
			  "SELECT t1.*,t2.Col1,t2.Col2 FROM Table1 AS t1 WITH(NOLOCK) INNER JOIN Table2 AS t2 WITH(NOLOCK) ON (t1.ID = t2.T1ColId) AND ((t1.ID < @0) OR (t1.ID > @1)) WHERE (t2.Col2 > @2)", qry.Sql);
		}

		private Sequel Get_Mimick_vwMS_AccountMonitorInformations_Query(bool prettyPrint)
		{
			var db = Db1.Init(null);
			var msa = new Db1.Table<object, int>(db, "MSA", "[dbo].[MS_Accounts]");
			var ind = new Db1.Table<object, int>(db, "IND", "[dbo].[MS_IndustryAccounts]");
			var rln = new Db1.Table<object, int>(db, "RLN", "[dbo].[MS_ReceiverLines]");
			var in2 = new Db1.Table<object, int>(db, "IN2", "[dbo].[MS_IndustryAccounts]");
			var mac = new Db1.Table<object, int>(db, "MAC", "[dbo].[MS_AccountCustomers]");
			var lds = new Db1.Table<object, int>(db, "LDS", "[dbo].[QL_Leads]");

			var qry = Sequel.NewSelect(prettyPrint,
				"MSA.AccountID"
				, "MSA.IndustryAccountId"
				, "IND.Csid"
				, "IND.ReceiverLineId"
				, "RLN.MonitoringStationOSId"
				, "MSA.IndustryAccount2Id"
				, "IN2.Csid".As("Csid2")// AS [Csid2]"
				, "IN2.ReceiverLineId".As("ReceiverLine2Id")// AS [ReceiverLine2Id]"
				, "MSA.TechId"
				, "[WISE_HumanResource].[dbo].fxRU_UsersGetFullnameByGPEmployeeID(MSA.TechId) AS TechFullName"
				, "LDS.SalesRepId"
				, "[WISE_HumanResource].[dbo].fxRU_UsersGetFullnameByGPEmployeeID(LDS.SalesRepId) AS SalesFullName"
				, "MSA.SystemTypeId"
				, "MSA.CellularTypeId"
				, "MSA.PanelTypeId"
				, "MSA.DslSeizureId"
				, "MSA.PanelItemId"
				, "MSA.CellPackageItemId"
				, "MSA.ContractId"
				, "MSA.AccountPassword"
			).From(msa).WithNoLock()
			.LeftOuterJoin(ind).WithNoLock()
				.On("IND.IndustryAccountID", Comparison.Equals, "MSA.IndustryAccountId", literalText: true)
			.LeftOuterJoin(rln).WithNoLock()
				.On("RLN.ReceiverLineID", Comparison.Equals, "IND.ReceiverLineId", literalText: true)
			.LeftOuterJoin(in2).WithNoLock()
				.On("IN2.IndustryAccountID", Comparison.Equals, "MSA.IndustryAccount2Id", literalText: true)
			.InnerJoin(mac).WithNoLock()
				.On("MAC.AccountId", Comparison.Equals, "MSA.AccountID", literalText: true)
				.And("MAC.AccountCustomerTypeId", Comparison.In, "('MONI','PRI')", literalText: true)
			.InnerJoin(lds).WithNoLock()
				.On("LDS.LeadID", Comparison.Equals, "MAC.LeadId", literalText: true);

			return qry;
		}
		[Fact]
		public void Test_Mimick_vwMS_AccountMonitorInformations()
		{
			var qry = Get_Mimick_vwMS_AccountMonitorInformations_Query(true);

			Assert.Equal(
@"SELECT
	MSA.AccountID
	, MSA.IndustryAccountId
	, IND.Csid
	, IND.ReceiverLineId
	, RLN.MonitoringStationOSId
	, MSA.IndustryAccount2Id
	, IN2.Csid AS [Csid2]
	, IN2.ReceiverLineId AS [ReceiverLine2Id]
	, MSA.TechId
	, [WISE_HumanResource].[dbo].fxRU_UsersGetFullnameByGPEmployeeID(MSA.TechId) AS TechFullName
	, LDS.SalesRepId
	, [WISE_HumanResource].[dbo].fxRU_UsersGetFullnameByGPEmployeeID(LDS.SalesRepId) AS SalesFullName
	, MSA.SystemTypeId
	, MSA.CellularTypeId
	, MSA.PanelTypeId
	, MSA.DslSeizureId
	, MSA.PanelItemId
	, MSA.CellPackageItemId
	, MSA.ContractId
	, MSA.AccountPassword
FROM [dbo].[MS_Accounts] AS MSA WITH(NOLOCK)
LEFT OUTER JOIN [dbo].[MS_IndustryAccounts] AS IND WITH(NOLOCK)
ON
	(IND.IndustryAccountID = MSA.IndustryAccountId)
LEFT OUTER JOIN [dbo].[MS_ReceiverLines] AS RLN WITH(NOLOCK)
ON
	(RLN.ReceiverLineID = IND.ReceiverLineId)
LEFT OUTER JOIN [dbo].[MS_IndustryAccounts] AS IN2 WITH(NOLOCK)
ON
	(IN2.IndustryAccountID = MSA.IndustryAccount2Id)
INNER JOIN [dbo].[MS_AccountCustomers] AS MAC WITH(NOLOCK)
ON
	(MAC.AccountId = MSA.AccountID)
	AND (MAC.AccountCustomerTypeId IN ('MONI','PRI'))
INNER JOIN [dbo].[QL_Leads] AS LDS WITH(NOLOCK)
ON
	(LDS.LeadID = MAC.LeadId)", qry.Sql);
		}

		public interface IMyInterface { }
		public class MyType : IMyInterface { }
		[Fact]
		public void TestCrap()
		{
			Assert.True(typeof(IMyInterface).IsAssignableFrom(typeof(MyType)));
			Assert.True(typeof(ITable).IsAssignableFrom(typeof(Db1.Table1Table)));
		}

		public Sequel GetGroupByQuery(bool prettyPrint)
		{
			var db = Db1.Init(null);
			var t1 = db.Table1s;
			var t2 = db.Table2s;

			return Sequel.NewSelect(prettyPrint,
				t2.Col1
				, t2.Col2
			).From(t1).WithNoLock()
			.GroupBy(t2.Col2, t2.Col1);
		}
		[Fact]
		public void TestGroupBy_With_PrettyPrint()
		{
			var qry = GetGroupByQuery(true);
			Assert.Equal(
@"SELECT
	t2.Col1
	, t2.Col2
FROM Table1 AS t1 WITH(NOLOCK)
GROUP BY
	t2.Col2
	, t2.Col1", qry.Sql);
		}
		[Fact]
		public void TestGroupBy_No_PrettyPrint()
		{
			var qry = GetGroupByQuery(false);
			Assert.Equal("SELECT t2.Col1,t2.Col2 FROM Table1 AS t1 WITH(NOLOCK) GROUP BY t2.Col2,t2.Col1", qry.Sql);
		}



		public Sequel GetUnionQuery(bool prettyPrint)
		{
			var db = Db1.Init(null);
			var t1 = db.Table1s;
			var t2 = db.Table2s;

			return Sequel.NewSelect(prettyPrint,
				t1.ID
			).From(t1).WithNoLock()
			.Union().Select().Columns(
				t2.Col1.As("ID")
			).From(t2).WithNoLock();
		}
		[Fact]
		public void TestUnion_With_PrettyPrint()
		{
			var qry = GetUnionQuery(true);
			Assert.Equal(
@"SELECT
	t1.ID
FROM Table1 AS t1 WITH(NOLOCK)
UNION
SELECT
	t2.Col1 AS [ID]
FROM Table2 AS t2 WITH(NOLOCK)", qry.Sql);
		}
		[Fact]
		public void TestUnion_No_PrettyPrint()
		{
			var qry = GetUnionQuery(false);
			Assert.Equal("SELECT t1.ID FROM Table1 AS t1 WITH(NOLOCK) UNION SELECT t2.Col1 AS [ID] FROM Table2 AS t2 WITH(NOLOCK)", qry.Sql);
		}



		public Sequel GetOrderByQuery(bool prettyPrint)
		{
			var db = Db1.Init(null);
			var t2 = db.Table2s;

			return Sequel.NewSelect(prettyPrint,
				t2.Col1
				, t2.Col2
			).From(t2).WithNoLock()
			.OrderBy(t2.Col2, t2.Col1);
		}
		[Fact]
		public void TestOrderBy_With_PrettyPrint()
		{
			var qry = GetOrderByQuery(true);
			Assert.Equal(
@"SELECT
	t2.Col1
	, t2.Col2
FROM Table2 AS t2 WITH(NOLOCK)
ORDER BY
	t2.Col2
	, t2.Col1", qry.Sql);
		}
		[Fact]
		public void TestOrderBy_No_PrettyPrint()
		{
			var qry = GetOrderByQuery(false);
			Assert.Equal("SELECT t2.Col1,t2.Col2 FROM Table2 AS t2 WITH(NOLOCK) ORDER BY t2.Col2,t2.Col1", qry.Sql);
		}



		public Sequel GetAndSql(bool prettyPrint)
		{
			var db = Db1.Init(null);
			var t1 = db.Table1s;

			return Sequel.NewSelect(prettyPrint,
				t1.ID
			).From(t1)
			.Where(t1.ID, Comparison.Equals, 1)
			.And(t1.Name, Comparison.Equals, "asdf");
		}
		[Fact]
		public void Test_And_With_String_Value_With_PrettyPrint()
		{
			var qry = GetAndSql(true);
			Assert.Equal(
@"SELECT
	t1.ID
FROM Table1 AS t1
WHERE
	(t1.ID = @0)
	AND (t1.Name = @1)", qry.Sql);
		}
		[Fact]
		public void Test_And_With_String_Value_No_PrettyPrint()
		{
			var qry = GetAndSql(false);
			Assert.Equal("SELECT t1.ID FROM Table1 AS t1 WHERE (t1.ID = @0) AND (t1.Name = @1)", qry.Sql);
		}



		public Sequel GetEnumerableQuery(bool prettyPrint)
		{
			//var msa = new QueryTable("[dbo].[MS_Accounts]", "MSA");
			//var mac = new QueryTable("[dbo].[MS_AccountCustomers]", "MAC");

			var db = Db1.Init(null);
			var msa = new Db1.Table<object, int>(db, "MSA", "[dbo].[MS_Accounts]");
			var mac = new Db1.Table<object, int>(db, "MAC", "[dbo].[MS_AccountCustomers]");

			return Sequel.NewSelect(prettyPrint,
				"MSA.AccountID"
			).From(msa).WithNoLock()
			.InnerJoin(mac).WithNoLock()
				.On("MAC.AccountId", Comparison.Equals, "MSA.AccountID", literalText: true)
				.And("MAC.AccountCustomerTypeId", Comparison.In, new string[] { "MONI", "PRI" });
		}
		[Fact]
		public void Test_Enumerable_Values_With_PrettyPrint()
		{
			var qry = GetEnumerableQuery(true);
			Assert.Equal(
@"SELECT
	MSA.AccountID
FROM [dbo].[MS_Accounts] AS MSA WITH(NOLOCK)
INNER JOIN [dbo].[MS_AccountCustomers] AS MAC WITH(NOLOCK)
ON
	(MAC.AccountId = MSA.AccountID)
	AND (MAC.AccountCustomerTypeId IN (
		@0
		, @1
	))", qry.Sql);
		}
		[Fact]
		public void Test_Enumerable_Values_No_PrettyPrint()
		{
			var qry = GetEnumerableQuery(false);
			Assert.Equal("SELECT MSA.AccountID FROM [dbo].[MS_Accounts] AS MSA WITH(NOLOCK) INNER JOIN [dbo].[MS_AccountCustomers] AS MAC WITH(NOLOCK) ON" +
				" (MAC.AccountId = MSA.AccountID) AND (MAC.AccountCustomerTypeId IN (@0,@1))", qry.Sql);
		}


		public Sequel GetNestedSelectSql(bool prettyPrint)
		{
			var db = Db1.Init(null);
			var t = new Db1.Table<object, int>(db, "T", "[dbo].[Table]");
			return Sequel.NewSelect(prettyPrint,
				"T2.ID"
			).From(s =>
			{
				s.Select().Columns(
					"T.ID"
				).From(t);
			}, "T2")
			.Where("T2.ID", Comparison.Equals, 10);
		}
		[Fact]
		public void Test_Nested_Select_With_PrettyPrint()
		{
			var qry = GetNestedSelectSql(true);
			Assert.Equal(
@"SELECT
	T2.ID
FROM (
	SELECT
		T.ID
	FROM [dbo].[Table] AS T
) AS T2
WHERE
	(T2.ID = @0)", qry.Sql);
		}
		[Fact]
		public void Test_Nested_Select_No_PrettyPrint()
		{
			var qry = GetNestedSelectSql(false);
			Assert.Equal(@"SELECT T2.ID FROM (SELECT T.ID FROM [dbo].[Table] AS T) AS T2 WHERE (T2.ID = @0)", qry.Sql);
		}




		public Sequel GetIsNullSql(bool prettyPrint)
		{
			var db = Db1.Init(null);
			var t = new Db1.Table<object, int>(db, "T", "[dbo].[Table]");
			return Sequel.NewSelect(prettyPrint,
				"T.*"
			).From(t)
			.Where("T.Col1", Comparison.Is, null)
			.And("T.Col2", Comparison.IsNot, null);
		}
		[Fact]
		public void Test_IsNull_With_PrettyPrint()
		{
			var qry = GetIsNullSql(true);
			Assert.Equal(
@"SELECT
	T.*
FROM [dbo].[Table] AS T
WHERE
	(T.Col1 IS NULL)
	AND (T.Col2 IS NOT NULL)", qry.Sql);
		}
		[Fact]
		public void Test_IsNull_No_PrettyPrint()
		{
			var qry = GetIsNullSql(false);
			Assert.Equal(@"SELECT T.* FROM [dbo].[Table] AS T WHERE (T.Col1 IS NULL) AND (T.Col2 IS NOT NULL)", qry.Sql);
		}



		public partial class Table3asfd
		{
			//#region .ctors and Default Settings
			//public Table3()
			//{
			//	SetSQLProps(); InitSetDefaults(); MarkNew();
			//}
			//private void InitSetDefaults() { SetDefaults(); }
			//protected static void SetSQLProps() { GetTableSchema(); }
			//#endregion

			//#region Schema and Query Accessor
			//public static Query CreateQuery() { return new Query(Schema); }
			//public static TableSchema.Table Schema
			//{
			//	get
			//	{
			//		if (BaseSchema == null) SetSQLProps();
			//		return BaseSchema;
			//	}
			//}
			//private static void GetTableSchema()
			//{
			//	if (!IsSchemaInitialized)
			//	{
			//		//Schema declaration
			//		TableSchema.Table schema = new TableSchema.Table("Table3", TableType.Table, DataService.GetInstance("SosCrmProvider"));
			//		schema.Columns = new TableSchema.TableColumnCollection();
			//		schema.SchemaName = @"dbo";
			//		//columns
			//
			//		TableSchema.TableColumn colvarID = new TableSchema.TableColumn(schema);
			//		colvarID.ColumnName = "ID";
			//		colvarID.DataType = DbType.Int32;
			//		colvarID.MaxLength = 0;
			//		colvarID.AutoIncrement = true;
			//		colvarID.IsNullable = false;
			//		colvarID.IsPrimaryKey = true;
			//		colvarID.IsForeignKey = false;
			//		colvarID.IsReadOnly = false;
			//		colvarID.DefaultSetting = @"";
			//		colvarID.ForeignKeyTableName = "";
			//		schema.Columns.Add(colvarID);
			//
			//		BaseSchema = schema;
			//		DataService.Providers["SosCrmProvider"].AddSchema("Table3", schema);
			//	}
			//}
			//#endregion // Schema and Query Accessor

			#region Properties
			public int ID { get; set; }
			public int Name { get; set; }
			#endregion //Properties

			#region MaxLengths
			public static class MaxLength
			{
				//public static readonly int ID = 0;
				public static readonly int Name = 100;
			}
			#endregion //MaxLengths

			#region Columns Struct
			public class Schema
			{
				public static readonly string ID = @"ID";
				public static readonly string UnitPrice = @"UnitPrice";
			}
			#endregion Columns Struct

			/*
		public override object PrimaryKeyValue
		{
			get { return ID; }
		}
		*/


		}
	}
}
