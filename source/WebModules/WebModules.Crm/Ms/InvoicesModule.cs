using NXS.DataServices.Crm;
using NXS.DataServices.Crm.Models;

namespace WebModules.Crm.Ms
{
	public class InvoicesModule : BaseModule
	{
		public InvoicesModule()
			: base("/Ms/Invoices")
		{
			this.RequiresPermission(applicationID: null, actionID: null);

			Get["/Items", true] = async (x, ct) =>
			{
				var srv = new AccountInvoicesService(this.User.GPEmployeeID);
				return await srv.Items().ConfigureAwait(false);
			};
			// update invoice
			Post["/{id:long}", true] = async (x, ct) =>
			{
				var srv = new AccountInvoicesService(this.User.GPEmployeeID);
				var inv = this.BindBody<AeInvoice>();
				inv.ID = x.id;
				return await srv.SaveInvoice(inv).ConfigureAwait(false);
			};
		}
	}
}
