using SubSonic;
using AR = SOS.Data.SosCrm.BE_DocType;
using ARCollection = SOS.Data.SosCrm.BE_DocTypeCollection;
using ARController = SOS.Data.SosCrm.BE_DocTypeController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class BE_DocTypeControllerExtensions
	{
		public static ARCollection GetByDocTypeColumn(this ARController controller, int docTypeColumnID)
		{
			Query qry = AR.Query()
				.WHERE(AR.Columns.DocTypeColumnId, docTypeColumnID);

			ARCollection result = controller.LoadCollection(qry);
			return result;
		}
	}
}
