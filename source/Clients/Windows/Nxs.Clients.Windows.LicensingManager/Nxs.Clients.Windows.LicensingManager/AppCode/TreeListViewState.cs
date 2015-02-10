using System.Collections;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Nodes.Operations;

// ReSharper disable once CheckNamespace
namespace Nxs.Clients.Windows.LicensingManager
{
    public class TreeListViewState
    {
        #region Properties

        private static ArrayList _expanded;
        private static ArrayList _selected;
        private static object _focused;
        private static int _topIndex;
        private static TreeList _treeList;
        public static TreeList TreeList
        {
            get
            {
                return _treeList;
            }
            set
            {
                _treeList = value;
                Clear();
            }
        }

        #endregion Properties

        #region Methods

        #region Public

        public static void Clear()
        {
            _expanded.Clear();
            _selected.Clear();
            _focused = null;
            _topIndex = 0;
        }
        //public TreeListViewState() : this(null) { }
        public static void _loadTreeListViewState(TreeList treelist)
        {
            _treeList = treelist;
            _expanded = new ArrayList();
            _selected = new ArrayList();
        }

        public static void LoadState()
        {
            TreeList.BeginUpdate();
            try
            {
                TreeList.CollapseAll();
                TreeListNode oNode;
                foreach (object key in _expanded)
                {
                    oNode = TreeList.FindNodeByKeyID(key);
                    if (oNode != null)
                    {
                        oNode.Expanded = true;
                    }
                }
                foreach (object key in _selected)
                {
                    oNode = TreeList.FindNodeByKeyID(key);
                    if (oNode != null)
                    {
                        TreeList.Selection.Add(oNode);
                    }
                }
                TreeList.FocusedNode = TreeList.FindNodeByKeyID(_focused);
            }
            finally
            {
                TreeList.EndUpdate();
                TreeList.TopVisibleNodeIndex = TreeList.GetVisibleIndexByNode(TreeList.FocusedNode) - _topIndex;
            }
        }

        public static void SaveState()
        {
            if (TreeList.FocusedNode != null)
            {
                _expanded = GetExpanded();
                _selected = GetSelected();
                _focused = TreeList.FocusedNode[TreeList.KeyFieldName];
                _topIndex = TreeList.GetVisibleIndexByNode(TreeList.FocusedNode) - TreeList.TopVisibleNodeIndex;
            }
            else
            {
                Clear();
            }
        }

        #endregion Public

        #region Private

        private static ArrayList GetExpanded()
        {
            OperationSaveExpanded op = new OperationSaveExpanded();
            TreeList.NodesIterator.DoOperation(op);
            return op.Nodes;
        }

        private static ArrayList GetSelected()
        {
            ArrayList al = new ArrayList();
            foreach (TreeListNode oNode in TreeList.Selection)
            {
                al.Add(oNode.GetValue(oNode.TreeList.KeyFieldName));
            }
            return al;
        }





        #endregion Private

        #endregion Methods

        class OperationSaveExpanded : TreeListOperation
        {
            private ArrayList al = new ArrayList();
            public override void Execute(TreeListNode node)
            {
                if (node.HasChildren && node.Expanded)
                {
                    al.Add(node.GetValue(node.TreeList.KeyFieldName));
                }
            }

            public ArrayList Nodes
            {
                get
                {
                    return al;
                }
            }


        }
    }
}
