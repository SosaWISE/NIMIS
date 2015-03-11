using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NXS.Data
{
	public abstract partial class Database<TDatabase> : IDisposable where TDatabase : Database<TDatabase>, new()
	{
		public partial class Table<T, TID>
		{
			/// <summary>
			/// Insert a row into the db asynchronously
			/// </summary>
			/// <param name="data">Either DynamicParameters or an anonymous type or concrete type</param>
			/// <returns></returns>
			public virtual async Task<TID> InsertAsync(dynamic data)
			{
				List<string> paramNames = GetParamNames((object)data);
				paramNames.Remove(PkName);

				string cols = string.Join(",", paramNames);
				string cols_params = string.Join(",", paramNames.Select(p => "@" + p));
				var sql = "SET NOCOUNT ON INSERT " + TableName + " (" + cols + ") VALUES (" + cols_params + ") SELECT SCOPE_IDENTITY()";

				return (await _database.QueryAsync<TID>(sql, (object)data).ConfigureAwait(false)).Single();
			}

			public virtual async Task<int> InsertNoIdAsync(dynamic data)
			{
				List<string> paramNames = GetParamNames((object)data);
				paramNames.Remove(PkName);

				string cols = string.Join(",", paramNames);
				string cols_params = string.Join(",", paramNames.Select(p => "@" + p));
				var sql = "SET NOCOUNT ON INSERT " + TableName + " (" + cols + ") VALUES (" + cols_params + ")";

				return (await _database.ExecuteAsync(sql, (object)data).ConfigureAwait(false));
			}

			/// <summary>
			/// Update a record in the DB asynchronously
			/// </summary>
			/// <param name="id"></param>
			/// <param name="data"></param>
			/// <returns></returns>
			public Task<int> UpdateAsync(TID id, dynamic data)
			{
				List<string> paramNames = GetParamNames((object)data);

				var builder = new StringBuilder();
				builder.Append("UPDATE ").Append(TableName).Append(" SET ");
				builder.AppendLine(string.Join(",", paramNames.Where(n => n != PkName).Select(p => p + "= @" + p)));
				builder.Append("WHERE " + PkName + " = @id");

				DynamicParameters parameters = new DynamicParameters(data);
				parameters.Add("id", id);

				return _database.ExecuteAsync(builder.ToString(), parameters);
			}

			/// <summary>
			/// Delete a record for the DB asynchronously
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

			public Task<IEnumerable<T>> AllAsync()
			{
				return _database.QueryAsync<T>("SELECT * FROM " + TableName);
			}
		}

		public Task<int> ExecuteAsync(string sql, dynamic param = null)
		{
			return _connection.ExecuteAsync(sql, param as object, _transaction, this._commandTimeout);
		}

		public Task<IEnumerable<T>> QueryAsync<T>(string sql, dynamic param = null)
		{
			return _connection.QueryAsync<T>(sql, param as object, _transaction, _commandTimeout);
		}

		public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "ID", int? commandTimeout = null)
		{
			return _connection.QueryAsync(sql, map, param as object, transaction, buffered, splitOn);
		}

		public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "ID", int? commandTimeout = null)
		{
			return _connection.QueryAsync(sql, map, param as object, transaction, buffered, splitOn);
		}

		public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "ID", int? commandTimeout = null)
		{
			return _connection.QueryAsync(sql, map, param as object, transaction, buffered, splitOn);
		}

		public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "ID", int? commandTimeout = null)
		{
			return _connection.QueryAsync(sql, map, param as object, transaction, buffered, splitOn);
		}

		public Task<IEnumerable<dynamic>> QueryAsync(string sql, dynamic param = null)
		{
			return _connection.QueryAsync(sql, param as object, _transaction);
		}

		public Task<SqlMapper.GridReader> QueryMultipleAsync(string sql, dynamic param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
		{
			return SqlMapper.QueryMultipleAsync(_connection, sql, param, transaction, commandTimeout, commandType);
		}
	}
}
