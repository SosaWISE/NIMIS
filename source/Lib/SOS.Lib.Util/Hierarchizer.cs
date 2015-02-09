using System.Collections.Generic;

namespace SOS.Lib.Util
{
	public static class Hierarchizer<TItem, TResultList>
		where TItem : IHierarchyItem
		where TResultList : List<TItem>, new()
	{
		public static TResultList BuildHierarchy(IList<TItem> list)
		{
			////clear current hierarchy
			//foreach (TItem item in list) {
			//    item.RemoveChildren(true);
			//}

			var result = new TResultList();

			var nodeDict = new Dictionary<int, TItem>();
			var orphanList = new List<TItem>();

			foreach (TItem curr in list)
			{
				curr.RemoveChildren(false);

				//add to node dictionary
				nodeDict.Add(curr.ID, curr);

				if (curr.ParentID == null)
				{
					// Root node, just add it to the result
					result.Add(curr);
				}
				else if (nodeDict.ContainsKey(curr.ParentID.Value))
				{
					// Look for the parent
					nodeDict[curr.ParentID.Value].AddChild(curr);
				}
				else
				{
					// Parent not yet found, add to the list of orphans
					orphanList.Add(curr);
				}
			}

			// Clean up orphans
			foreach (TItem curr in orphanList)
			{
				if (curr.ParentID == null)
				{
					result.Add(curr);
				}
				else if (nodeDict.ContainsKey(curr.ParentID.Value))
				{
					nodeDict[curr.ParentID.Value].AddChild(curr);
				}
				else
				{
					// Don't do anything with orphans that don't have a parent in the list
				}
			}

			////// Sort the result
			////result.SortHierarchy();
			//result.Sort();
			//foreach (TItem curr in result) {
			//    curr.SortChildren(true);
			//}

			return result;
			//}
		}
	}
}