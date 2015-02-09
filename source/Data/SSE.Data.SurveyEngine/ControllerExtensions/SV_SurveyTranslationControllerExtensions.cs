using AR = SSE.Data.SurveyEngine.SV_SurveyTranslation;
using ARCollection = SSE.Data.SurveyEngine.SV_SurveyTranslationCollection;
using ARController = SSE.Data.SurveyEngine.SV_SurveyTranslationController;

namespace SSE.Data.SurveyEngine.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
    public static class SV_SurveyTranslationControllerExtensions
    {
        public static ARCollection GetBySurveyId(this ARController cntlr, int surveyId)
		{
			var qry = AR.Query()
				.WHERE(AR.Columns.SurveyId, surveyId);
			return cntlr.LoadCollection(qry);
        }
    }
}
