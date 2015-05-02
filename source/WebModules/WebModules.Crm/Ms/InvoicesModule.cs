using NXS.DataServices.Crm;
using NXS.DataServices.Crm.Models;

namespace WebModules.Crm.Ms
{
	public class InvoicesModule : BaseModule
	{
		AccountInvoicesService Srv { get { return new AccountInvoicesService(this.User.GPEmployeeID); } }

		public InvoicesModule()
			: base("/Ms/Invoices")
		{
			this.RequiresPermission(applicationID: null, actionID: null);

			Get["/Items", true] = async (x, ct) =>
			{
				return await Srv.Items().ConfigureAwait(false);
			};
			// update invoice
			Post["/{id:long}", true] = async (x, ct) =>
			{
				var inv = this.BindBody<AeInvoice>();
				inv.ID = x.id;
				return await Srv.SaveInvoice(inv).ConfigureAwait(false);
			};
		}
	}
}
