/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 30/10/13
 * Time: 15:41
 * 
 * Description:  Describes a survey type.
 *********************************************************************************************************************/

using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.SurveyEngine
{
	public interface IFnsSurvey
	{
		#region Properties

		[DataMember]
		int SurveyID { get; }

		[DataMember]
		int SurveyTypeId { get; }

		[DataMember]
		string Version { get; }

		[DataMember]
		bool IsCurrent { get; }

		[DataMember]
		bool IsReadonly { get; }

		#endregion Properties
		 
	}
}