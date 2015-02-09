using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace NXS.Framework.Wpf.Controls
{
	public partial class ValueBox : UserControl, INotifyPropertyChanged
	{
		private static readonly int _maximum = 255;

		#region Fields

		private int _value = _maximum;
		private double _step;
		private double _width;

		#endregion //Fields

		public ValueBox()
		{
			InitializeComponent();

			SizeChanged += new SizeChangedEventHandler(ValueBox_SizeChanged);
		}

		#region Properties
		/// <summary>
		/// Sets or gets the value.
		/// </summary>
		public int Value
		{
			get { return _value; }
			set
			{
				if (value > _maximum) {
					_value = 255;
				}
				else {
					_value = value;
				}
				UpdateBox();
				OnPropertyChanged("Value");
			}
		}

		#endregion

		#region Events
		private void UpdateBox()
		{
			border_value.Width = _value * _step;
		}
		void ValueBox_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			_width = e.NewSize.Width;
			_step = _width / _maximum;
		}
		#endregion

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;
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

		#endregion

		public void Connect(int connectionId, object target)
		{
			throw new System.NotImplementedException();
		}
	}

}
