using System.ComponentModel;
using SOS.Lib.Util;

namespace NXS.Framework.Wpf.Mvvm
{
	public class AsyncList<T> : SelectableList<T>
	{
		#region Properties

		readonly static PropertyChangedEventArgs IsLoadingDataChangeArgs = ObservableHelper.CreateArgs<AsyncList<T>>(x => x.IsLoadingData);
		bool _isLoadingData;
		public bool IsLoadingData
		{
			get { return _isLoadingData; }
			protected set
			{
				if (_isLoadingData == value) return;

				_isLoadingData = value;

				OnPropertyChanged(IsLoadingDataChangeArgs);
			}
		}

		#endregion //Properties

		#region Public Methods

		public void SetIsLoading(bool isLoading)
		{
			this.IsLoadingData = isLoading;
		}

		#endregion //Public Methods
	}
}
