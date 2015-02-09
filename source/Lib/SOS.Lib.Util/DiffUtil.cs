using System;
using System.Collections.Generic;
using System.Reflection;

namespace SOS.Lib.Util
{
	public static class DiffUtil
	{
		public static DiffedValueList DiffInstances<T>(T left, T right)
		{
			return DiffInstances(left, right, new DiffList());
		}

		public static DiffedValueList DiffInstances<T>(T left, T right, DiffList diffList)
		{
			var diffedValues = new DiffedValueList();

			Type iType = typeof (T);

			PropertyInfo[] cachedProps = iType.GetProperties();
			foreach (PropertyInfo pi in cachedProps)
			{
				if (diffList.DiffValue(pi.Name))
				{
					object lValue = pi.GetValue(left, null);
					object rValue = pi.GetValue(right, null);

					if (!AreEqual(lValue, rValue))
					{
						diffedValues.Add(new DiffedValue
						                 	{Type = DiffedValueTypes.Property, Name = pi.Name, Left = lValue, Right = rValue,});
					}
				}
			}

			FieldInfo[] cachedFields = iType.GetFields();
			foreach (FieldInfo fi in cachedFields)
			{
				if (diffList.DiffValue(fi.Name))
				{
					object lValue = fi.GetValue(left);
					object rValue = fi.GetValue(right);

					if (!AreEqual(lValue, rValue))
					{
						diffedValues.Add(new DiffedValue {Type = DiffedValueTypes.Field, Name = fi.Name, Left = lValue, Right = rValue,});
					}
				}
			}

			return diffedValues;
		}

		public static bool AreEqual(object left, object right)
		{
			//retun true if any statement is true
			return
				(left == null && right == null) // both are null
				|| (left != null && left.Equals(right)) //left is not null and equals right
				|| (right != null && right.Equals(left)) //right is not null and equals left
				;
		}
	}

	public class DiffList
	{
		private readonly Dictionary<string, string> _dict;

		public DiffList()
		{
			_dict = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
		}

		public void Add(string valueToDiff)
		{
			if (!_dict.ContainsKey(valueToDiff))
			{
				_dict.Add(valueToDiff, valueToDiff);
			}
		}

		public bool Remove(string valueToDiff)
		{
			return _dict.Remove(valueToDiff);
		}

		public bool DiffValue(string valueToDiff)
		{
			return
				_dict.Count == 0 //diff if empty
				|| _dict.ContainsKey(valueToDiff) //or value exists in dictionary
				;
		}
	}

	public class DiffedValueList : List<DiffedValue>
	{
	}

	public class DiffedValue
	{
		public DiffedValueTypes Type { get; set; }
		public string Name { get; set; }
		public object Left { get; set; }
		public object Right { get; set; }

		public override string ToString()
		{
			return string.Format("{0}(1): {2} != {3}", Name, Type, Left, Right);
		}
	}

	public enum DiffedValueTypes
	{
		Property,
		Field,
	}
}