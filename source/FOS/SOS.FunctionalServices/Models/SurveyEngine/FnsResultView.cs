using SOS.FunctionalServices.Contracts.Models.SurveyEngine;
using SOS.Lib.Util.Extensions;
using SSE.Data.SurveyEngine;
using System;
using System.Collections.Generic;

namespace SOS.FunctionalServices.Models.SurveyEngine {
    public class FnsResultView : IFnsResultView {
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

        public static IFnsResultView ConvertFrom(SV_ResultsView from) {
            if (from == null) {
                return null;
            }
            return new FnsResultView() {
                ResultID = from.ResultID,
                SurveyTranslationId = from.SurveyTranslationId,
                AccountId = from.AccountId,
                Passed = from.Passed,
                IsComplete = from.IsComplete,
                Context = from.Context,
                CreatedBy = from.CreatedBy,
                CreatedOn = from.CreatedOn,
                SurveyId = from.SurveyId,
                Version = from.Version,
                SurveyType = from.SurveyType,
                SurveyTypeId = from.SurveyTypeId,
                LocalizationCode = from.LocalizationCode,
            };
        }
        public static List<IFnsResultView> ConvertAllFrom(SV_ResultsViewCollection list) {
            return list.ConvertAll(ConvertFrom);
        }
    }
}
