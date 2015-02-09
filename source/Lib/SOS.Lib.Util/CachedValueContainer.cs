using System;
using System.ComponentModel;

namespace SOS.Lib.Util
{
	public class CachedValueContainer<T> : INotifyPropertyChanged where T : class
	{
		#region Events

		public event PropertyChangedEventHandler PropertyChanged;
		public event PropertyChangedEventHandler CachedValueChanged;

		#endregion Events

		#region Properties

		#region Private

		private static readonly PropertyChangedEventArgs IsLoadingChangedArgs =
			ObservableHelper.CreateArgs<CachedValueContainer<T>>(param => param.IsLoading);

		private static readonly PropertyChangedEventArgs CachedValueChangedArgs =
			ObservableHelper.CreateArgs<CachedValueContainer<T>>(param => param.CachedValue);

		private static readonly object _syncRootLoading = new object();
		private T _cachedValue;
		private bool _isLoading;

		#endregion Private

		#region Public

		public bool IsLoading
		{
			get { return _isLoading; }
			set
			{
				if (_isLoading != value)
				{
					_isLoading = value;
					OnPropertyChanged(IsLoadingChangedArgs);
				}
			}
		}

		public T CachedValue
		{
			get
			{
				if (_cachedValue == null && !IsLoading)
				{
					StartLoading();
				}
				return _cachedValue;
			}
			private set
			{
				if (_cachedValue != value)
				{
					_cachedValue = value;
					OnPropertyChanged(CachedValueChangedArgs);
					OnCachedValueChanged(CachedValueChangedArgs);
				}
			}
		}

		public Func<T> LoadValue { get; private set; }
		public Action<Exception> HandleError { get; private set; }

		#endregion Public

		#endregion Properties

		#region Constructors

		public CachedValueContainer(Func<T> loadValue, Action<Exception> handleError)
		{
			if (loadValue == null)
			{
				throw new ArgumentNullException("loadValue");
			}
			if (handleError == null)
			{
				throw new ArgumentNullException("handleError");
			}

			LoadValue = loadValue;
			HandleError = handleError;
		}

		#endregion Constructors

		#region Methods

		#region Private

		private void StartLoading()
		{
			IsLoading = true;
			AsyncHelper.ExecuteAsync(ExecuteLoad, FinishLoading, HandleLoadingError);
		}

		private T ExecuteLoad()
		{
			if (_cachedValue == null)
			{
				lock (_syncRootLoading)
				{
					if (_cachedValue == null)
					{
						CachedValue = LoadValue();
					}
				}
			}
			return _cachedValue;
		}

		private void FinishLoading(T value)
		{
			IsLoading = false;
		}

		private void HandleLoadingError(Exception ex)
		{
			IsLoading = false;
			HandleError(ex);
		}

		#endregion Private

		#endregion Methods

		#region Protected

		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, e);
			}
		}

		protected virtual void OnCachedValueChanged(PropertyChangedEventArgs e)
		{
			if (CachedValueChanged != null)
			{
				CachedValueChanged(this, e);
			}
		}

		#endregion Protected
	}
}