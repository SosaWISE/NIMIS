using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NXS.Data.Licensing.Models;
using SOS.Data;

namespace NXS.Data.Licensing.Controllers
{
	public class MetAndNeededRequirementController : BaseModelController<MetAndNeededRequirement>
	{
		public IList<MetAndNeededRequirement> GetRequirementsMetAndNeeded(MetAndNeededRequirementSearchInfo searchInfo)
		{
			return LoadCollectionByProcedure(LicensingDataStoredProcedureManager.GetRequirementsMetAndNeeded(searchInfo.CompanyID, (int)searchInfo.RequirementType, searchInfo.LocationName)
			);
		}
	}
}
