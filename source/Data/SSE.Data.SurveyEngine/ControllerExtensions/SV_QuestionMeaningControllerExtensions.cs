using AR = SSE.Data.SurveyEngine.SV_QuestionMeaning;
using ARCollection = SSE.Data.SurveyEngine.SV_QuestionMeaningCollection;
using ARController = SSE.Data.SurveyEngine.SV_QuestionMeaningController;

namespace SSE.Data.SurveyEngine.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class SV_QuestionMeaningControllerExtensions
	{
		public static ARCollection GetBySurveyTypeId(this ARController cntlr, int surveyTypeId)
		{
			var qry = AR.Query()
				.WHERE(AR.Columns.SurveyTypeId, surveyTypeId);
			return cntlr.LoadCollection(qry);
		}
	}
}
