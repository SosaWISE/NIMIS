using System;

namespace NXS.Framework.Wpf.ParentChildService
{
	public interface IChildWindow
	{
		Guid ChildID { get; }

		void ParentClosed();

		long GetMainWindowHandle();

		void InvokeAction(InvokeActionArgs args);

		bool CanInvokeAction(InvokeActionArgs args);

		void Close();

		void ShowWindow();
	}
}