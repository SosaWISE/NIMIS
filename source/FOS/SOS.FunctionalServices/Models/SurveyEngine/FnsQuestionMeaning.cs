using SOS.FunctionalServices.Contracts.Models.SurveyEngine;
using SSE.Data.SurveyEngine;

namespace SOS.FunctionalServices.Models.SurveyEngine
{
	public class FnsQuestionMeaning : IFnsQuestionMeaning
	{
		#region .ctor

		public FnsQuestionMeaning(SV_QuestionMeaning questionMeaning)
		{
			QuestionMeaningID = questionMeaning.QuestionMeaningID;
			SurveyTypeId = questionMeaning.SurveyTypeId;
			Name = questionMeaning.Name;
		}

		public FnsQuestionMeaning(){}

		#endregion .ctor

		#region Properties
		public int QuestionMeaningID { get; set; }
		public int SurveyTypeId { get; set; }
		public string Name { get; set; }
		#endregion Properties
	}
}
