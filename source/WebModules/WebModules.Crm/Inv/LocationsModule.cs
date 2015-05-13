using NXS.DataServices.Crm;
using AuthApplications = NXS.Data.AuthenticationControl.AC_Application.MetaData;
using AuthActions = NXS.Data.AuthenticationControl.AC_Action.MetaData;

namespace WebModules.Crm.Inv
{
	public class LocationsModule : BaseModule
	{
		InventoryService Srv { get { return new InventoryService(this.User.GPEmployeeID); } }

		public LocationsModule()
			: base("/Inv/Locations")
		{
			this.RequiresPermission(applicationID: AuthApplications.InventoryScreenID, actionID: null);

			Get["/{id}/Audits", true] = async (x, ct) =>
			{
				return await Srv.AuditsByLocationIdAsync(locationId: (string)x.id).ConfigureAwait(false);
			};
			Get["/{id}/ProductBarcodes", true] = async (x, ct) =>
			{
				return await Srv.ProductBarcodesByLocationIdAsync(locationId: (string)x.id).ConfigureAwait(false);
			};
		}
	}
}
