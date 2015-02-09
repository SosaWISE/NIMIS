using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using NXS.Framework.Wpf.Mvvm;
using SOS.Lib.Util;

namespace NXS.Framework.Wpf.Validation
{
	public interface IValidatedListInput
	{
		//IList List { get; }

		string DisplayMemberPath { get; set; }
		string SelectedValuePath { get; set; }
	}
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T">Type of ID for list items</typeparam>
	/// <typeparam name="ListItemType">Type of List items</typeparam>
	public class ValidatedListInput<T, ListItemType> : ValidatedInput<T>, IValidatedListInput
	{
		#region Fields

		Func<ListItemType, T, bool> _listItemMatchesId;

		ListItemType _defaultListItem;
		T _defaultT;

		#endregion //Fields

		#region Properties

		readonly static PropertyChangedEventArgs CanClearChangeArgs = ObservableHelper.CreateArgs<ValidatedListInput<T, ListItemType>>(x => x.CanClear);
		private bool _CanClear;
		public bool CanClear
		{
			get { return _CanClear; }
			set
			{
				if (_CanClear != value) {
					_CanClear = value;
					OnPropertyChanged(CanClearChangeArgs);
				}
			}
		}

		private ICommand _ClearCommand;
		public ICommand ClearCommand
		{
			get { return _ClearCommand ?? (_ClearCommand = new RelayCommand<object>(param => this.ClearValue())); }
		}

		readonly static PropertyChangedEventArgs ListChangeArgs = ObservableHelper.CreateArgs<ValidatedListInput<T, ListItemType>>(x => x.List);
		private IList<ListItemType> _list;
		public IList<ListItemType> List
		{
			get { return _list; }
			set
			{
				if (_list != value) {

					_list = value;

					//set Value to default if not already default
					if (!AreEqual(Value, _defaultT)) {

						ListItemType selectedItem = GetSelectedValue();
						//set Value to default if not found in list(selectedItemd equals default)
						if (AreEqual(selectedItem, _defaultListItem)) {

							ClearValue();//set the base value since the override hack prevents setting it
						}
						else {
							////REVIEW:neither of these work
							//
							////tell UI the value has changed so it selects the correct one from the list
							//T tempV = Value;
							//ClearValue();
							//SetValue(tempV);
							//
							////tell UI the value has changed so it selects the correct one from the list
							//OnValueChanged();
						}
					}

					RunValidation();

					OnPropertyChanged(ListChangeArgs);
				}
			}
		}

		private void ClearValue()
		{
			base.SetValue(_defaultT);
		}
		protected virtual bool AreEqual(ListItemType left, ListItemType right)
		{
			var result = Equals(left, right);
			return result;
		}

		public override T Value
		{
			get { return base.Value; }
			set
			{
				//HACK: This is to prevent the view from unselecting the value when it is no longer visible
				if (value == null) return;

				SetValue(value);
			}
		}
		public override void SetValue(T value)
		{
			if (AreEqual(GetSelectedValue(value), _defaultListItem)) {
				//value is not in list, select nothing
				ClearValue();
			}
			else {
				base.SetValue(value);
			}
		}

		readonly static PropertyChangedEventArgs SelectedItemChangedArgs = ObservableHelper.CreateArgs<ValidatedListInput<T, ListItemType>>(x => x.SelectedItem);
		private ListItemType _selectedItem;
		public ListItemType SelectedItem
		{
			get { return _selectedItem; }
			private set
			{
				if (!Comparison<ListItemType>.Equals(_selectedItem, value))
				{
					_selectedItem = value;
					OnPropertyChanged(SelectedItemChangedArgs);
				}
			}
		}

		public string DisplayMemberPath { get; set; }
		public string SelectedValuePath { get; set; }

		#endregion //Properties

		#region .ctors

		/// <summary>
		/// 
		/// </summary>
		/// <param name="listItemMatchesId">e.g. (item, id) => item.YourIDHere == id</param>
		public ValidatedListInput(Func<ListItemType, T, bool> listItemMatchesId)
		{
			if (listItemMatchesId == null)
				throw new ArgumentNullException("listItemMatchesId");

			_listItemMatchesId = listItemMatchesId;

			_defaultListItem = default(ListItemType);
			_defaultT = default(T);
		}

		#endregion //.ctors

		#region Protected

		protected override void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			// Let the base class do needed work
			base.OnPropertyChanged(e);

			// When the 'Value' changes, also set the 'SelectedItem' property
			if (e.PropertyName == ValueChangeArgs.PropertyName)
			{
				this.SelectedItem = GetSelectedValue();
			}
		}

		#endregion //Protected

		#region Public Methods

		public ListItemType GetSelectedValue()
		{
			return GetSelectedValue(this.Value);
		}

		public ListItemType GetSelectedValue(T value)
		{
			IList<ListItemType> list = List;
			if (list != null && _listItemMatchesId != null && value != null) {
				return list.FirstOrDefault(a => _listItemMatchesId(a, value));
			}
			return _defaultListItem;
		}

		public object GetSelectedDisplayMember()
		{
			ListItemType item = SelectedItem;//GetSelectedValue();
			if (item == null)
				return null;

			return ReflectionHelper.GetPropertyValue(item, DisplayMemberPath);
		}

		#endregion //Public Methods
	}
}