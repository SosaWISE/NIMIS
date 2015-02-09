using System.Collections.Generic;
using SOS.Data;
using AR = NXS.Data.Accounting.Models.FilingStatus;

namespace NXS.Data.Accounting.Controllers
{
	public class FilingStatusController : BaseModelController<AR>
	{
		public IList<AR> GetAllFedFilingStatus()
		{
			return LoadCollectionByProcedure(
				AccountingDataStoredProcedureManager.Cust_GetFedFilingStatus()
			);
		}
		public IList<AR> GetAllEicFilingStatus()
		{
			return LoadCollectionByProcedure(
				AccountingDataStoredProcedureManager.Cust_GetEICFilingStatus()
			);
		}
		public IList<AR> GetAllFilingStatusForState(string stateAB)
		{
			return LoadCollectionByProcedure(
				AccountingDataStoredProcedureManager.Cust_GetStateFilingStatus(stateAB)
			);
		}
	}
}
