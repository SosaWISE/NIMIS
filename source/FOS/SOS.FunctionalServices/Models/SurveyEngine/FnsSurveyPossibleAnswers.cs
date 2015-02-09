using SOS.FunctionalServices.Contracts.Models.SurveyEngine;
using SSE.Data.SurveyEngine;

namespace SOS.FunctionalServices.Models.SurveyEngine
{
	public class FnsSurveyPossibleAnswers : IFnsSurveyPossibleAnswers
	{
		#region .ctor

		public FnsSurveyPossibleAnswers(SV_PossibleAnswer possibleAnswer)
		{
			PossibleAnswersID = possibleAnswer.PossibleAnswerID;
			AnswerText = possibleAnswer.AnswerText;
		}

		public FnsSurveyPossibleAnswers() { }
		#endregion .ctor

		#region Properties
		public int PossibleAnswersID { get;  set; }
		public string AnswerText { get; set; }
		
		#endregion Properties
	}
}
