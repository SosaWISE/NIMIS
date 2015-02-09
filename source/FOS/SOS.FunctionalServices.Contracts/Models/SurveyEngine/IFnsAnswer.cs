
namespace SOS.FunctionalServices.Contracts.Models.SurveyEngine {
    public interface IFnsAnswer {
        long AnswerID { get; set; }
        long ResultId { get; set; }
        int QuestionId { get; set; }
        string AnswerText { get; set; }
    }
}
