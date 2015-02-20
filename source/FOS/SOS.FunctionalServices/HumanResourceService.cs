/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 12/03/13
 * Time: 16:33
 * 
 * Description:  Service that will allow is to manage the Human Resources information.
 *********************************************************************************************************************/

using SOS.Data.HumanResource;
using SOS.Data.HumanResource.ControllerExtensions;
using SOS.Data.HumanResource.Models;
using SOS.Data.SosCrm;
using SOS.Data.SosCrm.ControllerExtensions;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Helper;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.HumanResource;
using SOS.FunctionalServices.Models;
using SOS.FunctionalServices.Models.HumanResource;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Linq;
using SOS.Lib.Util.Cryptography;
using IFnsRuUser = SOS.FunctionalServices.Contracts.Models.Data.IFnsRuUser;
using Bcrypt = BCrypt.Net.BCrypt;
using SOS.Lib.Util.Extensions;
using SOS.Data.AuthenticationControl;

namespace SOS.FunctionalServices
{
	[ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.NotAllowed)]
	public class HumanResourceService : IHumanResourceService
	{
		#region CRM Sales Info

		public IFnsResult<IFnsSalesRepInfo> SalesRepInfoGet(string companyId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "SalesRepInfoGet";
			var result = new FnsResult<IFnsSalesRepInfo>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing '{0}'", METHOD_NAME)
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				RU_User item = HumanResourceDataContext.Instance.RU_Users.LoadBySalesRepId(companyId);

				if (item != null && item.IsLoaded)
				{
					// ** Get the seasons for the rep
					RU_SeasonCollection seasons = HumanResourceDataContext.Instance.RU_Seasons.GetAllSeasonsByUserID(item.UserID);

					/** Init. */
					FnsSalesRepInfo resultValue;
					// ** Get Default TeamLocation
					if (seasons.Count > 0)
					{
						RU_TeamLocation ruTeamLocation =
							HumanResourceDataContext.Instance.RU_TeamLocations.GetBySeasonIdAndGPEmployeeID(seasons[0].SeasonID, companyId);

						// ** Create the result object.
						resultValue = new FnsSalesRepInfo(item, seasons, ruTeamLocation);
					}
					else
					{
						// ** Create the result object.
						resultValue = new FnsSalesRepInfo(item, seasons);
					}

					// ** Bind value to result transport.
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
				result = new FnsResult<IFnsSalesRepInfo>
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

		public IFnsResult<IFnsTechInfo> TechInfoGet(string companyId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "TechInfoGet";
			var result = new FnsResult<IFnsTechInfo>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing '{0}'", METHOD_NAME)
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				RU_User item = HumanResourceDataContext.Instance.RU_Users.LoadBySalesRepId(companyId);

				if (item != null && item.IsLoaded)
				{
					// ** Get the seasons for the rep
					RU_SeasonCollection seasons = HumanResourceDataContext.Instance.RU_Seasons.GetAllSeasonsByUserID(item.UserID);
					// ** Create the result object.
					var resultValue = new FnsTechInfo(item, seasons);
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
				result = new FnsResult<IFnsTechInfo>
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

		public IFnsResult<IFnsTechInfo> TechInfoSave(string companyId, long msAccountId, string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "TechInfoGet";
			var result = new FnsResult<IFnsTechInfo>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing '{0}'", METHOD_NAME)
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				RU_User item = HumanResourceDataContext.Instance.RU_Users.LoadByTechId(companyId);

				if (item != null && item.IsLoaded)
				{
					// ** Get the seasons for the rep
					RU_SeasonCollection seasons = HumanResourceDataContext.Instance.RU_Seasons.GetAllSeasonsByUserID(item.UserID);

					// ** Save to the MsAccount
					MS_AccountSalesInformation msAccount = SosCrmDataContext.Instance.MS_AccountSalesInformations.LoadByPrimaryKey(msAccountId);
					if (!msAccount.IsLoaded)
					{
						result.Code = (int)ErrorCodes.SqlItemNotFound;
						result.Message = string.Format("Ms Account was not found with that id: '{0}'.", msAccountId);
						return result;
					}

					msAccount.TechId = companyId;
					msAccount.Save(gpEmployeeId);

					// ** Create the result object.
					var resultValue = new FnsTechInfo(item, seasons);
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
				result = new FnsResult<IFnsTechInfo>
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

		public IFnsResult<IFnsSalesRepInfo> AccountSalesRep(long accountId)
		{
			// find the primary customer
			var cust = SosCrmDataContext.Instance.AE_Customers.GetByAccountID(accountId, "PRI");
			if (cust == null)
			{
				return new FnsResult<IFnsSalesRepInfo>
				{
					Code = (int)ErrorCodes.SqlItemNotFound,
					Message = "No primary customer found for account " + accountId,
				};
			}
			// load salesrep for customer lead
			return SalesRepInfoGet(cust.Lead.SalesRepId);
		}
		public IFnsResult<IFnsTechInfo> AccountTechnician(long accountId)
		{
			// find the primary customer
			var acct = SosCrmDataContext.Instance.MS_AccountSalesInformations.LoadByPrimaryKey(accountId);
			if (acct == null)
			{
				return new FnsResult<IFnsTechInfo>
				{
					Code = (int)ErrorCodes.SqlItemNotFound,
					Message = "No account " + accountId,
				};
			}
			// load tech for account
			return TechInfoGet(acct.TechId);
		}

		public IFnsResult<List<IFnsRuTeamLocation>> GetRuTeamLocationList()
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "RuTeamLocationsGet";
			var result = new FnsResult<List<IFnsRuTeamLocation>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing {0}", METHOD_NAME)
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{


				RU_TeamLocationCollection ruTeamLocationList = HumanResourceDataContext.Instance.RU_TeamLocations.GetRuTeamLocationList();
				var resultList = ruTeamLocationList.Select(item => new FnsRuTeamLocation(item)).Cast<IFnsRuTeamLocation>().ToList();


				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultList;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsRuTeamLocation>>
				{
					Code = (int)ErrorCodes.UnexpectedException
					,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<List<IFnsRuTechnician>> GetRuTechnicianList()
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "GetRuTechnicianListGet";
			var result = new FnsResult<List<IFnsRuTechnician>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing {0}", METHOD_NAME)
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{


				RU_TechniciansViewCollection ruTechnicianList = HumanResourceDataContext.Instance.RU_TechniciansViews.GetRuTechnicianList();
				var resultList = ruTechnicianList.Select(item => new FnsRuTechnician(item)).Cast<IFnsRuTechnician>().ToList();


				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultList;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsRuTechnician>>
				{
					Code = (int)ErrorCodes.UnexpectedException
					,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<IFnsRuTechnician> RuTechnicianGetByTechnicianId(string technicianId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "RuTechnicianGetByTechnicianId";
			var result = new FnsResult<IFnsRuTechnician>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing {0}", METHOD_NAME),
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				RU_TechniciansView ruTechnician = HumanResourceDataContext.Instance.RU_TechniciansViews.GetRuTechnicianByTechnicianId(technicianId);

				// ** Build result
				var resultValue = new FnsRuTechnician(ruTechnician);

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultValue;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsRuTechnician>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<IFnsRuTeam> TeamGet(int teamid)
		{
			var team = HumanResourceDataContext.Instance.RU_Teams.LoadByPrimaryKey(teamid);
			var result = new FnsResult<IFnsRuTeam>();
			if (team == null)
			{
				result.Code = (int)ErrorCodes.SqlItemNotFound;
				result.Message = "Team not found";
			}
			else
			{
				// set result value
				result.Value = new FnsRuTeam(team);
			}
			return result;
		}

		public IFnsResult<IFnsRuTeam> TeamSave(IFnsRuTeam fnsTeam, string gpEmployeeId)
		{
			RU_Team team;
			if (fnsTeam.TeamID > 0)
			{
				team = HumanResourceDataContext.Instance.RU_Teams.LoadByPrimaryKey(fnsTeam.TeamID);
				if (team == null)
				{
					return new FnsResult<IFnsRuTeam>
					{
						Code = (int)ErrorCodes.SqlItemNotFound,
						Message = "Team not found",
					};
				}
			}
			else
			{
				team = new RU_Team();
				team.CreatedBy = gpEmployeeId;
				team.CreatedOn = DateTime.UtcNow;
			}

			//
			// copy data
			//
			//team.TeamID = fnsTeam.TeamID;
			team.Description = fnsTeam.Description;
			team.CreatedFromTeamId = fnsTeam.CreatedFromTeamId;
			team.TeamLocationId = fnsTeam.TeamLocationId;
			team.RoleLocationId = fnsTeam.RoleLocationId;
			team.RegionalManagerRecruitId = fnsTeam.RegionalManagerRecruitId;
			team.IsActive = fnsTeam.IsActive;
			team.IsDeleted = fnsTeam.IsDeleted;
			//team.CreatedBy = fnsTeam.CreatedBy;
			//team.CreatedOn = fnsTeam.CreatedOn;
			//team.ModifiedBy = fnsTeam.ModifiedBy;
			//team.ModifiedOn = fnsTeam.ModifiedOn;

			team.Save(gpEmployeeId);

			return TeamGet(team.TeamID);
		}

		public IFnsResult<object> TeamsSearch(object teamSearchInfoObj)
		{
			var teamSearchInfo = (FnsTeamSearchInfo)teamSearchInfoObj;
			return new FnsResult<object>
			{
				Value = HumanResourceDataContext.Instance.RU_Teams.FindByTeamInfo(new TeamSearchInfo
				{
					SearchLike = teamSearchInfo.SearchLike,
					Top = teamSearchInfo.Top,
					TeamID = teamSearchInfo.TeamID,
					TeamName = teamSearchInfo.TeamName,
					OfficeName = teamSearchInfo.OfficeName,
					SeasonID = teamSearchInfo.SeasonID,
					SeasonName = teamSearchInfo.SeasonName,
					City = teamSearchInfo.City,
					StateAB = teamSearchInfo.StateAB,
					RoleLocationID = teamSearchInfo.RoleLocationID,
				}).ToList(),
			};
		}

		public IFnsResult<IFnsRuUser> UserGet(int userid)
		{
			var user = HumanResourceDataContext.Instance.RU_Users.LoadByPrimaryKey(userid);
			var result = new FnsResult<IFnsRuUser>();
			if (user == null)
			{
				result.Code = (int)ErrorCodes.SqlItemNotFound;
				result.Message = "User not found";
			}
			else
			{
				// decrypt ssn
				if (user.SSN != null)
				{
					var ssn = TripleDES.DecryptString(user.SSN, null);
					//@HACK: for unencrypted social insecurity numbers
					if (!ssn.StartsWith("Error:"))
					{
						user.SSN = ssn;
					}
				}
				// remove password
				user.Password = null;
				// set result value
				var acUser = SosAuthControlDataContext.Instance.AC_Users.ByHRUserId(user.UserID);
				result.Value = new FnsRuUser(user, acUser);
			}
			return result;
		}
		public IFnsResult<IFnsRuUser> UserSave(IFnsRuUser fnsUser, string gpEmployeeId, int userid)
		{
			RU_User user;
			if (fnsUser.UserID > 0)
			{
				user = HumanResourceDataContext.Instance.RU_Users.LoadByPrimaryKey(fnsUser.UserID);
				if (user == null)
				{
					return new FnsResult<IFnsRuUser>
					{
						Code = (int)ErrorCodes.SqlItemNotFound,
						Message = "User not found",
					};
				}
			}
			else
			{
				user = new RU_User();
				user.CreatedBy = gpEmployeeId;
				user.CreatedOn = DateTime.UtcNow;
			}

			//
			// copy data
			//
			//user.UserID = fnsUser.UserID;
			user.RecruitedById = fnsUser.RecruitedByID;
			user.GPEmployeeId = fnsUser.GPEmployeeID;
			user.UserEmployeeTypeId = fnsUser.UserEmployeeTypeId;
			user.PermanentAddressId = fnsUser.PermanentAddressID;
			user.SSN = (fnsUser.SSN == null) ? null : TripleDES.EncryptString(fnsUser.SSN, null); // encrypt ssn
			user.FirstName = fnsUser.FirstName;
			user.MiddleName = fnsUser.MiddleName;
			user.LastName = fnsUser.LastName;
			user.PreferredName = fnsUser.PreferredName;
			user.CompanyName = fnsUser.CompanyName;
			user.MaritalStatus = fnsUser.MaritalStatus;
			user.SpouseName = fnsUser.SpouseName;
			user.UserName = fnsUser.UserName;
			user.Password = ""; // this is no longer used
			user.BirthDate = fnsUser.BirthDate;
			user.HomeTown = fnsUser.HomeTown;
			user.BirthCity = fnsUser.BirthCity;
			user.BirthState = fnsUser.BirthState;
			user.BirthCountry = fnsUser.BirthCountry;
			user.Sex = fnsUser.Sex;
			user.ShirtSize = fnsUser.ShirtSize;
			user.HatSize = fnsUser.HatSize;
			user.DLNumber = fnsUser.DLNumber;
			user.DLState = fnsUser.DLState;
			user.DLCountry = fnsUser.DLCountry;
			user.DLExpiresOn = fnsUser.DLExpiresOn;
			user.Height = fnsUser.Height == null ? null : fnsUser.Height.ToString();
			user.Weight = fnsUser.Weight == null ? null : fnsUser.Weight.ToString();
			user.EyeColor = fnsUser.EyeColor;
			user.HairColor = fnsUser.HairColor;
			user.PhoneHome = fnsUser.PhoneHome;
			user.PhoneCell = fnsUser.PhoneCell;
			user.PhoneCellCarrierID = fnsUser.PhoneCellCarrierID;
			user.PhoneFax = fnsUser.PhoneFax;
			user.Email = fnsUser.Email;
			user.CorporateEmail = fnsUser.CorporateEmail;
			user.TreeLevel = fnsUser.TreeLevel;
			user.HasVerifiedAddress = fnsUser.HasVerifiedAddress;
			user.RightToWorkExpirationDate = fnsUser.RightToWorkExpirationDate;
			user.RightToWorkNotes = fnsUser.RightToWorkNotes;
			user.RightToWorkStatusID = fnsUser.RightToWorkStatusID;
			user.IsLocked = fnsUser.IsLocked;
			user.IsActive = fnsUser.IsActive;
			user.IsDeleted = fnsUser.IsDeleted;
			user.RecruitedDate = fnsUser.RecruitedDate;
			//user.CreatedBy = fnsUser.CreatedBy;
			//user.CreatedOn = fnsUser.CreatedOn;
			//user.ModifiedBy = fnsUser.ModifiedBy;
			//user.ModifiedOn = fnsUser.ModifiedOn;

			user.Save(gpEmployeeId);

			{
				var acUser = SosAuthControlDataContext.Instance.AC_Users.ByHRUserId(user.UserID);
				if (acUser == null)
				{
					// create one if non exist
					acUser = new AC_User()
					{
						HRUserId = user.UserID,
					};
				}

				// copy from fnsUser
				acUser.Username = fnsUser.UserName;
				if (!string.IsNullOrEmpty(fnsUser.Password))
				{
					// only set password if not null
					acUser.Password = Bcrypt.HashPassword(fnsUser.Password); // hash password
				}
				acUser.GPEmployeeID = fnsUser.GPEmployeeID;
				acUser.IsActive = fnsUser.IsActive;
				acUser.IsDeleted = fnsUser.IsDeleted;

				// save
				acUser.Save(userid);
			}

			// remove user from cache
			var authService = SosServiceEngine.Instance.FunctionalServices.Instance<AuthService>();
			authService.InvalidateCachedUser(user.UserName);

			return UserGet(user.UserID);
		}

		public IFnsResult<bool> UserPhotoSave(int userID, byte[] photoFile, string mimeType, string gpEmployeeId)
		{
			var result = new FnsResult<bool>();

			RU_UserPhoto userPhoto = HumanResourceDataContext.Instance.RU_UserPhotos.LoadByPrimaryKey(userID);
			if (userPhoto == null)
			{
				userPhoto = new RU_UserPhoto
				{
					UserID = userID,
				};
			}
			userPhoto.PhotoFile = photoFile;
			userPhoto.MimeType = mimeType;
			userPhoto.FileSize = photoFile.LongLength;
			userPhoto.Save(gpEmployeeId);

			result.Value = true;
			return result;
		}

		public IFnsResult<IFnsRuUserPhoto> UserPhotoGet(int userID)
		{
			var result = new FnsResult<IFnsRuUserPhoto>();
			RU_UserPhoto userPhoto = HumanResourceDataContext.Instance.RU_UserPhotos.LoadByPrimaryKey(userID);
			if (userPhoto == null)
			{
				result.Code = (int)ErrorCodes.SqlItemNotFound;
				result.Message = "No user photo";
			}
			else
			{
				result.Value = new FnsRuUserPhoto(userPhoto);
			}
			return result;
		}

		public IFnsResult<object> UsersSearch(object userSearchInfoObj)
		{
			var userSearchInfo = (FnsUserSearchInfo)userSearchInfoObj;
			var user = HumanResourceDataContext.Instance.RU_Users.FindUsers(
				SearchLike: userSearchInfo.SearchLike,
				Top: userSearchInfo.Top,
				UserID: userSearchInfo.UserID,
				RecruitID: userSearchInfo.RecruitID,
				SeasonID: userSearchInfo.SeasonID,
				UserTypeID: userSearchInfo.UserTypeID,
				FirstName: userSearchInfo.FirstName,
				LastName: userSearchInfo.LastName,
				CompanyID: userSearchInfo.CompanyID,
				SSN: (userSearchInfo.SSN == null) ? null : TripleDES.EncryptString(userSearchInfo.SSN, null),
				CellPhone: userSearchInfo.CellPhone,
				HomePhone: userSearchInfo.HomePhone,
				Email: userSearchInfo.Email,
				UserName: userSearchInfo.UserName,
				UserEmployeeTypeID: userSearchInfo.UserEmployeeTypeId
			);
			var result = new FnsResult<object> { Value = user, };
			if (user == null)
			{
				result.Code = (int)ErrorCodes.SqlItemNotFound;
				result.Message = "User not found";
			}
			return result;
		}

		public IFnsResult<List<IFnsRuRecruit>> UserRecruits(int userid)
		{
			var recruits = HumanResourceDataContext.Instance.RU_Recruits.ForUser(userid);
			var result = new FnsResult<List<IFnsRuRecruit>>();
			// set result value
			result.Value = recruits.ConvertAll(r => (IFnsRuRecruit)new FnsRuRecruit(r));
			return result;
		}

		public IFnsResult<IFnsRuRecruit> RecruitGet(int recruitid)
		{
			var user = HumanResourceDataContext.Instance.RU_Recruits.LoadByPrimaryKey(recruitid);
			var result = new FnsResult<IFnsRuRecruit>();
			if (user == null)
			{
				result.Code = (int)ErrorCodes.SqlItemNotFound;
				result.Message = "Recruit not found";
			}
			else
			{
				// set result value
				result.Value = new FnsRuRecruit(user);
			}
			return result;
		}
		public IFnsResult<IFnsRuRecruit> RecruitSave(IFnsRuRecruit fnsRecruit, string gpEmployeeId)
		{
			RU_Recruit recruit;
			if (fnsRecruit.RecruitID > 0)
			{
				recruit = HumanResourceDataContext.Instance.RU_Recruits.LoadByPrimaryKey(fnsRecruit.RecruitID);
				if (recruit == null)
				{
					return new FnsResult<IFnsRuRecruit>
					{
						Code = (int)ErrorCodes.SqlItemNotFound,
						Message = "Recruit not found",
					};
				}
			}
			else
			{
				recruit = new RU_Recruit();
				recruit.CreatedBy = gpEmployeeId;
				recruit.CreatedOn = DateTime.UtcNow;
			}

			//
			// copy data
			//
			//recruit.RecruitID = fnsRecruit.RecruitID;
			recruit.UserId = fnsRecruit.UserID;
			recruit.SeasonId = fnsRecruit.SeasonID;
			recruit.UserTypeId = fnsRecruit.UserTypeId;
			recruit.ReportsToId = fnsRecruit.ReportsToID;
			recruit.TeamId = fnsRecruit.TeamID;
			recruit.PayScaleId = fnsRecruit.PayScaleID;
			recruit.PreviousSummer = fnsRecruit.PreviousSummer;
			recruit.SignatureDate = fnsRecruit.SignatureDate;
			recruit.ManagerApprovalDate = fnsRecruit.ManagerApprovalDate;
			recruit.OwnerApprovalDate = fnsRecruit.OwnerApprovalDate;
			recruit.OwnerApprovalId = fnsRecruit.OwnerApprovalId;
			recruit.SchoolId = fnsRecruit.SchoolId;
			recruit.DriversLicenseStatusID = fnsRecruit.DriversLicenseStatusID;
			recruit.DriversLicenseNotes = fnsRecruit.DriversLicenseNotes;
			recruit.I9StatusID = fnsRecruit.I9StatusID;
			recruit.I9Notes = fnsRecruit.I9Notes;
			recruit.W9StatusID = fnsRecruit.W9StatusID;
			recruit.W9Notes = fnsRecruit.W9Notes;
			recruit.W4StatusID = fnsRecruit.W4StatusID;
			recruit.W4Notes = fnsRecruit.W4Notes;
			recruit.EmergencyName = fnsRecruit.EmergencyName;
			recruit.EmergencyRelationship = fnsRecruit.EmergencyRelationship;
			recruit.EmergencyPhone = fnsRecruit.EmergencyPhone;
			recruit.CountryId = fnsRecruit.CountryId;
			recruit.StreetAddress = fnsRecruit.StreetAddress;
			recruit.City = fnsRecruit.City;
			recruit.StateId = fnsRecruit.StateId;
			recruit.PostalCode = fnsRecruit.PostalCode;
			recruit.RecruitCohabbitTypeId = fnsRecruit.RecruitCohabbitTypeId;
			recruit.ShackingUpId = fnsRecruit.ShackingUpId;
			recruit.Rent = fnsRecruit.Rent;
			recruit.Pet = fnsRecruit.Pet;
			recruit.Utilities = fnsRecruit.Utilities;
			recruit.Fuel = fnsRecruit.Fuel;
			recruit.EIN = fnsRecruit.EIN;
			recruit.FedFilingStatus = fnsRecruit.FedFilingStatus;
			recruit.SUTA = fnsRecruit.SUTA;
			recruit.EICFilingStatus = fnsRecruit.EICFilingStatus;
			recruit.WorkersComp = fnsRecruit.WorkersComp;
			recruit.StateFilingStatus = fnsRecruit.StateFilingStatus;
			recruit.TaxWitholdingState = fnsRecruit.TaxWitholdingState;
			recruit.GPDependents = fnsRecruit.GPDependents;
			recruit.DealerId = fnsRecruit.DealerId;
			recruit.IsActive = fnsRecruit.IsActive;
			recruit.IsDeleted = fnsRecruit.IsDeleted;
			//recruit.CreatedBy = fnsRecruit.CreatedBy;
			//recruit.CreatedOn = fnsRecruit.CreatedOn;
			//recruit.ModifiedBy = fnsRecruit.ModifiedBy;
			//recruit.ModifiedOn = fnsRecruit.ModifiedOn;

			recruit.Save(gpEmployeeId);

			return RecruitGet(recruit.RecruitID);
		}

		public IFnsResult<object> UserEmployeeTypes()
		{
			var list = HumanResourceDataContext.Instance.RU_UserEmployeeTypes.LoadAll();
			var result = new FnsResult<object>();
			result.Value = list;
			return result;
		}

		public IFnsResult<object> PhoneCellCarriers()
		{
			var list = HumanResourceDataContext.Instance.RU_PhoneCellCarriers.LoadAll();
			var result = new FnsResult<object>();
			result.Value = list;
			return result;
		}

		public IFnsResult<object> Seasons()
		{
			var list = HumanResourceDataContext.Instance.RU_Seasons.LoadAll();
			var result = new FnsResult<object>();
			result.Value = list;
			return result;
		}

		public IFnsResult<object> Payscales()
		{
			var list = HumanResourceDataContext.Instance.RU_Payscales.LoadAll();
			var result = new FnsResult<object>();
			result.Value = list;
			return result;
		}

		public IFnsResult<object> RoleLocations()
		{
			var list = HumanResourceDataContext.Instance.RU_RoleLocations.LoadAll();
			var result = new FnsResult<object>();
			result.Value = list;
			return result;
		}

		public IFnsResult<object> Schools()
		{
			var list = HumanResourceDataContext.Instance.RU_Schools.LoadAll();
			var result = new FnsResult<object>();
			result.Value = list;
			return result;
		}

		public IFnsResult<object> UserTypes()
		{
			var list = HumanResourceDataContext.Instance.RU_UserTypes.LoadAll();
			var result = new FnsResult<object>();
			result.Value = list;
			return result;
		}

		public IFnsResult<object> Teams()
		{
			var list = HumanResourceDataContext.Instance.RU_Teams.LoadAll();
			var result = new FnsResult<object>();
			result.Value = list;
			return result;
		}

		public IFnsResult<object> TeamLocations()
		{
			var list = HumanResourceDataContext.Instance.RU_TeamLocations.LoadAll();
			var result = new FnsResult<object>();
			result.Value = list;
			return result;
		}
		#endregion CRM Sales Info

		#region Connext Sales Info
		public IFnsResult<List<IFnsConnextAccountList>> ConnextAccountList(int userId, DateTime beginDate, DateTime endDate, bool isActive)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "ConnextAccountList";
			var result = new FnsResult<List<IFnsConnextAccountList>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing {0}", METHOD_NAME),
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				var accountCollection =
					HumanResourceDataContext.Instance.RU_UsersAccountListConnextViews
						.LoadCollection(
							HumanResourceDataStoredProcedureManager.RU_UsersAccountListConnextGetByUserID(userId, beginDate, endDate, isActive));

				// ** Build list
				var rankingList = new List<IFnsConnextAccountList>();
				foreach (var account in accountCollection)
				{
					rankingList.Add(new FnsConnextAccountList(account));
				}


				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = rankingList;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsConnextAccountList>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<IFnsConnextCombinedMonthlySalesDetails> ConnextCombinedMonthlySalesDetails(int userID, int salesMonth, int salesYear)
		{
			#region INITIALIZATION
			// ** Initialize 
			const string METHOD_NAME = "ConnextCombinedMonthlySalesDetails";
			var result = new FnsResult<IFnsConnextCombinedMonthlySalesDetails>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing {0}", METHOD_NAME),
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				var combinedresults = new FnsConnextCombinedMonthlySalesDetails();
				//                combinedresults.OfficeStats = new List<RuUsersGetDetailedStatisticsConnext>();
				//                combinedresults.RepStats = new List<RuUsersGetDetailedStatisticsConnext>();

				// Office Stats
				var officeCollection =
					HumanResourceDataContext.Instance.RU_UsersGetDetailedStatisticsConnextViews
						.LoadCollection(
							HumanResourceDataStoredProcedureManager.RU_UsersSalesDetailedStatisticsConnextGetByUserID(userID, salesMonth, salesYear, true));

				// ** Build list
				//                var salesList = new List<IFnsConnextMonthlySalesDetails>();
				foreach (var office in officeCollection)
				{
					combinedresults.OfficeStats.Add(new FnsConnextMonthlySalesDetails(office));
				}

				// Salesrep Stats
				var salesrepCollection =
					HumanResourceDataContext.Instance.RU_UsersGetDetailedStatisticsConnextViews
						.LoadCollection(
							HumanResourceDataStoredProcedureManager.RU_UsersSalesDetailedStatisticsConnextGetByUserID(userID, salesMonth, salesYear, false));

				// ** Build list
				//                var salesList = new List<IFnsConnextMonthlySalesDetails>();
				foreach (var salesrep in salesrepCollection)
				{
					combinedresults.OfficeStats.Add(new FnsConnextMonthlySalesDetails(salesrep));
				}

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";

				result.Value = combinedresults;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsConnextCombinedMonthlySalesDetails>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<IFnsConnextCustomerInfo> ConnextCustomerInfo(long customerMasterFileID)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "ConnextCustomerInfo";
			var result = new FnsResult<IFnsConnextCustomerInfo>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing {0}", METHOD_NAME),
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				FnsConnextCustomerInfo resultValue;
				resultValue = new FnsConnextCustomerInfo(HumanResourceDataContext.Instance.AE_CustomersGetCustomerInfoConnextViews.LoadSingle(
					HumanResourceDataStoredProcedureManager.AE_CustomersGetCustomerInfoConnext(customerMasterFileID)));

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultValue;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsConnextCustomerInfo>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<List<IFnsConnextSalesRanking>> ConnextSalesRanking(int userId, string resultType, string rankingGroup, string rankingPeriod, int rows)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "ConnextSalesRanking";
			var result = new FnsResult<List<IFnsConnextSalesRanking>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing {0}", METHOD_NAME),
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				var rankingCollection =
					HumanResourceDataContext.Instance.RU_UsersSalesRankingConnextViews
						.LoadCollection(
							HumanResourceDataStoredProcedureManager.RU_UsersSalesRankingConnextGetbyUserID(userId,
								resultType, rankingGroup, rankingPeriod, rows));

				// ** Build list
				var rankingList = new List<IFnsConnextSalesRanking>();
				foreach (var ranking in rankingCollection)
				{
					rankingList.Add(new FnsConnextSalesRanking(ranking));
				}


				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = rankingList;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsConnextSalesRanking>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<IFnsConnextSalesRepExtendedInfo> ConnextSalesRepInfo(int userId, bool isExtended)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "ConnextSalesRepInfo";
			var result = new FnsResult<IFnsConnextSalesRepExtendedInfo>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing {0}", METHOD_NAME),
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				FnsConnextSalesRepExtendedInfo resultValue;
				if (isExtended == false)
				{
					resultValue = new FnsConnextSalesRepExtendedInfo(HumanResourceDataContext.Instance.RU_UsersSalesInfoConnextViews.LoadSingle(
							HumanResourceDataStoredProcedureManager.RU_UsersSalesInfoConnextGetByUserID(userId)));
				}
				else
				{
					resultValue = new FnsConnextSalesRepExtendedInfo(HumanResourceDataContext.Instance.RU_UsersSalesInfoExtendedConnextViews.LoadSingle(
							HumanResourceDataStoredProcedureManager.RU_UsersSalesInfoExtendedConnextGetByUserID(userId)));
				}

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultValue;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsConnextSalesRepExtendedInfo>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<IFnsSalesSalespersonMonthlyCommissions> SalespersonMonthlyCommissions(int userID, int salesMonth, int salesYear)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "SalesMonthlyCommissions";
			var result = new FnsResult<IFnsSalesSalespersonMonthlyCommissions>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing {0}", METHOD_NAME),
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				FnsSalesSalespersonMonthlyCommissions resultValue;
				resultValue = new FnsSalesSalespersonMonthlyCommissions(HumanResourceDataContext.Instance.SAE_SalesSalespersonMonthlyCommissionsViews.LoadSingle(
					HumanResourceDataStoredProcedureManager.SAE_SalesSalespersonMonthlyCommissions(userID, salesMonth, salesYear)));

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultValue;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsSalesSalespersonMonthlyCommissions>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}
		public IFnsResult<List<IFnsSalesSalespersonMonthlyHolds>> SalespersonMonthlyHolds(int userID, int salesMonth, int salesYear)
		{
			#region INITIALIZATION
			// ** Initialize 
			const string METHOD_NAME = "SalespersonMonthlyHolds";
			var result = new FnsResult<List<IFnsSalesSalespersonMonthlyHolds>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing {0}", METHOD_NAME),
			};
			#endregion INITIALIZATION

			#region TRY
			try
			{
				var holdCollection =
					HumanResourceDataContext.Instance.SAE_SalesSalespersonMonthlyHoldsViews
						.LoadCollection(
							HumanResourceDataStoredProcedureManager.SAE_SalesSalespersonMonthlyHolds(userID, salesMonth, salesYear));

				// ** Build list
				var holdList = new List<IFnsSalesSalespersonMonthlyHolds>();
				foreach (var item in holdCollection)
				{
					holdList.Add(new FnsSalesSalespersonMonthlyHolds(item));
				}


				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = holdList;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsSalesSalespersonMonthlyHolds>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}
		public IFnsResult<IFnsSalesSalespersonMonthlyEarningsSummary> SalespersonMonthlyEarningsSummary(int userID, int salesMonth, int salesYear)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "SalesMonthlyEarningsSummary";
			var result = new FnsResult<IFnsSalesSalespersonMonthlyEarningsSummary>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing {0}", METHOD_NAME),
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				FnsSalesSalespersonMonthlyEarningsSummary resultValue;
				resultValue = new FnsSalesSalespersonMonthlyEarningsSummary(HumanResourceDataContext.Instance.SAE_SalesSalespersonMonthlyEarningsViews.LoadSingle(
					HumanResourceDataStoredProcedureManager.SAE_SalesSalespersonMonthlyEarnings(userID, salesMonth, salesYear)));

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultValue;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsSalesSalespersonMonthlyEarningsSummary>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		#endregion Connext Sales Info
	}
}
