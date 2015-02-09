using System.Collections.Generic;
using System.Linq;
using SOS.Lib.Util;
using SubSonic;
using AR = SOS.Data.HumanResource.RU_UserType;
using ARCollection = SOS.Data.HumanResource.RU_UserTypeCollection;
using ARController = SOS.Data.HumanResource.RU_UserTypeController;

namespace SOS.Data.HumanResource.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class RU_UserTypeControllerExtensions
	{
		private static readonly CachedList<AR> _all = new CachedList<AR>(() => new ARController().LoadAll().ToList());
		public static List<AR> LoadAllCached(this ARController controller)
		{
			return _all.List;
		}

		public static ARCollection LoadAll(this ARController controller)
		{
			Query qry = AR.Query()
				.Order();

			return controller.LoadCollection(qry);
		}
		public static ARCollection GetByRoleLocationID(this ARController controller, int roleLocationID)
		{
			Query qry = AR.Query()
				.WHERE(AR.Columns.RoleLocationID, Comparison.Equals, roleLocationID)
				.Order();

			return controller.LoadCollection(qry);
		}

		#region Private Helper Extensions

		private static Query Order(this Query qry)
		{
			qry.ORDER_BY(string.Format("{0} ASC, {1} DESC, {2} ASC", AR.Columns.RoleLocationID, AR.Columns.ReportingLevel, AR.Columns.Description));
			return qry;
		}

		#endregion //Private Helper Methods
	}
}
