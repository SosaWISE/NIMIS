using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace PPro.Framework.WpfFramework.ParentChildService
{
	public class ApplicationChildProxy : ClientBase<IApplicationChild>, IApplicationChild
	{
		#region Constructors

		public ApplicationChildProxy()
		{
		}

		public ApplicationChildProxy(string endpointConfigurationName)
			: base(endpointConfigurationName)
		{
		}

		public ApplicationChildProxy(string endpointConfigurationName, string remoteAddress)
			: base(endpointConfigurationName, remoteAddress)
		{
		}

		public ApplicationChildProxy(string endpointConfigurationName, EndpointAddress remoteAddress)
			: base(endpointConfigurationName, remoteAddress)
		{
		}

		#endregion Constructors

		#region Methods

		#region Public

		public void CloseApplication()
		{
			Channel.CloseApplication();
		}

		public void ParentClosed()
		{
			Channel.ParentClosed();
		}

		public long GetMainWindowHandle()
		{
			return Channel.GetMainWindowHandle();
		}

		public void InvokeAction(string actionName, ParameterDictionary arguments)
		{
			Channel.InvokeAction(actionName, arguments);
		}

		public void ShowWindow()
		{
			Channel.ShowWindow();
		}

		#endregion Public

		#endregion Methods
	}
}