using AR = SOS.Data.SosCrm.MC_AccountNote;
using ARCollection = SOS.Data.SosCrm.MC_AccountNoteCollection;
using ARController = SOS.Data.SosCrm.MC_AccountNoteController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class MC_AccountNoteControllerExtensions
	{
		public static ARCollection SearchNotes(this ARController cntlr, long? customerMasterFileId, long? customerId,
			long? leadId, int pageSize = 30, int pageNumber = 1)
		{
			return cntlr.LoadCollection(SosCrmDataStoredProcedureManager.MC_AccountNotesGetByIds(customerMasterFileId, customerId, leadId, pageSize, pageNumber));
		}
	}
}
