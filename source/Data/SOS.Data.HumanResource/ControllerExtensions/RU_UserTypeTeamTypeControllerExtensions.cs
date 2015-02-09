using System.Collections.Generic;
using System.Linq;
using SOS.Lib.Util;
using SubSonic;
using AR = SOS.Data.HumanResource.RU_UserTypeTeamType;
using ARCollection = SOS.Data.HumanResource.RU_UserTypeTeamTypeCollection;
using ARController = SOS.Data.HumanResource.RU_UserTypeTeamTypeController;

namespace SOS.Data.HumanResource.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class RU_UserTypeTeamTypeControllerExtensions
	{
		private static readonly CachedList<AR> _all = new CachedList<AR>(() => new ARController().LoadAll().ToList());
		public static List<AR> LoadAllCached(this ARController controller)
		{
			return _all.List;
		}

		public static ARCollection LoadAll(this ARController controller)
		{
			Query qry = AR.Query();

			return controller.LoadCollection(qry);
		}
	}
}
