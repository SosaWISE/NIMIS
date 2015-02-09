using SOS.FunctionalServices.Contracts.Models.SurveyEngine;
using SSE.Data.SurveyEngine;

namespace SOS.FunctionalServices.Models.SurveyEngine
{
	public class FnsSurveyTranslation : IFnsSurveyTranslation
	{
		#region ctor

		public FnsSurveyTranslation(SV_SurveyTranslation surveyTranslation)
		{
			SurveyTranslationID = surveyTranslation.SurveyTranslationID;
			SurveyId = surveyTranslation.SurveyId;
			LocalizationCode = surveyTranslation.LocalizationCode;
		}

		public FnsSurveyTranslation() { }
		#endregion ctor

		#region Properties
		public int SurveyTranslationID { get;  set; }
		public int SurveyId { get; set; }
		public string LocalizationCode { get;  set; }
		#endregion Properties
	}
}
