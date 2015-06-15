using NXS.DataServices.Sales;
using NXS.Lib;
using NXS.Lib.Authentication;
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
		BlahService Srv { get { return new BlahService(this.User.GPEmployeeID); } }

		public SystemTypesModule()
			: base("/Sales/SystemTypes")
		{
			this.RequiresPermission((string)null, null);

			Get["/", true] = async (x, ct) =>
			{
				var officeid = 0;
				return (await Srv.SystemTypesAsync(officeid).ConfigureAwait(false));
			};
		}
	}
}
