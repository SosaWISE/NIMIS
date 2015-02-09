using System;
using System.Windows.Input;
using SOS.Lib.Util;

namespace NXS.Framework.Wpf.Mvvm
{
	public class RelayCommand<T> : ICommand<T>
	{
		#region Properties

		#region Public

		public bool CommandSucceeded { get; set; }

		#endregion //Public

		#region Private

		private readonly Action<T> _executeDelegate;
		private readonly Predicate<T> _canExecuteDelegate;

		#endregion Private

		#endregion Properties

		#region Constructors

		public RelayCommand(Action<T> executeDelegate)
			: this(executeDelegate, null)
		{
		}

		public RelayCommand(Action<T> executeDelegate, Predicate<T> canExecuteDelegate)
		{
			if (executeDelegate == null)
				throw new ArgumentNullException("executeDelegate");

			_executeDelegate = executeDelegate;
			_canExecuteDelegate = canExecuteDelegate;
		}

		#endregion Constructors

		#region ICommand Members

		public bool CanExecute(object parameter)
		{
			if (_canExecuteDelegate != null)
			{
				return _canExecuteDelegate(ConversionHelper.ChangeType<T>(parameter));
			}
			return true;
		}

		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public void Execute(object parameter)
		{
			if (_executeDelegate != null)
			{
				_executeDelegate(ConversionHelper.ChangeType<T>(parameter));
				CommandSucceeded = true;
			}
		}

		#endregion

		#region ICommand<T> Members

		public bool CanExecute(T parameter)
		{
			if (_canExecuteDelegate != null)
			{
				return _canExecuteDelegate(parameter);
			}
			return true;
		}

		public void Execute(T parameter)
		{
			if (_executeDelegate != null)
			{
				_executeDelegate(parameter);
				CommandSucceeded = true;
			}
		}

		#endregion
	}

	public class RelayCommand : RelayCommand<object>
	{
		#region Constructors

		public RelayCommand(Action<object> executeDelegate)
			: base(executeDelegate, null)
		{
		}

		public RelayCommand(Action<object> executeDelegate, Predicate<object> canExecuteDelegate)
			: base(executeDelegate, canExecuteDelegate)
		{
			if (executeDelegate == null)
				throw new ArgumentNullException("executeDelegate");
		}

		#endregion Constructors
	}
}