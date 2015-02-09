using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.SurveyEngine
{
	public class SvQuestionMeaningTokenMap
	{
		[DataMember]
		public int QuestionMeaningId { get; set; }

		[DataMember]
		public int TokenId { get; set; }

		[DataMember]
		public DateTime? CreatedOn { get; set; }
	}
}
