using SubSonic;
using AR = SOS.Data.HumanResource.RU_Migration;
using ARCollection = SOS.Data.HumanResource.RU_MigrationCollection;
using ARController = SOS.Data.HumanResource.RU_MigrationController;

namespace SOS.Data.HumanResource.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class RU_MigrationControllerExtensions
	{
		public static AR Latest(this ARController controller)
		{
			Query qry = AR.Query()
				.ORDER_BY(AR.Columns.CreatedOn, "DESC");

			qry.Top = "1";

			return controller.LoadSingle(qry);
		}
	}
}
