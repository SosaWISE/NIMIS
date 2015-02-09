using System.ComponentModel;
using SOS.Lib.Util;

namespace NXS.Framework.Wpf.Mvvm
{
	public class ObservableValueContainer<T> : INotifyPropertyChanging, INotifyPropertyChanged
	{
		public event PropertyChangingEventHandler PropertyChanging;
		public event PropertyChangedEventHandler PropertyChanged;
		public event PropertyChangedEventHandler ValueChanged;

		private T _value;
		public static readonly PropertyChangedEventArgs ValueChangedEventArgs = ObservableHelper.CreateArgs<ObservableValueContainer<T>>(param => param.Value);
		public static readonly PropertyChangingEventArgs ValueChangingEventArgs = ObservableHelper.CreateChangingArgs<ObservableValueContainer<T>>(param => param.Value);

		public T Value
		{
			get { return GetValue(); }
			set {SetValue(value);}
		}
		public virtual T GetValue()
		{
			return _value;
		}
		public void SetValue(T value)
		{
			if (!Equals(_value, value)) {

				// Send the changing event
				OnPropertyChanging(ValueChangingEventArgs);

				// Set the value
				_value = value;

				// Send the changed event
				OnPropertyChanged(ValueChangedEventArgs);
			}
		}

		public ObservableValueContainer()
		{
		}

		public ObservableValueContainer(T initialValue)
		{
			_value = initialValue;
		}

		protected virtual void OnPropertyChanging(PropertyChangingEventArgs e)
		{
			if (PropertyChanging != null)
			{
				PropertyChanging(this, e);
			}
		}

		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, e);
			}

			if (e.PropertyName == ValueChangedEventArgs.PropertyName)
			{
				if (ValueChanged != null)
				{
					ValueChanged(this, e);
				}
			}
		}

		public override string ToString()
		{
			if (_value != null)
			{
				return _value.ToString();
			}
			else
			{
				return string.Empty;
			}
		}
	}
}