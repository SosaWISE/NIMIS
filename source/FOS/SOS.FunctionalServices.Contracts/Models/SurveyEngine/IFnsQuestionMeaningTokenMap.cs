using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.SurveyEngine
{
	public interface IFnsQuestionMeaningTokenMap
	{
		[DataMember]
 		int QuestionMeaningId { get; }

		[DataMember]
		int TokenId { get; }

		[DataMember]
		DateTime? CreatedOn { get; }
	}
}