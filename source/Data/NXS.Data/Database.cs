/*
 License: http://www.apache.org/licenses/LICENSE-2.0 
 Home page: http://code.google.com/p/dapper-dot-net/
*/

using Dapper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace NXS.Data
{
	public interface ITable
	{
		string Alias { get; }
		string TableName { get; }
		string PkName { get; }
		string Star { get; }
	}

	/// <summary>
	/// A container for a database
	/// </summary>
	/// <typeparam name="TDatabase"></typeparam>
	public abstract partial class Database<TDatabase> : IDisposable where TDatabase : Database<TDatabase>, new()
	{
		DbConnection _connection;
		int _commandTimeout;
		DbTransaction _transaction;

		//@TODO: remove this once some better is created
		public static string ConnectionString;
		public static TDatabase Connect(int commandTimeout = 3)
		{
			var cn = new System.Data.SqlClient.SqlConnection(ConnectionString);
			cn.Open();
			var db = Init(cn, commandTimeout);
			return db;
		}

		public static TDatabase Init(DbConnection connection, int commandTimeout = 0)
		{
			TDatabase db = new TDatabase();
			db.InitDatabase(connection, commandTimeout);
			return db;
		}

		internal static Action<TDatabase> tableConstructor;

		internal void InitDatabase(DbConnection connection, int commandTimeout)
		{
			this._connection = connection;
			this._commandTimeout = commandTimeout;
			if (tableConstructor == null)
			{
				tableConstructor = CreateTableConstructorForTable();
			}

			tableConstructor(this as TDatabase);
		}

		internal virtual Action<TDatabase> CreateTableConstructorForTable()
		{
			return CreateTableConstructor(typeof(ITable));
		}

		//public DbTransaction Transaction
		//{
		//	get { return _transaction; }
		//}
		public void BeginTransaction(IsolationLevel isolation = IsolationLevel.ReadCommitted)
		{
			if (_transaction != null)
				throw new Exception("transaction already exists");
			_transaction = _connection.BeginTransaction(isolation);
		}
		public void CommitTransaction()
		{
			_transaction.Commit();
			_transaction = null;
		}
		public void RollbackTransaction()
		{
			_transaction.Rollback();
			_transaction = null;
		}

		public async Task<bool> TransactionAsync(Func<Task<bool>> action, IsolationLevel isolation = IsolationLevel.ReadCommitted)
		{
			BeginTransaction(isolation);
			try
			{
				// return true from `action` to signify it should be committed
				if (await action().ConfigureAwait(false))
				{
					// commit the transaction
					CommitTransaction();
					return true;
				}
			}
			//catch { } // no catch since we don't want to trap the exception
			finally
			{
				// rollback the transaction on exception or false returned from action
				if (_transaction != null)
					RollbackTransaction();
			}
			return false;
		}
		public bool Transaction(Func<bool> action, IsolationLevel isolation = IsolationLevel.ReadCommitted)
		{
			BeginTransaction(isolation);
			try
			{
				// return true from `action` to signify it should be committed
				if (action())
				{
					// commit the transaction
					CommitTransaction();
					return true;
				}
			}
			//catch { } // no catch since we don't want to trap the exception
			finally
			{
				// rollback the transaction on exception or false returned from action
				if (_transaction != null)
					RollbackTransaction();
			}
			return false;
		}

		protected Action<TDatabase> CreateTableConstructor(Type tableType)
		{
			var dm = new DynamicMethod("ConstructInstances", null, new Type[] { typeof(TDatabase) }, true);
			var il = dm.GetILGenerator();

			var setters = GetType().GetProperties()
				.Where(p =>
				{
					return (p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == tableType) ||
						tableType.IsAssignableFrom(p.PropertyType);
				})
				.Select(p => Tuple.Create(
						p.GetSetMethod(true),
						p.PropertyType.GetConstructor(new Type[] { typeof(TDatabase) }),
						p.DeclaringType
				 ));

			foreach (var setter in setters)
			{
				il.Emit(OpCodes.Ldarg_0);
				// [db]

				il.Emit(OpCodes.Newobj, setter.Item2);
				// [table]

				var table = il.DeclareLocal(setter.Item2.DeclaringType);
				il.Emit(OpCodes.Stloc, table);
				// []

				il.Emit(OpCodes.Ldarg_0);
				// [db]

				il.Emit(OpCodes.Castclass, setter.Item3);
				// [db cast to container]

				il.Emit(OpCodes.Ldloc, table);
				// [db cast to container, table]

				il.Emit(OpCodes.Callvirt, setter.Item1);
				// []
			}

			il.Emit(OpCodes.Ret);
			return (Action<TDatabase>)dm.CreateDelegate(typeof(Action<TDatabase>));
		}

		public int Execute(string sql, dynamic param = null)
		{
			return _connection.Execute(sql, param as object, _transaction, this._commandTimeout);
		}
		public Task<int> ExecuteAsync(string sql, dynamic param = null)
		{
			return _connection.ExecuteAsync(sql, param as object, _transaction, this._commandTimeout);
		}

		public IEnumerable<T> Query<T>(string sql, dynamic param = null)
		{
			return _connection.Query<T>(sql, param as object, _transaction, commandTimeout: _commandTimeout);
		}
		public Task<IEnumerable<T>> QueryAsync<T>(string sql, dynamic param = null)
		{
			return _connection.QueryAsync<T>(sql, param as object, _transaction, _commandTimeout);
		}

		//public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "ID", int? commandTimeout = null)
		//{
		//	return _connection.QueryAsync(sql, map, param as object, transaction, buffered, splitOn);
		//}
		//public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "ID", int? commandTimeout = null)
		//{
		//	return _connection.QueryAsync(sql, map, param as object, transaction, buffered, splitOn);
		//}
		//public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "ID", int? commandTimeout = null)
		//{
		//	return _connection.QueryAsync(sql, map, param as object, transaction, buffered, splitOn);
		//}
		//public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "ID", int? commandTimeout = null)
		//{
		//	return _connection.QueryAsync(sql, map, param as object, transaction, buffered, splitOn);
		//}
		public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string[] splitOn = null, int? commandTimeout = null)
		{
			return _connection.QueryAsync(sql, map, param as object, transaction ?? _transaction, buffered, ToSplitOnStr(splitOn));
		}
		public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string[] splitOn = null, int? commandTimeout = null)
		{
			return _connection.QueryAsync(sql, map, param as object, transaction ?? _transaction, buffered, ToSplitOnStr(splitOn));
		}
		public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string[] splitOn = null, int? commandTimeout = null)
		{
			return _connection.QueryAsync(sql, map, param as object, transaction ?? _transaction, buffered, ToSplitOnStr(splitOn));
		}
		public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string[] splitOn = null, int? commandTimeout = null)
		{
			return _connection.QueryAsync(sql, map, param as object, transaction ?? _transaction, buffered, ToSplitOnStr(splitOn));
		}

		public Task<IEnumerable<dynamic>> QueryAsync(string sql, dynamic param = null)
		{
			return _connection.QueryAsync(sql, param as object, _transaction);
		}
		public Task<SqlMapper.GridReader> QueryMultipleAsync(string sql, dynamic param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
		{
			return SqlMapper.QueryMultipleAsync(_connection, sql, param, transaction ?? _transaction, commandTimeout, commandType);
		}


		private static string ToSplitOnStr(string[] splitOn)
		{
			if (splitOn == null || splitOn.Length == 0)
				return "ID";

			var length = splitOn.Length;
			for (var i = 0; i < length; i++)
			{
				splitOn[i] = RawColName(splitOn[i]);
			}
			return string.Join(",", splitOn);
		}
		private static string RawColName(string colname)
		{
			var endIndex = colname.LastIndexOf(']');
			var startIndex = colname.LastIndexOf('[', endIndex);
			if (startIndex < 0 || endIndex < 0)
				return colname;
			++startIndex;
			return colname.Substring(startIndex, endIndex - startIndex);
		}


		public void Dispose()
		{
			if (_connection.State != ConnectionState.Closed)
			{
				if (_transaction != null)
					_transaction.Rollback();
				_connection.Close();
				_connection = null;
			}
		}

		public partial class Table<T, TID> : ITable
		{
			protected Database<TDatabase> _database;
			internal string _name;
			internal string _pkName;
			internal string _pkDbType;
			internal bool _hasIdentity;
			protected readonly string _aliasNoDot;
			protected readonly string _alias;

			//public Table(Database<TDatabase> database) : this(database, null, null, null) { }
			public Table(Database<TDatabase> database, string alias, string name, string pkName = "ID", string pkDbType = "int", bool hasIdentity = true)
			{
				_aliasNoDot = alias;
				_alias = string.IsNullOrEmpty(alias) ? "" : (alias + ".");

				_database = database;
				_name = name;
				_pkName = pkName;
				_pkDbType = pkDbType;
				_hasIdentity = hasIdentity;
			}

			public string Alias { get { return _aliasNoDot; } }
			public string TableName { get { return _name; } }
			public string PkName { get { return _pkName; } }
			public string Star { get { return _alias + "*"; } }

			/// <summary>
			/// Insert a row into the db synchronously
			/// </summary>
			/// <param name="data">Either DynamicParameters or an anonymous type or concrete type</param>
			/// <returns>SCOPE_IDENTITY() or default(TID)</returns>
			public virtual TID Insert(dynamic data)
			{
				var obj = (object)data;
				var sql = InsertSql(obj);
				if (_hasIdentity)
					return _database.Query<TID>(sql, obj).FirstOrDefault();
				else
				{
					_database.Execute(sql, obj);
					return default(TID);
				}
			}
			/// <summary>
			/// Insert a row into the db asynchronously
			/// </summary>
			/// <param name="data">Either DynamicParameters or an anonymous type or concrete type</param>
			/// <returns>SCOPE_IDENTITY() or default(TID)</returns>
			public virtual async Task<TID> InsertAsync(dynamic data)
			{
				var sql = InsertSql((object)data);
				if (_hasIdentity)
					return (await _database.QueryAsync<TID>(sql, (object)data).ConfigureAwait(false)).FirstOrDefault();
				else
				{
					await _database.ExecuteAsync(sql, (object)data).ConfigureAwait(false);
					return default(TID);
				}
			}
			private string InsertSql(object data)
			{
				List<string> paramNames = GetParamNames(data)
					.Where(name => !_hasIdentity || name != _pkName).ToList();

				string cols = string.Join(",", paramNames);
				string cols_params = string.Join(",", paramNames.Select(p => "@" + p));
				var sql = "SET NOCOUNT ON INSERT " + TableName + " (" + cols + ") VALUES (" + cols_params + ")";
				if (_hasIdentity)
					sql += " SELECT CAST(SCOPE_IDENTITY() AS " + _pkDbType + ")";
				return sql;
			}

			/// <summary>
			/// Update a record in the DB synchronously
			/// </summary>
			/// <param name="id"></param>
			/// <param name="data"></param>
			/// <returns></returns>
			public int Update(TID id, dynamic data)
			{
				DynamicParameters parameters;
				var sql = UpdateSql(id, (object)data, out parameters);
				if (sql == null)
					return 0;
				return _database.Execute(sql, parameters);
			}
			/// <summary>
			/// Update a record in the DB asynchronously
			/// </summary>
			/// <param name="id"></param>
			/// <param name="data"></param>
			/// <returns></returns>
			public Task<int> UpdateAsync(TID id, dynamic data)
			{
				DynamicParameters parameters;
				var sql = UpdateSql(id, (object)data, out parameters);
				if (sql == null)
					return Task.FromResult(0);
				return _database.ExecuteAsync(sql, parameters);
			}
			private string UpdateSql(TID id, object data, out DynamicParameters parameters)
			{
				List<string> paramNames = GetParamNames(data)
					.Where(name => name != _pkName).ToList();
				if (paramNames.Count == 0)
				{
					parameters = null;
					return null;
				}

				var builder = new StringBuilder();
				builder.Append("UPDATE ").Append(TableName).Append(" SET ");
				builder.AppendLine(string.Join(",", paramNames.Select(p => p + "= @" + p)));
				builder.Append("WHERE " + PkName + " = @id");

				parameters = new DynamicParameters(data);
				parameters.Add("id", id);

				return builder.ToString();
			}

			/// <summary>
			/// Delete a record from the DB synchronously
			/// </summary>
			/// <param name="id"></param>
			/// <returns></returns>
			public bool Delete(TID id)
			{
				return _database.Execute("DELETE FROM " + TableName + " WHERE " + PkName + " = @id", new { id }) > 0;
			}
			/// <summary>
			/// Delete a record from the DB asynchronously
			/// </summary>
			/// <param name="id"></param>
			/// <returns></returns>
			public async Task<bool> DeleteAsync(TID id)
			{
				return (await _database.ExecuteAsync("DELETE FROM " + TableName + " WHERE " + PkName + " = @id", new { id }).ConfigureAwait(false)) > 0;
			}
			/// <summary>
			/// Grab a record with a particular ID from the DB asynchronously
			/// </summary>
			/// <param name="id"></param>
			/// <returns></returns>
			public async Task<T> ByIdAsync(TID id)
			{
				return (await _database.QueryAsync<T>("SELECT * FROM " + TableName + " WHERE " + PkName + " = @id", new { id }).ConfigureAwait(false)).FirstOrDefault();
			}
			public virtual async Task<T> FirstAsync()
			{
				return (await _database.QueryAsync<T>("SELECT TOP(1) * FROM " + TableName).ConfigureAwait(false)).FirstOrDefault();
			}

			public IEnumerable<T> All()
			{
				return _database.Query<T>("SELECT * FROM " + TableName);
			}
			public Task<IEnumerable<T>> AllAsync()
			{
				return _database.QueryAsync<T>("SELECT * FROM " + TableName);
			}

			static ConcurrentDictionary<Type, string[]> paramNameCache = new ConcurrentDictionary<Type, string[]>();

			//public List<string> AsParamNames(object o)
			//{
			//	return GetParamNames(o);
			//}
			internal static IEnumerable<string> GetParamNames(object o)
			{
				if (o is DynamicParameters)
				{
					return (o as DynamicParameters).ParameterNames.ToList();
				}

				string[] paramNames;
				var type = o.GetType();
				if (!paramNameCache.TryGetValue(type, out paramNames))
				{
					var list = new List<string>();
					foreach (var prop in type.GetProperties(BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public))
					{
						if (DbHelper.ExcludeDict.Contains(prop.Name))
							continue;
						var attribs = prop.GetCustomAttributes(typeof(IgnorePropertyAttribute), true);
						var attr = attribs.FirstOrDefault() as IgnorePropertyAttribute;
						if (attr == null || (attr != null && !attr.Value))
						{
							list.Add(prop.Name);
						}
					}
					paramNameCache[type] = paramNames = list.ToArray();
				}
				return paramNames;
			}
		}

	}

	internal class DbHelper
	{
		public readonly static HashSet<string> ExcludeDict = new HashSet<string>(new string[] {
			"DEX_ROW_TS",
			"DEX_ROW_ID",
		});
	}
}