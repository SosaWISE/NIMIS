using NXS.DataServices.Crm;
using NXS.DataServices.Crm.Models;

namespace WebModules.Crm.Ms
{
	public class PackagesModule : BaseModule
	{
		PackagesService Srv { get { return new PackagesService(this.User.GPEmployeeID); } }

		public PackagesModule()
			: base("/Ms/Packages")
		{
			this.RequiresPermission(applicationID: null, actionID: null);

			Get["/", true] = async (x, ct) =>
			{
				return await Srv.Packages().ConfigureAwait(false);
			};
		}
	}
}
