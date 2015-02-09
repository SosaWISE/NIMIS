namespace NXS.Framework.Wpf.ParentChildService
{
	public interface IParentSubscriber
	{
		void InvokeAction(InvokeActionArgs args);
		bool CanInvokeAction(InvokeActionArgs args);
	}
}
