using System;
using System.Collections.Generic;
using System.Text;
using SOS.Lib.Util;
using System.Windows.Input;
using System.ComponentModel;

namespace NXS.Framework.Wpf.Mvvm
{
	public class ReasonCommand<T> : ICommand<T>, INotifyPropertyChanged
	{
		#region Properties

		#region Public

		public bool CommandSucceeded { get; set; }

		private string _InvalidReasons;
		public static readonly PropertyChangedEventArgs InvalidReasonsChangedArgs = ObservableHelper.CreateArgs<ReasonCommand<T>>(a => a.InvalidReasons);
		public string InvalidReasons
		{
			get { return _InvalidReasons; }
			private set
			{
				if (_InvalidReasons == value) return;

				_InvalidReasons = value;
				OnPropertyChanged(InvalidReasonsChangedArgs);
			}
		}

		#endregion //Public

		#region Private

		protected Action<T> _executeDelegate;
		protected PredicateReasonList<T> _canExecuteList;

		#endregion Private

		#endregion Properties

		#region Constructors

		public ReasonCommand(Action<T> executeDelegate, PredicateReasonList<T> canExecuteList)
		{
			if (executeDelegate == null)
				throw new ArgumentNullException("executeDelegate");
			if (canExecuteList == null)
				throw new ArgumentNullException("canExecuteList");

			_executeDelegate = executeDelegate;
			_canExecuteList = canExecuteList;
		}

		#endregion Constructors

		#region ICommand Members

		public bool CanExecute(object parameter)
		{
			return CanExecute(ConversionHelper.ChangeType<T>(parameter));
		}

		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public void Execute(object parameter)
		{
			if (_executeDelegate != null) {
				_executeDelegate(ConversionHelper.ChangeType<T>(parameter));
				CommandSucceeded = true;
			}
		}

		#endregion

		#region ICommand<T> Members

		public bool CanExecute(T parameter)
		{
			bool result = true;

			StringBuilder sob = new StringBuilder();
			foreach (var item in _canExecuteList) {
				bool valid = item.Predicate(parameter);
				if (!valid && !string.IsNullOrEmpty(item.InvalidReason)) {
					sob.AppendLine(item.InvalidReason);
				}
				result &= valid;
			}

			this.InvalidReasons = (sob.Length > 0) ? string.Concat("Reasons:", Environment.NewLine, sob.ToString()) : null;

			return result;
		}

		public void Execute(T parameter)
		{
			_executeDelegate(parameter);
			CommandSucceeded = true;
		}

		#endregion

		public bool CanExecute(T parameter, List<string> invalidList)
		{
			return false;
		}

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
	}
}
