using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.SurveyEngine
{
	public interface IFnsQuestionMeaning
	{
		[DataMember]
		int QuestionMeaningID { get; set; }

		[DataMember]
		int SurveyTypeId { get; set; }

		[DataMember]
		string Name { get; set; }
		 
	}
}