using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.SurveyEngine
{
	public class SvSurveyTranslations
	{
		#region Properties

		[DataMember]
		public int SurveyTranslationID { get; set; }

		[DataMember]
		public int SurveyId { get; set; }

		[DataMember]
		public string LocalizationCode { get; set; }

		#endregion Properties
	}
}
