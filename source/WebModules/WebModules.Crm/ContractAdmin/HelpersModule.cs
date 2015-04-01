using NXS.DataServices.Crm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.ModelBinding;
using NXS.DataServices.Crm.Models;
using SOS.Lib.Util;

namespace WebModules.Crm.ContractAdmin
{
	public class HelpersModule : BaseModule
	{
		public HelpersModule()
			: base("/ContractAdmin/Helpers")
		{
			this.RequiresPermission(applicationID: null, actionID: null);

			Get["/NOC/{startDate:datetime}", true] = async (x, ct) =>
			{
				var srv = new ContractAdminService(this.User.GPEmployeeID);
				// date should be in UTC, but the kind is Unspecified
				var startDate = DateUtility.SpecifyUtcKind(((DateTime)x.startDate).Date);
				return await srv.NocDate(startDate).ConfigureAwait(false);
			};
		}
	}
}
