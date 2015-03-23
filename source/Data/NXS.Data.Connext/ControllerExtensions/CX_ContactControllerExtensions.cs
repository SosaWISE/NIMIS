using SubSonic;
using AR = NXS.Data.Connext.CX_Contact;
using ARCollection = NXS.Data.Connext.CX_ContactCollection;
using ARController = NXS.Data.Connext.CX_ContactController;

namespace NXS.Data.Connext.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class CX_ContactControllerExtensions
	{
		public static AR LoadByPrimaryKeySafe(this ARController cntlr, long contactId)
		{
			var qry = AR.Query()
				.WHERE(AR.Columns.ContactID, contactId)
				.WHERE(AR.Columns.IsActive, true)
				.WHERE(AR.Columns.IsDeleted, false);
			return cntlr.LoadSingle(qry);
		}

		public static ARCollection LoadBySalesRepId(this ARController cntlr, string gpEmployeeId)
		{
			return cntlr.LoadCollection(NxseConnextDataStoredProcedureManager.CX_ContactBySalesRepID(gpEmployeeId));
		}
	}
}
