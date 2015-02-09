using System.Collections.Generic;

namespace SOS.Lib.Util
{
	/// <summary>
	/// A dictionary nested within a dictionary. Takes two keys to get the value.
	/// The first keys gets the inner dictionary and the second the value in the inner dictionary
	/// </summary>
	/// <typeparam name="TFirstKey"></typeparam>
	/// <typeparam name="TSecondKey"></typeparam>
	/// <typeparam name="TValue"></typeparam>
	public class DoubleKeyDictionary<TFirstKey, TSecondKey, TValue>
	{
		protected Dictionary<TFirstKey, Dictionary<TSecondKey, TValue>> _dict;

		//public IEnumerable<TSecondKey> GetSecondKeys(TFirstKey firstKey)
		//{
		//    Dictionary<TSecondKey, TValue> secondDict;
		//    if (TryGetSecondDictionary(firstKey, out secondDict)) {
		//        foreach (TSecondKey key in secondDict.Keys) {
		//            yield return key;
		//        }
		//    }
		//    yield break;
		//}

		public DoubleKeyDictionary()
		{
			_dict = new Dictionary<TFirstKey, Dictionary<TSecondKey, TValue>>();
		}

		public IEnumerable<TFirstKey> FirstKeys
		{
			get { return _dict.Keys; }
		}

		public TValue this[TFirstKey firstKey, TSecondKey secondKey]
		{
			get { return _dict[firstKey][secondKey]; }
			set { _dict[firstKey][secondKey] = value; }
		}

		/// <summary>
		/// Tries adding the value. If the secondKey already exists the value won't be overwritten
		/// </summary>
		/// <param name="firstKey"></param>
		/// <param name="secondKey"></param>
		/// <param name="value"></param>
		protected bool Add(TFirstKey firstKey, TSecondKey secondKey, TValue value, bool tryAdd)
		{
			Dictionary<TSecondKey, TValue> tempDict;
			if (!_dict.TryGetValue(firstKey, out tempDict))
			{
				tempDict = new Dictionary<TSecondKey, TValue>();
				_dict.Add(firstKey, tempDict);
			}

			//only add if dict doesn't contain key or tryAdd is false
			if (!tempDict.ContainsKey(secondKey) || !tryAdd)
			{
				tempDict.Add(secondKey, value);
				return true;
			}
			return false;
		}

		public void Add(TFirstKey firstKey, TSecondKey secondKey, TValue value)
		{
			Add(firstKey, secondKey, value, false);
		}

		public bool TryAdd(TFirstKey firstKey, TSecondKey secondKey, TValue value)
		{
			return Add(firstKey, secondKey, value, true);
		}

		public bool Contains(TFirstKey firstKey, TSecondKey secondKey)
		{
			Dictionary<TSecondKey, TValue> tempDict;
			if (_dict.TryGetValue(firstKey, out tempDict))
			{
				return tempDict.ContainsKey(secondKey);
			}
			return false;
		}

		public bool TryGetSecondDictionary(TFirstKey firstKey, out Dictionary<TSecondKey, TValue> secondDict)
		{
			return _dict.TryGetValue(firstKey, out secondDict);
		}

		public bool TryGetValue(TFirstKey firstKey, TSecondKey secondKey, out TValue value)
		{
			Dictionary<TSecondKey, TValue> secondDict;
			if (_dict.TryGetValue(firstKey, out secondDict))
			{
				if (secondDict.TryGetValue(secondKey, out value))
				{
					return true;
				}
			}
			value = default(TValue);
			return false;
		}
	}
}