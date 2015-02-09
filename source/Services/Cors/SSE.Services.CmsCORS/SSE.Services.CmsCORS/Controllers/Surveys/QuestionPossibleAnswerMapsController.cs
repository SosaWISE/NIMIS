using System;
using System.Collections.Generic;
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
	public class QuestionPossibleAnswerMapsController : ApiController
	{
		// POST SurveySrv/QuestionPossibleAnswerMaps
		[HttpPost, Route("QuestionPossibleAnswerMaps")]
		public Result<SvQuestionPossibleAnswerMap> Post([FromBody]SvQuestionPossibleAnswerMap paMap)
		{
			return CORSSecurity.Authorize("Post QuestionPossibleAnswerMap", AuthApplications.SurveyManagerID, null, user =>
			{
				var argArray = new List<CORSArg>
				{
					new CORSArg(paMap.QuestionId, (paMap.QuestionId < 1), "'QuestionId' must be greater than 0."),
					new CORSArg(paMap.PossibleAnswerId, (paMap.PossibleAnswerId < 1), "'PossibleAnswerId' must be greater than 0."),
				};
				var result = new Result<SvQuestionPossibleAnswerMap>();
				if (!CORSArg.ArgumentValidation(argArray, result))
				{
					return result;
				}

				var fnsResult = Service.QuestionPossibleAnswerMapPost(ToFnsQuestionPossibleAnswerMap(paMap), user.UserID);
				result.Code = fnsResult.Code;
				result.Message = fnsResult.Message;
				if (result.Success)
				{
					result.Value = FromFnsQuestionPossibleAnswerMap(fnsResult.GetTValue());
				}
				return result;
			});
		}

		// DELETE SurveySrv/QuestionPossibleAnswerMaps
		[HttpDelete, Route("QuestionPossibleAnswerMaps/{questionId}/{possibleAnswerId}")]
		public Result<bool> Delete(int questionId, int possibleAnswerId)
		{
			return CORSSecurity.Authorize("Delete QuestionPossibleAnswerMap", AuthApplications.SurveyManagerID, null, user =>
			{
				var fnsResult = Service.QuestionPossibleAnswerMapDelete(questionId, possibleAnswerId, user.UserID);
				var result = new Result<bool>();
				return result.FromFnsResult(fnsResult);
			});
		}




		internal static ISurveyEngineService Service
		{
			get { return SosServiceEngine.Instance.FunctionalServices.Instance<ISurveyEngineService>(); }
		}

		internal static SvQuestionPossibleAnswerMap FromFnsQuestionPossibleAnswerMap(IFnsQuestionPossibleAnswerMap fns)
		{
			return new SvQuestionPossibleAnswerMap
			{
				QuestionId = fns.QuestionId,
				PossibleAnswerId = fns.PossibleAnswerId,
				Expands = fns.Expands,
				Fails = fns.Fails,
				CreatedOn = fns.CreatedOn,
			};
		}
		internal static FnsQuestionPossibleAnswerMap ToFnsQuestionPossibleAnswerMap(SvQuestionPossibleAnswerMap item)
		{
			return new FnsQuestionPossibleAnswerMap
			{
				QuestionId = item.QuestionId,
				PossibleAnswerId = item.PossibleAnswerId,
				Expands = item.Expands,
				Fails = item.Fails,
				CreatedOn = item.CreatedOn,
			};
		}
	}
}
