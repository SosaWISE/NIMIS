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
using SSE.Services.CmsCORS.Models.SurveyEngine;
using SOS.Lib.Core;

namespace SSE.Services.CmsCORS.Controllers.Surveys
{
	[RoutePrefix("SurveySrv")]
	public class QuestionsController : ApiController
	{
		//// GET SurveySrv/Questions
		//[HttpGet]
		//public IApiResult Get()
		//{
		//	return CORSSecurity.Authorize("Get Questions", null, null, user =>
		//	{
		//		var fnsResult = Service.QuestionGet(user.UserID);
		//		var result = new ApiResult<List<SvQuestion>>(fnsResult.Code, fnsResult.Message);
		//		if (result.Code == 0)
		//		{
		//			result.TValue = fnsResult.GetTValue().ConvertAll(FromFnsQuestion);
		//		}
		//		return result;
		//	});
		//}
		//
		//// GET SurveySrv/Questions/{id}
		//[HttpGet]
		//public IApiResult Get(int id)
		//{
		//	return CORSSecurity.Authorize("Get Question by id", null, null, user =>
		//	{
		//		var fnsResult = Service.QuestionGet(id, user.UserID);
		//		var result = new ApiResult<SvQuestion>(fnsResult.Code, fnsResult.Message);
		//		if (result.Code == 0)
		//		{
		//			result.TValue = FromFnsQuestion(fnsResult.GetTValue());
		//		}
		//		return result;
		//	});
		//}

		[HttpPost, Route("Questions")]
		public Result<SvQuestion> Post([FromBody]SvQuestion question)
		{
			return CORSSecurity.Authorize("Post Question", AuthApplications.SurveyManagerID, null, user =>
			{
				var result = new Result<SvQuestion>();
				if (question == null)
				{
					result.Code = -1;
					result.Message = "No question posted";
					return result;
				}

				var argArray = new List<CORSArg>
				{
					new CORSArg(question.SurveyId, (question.SurveyId == 0), "'SurveyId' must be passed."),
					new CORSArg(question.QuestionMeaningId, (question.QuestionMeaningId == 0), "'QuestionMeaningId' must be passed."),
					new CORSArg(question.GroupOrder, (question.GroupOrder == 0), "'GroupOrder' must be passed.")
				};
				if (!CORSArg.ArgumentValidation(argArray, result))
				{
					return result;
				}

				var fnsResult = Service.QuestionPost(ToFnsQuestion(question), user.UserID);
				result.Code = fnsResult.Code;
				result.Message = fnsResult.Message;
				if (result.Success)
				{
					result.Value = FromFnsQuestion(fnsResult.GetTValue());
				}
				return result;
			});
		}

		// GET SurveySrv/questions/{id}/questionPossibleAnswerMaps
		[HttpGet, Route("Questions/{id}/QuestionPossibleAnswerMaps")]
		public Result<List<SvQuestionPossibleAnswerMap>> QuestionPossibleAnswerMaps(int id)
		{
			return CORSSecurity.Authorize("Get QuestionPossibleAnswerMaps for QuestionID", null, null, user =>
			{
				var fnsResult = Service.QuestionPossibleAnswerMapsGetByQuestionId(id, user.UserID);
				var result = new Result<List<SvQuestionPossibleAnswerMap>>(fnsResult.Code, fnsResult.Message);
				if (result.Success)
				{
					result.Value = fnsResult.GetTValue().ConvertAll(FromFnsQuestionPossibleAnswerMap);
				}
				return result;
			});
		}

		[HttpPost, Route("Questions/{id}/Swap/{sibid}")]
		public Result<bool> QuestionPossibleAnswerMaps(int id, int sibid, [FromBody]SwapGroupOrder swapData)
		{
			return CORSSecurity.Authorize("Swap Question GroupOrders", AuthApplications.SurveyManagerID, null, user =>
			{
				var fnsResult = Service.SwapQuestionGroupOrder(id, swapData.MyGroupOrder, sibid, swapData.SibGroupOrder, user.UserID);
				var result = new Result<bool>(fnsResult.Code, fnsResult.Message);
				if (result.Success)
				{
					result.Value = true;
				}
				return result;
			});
		}



		private static ISurveyEngineService Service
		{
			get { return SosServiceEngine.Instance.FunctionalServices.Instance<ISurveyEngineService>(); }
		}

		internal static SvQuestion FromFnsQuestion(IFnsQuestion fns)
		{
			return new SvQuestion
			{
				QuestionID = fns.QuestionID,
				SurveyId = fns.SurveyId,
				QuestionMeaningId = fns.QuestionMeaningId,
				ParentId = fns.ParentId,
				GroupOrder = fns.GroupOrder,
				MapToTokenId = fns.MapToTokenId,
				ConditionJson = fns.ConditionJson,
			};
		}
		private static FnsQuestion ToFnsQuestion(SvQuestion item)
		{
			return new FnsQuestion
			{
				QuestionID = item.QuestionID,
				SurveyId = item.SurveyId,
				QuestionMeaningId = item.QuestionMeaningId,
				ParentId = item.ParentId,
				GroupOrder = item.GroupOrder,
				MapToTokenId = item.MapToTokenId,
				ConditionJson = item.ConditionJson,
			};
		}
		private static SvQuestionPossibleAnswerMap FromFnsQuestionPossibleAnswerMap(IFnsQuestionPossibleAnswerMap fns)
		{
			return new SvQuestionPossibleAnswerMap
			{
				QuestionId = fns.QuestionId,
				PossibleAnswerId = fns.PossibleAnswerId,
				Expands = fns.Expands,
				Fails = fns.Fails,
			};
		}

	}
}
