using AR = SSE.Data.SurveyEngine.SV_QuestionTranslation;
using ARCollection = SSE.Data.SurveyEngine.SV_QuestionTranslationCollection;
using ARController = SSE.Data.SurveyEngine.SV_QuestionTranslationController;

namespace SSE.Data.SurveyEngine.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class SV_QuestionTranslationControllerExtensions
	{
		public static ARCollection GetBySurveyTranslationsId(this ARController cntlr, int surveyTranslationId)
		{
			var qry = AR.Query()
				.WHERE(AR.Columns.SurveyTranslationId, surveyTranslationId);
			return cntlr.LoadCollection(qry);
		}
	}
}
