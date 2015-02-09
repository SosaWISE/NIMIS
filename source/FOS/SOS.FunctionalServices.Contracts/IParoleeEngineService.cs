using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.Parolee;

namespace SOS.FunctionalServices.Contracts
{
	public interface IParoleeEngineService : IFunctionalService
	{
		IFnsResult<IFnsParoleeOfficer> OfficerCreate(IFnsParoleeOfficer officer);
		IFnsResult<IFnsParoleeOfficer> OfficerUpdate(IFnsParoleeOfficer officer);
		IFnsResult<IFnsParoleeOfficer> OfficerDelete(int officerID);
		IFnsResult<IFnsParoleeOfficer> OfficerRead(int officerID);
	}
}