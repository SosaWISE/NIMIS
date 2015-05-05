using System;
using System.Collections.Generic;
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
	public class SurveysController : ApiController
	{
		//// GET SurveySrv/Surveys
		//[HttpGet]
		//public IApiResult Get()
		//{
		//	return CORSSecurity.Authorize("Get Surveys", null, null, user =>
		//	{
		//		var fnsResult = Service.SurveysGet(user.UserID);
		//		var result = new ApiResult<List<SvSurvey>>(fnsResult.Code, fnsResult.Message);
		//		if (result.Code == 0)
		//		{
		//			result.TValue = fnsResult.GetTValue().ConvertAll(FromFnsSurvey);
		//		}
		//		return result;
		//	});
		//}

		// GET SurveySrv/Surveys/{id}
		[HttpGet, Route("Surveys/{id}")]
		public Result<SvSurvey> Get(int id)
		{
			return CORSSecurity.AuthorizeAny("Get Survey", null, null, user =>
			{
				var fnsResult = Service.SurveysGet(id, user.UserID);
				var result = new Result<SvSurvey>(fnsResult.Code, fnsResult.Message);
				if (result.Success)
				{
					result.Value = FromFnsSurvey(fnsResult.GetTValue());
				}
				return result;
			});
		}

		[HttpPost, Route("Surveys")]
		public Result<SvSurvey> Post([FromBody]SvSurvey survey)
		{
			return CORSSecurity.Authorize("Post Survey", AuthApplications.SurveyManagerID, null, user =>
			{
				var result = new Result<SvSurvey>();
				if (survey == null)
				{
					result.Code = -1;
					result.Message = "No survey posted";
					return result;
				}

				var argArray = new List<CORSArg>
				{
					new CORSArg(survey.SurveyTypeId, survey.SurveyTypeId == 0, "'SurveyTypeID' must be passed."),
					new CORSArg(survey.Version, string.IsNullOrEmpty(survey.Version), "'Version' must be passedk."),
				};
				if (!CORSArg.ArgumentValidation(argArray, result))
				{
					return result;
				}

				var fnsResult = Service.SurveyPost(ToFnsSurvey(survey), user.UserID);
				result.Code = fnsResult.Code;
				result.Message = fnsResult.Message;
				if (result.Success)
				{
					result.Value = FromFnsSurvey(fnsResult.GetTValue());
				}
				return result;
			});
		}

		[HttpGet, Route("Surveys/{id}/SurveyType")]
		public Result<SvSurveyType> SurveyType(int id)
		{
			return CORSSecurity.AuthorizeAny("Get SurveyType for SurveyID", null, null, user =>
			{
				var result = new Result<SvSurveyType>();
				if (CORSArg.ArgumentValidation(new List<CORSArg>() {
                    new CORSArg("", (id == 0), "'SurveyID' was not passed."),
                }, result))
				{
					var fnsResult = Service.SurveyTypeGetBySurveyId(id, user.UserID);
					result.Code = fnsResult.Code;
					result.Message = fnsResult.Message;
					if (result.Success)
					{
						result.Value = SurveyTypesController.FromFnsSurveyType(fnsResult.GetTValue());
					}
				}
				return result;
			});
		}

		[HttpGet, Route("Surveys/{id}/Questions")]
		public Result<List<SvQuestion>> Questions(int id)
		{
			return CORSSecurity.AuthorizeAny("Get Questions for SurveyID", null, null, user =>
			{
				var result = new Result<List<SvQuestion>>();
				if (CORSArg.ArgumentValidation(new List<CORSArg>() {
                    new CORSArg("", (id == 0), "'SurveyID' was not passed."),
                }, result))
				{
					var fnsResult = Service.QuestionsGetBySurveyId(id, user.UserID);
					result.Code = fnsResult.Code;
					result.Message = fnsResult.Message;
					if (result.Success)
					{
						result.Value = fnsResult.GetTValue().ConvertAll(QuestionsController.FromFnsQuestion);
					}
				}
				return result;
			});
		}

		[HttpGet, Route("Surveys/{id}/SurveyTranslations")]
		public Result<List<SvSurveyTranslations>> SurveyTranslations(int id)
		{
			return CORSSecurity.AuthorizeAny("Get SurveyTranslations for SurveyID", null, null, user =>
			{
				var result = new Result<List<SvSurveyTranslations>>();
				if (CORSArg.ArgumentValidation(new List<CORSArg>() {
                    new CORSArg("", (id == 0), "'SurveyID' was not passed."),
                }, result))
				{
					var fnsResult = Service.SurveyTranslationsGetBySurveyId(id, user.UserID);
					result.Code = fnsResult.Code;
					result.Message = fnsResult.Message;
					if (result.Success)
					{
						result.Value = fnsResult.GetTValue().ConvertAll(SurveyTranslationsController.FromFnsSurveyTranslation);
					}
				}
				return result;
			});
		}

		[HttpPost, Route("Surveys/{id}/Publish")]
		public Result<bool> Publish(int id)
		{
			return CORSSecurity.Authorize("Publish Survey", AuthApplications.SurveyManagerID, null, user =>
			{
				var result = new Result<bool>();
				if (CORSArg.ArgumentValidation(new List<CORSArg>() {
                    new CORSArg("", (id == 0), "'SurveyID' was not passed."),
                }, result))
				{
					var fnsResult = Service.PublishSurvey(id);
					return result.FromFnsResult(fnsResult);
				}
				return result;
			});
		}



		private static ISurveyEngineService Service
		{
			get { return SosServiceEngine.Instance.FunctionalServices.Instance<ISurveyEngineService>(); }
		}

		internal static SvSurvey FromFnsSurvey(IFnsSurvey fns)
		{
			return new SvSurvey
			{
				SurveyID = fns.SurveyID,
				SurveyTypeId = fns.SurveyTypeId,
				Version = fns.Version,
				IsCurrent = fns.IsCurrent,
				IsReadonly = fns.IsReadonly,
			};
		}
		private static FnsSurvey ToFnsSurvey(SvSurvey item)
		{
			return new FnsSurvey
			{
				SurveyID = item.SurveyID,
				SurveyTypeId = item.SurveyTypeId,
				Version = item.Version,
				IsCurrent = item.IsCurrent,
				IsReadonly = item.IsReadonly,
			};
		}
	}
}
