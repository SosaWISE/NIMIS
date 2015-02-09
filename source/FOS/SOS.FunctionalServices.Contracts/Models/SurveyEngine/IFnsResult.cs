using System;
using System.Collections.Generic;

namespace SOS.FunctionalServices.Contracts.Models.SurveyEngine {
    public interface IFnsResult {
        long ResultID { get; set; }
        int SurveyTranslationId { get; set; }
        long AccountId { get; set; }
        bool Passed { get; set; }
        bool IsComplete { get; set; }
        string Context { get; set; }
        string CreatedBy { get; set; }
        DateTime CreatedOn { get; set; }

		List<IFnsAnswer> Answers { get; set; }
		List<IFnsMapToTokenAnswer> MapToTokenAnswers { get; set; }
    }
}
