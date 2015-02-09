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
	public class QuestionMeaningsController : ApiController
	{
		//// GET api/questionmeanings
		//[HttpGet]
		//public IApiResult Get()
		//{
		//	return CORSSecurity.Authorize("Get QuestionMeanings", null, null, user =>
		//	{
		//		var fnsResult = Service.QuestionMeaningGet(user.UserID);
		//		var result = new ApiResult<List<SvQuestionMeaning>>(fnsResult.Code, fnsResult.Message);
		//		if (result.Code == 0)
		//		{
		//			result.TValue = fnsResult.GetTValue().ConvertAll(FromFnsQuestionMeaning);
		//		}
		//		return result;
		//	});
		//}
		//
		//// GET api/questionmeanings/5
		//[HttpGet]
		//public IApiResult Get(int id)
		//{
		//	return CORSSecurity.Authorize("Get QuestionMeaning", null, null, user =>
		//	{
		//		var fnsResult = Service.QuestionMeaningGet(id, user.UserID);
		//		var result = new ApiResult<SvQuestionMeaning>(fnsResult.Code, fnsResult.Message);
		//		if (result.Code == 0)
		//		{
		//			result.TValue = FromFnsQuestionMeaning(fnsResult.GetTValue());
		//		}
		//		return result;
		//	});
		//}
		//

		[HttpPost, Route("QuestionMeanings/{id}")]
		public Result<SvQuestionMeaning> Update(int id, [FromBody]SvQuestionMeaning questionMeaning)
		{
			if (questionMeaning != null)
			{
				questionMeaning.QuestionMeaningID = id;
			}
			return Save(questionMeaning);
		}

		[HttpPost, Route("QuestionMeanings")]
		public Result<SvQuestionMeaning> Save([FromBody]SvQuestionMeaning questionMeaning)
		{
			return CORSSecurity.Authorize("Post QuestionMeaning", AuthApplications.SurveyManagerID, null, user =>
			{
				var argArray = new List<CORSArg>
				{
					//new CORSArg(questionMeaning.QuestionMeaningID, (questionMeaning.QuestionMeaningID == 0), "'QuestionMeaningID' was not passed."),
					new CORSArg(questionMeaning.SurveyTypeId, (questionMeaning.SurveyTypeId == 0), "'SurveyTypeId' was not passed."),
					new CORSArg(questionMeaning.Name, (string.IsNullOrEmpty(questionMeaning.Name)), "'Name' was not passed."),
				};
				var result = new Result<SvQuestionMeaning>();
				if (!CORSArg.ArgumentValidation(argArray, result))
				{
					return result;
				}

				var fnsResult = Service.QuestionMeaningPost(ToFnsQuestionMeaning(questionMeaning), user.UserID);
				result.Code = fnsResult.Code;
				result.Message = fnsResult.Message;
				if (result.Success)
				{
					result.Value = FromFnsQuestionMeaning(fnsResult.GetTValue());
				}
				return result;
			});
		}

		[HttpGet, Route("QuestionMeanings/{id}/QuestionMeaningTokenMaps")]
		public Result<List<SvQuestionMeaningTokenMap>> QuestionMeaningTokenMaps(int id)
		{
			return CORSSecurity.Authorize("Get QuestionMeaningTokenMaps for QuestionMeaningId", null, null, user =>
			{
				var fnsResult = Service.QuestionMeaningTokenMapsGetByQuestionMeaningId(id, user.UserID);
				var result = new Result<List<SvQuestionMeaningTokenMap>>(fnsResult.Code, fnsResult.Message);
				if (result.Success)
				{
					result.Value = fnsResult.GetTValue().ConvertAll(QuestionMeaningTokenMapsController.FromFnsQuestionMeaningTokenMap);
				}
				return result;
			});
		}




		internal static ISurveyEngineService Service
		{
			get { return SosServiceEngine.Instance.FunctionalServices.Instance<ISurveyEngineService>(); }
		}

		internal static SvQuestionMeaning FromFnsQuestionMeaning(IFnsQuestionMeaning fns)
		{
			return new SvQuestionMeaning
			{
				QuestionMeaningID = fns.QuestionMeaningID,
				SurveyTypeId = fns.SurveyTypeId,
				Name = fns.Name,
			};
		}
		internal static FnsQuestionMeaning ToFnsQuestionMeaning(SvQuestionMeaning item)
		{
			return new FnsQuestionMeaning
			{
				QuestionMeaningID = item.QuestionMeaningID,
				Name = item.Name,
				SurveyTypeId = item.SurveyTypeId,
			};
		}
	}
}