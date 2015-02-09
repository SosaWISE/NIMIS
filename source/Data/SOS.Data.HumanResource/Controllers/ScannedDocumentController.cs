using AR = SOS.Data.HumanResource.Models.ScannedDocument;
using ARCollection = System.Collections.Generic.IList<SOS.Data.HumanResource.Models.ScannedDocument>;
using ARController = SOS.Data.HumanResource.Controllers.ScannedDocumentController;

namespace SOS.Data.HumanResource.Controllers
{
	public class ScannedDocumentController : BaseModelController<AR>
	{
		public AR LoadByPrimaryKey(int nDocumentID)
		{
			return LoadSingleByProcedure(HumanResourceDataStoredProcedureManager.DocLinkGetDocumentsByID(nDocumentID)
			);
		}
	}
}
