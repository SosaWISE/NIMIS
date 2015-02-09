using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PPro.Framework.WpfFramework.ParentChildService
{
	public interface IParentWindow
	{
		void NotifyChildReady(Guid childID);
		void InvokeAction(string actionName, ParameterDictionary arguments);
	}
}