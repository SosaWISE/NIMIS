
namespace SOS.Services.Interfaces.Models.SurveyEngine {
    public class SvAnswer {
        public long AnswerID { get; set; }
        public long ResultId { get; set; }
        public int QuestionId { get; set; }
        public string AnswerText { get; set; }
    }
}
