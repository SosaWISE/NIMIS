using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using NXS.Data.Accounting;
using NXS.Data.Accounting.ControllerExtensions;
using NXS.Data.Accounting.Models;
using NXS.Data.Inventory;
using NXS.Data.Inventory.ControllerExtensions;
using NXS.Data.Letters;
using NXS.Data.Licensing;
using NXS.Data.Licensing.ControllerExtensions;
using NXS.Data.Licensing.Models;
using NXS.Logic.Letters;
using NXS.Services.Wcf.AppServices.Logic;
using NXS.Services.Wcf.AppServices.Models;
using SOS.Data.Extensions;
using SOS.Data.HumanResource;
using SOS.Data.HumanResource.ControllerExtensions;
using SOS.Data.HumanResource.Models;
using SOS.Data.SorCrm.ControllerExtensions;
using SOS.Data.SosCrm;
using SOS.Data.SosCrm.ControllerExtensions;
using SOS.Data.SosCrm.Models;
using SOS.Lib.Util.ActiveDirectory;
using SOS.Lib.Util.Extensions;
using ConfigurationSettings = SOS.Lib.Util.Configuration.ConfigurationSettings;

namespace NXS.Services.Wcf.AppServices.HRManager
{
	public class HiringManagerService : IHiringManagerService
	{
		#region Fields

		readonly HumanResourceDataContext _humanResourceDB;
		readonly SosCrmDataContext _cmsDB;
		readonly AccountingDataContext _accountingDB;
		readonly LicensingDataContext _licensingDB;
		readonly LettersDataContext _lettersDB;
		readonly InventoryDataContext _inventoryDB;

		private static readonly object _syncSetup = new object();
		private static object _setup;
		private static bool _isSetup;
		public static bool IsSetup
		{
			get
			{
				if (_setup == null)
					lock (_syncSetup)
					{
						if (_setup == null)
						{
							// Initialize connection strings
							ConfigurationSettings.Current.SetProperties("Preferences", ConfigurationManager.AppSettings["Environment"]);
							SOS.Data.SubSonicConfigHelper.SetupConnectionStrings();

							_setup = new object();
							_isSetup = true;
						}
					}
				// Return value
				return _isSetup;
			}
		}

		readonly CountryMetaData _countryIDs;
		public CountryMetaData CountryIDs
		{
			get { return _countryIDs; }
		}

		#endregion //Fields

		#region .ctor

		private static void SetupSystem()
		{
			ConfigurationSettings.Current.SetProperties("Preferences", ConfigurationManager.AppSettings["Environment"]);
			Console.WriteLine("Hopefully this is never called.");
		}

		public HiringManagerService()
		{
			if (!IsSetup) SetupSystem();

			_countryIDs = new CountryMetaData();
			_humanResourceDB = HumanResourceDataContext.Instance;
			_cmsDB = SosCrmDataContext.Instance;
			_accountingDB = AccountingDataContext.Instance;
			_licensingDB = LicensingDataContext.Instance;
			_lettersDB = LettersDataContext.Instance;
			_inventoryDB = InventoryDataContext.Instance;
		}

		#endregion //.ctor

		#region Country ID's

		public CountryMetaData GetCountryIDs()
		{
			return CountryIDs;
		}

		#endregion Country ID's

		#region ADLogin

		public bool IsValidLogin(string szUsername, string szPassword, string szDomainName)
		{
			szUsername = EmailToUsername(szUsername);
			ADUtility.LoginResult loginResult = ADManager.Login(szUsername, szPassword, szDomainName);
			return (loginResult == ADUtility.LoginResult.LOGIN_OK);
		}

		public string[] GetGroupsForUser(string szUsername)
		{
			// Locals
			string[] result;

			// Print to Console so we can track what is going on.
			System.Diagnostics.Debug.WriteLine("C|GetGroupsForUser(\"{0}\")", szUsername);

			szUsername = EmailToUsername(szUsername);
			ADUser oUser = ADManager.Instance.LoadUser(szUsername);
			if (oUser != null)
			{

				result = new string[oUser.Groups.Count];
				for (int i = 0; i < result.Length; i++)
				{
					result[i] = oUser.Groups[i].Name;
					System.Diagnostics.Debug.WriteLine("C|GetGroupsForUser RESULT:");
					System.Diagnostics.Debug.WriteLine("C|\t[{0}]", oUser.Groups[i].Name);
				}
			}
			else
			{
				System.Diagnostics.Debug.WriteLine("C|GetGroupsForUser RESULT: [User Not Found]");
				result = new string[0];
			}

			return result;
		}

		//private string UsernameToEmail(string username)
		//{
		//    return (string.IsNullOrEmpty(username) || username.Contains("@")) ? username : string.Format("{0}@silverlinesecurity.com", username);
		//}
		private static string EmailToUsername(string username)
		{
			return (string.IsNullOrEmpty(username) || !username.Contains("@")) ? username : username.Substring(0, username.IndexOf("@", StringComparison.Ordinal));
		}

		#endregion //ADLogin

		#region Address

		public IList<SOS.Data.SosCrm.MC_PoliticalCountry> GetAllCountries()
		{
			return _cmsDB.MC_PoliticalCountries.LoadAll();
		}
		public IList<SOS.Data.SosCrm.MC_PoliticalState> GetPoliticalStatesUSA()
		{
			return _cmsDB.MC_PoliticalStates.GetAllInCountry("USA");
		}
		public IList<SOS.Data.SosCrm.MC_PoliticalState> GetPoliticalStatesCAN()
		{
			return _cmsDB.MC_PoliticalStates.GetAllInCountry("CAN");
		}
		public IList<SOS.Data.SosCrm.MC_PoliticalState> GetPoliticalStates(string szCountryID)
		{
			return _cmsDB.MC_PoliticalStates.GetAllInCountry(szCountryID);
		}

		public IList<MC_PoliticalTimeZone> GetAllPoliticalTimeZones()
		{
			return _cmsDB.MC_PoliticalTimeZones.GetAllActive();
		}

		#endregion //Address

		#region Barcodes

		public BE_PrefixDocument GetPrefixDocument(int prefixDocID)
		{
			return _cmsDB.BE_PrefixDocuments.LoadByPrimaryKey(prefixDocID);
		}

		public BarcodesValidationInfo GetBarcodesValidationInfo()
		{
			var bvi = new BarcodesValidationInfo
			{
				PrefixValidationDictionary = new Dictionary<BE_PrefixDocument.PrefixDocEnum, string>()
			};

			BE_PrefixDocumentCollection list = _cmsDB.BE_PrefixDocuments.LoadAll();
			foreach (var item in list)
			{
				bvi.PrefixValidationDictionary.Add((BE_PrefixDocument.PrefixDocEnum)item.PrefixDocID, item.PrefixDocName);
			}
			return bvi;
		}
		public RecruitBarcodes GetBarcodesForRecruit(int recruitID)
		{
			var result = new RecruitBarcodes
			{
				RecruitID = recruitID,
				BarcodeDocumentDictionary = new Dictionary<BE_DocType.DocTypeEnum, string>()
			};

			List<BE_Barcode> list = SosCrmDataStoredProcedureManager.BE_BarcodesGetLastBarcodesForRecruitID(recruitID).ToList<BE_Barcode>();
			foreach (var item in list)
			{
				result.BarcodeDocumentDictionary.Add((BE_DocType.DocTypeEnum)item.DocTypeId, item.BarcodeNumber);
			}
			return result;
		}
		public RecruitBarcodes SaveBarcodesForRecruit(RecruitBarcodes recruitBarcodes, string username)
		{
			ValidateUsername(username);

			int recruitID = recruitBarcodes.RecruitID;
			RecruitBarcodes currentBarcodes = GetBarcodesForRecruit(recruitID);

			Dictionary<BE_DocType.DocTypeEnum, string> dict = recruitBarcodes.GetMissingOrDifferent(currentBarcodes);
			DateTime now = DateTime.Now;
			foreach (KeyValuePair<BE_DocType.DocTypeEnum, string> kvp in dict)
			{

				var newBarcode = new BE_Barcode
				{
					BarcodeNumber = kvp.Value,
					DocTypeId = (int)kvp.Key,
					ForeignKey = recruitID,
					DateX = now
				};
				newBarcode.Save(username);
			}

			return GetBarcodesForRecruit(recruitID);
		}

		public IList<BE_DocType> GetDocTypesByDocTypeColumnID(BE_DocTypeColumn.DocTypeColumnEnum docTypeColumn)
		{
			return _cmsDB.BE_DocTypes.GetByDocTypeColumn((int)docTypeColumn);
		}
		public IList<BE_DocType> Bob()
		{
			var d = new List<BE_DocType> { new BE_DocType { DocTypeID = 1 } };
			return d;
		}
		public IList<Ug> Ug()
		{
			var d = new List<Ug> { new Ug() };
			return d;
		}

		public BE_BarcodeSchema GetBarcodeSchema(int barcodeSchemaID)
		{
			return _cmsDB.BE_BarcodeSchemas.LoadByPrimaryKey(barcodeSchemaID);
		}
		public BE_BarcodeSchema GetBarcodeSchemaByDocType(BE_DocType.DocTypeEnum docType, bool isAutogenerated)
		{
			return _cmsDB.BE_BarcodeSchemas.GetByDocType((int)docType, isAutogenerated);
		}
		public BE_BarcodeSchema SaveBarcodeSchema(BE_BarcodeSchema barcodeSchemaObj, string username)
		{
			ValidateUsername(username);
			if (barcodeSchemaObj == null)
				throw new ArgumentNullException("barcodeSchemaObj");

			BE_BarcodeSchema item = BE_BarcodeSchema.LoadFrom(barcodeSchemaObj);
			item.Save(username);
			return item;
		}

		public ExistingBarcodeResult GetExistingBarcodeResult(Guid queryKey, int recruitID, BE_DocType.DocTypeEnum docType, string barcodeNumber)
		{
			ExistingBarcodeResult result = _cmsDB.ExistingBarcodeResults.ExistingBarcode(recruitID, docType, barcodeNumber)
				?? new ExistingBarcodeResult { RecruitID = recruitID };
			result.QueryKey = queryKey;

			return result;
		}

		#endregion //Barcodes

		#region DocStatus

		public RU_DocStatus.DocStatusEnum WcfHackDocStatusEnum()
		{
			return RU_DocStatus.DocStatusEnum.Not_Received;
		}

		public IList<RU_DocStatus> GetAllDocStatus()
		{
			IList<RU_DocStatus> list = _humanResourceDB.RU_DocStatuses.LoadAllCached();
			return list;
		}

		#endregion //DocStatus

		#region Email

		public void QueueEmail(List<string> emailList, string subject, string body
			, bool isHtml, string senderName, string senderAddress, bool destroyAfter, List<Attachment> attachmentList, string username)
		{
			ValidateUsername(username);

			bool hasAttachments = attachmentList != null && attachmentList.Count > 0;
			ES_Message msg = _humanResourceDB.ES_Messages.CreateAndSaveMessage(emailList, subject, body, isHtml, senderName, senderAddress, destroyAfter, username, true, !hasAttachments);
			if (hasAttachments)
			{

				var attachments = new ES_MessageAttachmentCollection();
				foreach (var a in attachmentList)
				{
					attachments.Add(new ES_MessageAttachment
					{
						MessageID = msg.MessageID,
						Attachment = a.File,
						FileName = a.FileName,
					});
				}

				attachments.SaveAll(username);

				msg.SetReady(username);
			}
		}

		public void QueueWelcomeEmail(int recruitID, string fileName, string email, string subject, string body
			, bool isHtml, string senderName, string senderAddress, bool destroyAfter, string username)
		{
			if (fileName == null)
				throw new ArgumentNullException("fileName");
			if (fileName.Trim().Length == 0)
				throw new Exception("fileName is empty");

			ValidateUsername(username);

			const int WELCOME_EMAIL_ID = 137;
			byte[] buffer = TemplateHelper.CreateTemplatePdfBuffer(_lettersDB, WELCOME_EMAIL_ID, recruitID);

			var attachmentList = new List<Attachment>
			{
				new Attachment{
					File = buffer,
					FileName = fileName,
				}
			};

			var emailList = new List<string> { email };
			QueueEmail(emailList, subject, body, isHtml, senderName, senderAddress, destroyAfter, attachmentList, username);
		}

		#endregion //Email

		#region Market

		public IList<MS_Market> GetAllActiveMarkets()
		{
			IList<MS_Market> list = _cmsDB.MS_Markets.LoadAllActive();
			return list;
		}

		#endregion //Market

		#region Migration

		public RU_Migration GetLatestMigration()
		{
			RU_Migration item = _humanResourceDB.RU_Migrations.Latest();
			return item;
		}

		#endregion //Migration

		#region PhoneCellCarriers

		public IList<RU_PhoneCellCarrier> GetActiveCellCarriers()
		{
			IList<RU_PhoneCellCarrier> list = _humanResourceDB.RU_PhoneCellCarriers.LoadAllActive();
			return list;
		}
		public RU_PhoneCellCarrier GetCellCarrier(int cellCarrierID)
		{
			RU_PhoneCellCarrier item = _humanResourceDB.RU_PhoneCellCarriers.LoadByPrimaryKey(cellCarrierID);
			return item;
		}

		#endregion //PhoneCellCarriers

		#region PossibleReportsTos

		public IList<PossibleReportTo> GetPossibleReportTos(int seasonID, short userTypeID)
		{
			return
				HumanResourceDataStoredProcedureManager.RU_RecruitsGetPossibleReportTos(seasonID, userTypeID)
				.ToList<PossibleReportTo>();
		}

		#endregion //PossibleReportsTos

		#region PossibleReportsTos

		public IList<PossibleRecruitedBy> GetPossibleRecruitedBy()
		{
			RU_UserCollection list = _humanResourceDB.RU_Users.LoadAllActive();
			// ReSharper disable RedundantTypeArgumentsOfMethod
			return list.ConvertAll<RU_User, PossibleRecruitedBy>(a => new PossibleRecruitedBy(a.UserID, a.FullName));
			// ReSharper restore RedundantTypeArgumentsOfMethod
		}

		#endregion //PossibleReportsTos

		#region PayScales

		public IList<RU_Payscale> GetAllPayscales()
		{
			RU_PayscaleCollection list = _humanResourceDB.RU_Payscales.LoadAll();
			return list;
		}

		#endregion //PayScales

		#region Recruits

		public RU_Recruit GetRecruit(int recruitID)
		{
			RU_Recruit item = _humanResourceDB.RU_Recruits.LoadByPrimaryKey(recruitID);
			return item;
		}
		public RU_Recruit SaveRecruit(RU_Recruit recruitObj, string username)
		{
			ValidateUsername(username);
			if (recruitObj == null)
				throw new ArgumentNullException("recruitObj");

			RU_Recruit item = RU_Recruit.LoadFrom(recruitObj);
			item.IsActive = true;

			//this feels like a hack, but you do what ya gotta do
			item.CreatedBy = item.CreatedBy ?? "";
			item.ModifiedBy = item.ModifiedBy ?? "";

			item.Save(username);
			return item;
		}
		public IList<RU_Recruit> GetRecruitsForUser(int userID)
		{
			List<RU_Recruit> list = _humanResourceDB.RU_Recruits.ForUser(userID).ToList();
			return list;
		}
		public RU_Recruit GetNewestRecruit(int userID)
		{
			var oItem =
				HumanResourceDataStoredProcedureManager.RU_RecruitsGetNewestRecruit(userID)
				.ToItem<RU_Recruit>();
			return oItem;
		}
		public RU_Recruit GetRecruitByUserAndSeason(int userID, int seasonID)
		{
			RU_Recruit item = _humanResourceDB.RU_Recruits.LastByUserAndSeason(userID, seasonID).FirstOrDefault();
			return item;
		}
		public RU_Recruit GetRecruitByUserSeasonAndUserType(int userID, int seasonID, short userTypeID)
		{
			RU_Recruit item = _humanResourceDB.RU_Recruits.LastByUserSeasonAndUserType(userID, seasonID, userTypeID).FirstOrDefault();
			return item;
		}


		public RecruitInfo GetRecruitInfo(int recruitID)
		{
			return new RecruitInfo(_humanResourceDB, GetRecruit(recruitID));
		}
		public RecruitInfo SaveRecruitInfo(RecruitInfo recruitInfoObj, string username)
		{
			ValidateUsername(username);
			if (recruitInfoObj == null)
				throw new ArgumentNullException("recruitInfoObj");

			RU_Recruit recruit = recruitInfoObj.Recruit;

			//save address
			int? addressID = null;
			if (recruitInfoObj.Address != null)
			{

				//load
				RU_RecruitAddress newAddress = RU_RecruitAddress.LoadFrom(recruitInfoObj.Address);

				DateTime now = DateTime.Now;

				//don't set unless new
				if (newAddress.IsNew)
				{
					newAddress.CreatedByID = 0;
					newAddress.CreatedDate = now;
				}

				//don't set unless dirty
				if (newAddress.IsDirty)
				{
					newAddress.ModifiedByID = 0;
					newAddress.ModifiedDate = now;
				}

				//save
				newAddress.Save(username);
				//set
				addressID = newAddress.AddressId;
			}

			//set address on recruit
			// ReSharper disable RedundantCheckBeforeAssignment
			if (addressID != recruit.CurrentAddressID)
			{
				// ReSharper restore RedundantCheckBeforeAssignment

				recruit.CurrentAddressID = addressID;
			}

			//save recruit
			int recruitID = SaveRecruit(recruitInfoObj.Recruit, username).RecruitID;

			return GetRecruitInfo(recruitID);
		}

		public IList<RecruitInfo> GetRecruitInfosForUser(int userID)
		{
			// ReSharper disable RedundantTypeArgumentsOfMethod
			return GetRecruitsForUser(userID).ConvertAll<RU_Recruit, RecruitInfo>(item => new RecruitInfo(_humanResourceDB, item));
			// ReSharper restore RedundantTypeArgumentsOfMethod
		}
		public RecruitInfo GetNewestRecruitInfo(int userID)
		{
			return new RecruitInfo(_humanResourceDB, GetNewestRecruit(userID));
		}
		public RecruitInfo GetRecruitInfoByUserAndSeason(int userID, int seasonID)
		{
			return new RecruitInfo(_humanResourceDB, GetRecruitByUserAndSeason(userID, seasonID));
		}
		public int GetTopRecruitingLevel()
		{
			return 4;
		}
		public bool RecruitHasUndeletedRecruitInSameSeason(RU_Recruit recruit)
		{
			bool result = _humanResourceDB.RU_Recruits.HasUndeletedRecruitInSameSeason(recruit);
			return result;
		}

		public IList<SeasonsMap> GetRecruitSeasonsMaps(int nFromSeasonID, int toSeasonID)
		{
			List<SeasonsMap> list = _humanResourceDB.SeasonsMaps.GetRecruitSeasonsMaps(nFromSeasonID, toSeasonID).ToList();
			return list;
		}
		public IList<RU_Recruit> GetMigrationRecruits(int nFromSeasonID, int toSeasonID, short userTypeID, bool excludeAlreadyInSeason)
		{
			List<RU_Recruit> list = _humanResourceDB.RU_Recruits.GetMigrationRecruits(nFromSeasonID, toSeasonID, userTypeID, excludeAlreadyInSeason).ToList();
			return list;
		}

		public IDictionary<int, string> GetRecruitNames(int seasonID)
		{
			IDictionary<int, string> result = new Dictionary<int, string>();

			List<IDText> list = _humanResourceDB.IDTexts.GetRecruitNames(seasonID).ToList();
			foreach (IDText item in list)
			{
				if (!result.ContainsKey(item.ID))
				{
					result.Add(item.ID, item.Text);
				}
			}

			return result;
		}

		#endregion //Recruits

		#region RecruitCohabbitType

		public IList<RU_RecruitCohabbitType> GetAllRecruitCohabbitTypes()
		{
			IList<RU_RecruitCohabbitType> list = _humanResourceDB.RU_RecruitCohabbitTypes.LoadAll();
			return list;
		}

		#endregion //RecruitCohabbitType

		#region RecruitHistory

		public RecruitsHistory GetRecruitsHistory(long lRecruitsHistoryID)
		{
			RU_RecruitsHistory item = _humanResourceDB.RU_RecruitsHistories.LoadByPrimaryKey(lRecruitsHistoryID);
			var result = new RecruitsHistory(_humanResourceDB, _cmsDB, item);
			return result;
		}
		public IList<RecruitsHistoryVersion> GetHistoryForRecruit(int nUserID)
		{
			IList<RU_RecruitsHistory> list = _humanResourceDB.RU_RecruitsHistories.ByRecruit(nUserID);
			// ReSharper disable RedundantTypeArgumentsOfMethod
			return list.ConvertAll<RU_RecruitsHistory, RecruitsHistoryVersion>(item => new RecruitsHistoryVersion(_humanResourceDB, item));
			// ReSharper restore RedundantTypeArgumentsOfMethod
		}

		#endregion //RecruitHistory

		#region Recruiting Lines

		public RecruitLine GetRecruitLineForRecruit(int recruitID)
		{
			RecruitLine result;

			RecruitingLineView recLine = _humanResourceDB.RecruitingLineViews.LoadByRecruitID(recruitID);
			if (recLine != null)
			{

				result = new RecruitLine
				{
					Season = GetSeason(recLine.SeasonID),
					RoleLocation = GetRoleLocation(recLine.RoleLocationID)
				};

				//rep
				if (recLine.RepID.HasValue)
				{
					result.Rep = GetRecruitUserView(recLine.RepID.Value);
				}

				//team and office
				if (recLine.TeamID.HasValue)
				{
					result.Team = GetTeam(recLine.TeamID.Value);
				}
				if (recLine.TeamLocationID.HasValue)
				{
					result.Office = GetOffice(recLine.TeamLocationID.Value);
				}

				//manager
				if (recLine.ManagerID.HasValue)
				{
					result.Manager = GetRecruitUserView(recLine.ManagerID.Value);
				}
				//regional
				if (recLine.RegionID.HasValue)
				{
					result.Regional = GetRecruitUserView(recLine.RegionID.Value);
				}
				//national regional
				if (recLine.NationalRegionID.HasValue)
				{
					result.NationalRegional = GetRecruitUserView(recLine.NationalRegionID.Value);
				}
			}
			else
			{
				result = null;
			}

			return result;
		}

		#endregion //Recruiting Lines

		#region RecruitUserView

		public RecruitUserView GetRecruitUserView(int recruitID)
		{
			RecruitUserView item = _humanResourceDB.RecruitUserViews.LoadByRecruitID(recruitID);
			return item;
		}
		public RecruitUserView GetRecruitUserViewByUserAndSeason(int userID, int seasonID)
		{
			RecruitUserView item = _humanResourceDB.RecruitUserViews.LoadByUserAndSeason(userID, seasonID);
			return item;
		}
		public IList<RecruitUserView> GetAllNationalRegionalsForRoleLocationID(int roleLocationID)
		{
			IList<RecruitUserView> list = _humanResourceDB.RecruitUserViews.LoadByReportingLevelAndRoleLocation(GetTopRecruitingLevel(), roleLocationID);
			return list;
		}
		public IList<RecruitUserView> GetRecruitUserViewsThatReportToRecruit(int recruitID, int? reportingLevel)
		{
			IList<RecruitUserView> list = _humanResourceDB.RecruitUserViews.LoadByReportsToID(recruitID, reportingLevel);
			return list;
		}
		public IList<RecruitUserView> GetRecruitUserViewsThatManageTeam(int teamID)
		{
			IList<RecruitUserView> list = _humanResourceDB.RecruitUserViews.LoadTeamManagers(teamID);
			return list;
		}

		#endregion //RecruitUserView

		#region RoleLocations

		public RU_RoleLocation.RoleLocationEnum WcfHackRoleLocationEnum()
		{
			return RU_RoleLocation.RoleLocationEnum.Sales;
		}
		public IList<RU_RoleLocation> GetRoleLocationsForRecruiting()
		{
			IList<RU_RoleLocation> list = _humanResourceDB.RU_RoleLocations.LoadByPrimaryKeys(1, 2, 5);
			return list;
		}
		public RU_RoleLocation GetRoleLocation(int roleLocationID)
		{
			RU_RoleLocation item = _humanResourceDB.RU_RoleLocations.LoadByPrimaryKey(roleLocationID);
			return item;
		}
		public IList<RU_RoleLocation> GetAllRoleLocations()
		{
			IList<RU_RoleLocation> list = _humanResourceDB.RU_RoleLocations.LoadAll();
			return list;
		}

		#endregion //RoleLocations

		#region Schools

		public IList<RU_School> GetAllSchools()
		{
			IList<RU_School> list = _humanResourceDB.RU_Schools.LoadAll();
			return list;
		}

		#endregion //Schools

		#region Seasons

		public RU_Season GetCurrentSeason()
		{
			RU_Season item = _humanResourceDB.RU_Seasons.LoadCurrentSeason();
			return item;
		}
		public RU_Season GetSeason(int seasonID)
		{
			RU_Season item = _humanResourceDB.RU_Seasons.LoadByPrimaryKey(seasonID);
			return item;
		}
		public IList<RU_Season> GetAllSeasons()
		{
			IList<RU_Season> list = _humanResourceDB.RU_Seasons.LoadAll();
			return list;
		}
		public NewSeasonInfo GetNewSeasonInfo(int userID, bool? canShowInHiringManager)
		{
			var result = new NewSeasonInfo
			{
				ExistingSeasons = GetExistingSeasonsForUser(userID, canShowInHiringManager).ToList(),
				NonExistingSeasons = GetNonExistingSeasonsForUser(userID, canShowInHiringManager).ToList()
			};

			return result;
		}
		public IList<RU_Season> GetExistingSeasonsForUser(int userID, bool? canShowInHiringManager)
		{
			IList<RU_Season> list = _humanResourceDB.RU_Seasons.LoadExistingForUser(userID, canShowInHiringManager);
			return list;
		}
		public IList<RU_Season> GetNonExistingSeasonsForUser(int userID, bool? canShowInHiringManager)
		{
			IList<RU_Season> list = _humanResourceDB.RU_Seasons.LoadNonExistingForUser(userID, canShowInHiringManager);
			return list;
		}

		#endregion //Seasons

		#region Teams

		public RU_Team GetTeam(int teamID)
		{
			RU_Team item = _humanResourceDB.RU_Teams.LoadByPrimaryKey(teamID);
			return item;
		}
		public IList<RU_Team> GetAllTeamsForSeasonAndUserTypeID(int seasonID, short userTypeID)
		{
			RU_UserType userType = GetUserType(userTypeID);

			if (userType != null && UserTypeLogic.CanManageTeam(_humanResourceDB, userType))
			{
				return HumanResourceDataStoredProcedureManager.RU_TeamsGetAllForSeason(seasonID, userType.RoleLocationID)
					.ToCollection<RU_Team, RU_TeamCollection>();
			}

			// Default execution path.
			return new List<RU_Team>();
		}
		public IList<RU_Team> GetManageableTeams(int seasonID, int roleLocationID, int recruitID, int? teamLocationID)
		{
			IList<RU_Team> list = _humanResourceDB.RU_Teams.GetManageableTeams(seasonID, roleLocationID, recruitID, teamLocationID);
			return list;
		}
		public RU_Team SaveTeam(RU_Team teamObj, string username)
		{
			ValidateUsername(username);
			if (teamObj == null)
				throw new ArgumentNullException("teamObj");

			DateTime now = DateTime.Now;
			RU_Team item = RU_Team.LoadFrom(teamObj);
			item.IsActive = true;

			////this feels like a hack, but you do what ya gotta do
			//item.CreatedBy = item.CreatedBy ?? "";
			//item.ModifiedBy = item.ModifiedBy ?? "";
			//don't set unless new
			if (item.IsNew)
			{
				item.CreatedBy = username;
				item.CreatedOn = now;
			}
			//don't set unless dirty
			if (item.IsDirty)
			{
				item.ModifiedBy = username;
				item.ModifiedOn = now;
			}

			item.Save(username);
			return item;
		}
		public IList<RU_Team> GetAllTeamsThatCanMigrateToSeason(int previousSeasonID, int currentSeasonID, bool excludeThoseAlreadyInSeason)
		{
			IList<RU_Team> list = _humanResourceDB.RU_Teams.GetAllThatCanMigrateToSeason(previousSeasonID, currentSeasonID, excludeThoseAlreadyInSeason);
			return list;
		}

		public IList<SeasonsMap> GetTeamSeasonsMaps(int nFromSeasonID, int toSeasonID)
		{
			List<SeasonsMap> list = _humanResourceDB.SeasonsMaps.GetTeamSeasonsMaps(nFromSeasonID, toSeasonID).ToList();
			return list;
		}
		public IList<TeamsView> GetTeamSearchResult(TeamSearchInfo oTeamSearchInfo)
		{
			return _humanResourceDB.RU_Teams.FindByTeamInfo(oTeamSearchInfo);
		}

		#endregion //Teams

		#region TeamLocations

		public RU_TeamLocation GetOffice(int teamLocationID)
		{
			RU_TeamLocation item = _humanResourceDB.RU_TeamLocations.LoadByPrimaryKey(teamLocationID);
			return item;
		}
		public IList<RU_TeamLocation> GetOfficesUnderRegional(int regionID)
		{
			IList<RU_TeamLocation> list = _humanResourceDB.RU_TeamLocations.LoadAllOfficesUnderRegional(regionID);
			return list;
		}
		public IList<RU_TeamLocation> GetOfficesUnderNationalRegional(int nationalRegionID)
		{
			IList<RU_TeamLocation> list = _humanResourceDB.RU_TeamLocations.LoadAllOfficesUnderNationalRegional(nationalRegionID);
			return list;
		}
		public RU_TeamLocation SaveOffice(RU_TeamLocation teamLocObj, string username)
		{
			ValidateUsername(username);
			if (teamLocObj == null)
				throw new ArgumentNullException("teamLocObj");

			DateTime now = DateTime.Now;
			RU_TeamLocation item = RU_TeamLocation.LoadFrom(teamLocObj);
			item.IsActive = true;

			////this feels like a hack, but you do what ya gotta do
			//for some reason this isn't working like it does for RU_Recruit, so we do it like RU_User instead
			//item.CreatedBy = item.CreatedBy ?? "";
			//item.ModifiedBy = item.ModifiedBy ?? "";
			//don't set unless new
			if (item.IsNew)
			{
				item.CreatedBy = username;
				item.CreatedOn = now;
			}
			//don't set unless dirty
			if (item.IsDirty)
			{
				item.ModifiedBy = username;
				item.ModifiedOn = now;
			}

			item.Save(username);
			return item;
		}
		public IList<RU_TeamLocation> GetAllOfficesThatCanMigrateToSeason(int seasonID, bool excludeOfficesAlreadyInSeason)
		{
			IList<RU_TeamLocation> list = _humanResourceDB.RU_TeamLocations.GetAllThatCanMigrateToSeason(seasonID, excludeOfficesAlreadyInSeason);
			return list;
		}
		public void CopyOfficeStateMappings(int fromTeamLocationID, int toTeamLocationID, int seasonID)
		{
			_humanResourceDB.RU_TeamLocations.CopyOfficeStateMappings(fromTeamLocationID, toTeamLocationID, seasonID);
		}

		public RU_TeamLocation GetNextOffice(int previousSeasonTeamLocationID, int seasonID)
		{
			RU_TeamLocation item = _humanResourceDB.RU_TeamLocations.GetNextOffice(previousSeasonTeamLocationID, seasonID);
			return item;
		}

		public IList<RU_TeamLocation> GetOfficesActiveInSeason(int seasonID)
		{
			IList<RU_TeamLocation> list = _humanResourceDB.RU_TeamLocations.LoadAllActiveOfficesInSeason(seasonID);
			return list;
		}

		public IList<SeasonsMap> GetOfficeSeasonsMaps(int nFromSeasonID, int toSeasonID)
		{
			List<SeasonsMap> list = _humanResourceDB.SeasonsMaps.GetOfficeSeasonsMaps(nFromSeasonID, toSeasonID).ToList();
			return list;
		}

		public IList<AE_Office> GetAccountingOffices()
		{
			var oList = _accountingDB.AE_Offices.GetAllActive();
			return oList;
		}

		public IList<IV_Office> GetInventoryOffices()
		{
			var oList = _inventoryDB.IV_Offices.GetAllActive();
			return oList;
		}

		public IList<RU_TeamLocationView> GetOfficeSearchResults(OfficeSearchInfo oOfficeSearchInfo)
		{
			return _humanResourceDB.RU_TeamLocations.FindByOfficeInfo(oOfficeSearchInfo);
		}

		#endregion //TeamLocations

		#region Inventory -- Office Warehouse

		public IList<IV_Office> GetInventoryOfficeWarehousesActive()
		{
			IList<IV_Office> oList = _inventoryDB.IV_Offices.GetAllActive().ToList();
			return oList;
		}

		#endregion Inventory -- Office Warehouse

		#region Accounting -- Offices

		public IList<AE_Office> GetAccountingOfficesActive()
		{
			IList<AE_Office> oList = _accountingDB.AE_Offices.GetAllActive().ToList();
			return oList;
		}

		#endregion Accounting -- Offices

		#region Users

		public UserIDForCompanyIDResults GetUserIDForCompanyID(Guid queryKey, string companyID)
		{
			int? userID = _humanResourceDB.RU_Users.GetUserIDForCompanyID(companyID);
			var result = new UserIDForCompanyIDResults
			{
				QueryKey = queryKey,
				CompanyID = companyID,
				UserID = userID,
			};
			return result;
		}
		public UserIDForUsernameResults GetUserIDForUsername(Guid queryKey, string username)
		{
			int? userID = _humanResourceDB.RU_Users.GetUserIDForUsername(username);
			var result = new UserIDForUsernameResults
			{
				QueryKey = queryKey,
				Username = username,
				UserID = userID,
			};
			return result;
		}
		public UserInfo GetUserInfo(int userID)
		{
			var userInfo = new UserInfo
			{
				User = _humanResourceDB.RU_Users.LoadByPrimaryKey(userID),
				Notes = _licensingDB.LM_Notes.GetForUser(userID).ToList()
			};

			return userInfo;
		}
		public UserInfo SaveUserInfo(UserInfo userInfoObj, string username)
		{
			//save user
			int userID = SaveUser(userInfoObj.User, username).UserID;

			//save notes
			IList<LM_Note> notes = userInfoObj.Notes;
			if (notes != null)
			{

				foreach (LM_Note item in notes)
				{

					//load nota
					LM_Note note = LM_Note.LoadFrom(item);

					//set type and id
					note.NoteTypeID = (int)LM_NoteType.NoteTypeEnum.User;
					note.ForeignKeyID = userID;

					//save
					note.Save(username);
				}
			}

			return GetUserInfo(userID);
		}
		public RU_User GetUser(int userID)
		{
			RU_User item = _humanResourceDB.RU_Users.LoadByPrimaryKey(userID);
			return item;
		}
		private static RU_User SaveUser(RU_User userObj, string username)
		{
			ValidateUsername(username);
			if (userObj == null)
				throw new ArgumentNullException("userObj");

			DateTime now = DateTime.Now;
			RU_User item = RU_User.LoadFrom(userObj);
			item.IsActive = true;
			item.UserName = item.Email;

			//don't set unless new
			if (item.IsNew)
			{
				item.CreatedBy = username;
				item.CreatedOn = now;
			}

			//don't set unless dirty
			if (item.IsDirty)
			{
				item.ModifiedBy = username;
				item.ModifiedOn = now;
			}

			item.Save(username);
			return item;
		}
		public IList<RU_User> GetOwners()
		{
			return
				HumanResourceDataStoredProcedureManager.RU_UsersGetOwners()
				.ToCollection<RU_User, RU_UserCollection>();
		}
		public IList<AE_Dealer> GetDealers()
		{
			return
				_cmsDB.AE_Dealers.LoadAll();
		}

		public IList<RU_User> FindUsers(UserSearchInfo userInfo)
		{
			var oResult = _humanResourceDB.RU_Users.FindUsers(userInfo);
			return oResult;
		}

		public IList<RU_User> GetUsersWithExpiringRightToWork(DateTime beforeDate)
		{
			return _humanResourceDB.RU_Users.GetExpiringRightToWork(beforeDate);
		}

		#endregion //Users

		#region UserEmployeeTypes

		public IList<RU_UserEmployeeType> GetAllUserEmployeeTypes()
		{
			RU_UserEmployeeTypeCollection list = _humanResourceDB.RU_UserEmployeeTypes.LoadAll();
			return list;
		}
		public RU_UserEmployeeType GetUserEmployeeType(string userEmployeeTypeID)
		{
			RU_UserEmployeeType result = _humanResourceDB.RU_UserEmployeeTypes.LoadByPrimaryKey(userEmployeeTypeID);
			return result;
		}

		#endregion //UserEmployeeTypes

		#region UserHistory

		public UsersHistory GetUsersHistory(long lUsersHistoryID)
		{
			RU_UsersHistory item = _humanResourceDB.RU_UsersHistories.LoadByPrimaryKey(lUsersHistoryID);
			var result = new UsersHistory(_humanResourceDB, item);
			return result;
		}
		public IList<UsersHistoryVersion> GetHistoryForUser(int userID)
		{
			IList<RU_UsersHistory> list = _humanResourceDB.RU_UsersHistories.ByUser(userID);
			// ReSharper disable RedundantTypeArgumentsOfMethod
			return list.ConvertAll<RU_UsersHistory, UsersHistoryVersion>(item => new UsersHistoryVersion(_humanResourceDB, item));
			// ReSharper restore RedundantTypeArgumentsOfMethod 
		}

		#endregion //UserHistory

		#region RU_UserPhoto

		public RU_UserPhoto GetUserPhoto(int nUserID)
		{
			RU_UserPhoto oItem = _humanResourceDB.RU_UserPhotos.LoadByPrimaryKey(nUserID);
			return oItem;
		}

		public bool UploadUserPhoto(RU_UserPhoto oUserPhoto)
		{
			// Locals 
			bool bResult;

			try
			{
				RU_UserPhoto item = _humanResourceDB.RU_UserPhotos.LoadByPrimaryKey(oUserPhoto.UserID);
				if (item == null)
				{
					item = new RU_UserPhoto
					{
						UserID = oUserPhoto.UserID,
						CreatedByDate = DateTime.UtcNow,
						CreatedByID = "1"
					};
				}
				item.PhotoFile = oUserPhoto.PhotoFile;
				item.FileSize = oUserPhoto.FileSize;
				item.MimeType = oUserPhoto.MimeType;
				item.ModifiedByDate = DateTime.UtcNow;
				item.ModifiedByID = "1";
				item.Save();
				bResult = true;
			}
			catch
			{
				bResult = false;
			}

			// Return result
			return bResult;
		}

		#endregion //RU_UserPhoto

		#region UserTypes

		public RU_UserType GetUserType(short userTypeID)
		{
			RU_UserType item = _humanResourceDB.RU_UserTypes.LoadByPrimaryKey(userTypeID);
			return item;
		}
		public IList<RU_UserType> GetAllUserTypes()
		{
			IList<RU_UserType> list = _humanResourceDB.RU_UserTypes.LoadAllCached();
			return list;
		}
		public IList<RU_UserType> GetUserTypesThatMustManageTeam()
		{
			IList<RU_UserType> list = UserTypeLogic.AllThatMustManageTeam(_humanResourceDB);
			return list;
		}
		//public IList<RU_UserType> GetUserTypes(int roleLocationID)
		//{
		//    IList<RU_UserType> list = _recruitDB.RU_UserTypes.GetByRoleLocationID(roleLocationID);
		//    return list;
		//}

		#endregion //UserTypes

		#region ApplicationMenu

		//public IList<UI_ApplicationMenuView> GetCurrentApplicationMenuAD(int applicationID, string userNameAD, List<string> groupNamesAD)
		//{
		//    IList<UI_ApplicationMenuView> list = _interimDB.UI_ApplicationMenuViews.GetCurrentApplicationMenuAD(applicationID, userNameAD, groupNamesAD);
		//    return list;
		//}
		public IList<UI_ApplicationMenuView> GetCurrentApplicationMenu(int applicationID)
		{
			IList<UI_ApplicationMenuView> list = _cmsDB.UI_ApplicationMenuViews.GetCurrentApplicationMenu(applicationID);
			return list;
		}
		public IList<ActionPermission> GetCurrentApplicationActionPermissions(int applicationID)
		{
			IList<ActionPermission> list = _cmsDB.ActionPermissions.GetCurrentApplicationPermissions(applicationID);
			return list;
		}

		#endregion //ApplicationMenu

		#region ApplicationSettings

		public UI_UserSettingsContainer GetUserSettings(int applicationID, string username)
		{
			UI_UserSettingsContainer item = SosCrmDataContext.Instance.UI_UserSettingsContainers.GetUserSettingsForApp(applicationID, username);
			return item;
		}
		public UI_UserSettingsContainer SaveUserSettings(UI_UserSettingsContainer userSettingsObj)
		{
			if (userSettingsObj == null)
				throw new ArgumentNullException("userSettingsObj");

			UI_UserSettingsContainer item = UI_UserSettingsContainer.LoadFrom(userSettingsObj);
			item.Save();
			return item;
		}

		#endregion //ApplicationSettings

		#region TaxInfo

		public IList<WorkersComp> GetAllWorksComp()
		{
			return _accountingDB.WorkersComps.GetAllWorksComp();
		}
		public IList<FilingStatus> GetAllFedFilingStatus()
		{
			return _accountingDB.FilingStatuses.GetAllFedFilingStatus();
		}
		public IList<FilingStatus> GetAllEicFilingStatus()
		{
			return _accountingDB.FilingStatuses.GetAllEicFilingStatus();
		}
		public IList<FilingStatus> GetAllFilingStatusForState(string stateAB)
		{
			return _accountingDB.FilingStatuses.GetAllFilingStatusForState(stateAB);
		}

		#endregion //TaxInfo

		#region Terminations

		public RU_TerminationStatusCode.TerminationStatusCodeEnum WcfHackTerminationStatusCodeEnum()
		{
			return RU_TerminationStatusCode.TerminationStatusCodeEnum.Unsubmitted;
		}

		public IList<RU_TerminationType> GetAllTerminationTypes()
		{
			IList<RU_TerminationType> list = _humanResourceDB.RU_TerminationTypes.LoadAll();
			return list;
		}
		public IList<RU_TerminationCategory> GetTerminationCategoriesForType(int terminationTypeID)
		{
			IList<RU_TerminationCategory> list = _humanResourceDB.RU_TerminationCategories.GetAllForTerminationType(terminationTypeID);
			return list;
		}

		public IList<TerminationInfo> GetTerminationInfosForRecruit(int recruitID)
		{
			IList<RU_Termination> list = _humanResourceDB.RU_Terminations.GetAllForRecruit(recruitID);
			// ReSharper disable ConvertClosureToMethodGroup
			// ReSharper disable RedundantTypeArgumentsOfMethod
			return list.ConvertAll<RU_Termination, TerminationInfo>(item => GetTerminationInfo(item));
			// ReSharper restore RedundantTypeArgumentsOfMethod
			// ReSharper restore ConvertClosureToMethodGroup
		}
		private TerminationInfo GetTerminationInfo(RU_Termination termination)
		{
			int terminationID = termination.TerminationID;
			return new TerminationInfo
			{
				Termination = termination,
				TerminationStatusView = _humanResourceDB.RU_TerminationsWithStatusViews.LoadByPrimaryKey(terminationID),
				Notes = _humanResourceDB.RU_TerminationNotes.GetNotesForTermination(terminationID).ToList(),
			};
		}
		public TerminationInfo GetTerminationInfo(int terminationID)
		{
			return GetTerminationInfo(_humanResourceDB.RU_Terminations.LoadByPrimaryKey(terminationID));
		}
		public TerminationInfo SaveTerminationInfo(TerminationInfo terminationInfoObj, string username)
		{
			//save termination
			int terminationID = SaveTermination(terminationInfoObj.Termination, username).TerminationID;

			//save new status if one exists
			if (terminationInfoObj.NewTerminationStatus != null)
			{

				RU_TerminationStatus newStatus = RU_TerminationStatus.LoadFrom(terminationInfoObj.NewTerminationStatus);
				//newStatus.TerminationStatusCodeID = ;should be set in LoadFrom
				newStatus.TerminationID = terminationID;
				newStatus.CreatedByDate = DateTime.Now;
				newStatus.CreatedByID = username;
				newStatus.Save(username);
			}

			//save notes
			List<RU_TerminationNote> notes = terminationInfoObj.Notes;
			if (notes != null)
			{
				foreach (var item in notes)
				{

					//load nota
					RU_TerminationNote note = RU_TerminationNote.LoadFrom(item);
					//set id
					note.TerminationID = terminationID;
					//save
					note.Save(username);
				}
			}

			return GetTerminationInfo(terminationID);
		}

		public IList<RU_TerminationsWithStatusView> GetTerminationsPendingApproval()
		{
			return _humanResourceDB.RU_TerminationsWithStatusViews.GetTerminationsPendingApproval();
		}
		public IList<RU_TerminationsWithStatusView> GetTerminationsByRecruit(int recruitID)
		{
			return _humanResourceDB.RU_TerminationsWithStatusViews.GetTerminationsByRecruit(recruitID);
		}

		public RU_Termination GetTermination(int terminationID)
		{
			return _humanResourceDB.RU_Terminations.LoadByPrimaryKey(terminationID);
		}
		public RU_Termination SaveTermination(RU_Termination terminationObj, string username)
		{
			ValidateUsername(username);
			if (terminationObj == null)
				throw new ArgumentNullException("terminationObj");

			DateTime now = DateTime.Now;
			RU_Termination item = RU_Termination.LoadFrom(terminationObj);

			//don't set unless new
			if (item.IsNew)
			{
				item.CreatedByID = username;
				item.CreatedByDate = now;
			}

			item.Save(username);
			return item;
		}

		#endregion //Terminations

		#region Licensing

		public IList<LM_RequirementType> GetAllRequirementTypes()
		{
			IList<LM_RequirementType> list = _licensingDB.LM_RequirementTypes.LoadAll();
			return list;
		}
		public MetAndNeededRequirementSearchResults GetMetAndNeededRequirements(MetAndNeededRequirementSearchInfo searchInfo)
		{
			IList<MetAndNeededRequirement> list = _licensingDB.MetAndNeededRequirements.GetRequirementsMetAndNeeded(searchInfo);
			return new MetAndNeededRequirementSearchResults { SearchInfo = searchInfo, ResultsList = list, };
		}
		public IList<UserDocument> GetDocumentsForUser(string companyID)
		{
			return _humanResourceDB.UserDocuments.GetDocumentsByGPID(companyID);
		}
		//public ScannedDocument GetDocumentByID(int documentID)
		//{
		//    return _recruitDB.ScannedDocuments.LoadByPrimaryKey(documentID);
		//}

		#endregion //Licensing

		#region Private Methods

		private static void ValidateUsername(string username)
		{
			if (username == null)
				throw new ArgumentNullException("username");
			if (username.Trim().Length == 0)
				throw new Exception("username is empty");
		}

		#endregion //Private Methods
	}
}
