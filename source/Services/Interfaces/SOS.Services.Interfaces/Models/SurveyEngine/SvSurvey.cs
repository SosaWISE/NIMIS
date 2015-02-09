using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.SurveyEngine
{
	public class SvSurvey
	{
		#region Properties

		[DataMember]
		public int SurveyID { get; set; }

		[DataMember]
		public int SurveyTypeId { get; set; }

		[DataMember]
		public string Version { get; set; }

		[DataMember]
		public bool IsCurrent { get; set; }

		[DataMember]
		public bool IsReadonly { get; set; }

		#endregion Properties
	}
}
