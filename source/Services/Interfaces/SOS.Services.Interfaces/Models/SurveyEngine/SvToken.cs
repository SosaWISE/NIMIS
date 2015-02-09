using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.SurveyEngine
{
	public class SvToken
	{
		#region Properties
		
		[DataMember]
		public int TokenID { get; set; }

		[DataMember]
		public string Token { get; set; }

		#endregion Properties
	}
}
