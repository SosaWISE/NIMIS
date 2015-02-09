using System.ComponentModel;

namespace NXS.Framework.Wpf.Validation
{
	public interface IStateList : INotifyPropertyChanged
	{
		void MarkClean();
		void MarkDirty();
		bool IsDirty { get; }
		void Reset();
	}
}
