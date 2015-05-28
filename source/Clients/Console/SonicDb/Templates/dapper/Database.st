using NXS.Data;
using System;

namespace <Model.Namespace>
{
	public partial class <Model.DatabaseName> : Database\<<Model.DatabaseName>>
	{
		public static readonly string Database = "<Model.Database>";
		public readonly Sprocs Sprocs;
		public <Model.DatabaseName>()
		{
			Sprocs = new Sprocs(this);
		}

<Model.Tables:{tbl |
		public <tbl.ClassName>Table <tbl.QueryableName> { get; set; \}}>
<Model.Views:{tbl |
		public <tbl.ClassName>Table <tbl.QueryableName> { get; set; \}}>

<Model.Tables:{tbl |
		public partial class <tbl.ClassName>Table : Table\<<tbl.ClassName>, <tbl.PK.SysType>>
		{
			public <Model.DatabaseName> Db { get { return (<Model.DatabaseName>)_database; \} \}
			public <tbl.ClassName>Table(<Model.DatabaseName> db) : base(db, "<tbl.Alias>", "[<Model.Database>].[dbo].[<tbl.Name>]", "<tbl.PK.Name>", "<tbl.PK.DataType>", <tbl.PK.IsIdentity>) { \}
<tbl.Columns:{col |
			public string <col.CleanName> { get { return _alias + "[<col.Name>]"; \} \}}>
		\}}>
<Model.Views:{tbl |
		public partial class <tbl.ClassName>Table : Table\<<tbl.ClassName>, <tbl.PK.SysType>>
		{
			public <Model.DatabaseName> Db { get { return (<Model.DatabaseName>)_database; \} \}
			public <tbl.ClassName>Table(<Model.DatabaseName> db) : base(db, "<tbl.Alias>", "[<Model.Database>].[dbo].[<tbl.Name>]", "<tbl.PK.Name>", "<tbl.PK.DataType>", <tbl.PK.IsIdentity>) { \}
<tbl.Columns:{col |
			public string <col.CleanName> { get { return _alias + "[<col.Name>]"; \} \}}>
		\}}>

	}
}
