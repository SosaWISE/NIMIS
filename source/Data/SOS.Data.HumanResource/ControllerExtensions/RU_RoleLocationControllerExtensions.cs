using System.Linq;
using SubSonic;
using AR = SOS.Data.HumanResource.RU_RoleLocation;
using ARCollection = SOS.Data.HumanResource.RU_RoleLocationCollection;
using ARController = SOS.Data.HumanResource.RU_RoleLocationController;

namespace SOS.Data.HumanResource.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class RU_RoleLocationControllerExtensions
	{
		public static ARCollection LoadByPrimaryKeys(this ARController controller, params int[] roleLocationIDs)
		{
			object[] ray = (from region in roleLocationIDs select (object)region).ToArray();
			Query qry = AR.Query()
				.WHERE(AR.Columns.RoleLocationID, Comparison.In, ray);

			return controller.LoadCollection(qry);
		}
		public static ARCollection LoadAll(this ARController controller)
		{
			Query qry = AR.Query();
			return controller.LoadCollection(qry);
		}
	}
}
