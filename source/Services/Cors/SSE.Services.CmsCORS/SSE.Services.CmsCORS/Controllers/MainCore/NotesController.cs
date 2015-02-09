using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Http;
using SOS.Data.SosCrm;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.MainCore;
using SOS.FunctionalServices.Models.MainCore;
using SOS.Services.Interfaces.Models.MainCore;
using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;

namespace SSE.Services.CmsCORS.Controllers.MainCore
{
	[RoutePrefix("MainCoreSrv")]
	public class NotesController : ApiController
    {
		[Route("Notes/{id}/CMFID")]
		[HttpGet]
		// POST MainCoreSrv/Notes
		public CmsCORSResult<List<McNote>> GetNotesByCMFID(long id, int pageSize = 10, int pageNumber = 1)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "GetNotesByCMFID";

			#endregion Initialize

			return GetNotesByIDs(METHOD_NAME, id, null, null, pageSize, pageNumber);

		}

		[Route("Notes/{id}/CustomerId")]
		[HttpGet]
		// POST MainCoreSrv/Notes
		public CmsCORSResult<List<McNote>> GetNotesByCustomerId(long id, int pageSize = 10, int pageNumber = 1)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "GetNotesByCustomerId";

			#endregion Initialize

			return GetNotesByIDs(METHOD_NAME, null, id, null, pageSize, pageNumber);

		}

		[Route("Notes/{id}/LeadId")]
		[HttpGet]
		// POST MainCoreSrv/Notes
		public CmsCORSResult<List<McNote>> GetNotesByLeadId(long id, int pageSize = 10, int pageNumber = 1)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "GetNotesByLeadId";

			#endregion Initialize

			return GetNotesByIDs(METHOD_NAME, null, null, id, pageSize, pageNumber);

		}

		private CmsCORSResult<List<McNote>> GetNotesByIDs(string methodName, long? cmfid, long? customerId, long? leadId, int pageSize, int pageNumber)
		{
			#region Initialize

			/** Initialize. */
			var result = new CmsCORSResult<List<McNote>>((int)CmsResultCodes.Initializing
				, string.Format("Initializing {0}.", methodName));

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(methodName
				, user =>
				{
					#region TRY

					try
					{
						// ** Create Service
						var mcService = SosServiceEngine.Instance.FunctionalServices.Instance<IMainCoreService>();
						IFnsResult<List<IFnsMcAccountNotesFull>> fnsResult = mcService.AccountNotesGet(cmfid, customerId, leadId, pageSize, pageNumber, user.GPEmployeeID);

						// ** Save result
						result.Code = fnsResult.Code;
						result.SessionId = user.SessionID;
						result.Message = fnsResult.Message;

						// ** Get Values
						var fnsResultValue = (List<IFnsMcAccountNotesFull>)fnsResult.GetValue();
						if (result.Code == (int)CmsResultCodes.Success && fnsResultValue != null)
						{
							var resultValue = (from fnsToken in (List<IFnsMcAccountNotesFull>)fnsResult.GetValue()
											   select new McNote
											   {
												   NoteID = fnsToken.NoteID,
												   NoteTypeId = fnsToken.NoteTypeID,
												   NoteType = fnsToken.NoteType,
												   CustomerMasterFileId = fnsToken.CustomerMasterFileId,
												   CustomerId = fnsToken.CustomerId,
												   LeadId = fnsToken.LeadId,
												   NoteCategory1Id = fnsToken.NoteCategory1Id,
												   Category1 = fnsToken.Category1,
												   Desc1 = fnsToken.Desc1,
												   NoteCategory2Id = fnsToken.NoteCategory2Id,
												   Category2 = fnsToken.Category2,
												   Desc2 = fnsToken.Desc2,
												   Note = fnsToken.Note,
												   CreatedBy = fnsToken.CreatedBy,
												   CreatedOn = fnsToken.CreatedOn.ToUniversalTime()
											   }).ToList();

							result.Value = resultValue;
						}
					}
					#endregion TRY

					#region CATCH

					catch (Exception ex)
					{
						result.Code = (int)CmsResultCodes.ExceptionThrown;
						result.Message = string.Format("The following exception was thrown from '{0}' method: {1}", methodName,
							ex.Message);
					}

					#endregion CATCH

					#region Result

					return result;

					#endregion Result
				});

		}

		// POST MainCoreSrv/Notes
		[Route("Notes")]
		[HttpPost]
		[HttpOptions]
		public CmsCORSResult<McNote> Post([FromBody] McNote note)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Post Note";

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
				, user =>
				{
					#region Parameter Validation

					var argArray = new List<CORSArg>();
					argArray.Add(new CORSArg(note.NoteTypeId,
							(string.IsNullOrEmpty(note.NoteTypeId)), "<li>'NoteType' can not be blank.</li>"));
					argArray.Add(new CORSArg(note.CustomerMasterFileId,
							(note.CustomerMasterFileId == 0), "<li>'CustomerMasterFileId' can not be blank.</li>"));
					argArray.Add(new CORSArg(note.NoteCategory1Id,
							(string.IsNullOrEmpty(note.NoteCategory1Id.ToString(CultureInfo.InvariantCulture))), "<li>'NoteCategory1Id' can not be blank.</li>"));

					if (!note.NoteTypeId.Equals(MC_AccountNoteType.MetaData.Auto_GeneratedID))
					{
						argArray.Add(new CORSArg(note.Note,
							(string.IsNullOrEmpty(note.Note)), "<li>'Note' can not be blank.</li>"));
					}
					CmsCORSResult<McNote> result;
					if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

					#endregion Parameter Validation

					#region TRY

					try
					{
						// ** Create Service
						var mainCoreService = SosServiceEngine.Instance.FunctionalServices.Instance<IMainCoreService>();

						// ** Bind new data
						var fnsMcNote = new FnsMcNote
						{
							NoteID = note.NoteID,
							NoteTypeId = note.NoteTypeId,
							CustomerMasterFileId = note.CustomerMasterFileId,
							CustomerId = note.CustomerId,
							LeadId = note.LeadId,
							NoteCategory1Id = note.NoteCategory1Id,
							NoteCategory2Id = note.NoteCategory2Id,
							Note = note.Note,
						};
						IFnsResult<IFnsMcNote> fnsResult = fnsMcNote.NoteID > 0 
							? mainCoreService.AccountNoteUpdate(fnsMcNote, user.GPEmployeeID)
							: mainCoreService.AccountNoteCreate(fnsMcNote, user.GPEmployeeID);

						// ** Save result
						result.Code = fnsResult.Code;
						result.SessionId = user.SessionID;
						result.Message = fnsResult.Message;

						// ** Get Values
						if (result.Code == (int)CmsResultCodes.Success)
						{
							fnsMcNote = (FnsMcNote)fnsResult.GetValue();
							var resultValue = new McNote
							{
								NoteID = fnsMcNote.NoteID,
								NoteTypeId = fnsMcNote.NoteTypeId,
								CustomerMasterFileId = fnsMcNote.CustomerMasterFileId,
								CustomerId = fnsMcNote.CustomerId,
								LeadId = fnsMcNote.LeadId,
								NoteCategory1Id = fnsMcNote.NoteCategory1Id,
								NoteCategory2Id = fnsMcNote.NoteCategory2Id,
								Note = fnsMcNote.Note,
							};

							result.Value = resultValue;
						}
					}
					#endregion TRY

					#region CATCH

					catch (Exception ex)
					{
						result.Code = (int)CmsResultCodes.ExceptionThrown;
						result.Message = string.Format("The following exception was thrown from '{0}' method: {1}", METHOD_NAME,
							ex.Message);
					}

					#endregion CATCH

					#region Result

					return result;

					#endregion Result
				});
		}

		// POST MainCoreSrv/Note/{id}
		[Route("Notes/{id}")]
		[HttpPost]
		[HttpOptions]
		public CmsCORSResult<McNote> Post(int id, [FromBody] McNote note)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Post Note";
			note.NoteID = id;

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
				, user =>
				{
					#region Parameter Validation

					var argArray = new List<CORSArg>
					{
						new CORSArg(note.NoteID, 
							(note.NoteID == 0), "<li>'NoteID' must be passed to be able to do an update.</li>"),

						new CORSArg(note.NoteTypeId,
							(string.IsNullOrEmpty(note.NoteTypeId)), "<li>'NoteType' can not be blank.</li>"),

						new CORSArg(note.CustomerMasterFileId,
							(note.CustomerMasterFileId == 0), "<li>'CustomerMasterFileId' can not be blank.</li>"),

						new CORSArg(note.NoteCategory1Id,
							(string.IsNullOrEmpty(note.NoteCategory1Id.ToString(CultureInfo.InvariantCulture))), "<li>'NoteCategory1Id' can not be blank.</li>"),

						new CORSArg(note.Note,
							(string.IsNullOrEmpty(note.Note)), "<li>'Note' can not be blank.</li>"),

					};
					CmsCORSResult<McNote> result;
					if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

					#endregion Parameter Validation

					#region TRY

					try
					{
						// ** Create Service
						var mainCoreService = SosServiceEngine.Instance.FunctionalServices.Instance<IMainCoreService>();

						// ** Bind new data
						var fnsMcNote = new FnsMcNote
						{
							NoteID = note.NoteID,
							NoteTypeId = note.NoteTypeId,
							CustomerMasterFileId = note.CustomerMasterFileId,
							CustomerId = note.CustomerId,
							LeadId = note.LeadId,
							NoteCategory1Id = note.NoteCategory1Id,
							NoteCategory2Id = note.NoteCategory2Id,
							Note = note.Note,
						};

						IFnsResult<IFnsMcNote> fnsResult = mainCoreService.AccountNoteUpdate(fnsMcNote, user.GPEmployeeID);

						// ** Save result
						result.Code = fnsResult.Code;
						result.SessionId = user.SessionID;
						result.Message = fnsResult.Message;

						// ** Get Values
						if (result.Code == (int)CmsResultCodes.Success)
						{
							fnsMcNote = (FnsMcNote)fnsResult.GetValue();
							var resultValue = new McNote
							{
								NoteID = fnsMcNote.NoteID,
								NoteTypeId = fnsMcNote.NoteTypeId,
								CustomerMasterFileId = fnsMcNote.CustomerMasterFileId,
								CustomerId = fnsMcNote.CustomerId,
								LeadId = fnsMcNote.LeadId,
								NoteCategory1Id = fnsMcNote.NoteCategory1Id,
								NoteCategory2Id = fnsMcNote.NoteCategory2Id,
								Note = fnsMcNote.Note,
							};

							result.Value = resultValue;
						}
					}
					#endregion TRY

					#region CATCH

					catch (Exception ex)
					{
						result.Code = (int)CmsResultCodes.ExceptionThrown;
						result.Message = string.Format("The following exception was thrown from '{0}' method: {1}", METHOD_NAME,
							ex.Message);
					}

					#endregion CATCH

					#region Result

					return result;

					#endregion Result
				});
		}

		// GET MainCoreSrv/NoteCategory1/{id}/DepartmentID
		[HttpGet]
		[Route("NoteCategory1/{id}/DepartmentID")]
		public CmsCORSResult<List<McNoteCategory1>> GetNoteCategory1ByDepartmentId(string id)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "GetNoteCategory1ByDepartmentId";
			var result = new CmsCORSResult<List<McNoteCategory1>>((int)CmsResultCodes.Initializing
				, string.Format("Initializing {0}.", METHOD_NAME));

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
				, user =>
				{
					#region TRY

					try
					{
						// ** Create Service
						var mcService = SosServiceEngine.Instance.FunctionalServices.Instance<IMainCoreService>();
						IFnsResult<List<IFnsMcAccountNoteCat1>> fnsResult = mcService.AccountNoteCat1ByDepartmentId(id, user.GPEmployeeID);

						// ** Save result
						result.Code = fnsResult.Code;
						result.SessionId = user.SessionID;
						result.Message = fnsResult.Message;

						// ** Get Values
						var fnsResultValue = (List<IFnsMcAccountNoteCat1>)fnsResult.GetValue();
						if (result.Code == (int)CmsResultCodes.Success && fnsResultValue != null)
						{
							var resultValue = (from fnsCatg1 in fnsResultValue
											   select new McNoteCategory1
											   {
												   NoteCategory1ID = fnsCatg1.NoteCategory1ID,
												   Category = fnsCatg1.Category,
												   NoteTypeId = fnsCatg1.NoteTypeId,
												   Description = fnsCatg1.Description,
												   CreatedBy = fnsCatg1.CreatedBy,
												   CreatedOn = fnsCatg1.CreatedOn

											   }).ToList();

							result.Value = resultValue;
						}
					}
					#endregion TRY

					#region CATCH

					catch (Exception ex)
					{
						result.Code = (int)CmsResultCodes.ExceptionThrown;
						result.Message = string.Format("The following exception was thrown from '{0}' method: {1}", METHOD_NAME,
							ex.Message);
					}

					#endregion CATCH

					#region Result

					return result;

					#endregion Result
				});

		}

		// GET MainCoreSrv/NoteCategory2/{id}/Category1Id
		[HttpGet]
		[Route("NoteCategory2/{id}/Category1Id")]
		public CmsCORSResult<List<McNoteCategory2>> GetNoteCategory2ByCategory1Id(int id)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "GetNoteCategory2ByCategory1Id";
			var result = new CmsCORSResult<List<McNoteCategory2>>((int)CmsResultCodes.Initializing
				, string.Format("Initializing {0}.", METHOD_NAME));

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
				, user =>
				{
					#region TRY

					try
					{
						// ** Create Service
						var mcService = SosServiceEngine.Instance.FunctionalServices.Instance<IMainCoreService>();
						IFnsResult<List<IFnsMcAccountNoteCat2>> fnsResult = mcService.AccountNoteCat2ByAccountNoteCat1Id(id, user.GPEmployeeID);

						// ** Save result
						result.Code = fnsResult.Code;
						result.SessionId = user.SessionID;
						result.Message = fnsResult.Message;

						// ** Get Values
						var fnsResultValue = (List<IFnsMcAccountNoteCat2>)fnsResult.GetValue();
						if (result.Code == (int)CmsResultCodes.Success && fnsResultValue != null)
						{
							var resultValue = (from fnsCatg1 in fnsResultValue
											   select new McNoteCategory2
											   {
												   NoteCategory2ID = fnsCatg1.NoteCategory2ID,
												   NoteCategory1Id = fnsCatg1.NoteCategory1Id,
												   Category = fnsCatg1.Category,
												   Description = fnsCatg1.Description,
												   CreatedBy = fnsCatg1.CreatedBy,
												   CreatedOn = fnsCatg1.CreatedOn

											   }).ToList();

							result.Value = resultValue;
						}
					}
					#endregion TRY

					#region CATCH

					catch (Exception ex)
					{
						result.Code = (int)CmsResultCodes.ExceptionThrown;
						result.Message = string.Format("The following exception was thrown from '{0}' method: {1}", METHOD_NAME,
							ex.Message);
					}

					#endregion CATCH

					#region Result

					return result;

					#endregion Result
				});

		}

		[HttpGet]
		[Route("NoteTypes")]
		public CmsCORSResult<List<McNoteType>> GetNoteTypes()
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "GetNoteTypes";
			var result = new CmsCORSResult<List<McNoteType>>((int)CmsResultCodes.Initializing
				, string.Format("Initializing {0}.", METHOD_NAME));

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
				, user =>
				{
					#region TRY

					try
					{
						// ** Create Service
						var mcService = SosServiceEngine.Instance.FunctionalServices.Instance<IMainCoreService>();
						IFnsResult<List<IFnsMcAccountNoteType>> fnsResult = mcService.AccountNoteTypesGetAll(user.GPEmployeeID);

						// ** Save result
						result.Code = fnsResult.Code;
						result.SessionId = user.SessionID;
						result.Message = fnsResult.Message;

						// ** Get Values
						var fnsResultValue = (List<IFnsMcAccountNoteType>)fnsResult.GetValue();
						if (result.Code == (int)CmsResultCodes.Success && fnsResultValue != null)
						{
							var resultValue = (from fnsCatg1 in fnsResultValue
											   select new McNoteType
											   {
												   NoteTypeID = fnsCatg1.NoteTypeID,
												   NoteType = fnsCatg1.NoteType

											   }).ToList();

							result.Value = resultValue;
						}
					}
					#endregion TRY

					#region CATCH

					catch (Exception ex)
					{
						result.Code = (int)CmsResultCodes.ExceptionThrown;
						result.Message = string.Format("The following exception was thrown from '{0}' method: {1}", METHOD_NAME,
							ex.Message);
					}

					#endregion CATCH

					#region Result

					return result;

					#endregion Result
				});

		}
	}
}
