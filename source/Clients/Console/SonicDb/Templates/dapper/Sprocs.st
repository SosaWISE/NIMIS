using Dapper;
using NXS.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace <Model.Namespace>
{
	public partial class Sprocs
	{
		private readonly <Model.DatabaseName> db;
		public Sprocs(<Model.DatabaseName> db)
		{
			this.db = db;
		}

<Model.Sprocs:{sp |
		public Task\<IEnumerable\<T>> <sp.CleanName>\<T>(<sp.Args>)
		{
			var p = new Dapper.DynamicParameters();
<sp.Parameters:{p |
<if(p.InputOutput)>// InputOutput not supported<else>
			p.Add("<p.Name>", <p.CleanName>);<endif>}>
			return db.QueryAsync\<T>("<sp.Name>", p, commandType: System.Data.CommandType.StoredProcedure);
		\}}>
	}
}

