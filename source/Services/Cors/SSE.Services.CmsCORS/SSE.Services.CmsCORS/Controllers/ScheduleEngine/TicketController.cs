using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.ScheduleEngine;
using SOS.Services.Interfaces.Models.ScheduleEngine;
using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;
using SOS.FunctionalServices.Contracts.Helper;
using SOS.FunctionalServices.Models.ScheduleEngine;

namespace SSE.Services.CmsCORS.Controllers.ScheduleEngine
{
	[RoutePrefix("ScheduleEngineSrv")]
	public class TicketController : ApiController
	{

		//this method will save a new SeTicket record
		[Route("SeTicket/")]
		[HttpPost]
		public CmsCORSResult<SeTicket> Create([FromBody]SeTicket seTicket)
		{
			//call update 
			return this.Update((seTicket != null) ? seTicket.TicketID : 0, seTicket);
		}


		[Route("SeTicket/{id}")]
		[HttpPost]
		public CmsCORSResult<SeTicket> Update(long id, [FromBody]SeTicket seTicket)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Create/Update SeTicket";

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME, user =>
			{
				#region Parameter Validation

				var argArray = new List<CORSArg>();
				if (seTicket == null)
				{
					argArray.Add(new CORSArg(null, (true), "<li>No values where passed.</li>"));
				}
				else
				{
					//need to add validation here

					argArray.Add(new CORSArg(seTicket.TicketTypeId, (seTicket.TicketTypeId == 0), "<li>'TicketTypeID' was not passed.</li>"));
				}
				CmsCORSResult<SeTicket> result;
				if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

				#endregion Parameter Validation

				#region TRY

				try
				{
					var msService = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();
					var accountResult = msService.AccountValidate(seTicket.AccountId);
					if (accountResult.Code != 0)
					{
						result.Code = accountResult.Code;
						result.Message = accountResult.Message;
						return result;
					}

					// ** Create Service
					var seService = SosServiceEngine.Instance.FunctionalServices.Instance<IScheduleEngineService>();

					// ** Prepare arguents
					// ReSharper disable once PossibleNullReferenceException
					var fnsHeader = new FnsSeTicket(
						seTicket.TicketID,
						seTicket.AccountId,
						seTicket.CustomerMasterFileId,
						seTicket.MonitoringStationNo,
						seTicket.TicketTypeId,
						seTicket.StatusCodeId,
						seTicket.MoniConfirmation,
						seTicket.TechConfirmation,
						seTicket.TechnicianId,
						seTicket.TripCharges,
						seTicket.Appointment,
						seTicket.AgentConfirmation,
						seTicket.ExpirationDate,
						seTicket.Notes
					);
					IFnsResult<IFnsSeTicket> fnsResult = null;

					if (seTicket.TicketID == 0)
					{
						fnsHeader.StatusCodeId = 2; //pending

						fnsResult = seService.SeTicketCreate(fnsHeader, user.GPEmployeeID);
					}
					else
					{
						fnsResult = seService.SeTicketUpdate(fnsHeader, user.GPEmployeeID);
					}

					// ** Save result
					result.Code = fnsResult.Code;
					result.SessionId = user.SessionID;
					result.Message = fnsResult.Message;

					// ** Get Values
					var oSeTicket = ConvertTo.CastFnsToSeTicket((IFnsSeTicket)fnsResult.GetValue());
					if (result.Code == (int)CmsResultCodes.Success && oSeTicket != null)
					{
						result.Value = oSeTicket;
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




		//this method will return the list of Tickets based on the given ticketstatusid TSCID
		[Route("SeTicketList/{id}/TSCID")]
		[HttpGet]
		public CmsCORSResult<List<SeTicket>> SeTicketListByStatusCodeId(int id)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Get SeTicketList";
			var result = new CmsCORSResult<List<SeTicket>>((int)CmsResultCodes.Initializing
				, string.Format("Initializing {0}.", METHOD_NAME));

			#endregion Initializes

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME, user =>
			{
				#region TRY
				try
				{
					// ** Create Service

					var ieService = SosServiceEngine.Instance.FunctionalServices.Instance<IScheduleEngineService>();

					IFnsResult<List<IFnsSeTicket>> oFnsModel = null;
					if (id != 0)
					{
						oFnsModel = ieService.SeTicketListByStatusCodeId(id);
					}
					else
					{
						oFnsModel = ieService.SeTicketList();
					}

					/** Check corsResult. */
					if (oFnsModel.Code != 0)
					{
						result.Code = oFnsModel.Code;
						result.Message = oFnsModel.Message;
						return result;
					}

					/** Setup return corsResult. */
					var oTsTicketList = ConvertTo.CastFnsToSeTicketList((List<IFnsSeTicket>)oFnsModel.GetValue());

					/** Save success results. */
					result.Code = (int)CmsResultCodes.Success;
					result.SessionId = user.SessionID;
					result.Message = "Success";
					result.Value = oTsTicketList;
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



		//this method will return the list of Tickets to be rescheduled accept hourPassed
		[Route("SeTicketReScheduleList/{hoursPassed}")]
		[HttpGet]
		public CmsCORSResult<List<SeTicket>> SeTicketReScheduleList(int hoursPassed)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Get SeTicketReScheduleList";
			var result = new CmsCORSResult<List<SeTicket>>((int)CmsResultCodes.Initializing
				, string.Format("Initializing {0}.", METHOD_NAME));

			#endregion Initializes

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME, user =>
			{
				#region TRY
				try
				{
					// ** Create Service

					var ieService = SosServiceEngine.Instance.FunctionalServices.Instance<IScheduleEngineService>();

					IFnsResult<List<IFnsSeTicket>> oFnsModel = null;

					oFnsModel = ieService.SeTicketReScheduleList(hoursPassed);

					/** Check corsResult. */
					if (oFnsModel.Code != 0)
					{
						result.Code = oFnsModel.Code;
						result.Message = oFnsModel.Message;
						return result;
					}

					/** Setup return corsResult. */
					var oTsTicketList = ConvertTo.CastFnsToSeTicketList((List<IFnsSeTicket>)oFnsModel.GetValue());

					/** Save success results. */
					result.Code = (int)CmsResultCodes.Success;
					result.SessionId = user.SessionID;
					result.Message = "Success";
					result.Value = oTsTicketList;
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





		//this method will return the list of Tickets based on the given ticketstatusid
		[Route("SeTicket/{id}/TicketID")]
		[HttpGet]
		public CmsCORSResult<SeTicket> SeTicketByTicketId(long id)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Get SeTicket";
			var result = new CmsCORSResult<SeTicket>((int)CmsResultCodes.Initializing
				, string.Format("Initializing {0}.", METHOD_NAME));

			#endregion Initializes

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME, user =>
			{

				#region TRY
				try
				{
					// ** Create Service

					var ieService = SosServiceEngine.Instance.FunctionalServices.Instance<IScheduleEngineService>();
					IFnsResult<IFnsSeTicket> oFnsModel = ieService.SeTicketByTicketId(id);

					/** Check corsResult. */
					if (oFnsModel.Code != 0)
					{
						result.Code = oFnsModel.Code;
						result.Message = oFnsModel.Message;
						return result;
					}

					/** Setup return corsResult. */
					var oSeTicket = ConvertTo.CastFnsToSeTicket((IFnsSeTicket)oFnsModel.GetValue());

					/** Save success results. */
					result.Code = (int)CmsResultCodes.Success;
					result.SessionId = user.SessionID;
					result.Message = "Success";
					result.Value = oSeTicket;
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



		//this method will return the list of Tickets based on the given Account id
		[Route("SeTicketList/{id}/ACCID")]
		[HttpGet]
		public CmsCORSResult<List<SeTicket>> SeTicketListByAccountId(long id)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Get SeTicketListByAccountId";
			var result = new CmsCORSResult<List<SeTicket>>((int)CmsResultCodes.Initializing
				, string.Format("Initializing {0}.", METHOD_NAME));

			#endregion Initializes

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME, user =>
			{

				#region TRY
				try
				{
					// ** Create Service

					var ieService = SosServiceEngine.Instance.FunctionalServices.Instance<IScheduleEngineService>();
					IFnsResult<List<IFnsSeTicket>> oFnsModel = ieService.SeTicketListByAccountId(id);

					/** Check corsResult. */
					if (oFnsModel.Code != 0)
					{
						result.Code = oFnsModel.Code;
						result.Message = oFnsModel.Message;
						return result;
					}

					/** Setup return corsResult. */
					var oTsTicketList = ConvertTo.CastFnsToSeTicketList((List<IFnsSeTicket>)oFnsModel.GetValue());

					/** Save success results. */
					result.Code = (int)CmsResultCodes.Success;
					result.SessionId = user.SessionID;
					result.Message = "Success";
					result.Value = oTsTicketList;
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



		//this method will return the list of Tickets based on the given block id BID
		[Route("SeTicketList/{id}/BID")]
		[HttpGet]
		public CmsCORSResult<List<SeTicket>> SeTicketListByBlockId(long id)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Get SeTicketListByBlockId";
			var result = new CmsCORSResult<List<SeTicket>>((int)CmsResultCodes.Initializing
				, string.Format("Initializing {0}.", METHOD_NAME));

			#endregion Initializes

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME, user =>
			{

				#region TRY
				try
				{
					// ** Create Service

					var ieService = SosServiceEngine.Instance.FunctionalServices.Instance<IScheduleEngineService>();
					IFnsResult<List<IFnsSeTicket>> oFnsModel = ieService.SeTicketListByBlockId(id);

					/** Check corsResult. */
					if (oFnsModel.Code != 0)
					{
						result.Code = oFnsModel.Code;
						result.Message = oFnsModel.Message;
						return result;
					}

					/** Setup return corsResult. */
					var oTsTicketList = ConvertTo.CastFnsToSeTicketList((List<IFnsSeTicket>)oFnsModel.GetValue());

					/** Save success results. */
					result.Code = (int)CmsResultCodes.Success;
					result.SessionId = user.SessionID;
					result.Message = "Success";
					result.Value = oTsTicketList;
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


		//this method will return the list of Tickets based on the given technicianid
		[Route("SeTicketList/{id}/TID")]
		[HttpGet]
		public CmsCORSResult<List<SeTicket>> SeTicketListByTechnicianId(string id)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Get SeTicketListByTechnicianId";
			var result = new CmsCORSResult<List<SeTicket>>((int)CmsResultCodes.Initializing
				, string.Format("Initializing {0}.", METHOD_NAME));

			#endregion Initializes

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME, user =>
			{
				#region TRY
				try
				{
					// ** Create Service

					var ieService = SosServiceEngine.Instance.FunctionalServices.Instance<IScheduleEngineService>();
					IFnsResult<List<IFnsSeTicket>> oFnsModel = ieService.SeTicketListByTechnicianId(id);

					/** Check corsResult. */
					if (oFnsModel.Code != 0)
					{
						result.Code = oFnsModel.Code;
						result.Message = oFnsModel.Message;
						return result;
					}

					/** Setup return corsResult. */
					var oTsTicketList = ConvertTo.CastFnsToSeTicketList((List<IFnsSeTicket>)oFnsModel.GetValue());

					/** Save success results. */
					result.Code = (int)CmsResultCodes.Success;
					result.SessionId = user.SessionID;
					result.Message = "Success";
					result.Value = oTsTicketList;
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


		//this method will return SeTicket based on MonitoringStationNo MN
		[Route("SeTicket/{id}/MN")]
		[HttpPost]
		public CmsCORSResult<SeTicket> SeTicketGetByMonitoringStationNo(long id)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "SeTicketGetByMonitoringStationNo";

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME, user =>
			{
				#region Parameter Validation

				var argArray = new List<CORSArg>();
				if (id == 0)
				{
					argArray.Add(new CORSArg(null, (true), "<li>No Monitronics number where passed.</li>"));
				}

				CmsCORSResult<SeTicket> result;
				if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

				#endregion Parameter Validation

				#region TRY

				try
				{
					// ** Create Service
					var seService = SosServiceEngine.Instance.FunctionalServices.Instance<IScheduleEngineService>();

					IFnsResult<IFnsSeTicket> fnsResult = seService.SeTicketGetByMonitoringStationNo(id);

					// ** Save result
					result.Code = fnsResult.Code;
					result.SessionId = user.SessionID;
					result.Message = fnsResult.Message;

					// ** Get Values
					var oSeTicket = ConvertTo.CastFnsToSeTicket((IFnsSeTicket)fnsResult.GetValue());
					if (result.Code == (int)CmsResultCodes.Success && oSeTicket != null)
					{
						result.Value = oSeTicket;
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



		//this method will return SeTicket based on scheduleTicketID
		[Route("SeTicket/{id}/STID")]
		[HttpGet]
		public CmsCORSResult<SeTicket> SeTicketGetByScheduleTicketId(long id)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "SeTicketGetByScheduleTicketId";

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME, user =>
			{
				#region Parameter Validation

				var argArray = new List<CORSArg>();
				if (id == 0)
				{
					argArray.Add(new CORSArg(null, (true), "<li>No ScheduleTicketId where passed.</li>"));
				}

				CmsCORSResult<SeTicket> result;
				if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

				#endregion Parameter Validation

				#region TRY

				try
				{
					// ** Create Service
					var seService = SosServiceEngine.Instance.FunctionalServices.Instance<IScheduleEngineService>();

					IFnsResult<IFnsSeTicket> fnsResult = seService.SeTicketGetByScheduleTicketId(id);

					// ** Save result
					result.Code = fnsResult.Code;
					result.SessionId = user.SessionID;
					result.Message = fnsResult.Message;

					// ** Get Values
					var oSeTicket = ConvertTo.CastFnsToSeTicket((IFnsSeTicket)fnsResult.GetValue());
					if (result.Code == (int)CmsResultCodes.Success && oSeTicket != null)
					{
						result.Value = oSeTicket;
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




		//this method will return the list of Tickets 
		[Route("SeTicketList")]
		[HttpGet]
		public CmsCORSResult<List<SeTicket>> SeTicketList()
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Get SeTicketList";
			var result = new CmsCORSResult<List<SeTicket>>((int)CmsResultCodes.Initializing
				, string.Format("Initializing {0}.", METHOD_NAME));

			#endregion Initializes

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME, user =>
			{

				#region TRY
				try
				{
					// ** Create Service

					var ieService = SosServiceEngine.Instance.FunctionalServices.Instance<IScheduleEngineService>();
					IFnsResult<List<IFnsSeTicket>> oFnsModel = ieService.SeTicketList();

					/** Check corsResult. */
					if (oFnsModel.Code != 0)
					{
						result.Code = oFnsModel.Code;
						result.Message = oFnsModel.Message;
						return result;
					}

					/** Setup return corsResult. */
					var oTsTicketList = ConvertTo.CastFnsToSeTicketList((List<IFnsSeTicket>)oFnsModel.GetValue());

					/** Save success results. */
					result.Code = (int)CmsResultCodes.Success;
					result.SessionId = user.SessionID;
					result.Message = "Success";
					result.Value = oTsTicketList;
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



		// this method will set IsTechEnRoute(ITER) status to true
		// and send email to customer regarding the ticket status (enroute)
		[Route("SeTicket/{id}/ITER")]
		[HttpPost]
		public CmsCORSResult<SeTicket> SeTicketUpdateITER(long id)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "SeTicketUpdateITER";

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME, user =>
			{
				#region Parameter Validation

				var argArray = new List<CORSArg>();
				if (id == 0)
				{
					argArray.Add(new CORSArg(null, (true), "<li>No SeTicketId where passed.</li>"));
				}

				CmsCORSResult<SeTicket> result;
				if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

				#endregion Parameter Validation

				#region TRY

				try
				{
					// ** Create Service
					var seService = SosServiceEngine.Instance.FunctionalServices.Instance<IScheduleEngineService>();

					IFnsResult<IFnsSeTicket> fnsResult = seService.SeTicketUpdateITER(id, user.GPEmployeeID);

					// ** Save result
					result.Code = fnsResult.Code;
					result.SessionId = user.SessionID;
					result.Message = fnsResult.Message;

					// ** Get Values
					var oSeTicket = ConvertTo.CastFnsToSeTicket((IFnsSeTicket)fnsResult.GetValue());
					if (result.Code == (int)CmsResultCodes.Success && oSeTicket != null)
					{
						result.Value = oSeTicket;
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


		// this method will set IsTechDelayed(ITD) status to true
		// and send email to customer regarding the ticket status (enroute)
		[Route("SeTicket/{id}/ITD")]
		[HttpPost]
		public CmsCORSResult<SeTicket> SeTicketUpdateITD(long id)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "SeTicketUpdateITD";

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME, user =>
			{
				#region Parameter Validation

				var argArray = new List<CORSArg>();
				if (id == 0)
				{
					argArray.Add(new CORSArg(null, (true), "<li>No SeTicketId where passed.</li>"));
				}

				CmsCORSResult<SeTicket> result;
				if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

				#endregion Parameter Validation

				#region TRY

				try
				{
					// ** Create Service
					var seService = SosServiceEngine.Instance.FunctionalServices.Instance<IScheduleEngineService>();

					IFnsResult<IFnsSeTicket> fnsResult = seService.SeTicketUpdateITD(id, user.GPEmployeeID);

					// ** Save result
					result.Code = fnsResult.Code;
					result.SessionId = user.SessionID;
					result.Message = fnsResult.Message;

					// ** Get Values
					var oSeTicket = ConvertTo.CastFnsToSeTicket((IFnsSeTicket)fnsResult.GetValue());
					if (result.Code == (int)CmsResultCodes.Success && oSeTicket != null)
					{
						result.Value = oSeTicket;
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

		// this method will set IsTechCompleted(ITC) status to true
		// and send email to customer regarding the ticket status 
		[Route("SeTicket/{id}/ITC")]
		[HttpPost]
		public CmsCORSResult<SeTicket> SeTicketUpdateITC(long id, [FromBody]SeTicket seTicket)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "SeTicketUpdateITC";

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME, user =>
			{
				#region Parameter Validation

				var argArray = new List<CORSArg>();
				if (id == 0)
				{
					argArray.Add(new CORSArg(null, (true), "<li>No SeTicketId where passed.</li>"));
				}

				CmsCORSResult<SeTicket> result;
				if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

				#endregion Parameter Validation

				#region TRY

				try
				{
					// ** Create Service
					var seService = SosServiceEngine.Instance.FunctionalServices.Instance<IScheduleEngineService>();
					var fnsHeader = new FnsSeTicket(
						   seTicket.TicketID,
						   seTicket.ConfirmationNo,
						   seTicket.ClosingNote
						   );

					IFnsResult<IFnsSeTicket> fnsResult = seService.SeTicketUpdateITC(fnsHeader, user.GPEmployeeID);

					// ** Save result
					result.Code = fnsResult.Code;
					result.SessionId = user.SessionID;
					result.Message = fnsResult.Message;

					// ** Get Values
					var oSeTicket = ConvertTo.CastFnsToSeTicket((IFnsSeTicket)fnsResult.GetValue());
					if (result.Code == (int)CmsResultCodes.Success && oSeTicket != null)
					{
						result.Value = oSeTicket;
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
