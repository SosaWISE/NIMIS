using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.SurveyEngine
{
	public interface IFnsSurveyToken
	{
		[DataMember]
		int TokenID { get; }

		[DataMember]
		string Token { get; }
 
	}
}