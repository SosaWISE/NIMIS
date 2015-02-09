using SubSonic;
using AR = SSE.Data.SurveyEngine.SV_Questions_PossibleAnswers_Map;
using ARCollection = SSE.Data.SurveyEngine.SV_Questions_PossibleAnswers_MapCollection;
using ARController = SSE.Data.SurveyEngine.SV_Questions_PossibleAnswers_MapController;

namespace SSE.Data.SurveyEngine.ControllerExtensions
{
// ReSharper disable InconsistentNaming
	public static class SV_Questions_PossibleAnswers_MapControllerExtensions
// ReSharper restore InconsistentNaming
	{
		public static AR LoadByPrimaryKey(this ARController ocntlr, int questionId, int possibleAnswerId)
		{
			var qry = AR.Query()
				.WHERE(AR.Columns.QuestionId, questionId)
				.WHERE(AR.Columns.PossibleAnswerId, possibleAnswerId);
			return ocntlr.LoadSingle(qry);
		}

		public static ARCollection GetByQuestionId(this ARController cntlr, int questionId)
		{
			var qry = AR.Query()
				.WHERE(AR.Columns.QuestionId, questionId)
				.ORDER_BY(AR.Columns.CreatedOn, "ASC");
			return cntlr.LoadCollection(qry);
		}

		public static void Update(this ARController cntlr, AR record)
		{
			var qry = new Update(AR.Schema)
				.Set(AR.Columns.Expands).EqualTo(record.Expands)
				.Set(AR.Columns.Fails).EqualTo(record.Fails)
				.Where(AR.Columns.QuestionId).IsEqualTo(record.QuestionId)
				.And(AR.Columns.PossibleAnswerId).IsEqualTo(record.PossibleAnswerId);
			qry.Execute();
		}

		public static bool Delete(this ARController cntlr, int questionId, int possibleAnswerId)
		{
			var qry = new Delete(AR.Schema.Provider).From(AR.Schema.TableName)
				.Where(AR.Columns.QuestionId).IsEqualTo(questionId)
				.And(AR.Columns.PossibleAnswerId).IsEqualTo(possibleAnswerId);
			return qry.Execute() > 0;
		}
	}
}
