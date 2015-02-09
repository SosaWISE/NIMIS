using System.ServiceModel;

namespace NXS.Framework.Wpf.ParentChildService
{
	[ServiceContract]
	public interface IApplicationChild
	{
		[OperationContract]
		void CloseApplication();

		[OperationContract]
		void ParentClosed();

		[OperationContract]
		long GetMainWindowHandle();

		[OperationContract]
		void ShowWindow();

		[OperationContract]
		void InvokeAction(string actionName, ParameterDictionary arguments);
	}
}