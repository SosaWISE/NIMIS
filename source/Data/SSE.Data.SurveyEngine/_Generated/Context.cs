


using System;
using SubSonic;
using SOS.Data;

namespace SSE.Data.SurveyEngine
{
	public partial class SurveyEngineDataContext
	{
		#region Internal Instance

		private static SurveyEngineDataContext _contextInstance;
		private static readonly object _syncRootContextInstance = new object();

		public static SurveyEngineDataContext Instance
		{
			get
			{
				if (_contextInstance == null)
				{
					lock (_syncRootContextInstance)
					{
						if (_contextInstance == null)
						{
							_contextInstance = new SurveyEngineDataContext();
						}
					}
				}
				return _contextInstance;
			}
		}

		#endregion // Internal Instance

		#region Controllers Properties

		SV_AnswerController _SV_Answers;
		public SV_AnswerController SV_Answers
		{
			get
			{
				if (_SV_Answers == null) _SV_Answers = new SV_AnswerController();
				return _SV_Answers;
			}
		}

		SV_PossibleAnswerController _SV_PossibleAnswers;
		public SV_PossibleAnswerController SV_PossibleAnswers
		{
			get
			{
				if (_SV_PossibleAnswers == null) _SV_PossibleAnswers = new SV_PossibleAnswerController();
				return _SV_PossibleAnswers;
			}
		}

		SV_PossibleAnswerTranslationController _SV_PossibleAnswerTranslations;
		public SV_PossibleAnswerTranslationController SV_PossibleAnswerTranslations
		{
			get
			{
				if (_SV_PossibleAnswerTranslations == null) _SV_PossibleAnswerTranslations = new SV_PossibleAnswerTranslationController();
				return _SV_PossibleAnswerTranslations;
			}
		}

		SV_QuestionMeaningController _SV_QuestionMeanings;
		public SV_QuestionMeaningController SV_QuestionMeanings
		{
			get
			{
				if (_SV_QuestionMeanings == null) _SV_QuestionMeanings = new SV_QuestionMeaningController();
				return _SV_QuestionMeanings;
			}
		}

		SV_QuestionMeanings_Tokens_MapController _SV_QuestionMeanings_Tokens_Maps;
		public SV_QuestionMeanings_Tokens_MapController SV_QuestionMeanings_Tokens_Maps
		{
			get
			{
				if (_SV_QuestionMeanings_Tokens_Maps == null) _SV_QuestionMeanings_Tokens_Maps = new SV_QuestionMeanings_Tokens_MapController();
				return _SV_QuestionMeanings_Tokens_Maps;
			}
		}

		SV_QuestionController _SV_Questions;
		public SV_QuestionController SV_Questions
		{
			get
			{
				if (_SV_Questions == null) _SV_Questions = new SV_QuestionController();
				return _SV_Questions;
			}
		}

		SV_Questions_PossibleAnswers_MapController _SV_Questions_PossibleAnswers_Maps;
		public SV_Questions_PossibleAnswers_MapController SV_Questions_PossibleAnswers_Maps
		{
			get
			{
				if (_SV_Questions_PossibleAnswers_Maps == null) _SV_Questions_PossibleAnswers_Maps = new SV_Questions_PossibleAnswers_MapController();
				return _SV_Questions_PossibleAnswers_Maps;
			}
		}

		SV_QuestionTranslationController _SV_QuestionTranslations;
		public SV_QuestionTranslationController SV_QuestionTranslations
		{
			get
			{
				if (_SV_QuestionTranslations == null) _SV_QuestionTranslations = new SV_QuestionTranslationController();
				return _SV_QuestionTranslations;
			}
		}

		SV_ResultController _SV_Results;
		public SV_ResultController SV_Results
		{
			get
			{
				if (_SV_Results == null) _SV_Results = new SV_ResultController();
				return _SV_Results;
			}
		}

		SV_SurveyController _SV_Surveys;
		public SV_SurveyController SV_Surveys
		{
			get
			{
				if (_SV_Surveys == null) _SV_Surveys = new SV_SurveyController();
				return _SV_Surveys;
			}
		}

		SV_SurveyTranslationController _SV_SurveyTranslations;
		public SV_SurveyTranslationController SV_SurveyTranslations
		{
			get
			{
				if (_SV_SurveyTranslations == null) _SV_SurveyTranslations = new SV_SurveyTranslationController();
				return _SV_SurveyTranslations;
			}
		}

		SV_SurveyTypeController _SV_SurveyTypes;
		public SV_SurveyTypeController SV_SurveyTypes
		{
			get
			{
				if (_SV_SurveyTypes == null) _SV_SurveyTypes = new SV_SurveyTypeController();
				return _SV_SurveyTypes;
			}
		}

		SV_TokenController _SV_Tokens;
		public SV_TokenController SV_Tokens
		{
			get
			{
				if (_SV_Tokens == null) _SV_Tokens = new SV_TokenController();
				return _SV_Tokens;
			}
		}

		#endregion //Controllers Properties

		#region View Controllers Properties

		SV_ResultsViewController _SV_ResultsViews;
		public SV_ResultsViewController SV_ResultsViews
		{
			get
			{
				if (_SV_ResultsViews == null) _SV_ResultsViews = new SV_ResultsViewController();
				return _SV_ResultsViews;
			}
		}

		#endregion //View Controllers Properties
	}

	#region Controllers

	public class SV_AnswerController : BaseTableController<SV_Answer, SV_AnswerCollection> { }
	public class SV_PossibleAnswerController : BaseTableController<SV_PossibleAnswer, SV_PossibleAnswerCollection> { }
	public class SV_PossibleAnswerTranslationController : BaseTableController<SV_PossibleAnswerTranslation, SV_PossibleAnswerTranslationCollection> { }
	public class SV_QuestionMeaningController : BaseTableController<SV_QuestionMeaning, SV_QuestionMeaningCollection> { }
	public class SV_QuestionMeanings_Tokens_MapController : BaseTableController<SV_QuestionMeanings_Tokens_Map, SV_QuestionMeanings_Tokens_MapCollection> { }
	public class SV_QuestionController : BaseTableController<SV_Question, SV_QuestionCollection> { }
	public class SV_Questions_PossibleAnswers_MapController : BaseTableController<SV_Questions_PossibleAnswers_Map, SV_Questions_PossibleAnswers_MapCollection> { }
	public class SV_QuestionTranslationController : BaseTableController<SV_QuestionTranslation, SV_QuestionTranslationCollection> { }
	public class SV_ResultController : BaseTableController<SV_Result, SV_ResultCollection> { }
	public class SV_SurveyController : BaseTableController<SV_Survey, SV_SurveyCollection> { }
	public class SV_SurveyTranslationController : BaseTableController<SV_SurveyTranslation, SV_SurveyTranslationCollection> { }
	public class SV_SurveyTypeController : BaseTableController<SV_SurveyType, SV_SurveyTypeCollection> { }
	public class SV_TokenController : BaseTableController<SV_Token, SV_TokenCollection> { }

	#endregion //Controllers

	#region View Controllers

	public class SV_ResultsViewController : BaseViewController<SV_ResultsView, SV_ResultsViewCollection> { }

	#endregion //View Controllers
}
