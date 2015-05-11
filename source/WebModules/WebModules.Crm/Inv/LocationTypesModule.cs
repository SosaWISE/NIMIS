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
		}
	}
}
