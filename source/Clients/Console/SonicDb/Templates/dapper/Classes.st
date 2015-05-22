using NXS.Data;
using System;

namespace <Model.Namespace>
{
<Model.Tables:{tbl |
	public partial class <tbl.ClassName> // <tbl.Name>
	{
<if(tbl.Enum)>
		public enum IDEnum : <tbl.Enum.BaseClassName>
		{
<tbl.Enum.Values:{enum |
			<enum.Name> = <enum.ID>,
}>		\}
<endif>
<if(tbl.MetaData)>
		public static class MetaData
		{
<tbl.MetaData.Values:{md |
			public const string <md.Name> = "<md.ID>";
}>		\}
<endif>
<if(!tbl.PK.IsID)>
		[IgnorePropertyAttribute(true)] public <tbl.PK.SysTypeFull> ID { get { return <tbl.PK.CleanName>; \} set { <tbl.PK.CleanName> = value; \} \}
<endif>
<tbl.Columns:{col |
		public <col.SysTypeFull> <col.CleanName> { get; set; \}}>
	\}}>
<Model.Views:{tbl |
	public partial class <tbl.ClassName> // <tbl.Name>
	{
<tbl.Columns:{col |
		public <col.SysTypeFull> <col.CleanName> { get; set; \}}>
	\}}>
}
