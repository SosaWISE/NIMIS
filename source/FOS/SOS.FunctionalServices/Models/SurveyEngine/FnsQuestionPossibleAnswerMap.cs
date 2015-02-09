using SOS.FunctionalServices.Contracts.Models.SurveyEngine;
using SSE.Data.SurveyEngine;
using System;

namespace SOS.FunctionalServices.Models.SurveyEngine
{
	public class FnsQuestionPossibleAnswerMap : IFnsQuestionPossibleAnswerMap
	{
		#region ctor

		public FnsQuestionPossibleAnswerMap(SV_Questions_PossibleAnswers_Map map)
		{
			QuestionId = map.QuestionId;
			PossibleAnswerId = map.PossibleAnswerId;
			Expands = map.Expands;
			Fails = map.Fails;
			CreatedOn = map.CreatedOn;
		}

		public FnsQuestionPossibleAnswerMap() { }

		#endregion ctor

		#region Properties

		public int QuestionId { get; set; }
		public int PossibleAnswerId { get; set; }
		public bool Expands { get; set; }
		public bool Fails { get; set; }
		public DateTime? CreatedOn { get; set; }

		#endregion Properties
	}
}
