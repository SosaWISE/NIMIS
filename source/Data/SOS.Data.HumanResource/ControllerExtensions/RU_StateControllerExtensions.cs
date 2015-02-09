using SubSonic;
using AR = SOS.Data.HumanResource.RU_State;
using ARCollection = SOS.Data.HumanResource.RU_StateCollection;
using ARController = SOS.Data.HumanResource.RU_StateController;

namespace SOS.Data.HumanResource.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class RU_StateControllerExtensions
	{
		//public static AR GetByStateAB(this ARController controller, string stateAB)
		//{
		//	Query qry = AR.Query().WHERE(AR.Columns.StateAbbreviation, stateAB);

		//	return controller.LoadSingle(qry);
		//}
		public static ARCollection LoadAllActive(this ARController controller)
		{
			Query qry = AR.Query()
				.AND(AR.Columns.IsActive, true);

			return controller.LoadCollection(qry);
		}
	}
}
