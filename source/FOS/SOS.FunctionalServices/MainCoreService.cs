using System;
using System.Collections.Generic;
using System.Linq;
using SOS.Data.SosCrm;
using SOS.Data.SosCrm.ControllerExtensions;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Helper;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.Data;
using SOS.FunctionalServices.Contracts.Models.MainCore;
using SOS.FunctionalServices.Models;
using SOS.FunctionalServices.Models.Cms;
using SOS.FunctionalServices.Models.MainCore;

namespace SOS.FunctionalServices
{
	public class MainCoreService : IMainCoreService
	{
		#region Account Notes
		public IFnsResult<List<IFnsMcAccountNotesFull>> AccountNotesGet(long? customerMasterFileID, long? customerId, long? leadId, int pageSize, int pageNumber, string gpEmployeeID)
		{
			#region INITIALIZATION

			// ** Initialize 
			var result = new FnsResult<List<IFnsMcAccountNotesFull>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = "Initializing AccountNotesGet",
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Execute search
				//MC_AccountNoteCollection col = SosCrmDataContext.Instance.MC_AccountNotes.SearchNotes(customerMasterFileID, customerId, leadId, pageSize, pageNumber);
				MC_AccountNotesAllInfoViewCollection col = SosCrmDataContext.Instance.MC_AccountNotesAllInfoViews.SearchNotes(customerMasterFileID, customerId, leadId, pageSize, pageNumber);

				// ** Build result value
				var resultList = col.Select(note => new FnsMcAccountNotesFull(note)).Cast<IFnsMcAccountNotesFull>().ToList();

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultList;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsMcAccountNotesFull>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at AccountNotesGet: {0}", ex.Message),
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<IFnsMcNote> AccountNoteCreate(IFnsMcNote fnsMcNote, string gpEmployeeID)
		{
			#region INITIALIZATION

			// ** Initialize 
			var result = new FnsResult<IFnsMcNote>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = "Initializing AccountNotesGet",
			};

			/** Check to see if this is an AUTO_GEN note. */
			if (fnsMcNote.NoteTypeId.Equals(MC_AccountNoteType.MetaData.Auto_GeneratedID)
				&& string.IsNullOrEmpty(fnsMcNote.Note))
			{
				fnsMcNote.Note = "System auto noted the account.";
			}

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Execute Create
				var accountNote = new MC_AccountNote
				{
					NoteTypeId = fnsMcNote.NoteTypeId,
					CustomerMasterFileId = fnsMcNote.CustomerMasterFileId,
					CustomerId = fnsMcNote.CustomerId,
					LeadId = fnsMcNote.LeadId,
					NoteCategory1Id = fnsMcNote.NoteCategory1Id,
					NoteCategory2Id = fnsMcNote.NoteCategory2Id,
					Note = fnsMcNote.Note,
					CreatedBy = gpEmployeeID
				};

				accountNote.Save(gpEmployeeID);

				var resultValue = new FnsMcNote(accountNote);

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultValue;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsMcNote>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at AccountNotesGet: {0}", ex.Message),
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<IFnsMcNote> AccountNoteUpdate(IFnsMcNote fnsMcNote, string gpEmployeeID)
		{
			#region INITIALIZATION

			// ** Initialize 
			var result = new FnsResult<IFnsMcNote>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = "Initializing AccountNoteUpdate",
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Find the AccountNote
				var accountNote = SosCrmDataContext.Instance.MC_AccountNotes.LoadByPrimaryKey(fnsMcNote.NoteID);

				// ** Check that there is data.
				if (accountNote == null)
				{
					result.Code = (int)ErrorCodes.GeneralError;
					result.Message = string.Format("The note id passed did not return a note.");
					result.Value = null;

					return result;
				}

				// ** Execute Create
				accountNote = new MC_AccountNote
				{
					IsNew = false,
					IsLoaded = true,
					NoteID = fnsMcNote.NoteID,

					NoteTypeId = fnsMcNote.NoteTypeId,
					CustomerMasterFileId = fnsMcNote.CustomerMasterFileId,
					CustomerId = fnsMcNote.CustomerId,
					LeadId = fnsMcNote.LeadId,
					NoteCategory1Id = fnsMcNote.NoteCategory1Id,
					NoteCategory2Id = fnsMcNote.NoteCategory2Id,
					Note = fnsMcNote.Note,
					CreatedBy = gpEmployeeID
				};

				accountNote.Save(gpEmployeeID);

				var resultValue = new FnsMcNote(accountNote);

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultValue;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsMcNote>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at AccountNoteUpdate: {0}", ex.Message),
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<List<IFnsMcAccountNoteType>> AccountNoteTypesGetAll(string gpEmployeeID)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "AccountNoteTypesGetAll";
			var result = new FnsResult<List<IFnsMcAccountNoteType>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing '{0}'", METHOD_NAME),
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Execute search
				MC_AccountNoteTypeCollection col = SosCrmDataContext.Instance.MC_AccountNoteTypes.LoadAll();

				// ** Build result value
				var resultList = col.Select(note => new FnsMcAccountNoteType(note)).Cast<IFnsMcAccountNoteType>().ToList();

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultList;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsMcAccountNoteType>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at '{0}': {1}", METHOD_NAME, ex.Message),
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		#endregion Account Notes

		#region Account Note Categories

		public IFnsResult<List<IFnsMcAccountNoteCat1>> AccountNoteCat1ByDepartmentId(string departmentId, string gpEmployeeID)
		{
			#region INITIALIZATION

			// ** Initialize 
			var result = new FnsResult<List<IFnsMcAccountNoteCat1>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = "Initializing AccountNotesGet",
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Execute search
				MC_AccountNoteCat1Collection col = SosCrmDataContext.Instance.MC_AccountNoteCat1s.GetByDepartmentId(departmentId);

				// ** Build result value
				var resultList = col.Select(note => new FnsMcAccountNoteCat1(note)).Cast<IFnsMcAccountNoteCat1>().ToList();

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultList;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsMcAccountNoteCat1>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at AccountNotesGet: {0}", ex.Message),
				};
			}
			#endregion CATCH

			// ** Return result
			return result;

		}

		public IFnsResult<List<IFnsMcAccountNoteCat2>> AccountNoteCat2ByAccountNoteCat1Id(int cat1Id, string gpEmployeeID)
		{
			#region INITIALIZATION

			// ** Initialize 
			var result = new FnsResult<List<IFnsMcAccountNoteCat2>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = "Initializing AccountNoteCat2ByAccountNoteCat1Id",
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Execute search
				MC_AccountNoteCat2Collection col = SosCrmDataContext.Instance.MC_AccountNoteCat2s.GetByCategory1Id(cat1Id);

				// ** Build result value
				var resultList = col.Select(note => new FnsMcAccountNoteCat2(note)).Cast<IFnsMcAccountNoteCat2>().ToList();

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultList;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsMcAccountNoteCat2>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at AccountNoteCat2ByAccountNoteCat1Id: {0}", ex.Message),
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		#endregion Account Note Categories

		#region Departments

		public IFnsResult<List<IFnsMcDepartment>> DepartmentGet(string gpEmployeeID)
		{
			#region INITIALIZATION

			// ** Initialize 
			var result = new FnsResult<List<IFnsMcDepartment>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = "Initializing DepartmentGet",
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Execute search
				MC_DepartmentCollection col = SosCrmDataContext.Instance.MC_Departments.LoadAll();

				// ** Build result value
				var resultList = col.Select(note => new FnsMcDepartment(note)).Cast<IFnsMcDepartment>().ToList();

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultList;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsMcDepartment>>
				{
					Code = (int)ErrorCodes.UnexpectedException
					,
					Message = string.Format("Exception thrown at DepartmentGet: {0}", ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		#endregion Departments

		#region Localizations

		public IFnsResult<List<IFnsMcLocalization>> LocalizationsGet(string gpEmployeeID)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "LocalizationsGet";

			var result = new FnsResult<List<IFnsMcLocalization>>
			{
				Code = (int)ErrorCodes.GeneralMessage
				,
				Message = string.Format("Initializing {0}", METHOD_NAME)
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Execute search
				MC_LocalizationCollection col = SosCrmDataContext.Instance.MC_Localizations.LoadAll();

				// ** Build result value
				var resultList = col.Select(local => new FnsMcLocalization(local)).Cast<IFnsMcLocalization>().ToList();

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultList;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsMcLocalization>>
				{
					Code = (int)ErrorCodes.UnexpectedException
					,
					Message = string.Format("Exception thrown at {1}: {0}", ex.Message, METHOD_NAME)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		#endregion Localizations

	}
}
