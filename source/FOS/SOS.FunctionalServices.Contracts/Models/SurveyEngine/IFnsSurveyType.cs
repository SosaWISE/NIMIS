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
	public interface IFnsSurveyType
	{
		#region Properties

		[DataMember]
		int SurveyTypeID { get; }

		[DataMember]
		string Name { get; }

		#endregion Properties
	}
}