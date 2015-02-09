using AR = SOS.Data.SosCrm.AE_ContractTemplate;
using ARCollection = SOS.Data.SosCrm.AE_ContractTemplateCollection;
using ARController = SOS.Data.SosCrm.AE_ContractTemplateController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class AE_ContractTemplateControllerExtensions
	{
		public static ARCollection ContractLengthsGet(this ARController cntlr, int invoiceTemplateId)
		{
			return
				cntlr.LoadCollection(SosCrmDataStoredProcedureManager.AE_ContractTemplatesGetByInvoiceTemplateId(invoiceTemplateId));
		}
	}
}
