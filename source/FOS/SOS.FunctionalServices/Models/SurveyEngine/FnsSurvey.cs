using SOS.FunctionalServices.Contracts.Models.SurveyEngine;
using SSE.Data.SurveyEngine;

namespace SOS.FunctionalServices.Models.SurveyEngine
{
	public class FnsSurvey : IFnsSurvey
	{
		#region .ctor
		
		public FnsSurvey(){}

		public FnsSurvey(SV_Survey survey)
		{
			SurveyID = survey.SurveyID;
			SurveyTypeId = survey.SurveyTypeId;
			Version = survey.Version;
			IsCurrent = survey.IsCurrent;
			IsReadonly = survey.IsReadonly;
		}

		#endregion .ctor

		#region Properties
		public int SurveyID { get; set; }
		public int SurveyTypeId { get; set; }
		public string Version { get; set; }
		public bool IsCurrent { get; set; }
		public bool IsReadonly { get; set; }
		#endregion Properties

        public static IFnsSurvey ConvertFrom(SV_Survey from) {
            if (from == null) {
                return null;
            }
            return new FnsSurvey() {
                SurveyID = from.SurveyID,
                SurveyTypeId = from.SurveyTypeId,
				Version = from.Version,
				IsCurrent = from.IsCurrent,
				IsReadonly = from.IsReadonly,
            };
        }
	}
}
