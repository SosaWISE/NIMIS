using System;
using System.Collections.Generic;
using System.Data;
using SubSonic;

namespace SOS.Data.Extensions
{
	public static class StoredProcedureExtensions
	{
		public static DataTable ToTable(this StoredProcedure sp)
		{
			DataSet resultSet = sp.GetDataSet();
			if (resultSet.Tables.Count > 0)
				return resultSet.Tables[0];

			// Default path of execution
			return new DataTable();
		}

		public static TList ToCollectionView<T, TList>(this StoredProcedure sp)
			where T : ReadOnlyRecord<T>, new()
			where TList : ReadOnlyList<T, TList>, new()
		{
			var oResult = new TList();
			oResult.LoadAndCloseReader(sp.GetReader());
			return oResult;
		}

		public static TList ToCollection<T, TList>(this StoredProcedure sp)
			where T : ActiveRecord<T>, new()
			where TList : ActiveList<T, TList>, new()
		{
			var result = new TList();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
		public static T ToItem<T>(this StoredProcedure sp)
			where T : ActiveRecord<T>, new()
		{
			var result = new T();
			result.LoadAndCloseReader(sp.GetReader());
			return result.IsLoaded ? result : null;
		}

		public static TList ToReadOnlyCollection<T, TList>(this StoredProcedure sp)
			where T : ReadOnlyRecord<T>, new()
			where TList : ReadOnlyList<T, TList>, new()
		{
			var result = new TList();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
		public static T ToReadOnlyItem<T>(this StoredProcedure sp)
			where T : ReadOnlyRecord<T>, new()
		{
			var result = new T();
			result.LoadAndCloseReader(sp.GetReader());
			return result.IsLoaded ? result : null;
		}

		public static T ExecuteScalar<T>(this StoredProcedure sp)
		{
			return ExecuteScalar<T>(sp.ExecuteScalar());
		}
		public static T ExecuteScalar<T>(object obj)
		{
			return Convert.IsDBNull(obj) ? default(T) : (T)obj;
		}

		/// <summary>
		/// Don't use a Subsonic object. Creates a typed list from an IDataReader, using reflection and column name/property matching.
		/// </summary>
		public static List<T> ToList<T>(this StoredProcedure sp) where T : new()
		{
			return sp.GetReader().ToList<T>();
		}
		public static T ToSingle<T>(this StoredProcedure sp) where T : new()
		{
			return sp.GetReader().ToSingle<T>();
		}
	}
}
