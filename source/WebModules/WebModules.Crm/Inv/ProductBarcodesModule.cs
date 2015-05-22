using NXS.DataServices.Crm;
using AuthApplications = NXS.Data.AuthenticationControl.AC_Application.MetaData;
using AuthActions = NXS.Data.AuthenticationControl.AC_Action.MetaData;

namespace WebModules.Crm.Inv
{
	public class ProductBarcodesModule : BaseModule
	{
		InventoryService Srv { get { return new InventoryService(this.User.GPEmployeeID); } }

		public ProductBarcodesModule()
			: base("/Inv/ProductBarcodes")
		{
			this.RequiresPermission(applicationID: AuthApplications.InventoryScreenID, actionID: null);

			Get["/{id}", true] = async (x, ct) =>
			{
				return await Srv.ProductBarcodesByIdAsync(productBarcodeId: (string)x.id).ConfigureAwait(false);
			};
		}
	}
}
