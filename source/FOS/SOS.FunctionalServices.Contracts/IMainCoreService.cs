/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 12/30/13
 * Time: 08:43
 * 
 * Description:  Describes the Authentication Service for SOS.
 *********************************************************************************************************************/

using System.Collections.Generic;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.Data;
using SOS.FunctionalServices.Contracts.Models.MainCore;

namespace SOS.FunctionalServices.Contracts
{
	public interface IMainCoreService : IFunctionalService
	{
		#region Notes

		IFnsResult<List<IFnsMcAccountNotesFull>> AccountNotesGet(long? customerMasterFileID, long? customerId, long? leadId, int pageSize, int pageNumber, string gpEmployeeID);

		IFnsResult<IFnsMcNote> AccountNoteCreate(IFnsMcNote fnsMcNote, string gpEmployeeID);

		IFnsResult<IFnsMcNote> AccountNoteUpdate(IFnsMcNote fnsMcNote, string gpEmployeeID);

		IFnsResult<List<IFnsMcAccountNoteType>> AccountNoteTypesGetAll( string gpEmployeeID);

		#endregion Notes

		#region Account Note Categories
		IFnsResult<List<IFnsMcAccountNoteCat1>> AccountNoteCat1ByDepartmentId(string departmentId, string gpEmployeeID);

		IFnsResult<List<IFnsMcAccountNoteCat2>> AccountNoteCat2ByAccountNoteCat1Id(int cat1Id, string gpEmployeeID);

		#endregion Account Note Categories

		#region Departments
		IFnsResult<List<IFnsMcDepartment>> DepartmentGet(string gpEmployeeID);
		#endregion Departments

		#region Localizations
		IFnsResult<List<IFnsMcLocalization>> LocalizationsGet(string gpEmployeeID);
		#endregion Localizations
	}
}