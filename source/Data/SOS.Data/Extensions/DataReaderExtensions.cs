using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using SOS.Lib.Util;

namespace SOS.Data.Extensions
{
	public static class DataReaderExtensions
	{
		public static T ToSingle<T>(this IDataReader rdr) where T : new()
		{
			using (rdr) {
				while (rdr.Read()) {
					T item = new T();
					rdr.Load(item);
					return item;
				}
				return default(T);
			}
		}

		/// <summary>
		/// Creates a typed list from an IDataReader, using reflection and column name/property matching
		/// </summary>
		public static List<T> ToList<T>(this IDataReader rdr) where T : new()
		{
			List<T> result = new List<T>();
			Type iType = typeof(T);

			using (rdr) {
				//set the values        
				while (rdr.Read()) {
					T item = new T();
					rdr.Load(item);
					result.Add(item);
				}
			}
			return result;
		}
		/// <summary>
		/// Creates a typed list from an IDataReader, using reflection and column name/property matching
		/// </summary>
		/// <typeparam name="T">Base type to be returned</typeparam>
		/// <param name="rdr">IDataReader</param>
		/// <param name="createInstance">Function used to create an instance</param>
		public static List<T> ToList<T>(this IDataReader rdr, Func<IDataReader, T> createInstance)
		{
			List<T> result = new List<T>();

			using (rdr) {
				//set the values        
				while (rdr.Read()) {
					T item = createInstance(rdr);
					rdr.Load(item, item.GetType());
					result.Add(item);
				}
			}
			return result;
		}

		/// <summary>
		/// Coerces an IDataReader to try and load an object using name/property matching
		/// </summary>
		public static void Load<T>(this IDataReader rdr, T item)
		{
			rdr.Load(item, typeof(T));
		}
		public static void Load(this IDataReader rdr, object item)
		{
			rdr.Load(item, item.GetType());
		}
		private static void Load(this IDataReader rdr, object item, Type iType)
		{
			PropertyInfo[] cachedProps = iType.GetProperties();
			FieldInfo[] cachedFields = iType.GetFields();

			PropertyInfo currentProp;
			FieldInfo currentField = null;

			for (int i = 0; i < rdr.FieldCount; i++) {

				string pName = rdr.GetName(i);
				currentProp = cachedProps.SingleOrDefault(x => x.Name.Equals(pName, StringComparison.InvariantCultureIgnoreCase));

				//if the property is null, likely it's a Field
				if (currentProp == null) {
					currentField = cachedFields.SingleOrDefault(x => x.Name.Equals(pName, StringComparison.InvariantCultureIgnoreCase));
				}

				if (currentProp != null && !DBNull.Value.Equals(rdr.GetValue(i))) {
					Type valueType = rdr.GetValue(i).GetType();
					if (valueType == typeof(SByte)) {
						currentProp.SetValue(item, (rdr.GetValue(i).ToString() == "1"), null);
					}
					else if (currentProp.PropertyType == typeof(Guid)) {
						currentProp.SetValue(item, rdr.GetGuid(i), null);
					}
					else {
						currentProp.SetValue(item, ConvertHelper.ChangeType(rdr.GetValue(i), valueType), null);
					}
				}
				else if (currentField != null && !DBNull.Value.Equals(rdr.GetValue(i))) {
					Type valueType = rdr.GetValue(i).GetType();
					if (valueType == typeof(SByte)) {
						currentField.SetValue(item, (rdr.GetValue(i).ToString() == "1"));
					}
					else if (currentField.FieldType == typeof(Guid)) {
						currentField.SetValue(item, rdr.GetGuid(i));
					}
					else {
						currentField.SetValue(item, ConvertHelper.ChangeType(rdr.GetValue(i), valueType));
					}
				}
			}
		}
	}
}
