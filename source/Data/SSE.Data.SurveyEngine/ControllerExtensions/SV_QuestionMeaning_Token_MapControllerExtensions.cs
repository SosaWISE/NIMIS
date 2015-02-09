using AR = SSE.Data.SurveyEngine.SV_QuestionMeanings_Tokens_Map;
using ARCollection = SSE.Data.SurveyEngine.SV_QuestionMeanings_Tokens_MapCollection;
using ARController = SSE.Data.SurveyEngine.SV_QuestionMeanings_Tokens_MapController;

namespace SSE.Data.SurveyEngine.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class SV_QuestionMeaning_Token_MapControllerExtensions
	{
		public static ARCollection GetByQuestionMeaningId(this ARController cntlr, int questionMeaningId)
		{
			var qry = AR.Query()
				.WHERE(AR.Columns.QuestionMeaningId, questionMeaningId)
				.ORDER_BY(AR.Columns.CreatedOn, "ASC");
			return cntlr.LoadCollection(qry);
		}
	}
}
