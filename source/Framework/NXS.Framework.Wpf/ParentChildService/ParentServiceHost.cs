using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace PPro.Framework.WpfFramework.ParentChildService
{
	[ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single, UseSynchronizationContext = true, IncludeExceptionDetailInFaults = true)]
	public class ParentServiceHost<T> : IApplicationParent where T : IParentWindow
	{
		#region Properties

		#region Protected

		protected T Parent { get; private set; }
		protected ServiceHost MessageHost { get; private set; }

		#endregion Protected

		#endregion Properties

		#region Constructors

		public ParentServiceHost(T parent)
		{
			if (parent == null)
			{
				throw new ArgumentNullException("parent");
			}

			this.Parent = parent;
		}

		#endregion Constructors

		#region Methods

		#region Private

		private void ServiceClosedCalback(IAsyncResult result)
		{
		}

		#endregion Private

		#region Public

		public void InitializeService()
		{
			if (MessageHost == null)
			{
				MessageHost = new ServiceHost(this, new Uri(WCFHelper.BaseUri));

				NetNamedPipeBinding binding = new NetNamedPipeBinding();
				binding.ReceiveTimeout = TimeSpan.MaxValue;

				MessageHost.AddServiceEndpoint(typeof(IApplicationParent), binding, WCFHelper.ParentServiceUri);

				try
				{
					MessageHost.Open();
				}
				catch// (Exception ex)
				{
				}
			}
			else if (MessageHost.State != CommunicationState.Opened)
				MessageHost.Open();
		}

		public void TerminateService()
		{
			if (MessageHost != null)
			{
				if (MessageHost.State == CommunicationState.Opened)
				{
					MessageHost.BeginClose(new AsyncCallback(ServiceClosedCalback), this);
				}
			}
			MessageHost = null;
		}

		//public IApplicationChild RegisterChildService(Guid childWindowID)
		//{
		//    ChannelFactory<IApplicationChild> childFactory =
		//        new ChannelFactory<IApplicationChild>(new NetNamedPipeBinding(), new EndpointAddress(WCFHelper.GetFullChildServiceUri(childWindowID)));

		//    return childFactory.CreateChannel();
		//}

		public ChannelFactory<IApplicationChild> CreateChildServiceFactory(Guid childWindowID)
		{
			return new ChannelFactory<IApplicationChild>(new NetNamedPipeBinding(), new EndpointAddress(WCFHelper.GetFullChildServiceUri(childWindowID)));

			//return childFactory.CreateChannel();
		}



		#endregion Public

		#region IApplicationParent Members

		public virtual void NotifyChildReady(Guid childID)
		{
			this.Parent.NotifyChildReady(childID);
		}

		public virtual void InvokeAction(string actionName, ParameterDictionary arguments)
		{
			this.Parent.InvokeAction(actionName, arguments);
		}

		#endregion

		#endregion Methods
	}
}