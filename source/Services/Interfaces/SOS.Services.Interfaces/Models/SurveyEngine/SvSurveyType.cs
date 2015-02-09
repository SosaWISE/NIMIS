using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.SurveyEngine
{
	public class SvSurveyType
	{
		#region .ctor

		#endregion .ctor

		#region Properties

		[DataMember]
		public int SurveyTypeID { get; set; }

		[DataMember]
		public string Name { get; set; }

		#endregion Properties
	}
}
