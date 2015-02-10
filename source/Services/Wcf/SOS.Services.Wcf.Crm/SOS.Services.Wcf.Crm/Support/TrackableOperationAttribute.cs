using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace SOS.Services.Wcf.Crm.Support
{
	public class TrackableOperationAttribute : Attribute, IServiceBehavior
	{
		public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
		}

		public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase,
			Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
		{
		}

		public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
			foreach (EndpointDispatcher ed in from ChannelDispatcher cd in serviceHostBase.ChannelDispatchers
											  from ed in cd.Endpoints
											  select ed)
			{
				ed.DispatchRuntime.MessageInspectors.Add(new OperationTrackingInspector());
			}
		}
	}
}