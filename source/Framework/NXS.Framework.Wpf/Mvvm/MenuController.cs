using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using NXS.Framework.Wpf.Mvvm.Models;
using NXS.Framework.Wpf.Mvvm.Security;
using NXS.Framework.Wpf.ParentChildService;
using SOS.Lib.Util;

namespace NXS.Framework.Wpf.Mvvm
{
	public class MenuController : INotifyPropertyChanged
	{
		#region Fields

		bool _isLoadingData;

		#endregion //Fields

		#region Properties

		public ObservableCollection<MenuNode> MenuNodes { get; private set; }

		readonly static PropertyChangedEventArgs isLoadingDataChangeArgs = ObservableHelper.CreateArgs<MenuController>(a => a.IsLoadingData);
		/// <summary>
		/// Returns true if the community member data is
		/// currently being loaded from a data source.
		/// </summary>
		public bool IsLoadingData
		{
			get { return _isLoadingData; }
			private set
			{
				if (value.Equals(_isLoadingData))
					return;

				_isLoadingData = value;
				OnPropertyChanged(isLoadingDataChangeArgs);
			}
		}

		private List<MenuItem> HierarchyMenuList { get; set; }

		#endregion //Properties

		#region .ctor

		public MenuController()
		{
			this.MenuNodes = new ObservableCollection<MenuNode>();
		}

		#endregion //.ctor

		#region Public Methods

		public void BuildMenu(object owner, Func<IList<MenuItem>> getList, Func<object, string, Predicate<InvokeActionArgs>, OwnedAbstractAction> getVirtualActionFromActionName)
		{
			if (getList == null)
				throw new ArgumentNullException("getList");

			if (getVirtualActionFromActionName == null)
				throw new ArgumentNullException("getVirtualActionFromActionName");

			if (this.IsLoadingData) {
				throw new Exception("Menu is currently loading");
			}

			ClearMenu();
			this.IsLoadingData = true;

			IList<MenuItem> menuList = getList();

			BuildMenuHierarchy(owner, menuList, getVirtualActionFromActionName, null);
		}
		public void BuildMenuAsync(object owner, Func<IList<MenuItem>> getList, Func<object, string, Predicate<InvokeActionArgs>, OwnedAbstractAction> getVirtualActionFromActionName, Action onCompleted)
		{
			if (getList == null)
				throw new ArgumentNullException("getList");

			if (getVirtualActionFromActionName == null)
				throw new ArgumentNullException("getVirtualActionFromActionName");

			if (this.IsLoadingData) {
				throw new Exception("Menu is currently loading");
			}

			ClearMenu();
			this.IsLoadingData = true;

			ObservableCollection<MenuItem> menuList = new ObservableCollection<MenuItem>();

			// Create the data source and begin a call to fetch and transform objects
			AsyncHelper.LoadDataAsync<MenuItem>(
					() => getList(),
					menuList,
					() => BuildMenuHierarchy(owner, menuList, getVirtualActionFromActionName, onCompleted),
					MenuLoadError
				);
		}

		public MenuItem FindItemByActionName(string actionName)
		{
			MenuItem result = null;

			if (this.HierarchyMenuList != null)
			{
				foreach (MenuItem curr in this.HierarchyMenuList)
				{
					if (StringUtility.AreEqual(curr.ActionName, actionName, false))
					{
						result = curr;
						break;
					}
					else
					{
						result = curr.FindChildByActionName(actionName);
						if (result != null)
						{
							break;
						}
					}
				}
			}

			return result;
		}

		#endregion //Public Methods

		#region Private Methods

		private void BuildMenuHierarchy(object owner, IList<MenuItem> menuList, Func<object, string, Predicate<InvokeActionArgs>, OwnedAbstractAction> getVirtualActionFromActionName, Action onCompleted)
		{
			//structure menu
			this.HierarchyMenuList = Hierarchizer<MenuItem, List<MenuItem>>.BuildHierarchy(menuList);

			//build menu nodes from structured menu items
			List<MenuNode> menuNodeList = GetMenuNodeList(owner, this.HierarchyMenuList, getVirtualActionFromActionName);

			//add top level to MenuNodes
			foreach (MenuNode node in menuNodeList) {

				MenuNodes.Add(node);
			}

			CleanupMenu(MenuNodes);

			this.IsLoadingData = false;

			if (onCompleted != null) {
				onCompleted();
			}
		}
		private static void CleanupMenu(ICollection<MenuNode> nodeCollection)
		{
			if (nodeCollection == null || nodeCollection.Count == 0) return;

			//remove nodes that don't have a command and don't have children

			List<MenuNode> removeNodes = new List<MenuNode>();
			foreach (MenuNode node in nodeCollection) {
				if (node.Children.Count == 0) {
					if (node.Command == null) {
						//remove me please, i do nothing. assolutamente niente.
						removeNodes.Add(node);
					}
				}
				else {
					CleanupMenu(node.Children);
				}
			}

			//remove nodes in list
			foreach (MenuNode item in removeNodes) {
				nodeCollection.Remove(item);
			}
		}
		private List<MenuNode> GetMenuNodeList(object owner, List<MenuItem> menuList, Func<object, string, Predicate<InvokeActionArgs>, OwnedAbstractAction> getVirtualActionFromActionName)
		{
			List<MenuNode> menuNodeList = new List<MenuNode>();

			if (menuList != null) {

				//recursively add menu nodes
				foreach (MenuItem item in menuList) {

					if (item.IsVisible)
					{
						OwnedAbstractAction ownedAbstractAction = getVirtualActionFromActionName(owner, item.ActionName, null);

						MenuNode node = new MenuNode()
						{
							Label = item.Label,
							ToolTip = item.ToolTip,
							Command = ownedAbstractAction,
							IsVisible = item.IsVisible
						};
						//recursively get children
						node.Children = GetMenuNodeList(owner, item.Children, getVirtualActionFromActionName);

						menuNodeList.Add(node);
					}
				}
			}

			return menuNodeList;
		}
		private void MenuLoadError(Exception ex)
		{
			this.IsLoadingData = false;

			//TODO:save and show error correctly, no MessageBox.Show junk
			//System.Windows.MessageBox.Show(ex.ToString(), "Menu failed to load");
		}

		#region Clear Current Menu
		private void ClearMenu()
		{
			//clear
			foreach (MenuNode node in MenuNodes) {
				ClearMenu_Helper(node);
			}
			MenuNodes.Clear();

			if (this.HierarchyMenuList != null)
			{
				this.HierarchyMenuList.Clear();
			}
		}
		private void ClearMenu_Helper(MenuNode node)
		{
			if (node == null) return;

			//do recursion
			foreach (MenuNode item in node.Children) {
				ClearMenu_Helper(item);
			}
			//clear children
			node.Children.Clear();
		}
		#endregion //Clear Current Menu

		#endregion //Private Methods

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null) {
				PropertyChanged(this, e);
			}
		}

		#endregion //INotifyPropertyChanged Members
	}
}
