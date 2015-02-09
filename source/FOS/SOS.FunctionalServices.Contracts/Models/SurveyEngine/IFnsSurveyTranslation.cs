using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.SurveyEngine
{
	public interface IFnsSurveyTranslation
	{
		#region Properties
		[DataMember]
		int SurveyTranslationID { get; }

		[DataMember]
		int SurveyId { get; }

		[DataMember]
		string LocalizationCode { get; }
		#endregion Properties
	}
}