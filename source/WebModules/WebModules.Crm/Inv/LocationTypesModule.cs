using NXS.DataServices.Crm;
using AuthApplications = NXS.Data.AuthenticationControl.AC_Application.MetaData;
using AuthActions = NXS.Data.AuthenticationControl.AC_Action.MetaData;

namespace WebModules.Crm.Inv
{
	public class LocationTypesModule : BaseModule
	{
		InventoryService Srv { get { return new InventoryService(this.User.GPEmployeeID); } }

		public LocationTypesModule()
			: base("/Inv/LocationTypes")
		{
			this.RequiresPermission(applicationID: AuthApplications.InventoryScreenID, actionID: null);

			Get["/", true] = async (x, ct) =>
			{
				return await Srv.LocationTypesAsync().ConfigureAwait(false);
			};
			Get["/{id}/Locations", true] = async (x, ct) =>
			{
				return await Srv.LocationsByLocationTypeIdAsync(locationTypeId: (string)x.id).ConfigureAwait(false);
			};
			Get["/{id}/Locations/{locationId}/Audits", true] = async (x, ct) =>
			{
				return await Srv.AuditsByLocationAsync((string)x.locationId, locationTypeId: (string)x.id).ConfigureAwait(false);
			};
			Get["/{id}/Locations/{locationId}/ProductBarcodes", true] = async (x, ct) =>
			{
				return await Srv.ProductBarcodesByLocationAsync((string)x.locationId, locationTypeId: (string)x.id).ConfigureAwait(false);
			};

			Post["/Technician/Locations/{userId:int}/Reconcile", true] = async (x, ct) =>
			{
				return await Srv.ReconcileTekEquipmentAsync((int)x.userId).ConfigureAwait(false);
			};
		}
	}
}
