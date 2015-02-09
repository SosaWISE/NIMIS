using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace PPro.Framework.WpfFramework.ParentChildService
{
	public class ApplicationParentProxy : ClientBase<IApplicationParent>, IApplicationParent
	{
		#region Constructors

		public ApplicationParentProxy()
		{
		}

		public ApplicationParentProxy(string endpointConfigurationName)
			: base(endpointConfigurationName)
		{
		}

		public ApplicationParentProxy(string endpointConfigurationName, string remoteAddress)
			: base(endpointConfigurationName, remoteAddress)
		{
		}

		public ApplicationParentProxy(string endpointConfigurationName, EndpointAddress remoteAddress)
			: base(endpointConfigurationName, remoteAddress)
		{
		}

		#endregion Constructors

		#region Methods

		#region Public

		public void NotifyChildReady(Guid childID)
		{
			Channel.NotifyChildReady(childID);
		}

		public void InvokeAction(string actionName, ParameterDictionary arguments)
		{
			Channel.InvokeAction(actionName, arguments);
		}

		#endregion Public

		#endregion Methods
	}
}