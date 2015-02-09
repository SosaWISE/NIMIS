using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.CentralStation;
using SOS.FunctionalServices.Models.CentralStation;
using SOS.Lib.Core.ErrorHandling;
using SOS.Services.Interfaces.Models;
using SOS.Services.Interfaces.Models.MonitoringStation;
using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;

namespace SSE.Services.CmsCORS.Controllers.MonitoringStation
{
	[RoutePrefix("MonitoringStationSrv")]
	public class DispatchAgenciesController : ApiController
	{
		#region Dispatch Agencies
		// GET api/dispatchagencies
		[Route("dispatchAgencies")] //?cityName={cityName}&state={state}&zipCode={zipCode}
		[HttpGet]
		public CmsCORSResult<List<MsDispatchAgency>> Get(string cityName, string stateAB, string zipCode)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "DispatchAgencies\\Get by City and Zip";
			// ReSharper disable once RedundantAssignment
			var result = new CmsCORSResult<List<MsDispatchAgency>>((int)CmsResultCodes.Initializing
				, string.Format("Initializing {0}.", METHOD_NAME));

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
				, user =>
				{
					#region Parameter Validation

					var argArray = new List<CORSArg>
					{
						new CORSArg(cityName, string.IsNullOrEmpty(cityName), "<li>'CityName' is empty and is required.</li>"),
						new CORSArg(stateAB, string.IsNullOrEmpty(stateAB), "<li>'State' is empty and is required.</li>"),
						new CORSArg(zipCode, string.IsNullOrEmpty(zipCode), "<li>'ZipCode' is empty and is required.</li>")
					};

					if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

					#endregion Parameter Validation

					#region Execute action

					return _get(cityName, stateAB, zipCode, METHOD_NAME, user);

					#endregion Execute action

				});
		}

		[Route("DispatchAgencies")] //?cityName={cityName}&zipCode={zipCode}
		[HttpGet]
		public CmsCORSResult<List<MsDispatchAgency>> Get(string cityName, string zipCode)
        {
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "DispatchAgencies\\Get by City and Zip";
// ReSharper disable once RedundantAssignment
			var result = new CmsCORSResult<List<MsDispatchAgency>>((int)CmsResultCodes.Initializing
				, string.Format("Initializing {0}.", METHOD_NAME));

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
				, user =>
				{
					#region Parameter Validation

					var argArray = new List<CORSArg>
					{
						new CORSArg(cityName, string.IsNullOrEmpty(cityName), "<li>'CityName' is empty and is required.</li>"),
						new CORSArg(zipCode, string.IsNullOrEmpty(zipCode), "<li>'ZipCode' is empty and is required.</li>")
					};

					if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

					#endregion Parameter Validation

					#region Execute action

					return _get(cityName, null, zipCode, METHOD_NAME, user);

					#endregion Execute action

				});

		}

		#region Private Methods

		private CmsCORSResult<List<MsDispatchAgency>> _get(string cityName, string state, string zipCode, string methodName, SseCmsUser user)
		{
			#region Initialize
			var result = new CmsCORSResult<List<MsDispatchAgency>>((int)CmsResultCodes.Initializing
				, string.Format("Initializing {0}.", methodName));
			#endregion Initialize

			#region TRY

			try
			{
				// ** Create Service
				var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();
				IFnsResult<List<IFnsMsDispatchAgencyView>> oFnsModel = oService.GetDispatchAgencies(cityName, state, zipCode,
					user.GPEmployeeID);
				/** Check corsResult. */
				if (oFnsModel.Code != 0)
				{
					result.Code = oFnsModel.Code;
					result.Message = oFnsModel.Message;
					return result;
				}

				/** Setup return corsResult. */
				var fnsMsDispatchAgencyViews = (List<IFnsMsDispatchAgencyView>)oFnsModel.GetValue();

				var msDispatchAgencies = fnsMsDispatchAgencyViews.Select(ConvertTo.CastFnsToMsDispatchAgenciesView).ToList();

				/** Save success results. */
				result.Code = (int)CmsResultCodes.Success;
				result.SessionId = user.SessionID;
				result.Message = "Success";
				result.Value = msDispatchAgencies;
			}
			#endregion TRY

			#region CATCH

			catch (Exception ex)
			{
				result.Code = (int)CmsResultCodes.ExceptionThrown;
				result.Message = string.Format("The following exception was thrown from '{0}' method: {1}",
					methodName,
					ex.Message);
			}

			#endregion CATCH

			#region Result

			return result;

			#endregion Result
		}

		#endregion Private Methods

		#endregion Dispatch Agencies

		#region Account Dispatch Agency Assignments

		[Route("AccountDispatchAgencyAssignments/{id}")]
		[HttpDelete]
		public CmsCORSResult<List<MsAccountDispatchAgencyAssignmentView>> DeleteDaAssignment(int id)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "CORS: DeleteDaAssignment";
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
						new CORSArg(id, (id == 0), "<li>'id' has to be passed.</li>")
					};

					if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

					#endregion Parameter Validations

					#region TRY

					try
					{
						// ** Create Service
						var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();
						IFnsResult<List<IFnsMsAccountDispatchAgencyAssignmentView>> oFnsModel = oService.DeleteDaAssignments(id, user.GPEmployeeID);

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

		[Route("AccountDispatchAgencyAssignments")]
		[HttpPost]
		public CmsCORSResult<MsAccountDispatchAgencyAssignmentView> SaveDaAssignment([FromBody] MsAccountDispatchAgencyAssignmentView agency)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "CORS: UpdateDaAssignment";
			// ReSharper disable once RedundantAssignment
			var result = new CmsCORSResult<MsAccountDispatchAgencyAssignmentView>((int)CmsResultCodes.Initializing
				, string.Format("Initializing {0}.", METHOD_NAME));

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
				, user =>
				{
					#region Parameter Validations

					var argArray = new List<CORSArg>
					{
						new CORSArg(agency, (agency == null), "<li>'agency' was not passed.")
					};

					if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

					#endregion Parameter Validations

					#region TRY

					try
					{
						// ** Cast to fns
						FnsMsAccountDispatchAgencyAssignmentView fnsItem = ConvertTo.CastMsToFnsMsAccountDispatchAgencyAssignmentView(agency);
						// ** Create Service
						var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();
						IFnsResult<IFnsMsAccountDispatchAgencyAssignmentView> oFnsModel = oService.SaveDaAssignments(fnsItem.DispatchAgencyAssignmentID, fnsItem, user.GPEmployeeID);

						if (oFnsModel.Code != BaseErrorCodes.ErrorCodes.Success.Code())
						{
							result.Code = oFnsModel.Code;
							result.Message = oFnsModel.Message;
							return result;
						}

						// ** Get assignments and return to REST.
						IFnsMsAccountDispatchAgencyAssignmentView fnsDispatchAgencyAssignment = oFnsModel.GetTValue();
						var assignmentsList = ConvertTo.CastFnsToMsAccountDispatchAgencyAssignmentView(fnsDispatchAgencyAssignment);

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

		[Route("AccountDispatchAgencyAssignments/{id}")]
		[HttpPost]
		public CmsCORSResult<MsAccountDispatchAgencyAssignmentView> SaveDaAssignment(int id, [FromBody] MsAccountDispatchAgencyAssignmentView agency)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "CORS: UpdateDaAssignment";
			// ReSharper disable once RedundantAssignment
			var result = new CmsCORSResult<MsAccountDispatchAgencyAssignmentView>((int)CmsResultCodes.Initializing
				, string.Format("Initializing {0}.", METHOD_NAME));

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
				, user =>
				{
					#region Parameter Validations

					var argArray = new List<CORSArg>
					{
						new CORSArg(agency, (agency == null), "<li>'agency' was not passed.")
					};

					if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

					#endregion Parameter Validations

					#region TRY

					try
					{
						// ** Cast to fns
						FnsMsAccountDispatchAgencyAssignmentView fnsItem = ConvertTo.CastMsToFnsMsAccountDispatchAgencyAssignmentView(agency);
						// ** Create Service
						var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();
						IFnsResult<IFnsMsAccountDispatchAgencyAssignmentView> oFnsModel = oService.SaveDaAssignments(id, fnsItem, user.GPEmployeeID);

						if (oFnsModel.Code != BaseErrorCodes.ErrorCodes.Success.Code())
						{
							result.Code = oFnsModel.Code;
							result.Message = oFnsModel.Message;
							return result;
						}

						// ** Get assignments and return to REST.
						IFnsMsAccountDispatchAgencyAssignmentView fnsDispatchAgencyAssignment = oFnsModel.GetTValue();
						var assignmentsList = ConvertTo.CastFnsToMsAccountDispatchAgencyAssignmentView(fnsDispatchAgencyAssignment);

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


		#endregion Account Dispatch Agency Assignments

		#region Agency Types
		// GET api/dispatchagencies

		[Route("dispatchAgencyTypes/{id}")]
		[HttpGet]
		public CmsCORSResult<List<MsDispatchAgencyType>> GetDispatchAgencyTypes(string id)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "DispatchAgencyTypes\\Get";
			// ReSharper disable once RedundantAssignment
			var result = new CmsCORSResult<List<MsDispatchAgencyType>>((int)CmsResultCodes.Initializing
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
					IFnsResult<List<IFnsMsDispatchAgencyType>> oFnsModel = oService.GetDispatchAgencyTypes(id, user.GPEmployeeID);
					/** Check corsResult. */
					if (oFnsModel.Code != 0)
					{
						result.Code = oFnsModel.Code;
						result.Message = oFnsModel.Message;
						return result;
					}

					/** Setup return corsResult. */
					var fnsMsDispatchAgencyType = (List<IFnsMsDispatchAgencyType>)oFnsModel.GetValue();

					var msDispatchAgencyTypes = fnsMsDispatchAgencyType.Select(ConvertTo.CastFnsToMsDispatchAgencyType).ToList();

					/** Save success results. */
					result.Code = (int)CmsResultCodes.Success;
					result.SessionId = user.SessionID;
					result.Message = "Success";
					result.Value = msDispatchAgencyTypes;
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

		#endregion Agency Types

	}
}