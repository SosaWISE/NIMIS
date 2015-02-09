using AR = SOS.Data.SosCrm.Models.ExistingBarcodeResult;
using ARCollection = System.Collections.Generic.IList<SOS.Data.SosCrm.Models.ExistingBarcodeResult>;

namespace SOS.Data.SosCrm.Controllers
{
	public class ExistingBarcodeResultController : BaseModelController<AR>
	{
		public AR ExistingBarcode(int recruitID, BE_DocType.DocTypeEnum docType, string barcode)
		{
			return LoadSingleByProcedure(SosCrmDataStoredProcedureManager.BE_BarcodesExistingBarcode(recruitID, (int)docType, barcode)
			);
		}
	}
}
