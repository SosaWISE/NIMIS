using System;
using System.ComponentModel;
using System.Windows.Input;
using SOS.Lib.Util;

namespace NXS.Framework.Wpf.Mvvm.Models
{
	public class BaseLink : INotifyPropertyChanged
	{
		#region Properties

		#region Private

		private ICommand _command;

		#endregion Private

		#region Public

		public ICommand Command
		{
			get { return _command; }
			set
			{
				// Unwire the CanExecuteChanged event for the old value
				if (_command != null) {
					_command.CanExecuteChanged -= new EventHandler(Command_CanExecuteChanged);
				}

				// Set the value
				_command = value;
				CanCommandExecute = false;

				// Wire the CanExecuteChanged event for the new value
				if (_command != null) {
					_command.CanExecuteChanged += new EventHandler(Command_CanExecuteChanged);
					CanCommandExecute = _command.CanExecute(CommandParameter);
				}
			}
		}
		readonly static PropertyChangedEventArgs LabelChangeArgs = ObservableHelper.CreateArgs<BaseLink>(a => a.Label);
		private string _Label;
		public string Label
		{
			get { return _Label; }
			set
			{
				if (_Label != value) {

					_Label = value;

					OnPropertyChanged(LabelChangeArgs);
				}
			}
		}

		readonly static PropertyChangedEventArgs ToolTipChangeArgs = ObservableHelper.CreateArgs<BaseLink>(a => a.ToolTip);
		private string _ToolTip;
		public string ToolTip
		{
			get { return _ToolTip; }
			set
			{
				if (_ToolTip != value)
				{

					_ToolTip = value;

					OnPropertyChanged(ToolTipChangeArgs);
				}
			}
		}

		public virtual object CommandParameter { get; set; }

		readonly static PropertyChangedEventArgs canCommandExecuteChangeArgs = ObservableHelper.CreateArgs<BaseLink>(a => a.CanCommandExecute);
		private bool _canCommandExecute;
		public bool CanCommandExecute
		{
			get { return _canCommandExecute; }
			set
			{
				if (_canCommandExecute != value) {

					_canCommandExecute = value;

					OnPropertyChanged(canCommandExecuteChangeArgs);
				}
			}
		}

		readonly static PropertyChangedEventArgs IsVisibleChangeArgs = ObservableHelper.CreateArgs<BaseLink>(a => a.IsVisible);
		private bool _isVisible;
		public bool IsVisible
		{
			get { return _isVisible; }
			set
			{
				if (_isVisible != value)
				{

					_isVisible = value;

					OnPropertyChanged(IsVisibleChangeArgs);
				}
			}
		}

		#endregion Public

		#endregion Properties

		#region .ctor

		public BaseLink()
		{
		}

		#endregion //.ctor

		#region Methods

		#region Event Handlers

		private void Command_CanExecuteChanged(object sender, EventArgs e)
		{
			if (this.Command != null) {
				CanCommandExecute = this.Command.CanExecute(CommandParameter);
			}
		}

		#endregion //Event Handlers

		#endregion Methods

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
