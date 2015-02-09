/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 10/30/13
 * Time: 15:29
 * 
 * Description:  Describes the Survey Engine Service for SSE.
 *********************************************************************************************************************/

using System.Collections.Generic;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.SurveyEngine;
using SurveyEngineContracts = SOS.FunctionalServices.Contracts.Models.SurveyEngine;

namespace SOS.FunctionalServices.Contracts
{
	public interface ISurveyEngineService : IFunctionalService
	{
		IFnsResult<List<IFnsSurveyType>> SurveyTypesGet(int userId);
		IFnsResult<IFnsSurveyType> SurveyTypesGet(int surveyTypeID, int userId);
		IFnsResult<IFnsSurveyType> SurveyTypePost(IFnsSurveyType fnsSurveyType, int userID);
        IFnsResult<IFnsSurveyType> SurveyTypeGetBySurveyId(int surveyId, int userID);

		IFnsResult<List<IFnsQuestionMeaning>> QuestionMeaningGet(int userID);
		IFnsResult<IFnsQuestionMeaning> QuestionMeaningGet(int questionMeaningID, int userID);
		IFnsResult<List<IFnsQuestionMeaning>> QuestionMeaningsGetBySurveyTypeId(int surveyTypeId, int userID);

		IFnsResult<List<IFnsQuestion>> QuestionGet(int userID);
		IFnsResult<IFnsQuestion> QuestionGet(int questionId, int userID);
		IFnsResult<IFnsQuestion> QuestionPost(IFnsQuestion fnsQuestion, int userId);
        IFnsResult<List<IFnsQuestion>> QuestionsGetBySurveyId(int surveyId, int userID);
		IFnsResult<bool> SwapQuestionGroupOrder(int questionId, int groupOrder, int sibQuestionId, int sibGroupOrder, int userID);

		IFnsResult<List<IFnsQuestionPossibleAnswerMap>> QuestionPossibleAnswerMapsGetByQuestionId(int questionId, int userID);

		IFnsResult<List<IFnsQuestionMeaningTokenMap>> QuestionMeaningTokenMapsGetByQuestionMeaningId(int questionMeaningId, int userID);

		IFnsResult<List<IFnsSurveyToken>> TokensGet(int userId);
		IFnsResult<IFnsSurveyToken> TokenGet(int tokenId, int userId);
		IFnsResult<IFnsSurveyToken> TokenPost(IFnsSurveyToken fnsToken, int userID);

		IFnsResult<List<IFnsSurveyPossibleAnswers>> PossibleAnswersGet(int userID);
		IFnsResult<IFnsSurveyPossibleAnswers> PossibleAnswersGet(int possibleAnswerID, int userID);
		IFnsResult<IFnsSurveyPossibleAnswers> PossibleAnswerPost(IFnsSurveyPossibleAnswers fnsPossibleAnswer, int userID);
		
		IFnsResult<List<IFnsSurveyTranslation>> SurveyTranslationsGet(int userID);
		IFnsResult<IFnsSurveyTranslation> SurveyTranslationsGet(int surveyTranslationId, int userID);
		IFnsResult<List<IFnsQuestionTranslation>> QuestionTranslationsGetBySurveyTranslationsId(int surveyTranslationId, int userID);
		IFnsResult<IFnsSurveyTranslation> SurveyTranslationPost(IFnsSurveyTranslation fnsSurveyTranslation, int userID);
        IFnsResult<List<IFnsSurveyTranslation>> SurveyTranslationsGetBySurveyId(int surveyId, int userID);

		IFnsResult<IFnsSurvey> SurveysGet(int surveyID, int userID);
		IFnsResult<List<IFnsSurvey>> SurveysGet(int userID);
		IFnsResult<List<IFnsSurvey>> SurveysGetBySurveyTypeId(int surveyTypeID, int userId);
		IFnsResult<IFnsSurvey> SurveyPost(IFnsSurvey fnsSurvey, int userId);
        IFnsResult<IFnsSurvey> CurrentSurvey(int surveyTypeID);
		IFnsResult<bool> PublishSurvey(int surveyID);

		IFnsResult<IFnsQuestionMeaning> QuestionMeaningPost(IFnsQuestionMeaning fnsQuestionMeaning, int userID);

		IFnsResult<List<IFnsQuestionTranslation>> QuestionTranslationsGet(int userID);
		IFnsResult<IFnsQuestionTranslation> QuestionTranslationsGet(int questionTranslationID, int userID);
		IFnsResult<IFnsQuestionTranslation> QuestionTranslationPost(IFnsQuestionTranslation fnsQuestionTranslation, int userID);

        IFnsResult<SurveyEngineContracts.IFnsResult> ResultSave(SurveyEngineContracts.IFnsResult input, string user);
        IFnsResult<List<SurveyEngineContracts.IFnsAnswer>> ResultAnswers(long resultID);
        IFnsResult<List<SurveyEngineContracts.IFnsResultView>> ResultViewsForAccount(long accountID);

		IFnsResult<IFnsQuestionMeaningTokenMap> QuestionMeaningTokenMapPost(IFnsQuestionMeaningTokenMap fnsQuestionMeaningTokenMap, int userID);
		IFnsResult<IFnsQuestionPossibleAnswerMap> QuestionPossibleAnswerMapPost(IFnsQuestionPossibleAnswerMap fnsQuestionPossibleAnswerMap, int userID);
		IFnsResult<bool> QuestionPossibleAnswerMapDelete(int questionId, int possibleAnswerId, int userID);

	    
	}
}