/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 05/25/12
 * Time: 08:00
 * 
 * Description:  Service that will process a credit card.
 *********************************************************************************************************************/

using System.ServiceModel;
using System.ServiceModel.Activation;
using SOS.FOS.MerchantServices;
using SOS.FOS.MerchantServices.Interfaces;
using SOS.FOS.MerchantServices.Models;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.Merchant;
using SOS.FunctionalServices.Models;
using SOS.FunctionalServices.Models.Merchant;
using SSE.Lib.Interfaces;
using SSE.Lib.Interfaces.FOS;

namespace SOS.FunctionalServices
{
	[ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.NotAllowed)]
	public class MerchantService : IMerchantService
	{
		#region Implementation of IMerchantService

		/// <summary>
		/// At some point this module will discrimiate between MG's.  For now there is only one.
		/// </summary>
		/// <param name="oFnsInvoicePaymentInfoModel">IFnsInvoicePaymentInfoModel</param>
		/// <param name="sUserId">string</param>
		/// <returns>IFnsResult</returns>
		public IFnsResult<IFnsMerchResponseModel> CcProcess(IFnsInvoicePaymentInfoModel oFnsInvoicePaymentInfoModel, string sUserId)
		{
			/** Initialize. */
			var oFosMain = new Main();
			FosInvoicePaymentInfoModel oFosInvPayModel = ((FnsInvoicePaymentInfoModel)oFnsInvoicePaymentInfoModel).CastToFos();

			/** Process. */
			IFosResult<IFosMerchResponseModel> oFosResult = oFosMain.CcProcess(oFosInvPayModel, sUserId);
			var oResult = new FnsResult<IFnsMerchResponseModel>
				{
					Code = oFosResult.Code,
					Message = oFosResult.Message,
					Value = new FnsMerchResponseModel((IFosMerchResponseModel) oFosResult.GetValue())
				};

			/** Return result. */
			return oResult;
		}

		public IFnsResult<IFnsMerchResponseModel> ECheck(IFnsInvoicePaymentInfoModel oFnsInvoicePaymentInfoModel, string sUserId)
		{
			/** Initialize. */
			var oFosMain = new Main();
			FosInvoicePaymentInfoModel oFosInvPayModel = ((FnsInvoicePaymentInfoModel)oFnsInvoicePaymentInfoModel).CastToFos();

			/** Process. */
			IFosResult<IFosMerchResponseModel> oFosResult = oFosMain.ECheck(oFosInvPayModel, sUserId);
			var oResult = new FnsResult<IFnsMerchResponseModel>
				{
					Code = oFosResult.Code,
					Message = oFosResult.Message,
					Value = new FnsMerchResponseModel((IFosMerchResponseModel) oFosResult.GetValue())
				};

			/** Return result. */
			return oResult;
		}

		#endregion Implementation of IMerchantService
	}
}