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
	public class SurveyTranslationsController : ApiController
	{
		//// GET SurveySrv/SurveyTranslations
		//[HttpGet]
		//public IApiResult Get()
		//{
		//	return CORSSecurity.Authorize("Get SurveyTranslations", null, null, user =>
		//	{
		//		var fnsResult = Service.SurveyTranslationsGet(user.UserID);
		//		var result = new ApiResult<List<SvSurveyTranslations>>(fnsResult.Code, fnsResult.Message);
		//		if (result.Code == 0)
		//		{
		//			result.TValue = fnsResult.GetTValue().ConvertAll(FromFnsSurveyTranslation);
		//		}
		//		return result;
		//	});
		//}
		//
		//// GET SurveySrv/SurveyTranslations/{id}
		//[HttpGet]
		//public IApiResult Get(int id)
		//{
		//	return CORSSecurity.Authorize("Get SurveyTranslation", null, null, user =>
		//	{
		//		var fnsResult = Service.SurveyTranslationsGet(id, user.UserID);
		//		var result = new ApiResult<SvSurveyTranslations>(fnsResult.Code, fnsResult.Message);
		//		if (result.Code == 0)
		//		{
		//			result.TValue = FromFnsSurveyTranslation(fnsResult.GetTValue());
		//		}
		//		return result;
		//	});
		//}

		[HttpPost, Route("SurveyTranslations")]
		public Result<SvSurveyTranslations> Post([FromBody]SvSurveyTranslations surveyTranslation)
		{
			return CORSSecurity.Authorize("Post SurveyTranslation", AuthApplications.SurveyManagerID, null, user =>
			{
				var result = new Result<SvSurveyTranslations>();
				if (surveyTranslation == null)
				{
					result.Code = -1;
					result.Message = "No survey posted";
					return result;
				}

				var argArray = new List<CORSArg>
				{
					new CORSArg(surveyTranslation.SurveyId, surveyTranslation.SurveyId == 0, "'SurveyId' must be passed."),
					new CORSArg(surveyTranslation.LocalizationCode, string.IsNullOrEmpty(surveyTranslation.LocalizationCode), "'LocalizationCode' must be passed."),
				};
				if (!CORSArg.ArgumentValidation(argArray, result))
				{
					return result;
				}

				var fnsResult = Service.SurveyTranslationPost(ToFnsSurveyTranslation(surveyTranslation), user.UserID);
				result.Code = fnsResult.Code;
				result.Message = fnsResult.Message;
				if (result.Success)
				{
					result.Value = FromFnsSurveyTranslation(fnsResult.GetTValue());
				}
				return result;
			});
		}

		[HttpGet, Route("SurveyTranslations/{id}/QuestionTranslations")]
		public Result<List<SvQuestionTranslation>> QuestionTranslations(int id)
		{
			return CORSSecurity.Authorize("Get QuestionTranslations for SurveyID", null, null, user =>
			{
				var result = new Result<List<SvQuestionTranslation>>();
				if (CORSArg.ArgumentValidation(new List<CORSArg>() {
                    new CORSArg("", (id == 0), "'SurveyID' was not passed."),
                }, result))
				{
					var fnsResult = Service.QuestionTranslationsGetBySurveyTranslationsId(id, user.UserID);
					result.Code = fnsResult.Code;
					result.Message = fnsResult.Message;
					if (result.Success)
					{
						result.Value = fnsResult.GetTValue().ConvertAll(QuestionTranslationsController.FromFnsQuestionTranslation);
					}
				}
				return result;
			});
		}



		private static ISurveyEngineService Service
		{
			get { return SosServiceEngine.Instance.FunctionalServices.Instance<ISurveyEngineService>(); }
		}

		internal static SvSurveyTranslations FromFnsSurveyTranslation(IFnsSurveyTranslation fns)
		{
			return new SvSurveyTranslations
			{
				SurveyTranslationID = fns.SurveyTranslationID,
				SurveyId = fns.SurveyId,
				LocalizationCode = fns.LocalizationCode,
			};
		}
		private static FnsSurveyTranslation ToFnsSurveyTranslation(SvSurveyTranslations item)
		{
			return new FnsSurveyTranslation
			{
				SurveyTranslationID = item.SurveyTranslationID,
				SurveyId = item.SurveyId,
				LocalizationCode = item.LocalizationCode,
			};
		}
	}
}