using SubSonic;
using AR = NXS.Data.Licensing.LM_RequirementType;
using ARCollection = NXS.Data.Licensing.LM_RequirementTypeCollection;
using ARController = NXS.Data.Licensing.LM_RequirementTypeController;

namespace NXS.Data.Licensing.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class LM_RequirementTypeControllerExtensions
	{
		public static ARCollection LoadAll(this ARController controller)
		{
			Query qry = AR.Query();
			return controller.LoadCollection(qry);
		}
	}
}
