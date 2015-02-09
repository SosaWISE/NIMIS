using AR= SOS.Data.SosCrm.SAE_CreditReportAbara;
using ARCollection= SOS.Data.SosCrm.SAE_CreditReportAbaraCollection;
using ARController= SOS.Data.SosCrm.SAE_CreditReportAbaraController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class SAE_CreditReportAbaraControllerExtensions
	{
		public static AR GetByRandomNumber(this ARController cntlr, int rndNumber)
		{
			return cntlr.LoadSingle(SosCrmDataStoredProcedureManager.SAE_CreditReportAbaraGetByRandomNumber(rndNumber));
		}
	}
}
