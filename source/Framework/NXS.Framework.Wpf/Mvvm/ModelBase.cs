using System.Collections.ObjectModel;
using System.ComponentModel;
using NXS.Framework.Wpf.Validation;

namespace NXS.Framework.Wpf.Mvvm
{
	public class ModelBase : ValidatedInput<ModelBase>
	{
		public ObservableCollection<IValidatedInput> Inputs
		{
			get { return _inputs; }
		}
		protected ObservableCollection<IValidatedInput> _inputs;
		protected ObservableCollection<IStateList> _stateLists;

		public ModelBase()
		{
			_inputs = new ObservableCollection<IValidatedInput>();
			_stateLists = new ObservableCollection<IStateList>();

			Validator = new ModelBaseValidator();
		}

		#region Protected Methods

		public IValidatedInput AddInput(IValidatedInput input)
		{
			input.PropertyChanged += input_PropertyChanged;

			_inputs.Add(input);
			return input;
		}
		public IStateList AddStateList(IStateList item)
		{
			item.PropertyChanged += input_PropertyChanged;

			_stateLists.Add(item);
			return item;
		}

		private void input_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			HandleInputPropertyChanged(e);
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
				foreach (IValidatedInput input in _inputs) {
					if (input.IsDirty) {
						dirty = true;
						break;
					}
				}
			}
			if (!dirty) {
				foreach (IStateList item in _stateLists) {
					if (item.IsDirty) {
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

		protected virtual void HandleInputPropertyChanged(PropertyChangedEventArgs e)
		{
			if (string.Compare(e.PropertyName, "IsValid") == 0) {
				UpdateIsValid();
			}
			else if (string.Compare(e.PropertyName, "IsDirty") == 0) {
				UpdateIsDirty();
			}
		}

		#endregion //Protected Methods

		#region Public Methods

		public override void Clean()
		{
			foreach (IValidatedInput input in _inputs) {
				input.Clean();
			}
			foreach (IStateList item in _stateLists) {
				item.MarkClean();
			}
		}

		public override void RunValidation()
		{
			foreach (IValidatedInput input in _inputs) {
				input.RunValidation();
			}
			base.RunValidation();
		}

		public override void Reset()
		{
			//base.Reset();
			foreach (IValidatedInput input in _inputs) {
				input.Reset();
			}
			foreach (IStateList item in _stateLists) {
				item.Reset();
			}
		}

		public override void RemoveValidation()
		{
			//base.RemoveValidation();//don't remove validation for ModelBase
			foreach (IValidatedInput input in _inputs) {
				input.RemoveValidation();
			}
		}

		#endregion //Public Methods
	}
}