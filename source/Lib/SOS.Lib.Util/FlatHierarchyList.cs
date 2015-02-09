using System;
using System.Collections.Generic;

namespace SOS.Lib.Util
{
	/// <summary>
	/// Used for createing a list of items in hierarchy
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="LT"></typeparam>
	public class FlatHierarchyList<ItemType, ListType>
		where ItemType : IFlatHierarchy<ItemType>
		where ListType : IList<ItemType>, IFlatHierarchyList<ItemType, ListType>, new()
	{
		public int Count { get; private set; }

		/// <summary>
		/// Creates a list of all items in hierarchy starting from parent. Assumes collection contains items with unique IDs. Max Levels is around 6500 and then you get stack overflow.
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="collection"></param>
		/// <returns></returns>
		public ListType GetFlatTree(ItemType parent, ListType collection, bool recursive)
		{
			Count = 0;

			var dict = new HierarchyDictionary<ItemType, ListType>();
			var tempDict = new TempHierarchyDictionary<ItemType, ListType>();

			dict.Add(parent);

			GetFlatTree_Helper(dict, tempDict, collection, recursive, 0);

			return dict.GetCollection();
		}

		private void GetFlatTree_Helper(HierarchyDictionary<ItemType, ListType> dict,
		                                TempHierarchyDictionary<ItemType, ListType> tempDict, ListType collection,
		                                bool recursive, int level)
		{
			foreach (ItemType item in collection)
			{
				Count++;
				if (dict.ContainsID(item.ParentID))
				{
					dict.Add(item);

					int key = item.ID;
					//check if key is waiting in temp for a parent
					if (tempDict.ContainsID(key))
					{
						//get all children of current item
						ListType tempCollection = tempDict.GetAndRemoveCollection(key);
						//add the children
						GetFlatTree_Helper(dict, tempDict, tempCollection, false, level + 1);
					}
				}
				else
				{
					//code shouldn't get here if we're recursing, but just in case
					if (level != 0) throw new Exception("Can only add to tempDict at level zero");

					if (recursive)
					{
						//add to temp
						tempDict.Add(item);
					}
				}
			}
		}
	}

	public interface IFlatHierarchy<ItemType>
		where ItemType : IFlatHierarchy<ItemType>
	{
		int ID { get; set; }
		int? ParentID { get; set; }
		int Level { get; set; }
	}

	public interface IFlatHierarchyList<ItemType, ListType>
		where ItemType : IFlatHierarchy<ItemType>
		where ListType : IList<ItemType>, IFlatHierarchyList<ItemType, ListType>, new()
	{
	}

	public class HierarchyDictionary<ItemType, ListType>
		where ItemType : IFlatHierarchy<ItemType>
		where ListType : IList<ItemType>, IFlatHierarchyList<ItemType, ListType>, new()
	{
		private readonly Dictionary<int, ItemType> _dict;

		public HierarchyDictionary()
		{
			_dict = new Dictionary<int, ItemType>();
		}

		public void Add(ItemType item)
		{
			if (item.ParentID != null)
			{
				if (ContainsID(item.ParentID))
				{
					item.Level = _dict[item.ParentID.Value].Level + 1;
				}
			}
			_dict.Add(item.ID, item);
		}

		public bool ContainsItem(ItemType item)
		{
			return _dict.ContainsKey(item.ID);
		}

		public bool ContainsID(int? id)
		{
			return id != null && _dict.ContainsKey(id.Value);
		}

		public ListType GetCollection()
		{
			var result = new ListType();
			foreach (var kvp in _dict)
			{
				result.Add(kvp.Value);
			}
			return result;
		}

		//#region Nested Classes
		//#endregion //Nested Classes
	}

	public class TempHierarchyDictionary<ItemType, ListType>
		where ItemType : IFlatHierarchy<ItemType>
		where ListType : IList<ItemType>, IFlatHierarchyList<ItemType, ListType>, new()
	{
		private readonly Dictionary<int, ItemType> _dict1;
		private readonly Dictionary<int, ListType> _dictCollection;

		public TempHierarchyDictionary()
		{
			_dict1 = new Dictionary<int, ItemType>();
			_dictCollection = new Dictionary<int, ListType>();
		}

		public void Add(ItemType item)
		{
			if (item.ParentID != null)
			{
				int key = item.ParentID.Value;

				bool inCollection = _dictCollection.ContainsKey(key);
				bool inSingle = _dict1.ContainsKey(key);

				if (inCollection || inSingle)
				{
					ListType collection;
					if (inCollection)
					{
						//set collection
						collection = _dictCollection[key];
					}
					else
					{
						//create new collection
						collection = new ListType();
						//add from single
						collection.Add(_dict1[key]);
						if (!_dict1.Remove(key))
						{
							throw new Exception("Single contains key but didn't remove key");
						}

						//add collection
						_dictCollection.Add(key, collection);
					}
					//add passed in
					collection.Add(item);
				}
				else
				{
					_dict1.Add(key, item);
				}
			}
		}

		public ListType GetAndRemoveCollection(int key)
		{
			ListType collection;
			if (_dictCollection.ContainsKey(key))
			{
				collection = _dictCollection[key];
				if (!_dictCollection.Remove(key))
				{
					throw new Exception("Collection contains key but didn't remove key");
				}
			}
			else if (_dict1.ContainsKey(key))
			{
				collection = new ListType();
				collection.Add(_dict1[key]);
				if (!_dict1.Remove(key))
				{
					throw new Exception("Single contains key but didn't remove key");
				}
			}
			else
			{
				throw new NotSupportedException();
			}
			return collection;
		}

		public bool ContainsID(int key)
		{
			return _dictCollection.ContainsKey(key) || _dict1.ContainsKey(key);
		}
	}
}