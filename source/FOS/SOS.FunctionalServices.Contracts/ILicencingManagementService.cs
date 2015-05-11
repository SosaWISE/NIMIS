/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 04/01/15
 * Time: 20:07
 * 
 * Description:  This service will expose interfaces to the Licensing Manger.
 *********************************************************************************************************************/

using System.Collections.Generic;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.Licensing;

namespace SOS.FunctionalServices.Contracts
{
	public interface ILicencingManagementService : IFunctionalService
	{
		IFnsResult<List<IFnsLmSalesRepRequirementsView>> SalesRepComplianceGet(string salesRepId, int dealerId, string countryName, string stateName,
			string countyName, string cityName, string townshipName, string gpEmployeeId);
	}
}