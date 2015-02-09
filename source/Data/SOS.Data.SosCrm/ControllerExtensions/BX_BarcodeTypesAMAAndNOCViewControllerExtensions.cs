using AR = SOS.Data.SosCrm.BX_BarcodeTypesAMAAndNOCView;
using ARCollection = SOS.Data.SosCrm.BX_BarcodeTypesAMAAndNOCViewCollection;
using ARController = SOS.Data.SosCrm.BX_BarcodeTypesAMAAndNOCViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class BX_BarcodeTypesAMAAndNOCViewControllerExtensions
	{
		public static AR GeneGenerateBarcodeNumbersAMA(this ARController cntlr, string amaBarcodeTypeID, long accountId)
		{
			return cntlr.LoadSingle(SosCrmDataStoredProcedureManager.BX_BarcodeTypesAMAAndNOCViewGenerateBarcodeNumbers(
				amaBarcodeTypeID, accountId));
		}
	}
}
