using System.Collections.Generic;

namespace SOS.Data
{
	public static class RecordSorter
	{
		public static void Move(IList<ISortableRecord> items, ISortableRecord target, bool up)
		{
			if (up) {
				MoveTargetUp(items, target);
			}
			else {
				MoveTargetDown(items, target);
			}
		}

		public static void MoveTargetDown(IList<ISortableRecord> items, ISortableRecord target)
		{
			for (int i = 0; i < items.Count; i++)
			{
				ISortableRecord currItem = items[i];
				if (currItem == target && i < items.Count - 1)
				{
					currItem.SortOrder = i + 2;
					items[i + 1].SortOrder = i + 1;
					i += 1;
				}
				else
				{
					currItem.SortOrder = i + 1;
				}
			}
		}

		public static void MoveTargetUp(IList<ISortableRecord> items, ISortableRecord target)
		{
			for (int i = 0; i < items.Count; i++)
			{
				ISortableRecord currItem = items[i];
				if (currItem == target && i > 0)
				{
					currItem.SortOrder = i;
					items[i - 1].SortOrder = i + 1;
				}
				else
				{
					currItem.SortOrder = i + 1;
				}
			}
		}
	}
}