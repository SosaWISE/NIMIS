using NXS.DataServices.Crm;
using SOS.Lib.Util;
using System;

namespace WebModules.Crm.ContractAdmin
{
	public class HelpersModule : BaseModule
	{
		ContractAdminService Srv { get { return new ContractAdminService(this.User.GPEmployeeID); } }

		public HelpersModule()
			: base("/ContractAdmin/Helpers")
		{
			this.RequiresPermission(applicationID: null, actionID: null);

			Get["/NOC/{startDate:datetime}", true] = async (x, ct) =>
			{
				// date should be in UTC, but the kind is Unspecified
				var startDate = DateUtility.SpecifyUtcKind(((DateTime)x.startDate).Date);
				return await Srv.NocDate(startDate).ConfigureAwait(false);
			};
		}
	}
}
