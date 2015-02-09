using System.Collections.Generic;
using SOS.Data;
using AR = NXS.Data.GreatPlains.Models.FilingStatus;

namespace NXS.Data.GreatPlains.Controllers
{
	public class FilingStatusController : BaseModelController<AR>
	{
		public IList<AR> GetAllFedFilingStatus()
		{
			return LoadCollectionByProcedure(
				GreatPlainsStoredProcedureManager.GetFedFilingStatus()
			);
		}
		public IList<AR> GetAllEicFilingStatus()
		{
			return LoadCollectionByProcedure(
				GreatPlainsStoredProcedureManager.GetEICFilingStatus()
			);
		}
		public IList<AR> GetAllFilingStatusForState(string stateAB)
		{
			return LoadCollectionByProcedure(
				GreatPlainsStoredProcedureManager.GetStateFilingStatus(stateAB)
			);
		}
	}
}
