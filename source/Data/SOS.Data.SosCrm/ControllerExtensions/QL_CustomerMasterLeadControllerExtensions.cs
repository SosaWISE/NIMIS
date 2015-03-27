using AR = SOS.Data.SosCrm.QL_CustomerMasterLead;
using ARCollection = SOS.Data.SosCrm.QL_CustomerMasterLeadCollection;
using ARController = SOS.Data.SosCrm.QL_CustomerMasterLeadController;

namespace SOS.Data.SosCrm
{
	public static class QL_CustomerMasterLeadControllerExtensions
	{
		public static ARCollection ByCmfID(this ARController cntlr, long cmfid)
		{
			var qry = AR.Query().WHERE(AR.Columns.CustomerMasterFileId, cmfid);
			return cntlr.LoadCollection(qry);
		}
	}
}
