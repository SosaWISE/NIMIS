using System.Collections.Generic;
using SOS.Data;
using AR = NXS.Data.Accounting.Models.WorkersComp;
using ARCollection = System.Collections.Generic.IList<NXS.Data.Accounting.Models.WorkersComp>;
using ARController = NXS.Data.Accounting.Controllers.WorkersCompController;

namespace NXS.Data.Accounting.Controllers
{
	public class WorkersCompController : BaseModelController<AR>
	{
		public IList<AR> GetAllWorksComp()
		{
			return LoadCollectionByProcedure(AccountingDataStoredProcedureManager.Cust_GetWorkersComp()
			);
		}
	}
}
