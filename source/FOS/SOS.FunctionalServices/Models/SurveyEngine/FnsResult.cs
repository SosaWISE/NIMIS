using SOS.FunctionalServices.Contracts.Models.SurveyEngine;
using SOS.Lib.Util.Extensions;
using SSE.Data.SurveyEngine;
using System;
using System.Collections.Generic;

namespace SOS.FunctionalServices.Models.SurveyEngine {
    public class FnsResult : IFnsResult {
        public long ResultID { get; set; }
        public int SurveyTranslationId { get; set; }
        public long AccountId { get; set; }
        public bool Passed { get; set; }
        public bool IsComplete { get; set; }
        public string Context { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

		public List<IFnsAnswer> Answers { get; set; }
		public List<IFnsMapToTokenAnswer> MapToTokenAnswers { get; set; }

        public static SV_Result ConvertToDb(IFnsResult from) {
            if (from == null) {
                return null;
            }
            return new SV_Result() {
                ResultID = from.ResultID,
                SurveyTranslationId = from.SurveyTranslationId,
                AccountId = from.AccountId,
                Passed = from.Passed,
                IsComplete = from.IsComplete,
                Context = from.Context,
                CreatedBy = from.CreatedBy,
                CreatedOn = from.CreatedOn,
            };
        }
        public static IFnsResult ConvertFrom(SV_Result from) {
            if (from == null) {
                return null;
            }
            return new FnsResult() {
                ResultID = from.ResultID,
                SurveyTranslationId = from.SurveyTranslationId,
                AccountId = from.AccountId,
                Passed = from.Passed,
                IsComplete = from.IsComplete,
                Context = from.Context,
                CreatedBy = from.CreatedBy,
                CreatedOn = from.CreatedOn,
                Answers = new List<IFnsAnswer>(),
            };
        }
        public static IList<IFnsResult> ConvertAllFrom(SV_ResultCollection list) {
            return list.ConvertAll(ConvertFrom);
        }
    }
}
