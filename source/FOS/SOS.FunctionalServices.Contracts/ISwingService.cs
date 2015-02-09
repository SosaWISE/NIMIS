/**********************************************************************************************************************
 * User: Reagan/Junryll
 * Date: 04/22/2014
 * Time: 04:14 pm
 * 
 * Description:  Describes the Swing Service for SOS.
 *********************************************************************************************************************/

using System.Collections.Generic;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.Swing;

namespace SOS.FunctionalServices.Contracts
{
    public interface  ISwingService:IFunctionalService
    {
        IFnsResult<IFnsCustomerSwingInfo> CustomerSwingInfoRead(long interimAccountId, string customerType);
        IFnsResult<IFnsCustomerSwingPremiseAddress> CustomerSwingPremiseAddressRead(long interimAccountId);
        IFnsResult<List<IFnsCustomerSwingEmergencyContact>> CustomerSwingEmergencyContactRead(long interimAccountId);
        IFnsResult<List<IFnsCustomerSwingEquipmentInfo>> CustomerSwingEquipmentInfoRead(long interimAccountId);
        IFnsResult<IFnsCustomerSwingInterim> CustomerSwingInterimPost(long interimAccountId, bool swingEquipment);
        IFnsResult<IFnsCustomerSwingSystemDetails> CustomerSwingSystemDetailsRead(long interimAccountId);
        IFnsResult<IFnsCustomerSwungInfo> CustomerSwungInfoRead(long interimAccountId);
        IFnsResult<IFnsCustomerSwingAddDnc> CustomerSwingAddDncRead(string phoneNumber);
    }
}
