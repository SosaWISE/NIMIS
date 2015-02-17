using System.Collections.Generic;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.Reporting;

namespace SOS.FunctionalServices.Contracts
{
	public interface IReportingService : IFunctionalService
	{
		IFnsResult<List<IFnsMsAccountOnlineStatusInfo>> GetAccountOnlineStatusInfoByAccountId(long accountId, string gpEmployeeId);
	}
}