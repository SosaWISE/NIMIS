using NXS.DataServices.Crm;
using NXS.DataServices.Crm.Models;

namespace WebModules.Crm.Ms
{
	public class PackagesModule : BaseModule
	{
		public PackagesModule()
			: base("/Ms/Packages")
		{
			this.RequiresPermission(applicationID: null, actionID: null);

			Get["/", true] = async (x, ct) =>
			{
				var srv = new PackagesService(this.User.GPEmployeeID);
				return await srv.Packages().ConfigureAwait(false);
			};
		}
	}
}
