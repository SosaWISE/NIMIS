using SOS.FunctionalServices.Contracts.Models.SurveyEngine;
using SSE.Data.SurveyEngine;

namespace SOS.FunctionalServices.Models.SurveyEngine
{
	public class FnsSurveyType : IFnsSurveyType
	{
		#region .ctor

		public FnsSurveyType() {}

		public FnsSurveyType(SV_SurveyType svSurveyType)
		{
			SurveyTypeID = svSurveyType.SurveyTypeID;
			Name = svSurveyType.Name;
		}

		#endregion .ctor

		#region Properties
		public int SurveyTypeID { get; set; }
		public string Name { get; set; }
		#endregion Properties
	}
}
