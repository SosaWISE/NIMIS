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
	public class QuestionMeaningTokenMapsController : ApiController
    {
        // POST SurveySrv/QuestionMeaningTokenMaps
		[HttpPost, Route("QuestionMeaningTokenMaps")]
		public Result<SvQuestionMeaningTokenMap> Post([FromBody]SvQuestionMeaningTokenMap qmtMap)
		{
			return CORSSecurity.Authorize("Post QuestionMeaningTokenMap", AuthApplications.SurveyManagerID, null, user =>
			{
				var argArray = new List<CORSArg>
				{
					new CORSArg(qmtMap.QuestionMeaningId, (qmtMap.QuestionMeaningId < 1), "'QuestionMeaningId' must be greater than 0."),
					new CORSArg(qmtMap.TokenId, (qmtMap.TokenId < 1), "'TokenId' must be greater than 0."),
				};
				var result = new Result<SvQuestionMeaningTokenMap>();
				if (!CORSArg.ArgumentValidation(argArray, result))
				{
					return result;
				}

				var fnsResult = Service.QuestionMeaningTokenMapPost(ToFnsQuestionMeaningTokenMap(qmtMap), user.UserID);
				result.Code = fnsResult.Code;
				result.Message = fnsResult.Message;
				if (result.Success)
				{
					result.Value = FromFnsQuestionMeaningTokenMap(fnsResult.GetTValue());
				}
				return result;
			});
		}




		internal static ISurveyEngineService Service
		{
			get { return SosServiceEngine.Instance.FunctionalServices.Instance<ISurveyEngineService>(); }
		}

		internal static SvQuestionMeaningTokenMap FromFnsQuestionMeaningTokenMap(IFnsQuestionMeaningTokenMap fns)
		{
			return new SvQuestionMeaningTokenMap
			{
				QuestionMeaningId = fns.QuestionMeaningId,
				TokenId = fns.TokenId,
				CreatedOn = fns.CreatedOn,
			};
		}
		internal static FnsQuestionMeaningTokenMap ToFnsQuestionMeaningTokenMap(SvQuestionMeaningTokenMap item)
		{
			return new FnsQuestionMeaningTokenMap
			{
				QuestionMeaningId = item.QuestionMeaningId,
				TokenId = item.TokenId,
				CreatedOn = item.CreatedOn,
			};
		}
	}
}