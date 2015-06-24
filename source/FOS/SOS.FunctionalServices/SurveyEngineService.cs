/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 10/30/13
 * Time: 15:54
 * 
 * Description:  Service that manages the Survey Engine.
 *********************************************************************************************************************/

using SOS.Data.SosCrm;
using SOS.Data.SosCrm.ControllerExtensions;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Helper;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.SurveyEngine;
using SOS.FunctionalServices.Helpers;
using SOS.FunctionalServices.Models;
using SOS.FunctionalServices.Models.SurveyEngine;
using SOS.Lib.Util;
using SSE.Data.SurveyEngine;
using SSE.Data.SurveyEngine.ControllerExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using SurveyEngineContracts = SOS.FunctionalServices.Contracts.Models.SurveyEngine;
using SurveyEngineModels = SOS.FunctionalServices.Models.SurveyEngine;

namespace SOS.FunctionalServices
{
	[ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.NotAllowed)]
	public class SurveyEngineService : ISurveyEngineService
	{
		#region Tokens

		public IFnsResult<List<IFnsSurveyToken>> TokensGet(int userId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "TokensGet";
			var result = new FnsResult<List<IFnsSurveyToken>>
				{
					Code = (int)ErrorCodes.GeneralMessage
					,
					Message = string.Format("Initializing '{0}'", METHOD_NAME)
				};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				SV_TokenCollection coll = SurveyEngineDataContext.Instance.SV_Tokens.LoadAll();

				// ** Init
				var resultList = coll.Select(svSurveyType => new FnsSurveyToken(svSurveyType)).ToList();
				result.Value = new List<IFnsSurveyToken>(resultList);

				// ** Set result values
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsSurveyToken>>
				{
					Code = (int)ErrorCodes.UnexpectedException
					,
					Message = string.Format("Exception thrown at '{1}': {0}", ex.Message, METHOD_NAME)
				};
			}

			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<IFnsSurveyToken> TokenGet(int tokenId, int userId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "TokenGet";
			var result = new FnsResult<IFnsSurveyToken>
			{
				Code = (int)ErrorCodes.GeneralMessage
				,
				Message = string.Format("Initializing '{0}'", METHOD_NAME)
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				SV_Token svToken = SurveyEngineDataContext.Instance.SV_Tokens.LoadByPrimaryKey(tokenId);

				if (svToken.IsLoaded)
				{
					// ** Init
					var resultList = new FnsSurveyToken(svToken);
					// ** Set result values
					result.Code = (int)ErrorCodes.Success;
					result.Message = "Success";
					result.Value = resultList;
				}
				else
				{
					result.Code = (int)ErrorCodes.GeneralWarning;
					result.Message = "Invalid id.  Object not found!";
				}
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsSurveyToken>
				{
					Code = (int)ErrorCodes.UnexpectedException
					,
					Message = string.Format("Exception thrown at '{1}': {0}", ex.Message, METHOD_NAME)
				};
			}

			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<IFnsSurveyToken> TokenPost(IFnsSurveyToken fnsToken, int userID)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "TokenGet";
			var result = new FnsResult<IFnsSurveyToken>
			{
				Code = (int)ErrorCodes.GeneralMessage
				,
				Message = string.Format("Initializing '{0}'", METHOD_NAME)
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				if (fnsToken.TokenID != 0)
				{
					SV_Token svToken = SurveyEngineDataContext.Instance.SV_Tokens.LoadByPrimaryKey(fnsToken.TokenID);

					if (svToken.IsLoaded)
					{
						// ** DataBind
						svToken.Token = fnsToken.Token;
						svToken.Save(userID);

						// ** Init
						var resultList = new FnsSurveyToken(svToken);
						// ** Set result values
						result.Code = (int)ErrorCodes.Success;
						result.Message = "Success";
						result.Value = resultList;
					}
					else
					{
						result.Code = (int)ErrorCodes.GeneralWarning;
						result.Message = "Invalid id.  Object not found!";
					}
				}
				else
				{
					var svNewToken = new SV_Token { Token = fnsToken.Token };
					svNewToken.Save(userID);
					// ** Init
					var resultList = new FnsSurveyToken(svNewToken);
					// ** Set result values
					result.Code = (int)ErrorCodes.Success;
					result.Message = "Success";
					result.Value = resultList;
				}
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsSurveyToken>
				{
					Code = (int)ErrorCodes.UnexpectedException
					,
					Message = string.Format("Exception thrown at '{1}': {0}", ex.Message, METHOD_NAME)
				};
			}

			#endregion CATCH

			// ** Return result
			return result;
		}

		#endregion Tokens

		#region Possible Answers

		public IFnsResult<List<IFnsSurveyPossibleAnswers>> PossibleAnswersGet(int userId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "PossibleAnswersGet";
			var result = new FnsResult<List<IFnsSurveyPossibleAnswers>>
				{
					Code = (int)ErrorCodes.GeneralMessage
					,
					Message = string.Format("Initializing '{0}'", METHOD_NAME)
				};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				SV_PossibleAnswerCollection coll = SurveyEngineDataContext.Instance.SV_PossibleAnswers.LoadAll();

				// ** Init
				var resultList = coll.Select(svPossibleAnswer => new FnsSurveyPossibleAnswers(svPossibleAnswer)).ToList();
				result.Value = new List<IFnsSurveyPossibleAnswers>(resultList);

				// ** Set result values
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsSurveyPossibleAnswers>>
				{
					Code = (int)ErrorCodes.UnexpectedException
					,
					Message = string.Format("Exception thrown at '{1}': {0}", ex.Message, METHOD_NAME)
				};
			}

			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<IFnsSurveyPossibleAnswers> PossibleAnswersGet(int possibleAnswerID, int userId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "PossibleAnswersGet";
			var result = new FnsResult<IFnsSurveyPossibleAnswers>
			{
				Code = (int)ErrorCodes.GeneralMessage
				,
				Message = string.Format("Initializing '{0}'", METHOD_NAME)
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				SV_PossibleAnswer svPossibleAnswer = SurveyEngineDataContext.Instance.SV_PossibleAnswers.LoadByPrimaryKey(possibleAnswerID);

				if (svPossibleAnswer != null && svPossibleAnswer.IsLoaded)
				{
					// ** Init
					var resultSv = new FnsSurveyPossibleAnswers(svPossibleAnswer);
					// ** Set result values
					result.Code = (int)ErrorCodes.Success;
					result.Message = "Success";
					result.Value = resultSv;
				}
				else
				{
					result.Code = (int)ErrorCodes.GeneralWarning;
					result.Message = "Invalid id.  Object not found!";
				}

			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsSurveyPossibleAnswers>
				{
					Code = (int)ErrorCodes.UnexpectedException
					,
					Message = string.Format("Exception thrown at '{1}': {0}", ex.Message, METHOD_NAME)
				};
			}

			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<IFnsSurveyPossibleAnswers> PossibleAnswerPost(IFnsSurveyPossibleAnswers fnsPossibleAnswer, int userID)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "PossibleAnswersPost";
			var result = new FnsResult<IFnsSurveyPossibleAnswers>
			{
				Code = (int)ErrorCodes.GeneralMessage
				,
				Message = string.Format("Initializing '{0}'", METHOD_NAME)
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				if (fnsPossibleAnswer.PossibleAnswersID != 0)
				{
					SV_PossibleAnswer svPossibleAnswer = SurveyEngineDataContext.Instance.SV_PossibleAnswers.LoadByPrimaryKey(fnsPossibleAnswer.PossibleAnswersID);

					if (svPossibleAnswer != null && svPossibleAnswer.IsLoaded)
					{
						// ** Databind
						svPossibleAnswer.AnswerText = fnsPossibleAnswer.AnswerText;

						// ** Save
						svPossibleAnswer.Save(userID);

						// ** Init
						var resultSv = new FnsSurveyPossibleAnswers(svPossibleAnswer);
						// ** Set result values
						result.Code = (int)ErrorCodes.Success;
						result.Message = "Success";
						result.Value = resultSv;
					}
					else
					{
						result.Code = (int)ErrorCodes.GeneralWarning;
						result.Message = "Invalid id.  Object not found!";
					}
				}
				else
				{
					// ** Databind
					var newItem = new SV_PossibleAnswer { AnswerText = fnsPossibleAnswer.AnswerText };

					// ** Save
					newItem.Save(userID);

					// ** Set result
					result.Code = (int)ErrorCodes.Success;
					result.Message = "Success";
					result.Value = new FnsSurveyPossibleAnswers(newItem);
				}

			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsSurveyPossibleAnswers>
				{
					Code = (int)ErrorCodes.UnexpectedException
					,
					Message = string.Format("Exception thrown at '{1}': {0}", ex.Message, METHOD_NAME)
				};
			}

			#endregion CATCH

			// ** Return result
			return result;
		}

		#endregion Possible Answers

		#region SurveyTranslations

		public IFnsResult<List<IFnsSurveyTranslation>> SurveyTranslationsGet(int userID)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "SurveyTranslationsGet";
			var result = new FnsResult<List<IFnsSurveyTranslation>>
				{
					Code = (int)ErrorCodes.GeneralMessage
					,
					Message = string.Format("Initializing '{0}'", METHOD_NAME)
				};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				SV_SurveyTranslationCollection coll = SurveyEngineDataContext.Instance.SV_SurveyTranslations.LoadAll();

				// ** Init
				var resultList = coll.Select(svSurveyTranslation => new FnsSurveyTranslation(svSurveyTranslation)).ToList();
				result.Value = new List<IFnsSurveyTranslation>(resultList);

				// ** Set result values
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsSurveyTranslation>>
				{
					Code = (int)ErrorCodes.UnexpectedException
					,
					Message = string.Format("Exception thrown at '{1}': {0}", ex.Message, METHOD_NAME)
				};
			}

			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<IFnsSurveyTranslation> SurveyTranslationsGet(int surveyTranslationId, int userID)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "SurveyTranslationsGet by surveyTranslationId";
			var result = new FnsResult<IFnsSurveyTranslation>
				{
					Code = (int)ErrorCodes.GeneralMessage
					,
					Message = string.Format("Initializing '{0}'", METHOD_NAME)
				};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				SV_SurveyTranslation item = SurveyEngineDataContext.Instance.SV_SurveyTranslations.LoadByPrimaryKey(surveyTranslationId);

				if (item != null && item.IsLoaded)
				{
					// ** Init
					var resultValue = new FnsSurveyTranslation(item);
					result.Value = resultValue;
				}

				// ** Set result values
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsSurveyTranslation>
				{
					Code = (int)ErrorCodes.UnexpectedException
					,
					Message = string.Format("Exception thrown at '{1}': {0}", ex.Message, METHOD_NAME)
				};
			}

			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<List<IFnsQuestionTranslation>> QuestionTranslationsGetBySurveyTranslationsId(int surveyTranslationId, int userID)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "QuestionTranslationsGetBySurveyTranslationsId";
			var result = new FnsResult<List<IFnsQuestionTranslation>>
				{
					Code = (int)ErrorCodes.GeneralMessage
					,
					Message = string.Format("Initializing '{0}'", METHOD_NAME)
				};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				SV_QuestionTranslationCollection coll = SurveyEngineDataContext.Instance.SV_QuestionTranslations.GetBySurveyTranslationsId(surveyTranslationId);

				// ** Init
				var resultList = coll.Select(svQuestionTranslation => new FnsQuestionTranslation(svQuestionTranslation)).ToList();
				result.Value = new List<IFnsQuestionTranslation>(resultList);

				// ** Set result values
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsQuestionTranslation>>
				{
					Code = (int)ErrorCodes.UnexpectedException
					,
					Message = string.Format("Exception thrown at '{1}': {0}", ex.Message, METHOD_NAME)
				};
			}

			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<IFnsSurveyTranslation> SurveyTranslationPost(IFnsSurveyTranslation fnsSurveyTranslation, int userID)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "SurveyTranslationsGet by surveyTranslationId";
			var result = new FnsResult<IFnsSurveyTranslation>
				{
					Code = (int)ErrorCodes.GeneralMessage
					,
					Message = string.Format("Initializing '{0}'", METHOD_NAME)
				};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Check for create or update
				if (fnsSurveyTranslation.SurveyTranslationID != 0)
				{
					SV_SurveyTranslation item = SurveyEngineDataContext.Instance.SV_SurveyTranslations.LoadByPrimaryKey(fnsSurveyTranslation.SurveyTranslationID);

					if (item != null && item.IsLoaded)
					{
						// ** Databind
						item.SurveyId = fnsSurveyTranslation.SurveyId;
						item.LocalizationCode = fnsSurveyTranslation.LocalizationCode;

						// ** Save
						item.Save(userID);

						// ** Init
						var resultValue = new FnsSurveyTranslation(item);
						result.Value = resultValue;
					}
					else
					{
						throw new Exception(string.Format("The id '{0}' passed is invalid", fnsSurveyTranslation.SurveyTranslationID));
					}

					// ** Set result values
					result.Code = (int)ErrorCodes.Success;
					result.Message = "Success";

				}
				else
				{
					// ** Databind
					var newItem = new SV_SurveyTranslation
					{
						SurveyId = fnsSurveyTranslation.SurveyId,
						LocalizationCode = fnsSurveyTranslation.LocalizationCode
					};
					// ** Save
					newItem.Save(userID);

					// Return results
					result.Code = (int)ErrorCodes.Success;
					result.Message = "Success";
					result.Value = new FnsSurveyTranslation(newItem);
				}
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsSurveyTranslation>
				{
					Code = (int)ErrorCodes.UnexpectedException
					,
					Message = string.Format("Exception thrown at '{1}': {0}", ex.Message, METHOD_NAME)
				};
			}

			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<List<IFnsSurveyTranslation>> SurveyTranslationsGetBySurveyId(int surveyId, int userID)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "SurveyTranslationsGetBySurveyId";
			var result = new FnsResult<List<IFnsSurveyTranslation>>
				{
					Code = (int)ErrorCodes.GeneralMessage
					,
					Message = string.Format("Initializing '{0}'", METHOD_NAME)
				};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				SV_SurveyTranslationCollection coll = SurveyEngineDataContext.Instance.SV_SurveyTranslations.GetBySurveyId(surveyId);

				// ** Init
				var resultList = coll.Select(svSurveyTranslation => new FnsSurveyTranslation(svSurveyTranslation)).ToList();
				result.Value = new List<IFnsSurveyTranslation>(resultList);

				// ** Set result values
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsSurveyTranslation>>
				{
					Code = (int)ErrorCodes.UnexpectedException
					,
					Message = string.Format("Exception thrown at '{1}': {0}", ex.Message, METHOD_NAME)
				};
			}

			#endregion CATCH

			// ** Return result
			return result;
		}

		#endregion SurveyTranslations

		#region SurveyTypes

		public IFnsResult<List<IFnsSurveyType>> SurveyTypesGet(int userId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "SurveyTypesGet";
			var result = new FnsResult<List<IFnsSurveyType>>
				{
					Code = (int)ErrorCodes.GeneralMessage
					,
					Message = string.Format("Initializing '{0}'", METHOD_NAME)
				};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				SV_SurveyTypeCollection coll = SurveyEngineDataContext.Instance.SV_SurveyTypes.LoadAll();

				// ** Init
				var resultList = coll.Select(svSurveyType => new FnsSurveyType(svSurveyType)).ToList();
				result.Value = new List<IFnsSurveyType>(resultList);

				// ** Set result values
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsSurveyType>>
				{
					Code = (int)ErrorCodes.UnexpectedException
					,
					Message = string.Format("Exception thrown at '{1}': {0}", ex.Message, METHOD_NAME)
				};
			}

			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<IFnsSurveyType> SurveyTypesGet(int surveyTypeID, int userId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "SurveyTypesGet by surveyTypeID";
			var result = new FnsResult<IFnsSurveyType>
				{
					Code = (int)ErrorCodes.GeneralMessage
					,
					Message = string.Format("Initializing '{0}'", METHOD_NAME)
				};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				SV_SurveyType item = SurveyEngineDataContext.Instance.SV_SurveyTypes.LoadByPrimaryKey(surveyTypeID);

				if (item != null && item.IsLoaded)
				{
					// ** Init
					var resultValue = new FnsSurveyType(item);
					result.Value = resultValue;
				}

				// ** Set result values
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsSurveyType>
				{
					Code = (int)ErrorCodes.UnexpectedException
					,
					Message = string.Format("Exception thrown at '{1}': {0}", ex.Message, METHOD_NAME)
				};
			}

			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<IFnsSurveyType> SurveyTypePost(IFnsSurveyType fnsSurveyType, int userID)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "SurveyTypePost";
			var result = new FnsResult<IFnsSurveyType>
				{
					Code = (int)ErrorCodes.GeneralMessage
					,
					Message = string.Format("Initializing '{0}'", METHOD_NAME)
				};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Check if this is a Create or Update
				if (fnsSurveyType.SurveyTypeID == 0)
				{
					var newSurveyType = new SV_SurveyType
					{
						Name = fnsSurveyType.Name
					};
					newSurveyType.Save(userID);
					result.Value = new FnsSurveyType(newSurveyType);

				}
				else
				{
					SV_SurveyType item = SurveyEngineDataContext.Instance.SV_SurveyTypes.LoadByPrimaryKey(fnsSurveyType.SurveyTypeID);

					if (item != null && item.IsLoaded)
					{
						// ** Bind new data.
						item.Name = fnsSurveyType.Name;
						item.Save(userID);

						// ** Init
						var resultValue = new FnsSurveyType(item);
						result.Value = resultValue;
					}
				}


				// ** Set result values
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsSurveyType>
				{
					Code = (int)ErrorCodes.UnexpectedException
					,
					Message = string.Format("Exception thrown at '{1}': {0}", ex.Message, METHOD_NAME)
				};
			}

			#endregion CATCH

			// ** Return result
			return result;

		}

		public IFnsResult<IFnsSurveyType> SurveyTypeGetBySurveyId(int surveyId, int userID)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "SurveyTypeGetBySurveyId";
			var result = new FnsResult<IFnsSurveyType>
				{
					Code = (int)ErrorCodes.GeneralMessage
					,
					Message = string.Format("Initializing '{0}'", METHOD_NAME)
				};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				SV_Survey survey = SurveyEngineDataContext.Instance.SV_Surveys.LoadByPrimaryKey(surveyId);
				if (survey != null && survey.IsLoaded)
				{
					// ** Set result values
					result.Code = (int)ErrorCodes.Success;
					result.Message = "Success";
					result.Value = new FnsSurveyType(survey.SurveyType);
				}
				else
				{
					result.Code = (int)ErrorCodes.GeneralError;
					result.Message = string.Format("The SurveyID passed ('{0}') did not find a survey.", surveyId);
				}

			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsSurveyType>
				{
					Code = (int)ErrorCodes.UnexpectedException
					,
					Message = string.Format("Exception thrown at '{1}': {0}", ex.Message, METHOD_NAME)
				};
			}

			#endregion CATCH

			// ** Return result
			return result;
		}

		#endregion SurveyTypes

		#region Surveys

		public IFnsResult<IFnsSurvey> SurveysGet(int surveyID, int userID)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "SurveysGet";
			var result = new FnsResult<IFnsSurvey>
			{
				Code = (int)ErrorCodes.GeneralMessage
				,
				Message = string.Format("Initializing '{0}'", METHOD_NAME)
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				SV_Survey item = SurveyEngineDataContext.Instance.SV_Surveys.LoadByPrimaryKey(surveyID);

				if (item != null && item.IsLoaded)
				{
					// ** Init
					var resultValue = new FnsSurvey(item);
					result.Value = resultValue;
				}

				// ** Set result values
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsSurvey>
				{
					Code = (int)ErrorCodes.UnexpectedException
					,
					Message = string.Format("Exception thrown at '{1}': {0}", ex.Message, METHOD_NAME)
				};
			}

			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<List<IFnsSurvey>> SurveysGet(int userID)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "SurveysGet";
			var result = new FnsResult<List<IFnsSurvey>>
			{
				Code = (int)ErrorCodes.GeneralMessage
				,
				Message = string.Format("Initializing '{0}'", METHOD_NAME)
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				SV_SurveyCollection coll = SurveyEngineDataContext.Instance.SV_Surveys.LoadAll();

				// ** Init
				var resultList = coll.Select(svSurveyTranslation => new FnsSurvey(svSurveyTranslation)).ToList();
				result.Value = new List<IFnsSurvey>(resultList);

				// ** Set result values
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsSurvey>>
				{
					Code = (int)ErrorCodes.UnexpectedException
					,
					Message = string.Format("Exception thrown at '{1}': {0}", ex.Message, METHOD_NAME)
				};
			}

			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<List<IFnsSurvey>> SurveysGetBySurveyTypeId(int surveyTypeID, int userId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "SurveysGetBySurveyTypeId";
			var result = new FnsResult<List<IFnsSurvey>>
				{
					Code = (int)ErrorCodes.GeneralMessage
					,
					Message = string.Format("Initializing '{0}'", METHOD_NAME)
				};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				SV_SurveyCollection coll = SurveyEngineDataContext.Instance.SV_Surveys.GetBySurveyTypeId(surveyTypeID);

				// ** Init
				var resultList = coll.Select(svSurveyType => new FnsSurvey(svSurveyType)).ToList();
				result.Value = new List<IFnsSurvey>(resultList);

				// ** Set result values
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsSurvey>>
				{
					Code = (int)ErrorCodes.UnexpectedException
					,
					Message = string.Format("Exception thrown at '{1}': {0}", ex.Message, METHOD_NAME)
				};
			}

			#endregion CATCH
			// ** Return result
			return result;
		}

		public IFnsResult<IFnsSurvey> SurveyPost(IFnsSurvey fnsSurvey, int userId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "SurveyPost";
			var result = new FnsResult<IFnsSurvey>
				{
					Code = (int)ErrorCodes.GeneralMessage
					,
					Message = string.Format("Initializing '{0}'", METHOD_NAME)
				};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Check if this is a Create or Update
				if (fnsSurvey.SurveyID == 0)
				{
					var newSurvey = new SV_Survey
					{
						SurveyTypeId = fnsSurvey.SurveyTypeId,
						Version = fnsSurvey.Version,
						IsCurrent = false,
						IsReadonly = false,
					};
					newSurvey.Save(userId);
					result.Value = new FnsSurvey(newSurvey);
				}
				else
				{
					SV_Survey item = SurveyEngineDataContext.Instance.SV_Surveys.LoadByPrimaryKey(fnsSurvey.SurveyID);
					if (item != null && item.IsLoaded)
					{
						// ** Bind new data.
						item.SurveyTypeId = fnsSurvey.SurveyTypeId;
						item.Version = fnsSurvey.Version;
						//item.IsCurrent = fnsSurvey.IsCurrent;
						//item.IsReadonly = fnsSurvey.IsReadonly;
						item.Save(userId);

						// ** Init
						var resultValue = new FnsSurvey(item);
						result.Value = resultValue;
					}
				}


				// ** Set result values
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsSurvey>
				{
					Code = (int)ErrorCodes.UnexpectedException
					,
					Message = string.Format("Exception thrown at '{1}': {0}", ex.Message, METHOD_NAME)
				};
			}

			#endregion CATCH

			// ** Return result
			return result;

		}

		public IFnsResult<IFnsSurvey> CurrentSurvey(int surveyTypeID)
		{
			var result = new FnsResult<IFnsSurvey> { Code = (int)ErrorCodes.Success, Message = "Success" };
			try
			{
				var list = SurveyEngineDataContext.Instance.SV_Surveys.Current(surveyTypeID);
				if (list.Count > 0)
				{
					result.Value = SurveyEngineModels.FnsSurvey.ConvertFrom(list[0]);
				}
				else
				{
					result.Code = (int)ErrorCodes.GeneralError;
					result.Message = string.Format("No current survey for survey type {0}", surveyTypeID);
				}
			}
			catch (Exception ex)
			{
				result.Code = (int)ErrorCodes.UnexpectedException;
				result.Message = string.Format("Exception thrown: {0}", ex.Message);
			}
			return result;
		}
		public IFnsResult<bool> PublishSurvey(int surveyID)
		{
			var result = new FnsResult<bool> { Code = (int)ErrorCodes.Success, Message = "Success" };
			try
			{
				SV_Survey survey = SurveyEngineDataContext.Instance.SV_Surveys.LoadByPrimaryKey(surveyID);
				if (survey == null)
				{
					result.Code = -1;
					result.Message = string.Format("No survey {0}", surveyID);
				}
				//else if (survey.IsReadonly)
				//{
				//	//@REVIEW: should this be an error??
				//	result.Code = -1;
				//	result.Message = string.Format("Survey {0} cannot be republished", surveyID);
				//}
				else if (survey.IsCurrent)
				{
					// warning
					result.Message = string.Format("Survey {0} is already the current survey", surveyID);
				}
				else
				{
					DatabaseHelper.UseTransaction(SOS.Data.SubSonicConfigHelper.SSE_SURVEY_ENGINE_PROVIDER_NAME, () =>
					{
						// mark current surveys as not current (there should only be one)
						var list = SurveyEngineDataContext.Instance.SV_Surveys.Current(survey.SurveyTypeId);
						foreach (var item in list)
						{
							item.IsCurrent = false;
							item.Save();
						}

						// mark this one as current
						survey.IsCurrent = true;
						survey.IsReadonly = true;
						survey.Save();

						// commit transaction
						return true;
					});

					result.Value = true;
				}
			}
			catch (Exception ex)
			{
				result.Code = (int)ErrorCodes.UnexpectedException;
				result.Message = string.Format("Exception thrown: {0}", ex.Message);
			}
			return result;
		}

		#endregion Surveys

		#region Question

		public IFnsResult<List<IFnsQuestion>> QuestionGet(int id)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "QuestionGet";
			var result = new FnsResult<List<IFnsQuestion>>
				{
					Code = (int)ErrorCodes.GeneralMessage
					,
					Message = string.Format("Initializing '{0}'", METHOD_NAME)
				};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				SV_QuestionCollection coll = SurveyEngineDataContext.Instance.SV_Questions.LoadAll();

				// ** Init
				var resultList = coll.Select(svSurveyType => new FnsQuestion(svSurveyType)).ToList();
				result.Value = new List<IFnsQuestion>(resultList);

				// ** Set result values
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsQuestion>>
				{
					Code = (int)ErrorCodes.UnexpectedException
					,
					Message = string.Format("Exception thrown at '{1}': {0}", ex.Message, METHOD_NAME)
				};
			}

			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<IFnsQuestion> QuestionGet(int questionId, int userID)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "QuestionGet by questionId";
			var result = new FnsResult<IFnsQuestion>
				{
					Code = (int)ErrorCodes.GeneralMessage
					,
					Message = string.Format("Initializing '{0}'", METHOD_NAME)
				};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				SV_Question item = SurveyEngineDataContext.Instance.SV_Questions.LoadByPrimaryKey(questionId);

				if (item != null && item.IsLoaded)
				{
					// ** Init
					var resultValue = new FnsQuestion(item);
					result.Value = resultValue;
				}

				// ** Set result values
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsQuestion>
				{
					Code = (int)ErrorCodes.UnexpectedException
					,
					Message = string.Format("Exception thrown at '{1}': {0}", ex.Message, METHOD_NAME)
				};
			}

			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<IFnsQuestion> QuestionPost(IFnsQuestion fnsQuestion, int userId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "QuestionPost";
			var result = new FnsResult<IFnsQuestion>
				{
					Code = (int)ErrorCodes.GeneralMessage
					,
					Message = string.Format("Initializing '{0}'", METHOD_NAME)
				};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Check if this is a Create or Update
				if (fnsQuestion.QuestionID == 0)
				{
					var newQuestion = new SV_Question
					{
						SurveyId = fnsQuestion.SurveyId,
						QuestionMeaningId = fnsQuestion.QuestionMeaningId,
						ParentId = fnsQuestion.ParentId,
						GroupOrder = fnsQuestion.GroupOrder,
						MapToTokenId = fnsQuestion.MapToTokenId,
						ConditionJson = fnsQuestion.ConditionJson,
					};
					newQuestion.Save(userId);
					result.Value = new FnsQuestion(newQuestion);
				}
				else
				{
					SV_Question item = SurveyEngineDataContext.Instance.SV_Questions.LoadByPrimaryKey(fnsQuestion.QuestionID);

					if (item != null && item.IsLoaded)
					{
						// ** Bind new data.
						item.SurveyId = fnsQuestion.SurveyId;
						item.QuestionMeaningId = fnsQuestion.QuestionMeaningId;
						item.ParentId = fnsQuestion.ParentId;
						item.GroupOrder = fnsQuestion.GroupOrder;
						item.MapToTokenId = fnsQuestion.MapToTokenId;
						item.ConditionJson = fnsQuestion.ConditionJson;

						// ** Init
						item.Save(userId);
						var resultValue = new FnsQuestion(item);
						result.Value = resultValue;
					}
				}


				// ** Set result values
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsQuestion>
				{
					Code = (int)ErrorCodes.UnexpectedException
					,
					Message = string.Format("Exception thrown at '{1}': {0}", ex.Message, METHOD_NAME)
				};
			}

			#endregion CATCH

			// ** Return result
			return result;

		}

		public IFnsResult<List<IFnsQuestion>> QuestionsGetBySurveyId(int surveyId, int userID)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "QuestionsGetBySurveyId";
			var result = new FnsResult<List<IFnsQuestion>>
				{
					Code = (int)ErrorCodes.GeneralMessage
					,
					Message = string.Format("Initializing '{0}'", METHOD_NAME)
				};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				SV_QuestionCollection coll = SurveyEngineDataContext.Instance.SV_Questions.GetBySurveyId(surveyId);

				// ** Init
				var resultList = coll.Select(svSurveyType => new FnsQuestion(svSurveyType)).ToList();
				result.Value = new List<IFnsQuestion>(resultList);

				// ** Set result values
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsQuestion>>
				{
					Code = (int)ErrorCodes.UnexpectedException
					,
					Message = string.Format("Exception thrown at '{1}': {0}", ex.Message, METHOD_NAME)
				};
			}

			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<bool> SwapQuestionGroupOrder(int questionId, int groupOrder, int sibQuestionId, int sibGroupOrder, int userID)
		{
			var result = new FnsResult<bool>();

			var instance = SurveyEngineDataContext.Instance;

			var question = instance.SV_Questions.LoadByPrimaryKey(questionId);
			if (question == null)
			{
				result.Code = (int)ErrorCodes.SqlItemNotFound;
				result.Message = "No question with id " + questionId;
				return result;
			}
			if (question.GroupOrder != groupOrder)
			{
				result.Code = -1;
				result.Message = string.Format("Expected Question {0} GroupOrder to be {1} not {2}", questionId, question.GroupOrder, groupOrder);
				return result;
			}

			var sib = instance.SV_Questions.LoadByPrimaryKey(sibQuestionId);
			if (sib == null)
			{
				result.Code = (int)ErrorCodes.SqlItemNotFound;
				result.Message = "No question with id " + sibQuestionId;
				return result;
			}
			if (sib.GroupOrder != sibGroupOrder)
			{
				result.Code = -1;
				result.Message = string.Format("Expected Question {0} GroupOrder to be {1} not {2}", sibQuestionId, sib.GroupOrder, sibGroupOrder);
				return result;
			}

			DatabaseHelper.UseTransaction(SOS.Data.SubSonicConfigHelper.SSE_SURVEY_ENGINE_PROVIDER_NAME, () =>
			{
				question.GroupOrder = sibGroupOrder;
				question.Save(userID);

				sib.GroupOrder = groupOrder;
				sib.Save(userID);
	
				return true;
			});

			return result;
		}

		public IFnsResult<List<IFnsQuestionPossibleAnswerMap>> QuestionPossibleAnswerMapsGetByQuestionId(int questionId, int userID)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "QuestionPossibleAnswerMapsGetByQuestionId";
			var result = new FnsResult<List<IFnsQuestionPossibleAnswerMap>>
				{
					Code = (int)ErrorCodes.GeneralMessage
					,
					Message = string.Format("Initializing '{0}'", METHOD_NAME)
				};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				var coll = SurveyEngineDataContext.Instance.SV_Questions_PossibleAnswers_Maps.GetByQuestionId(questionId);

				// ** Init
				var resultList = coll.Select(svSurveyType => new FnsQuestionPossibleAnswerMap(svSurveyType)).ToList();
				result.Value = new List<IFnsQuestionPossibleAnswerMap>(resultList);

				// ** Set result values
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsQuestionPossibleAnswerMap>>
				{
					Code = (int)ErrorCodes.UnexpectedException
					,
					Message = string.Format("Exception thrown at '{1}': {0}", ex.Message, METHOD_NAME)
				};
			}

			#endregion CATCH

			// ** Return result
			return result;
		}
		#endregion Question

		#region Question Meaning

		public IFnsResult<List<IFnsQuestionMeaning>> QuestionMeaningGet(int userID)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "QuestionMeaningsGetBySurveyTypeId";
			var result = new FnsResult<List<IFnsQuestionMeaning>>
			{
				Code = (int)ErrorCodes.GeneralMessage
				,
				Message = string.Format("Initializing '{0}'", METHOD_NAME)
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				SV_QuestionMeaningCollection coll = SurveyEngineDataContext.Instance.SV_QuestionMeanings.LoadAll();

				// ** Init
				var resultList = coll.Select(svSurveyType => new FnsQuestionMeaning(svSurveyType)).ToList();
				result.Value = new List<IFnsQuestionMeaning>(resultList);

				// ** Set result values
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsQuestionMeaning>>
				{
					Code = (int)ErrorCodes.UnexpectedException
					,
					Message = string.Format("Exception thrown at '{1}': {0}", ex.Message, METHOD_NAME)
				};
			}

			#endregion CATCH
			// ** Return result
			return result;
		}

		public IFnsResult<IFnsQuestionMeaning> QuestionMeaningGet(int questionMeaningID, int userID)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "QuestionMeaningGet by id";
			var result = new FnsResult<IFnsQuestionMeaning>
			{
				Code = (int)ErrorCodes.GeneralMessage
				,
				Message = string.Format("Initializing '{0}'", METHOD_NAME)
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				SV_QuestionMeaning item = SurveyEngineDataContext.Instance.SV_QuestionMeanings.LoadByPrimaryKey(questionMeaningID);

				if (item != null && item.IsLoaded)
				{
					// ** Init
					var resultList = new FnsQuestionMeaning(item);
					result.Value = resultList;
				}

				// ** Set result values
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsQuestionMeaning>
				{
					Code = (int)ErrorCodes.UnexpectedException
					,
					Message = string.Format("Exception thrown at '{1}': {0}", ex.Message, METHOD_NAME)
				};
			}

			#endregion CATCH
			// ** Return result
			return result;
		}

		public IFnsResult<List<IFnsQuestionMeaning>> QuestionMeaningsGetBySurveyTypeId(int surveyTypeId, int userID)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "QuestionMeaningsGetBySurveyTypeId";
			var result = new FnsResult<List<IFnsQuestionMeaning>>
				{
					Code = (int)ErrorCodes.GeneralMessage
					,
					Message = string.Format("Initializing '{0}'", METHOD_NAME)
				};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				SV_QuestionMeaningCollection coll = SurveyEngineDataContext.Instance.SV_QuestionMeanings.GetBySurveyTypeId(surveyTypeId);

				// ** Init
				var resultList = coll.Select(svSurveyType => new FnsQuestionMeaning(svSurveyType)).ToList();
				result.Value = new List<IFnsQuestionMeaning>(resultList);

				// ** Set result values
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsQuestionMeaning>>
				{
					Code = (int)ErrorCodes.UnexpectedException
					,
					Message = string.Format("Exception thrown at '{1}': {0}", ex.Message, METHOD_NAME)
				};
			}

			#endregion CATCH
			// ** Return result
			return result;
		}

		public IFnsResult<IFnsQuestionMeaning> QuestionMeaningPost(IFnsQuestionMeaning fnsQuestionMeaning, int userID)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "QuestionMeaning Post";
			var result = new FnsResult<IFnsQuestionMeaning>
			{
				Code = (int)ErrorCodes.GeneralMessage
				,
				Message = string.Format("Initializing '{0}'", METHOD_NAME)
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				if (fnsQuestionMeaning.QuestionMeaningID != 0)
				{
					SV_QuestionMeaning item = SurveyEngineDataContext.Instance.SV_QuestionMeanings.LoadByPrimaryKey(fnsQuestionMeaning.QuestionMeaningID);

					if (item != null && item.IsLoaded)
					{
						item.SurveyTypeId = fnsQuestionMeaning.SurveyTypeId;
						item.Name = fnsQuestionMeaning.Name;
						item.Save(userID);

						// ** Init
						var resultList = new FnsQuestionMeaning(item);
						result.Value = resultList;
					}
					else throw new Exception(string.Format("Question Meaning with ID of '{0}' was not found.", fnsQuestionMeaning.QuestionMeaningID));
				}
				else
				{
					var newItem = new SV_QuestionMeaning
					{
						SurveyTypeId = fnsQuestionMeaning.SurveyTypeId,
						Name = fnsQuestionMeaning.Name
					};
					newItem.Save(userID);

					// ** Init
					var resultList = new FnsQuestionMeaning(newItem);
					result.Value = resultList;
				}

				// ** Set result values
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsQuestionMeaning>
				{
					Code = (int)ErrorCodes.UnexpectedException
					,
					Message = string.Format("Exception thrown at '{1}': {0}", ex.Message, METHOD_NAME)
				};
			}

			#endregion CATCH
			// ** Return result
			return result;
		}

		#endregion Question Meaning

		#region Question Meaning Token

		public IFnsResult<List<IFnsQuestionMeaningTokenMap>> QuestionMeaningTokenMapsGetByQuestionMeaningId(int questionMeaningId, int userID)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "QuestionMeaningTokenMapsGetByQuestionMeaningId";
			var result = new FnsResult<List<IFnsQuestionMeaningTokenMap>>
				{
					Code = (int)ErrorCodes.GeneralMessage
					,
					Message = string.Format("Initializing '{0}'", METHOD_NAME)
				};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				SV_QuestionMeanings_Tokens_MapCollection coll = SurveyEngineDataContext.Instance.SV_QuestionMeanings_Tokens_Maps.GetByQuestionMeaningId(questionMeaningId);

				// ** Init
				var resultList = coll.Select(svQuestionMeaningsTokensMapCollection => new FnsQuestionMeaningTokenMap(svQuestionMeaningsTokensMapCollection)).ToList();
				result.Value = new List<IFnsQuestionMeaningTokenMap>(resultList);

				// ** Set result values
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsQuestionMeaningTokenMap>>
				{
					Code = (int)ErrorCodes.UnexpectedException
					,
					Message = string.Format("Exception thrown at '{1}': {0}", ex.Message, METHOD_NAME)
				};
			}

			#endregion CATCH
			// ** Return result
			return result;
		}

		#endregion Question Meaning Token

		#region Question Translations

		public IFnsResult<List<IFnsQuestionTranslation>> QuestionTranslationsGet(int userID)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "QuestionTranslationsGet";
			var result = new FnsResult<List<IFnsQuestionTranslation>>
			{
				Code = (int)ErrorCodes.GeneralMessage
				,
				Message = string.Format("Initializing '{0}'", METHOD_NAME)
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				SV_QuestionTranslationCollection coll = SurveyEngineDataContext.Instance.SV_QuestionTranslations.LoadAll();

				// ** Init
				var resultList = coll.Select(svSurveyTranslation => new FnsQuestionTranslation(svSurveyTranslation)).ToList();
				result.Value = new List<IFnsQuestionTranslation>(resultList);

				// ** Set result values
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsQuestionTranslation>>
				{
					Code = (int)ErrorCodes.UnexpectedException
					,
					Message = string.Format("Exception thrown at '{1}': {0}", ex.Message, METHOD_NAME)
				};
			}

			#endregion CATCH

			// ** Return result
			return result;
		}
		public IFnsResult<IFnsQuestionTranslation> QuestionTranslationsGet(int questionTranslationID, int userID)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "QuestionTranslationsGet";
			var result = new FnsResult<IFnsQuestionTranslation>
			{
				Code = (int)ErrorCodes.GeneralMessage
				,
				Message = string.Format("Initializing '{0}'", METHOD_NAME)
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				SV_QuestionTranslation item = SurveyEngineDataContext.Instance.SV_QuestionTranslations.LoadByPrimaryKey(questionTranslationID);

				if (item != null && item.IsLoaded)
				{
					// ** Init
					var resultValue = new FnsQuestionTranslation(item);
					result.Value = resultValue;
				}

				// ** Set result values
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsQuestionTranslation>
				{
					Code = (int)ErrorCodes.UnexpectedException
					,
					Message = string.Format("Exception thrown at '{1}': {0}", ex.Message, METHOD_NAME)
				};
			}

			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<IFnsQuestionTranslation> QuestionTranslationPost(IFnsQuestionTranslation fnsQuestionTranslation, int userId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "QuestionTranslationPost";
			var result = new FnsResult<IFnsQuestionTranslation>
			{
				Code = (int)ErrorCodes.GeneralMessage
				,
				Message = string.Format("Initializing '{0}'", METHOD_NAME)
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Check if this is a Create or Update
				if (fnsQuestionTranslation.QuestionTranslationID == 0)
				{
					var newSurvey = new SV_QuestionTranslation
					{
						SurveyTranslationId = fnsQuestionTranslation.SurveyTranslationId,
						QuestionId = fnsQuestionTranslation.QuestionId,
						TextFormat = fnsQuestionTranslation.TextFormat
					};
					newSurvey.Save(userId);
					result.Value = new FnsQuestionTranslation(newSurvey);

				}
				else
				{
					SV_QuestionTranslation item = SurveyEngineDataContext.Instance.SV_QuestionTranslations.LoadByPrimaryKey(fnsQuestionTranslation.QuestionTranslationID);

					if (item != null && item.IsLoaded)
					{
						// ** Bind new data.
						item.QuestionTranslationID = fnsQuestionTranslation.QuestionTranslationID;
						item.SurveyTranslationId = fnsQuestionTranslation.SurveyTranslationId;
						item.QuestionId = fnsQuestionTranslation.QuestionId;
						item.TextFormat = fnsQuestionTranslation.TextFormat;
						item.Save(userId);

						// ** Init
						var resultValue = new FnsQuestionTranslation(item);
						result.Value = resultValue;
					}
				}

				// ** Set result values
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsQuestionTranslation>
				{
					Code = (int)ErrorCodes.UnexpectedException
					,
					Message = string.Format("Exception thrown at '{1}': {0}", ex.Message, METHOD_NAME)
				};
			}

			#endregion CATCH

			// ** Return result
			return result;
		}

		#endregion Question Translations

		#region Results

		public IFnsResult<SurveyEngineContracts.IFnsResult> ResultSave(SurveyEngineContracts.IFnsResult input, string user)
		{
			var result = new FnsResult<SurveyEngineContracts.IFnsResult> { Code = (int)ErrorCodes.Success, Message = "Success" };

			try
			{
				DatabaseHelper.UseTransaction(Data.SubSonicConfigHelper.SSE_SURVEY_ENGINE_PROVIDER_NAME, () =>
				{
					// save MapToTokenAnswers (uses a transaction)
					var resultMsg = SaveMapToTokenAnswers(input.MapToTokenAnswers, input.AccountId, user);
					if (resultMsg != string.Empty)
					{
						//
						result.Code = -1;
						result.Message = resultMsg;
						// rollback transaction
						return false;
					}

					// save result
					var itemResult = SurveyEngineModels.FnsResult.ConvertToDb(input);
					itemResult.CreatedBy = user;
					itemResult.CreatedOn = DateTime.UtcNow;
					itemResult.Save(user);
					// store result
					result.Value = SurveyEngineModels.FnsResult.ConvertFrom(itemResult);

					// save each answer
					foreach (var answer in input.Answers)
					{
						var item = SurveyEngineModels.FnsAnswer.ConvertToDb(answer);
						// set fk
						item.ResultId = result.Value.ResultID;
						item.Save(user);
						// store result
						result.Value.Answers.Add(SurveyEngineModels.FnsAnswer.ConvertFrom(item));
					}

					return true;
				});
				var itemResult1 = SurveyEngineModels.FnsResult.ConvertToDb(input);

				var surveyTypeName = itemResult1.SurveyTranslation.Survey.SurveyType.Name;
				switch (surveyTypeName)
				{
					case "Post Survey":
						SosCrmDataContext.Instance.MS_AccountSetupCheckLists.SetKeyValue(input.AccountId,
							MS_AccountSetupCheckList.Columns.PostSurvey);
						break;
					case "Pre Survey":
						SosCrmDataContext.Instance.MS_AccountSetupCheckLists.SetKeyValue(input.AccountId,
							MS_AccountSetupCheckList.Columns.PreSurvey);
						break;
				}
			}
			catch (Exception ex)
			{
				result.Code = (int)ErrorCodes.UnexpectedException;
				result.Message = string.Format("Exception thrown: {0}", ex.Message);
			}
			return result;
		}
		private string SaveMapToTokenAnswers(List<IFnsMapToTokenAnswer> mapToTokenAnswers, long accountId, string user)
		{
			if (mapToTokenAnswers.Count == 0)
			{
				return string.Empty;
			}

			string PrimaryCustomer_Email = null;
			string SystemDetails_Password = null;
			short? ContractTerms_BillingDate = null;
			//BEGIN PrimaryCustomer.FullName
			string PrimaryCustomer_FullName_Prefix = null;
			string PrimaryCustomer_FullName_FirstName = null;
			string PrimaryCustomer_FullName_MiddleName = null;
			string PrimaryCustomer_FullName_LastName = null;
			string PrimaryCustomer_FullName_Postfix = null;
			//END PrimaryCustomer.FullName

			foreach (var mttAnswer in mapToTokenAnswers)
			{
				switch (mttAnswer.TokenId)
				{
					case 1: // PrimaryCustomer.FullName
						if (PrimaryCustomer_FullName_FirstName != null) // only need to check FirstName since it is required
						{
							return "Only one primary customer fullname is allowed";
						}
						var errMsg = ParseCustomerFullName((string)mttAnswer.Answer,
							out PrimaryCustomer_FullName_Prefix,
							out PrimaryCustomer_FullName_FirstName,
							out PrimaryCustomer_FullName_MiddleName,
							out PrimaryCustomer_FullName_LastName,
							out PrimaryCustomer_FullName_Postfix
						);
						if (!string.IsNullOrWhiteSpace(errMsg))
						{
							return errMsg;
						}
						break;
					case 19: // PrimaryCustomer.Email
						if (PrimaryCustomer_Email != null)
						{
							return "Only one primary customer email is allowed";
						}
						PrimaryCustomer_Email = (string)mttAnswer.Answer;
						if (PrimaryCustomer_Email == null || (!SubSonic.Sugar.Validation.IsEmail(PrimaryCustomer_Email)))
						{
							return string.Format("Invalid primary customer email: {0}", PrimaryCustomer_Email);
						}
						break;
					case 21: // SystemDetails.Password
						if (SystemDetails_Password != null)
						{
							return "Only one account password is allowed";
						}
						SystemDetails_Password = (string)mttAnswer.Answer;
						if (SystemDetails_Password == null || (SystemDetails_Password.Length < 2)) //?????
						{
							return string.Format("Invalid account password: '{0}'", SystemDetails_Password);
						}
						break;
					case 14: // ContractTerms.BillingDate
						if (ContractTerms_BillingDate != null)
						{
							return "Only one billing date is allowed";
						}
						ContractTerms_BillingDate = (short?)(long?)mttAnswer.Answer;
						if (ContractTerms_BillingDate == null ||
							(ContractTerms_BillingDate.Value < 1 || 25 < ContractTerms_BillingDate.Value))
						{
							return "Billing date must be between the 1st and 25th";
						}
						break;
				}
			}

			SaveTokenAnswers(user, accountId,
				PrimaryCustomer_Email: PrimaryCustomer_Email,
				SystemDetails_Password: SystemDetails_Password,
				ContractTerms_BillingDate: ContractTerms_BillingDate,
				PrimaryCustomer_FullName_Prefix: PrimaryCustomer_FullName_Prefix,
				PrimaryCustomer_FullName_FirstName: PrimaryCustomer_FullName_FirstName,
				PrimaryCustomer_FullName_MiddleName: PrimaryCustomer_FullName_MiddleName,
				PrimaryCustomer_FullName_LastName: PrimaryCustomer_FullName_LastName,
				PrimaryCustomer_FullName_Postfix: PrimaryCustomer_FullName_Postfix
			);

			return string.Empty;
		}
		private void SaveTokenAnswers(string username, long accountId,
			string PrimaryCustomer_Email,
			string SystemDetails_Password,
			short? ContractTerms_BillingDate,
			//BEGIN PrimaryCustomer.FullName
			string PrimaryCustomer_FullName_Prefix,
			string PrimaryCustomer_FullName_FirstName,
			string PrimaryCustomer_FullName_MiddleName,
			string PrimaryCustomer_FullName_LastName,
			string PrimaryCustomer_FullName_Postfix
			//END PrimaryCustomer.FullName
			)
		{
			//@HACK: to get around SharedDbConnection issues
			SurveyEngineDataContext.Instance.SV_Results.SaveMapToTokenAnswers(username, accountId,
				PrimaryCustomer_Email: PrimaryCustomer_Email,
				SystemDetails_Password: SystemDetails_Password,
				ContractTerms_BillingDate: ContractTerms_BillingDate,
				PrimaryCustomer_FullName_Prefix: PrimaryCustomer_FullName_Prefix,
				PrimaryCustomer_FullName_FirstName: PrimaryCustomer_FullName_FirstName,
				PrimaryCustomer_FullName_MiddleName: PrimaryCustomer_FullName_MiddleName,
				PrimaryCustomer_FullName_LastName: PrimaryCustomer_FullName_LastName,
				PrimaryCustomer_FullName_Postfix: PrimaryCustomer_FullName_Postfix
			);

			//@NOTE: this transaction cannot be run inside of a transaction to a different database since it uses SharedDbConnection
			//
			//DatabaseHelper.UseTransaction(SOS.Data.SubSonicConfigHelper.SOS_CRM_PROVIDER_NAME, () =>
			//{
			//	var instance = SosCrmDataContext.Instance;
			//
			//	if ((PrimaryCustomer_FullName_FirstName != null) || (PrimaryCustomer_Email != null))
			//	{
			//		// load primary customer
			//		var customer = instance.AE_Customers.GetByAccountID(accountId, "PRI");
			//
			//		if (PrimaryCustomer_FullName_FirstName != null) // only need to check FirstName since it is required
			//		{
			//			customer.Prefix = PrimaryCustomer_FullName_Prefix;
			//			customer.FirstName = PrimaryCustomer_FullName_FirstName;
			//			customer.MiddleName = PrimaryCustomer_FullName_MiddleName;
			//			customer.LastName = PrimaryCustomer_FullName_LastName;
			//			customer.Postfix = PrimaryCustomer_FullName_Postfix;
			//		}
			//		if (PrimaryCustomer_Email != null)
			//		{
			//			customer.Email = PrimaryCustomer_Email;
			//		}
			//		// save primary customer
			//		customer.Save(username);
			//	}
			//
			//	if (SystemDetails_Password != null)
			//	{
			//		var account = instance.MS_Accounts.LoadByPrimaryKey(accountId);
			//		account.AccountPassword = SystemDetails_Password;
			//		account.Save(username);
			//	}
			//
			//	if (ContractTerms_BillingDate != null)
			//	{
			//		var salesInfos = instance.MS_AccountSalesInformations.LoadByPrimaryKey(accountId);
			//		salesInfos.BillingDay = ContractTerms_BillingDate.Value;
			//		salesInfos.Save(username);
			//	}
			//
			//	// commit transaction
			//	return true;
			//});
		}

		private string ParseCustomerFullName(string fullname, out string prefix, out string firstName, out string middleName, out string lastName, out string postfix)
		{
			// example of fullname: Prefix|FirstName|MiddleName|LastName|Postfix

			if (string.IsNullOrWhiteSpace(fullname))
			{
				// set all out parameters
				prefix = firstName = middleName = lastName = postfix = null;
				return ""; // no error message since there is no fullname to parse
			}

			var parts = fullname.Split(new char[] { '|' }, StringSplitOptions.None);
			if (parts.Length != 5)
			{
				// set all out parameters
				prefix = firstName = middleName = lastName = postfix = null;
				return "Customer FullName must contain five parts";
			}
			prefix = StringHelper.NullIfWhiteSpace(parts[0]);
			firstName = StringHelper.NullIfWhiteSpace(parts[1]);
			middleName = StringHelper.NullIfWhiteSpace(parts[2]);
			lastName = StringHelper.NullIfWhiteSpace(parts[3]);
			postfix = StringHelper.NullIfWhiteSpace(parts[4]);

			if (firstName == null)
			{
				return "Customer FirsName is required";
			}
			if (lastName == null)
			{
				return "Customer LasName is required";
			}
			return "";
		}

		public IFnsResult<List<SurveyEngineContracts.IFnsAnswer>> ResultAnswers(long resultID)
		{
			var result = new FnsResult<List<SurveyEngineContracts.IFnsAnswer>> { Code = (int)ErrorCodes.Success, Message = "Success" };
			try
			{
				result.Value = SurveyEngineModels.FnsAnswer.ConvertAllFrom(SurveyEngineDataContext.Instance.SV_Answers.ByResultID(resultID));
			}
			catch (Exception ex)
			{
				result.Code = (int)ErrorCodes.UnexpectedException;
				result.Message = string.Format("Exception thrown: {0}", ex.Message);
			}
			return result;
		}

		public IFnsResult<List<SurveyEngineContracts.IFnsResultView>> ResultViewsForAccount(long accountID)
		{
			var result = new FnsResult<List<SurveyEngineContracts.IFnsResultView>> { Code = (int)ErrorCodes.Success, Message = "Success" };
			try
			{
				result.Value = SurveyEngineModels.FnsResultView.ConvertAllFrom(SurveyEngineDataContext.Instance.SV_ResultsViews.ByAccountID(accountID));
			}
			catch (Exception ex)
			{
				result.Code = (int)ErrorCodes.UnexpectedException;
				result.Message = string.Format("Exception thrown: {0}", ex.Message);
			}
			return result;
		}

		#endregion Results

		#region Maps

		public IFnsResult<IFnsQuestionMeaningTokenMap> QuestionMeaningTokenMapPost(IFnsQuestionMeaningTokenMap fnsQuestionMeaningTokenMap, int userID)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "QuestionMeaningTokenMapPost";
			var result = new FnsResult<IFnsQuestionMeaningTokenMap>
			{
				Code = (int)ErrorCodes.GeneralMessage
				,
				Message = string.Format("Initializing '{0}'", METHOD_NAME)
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				var newMap = new SV_QuestionMeanings_Tokens_Map
				{
					QuestionMeaningId = fnsQuestionMeaningTokenMap.QuestionMeaningId,
					TokenId = fnsQuestionMeaningTokenMap.TokenId,
					CreatedOn = fnsQuestionMeaningTokenMap.CreatedOn,
				};
				newMap.Save(userID);
				result.Value = new FnsQuestionMeaningTokenMap(newMap);

				// ** Set result values
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsQuestionMeaningTokenMap>
				{
					Code = (int)ErrorCodes.UnexpectedException
					,
					Message = string.Format("Exception thrown at '{1}': {0}", ex.Message, METHOD_NAME)
				};
			}

			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<IFnsQuestionPossibleAnswerMap> QuestionPossibleAnswerMapPost(IFnsQuestionPossibleAnswerMap fnsQuestionPossibleAnswerMap, int userID)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "QuestionPossibleAnswerMapPost";
			var result = new FnsResult<IFnsQuestionPossibleAnswerMap>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing '{0}'", METHOD_NAME)
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				var newMap = new SV_Questions_PossibleAnswers_Map
				{
					QuestionId = fnsQuestionPossibleAnswerMap.QuestionId,
					PossibleAnswerId = fnsQuestionPossibleAnswerMap.PossibleAnswerId,
					Expands = fnsQuestionPossibleAnswerMap.Expands,
					Fails = fnsQuestionPossibleAnswerMap.Fails,
					CreatedOn = fnsQuestionPossibleAnswerMap.CreatedOn,
				};

				// ** Chekc that the tuple is there or not.
				var map = SurveyEngineDataContext.Instance.SV_Questions_PossibleAnswers_Maps.LoadByPrimaryKey(
						fnsQuestionPossibleAnswerMap.QuestionId, fnsQuestionPossibleAnswerMap.PossibleAnswerId);
				if (map != null)
				{
					SurveyEngineDataContext.Instance.SV_Questions_PossibleAnswers_Maps.Update(newMap);
				}
				else
				{
					newMap.Save(userID);
				}
				result.Value = new FnsQuestionPossibleAnswerMap(newMap);

				// ** Set result values
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsQuestionPossibleAnswerMap>
				{
					Code = (int)ErrorCodes.UnexpectedException
					,
					Message = string.Format("Exception thrown at '{1}': {0}", ex.Message, METHOD_NAME)
				};
			}

			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<bool> QuestionPossibleAnswerMapDelete(int questionId, int possibleAnswerId, int userID)
		{
			var result = new FnsResult<bool>();
			result.Value = SurveyEngineDataContext.Instance.SV_Questions_PossibleAnswers_Maps.Delete(questionId, possibleAnswerId);
			return result;
		}
		#endregion Maps
	}
}