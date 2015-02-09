using System;
using System.Collections.Generic;

namespace SOS.Services.Interfaces.Models.SurveyEngine {
    public class SvResult {
        public long ResultID { get; set; }
        public int SurveyTranslationId { get; set; }
        public long AccountId { get; set; }
        public bool Passed { get; set; }
        public bool IsComplete { get; set; }
        public string Context { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

		public List<SvAnswer> Answers { get; set; }
		public List<MapToTokenAnswer> MapToTokenAnswers { get; set; }
    }
}
