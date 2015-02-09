using AR = SOS.Data.SosCrm.MS_TimeZoneLookup;
using ARCollection = SOS.Data.SosCrm.MS_TimeZoneLookupCollection;
using ARController = SOS.Data.SosCrm.MS_TimeZoneLookupController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable InconsistentNaming
	public static class MS_TimeZoneLookupControllerExtensions
// ReSharper restore InconsistentNaming
	{
		public static ARCollection GetByStateAB(this ARController cntlr, string stateAB)
		{
			// ** Initialize
			var result = cntlr.LoadCollection(SosCrmDataStoredProcedureManager.MS_TimeZoneLookupGetByStateAB(stateAB));

			// ** Return result
			return result;
		}
	}
}
