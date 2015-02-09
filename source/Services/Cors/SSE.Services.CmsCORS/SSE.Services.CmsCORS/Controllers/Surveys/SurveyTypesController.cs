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
	public class SurveyTypesController : ApiController
	{
		[HttpGet, Route("SurveyTypes")]
		public Result<List<SvSurveyType>> Get()
		{
			return CORSSecurity.Authorize("Get SurveyTypes", null, null, user =>
			{
				var fnsResult = Service.SurveyTypesGet(user.UserID);
				var result = new Result<List<SvSurveyType>>(fnsResult.Code, fnsResult.Message);
				if (result.Success)
				{
					result.Value = fnsResult.GetTValue().ConvertAll(FromFnsSurveyType);
				}
				return result;
			});
		}

		//// GET SurveySrv/SurveyTypes/{id}
		//[HttpGet]
		//public IApiResult Get(int id)
		//{
		//	return CORSSecurity.Authorize("Get SurveyType", null, null, user =>
		//	{
		//		var fnsResult = Service.SurveyTypesGet(id, user.UserID);
		//		var result = new ApiResult<SvSurveyType>(fnsResult.Code, fnsResult.Message);
		//		if (result.Code == 0)
		//		{
		//			result.TValue = FromFnsSurveyType(fnsResult.GetTValue());
		//		}
		//		return result;
		//	});
		//}

		[HttpGet, Route("SurveyTypes/{id}/Surveys")]
		public Result<List<SvSurvey>> Surveys(int id)
		{
			return CORSSecurity.Authorize("Get Surveys for SurveyTypeID", null, null, user =>
			{
				var result = new Result<List<SvSurvey>>();
				if (CORSArg.ArgumentValidation(new List<CORSArg>() {
                    new CORSArg("", (id == 0), "'SurveyTypeID' was not passed."),
                }, result))
				{
					var fnsResult = Service.SurveysGetBySurveyTypeId(id, user.UserID);
					result.Code = fnsResult.Code;
					result.Message = fnsResult.Message;
					if (result.Success)
					{
						result.Value = fnsResult.GetTValue().ConvertAll(SurveysController.FromFnsSurvey);
					}
				}
				return result;
			});
		}

		[HttpGet, Route("SurveyTypes/{id}/QuestionMeanings")]
		public Result<List<SvQuestionMeaning>> QuestionMeanings(int id)
		{
			return CORSSecurity.Authorize("Get QuestionMeanings for SurveyTypeID", null, null, user =>
			{
				var result = new Result<List<SvQuestionMeaning>>();
				if (CORSArg.ArgumentValidation(new List<CORSArg>() {
                    new CORSArg("", (id == 0), "'SurveyTypeID' was not passed."),
                }, result))
				{
					var fnsResult = Service.QuestionMeaningsGetBySurveyTypeId(id, user.UserID);
					result.Code = fnsResult.Code;
					result.Message = fnsResult.Message;
					if (result.Success)
					{
						result.Value = fnsResult.GetTValue().ConvertAll(QuestionMeaningsController.FromFnsQuestionMeaning);
					}
				}
				return result;
			});
		}

		[HttpGet, Route("SurveyTypes/{id}/CurrentSurvey")]
		public Result<IFnsSurvey> CurrentSurvey(int id)
		{
			return CORSSecurity.Authorize("Get CurrentSurvey for SurveyTypeID", null, null, user =>
			{
				var result = new Result<IFnsSurvey>();
				if (CORSArg.ArgumentValidation(new List<CORSArg>() {
                    new CORSArg("", (id == 0), "'SurveyTypeID' was not passed."),
                }, result))
				{
					var fnsResult = Service.CurrentSurvey(id);
					return result.FromFnsResult(fnsResult);
				}
				return result;
			});
		}



		private static ISurveyEngineService Service
		{
			get { return SosServiceEngine.Instance.FunctionalServices.Instance<ISurveyEngineService>(); }
		}

		internal static SvSurveyType FromFnsSurveyType(IFnsSurveyType fns)
		{
			return new SvSurveyType
			{
				SurveyTypeID = fns.SurveyTypeID,
				Name = fns.Name,
			};
		}
		internal static FnsSurveyType ToFnsSurveyType(SvSurveyType item)
		{
			return new FnsSurveyType
			{
				SurveyTypeID = item.SurveyTypeID,
				Name = item.Name,
			};
		}
	}
}
