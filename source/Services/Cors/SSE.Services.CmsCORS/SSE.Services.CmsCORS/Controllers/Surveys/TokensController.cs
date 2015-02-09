using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Http;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.SurveyEngine;
using SOS.FunctionalServices.Models.SurveyEngine;
using SOS.Services.Interfaces.Models.SurveyEngine;
using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;
using AuthApplications = SOS.Data.AuthenticationControl.AC_Application.MetaData;
using AuthActions = SOS.Data.AuthenticationControl.AC_Action.MetaData;
using SOS.Lib.Core;

namespace SSE.Services.CmsCORS.Controllers.Surveys
{
	[RoutePrefix("SurveySrv")]
	public class TokensController : ApiController
	{
		[HttpGet, Route("Tokens")]
		public Result<List<SvToken>> Get()
		{
			return CORSSecurity.Authorize("Get Tokens", null, null, user =>
			{
				var fnsResult = Service.TokensGet(user.UserID);
				var result = new Result<List<SvToken>>(fnsResult.Code, fnsResult.Message);
				if (result.Success)
				{
					result.Value = fnsResult.GetTValue().ConvertAll(FromFnsToken);
				}
				return result;
			});
		}

		//// GET SurveySrv/Tokens/{id}
		//[HttpGet]
		//public IApiResult Get(int id)
		//{
		//	return CORSSecurity.Authorize("Get Token by id", null, null, user =>
		//	{
		//		var fnsResult = Service.TokenGet(id, user.UserID);
		//		var result = new ApiResult<SvToken>(fnsResult.Code, fnsResult.Message);
		//		if (result.Code == 0)
		//		{
		//			result.TValue = FromFnsToken(fnsResult.GetTValue());
		//		}
		//		return result;
		//	});
		//}
		//
		//[HttpPost]
		//public IApiResult Post([FromBody]SvToken token)
		//{
		//	return CORSSecurity.Authorize("Post Token", AuthApplications.SurveyManagerID, null, user =>
		//	{
		//		var result = new ApiResult<SvToken>();
		//		if (token == null)
		//		{
		//			result.Code = -1;
		//			result.Message = "No token posted";
		//			return result;
		//		}
		//
		//		var argArray = new List<CORSArg>
		//		{
		//			new CORSArg(token.Token, string.IsNullOrEmpty(token.Token), "'Token' must be passed.")
		//		};
		//		if (!CORSArg.ArgumentValidation(argArray, result))
		//		{
		//			return result;
		//		}
		//
		//		var fnsResult = Service.TokenPost(ToFnsToken(token), user.UserID);
		//		result.Code = fnsResult.Code;
		//		result.Message = fnsResult.Message;
		//		if (result.Code == 0)
		//		{
		//			result.TValue = FromFnsToken(fnsResult.GetTValue());
		//		}
		//		return result;
		//	});
		//}



		private static ISurveyEngineService Service
		{
			get { return SosServiceEngine.Instance.FunctionalServices.Instance<ISurveyEngineService>(); }
		}

		internal static SvToken FromFnsToken(IFnsSurveyToken fns)
		{
			return new SvToken
			{
				TokenID = fns.TokenID,
				Token = fns.Token,
			};
		}
		private static FnsSurveyToken ToFnsToken(SvToken item)
		{
			return new FnsSurveyToken
			{
				TokenID = item.TokenID,
				Token = item.Token,
			};
		}
	}
}
