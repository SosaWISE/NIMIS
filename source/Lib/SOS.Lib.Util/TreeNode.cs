using System.Collections.Generic;

namespace SOS.Lib.Util
{
	public class TreeNode<TKey, TValue>
	{
		#region Properties

		#region Private

		private readonly Dictionary<TKey, TreeNode<TKey, TValue>> _childNodesList =
			new Dictionary<TKey, TreeNode<TKey, TValue>>();

		#endregion Private

		#region Public

		/// <summary>
		/// Gets or sets the ID of this node
		/// </summary>
		public TKey ID { get; set; }

		/// <summary>
		///  Gets or sets the value of this node.
		/// </summary>
		public TValue Value { get; set; }

		/// <summary>
		/// Gets a list of the child nodes of this node.
		/// </summary>
		protected Dictionary<TKey, TreeNode<TKey, TValue>> ChildNodesList
		{
			get { return _childNodesList; }
		}

		/// <summary>
		/// An enumeration of the child nodes of this node.
		/// </summary>
		public IEnumerable<TreeNode<TKey, TValue>> ChildNodes
		{
			get
			{
				foreach (var curr in _childNodesList.Values)
					yield return curr;
			}
		}

		/// <summary>
		/// Gets the number of child nodes of this node.
		/// </summary>
		public int NChildNodes
		{
			get { return _childNodesList.Count; }
		}

		/// <summary>
		/// Gets the parent node of this node.
		/// </summary>
		public TreeNode<TKey, TValue> Parent { get; protected set; }

		#endregion Public

		#endregion Properties

		#region Constructors

		public TreeNode()
		{
		}

		public TreeNode(TKey id, TValue value)
		{
			ID = id;
			Value = value;
		}

		#endregion Constructors

		#region Methods

		#region Private

		#endregion Private

		#region Public

		public void AddChild(TreeNode<TKey, TValue> childNode)
		{
			// Make sure the given node doesn't already exist in the tree
			RemoveChild(childNode);

			// Add the child to the list of nodes
			_childNodesList.Add(childNode.ID, childNode);
			childNode.Parent = this;
		}

		public void RemoveChild(TreeNode<TKey, TValue> childNode)
		{
			// Remove the child from the list of nodes
			_childNodesList.Remove(childNode.ID);
			childNode.Parent = null;
		}

		public void ClearChildren()
		{
			foreach (var curr in _childNodesList.Values)
				curr.Parent = null;

			_childNodesList.Clear();
		}

		public TreeNode<TKey, TValue> LookupNode(TKey key)
		{
			if (key.Equals(ID))
				return this;
			if (_childNodesList.ContainsKey(key))
				return _childNodesList[key];
			
			//Default execution path.
			foreach (var curr in _childNodesList.Values)
			{
				TreeNode<TKey, TValue> result = curr.LookupNode(key);
				if (result != null)
					return result;
			}

			return null;
		}

		#endregion Public

		#endregion Methods
	}
}