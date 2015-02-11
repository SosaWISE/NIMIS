using SOS.FunctionalServices.Contracts.Models;
using SOS.Lib.Core;

namespace SOS.FunctionalServices.Contracts {
    public interface ICellStationService : IFunctionalService {
		Result<object> Register(long accountID, string serialNumber, bool enableTwoWay, string gpTechId, string gpEmployeeId);
		Result<object> GetEquipmentList(long accountID);
		Result<bool> SwapModem(long accountID, string newSerialNumber, string swapReason, string specialRequest, bool restoreBackedUpSettingsAfterSwap);

		Result<bool> Unregister(long accountID);
		Result<object> AccountStatus(long accountID);

		Result<bool> ChangeServicePackage(long accountID, string gpEmployeeID, string newCellPackageItemId);
    }
}