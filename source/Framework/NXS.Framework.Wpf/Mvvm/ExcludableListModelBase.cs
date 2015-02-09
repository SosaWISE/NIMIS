using System;
using NXS.Framework.Wpf.Validation;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace NXS.Framework.Wpf.Mvvm
{
	public class ExcludableListModelBase<T> : ValidatedInput<ExcludableListModelBase<T>>
		where T : IExcludableListModelBaseItem
	{
		public ReadOnlyObservableCollection<T> ItemsToInclude { get; private set; }
		protected ObservableCollection<T> _ItemsToInclude;

		public ExcludableListModelBase()
			: this(new ListModelBaseValidator<T>())
		{
		}
		protected ExcludableListModelBase(ListModelBaseValidator<T> validator)
		{
			this._ItemsToInclude = new ObservableCollection<T>();
			this.ItemsToInclude = new ReadOnlyObservableCollection<T>(this._ItemsToInclude);
			this.Validator = validator;
		}

		public T AddItemToInclude(T itemToInclude)
		{
			_ItemsToInclude.Add(itemToInclude);

			ItemAdded(itemToInclude);

			return itemToInclude;
		}
		protected virtual void ItemAdded(T itemToInclude)
		{
			itemToInclude.PropertyChanged += input_PropertyChanged;
			itemToInclude.IncludeInValidation.ValueChanged += IncludeInValidation_ValueChanged;
		}

		public bool RemoveItemToInclude(T itemToInclude)
		{
			bool removed = _ItemsToInclude.Remove(itemToInclude);
			if (removed) {
				ItemRemoved(itemToInclude);
			}
			return removed;
		}
		protected virtual void ItemRemoved(T itemToInclude)
		{
			itemToInclude.PropertyChanged -= input_PropertyChanged;
			itemToInclude.IncludeInValidation.ValueChanged -= IncludeInValidation_ValueChanged;
		}

		void IncludeInValidation_ValueChanged(object sender, EventArgs e)
		{
			//update IsValid whenever this changes
			UpdateIsValid();
		}

		private void input_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			HandleInputPropertyChanged(e);
		}

		protected virtual void HandleInputPropertyChanged(PropertyChangedEventArgs e)
		{
			if (string.Compare(e.PropertyName, "IsValid") == 0) {
				UpdateIsValid();
			}
			else if (string.Compare(e.PropertyName, "IsDirty") == 0) {
				UpdateIsDirty();
			}
		}

		protected void UpdateIsValid()
		{
			bool isValid = Validate();
			if (_isValid != isValid) {
				//set is valid value
				_isValid = isValid;
				OnIsValidChanged();
			}
		}

		private void UpdateIsDirty()
		{
			bool dirty = false;
			if (!dirty) {
				foreach (IValidatedInput input in _ItemsToInclude) {
					if (input.IsDirty) {
						dirty = true;
						break;
					}
				}
			}

			if (_isDirty != dirty) {
				//set is dirty value
				_isDirty = dirty;
				OnIsDirtyChanged();
			}
		}

		protected override bool Validate()
		{
			return Validator.Validate(this);
		}

		public override void Clean()
		{
			foreach (IValidatedInput input in _ItemsToInclude) {
				input.Clean();
			}
		}

		public override void RunValidation()
		{
			foreach (IValidatedInput input in _ItemsToInclude) {
				input.RunValidation();
			}
			base.RunValidation();
		}

		public override void Reset()
		{
			_ItemsToInclude.Clear();
			//foreach (IValidatedInput input in _OfficesToMigrate) {
			//    input.Reset();
			//}
		}
		public override void RemoveValidation()
		{
			//base.RemoveValidation();//don't remove validation for ModelBase
			foreach (IValidatedInput input in _ItemsToInclude) {
				input.RemoveValidation();
			}
		}
	}

	public class ListModelBaseValidator<T> : IInputValidator<ExcludableListModelBase<T>>
		where T : IExcludableListModelBaseItem
	{
		public ListModelBaseValidator()
		{
		}

		public static ExcludableListModelBase<IExcludableListModelBaseItem> Create()
		{
			return new ExcludableListModelBase<IExcludableListModelBaseItem>();
		}

		#region IInputValidator<T> Members

		public bool Validate(ExcludableListModelBase<T> value)
		{
			bool valid = true;
			foreach (T item in value.ItemsToInclude) {

				if (item.IncludeInValidation.Value && !item.IsValid) {
					valid = false;
					break;
				}
			}
			return valid;
		}

		#endregion
	}
}
