using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.SurveyEngine
{
	public class SvQuestion
	{
		[DataMember]
		public int QuestionID { get; set; }

		[DataMember]
		public int SurveyId { get; set; }

		[DataMember]
		public int QuestionMeaningId { get; set; }

		[DataMember]
		public int? ParentId { get; set; }

		[DataMember]
		public int GroupOrder { get; set; }

		[DataMember]
		public int? MapToTokenId { get; set; }

		[DataMember]
		public string ConditionJson { get; set; }

	}
}
