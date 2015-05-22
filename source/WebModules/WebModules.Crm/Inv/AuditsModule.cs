using NXS.DataServices.Crm;
using AuthApplications = NXS.Data.AuthenticationControl.AC_Application.MetaData;
using AuthActions = NXS.Data.AuthenticationControl.AC_Action.MetaData;
using NXS.DataServices.Crm.Models;

namespace WebModules.Crm.Inv
{
	public class AuditsModule : BaseModule
	{
		InventoryService Srv { get { return new InventoryService(this.User.GPEmployeeID); } }

		public AuditsModule()
			: base("/Inv/Audits")
		{
			this.RequiresPermission(applicationID: AuthApplications.InventoryScreenID, actionID: null);

			// create audit
			Post["/", true] = async (x, ct) =>
			{
				var inputItem = this.BindBody<IeAuditSave>();
				return await Srv.SaveAuditAsync(inputItem).ConfigureAwait(false);
			};
			// update audit
			Post["/{id}", true] = async (x, ct) =>
			{
				var inputItem = this.BindBody<IeAuditSave>();
				inputItem.ID = x.id;
				return await Srv.SaveAuditAsync(inputItem).ConfigureAwait(false);
			};
		}
	}
}
