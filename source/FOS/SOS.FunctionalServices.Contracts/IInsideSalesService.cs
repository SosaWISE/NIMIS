using SOS.FunctionalServices.Contracts.Models;

namespace SOS.FunctionalServices.Contracts
{
	public interface IInsideSalesService : IFunctionalService
	{
		IFnsResult<bool> Send(long leadId, string gpEmployeeId);
	}
}
