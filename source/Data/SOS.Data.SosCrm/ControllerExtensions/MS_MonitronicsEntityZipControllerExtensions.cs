using AR = SOS.Data.SosCrm.MS_MonitronicsEntityZip;
using ARCollection = SOS.Data.SosCrm.MS_MonitronicsEntityZipCollection;
using ARController = SOS.Data.SosCrm.MS_MonitronicsEntityZipController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class MS_MonitronicsEntityZipControllerExtensions
	{
		public static AR GetFirstByZipCode(this ARController cntlr, string zipCode)
		{
			return cntlr.LoadSingle(SosCrmDataStoredProcedureManager.MS_MonitronicsEntityZipGetByZipCode(zipCode));
		}
	}
}
