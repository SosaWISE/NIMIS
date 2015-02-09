using System.Collections.Generic;
using SOS.Lib.Util;

namespace NXS.Framework.Wpf.Mvvm.Models
{
	public class MenuItem : IHierarchyItem
	{
		public string Label { get; set; }
		public string ToolTip { get; set; }
		public string ActionName { get; set; }
		public bool IsOverrideable { get; set; }
		public bool IsVisible { get; set; }

		List<MenuItem> _children;
		public List<MenuItem> Children
		{
			get { return _children; }
		}

		public MenuItem()
		{
			_children = new List<MenuItem>();
			this.IsVisible = true;
		}

		#region IHierachyItem Members

		public int ID { get; set; }
		public int? ParentID { get; set; }

		public void AddChild(IHierarchyItem item)
		{
			_children.Add((MenuItem)item);
		}
		public void RemoveChildren(bool recursive)
		{
			if (recursive) {
				foreach (MenuItem child in _children) {
					child.RemoveChildren(recursive);
				}
			}
			_children.Clear();
		}
		public void SortChildren(bool recursive)
		{
			if (recursive) {
				foreach (MenuItem child in _children) {
					child.SortChildren(recursive);
				}
			}
			_children.Sort();
		}

		#endregion

		#region IEnumerable
		//public IEnumerator<IHierachyItem> GetEnumerator()
		//{
		//    throw new NotImplementedException();
		//}
		//System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		//{
		//    throw new NotImplementedException();
		//}
		#endregion //IEnumerable

		public override string ToString()
		{
			return string.Format("{2} - ParentID:{0}, ID:{1}", ((ParentID == null) ? "null" : ParentID.Value.ToString()), ID, Label);
		}

		public MenuItem FindChildByActionName(string actionName)
		{
			MenuItem result = null;

			if (_children != null && _children.Count > 0)
			{
				foreach (MenuItem currChild in _children)
				{
					if (StringUtility.AreEqual(currChild.ActionName, actionName, false))
					{
						result = currChild;
						break;
					}
					else
					{
						result = currChild.FindChildByActionName(actionName);
						if (result != null)
						{
							break;
						}
					}
				}
			}

			return result;
		}
	}
}
