using System;
using System.Collections.Generic;
using System.ServiceModel;
using SOS.Lib.Util;

namespace NXS.Framework.Wpf.ParentChildService
{
	[ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single, UseSynchronizationContext = true, IncludeExceptionDetailInFaults = true)]
	public class WindowApplicationChild<T> : IApplicationChild where T : IChildWindow
	{
		#region Properties

		#region Private
		#endregion Private

		#region Protected

		protected T ChildWindow { get; private set; }
		protected ServiceHost MessageHost { get; private set; }
		public ChannelFactory<IApplicationParent> ParentFactory { get; private set; }

		#endregion Protected

		#region Public
		#endregion Public

		#endregion Properties

		#region Constructors

		public WindowApplicationChild(T childWindow)
		{
			if (childWindow == null)
				throw new ArgumentNullException("childWindow");

			ChildWindow = childWindow;

			ParentFactory =
				new ChannelFactory<IApplicationParent>(new NetNamedPipeBinding(), new EndpointAddress(WCFHelper.FullParentServiceUri));
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
				MessageHost = new ServiceHost(this, new Uri(WCFHelper.GetBaseChildServiceUri(ChildWindow.ChildID)));

				var binding = new NetNamedPipeBinding();
				binding.ReceiveTimeout = TimeSpan.MaxValue;

				MessageHost.AddServiceEndpoint(typeof(IApplicationChild), binding, WCFHelper.GetChildServiceUri(ChildWindow.ChildID));
				MessageHost.Open();
			}
			else if (MessageHost.State != CommunicationState.Opened)
				MessageHost.Open();
		}

		public void TerminateService()
		{
			if (MessageHost != null)
			{
				if (MessageHost.State != CommunicationState.Closed)
				{
					MessageHost.BeginClose(new AsyncCallback(ServiceClosedCalback), this);
				}
			}
			MessageHost = null;
		}

		public IApplicationParent RegisterParent()
		{
			return ParentFactory.CreateChannel();
		}

		public Guid GetChildWindowIDParameter()
		{
			Dictionary<string, string> args = StringUtility.GetParameters(Environment.GetCommandLineArgs());
			if (args.ContainsKey(ParameterNames.ChildWindowID))
				return new Guid(args[ParameterNames.ChildWindowID]);
			else
				return Guid.Empty;
		}

		#endregion Public

		#region IApplicationChild Members

		public virtual void CloseApplication()
		{
			ChildWindow.Close();
		}

		public virtual void ParentClosed()
		{
			ChildWindow.ParentClosed();
		}

		public virtual long GetMainWindowHandle()
		{
			return ChildWindow.GetMainWindowHandle();
		}

		public virtual void InvokeAction(string actionName, ParameterDictionary arguments)
		{
			ChildWindow.InvokeAction(new InvokeActionArgs(actionName, arguments));
		}

		public virtual void ShowWindow()
		{
			ChildWindow.ShowWindow();
		}

		#endregion

		#endregion Methods
	}
}