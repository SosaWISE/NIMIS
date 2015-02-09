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
	public class QuestionTranslationsController : ApiController
	{
		//// GET SurveySrv/QuestionTranslations
		//[HttpGet]
		//public IApiResult Get()
		//{
		//	return CORSSecurity.Authorize("Get QuestionTranslations", null, null, user =>
		//	{
		//		var fnsResult = Service.QuestionTranslationsGet(user.UserID);
		//		var result = new ApiResult<List<SvQuestionTranslation>>(fnsResult.Code, fnsResult.Message);
		//		if (result.Code == 0)
		//		{
		//			result.TValue = fnsResult.GetTValue().ConvertAll(FromFnsQuestionTranslation);
		//		}
		//		return result;
		//	});
		//}
		//
		//// GET SurveySrv/QuestionTranslations/{id}
		//[HttpGet]
		//public IApiResult Get(int id)
		//{
		//	return CORSSecurity.Authorize("Get QuestionTranslation by id", null, null, user =>
		//	{
		//		var fnsResult = Service.QuestionTranslationsGet(id, user.UserID);
		//		var result = new ApiResult<SvQuestionTranslation>(fnsResult.Code, fnsResult.Message);
		//		if (result.Code == 0)
		//		{
		//			result.TValue = FromFnsQuestionTranslation(fnsResult.GetTValue());
		//		}
		//		return result;
		//	});
		//}


		[HttpPost, Route("QuestionTranslations/{id}")]
		public Result<SvQuestionTranslation> Update(int id, [FromBody]SvQuestionTranslation qt)
		{
			if (qt != null)
			{
				qt.QuestionTranslationID = id;
			}
			return Save(qt);
		}

		[HttpPost, Route("QuestionTranslations")]
		public Result<SvQuestionTranslation> Save([FromBody]SvQuestionTranslation qt)
		{
			return CORSSecurity.Authorize("Post QuestionTranslation", AuthApplications.SurveyManagerID, null, user =>
			{
				var result = new Result<SvQuestionTranslation>();
				if (qt == null)
				{
					result.Code = -1;
					result.Message = "No question translation posted";
					return result;
				}

				var argArray = new List<CORSArg>
				{
					new CORSArg(qt.SurveyTranslationId, (qt.SurveyTranslationId == 0), "'SurveyTranslationId' must be passed."),
					new CORSArg(qt.QuestionId, (qt.QuestionId == 0), "'QuestionId' must be passed."),
					new CORSArg(qt.TextFormat, string.IsNullOrEmpty(qt.TextFormat), "'Token' must be passed."),
				};
				if (!CORSArg.ArgumentValidation(argArray, result))
				{
					return result;
				}

				var fnsResult = Service.QuestionTranslationPost(ToFnsQuestionTranslation(qt), user.UserID);
				result.Code = fnsResult.Code;
				result.Message = fnsResult.Message;
				if (result.Success)
				{
					result.Value = FromFnsQuestionTranslation(fnsResult.GetTValue());
				}
				return result;
			});
		}



		private static ISurveyEngineService Service
		{
			get { return SosServiceEngine.Instance.FunctionalServices.Instance<ISurveyEngineService>(); }
		}

		internal static SvQuestionTranslation FromFnsQuestionTranslation(IFnsQuestionTranslation fns)
		{
			return new SvQuestionTranslation
			{
				QuestionTranslationID = fns.QuestionTranslationID,
				SurveyTranslationId = fns.SurveyTranslationId,
				QuestionId = fns.QuestionId,
				TextFormat = fns.TextFormat,
			};
		}
		private static FnsQuestionTranslation ToFnsQuestionTranslation(SvQuestionTranslation item)
		{
			return new FnsQuestionTranslation
			{
				QuestionTranslationID = item.QuestionTranslationID,
				SurveyTranslationId = item.SurveyTranslationId,
				QuestionId = item.QuestionId,
				TextFormat = item.TextFormat,
			};
		}
	}
}
