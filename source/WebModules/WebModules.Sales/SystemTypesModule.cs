using NXS.DataServices.Sales;
using NXS.Lib.Web;
using NXS.Lib.Web.Authentication;
using SOS.Lib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebModules.Sales
{
	public class SystemTypesModule : BaseModule
	{
		BlahService Srv { get { return new BlahService(/*this.User.GPEmployeeID*/); } }

		public SystemTypesModule()
			//: base("/SystemType", "/ng")
			: base("/Sales/SystemTypes")
		{
			//$http.get('ng/SystemType/get_system_types')
			//Get["/get_system_types", true] = async (x, ct) =>
			Get["/", true] = async (x, ct) =>
			{
				var officeid = 0;
				//return (await Srv.SystemTypesAsync(officeid).ConfigureAwait(false)).Value;
				return (await Srv.SystemTypesAsync(officeid).ConfigureAwait(false));
			};
		}
	}
}
