using SubSonic;
using AR = SSE.Data.SurveyEngine.SV_ResultsView;
using ARCollection = SSE.Data.SurveyEngine.SV_ResultsViewCollection;
using ARController = SSE.Data.SurveyEngine.SV_ResultsViewController;

namespace SSE.Data.SurveyEngine.ControllerExtensions {
    public static class SV_ResultsViewControllerExtensions {
        public static AR LoadByPrimaryKey(this ARController ctlr, long id) {
            return ctlr.LoadSingle(ReadOnlyRecord<AR>.Query().WHERE(AR.Columns.ResultID, id));
        }
        public static ARCollection ByAccountID(this ARController ctlr, long accountID) {
            return ctlr.LoadCollection(ReadOnlyRecord<AR>.Query().WHERE(AR.Columns.AccountId, accountID));
        }
    }
}
