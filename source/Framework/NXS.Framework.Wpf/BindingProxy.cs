using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace NXS.Framework.Wpf
{
	public class BindingProxy<T> : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		#region Properties

		private T _value;
		public T Value
		{
			get { return _value; }
			set
			{
				if (!EqualityComparer<T>.Default.Equals(_value, value))
				{
					_value = value;
					OnPropertyChanged(new PropertyChangedEventArgs("Value"));
				}
			}
		}

		#endregion Properties

		#region Constructors

		public BindingProxy()
		{
		}

		public BindingProxy(T value)
		{
			Value = value;
		}

		#endregion Constructors

		#region Methods

		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, e);
		}

		#endregion Methods
	}
}