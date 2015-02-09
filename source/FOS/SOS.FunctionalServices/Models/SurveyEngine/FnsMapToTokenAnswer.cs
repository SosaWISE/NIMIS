using SOS.FunctionalServices.Contracts.Models.SurveyEngine;

namespace SOS.FunctionalServices.Models.SurveyEngine
{
	public class FnsMapToTokenAnswer : IFnsMapToTokenAnswer
	{
		public int TokenId { get; set; }
		public object Answer { get; set; }
	}
}
