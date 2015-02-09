/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 02/28/12
 * Time: 10:18
 * 
 * Description:  Describes the Authentication Service for SOS.
 *********************************************************************************************************************/
using SOS.FunctionalServices.Contracts.Models;

namespace SOS.FunctionalServices.Contracts
{
	public interface IEmailSupportService : IFunctionalService
	{
		bool SendSignupConfirmation();
	}
}