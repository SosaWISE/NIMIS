using System.Collections.Generic;
using System.Web.Http;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SurveyEngineContracts = SOS.FunctionalServices.Contracts.Models.SurveyEngine;
using SOS.FunctionalServices.Models.SurveyEngine;
using SOS.Services.Interfaces.Models.SurveyEngine;
using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;
using AuthApplications = SOS.Data.AuthenticationControl.AC_Application.MetaData;
using AuthActions = SOS.Data.AuthenticationControl.AC_Action.MetaData;
using SOS.Lib.Core;

namespace SSE.Services.CmsCORS.Controllers.Surveys
{
	public class ResultsController : ApiController
	{
		[HttpPost, Route("SurveySrv/Results")]
		public Result<SurveyEngineContracts.IFnsResult> Post([FromBody]SvResult body)
		{
			return CORSSecurity.AuthorizeAny("Post Survey Results", null, null, user =>
			{
				var result = new Result<SurveyEngineContracts.IFnsResult>();
				if (CORSArg.ArgumentValidation(new List<CORSArg>() {
                    new CORSArg("", (body == null), "No result posted."),
                }, result))
				{
					var answers = body.Answers ?? new List<SvAnswer>();
					var mttAnswers = body.MapToTokenAnswers ?? new List<MapToTokenAnswer>();
					var input = new FnsResult()
					{
						ResultID = body.ResultID,
						SurveyTranslationId = body.SurveyTranslationId,
						AccountId = body.AccountId,
						Passed = body.Passed,
						IsComplete = body.IsComplete,
						Context = body.Context,
						CreatedBy = body.CreatedBy,
						CreatedOn = body.CreatedOn,

						Answers = answers.ConvertAll((from) =>
						{
							return new FnsAnswer()
							{
								AnswerID = from.AnswerID,
								ResultId = from.ResultId,
								QuestionId = from.QuestionId,
								AnswerText = from.AnswerText,
							} as SOS.FunctionalServices.Contracts.Models.SurveyEngine.IFnsAnswer;
						}),
						MapToTokenAnswers = mttAnswers.ConvertAll((from) =>
						{
							return new FnsMapToTokenAnswer()
							{
								TokenId = from.TokenId,
								Answer = from.Answer,
							} as SOS.FunctionalServices.Contracts.Models.SurveyEngine.IFnsMapToTokenAnswer;
						}),
					};
					var fnsResult = Service.ResultSave(input, user.GPEmployeeID);
					return result.FromFnsResult(fnsResult);
				}
				return result;
			});
		}

		[HttpGet, Route("SurveySrv/Results/{id}/Answers")]
		public Result<List<SurveyEngineContracts.IFnsAnswer>> Answers(long id)
		{
			return CORSSecurity.Authorize("Answers", null, null, user =>
			{
				var result = new Result<List<SurveyEngineContracts.IFnsAnswer>>();
				if (CORSArg.ArgumentValidation(new List<CORSArg>() {
                    new CORSArg("", (id == 0), "'ResultID' was not passed."),
                }, result))
				{
					var fnsResult = Service.ResultAnswers(id);
					return result.FromFnsResult(fnsResult);
				}
				return result;
			});
		}

		[HttpGet, Route("MsAccountSetupSrv/Accounts/{id}/SurveyResults")]
		public Result<List<SurveyEngineContracts.IFnsResultView>> SurveyResults(long id)
		{
			return CORSSecurity.Authorize("SurveyResults", null, null, user =>
			{
				var result = new Result<List<SurveyEngineContracts.IFnsResultView>>();
				if (CORSArg.ArgumentValidation(new List<CORSArg>() {
                    new CORSArg("", (id == 0), "'AccountID' was not passed."),
                }, result))
				{
					var fnsResult = Service.ResultViewsForAccount(id);
					return result.FromFnsResult(fnsResult);
				}
				return result;
			});
		}




		private static ISurveyEngineService Service
		{
			get { return SosServiceEngine.Instance.FunctionalServices.Instance<ISurveyEngineService>(); }
		}
	}
}
