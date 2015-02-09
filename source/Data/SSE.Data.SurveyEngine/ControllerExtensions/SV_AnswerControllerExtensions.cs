using SubSonic;
using AR = SSE.Data.SurveyEngine.SV_Answer;
using ARCollection = SSE.Data.SurveyEngine.SV_AnswerCollection;
using ARController = SSE.Data.SurveyEngine.SV_AnswerController;

namespace SSE.Data.SurveyEngine.ControllerExtensions {
    public static class SV_AnswerControllerExtensions {
        public static ARCollection ByResultID(this ARController ctlr, long resultID) {
            return ctlr.LoadCollection(ReadOnlyRecord<AR>.Query().WHERE(AR.Columns.ResultId, resultID));
        }
    }
}
