using SOS.FunctionalServices.Contracts.Models.SurveyEngine;
using SSE.Data.SurveyEngine;

namespace SOS.FunctionalServices.Models.SurveyEngine
{
	public class FnsSurveyToken : IFnsSurveyToken
	{
		#region .ctor

		public FnsSurveyToken(SV_Token token)
		{
			TokenID = token.TokenID;
			Token = token.Token;
		}

		public FnsSurveyToken() { }
		#endregion .ctor

		#region Properties
		public int TokenID { get;  set; }
		public string Token { get; set; }
		#endregion Properties

	}
}
