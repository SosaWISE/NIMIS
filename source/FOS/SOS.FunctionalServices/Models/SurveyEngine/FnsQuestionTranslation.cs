using SOS.FunctionalServices.Contracts.Models.SurveyEngine;
using SSE.Data.SurveyEngine;

namespace SOS.FunctionalServices.Models.SurveyEngine
{
	public class FnsQuestionTranslation : IFnsQuestionTranslation
	{
		#region ctor

		public FnsQuestionTranslation(SV_QuestionTranslation qTranslation)
		{
			QuestionTranslationID = qTranslation.QuestionTranslationID;
			SurveyTranslationId = qTranslation.SurveyTranslationId;
			QuestionId = qTranslation.QuestionId;
			TextFormat = qTranslation.TextFormat;
		}

		public FnsQuestionTranslation() { }

		#endregion ctor

		#region Properties

		public int QuestionTranslationID { get;  set; }
		public int SurveyTranslationId { get;  set; }
		public int QuestionId { get;  set; }
		public string TextFormat { get;  set; }

		#endregion Properties
	}
}
