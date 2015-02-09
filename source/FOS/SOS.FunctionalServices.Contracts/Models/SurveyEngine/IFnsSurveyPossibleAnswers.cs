using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.SurveyEngine
{
	public interface IFnsSurveyPossibleAnswers
	{
		[DataMember]
		int PossibleAnswersID { get; }

		[DataMember]
		string AnswerText { get; }
 
	}
}