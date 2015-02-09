using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.SurveyEngine
{
	public interface IFnsQuestionTranslation
	{
		#region Properties

		[DataMember]
		int QuestionTranslationID { get; }

		[DataMember]
		int SurveyTranslationId { get; }

		[DataMember]
		int QuestionId { get; }

		[DataMember]
		string TextFormat { get; }

		#endregion Properties
	}
}