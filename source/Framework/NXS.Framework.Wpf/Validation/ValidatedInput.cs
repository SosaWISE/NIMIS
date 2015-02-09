using System;
using System.ComponentModel;
using SOS.Lib.Util;

namespace NXS.Framework.Wpf.Validation
{
	public class ValidatedInput<T> : IValidatedInput
	{
		#region Properties

		protected bool _isDirty = false;
		protected bool _isValid = true;
		protected T _value;

		public IInputValidator<T> Validator { get; set; }

		readonly static PropertyChangedEventArgs IsEnabledChangeArgs = ObservableHelper.CreateArgs<ValidatedInput<T>>(x => x.IsEnabled);
		private bool _IsEnabled = true;
		public bool IsEnabled
		{
			get { return _IsEnabled; }
			set
			{
				if (_IsEnabled != value) {
					_IsEnabled = value;
					OnPropertyChanged(IsEnabledChangeArgs);
				}
			}
		}

		public object InputValue
		{
			get { return GetValue(); }
		}
		public virtual T Value
		{
			get { return GetValue(); }
			set { SetValue(value); }
		}
		public object GetInputValue()
		{
			return GetValue();
		}
		public virtual T GetValue()
		{
			return _value;
		}
		public void SetInputValue(object value)
		{
			SetValue((T)value);
		}
		public virtual void SetValue(T value)
		{
			if (!AreEqual(_value, value)) {

				//set value
				_value = value;

				//set is dirty
				bool isDirtyChanged = !_isDirty;
				if (isDirtyChanged) {
					//set is dirty value
					_isDirty = true;
				}

				RunValidation();

				//call is dirty changed event
				if (isDirtyChanged) {
					OnIsDirtyChanged();
				}

				//call value changed event
				OnValueChanged();
			}
		}

		#region BindingValue

		public virtual string BindingValue
		{
			get { return GetBindingValue(); }
			set { SetBindingValue(value); }
		}
		public virtual string GetBindingValue()
		{
			if (Value != null)
			{
				if (!string.IsNullOrEmpty(BindingValueFormatString))
				{
					return string.Format("{0:" + BindingValueFormatString + "}", Value);
				}
				else
				{
					return Value.ToString();
				}
			}
			else
			{
				return null;
			}
		}
		public virtual void SetBindingValue(string bindingValue)
		{
			T value;
			if (bindingValue != null) {
				try {
					value = ConversionHelper.ChangeType<T>(bindingValue);
				}
				catch {
					value = default(T);
				}
			}
			else {
				value = default(T);
			}

			this.Value = value;
		}

		public virtual string BindingValueFormatString
		{
			get; set;
		}

		#endregion //BindingValue

		public string Name { get; set; }

		public virtual bool IsDirty
		{
			get { return _isDirty; }
		}

		public virtual bool IsValid
		{
			get { return _isValid; }
		}

		public string InvalidMessage { get; set; }

		public string NullValueString { get; set; }

		public T NullCheckedValue
		{
			get
			{
				if (typeof(T) == typeof(string))
				{
					string testValue = this.Value as string;
					if (StringUtility.NullIfWhiteSpace(testValue) == null || StringUtility.AreEqual(testValue.Trim(), this.NullValueString, false))
					{
						return default(T);
					}
					else
					{
						return this.Value;
					}
				}
				else
				{
					return Value;
				}
			}
		}

		#endregion //Properties

		#region Protected Methods

		protected virtual bool AreEqual(T left, T right)
		{
			var result = Equals(left, right);
			return result;
		}

		protected virtual bool Validate()
		{
			bool valid;
			if (Validator != null) {
				valid = Validator.Validate(this.NullCheckedValue);
			}
			else {
				valid = true;
			}

			return valid;
		}

		#endregion //Protected Methods

		#region Public Methods

		public virtual void Clean()
		{
			if (_isDirty) {
				//set is dirty value
				_isDirty = false;
				OnIsDirtyChanged();
			}
		}

		public virtual void RunValidation()
		{
			//run validation
			bool isValid = Validate();
			if (_isValid != isValid) {
				//set is valid value
				_isValid = isValid;
				OnIsValidChanged();
			}
		}

		public virtual void Reset()
		{
			SetValue(default(T));
		}
		public virtual void RemoveValidation()
		{
			this.Validator = null;
		}

		#endregion //Public Methods

		#region IValidatedInput Members

		public event PropertyChangedEventHandler PropertyChanged;
		public event EventHandler ValueChanged;

		#endregion

		public readonly static PropertyChangedEventArgs ValueChangeArgs = ObservableHelper.CreateArgs<ValidatedInput<T>>(x => x.Value);
		public readonly static PropertyChangedEventArgs BindingValueChangeArgs = ObservableHelper.CreateArgs<ValidatedInput<T>>(x => x.BindingValue);
		public readonly static PropertyChangedEventArgs NullCheckedValueChangeArgs = ObservableHelper.CreateArgs<ValidatedInput<T>>(x => x.NullCheckedValue);
		protected virtual void OnValueChanged()
		{
			OnPropertyChanged(ValueChangeArgs);
			OnPropertyChanged(BindingValueChangeArgs);
			OnPropertyChanged(NullCheckedValueChangeArgs);
			if (ValueChanged != null) {
				ValueChanged(this, EventArgs.Empty);
			}
		}

		public readonly static PropertyChangedEventArgs IsValidChangeArgs = ObservableHelper.CreateArgs<ValidatedInput<T>>(x => x.IsValid);
		protected virtual void OnIsValidChanged()
		{
			OnPropertyChanged(IsValidChangeArgs);
		}

		public readonly static PropertyChangedEventArgs IsDirtyChangeArgs = ObservableHelper.CreateArgs<ValidatedInput<T>>(x => x.IsDirty);
		protected virtual void OnIsDirtyChanged()
		{
			OnPropertyChanged(IsDirtyChangeArgs);
		}

		protected virtual void OnPropertyChanged(string propertyName)
		{
			OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
		}

		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null) {
				PropertyChanged(this, e);
			}
		}

		public override string ToString()
		{
			return string.Format("{0}: {1}", Name, Value);
		}
	}
}