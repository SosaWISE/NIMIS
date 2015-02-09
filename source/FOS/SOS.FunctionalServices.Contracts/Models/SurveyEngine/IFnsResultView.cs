using System;

namespace SOS.FunctionalServices.Contracts.Models.SurveyEngine {
    public interface IFnsResultView {
        long ResultID { get; set; }
        int SurveyTranslationId { get; set; }
        long AccountId { get; set; }
        bool Passed { get; set; }
        bool IsComplete { get; set; }
        string Context { get; set; }
        string CreatedBy { get; set; }
        DateTime CreatedOn { get; set; }

        int SurveyId { get; set; }
        string Version { get; set; }

        string SurveyType { get; set; }
        int SurveyTypeId { get; set; }

        string LocalizationCode { get; set; }
    }
}
