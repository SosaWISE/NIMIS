using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.SurveyEngine
{
	public class SvQuestionPossibleAnswerMap
	{
		#region Properties

		[DataMember]
		public int QuestionId { get; set; }

		[DataMember]
		public int PossibleAnswerId { get; set; }

		[DataMember]
		public bool Expands { get; set; }

		[DataMember]
		public bool Fails { get; set; }

		[DataMember]
		public DateTime? CreatedOn { get; set; }

		#endregion Properties
	}
}
