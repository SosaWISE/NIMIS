using SubSonic;
using AR = SOS.Data.HumanResource.RU_TerminationNote;
using ARCollection = SOS.Data.HumanResource.RU_TerminationNoteCollection;
using ARController = SOS.Data.HumanResource.RU_TerminationNoteController;

namespace SOS.Data.HumanResource.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class RU_TerminationNoteControllerExtensions
	{
		public static ARCollection GetNotesForTermination(this ARController controller, int terminationID)
		{
			Query qry = AR.Query()
				.WHERE(AR.Columns.TerminationID, terminationID)
				.ORDER_BY(AR.Columns.CreatedOn, "DESC");

			return controller.LoadCollection(qry);
		}
	}
}
