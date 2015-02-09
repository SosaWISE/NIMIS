using SubSonic;
using AR = NXS.Data.Licensing.LM_Note;
using ARCollection = NXS.Data.Licensing.LM_NoteCollection;
using ARController = NXS.Data.Licensing.LM_NoteController;

namespace NXS.Data.Licensing.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class LM_NoteControllerExtensions
	{
		public static ARCollection GetForUser(this ARController controller, int userID)
		{
			Query qry = AR.Query()
				.WHERE(AR.Columns.ForeignKeyID, userID)
				.AND(AR.Columns.NoteTypeID, (int)LM_NoteType.NoteTypeEnum.User);

			return controller.LoadCollection(qry);
		}
	}
}
