using SOS.FunctionalServices.Contracts.Models.SurveyEngine;
using SOS.Lib.Util.Extensions;
using SSE.Data.SurveyEngine;
using System.Collections.Generic;

namespace SOS.FunctionalServices.Models.SurveyEngine {
    public class FnsAnswer : IFnsAnswer {
        public long AnswerID { get; set; }
        public long ResultId { get; set; }
        public int QuestionId { get; set; }
        public string AnswerText { get; set; }

        public static SV_Answer ConvertToDb(IFnsAnswer from) {
            if (from == null) {
                return null;
            }
            return new SV_Answer() {
                AnswerID = from.AnswerID,
                ResultId = from.ResultId,
                QuestionId = from.QuestionId,
                AnswerText = from.AnswerText,
            };
        }
        public static IFnsAnswer ConvertFrom(SV_Answer from) {
            if (from == null) {
                return null;
            }
            return new FnsAnswer() {
                AnswerID = from.AnswerID,
                ResultId = from.ResultId,
                QuestionId = from.QuestionId,
                AnswerText = from.AnswerText,
            };
        }
        public static List<IFnsAnswer> ConvertAllFrom(SV_AnswerCollection list) {
            return list.ConvertAll(ConvertFrom);
        }
    }
}
