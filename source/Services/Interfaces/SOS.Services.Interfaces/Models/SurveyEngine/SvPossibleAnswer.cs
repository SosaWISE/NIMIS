using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.SurveyEngine
{
	public class SvPossibleAnswer
	{
		#region Properties
		
		[DataMember]
		public int PossibleAnswerID { get; set; }

		[DataMember]
		public string AnswerText { get; set; }

		#endregion Properties
	}
}
