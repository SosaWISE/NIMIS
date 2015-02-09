namespace SOS.Lib.Util
{
	public interface IHierarchyItem
	{
		int ID { get; set; }
		int? ParentID { get; set; }

		void AddChild(IHierarchyItem item);
		void RemoveChildren(bool recursive);
		void SortChildren(bool recursive);
	}
}