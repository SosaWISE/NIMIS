/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 05/25/12
 * Time: 08:00
 * 
 * Description:  Service that will process a credit card.
 *********************************************************************************************************************/

using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.Merchant;

namespace SOS.FunctionalServices.Contracts
{
	public interface IMerchantService : IFunctionalService
	{
		IFnsResult<IFnsMerchResponseModel> CcProcess(IFnsInvoicePaymentInfoModel oFnsInvoicePaymentInfoModel, string sUserId);

		IFnsResult<IFnsMerchResponseModel> ECheck(IFnsInvoicePaymentInfoModel oFnsInvoicePaymentInfoModel, string sUserId);
	}
}