using AR = NXS.Data.Licensing.LM_SalesRepRequirementsView;
using ARCollection = NXS.Data.Licensing.LM_SalesRepRequirementsViewCollection;
using ARController = NXS.Data.Licensing.LM_SalesRepRequirementsViewController;

namespace NXS.Data.Licensing.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class LM_SalesRepRequirementsViewControllerExtensions
	{
		public static ARCollection GetSalesRepCompliance(this ARController cntlr, string countryName, string stateName, string countyName, string cityName, string townshipName, string salesRepId)
		{
			return
				cntlr.LoadCollection(LicensingDataStoredProcedureManager.LM_RequirementsGetRepLicenseByLocation(countryName,
					stateName, countyName, cityName, townshipName, salesRepId));
		}
	}
}
