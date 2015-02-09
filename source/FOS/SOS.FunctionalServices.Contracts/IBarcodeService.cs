using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.DocBarcode;

namespace SOS.FunctionalServices.Contracts
{
    public interface IBarcodeService : IFunctionalService
    {
        IFnsResult<IFnsBxBarcode> BarcodeCreate(IFnsBxBarcode fnsBxBarcode, string gpEmployeeID);
    }
}
