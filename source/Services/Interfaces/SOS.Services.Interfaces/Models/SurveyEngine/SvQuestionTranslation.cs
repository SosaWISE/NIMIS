using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.SurveyEngine
{
	public class SvQuestionTranslation
	{
		#region Properties

		[DataMember]
		public int QuestionTranslationID { get; set; }
		
		[DataMember]
		public int SurveyTranslationId { get; set; }
	
		[DataMember]
		public int QuestionId { get; set; }

		[DataMember]
		public string TextFormat { get; set; }

		#endregion Properties
	}
}
