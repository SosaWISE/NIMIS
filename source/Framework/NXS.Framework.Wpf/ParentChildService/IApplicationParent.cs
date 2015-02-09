using System;
using System.ServiceModel;

namespace NXS.Framework.Wpf.ParentChildService
{
	[ServiceContract]
	public interface IApplicationParent
	{
		[OperationContract]
		void NotifyChildReady(Guid childID);

		[OperationContract]
		void InvokeAction(string actionName, ParameterDictionary arguments);
	}
}