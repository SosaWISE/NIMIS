using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.SurveyEngine
{
	public interface IFnsQuestion
	{
		[DataMember]
		int QuestionID { get; }

		[DataMember]
		int SurveyId { get; }

		[DataMember]
		int QuestionMeaningId { get; }

		[DataMember]
		int? ParentId { get; }

		[DataMember]
		int GroupOrder { get; }

		[DataMember]
		int? MapToTokenId { get; }

		[DataMember]
		string ConditionJson { get; set; }
	}
}