/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 04/01/15
 * Time: 20:07
 * 
 * Description:  This service will expose interfaces to the Licensing Manger.
 *********************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using NXS.Data.Licensing;
using NXS.Data.Licensing.ControllerExtensions;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Helper;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.Licensing;
using SOS.FunctionalServices.Models;
using SOS.FunctionalServices.Models.Licensing;
using SOS.Lib.Core.ErrorHandling;

namespace SOS.FunctionalServices
{
	public class LicencingManagementService : ILicencingManagementService
	{
		public IFnsResult<List<IFnsLmSalesRepRequirementsView>> SalesRepComplianceGet(string salesRepId, int dealerId, string countryId, string stateId, string countyName,
			string cityName, string townshipName, string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "FNS SalesRepComplianceGet";
			var result = new FnsResult<List<IFnsLmSalesRepRequirementsView>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				var countryName = SosCrmDataContext.Instance.MC_PoliticalCountries.LoadByPrimaryKey(countryId);
				var stateName = SosCrmDataContext.Instance.MC_PoliticalStates.LoadByPrimaryKey(stateId);
				var lmSalesRepRequirementsViewCol = LicensingDataContext.Instance.LM_SalesRepRequirementsViews.GetSalesRepCompliance(countryName.CountryName, stateName.StateName, countyName, cityName, townshipName, salesRepId, dealerId);
				var fnsList = lmSalesRepRequirementsViewCol.Select(feCriteria => new FnsLmSalesRepRequirementsView(feCriteria)).Cast<IFnsLmSalesRepRequirementsView>().ToList();

				result.Message = BaseErrorCodes.ErrorCodes.Success.Message();
				result.Code = BaseErrorCodes.ErrorCodes.Success.Code();
				result.Value = fnsList;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsLmSalesRepRequirementsView>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}
	}
}
