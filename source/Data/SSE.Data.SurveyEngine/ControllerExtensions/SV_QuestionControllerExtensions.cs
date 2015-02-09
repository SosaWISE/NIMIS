using AR = SSE.Data.SurveyEngine.SV_Question;
using ARCollection = SSE.Data.SurveyEngine.SV_QuestionCollection;
using ARController = SSE.Data.SurveyEngine.SV_QuestionController;
 
namespace SSE.Data.SurveyEngine.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
    public static class SV_QuestionControllerExtensions
    {
        public static ARCollection GetBySurveyId(this ARController cntlr, int surveyId)
		{
			var qry = AR.Query()
				.WHERE(AR.Columns.SurveyId, surveyId)
				.ORDER_BY(AR.Columns.GroupOrder, "ASC");
			return cntlr.LoadCollection(qry);
        }
    }
}
