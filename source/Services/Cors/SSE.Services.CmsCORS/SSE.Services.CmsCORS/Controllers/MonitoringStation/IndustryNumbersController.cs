using System.Collections.Generic;
using System.Web.Http;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.Services.Interfaces.Models.MonitoringStation;
using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;

namespace SSE.Services.CmsCORS.Controllers.MonitoringStation
{
	[RoutePrefix("MonitoringStationSrv")]
	public class IndustryNumbersController : ApiController
	{
		// GET: /IndustryNumbers/
		[Route("IndustryNumbers/{id}/GetByAccountId")]
		[HttpGet]
		public CmsCORSResult<List<MsIndustryAccount>> GetByAccountId(long id)
		{
			return IndustryAccountsByAccountId(id);
		}
		[Route("IndustryNumbers/{id}/GetByAccountId")]
		[HttpPost]
		public CmsCORSResult<MsIndustryAccount> Generate(MsIndustryAccountReq argObject)
		{
			return Generate(argObject.AccountId);
		}

		[Route("MsAccounts/{id}/GenerateIndustryAccount")]
		[HttpPost]
		public CmsCORSResult<MsIndustryAccount> Generate(long id)
		{
			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper("Generate", user =>
			{
				var result = new CmsCORSResult<MsIndustryAccount>();

				// ** Create Service
				var service = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();
				var fnsResult = service.MsIndustryNumberGenerate(id, true, user.GPEmployeeID);
				result.Code = fnsResult.Code;
				result.Message = fnsResult.Message;
				if (fnsResult.GetValue() != null)
				{
					result.Value = ConvertTo.CastFnsToMsIndustryAccount(fnsResult.GetTValue());
				}
				result.SessionId = user.SessionID;

				return result;
			});
		}

		[Route("MsIndustryAccounts/{id}/Primary")]
		[HttpPost]
		public CmsCORSResult<bool> IndustryNumberSaveAsPrimary(long id)
		{
			return CORSSecurity.AuthenticationWrapper("IndustryNumberSaveAsPrimary", user =>
			{
				// ** Initialize
				var result = new CmsCORSResult<bool>();

				// ** Execute
				var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();
				var oFnsModel = oService.MsIndustryNumberSetAsPrimary(id, user.GPEmployeeID);
				result.Code = oFnsModel.Code;
				result.Message = oFnsModel.Message;

				// ** Return result
				return result;
			});
		}
		//monitoringStationSrv/msIndustryAccounts/10175/Secondary
		[Route("MsIndustryAccounts/{id}/Secondary")]
		[HttpPost]
		public CmsCORSResult<bool> IndustryNumberSaveAsSecondary(long id)
		{
			return CORSSecurity.AuthenticationWrapper("IndustryNumberSaveAsSecondary", user =>
			{
				// ** Initialize
				var result = new CmsCORSResult<bool>();

				// ** Execute
				var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();
				var oFnsModel = oService.MsIndustryNumberSetAsSecondary(id, user.GPEmployeeID);
				result.Code = oFnsModel.Code;
				result.Message = oFnsModel.Message;

				// ** Return result
				return result;
			});
		}

		[Route("MsAccounts/{id}/IndustryAccounts")]
		[HttpGet]
		public CmsCORSResult<List<MsIndustryAccount>> IndustryAccountsByAccountId(long id)
		{
			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper("IndustryAccountsByAccountId", user =>
			{
				var result = new CmsCORSResult<List<MsIndustryAccount>>();

				// ** Create Service
				var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();
				var oFnsModel = oService.MsIndustryNumbersGet(id, user.GPEmployeeID);
				result.Code = oFnsModel.Code;
				result.Message = oFnsModel.Message;
				if (oFnsModel.GetValue() != null)
				{
					result.Value = ConvertTo.CastFnsToMsIndustryAccountList(oFnsModel.GetTValue());
				}
				return result;
			});
		}

		[Route("MsAccounts/{id}/IndustryAccountWithReceiverLines")]
		[HttpGet]
		public CmsCORSResult<List<MsIndustryAccountWithReceiverLineInfo>> IndustryAccountWithReceiverLineInfoByAccountId(
			long id)
		{
			// ** Authentication first. */
			return CORSSecurity.AuthenticationWrapper("IndustryAccountWithReceiverLineInfoByAccountId", user =>
			{
				// ** Initialize
				var result = new CmsCORSResult<List<MsIndustryAccountWithReceiverLineInfo>>();

				var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();
				var oFnsModel = oService.MsIndustryNumberWithReceiverLineGet(id, user.GPEmployeeID);
				result.Code = oFnsModel.Code;
				result.Message = oFnsModel.Message;
				if (oFnsModel.GetValue() != null)
				{
					result.Value = ConvertTo.CastFnsToMsIndustryAccountList(oFnsModel.GetTValue());
				}

				// ** Return result
				return result;
			});
		}
	}
}
