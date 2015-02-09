using System.ComponentModel;
using NXS.Framework.Wpf.Mvvm;
using SOS.Lib.Util;

namespace NXS.Framework.Wpf.Validation
{
	public class StateList<T> : SelectableList<T>, IStateList
	{
		#region Properties

		readonly static PropertyChangedEventArgs IsDirtyChangeArgs = ObservableHelper.CreateArgs<StateList<T>>(x => x.IsDirty);
		private bool _isDirty;
		public virtual bool IsDirty
		{
			get { return _isDirty; }
			protected set
			{
				if (_isDirty != value) {

					_isDirty = value;
					OnPropertyChanged(IsDirtyChangeArgs);
				}
			}
		}

		#endregion //Properties

		#region Public Methods

		public virtual void MarkClean()
		{
			IsDirty = false;
		}
		public virtual void MarkDirty()
		{
			IsDirty = true;
		}
		public virtual void Reset()
		{
			Clear();
			SelectCurrent();
		}

		public override void Add(T item)
		{
			base.Add(item);
			IsDirty = true;
		}
		public override void Clear()
		{
			base.Clear();
			IsDirty = true;
		}

		#endregion //Public Methods
	}
}
