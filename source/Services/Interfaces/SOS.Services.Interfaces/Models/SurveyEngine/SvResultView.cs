using System;

namespace SOS.Services.Interfaces.Models.SurveyEngine {
    public class SvResultView {
        public long ResultID { get; set; }
        public int SurveyTranslationId { get; set; }
        public long AccountId { get; set; }
        public bool Passed { get; set; }
        public bool IsComplete { get; set; }
        public string Context { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public int SurveyId { get; set; }
        public string Version { get; set; }

        public string SurveyType { get; set; }
        public int SurveyTypeId { get; set; }

        public string LocalizationCode { get; set; }
    }
}
