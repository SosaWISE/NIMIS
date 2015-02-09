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
	public class PossibleAnswersController : ApiController
	{
		[HttpGet, Route("PossibleAnswers")]
		public Result<List<SvPossibleAnswer>> Get()
		{
			return CORSSecurity.Authorize("Get PossibleAnswers", null, null, user =>
			{
				var fnsResult = Service.PossibleAnswersGet(user.UserID);
				var result = new Result<List<SvPossibleAnswer>>(fnsResult.Code, fnsResult.Message);
				if (result.Success)
				{
					result.Value = fnsResult.GetTValue().ConvertAll(FromFnsSurveyPossibleAnswer);
				}
				return result;
			});
		}

		//// GET api/possibleanswers/5
		//[HttpGet]
		//public IApiResult Get(int id)
		//{
		//	return CORSSecurity.Authorize("Get PossibleAnswer", null, null, user =>
		//	{
		//		var fnsResult = Service.PossibleAnswersGet(id, user.UserID);
		//		var result = new ApiResult<SvPossibleAnswer>(fnsResult.Code, fnsResult.Message);
		//		if (result.Code == 0)
		//		{
		//			result.TValue = FromFnsSurveyPossibleAnswer(fnsResult.GetTValue());
		//		}
		//		return result;
		//	});
		//}
		//
		//[HttpPost]
		//public IApiResult Post([FromBody]SvPossibleAnswer possibleAnswer)
		//{
		//	return CORSSecurity.Authorize("Post PossibleAnswer", AuthApplications.SurveyManagerID, null, user =>
		//	{
		//		var argArray = new List<CORSArg>
		//		{
		//			new CORSArg(possibleAnswer.AnswerText, string.IsNullOrEmpty(possibleAnswer.AnswerText),"'AnswerText' must be passed."),
		//		};
		//		var result = new ApiResult<SvPossibleAnswer>();
		//		if (!CORSArg.ArgumentValidation(argArray, result))
		//		{
		//			return result;
		//		}
		//
		//		var fnsResult = Service.PossibleAnswerPost(ToFnsSurveyPossibleAnswer(possibleAnswer), user.UserID);
		//		result.Code = fnsResult.Code;
		//		result.Message = fnsResult.Message;
		//		if (result.Code == (int)CmsResultCodes.Success)
		//		{
		//			result.TValue = FromFnsSurveyPossibleAnswer(fnsResult.GetTValue());
		//		}
		//		return result;
		//	});
		//}



		private static ISurveyEngineService Service
		{
			get { return SosServiceEngine.Instance.FunctionalServices.Instance<ISurveyEngineService>(); }
		}

		private static SvPossibleAnswer FromFnsSurveyPossibleAnswer(IFnsSurveyPossibleAnswers fns)
		{
			return new SvPossibleAnswer
			{
				PossibleAnswerID = fns.PossibleAnswersID,
				AnswerText = fns.AnswerText
			};
		}
		private static FnsSurveyPossibleAnswers ToFnsSurveyPossibleAnswer(SvPossibleAnswer item)
		{
			return new FnsSurveyPossibleAnswers
			{
				PossibleAnswersID = item.PossibleAnswerID,
				AnswerText = item.AnswerText
			};
		}
	}
}
