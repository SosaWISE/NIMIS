using System.Globalization;
using AR = SOS.Data.SosCrm.MC_AccountNotesAllInfoView;
using ARCollection = SOS.Data.SosCrm.MC_AccountNotesAllInfoViewCollection;
using ARController = SOS.Data.SosCrm.MC_AccountNotesAllInfoViewController;
	
namespace SOS.Data.SosCrm.ControllerExtensions
{
	// ReSharper disable once InconsistentNaming
	public static class MC_AccountNotesAllInfoViewControllerExtensions
	{
		public static ARCollection SearchNotes(this ARController cntlr, long? customerMasterFileId, long? customerId,
			long? leadId, int pageSize = 30, int pageNumber = 1)
		{
			return cntlr.LoadCollection(SosCrmDataStoredProcedureManager.MC_AccountNotesAllInfoViewGetByIds(customerMasterFileId, customerId, leadId, pageSize, pageNumber));
		}
		public static ARCollection GetByLeadId(this ARController oCntlr, long lLeadId, bool bAsc = false, int nPageSize = 30)
		{
			/** Initialized. */
			var oQry = AR.Query();
			var szOrderDirection = bAsc ? "ASC" : "DESC";
			oQry.WHERE(AR.Columns.LeadId, lLeadId).ORDER_BY(AR.Columns.CreatedOn, szOrderDirection);
			oQry.Top = nPageSize.ToString(CultureInfo.InvariantCulture);

			var oResult = oCntlr.LoadCollection(oQry);

			/** Return result. */
			return oResult;
		}

		public static ARCollection GetByCustomerId(this ARController oCntlr, long lCustomerId, bool bAsc = false, int nPageSize = 30)
		{
			/** Initialized. */
			var oQry = AR.Query();
			var szOrderDirection = bAsc ? "ASC" : "DESC";
			oQry.WHERE(AR.Columns.CustomerId, lCustomerId).ORDER_BY(AR.Columns.CreatedOn, szOrderDirection);
			oQry.Top = nPageSize.ToString(CultureInfo.InvariantCulture);

			var oResult = oCntlr.LoadCollection(oQry);

			/** Return result. */
			return oResult;
		}

		public static ARCollection GetByCustomerMasterFileId(this ARController oCntlr, long lCustomerMasterFileId, bool bAsc = false, int nPageSize = 30)
		{
			/** Initialized. */
			var oQry = AR.Query();
			var szOrderDirection = bAsc ? "ASC" : "DESC";
			oQry.WHERE(AR.Columns.CustomerMasterFileId, lCustomerMasterFileId).ORDER_BY(AR.Columns.CreatedOn, szOrderDirection);
			oQry.Top = nPageSize.ToString(CultureInfo.InvariantCulture);

			var oResult = oCntlr.LoadCollection(oQry);

			/** Return result. */
			return oResult;
		}

		public static AR GetByNoteId(this ARController oCntlr, long lNoteID)
		{
			/** Initialize. */
			var oQry = AR.Query();
			oQry.WHERE(AR.Columns.NoteID, lNoteID);

			var oResult = oCntlr.LoadSingle(oQry);

			/** Return result. */
			return oResult;
		}

	}
}
