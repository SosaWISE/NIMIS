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

		public void BeginTransaction(IsolationLevel isolation = IsolationLevel.ReadCommitted)
		{
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
			return SqlMapper.Execute(_connection, sql, param as object, _transaction, commandTimeout: this._commandTimeout);
		}

		public IEnumerable<T> Query<T>(string sql, dynamic param = null, bool buffered = true)
		{
			return SqlMapper.Query<T>(_connection, sql, param as object, _transaction, buffered, _commandTimeout);
		}

		public IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "ID", int? commandTimeout = null)
		{
			return SqlMapper.Query(_connection, sql, map, param as object, transaction, buffered, splitOn);
		}

		public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "ID", int? commandTimeout = null)
		{
			return SqlMapper.Query(_connection, sql, map, param as object, transaction, buffered, splitOn);
		}

		public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "ID", int? commandTimeout = null)
		{
			return SqlMapper.Query(_connection, sql, map, param as object, transaction, buffered, splitOn);
		}

		public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "ID", int? commandTimeout = null)
		{
			return SqlMapper.Query(_connection, sql, map, param as object, transaction, buffered, splitOn);
		}

		public IEnumerable<dynamic> Query(string sql, dynamic param = null, bool buffered = true)
		{
			return SqlMapper.Query(_connection, sql, param as object, _transaction, buffered);
		}

		public Dapper.SqlMapper.GridReader QueryMultiple(string sql, dynamic param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
		{
			return SqlMapper.QueryMultiple(_connection, sql, param, transaction, commandTimeout, commandType);
		}

		public void Dispose()
		{
			if (_connection.State != ConnectionState.Closed)
			{
				if (_transaction != null)
				{
					_transaction.Rollback();
				}

				_connection.Close();
				_connection = null;
			}
		}

		public partial class Table<T, TID> : ITable
		{
			protected Database<TDatabase> _database;
			internal string _name;
			internal string _pkName;
			protected readonly string _aliasNoDot;
			protected readonly string _alias;

			public Table(Database<TDatabase> database) : this(database, null, null, null) { }
			public Table(Database<TDatabase> database, string alias, string name, string pkName = "ID")
			{
				_aliasNoDot = alias;
				_alias = string.IsNullOrEmpty(alias) ? "" : (alias + ".");

				_database = database;
				_name = name;
				_pkName = pkName;
			}

			public string Alias { get { return _aliasNoDot; } }
			public string TableName { get { return _name; } }
			public string PkName { get { return _pkName; } }
			public string Star { get { return _alias + "*"; } }

			/// <summary>
			/// Insert a row into the db
			/// </summary>
			/// <param name="data">Either DynamicParameters or an anonymous type or concrete type</param>
			/// <returns></returns>
			public virtual TID Insert(dynamic data)
			{
				List<string> paramNames = GetParamNames((object)data);
				paramNames.Remove(PkName);

				string cols = string.Join(",", paramNames);
				string cols_params = string.Join(",", paramNames.Select(p => "@" + p));
				var sql = "SET NOCOUNT ON INSERT " + TableName + " (" + cols + ") VALUES (" + cols_params + ") SELECT SCOPE_IDENTITY()";

				return _database.Query<TID>(sql, data).Single();
			}

			/// <summary>
			/// Update a record in the DB
			/// </summary>
			/// <param name="id"></param>
			/// <param name="data"></param>
			/// <returns></returns>
			public int Update(TID id, dynamic data)
			{
				List<string> paramNames = GetParamNames((object)data);
				paramNames.Remove(PkName);

				var builder = new StringBuilder();
				builder.Append("UPDATE ").Append(TableName).Append(" SET ");
				builder.AppendLine(string.Join(",", paramNames.Select(p => p + "= @" + p)));
				builder.Append("WHERE " + PkName + " = @id");

				DynamicParameters parameters = new DynamicParameters(data);
				parameters.Add("id", id);

				return _database.Execute(builder.ToString(), parameters);
			}

			/// <summary>
			/// Delete a record for the DB
			/// </summary>
			/// <param name="id"></param>
			/// <returns></returns>
			public bool Delete(TID id)
			{
				return _database.Execute("DELETE FROM " + TableName + " WHERE " + PkName + " = @id", new { id }) > 0;
			}

			/// <summary>
			/// Grab a record with a particular ID from the DB 
			/// </summary>
			/// <param name="id"></param>
			/// <returns></returns>
			public T ById(TID id)
			{
				return _database.Query<T>("SELECT * FROM " + TableName + " WHERE " + PkName + " = @id", new { id }).FirstOrDefault();
			}

			public virtual T First()
			{
				return _database.Query<T>("SELECT TOP(1) * FROM " + TableName).FirstOrDefault();
			}

			public IEnumerable<T> All()
			{
				return _database.Query<T>("SELECT * FROM " + TableName);
			}

			static ConcurrentDictionary<Type, List<string>> paramNameCache = new ConcurrentDictionary<Type, List<string>>();

			public List<string> AsParamNames(object o)
			{
				return GetParamNames(o);
			}
			internal static List<string> GetParamNames(object o)
			{
				if (o is DynamicParameters)
				{
					return (o as DynamicParameters).ParameterNames.ToList();
				}

				List<string> paramNames;
				var type = o.GetType();
				if (!paramNameCache.TryGetValue(type, out paramNames))
				{
					paramNames = new List<string>();
					foreach (var prop in type.GetProperties(BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public))
					{
						var attribs = prop.GetCustomAttributes(typeof(IgnorePropertyAttribute), true);
						var attr = attribs.FirstOrDefault() as IgnorePropertyAttribute;
						if (attr == null || (attr != null && !attr.Value))
						{
							paramNames.Add(prop.Name);
						}
					}
					paramNameCache[type] = paramNames;
				}
				return paramNames;
			}
		}

	}
}