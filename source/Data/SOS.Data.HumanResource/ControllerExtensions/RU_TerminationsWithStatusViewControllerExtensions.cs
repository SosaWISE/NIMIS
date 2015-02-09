using SubSonic;

namespace SOS.Data.HumanResource.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class RU_TerminationsWithStatusViewControllerExtensions
	{
		public static RU_TerminationsWithStatusView LoadByPrimaryKey(this RU_TerminationsWithStatusViewController controller, int terminationID)
		{
			Query qry = RU_TerminationsWithStatusView.Query()
				.WHERE(RU_TerminationsWithStatusView.Columns.TerminationID, terminationID);

			return controller.LoadSingle(qry);
		}
		public static RU_TerminationsWithStatusViewCollection GetAllForRecruit(this RU_TerminationsWithStatusViewController controller, int recruitID)
		{
			Query qry = RU_TerminationsWithStatusView.Query()
				.WHERE(RU_TerminationsWithStatusView.Columns.RecruitID, recruitID)
				.ORDER_BY(RU_TerminationsWithStatusView.Columns.CreatedByDate, "DESC");

			return	controller.LoadCollection(qry);
		}
		public static RU_TerminationsWithStatusViewCollection GetTerminationsPendingApproval(this RU_TerminationsWithStatusViewController controller)
		{
			Query qry = RU_TerminationsWithStatusView.Query()
				.WHERE(RU_TerminationsWithStatusView.Columns.TerminationStatusCodeID, (int)RU_TerminationStatusCode.TerminationStatusCodeEnum.Open)
				.OR(RU_TerminationsWithStatusView.Columns.TerminationStatusCodeID, (int)RU_TerminationStatusCode.TerminationStatusCodeEnum.Rejected)
				.ORDER_BY(RU_TerminationsWithStatusView.Columns.LastDateWorked)
				.ORDER_BY(RU_TerminationsWithStatusView.Columns.CreatedByDate);

			return controller.LoadCollection(qry);
		}
		public static RU_TerminationsWithStatusViewCollection GetTerminationsByRecruit(this RU_TerminationsWithStatusViewController controller, int recruitID)
		{
			Query qry = RU_TerminationsWithStatusView.Query()
				.WHERE(RU_TerminationsWithStatusView.Columns.RecruitID, recruitID)
				.ORDER_BY(RU_TerminationsWithStatusView.Columns.CreatedByDate, "DESC");

			return controller.LoadCollection(qry);
		}
	}
}
