using NXS.Data.AuthenticationControl;
using NXS.DataServices.Crm;

namespace WebModules.Crm.Inv
{
	public class ProductBarcodeHistoryModule : BaseModule
	{
		#region .ctor
		public ProductBarcodeHistoryModule() : base("/Inv/ProductBarcodeHistory")
		{
			RequiresPermission(applicationID: AC_Application.MetaData.InventoryScreenID, actionID: null);

			Get["/{id}", true] = async (x, ct) =>
			{
				return await Srv.ProductBarcodeHistoryByID(x.id);

//				return await Srv.ProductBarcodesByIdAsync(productBarcodeId: (string)x.id).ConfigureAwait(false);
			};
		}
		#endregion .ctor

		#region Properties
		InventoryService Srv { get { return new InventoryService(User.GPEmployeeID); } }
		#endregion Properties

		#region Methods

		//private static IDictionary<string, string> ConvertDynamicDictionary(DynamicDictionary dictionary)
		//{
		//	return dictionary.GetDynamicMemberNames().ToDictionary(
		//			memberName => memberName,
		//			memberName => (string)dictionary[memberName]);
		//}

		#endregion Methods
	}
}
