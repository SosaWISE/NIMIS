using System.Collections.Generic;
using SOS.Data;
using AR = NXS.Data.GreatPlains.Models.WorkersComp;

namespace NXS.Data.GreatPlains.Controllers
{
	public class WorkersCompController : BaseModelController<AR>
	{
		public IList<AR> GetAllWorksComp()
		{
			return LoadCollectionByProcedure(
				GreatPlainsStoredProcedureManager.GetWorkersComp()
			);
		}
	}
}
