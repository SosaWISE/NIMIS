using AR = SSE.Data.SurveyEngine.SV_Survey;
using ARCollection = SSE.Data.SurveyEngine.SV_SurveyCollection;
using ARController = SSE.Data.SurveyEngine.SV_SurveyController;

namespace SSE.Data.SurveyEngine.ControllerExtensions
{
	// ReSharper disable InconsistentNaming
	public static class SV_SurveyControllerExtensions
	// ReSharper restore InconsistentNaming
	{
		public static ARCollection GetBySurveyTypeId(this ARController cntlr, int surveyTypeId)
		{
			var qry = AR.Query()
				.WHERE(AR.Columns.SurveyTypeId, surveyTypeId);
			return cntlr.LoadCollection(qry);
		}
		public static ARCollection Current(this ARController cntlr, int surveyTypeId)
		{
			var qry = AR.Query()
				.WHERE(AR.Columns.SurveyTypeId, surveyTypeId)
				.WHERE(AR.Columns.IsCurrent, true)
				.ORDER_BY(AR.Columns.SurveyID, "DESC");
			return cntlr.LoadCollection(qry);
		}
	}
}
