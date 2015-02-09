using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.CentralStation;
using SOS.Lib.Core.ErrorHandling;
using SOS.Services.Interfaces.Models.MonitoringStation;
using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;
using AuthActions = SOS.Data.AuthenticationControl.AC_Action.MetaData;
using AuthApplications = SOS.Data.AuthenticationControl.AC_Application.MetaData;
using SOS.Lib.Core;

namespace SSE.Services.CmsCORS.Controllers.MonitoringStation
{
	[RoutePrefix("MonitoringStationSrv")]
	public class AccountsController : ApiController
	{

		[Route("MsAccounts")]
		[HttpPost]
		public CmsCORSResult<MsAccountSubmit> Post([FromBody]MsAccountSubmit account)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Submit Online";
			var result = new CmsCORSResult<MsAccountSubmit>((int)CmsResultCodes.Initializing
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
						var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();
						IFnsResult<IFnsMsAccountSubmit> oFnsModel = oService.SubmitOnline(account.AccountId, user.GPEmployeeID);
						/** Check corsResult. */
						if (oFnsModel.Code != 0 && oFnsModel.Code != (decimal) BaseErrorCodes.ErrorCodes.MSAccountOnboardSuccessful)
						{
							result.Code = oFnsModel.Code;
							result.Message = oFnsModel.Message;
							return result;
						}

						/** Setup return corsResult. */
						var fnsAccountSubmit = (IFnsMsAccountSubmit)oFnsModel.GetValue();
						var oMsAccountSubmit = new MsAccountSubmit
						{
							AccountSubmitID = fnsAccountSubmit.AccountSubmitID,
							AccountId = fnsAccountSubmit.AccountId,
							GPTechId = fnsAccountSubmit.GPTechId,
							DateSubmitted = fnsAccountSubmit.DateSubmitted,
							WasSuccessfull = fnsAccountSubmit.WasSuccessfull
						};


						/** Save success results. */
						result.Code = oFnsModel.Code;
						result.SessionId = user.SessionID;
						result.Message = oFnsModel.Message;
						result.Value = oMsAccountSubmit;
					}
					#endregion TRY

					#region CATCH

					catch (Exception ex)
					{
						result.Code = (int)CmsResultCodes.ExceptionThrown;
						result.Message = string.Format("The following exception was thrown from '{0}' method: {1}",
							METHOD_NAME,
							ex.Message);
					}

					#endregion CATCH

					#region Result

					return result;

					#endregion Result

				});

		}

		// GET: /SignalHistory/
		[Route("MsAccounts/{id}/SignalHistory")] //?days={days}
		[HttpGet]
		public CmsCORSResult<List<MsSignalHistoryItem>> Get(long id, int days)
		{
			return CORSSecurity.AuthenticationWrapper("SignalHistory", user =>
			{
				var now = DateTime.UtcNow;
				DateTime startDate;
				if (days < 0)
				{
					// full history
					startDate = new DateTime(2014, 1, 1);
				}
				else
				{
					// start of the day minus num days
					startDate = now.Date.AddDays(days * -1);
				}
				// end of today day
				var endDate = now.Date.AddDays(1);

				// get history
				var fnsResult = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>().GetSignalHistory(id, startDate, endDate, user.GPEmployeeID);

				List<MsSignalHistoryItem> value = null;
				// check result
				if (fnsResult.Code == 0)
				{
					// cast result???
					value = ConvertTo.CastFnsToMsSignalHistoryList((List<IFnsSignalHistoryItemModel>)fnsResult.GetValue());
				}
				// return cors result
				var code = fnsResult.Code == BaseErrorCodes.ErrorCodes.MSAccountNoSignalHistoryFound.Code()
					? 0
					: fnsResult.Code; // This will make the UI show a message instead of an error.
				return new CmsCORSResult<List<MsSignalHistoryItem>>(code, fnsResult.Message)
				{
					SessionId = user.SessionID,
					Value = value,
				};
			});
		}

		[HttpGet, Route("MsAccounts/{id}/TwoWayTestData")]
		public Result<object> TwoWayTestData(long id)
		{
			return CORSSecurity.Authorize("TwoWayTestData", AuthApplications.SSECmsCORSID, null, user =>
			{
				var service = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();
				var fnsResult = service.TwoWayTestData(id);
				return new Result<object>(fnsResult.Code, fnsResult.Message, fnsResult.GetTValue());
			});
		}

		[HttpPost, Route("MsAccounts/{id}/InitTwoWayTest")]
		public Result<object> InitTwoWayTest(long id)
		{
			return CORSSecurity.Authorize("InitTwoWayTest", AuthApplications.SSECmsCORSID, null, user =>
			{
				var service = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();
				var fnsResult = service.InitTwoWayTest(id, user.GPEmployeeID);
				return new Result<object>(fnsResult.Code, fnsResult.Message, fnsResult.GetTValue());
			});
		}

		[HttpPost, Route("MsAccounts/{id}/CompleteTwoWayTest")] //?confirmedBy={confirmedBy}
		public Result<object> CompleteTwoWayTest(long id, string confirmedBy)
		{
			return CORSSecurity.Authorize("CompleteTwoWayTest", AuthApplications.SSECmsCORSID, null, user =>
			{
				var service = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();
				var fnsResult = service.CompleteTwoWayTest(id, confirmedBy, user.GPEmployeeID);
				return new Result<object>(fnsResult.Code, fnsResult.Message, fnsResult.GetTValue());
			});
		}

		[HttpGet, Route("MsAccounts/{id}/ActiveTests")]
		public Result<object> ActiveTests(long id)
		{
			return CORSSecurity.Authorize("ActiveTests", AuthApplications.SSECmsCORSID, null, user =>
			{
				var service = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();
				var fnsResult = service.ActiveTests(id);
				return new Result<object>(fnsResult.Code, fnsResult.Message, fnsResult.GetTValue());
			});
		}

		[HttpPost, Route("MsAccounts/{id}/ClearActiveTests")]
		public Result<bool> ClearActiveTests(long id)
		{
			return CORSSecurity.Authorize("ClearActiveTests", AuthApplications.SSECmsCORSID, null, user =>
			{
				var service = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();
				var fnsResult = service.ClearActiveTests(id);
				return new Result<bool>(fnsResult.Code, fnsResult.Message, fnsResult.GetTValue());
			});
		}

		[HttpPost, Route("MsAccounts/{id}/ClearTest")] //?testNum={testNum}
		public Result<bool> ClearTest(long id, int testNum)
		{
			return CORSSecurity.Authorize("ClearTest", AuthApplications.SSECmsCORSID, null, user =>
			{
				var service = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();
				var fnsResult = service.ClearTest(id, testNum);
				return new Result<bool>(fnsResult.Code, fnsResult.Message, fnsResult.GetTValue());
			});
		}

		[HttpGet, Route("MsAccounts/{id}/ServiceStatus")]
		public Result<object> ServiceStatus(long id)
		{
			return CORSSecurity.Authorize("ServiceStatus", AuthApplications.SSECmsCORSID, null, user =>
			{
				var service = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();
				var fnsResult = service.ServiceStatus(id);
				return new Result<object>(fnsResult.Code, fnsResult.Message, fnsResult.GetTValue());
			});
		}

		[HttpPost, Route("MsAccounts/{id}/ServiceStatus")] //?oosCat={oosCat}
		// ReSharper disable once MethodOverloadWithOptionalParameter
		public Result<string> ClearActiveTests(long id, string oosCat = null)
		{
			return CORSSecurity.Authorize("Post ServiceStatus", AuthApplications.SSECmsCORSID, null, user =>
			{
				var service = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();
				var fnsResult = service.SetServiceStatus(id, oosCat, DateTime.Now, "Set Service Status", user.GPEmployeeID);
				return new Result<string>(fnsResult.Code, fnsResult.Message, fnsResult.GetTValue());
			});
		}

		[HttpGet, Route("MsAccounts/{id}/DispatchAgencyAssignments")]
		public CmsCORSResult<List<MsAccountDispatchAgencyAssignmentView>> GetDAAssignments(long id)
		{
			#region Initialization
			const string METHOD_NAME = "GetDAAssignments";
// ReSharper disable once RedundantAssignment
			var result = new CmsCORSResult<List<MsAccountDispatchAgencyAssignmentView>>(BaseErrorCodes.ErrorCodes.CallInitialization.Code()
				, string.Format(BaseErrorCodes.ErrorCodes.CallInitialization.Message(), "MsAccounts/{id}/DispatchAgencyAssignments"), null);
			#endregion Initialization

			return CORSSecurity.AuthenticationWrapper(METHOD_NAME, user =>
			{
				#region Parameter Validation

				var argArray = new List<CORSArg>
					{
						new CORSArg(id, ((id <= 0)), "<li>'id' was either not passed or is invalid.</li>")
					};

				if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

				#endregion Parameter Validation

				#region TRY
				try
				{
					// ** Create Service
					var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();
					IFnsResult<List<IFnsMsAccountDispatchAgencyAssignmentView>> oFnsModel = oService.ReadDaAssignments(id, user.GPEmployeeID);

					if (oFnsModel.Code != BaseErrorCodes.ErrorCodes.Success.Code())
					{
						result.Code = oFnsModel.Code;
						result.Message = oFnsModel.Message;
						return result;
					}

					// ** Get assignments and return to REST.
					List<IFnsMsAccountDispatchAgencyAssignmentView> fnsAssignList = oFnsModel.GetTValue();
					var assignmentsList = fnsAssignList.Select(ConvertTo.CastFnsToMsAccountDispatchAgencyAssignmentView).ToList();

					// ** Initialize Successfull return
					result.Code = BaseErrorCodes.ErrorCodes.Success.Code();
					result.Message = string.Format(BaseErrorCodes.ErrorCodes.Success.Message(), METHOD_NAME);
					result.Value = assignmentsList;
				}
				#endregion TRY

				#region CATCH

				catch (Exception ex)
				{
					result.Code = (int)CmsResultCodes.ExceptionThrown;
					result.Message = string.Format("The following exception was thrown from '{0}' method: {1}",
						METHOD_NAME,
						ex.Message);
				}
				#endregion CATCH

				#region Result
				return result;
				#endregion Result
			});
		}

		[HttpPost, Route("MsAccounts/{id}/DispatchAgencyAssignments")]
		public CmsCORSResult<List<MsAccountDispatchAgencyAssignmentView>> SaveDaAssignments(long id, [FromBody] List<MsAccountDispatchAgencyAssignmentView> agencyList)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "CORS: SaveDaAssignmentsList";
			// ReSharper disable once RedundantAssignment
			var result = new CmsCORSResult<List<MsAccountDispatchAgencyAssignmentView>>((int)CmsResultCodes.Initializing
				, string.Format("Initializing {0}.", METHOD_NAME));

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
				, user =>
				{
					#region Parameter Validations

					var argArray = new List<CORSArg>
					{
						new CORSArg(agencyList, (agencyList == null), "<li>'agencyList' has to be passed.</li>")
					};
					if (agencyList != null)
						argArray.Add(new CORSArg(agencyList.Count, (agencyList.Count == 0), "<li>'agencyList' has no agencies.  The list is empty.</li>"));

					if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

					#endregion Parameter Validations

					#region TRY

					try
					{
						// ** Build argument
						// ReSharper disable once AssignNullToNotNullAttribute
						var fnsAgencyIdList = agencyList.Select(msDispatchAgency => msDispatchAgency.DispatchAgencyOsId).ToList();

						// ** Create Service
						var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();
						IFnsResult<List<IFnsMsAccountDispatchAgencyAssignmentView>> oFnsModel = oService.SaveDaAssignmentsList(id, fnsAgencyIdList, user.GPEmployeeID);

						/** Check corsResult. */
						if (oFnsModel.Code != 0)
						{
							result.Code = oFnsModel.Code;
							result.Message = oFnsModel.Message;
							return result;
						}

						/** Setup return corsResult. */
						var fnsMcPremiseAddress = (List<IFnsMsAccountDispatchAgencyAssignmentView>)oFnsModel.GetValue();
						var msAgencyResultList = fnsMcPremiseAddress.Select(ConvertTo.CastFnsToMsAccountDispatchAgencyAssignmentView).ToList();

						/** Save success results. */
						result.Code = (int)CmsResultCodes.Success;
						result.SessionId = user.SessionID;
						result.Message = "Success";
						result.Value = msAgencyResultList;
					}
					#endregion TRY

					#region CATCH

					catch (Exception ex)
					{
						result.Code = (int)CmsResultCodes.ExceptionThrown;
						result.Message = string.Format("The following exception was thrown from '{0}' method: {1}",
							METHOD_NAME,
							ex.Message);
					}

					#endregion CATCH

					#region Result

					return result;

					#endregion Result

				});
		}

		[HttpGet, Route("MsAccounts/{id}/ServiceTickets")]
		public Result<object> Read(int id)
		{
			return CORSSecurity.Authorize("Read Account ServiceTickets", null, null, user =>
			{
				var service = SosServiceEngine.Instance.FunctionalServices.Instance<TicketService>();
				return service.GetServiceTicketsForAccount(id);
			});
		}

		[HttpGet]
		[Route("Accounts/{id}")]
		public CmsCORSResult<object> Get(long id)
		{
			return CORSSecurity.AuthenticationWrapper("Get", user =>
			{
				return new CmsCORSResult<object>(-1, "Not implement");
			});
		}

		[HttpGet]
		[Route("Accounts/{accountId}/Customers/{customerTypeId}")]
		public CmsCORSResult<object> Customers(long accountId, string customerTypeId)
		{
			return CORSSecurity.AuthenticationWrapper("Customers", user =>
			{
				var service = SosServiceEngine.Instance.FunctionalServices.Instance<IAccountingEngineService>();
				var fnsResult = service.Customer(accountId, customerTypeId);
				return new CmsCORSResult<object>(fnsResult.Code, fnsResult.Message)
				{
					Value = fnsResult.GetValue(),
				};
			});
		}

		[HttpGet]
		[Route("Accounts/{accountId}/Details")]
		public CmsCORSResult<object> Details(long accountId)
		{
			return CORSSecurity.AuthenticationWrapper("Details", user =>
			{
				var service = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();
				var fnsResult = service.AccountDetails(accountId);
				return new CmsCORSResult<object>(fnsResult.Code, fnsResult.Message)
				{
					Value = fnsResult.GetValue(),
				};
			});
		}

		[HttpGet]
		[Route("Accounts/{accountId}/SalesRep")]
		public CmsCORSResult<object> SalesRep(long accountId)
		{
			return CORSSecurity.AuthenticationWrapper("SalesRep", user =>
			{
				var service = SosServiceEngine.Instance.FunctionalServices.Instance<IHumanResourceService>();
				var fnsResult = service.AccountSalesRep(accountId);
				return new CmsCORSResult<object>(fnsResult.Code, fnsResult.Message)
				{
					Value = fnsResult.GetValue(),
				};
			});
		}

		[HttpGet]
		[Route("Accounts/{accountId}/Technician")]
		public CmsCORSResult<object> Technician(long accountId)
		{
			return CORSSecurity.AuthenticationWrapper("Technician", user =>
			{
				var service = SosServiceEngine.Instance.FunctionalServices.Instance<IHumanResourceService>();
				var fnsResult = service.AccountTechnician(accountId);
				return new CmsCORSResult<object>(fnsResult.Code, fnsResult.Message)
				{
					Value = fnsResult.GetValue(),
				};
			});
		}

		[HttpGet]
		[Route("Accounts/{accountId}/ContractTerms")]
		public CmsCORSResult<object> ContractTerms(long accountId)
		{
			return CORSSecurity.AuthenticationWrapper("Account-ContractTerms", user =>
			{
				return new CmsCORSResult<object>(-1, "Not implement");
			});
		}

		//added by reagan - primarily used for validation of accountid field in Tickets Scheduling
		[HttpGet]
		[Route("Accounts/{accountId}/Validate")]
		public CmsCORSResult<object> Validate(long accountId)
		{
			return CORSSecurity.AuthenticationWrapper("Validate", user =>
			{
				var service = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();
				var fnsResult = service.AccountValidate(accountId);
				return new CmsCORSResult<object>(fnsResult.Code, fnsResult.Message)
				{
					Value = fnsResult.GetValue(),
				};
			});
		}

		//[HttpGet]
		//[Route("Accounts/{id}/Equipment")]
		//public CmsCORSResult<object> Equipment(long id)
		//{
		//	throw new NotImplementedException();
		//}
	}
}
