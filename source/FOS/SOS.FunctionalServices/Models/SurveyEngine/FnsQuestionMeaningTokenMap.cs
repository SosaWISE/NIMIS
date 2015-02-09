using SOS.FunctionalServices.Contracts.Models.SurveyEngine;
using SSE.Data.SurveyEngine;
using System;

namespace SOS.FunctionalServices.Models.SurveyEngine
{
	public class FnsQuestionMeaningTokenMap : IFnsQuestionMeaningTokenMap
	{
		#region ctor

		public FnsQuestionMeaningTokenMap(SV_QuestionMeanings_Tokens_Map map)
		{
			QuestionMeaningId = map.QuestionMeaningId;
			TokenId = map.TokenId;
			CreatedOn = map.CreatedOn;
		}

		public FnsQuestionMeaningTokenMap(){}

		#endregion ctor

		#region Porperties
		public int QuestionMeaningId { get; set; }
		public int TokenId { get; set; }
		public DateTime? CreatedOn { get; set; }
		#endregion Porperties
	}
}
