using SOS.FunctionalServices.Contracts.Models.SurveyEngine;
using SSE.Data.SurveyEngine;

namespace SOS.FunctionalServices.Models.SurveyEngine
{
	public class FnsQuestion : IFnsQuestion
	{
		#region ctor

		public FnsQuestion(SV_Question question)
		{
			QuestionID = question.QuestionID;
			SurveyId = question.SurveyId;
			QuestionMeaningId = question.QuestionMeaningId;
			ParentId = question.ParentId;
			GroupOrder = question.GroupOrder;
			MapToTokenId = question.MapToTokenId;
			ConditionJson = question.ConditionJson;
		}

		public FnsQuestion() { }

		#endregion ctor

		#region Properties

		public int QuestionID { get; set; }
		public int SurveyId { get; set; }
		public int QuestionMeaningId { get; set; }
		public int? ParentId { get; set; }
		public int GroupOrder { get; set; }
		public int? MapToTokenId { get; set; }
		public string ConditionJson { get; set; }

		#endregion Properties
	}
}
