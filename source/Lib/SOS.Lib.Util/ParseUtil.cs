using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SOS.Lib.Util
{
	public static class ParseUtil
	{
		public static List<T> ToListFromDelimitedFile<T>(string filePath, string delimiter,
		                                                 PropertyColumnMapList headerMapping) where T : new()
		{
			var result = new List<T>();

			string line;

			using (TextReader reader = new StreamReader(filePath))
			{
				line = reader.ReadLine();
				if (line != null)
				{
					//read first line into headers
					string[] columnHeaders = line.Split(new[] {delimiter}, StringSplitOptions.None);

					var headers = new PropertyColumnMapList();
					foreach (string header in columnHeaders)
					{
						PropertyColumnMap pcm = headerMapping.FirstOrDefault(a => string.Compare(a.ColumnName, header, true) == 0);
						if (pcm != null)
						{
							headers.Add(pcm);
						}
						else
						{
							throw new Exception(string.Format("'{0}' was not found in header mapping list", header));
						}
					}

					int length = headers.Count;
					if (length > 0)
					{
						while ((line = reader.ReadLine()) != null)
						{
							string[] rowValues = line.Split(new[] {delimiter}, StringSplitOptions.None);

							var item = new T();
							Load(headers, rowValues, item);
							result.Add(item);
						}
					}
				}
			}

			return result;
		}

		public static void Load<T>(PropertyColumnMapList headers, string[] rowValues, T item)
		{
			if (headers.Count != rowValues.Length)
			{
				throw new Exception("Header length doesn't match RowValue length");
			}

			Type iType = typeof (T);

			PropertyInfo[] cachedProps = iType.GetProperties();
			FieldInfo[] cachedFields = iType.GetFields();

			PropertyInfo currentProp;
			FieldInfo currentField = null;

			for (int i = 0; i < headers.Count; i++)
			{
				string pName = headers[i].PropertyName;

				currentProp = cachedProps.SingleOrDefault(x => x.Name.Equals(pName, StringComparison.InvariantCultureIgnoreCase));

				bool isProp = (currentProp != null);

				//if it's not a property, see if it's a Field
				if (!isProp)
				{
					currentField = cachedFields.SingleOrDefault(x => x.Name.Equals(pName, StringComparison.InvariantCultureIgnoreCase));
				}

				string value = rowValues[i];
				if (!string.IsNullOrEmpty(value) && (isProp || currentField != null))
				{
					Type valueType = isProp ? currentProp.PropertyType : currentField.FieldType;
					object obj;

					//convert value
					if (valueType == typeof (string))
					{
						obj = value;
					}
					if (valueType == typeof (SByte))
					{
						obj = (value == "1");
					}
					else if (currentProp.PropertyType == typeof (Guid))
					{
						obj = new Guid(value);
					}
					else
					{
						obj = ConversionHelper.ChangeType(value, valueType);
					}

					//set value
					if (isProp)
					{
						currentProp.SetValue(item, obj, null);
					}
					else
					{
						currentField.SetValue(item, obj);
					}
				}
			}
		}
	}

	public class PropertyColumnMapList : List<PropertyColumnMap>
	{
	}

	public class PropertyColumnMap
	{
		public string PropertyName { get; set; }
		public string ColumnName { get; set; }
	}
}