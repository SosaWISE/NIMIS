using SOS.Data.SosCrm.Controllers;

namespace SOS.Data.SosCrm
{
	public partial class SosCrmDataContext
	{
		#region Private Properties

		#endregion Private Properties

		#region Controllers Properties

		ActionPermissionController _actionPermissions;
		public ActionPermissionController ActionPermissions
		{
			get
			{
				return _actionPermissions
				  ?? (_actionPermissions = new ActionPermissionController());
			}
		}

		ExistingBarcodeResultController _existingBarcodeResults;
		public ExistingBarcodeResultController ExistingBarcodeResults
		{
			get
			{
				return _existingBarcodeResults
					?? (_existingBarcodeResults = new ExistingBarcodeResultController());
			}
		}

		AccountSearchController _accountSearchResults;
		public AccountSearchController AccountSearchResults
		{
			get
			{
				return _accountSearchResults
					?? (_accountSearchResults = new AccountSearchController());
			}
		}

		private CustomerSearchController _customerSearchResults;
		public CustomerSearchController CustomerSearchResults
		{
			get
			{
				return _customerSearchResults
					   ?? (_customerSearchResults = new CustomerSearchController());
			}
		}


		#endregion Controllers Properties
	}
}