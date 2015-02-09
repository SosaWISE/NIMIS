using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.SurveyEngine
{
	public interface IFnsQuestionPossibleAnswerMap
	{
		#region Properties

		[DataMember]
		int QuestionId { get; }

		[DataMember]
		int PossibleAnswerId { get; }

		[DataMember]
		bool Expands { get; }

		[DataMember]
		bool Fails { get; }

		[DataMember]
		DateTime? CreatedOn { get; set; }

		#endregion Properties

	}
}