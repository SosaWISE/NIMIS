using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.SurveyEngine
{
	public class SvQuestionMeaning
	{
		[DataMember]
		public int QuestionMeaningID { get; set; }

		[DataMember]
		public int SurveyTypeId { get; set; }

		[DataMember]
		public string Name { get; set; }
	}
}
