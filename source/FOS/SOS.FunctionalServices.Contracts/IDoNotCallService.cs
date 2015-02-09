/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 04/17/2014
 * Time: 09:20
 * 
 * Description:  Describes the Do Not calls service.
 *********************************************************************************************************************/

using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.DoNotCall;

namespace SOS.FunctionalServices.Contracts
{
    public interface IDoNotCallService: IFunctionalService
    {
        IFnsResult<IFnsDncPhoneNumber> PhoneNumberRead(IFnsDncPhoneNumber fnsDncPhoneNumber, string phoneNumber);
    }
}
