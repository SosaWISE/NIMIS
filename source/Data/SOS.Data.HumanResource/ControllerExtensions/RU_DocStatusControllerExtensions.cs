using System.Collections.Generic;
using System.Linq;
using SOS.Lib.Util;
using SubSonic;
using AR = SOS.Data.HumanResource.RU_DocStatus;
using ARCollection = SOS.Data.HumanResource.RU_DocStatusCollection;
using ARController = SOS.Data.HumanResource.RU_DocStatusController;

namespace SOS.Data.HumanResource.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class RU_DocStatusControllerExtensions
	{
		private static readonly CachedList<AR> All = new CachedList<AR>(() => new ARController().LoadAll().ToList());
		public static List<AR> LoadAllCached(this ARController controller)
		{
			return All.List;
		}

		public static ARCollection LoadAll(this ARController controller)
		{
			Query qry = AR.Query();
			return controller.LoadCollection(qry);
		}
	}

}
