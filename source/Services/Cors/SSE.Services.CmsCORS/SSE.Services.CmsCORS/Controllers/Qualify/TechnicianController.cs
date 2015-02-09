using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.HumanResource;
using SOS.Services.Interfaces.Models.HumanResources;
using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SSE.Services.CmsCORS.Controllers.Qualify
{
	[RoutePrefix("QualifySrv")]
	public class TechnicianController : ApiController
    {
		// GET QualifySrv/technician
		[HttpGet]
		[Route("Technician/{id}")]
		public CmsCORSResult<RuTechInfo> Get(string id)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Get Survey by id";
			var result = new CmsCORSResult<RuTechInfo>((int)CmsResultCodes.Initializing
				, string.Format("Initializing {0}.", METHOD_NAME));

			#endregion Initialize

			
			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
				, user =>
				{
					#region TRY

					try
					{
						// ** Create Service
						var hrSrv = SosServiceEngine.Instance.FunctionalServices.Instance<IHumanResourceService>();
						IFnsResult<IFnsTechInfo> fnsResult = hrSrv.TechInfoGet(id);

						// ** Save result
						result.Code = fnsResult.Code;
						result.SessionId = user.SessionID;
						result.Message = fnsResult.Message;

						// ** Get Values
						var fnsResultValue = (IFnsTechInfo) fnsResult.GetValue();
						if (result.Code == (int) CmsResultCodes.Success && fnsResultValue != null)
						{
							// ** Get the seasons
							var seasonsList = new List<IRuSeason>();
							foreach (var fnsSeason in fnsResultValue.Seasons)
							{
								seasonsList.Add(new RuSeason
								{
									SeasonID = fnsSeason.SeasonID,
									PreSeasonID = fnsSeason.PreSeasonID,
									SeasonName = fnsSeason.SeasonName,
									StartDate = fnsSeason.StartDate,
									EndDate = fnsSeason.EndDate,
									ShowInHiringManager = fnsSeason.ShowInHiringManager,
									IsCurrent = fnsSeason.IsCurrent,
									IsVisibleToRecruits = fnsSeason.IsVisibleToRecruits,
									IsInsideSales = fnsSeason.IsInsideSales,
									IsPreseason = fnsSeason.IsPreseason,
									IsSummer = fnsSeason.IsSummer,
									IsExtended = fnsSeason.IsExtended,
									IsYearRound = fnsSeason.IsYearRound,
									ExcellentCreditScoreThreshold = fnsSeason.ExcellentCreditScoreThreshold,
									PassCreditScoreThreshold = fnsSeason.PassCreditScoreThreshold,
									SubCreditScoreThreshold = fnsSeason.SubCreditScoreThreshold,
									IsActive = fnsSeason.IsActive,
									IsDeleted = fnsSeason.IsDeleted,
									CreatedByID = fnsSeason.CreatedByID,
									CreatedDate = fnsSeason.CreatedDate,
									ModifiedByID = fnsSeason.ModifiedByID,
									ModifiedDate = fnsSeason.ModifiedDate
								});
							}

							// ** Create result value package
							var resultValue = new RuTechInfo
							{
								UserID = fnsResultValue.UserID,
								CompanyID = fnsResultValue.CompanyID,
								TeamLocationId = fnsResultValue.TeamLocationId,
								DefaultOfficeName = fnsResultValue.DefaultOfficeName,
								FirstName = fnsResultValue.FirstName,
								LastName = fnsResultValue.LastName,
								CompanyName = fnsResultValue.CompanyName,
								UserName = fnsResultValue.UserName,
								BirthDate = fnsResultValue.BirthDate,
								HomeTown = fnsResultValue.HomeTown,
								Sex = fnsResultValue.Sex,
								ShirtSize = fnsResultValue.ShirtSize,
								HatSize = fnsResultValue.HatSize,
								PhoneHome = fnsResultValue.PhoneHome,
								PhoneCell = fnsResultValue.PhoneCell,
								Email = fnsResultValue.Email,
								SSN = fnsResultValue.SSN,
								ImagePath = AvatarImage.GetImagePath(fnsResultValue.UserID),
								Seasons = seasonsList
							};

							result.Value = resultValue;
						}
					}
						#endregion TRY

					#region CATCH

					catch (Exception ex)
					{
						result.Code = (int) CmsResultCodes.ExceptionThrown;
						result.Message = string.Format("The following exception was thrown from '{0}' method: {1}", METHOD_NAME,
							ex.Message);
					}

					#endregion CATCH

					#region Result

					return result;

					#endregion Result
				});
		}

		[HttpPost]
		[Route("Technician")]
		public CmsCORSResult<RuTechInfo> Post(RuTechInfo techInfo)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Save Tech to an MS Account.";

			#endregion Initialize

			
			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
			, user =>
			{
				#region Parameter Validation

				var argArray = new List<CORSArg>();
				if (techInfo == null)
				{
					argArray.Add(new CORSArg(null, (true), "<li>No values where passed.</li>"));
				}
				else
				{
					argArray.Add(new CORSArg(techInfo.CompanyID, (string.IsNullOrEmpty(techInfo.CompanyID)), "<li>'CompanyID' was not passed.</li>"));
					argArray.Add(new CORSArg(techInfo.MsAccountId, (techInfo.MsAccountId == 0), "<li>'MsAccountId' was not passed.</li>"));
				}

				CmsCORSResult<RuTechInfo> result;
				if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

				#endregion Parameter Validation

				#region TRY

				try
				{
					// ** Create Service
					var hrSrv = SosServiceEngine.Instance.FunctionalServices.Instance<IHumanResourceService>();
// ReSharper disable once PossibleNullReferenceException
					IFnsResult<IFnsTechInfo> fnsResult = hrSrv.TechInfoSave(techInfo.CompanyID, techInfo.MsAccountId, user.GPEmployeeID);

					// ** Save result
					result.Code = fnsResult.Code;
					result.SessionId = user.SessionID;
					result.Message = fnsResult.Message;

					// ** Get Values
					var fnsResultValue = (IFnsTechInfo) fnsResult.GetValue();
					if (result.Code == (int) CmsResultCodes.Success && fnsResultValue != null)
					{
						// ** Get the seasons
						var seasonsList = new List<IRuSeason>();
						foreach (var fnsSeason in fnsResultValue.Seasons)
						{
							seasonsList.Add(new RuSeason
							{
								SeasonID = fnsSeason.SeasonID,
								PreSeasonID = fnsSeason.PreSeasonID,
								SeasonName = fnsSeason.SeasonName,
								StartDate = fnsSeason.StartDate,
								EndDate = fnsSeason.EndDate,
								ShowInHiringManager = fnsSeason.ShowInHiringManager,
								IsCurrent = fnsSeason.IsCurrent,
								IsVisibleToRecruits = fnsSeason.IsVisibleToRecruits,
								IsInsideSales = fnsSeason.IsInsideSales,
								IsPreseason = fnsSeason.IsPreseason,
								IsSummer = fnsSeason.IsSummer,
								IsExtended = fnsSeason.IsExtended,
								IsYearRound = fnsSeason.IsYearRound,
								ExcellentCreditScoreThreshold = fnsSeason.ExcellentCreditScoreThreshold,
								PassCreditScoreThreshold = fnsSeason.PassCreditScoreThreshold,
								SubCreditScoreThreshold = fnsSeason.SubCreditScoreThreshold,
								IsActive = fnsSeason.IsActive,
								IsDeleted = fnsSeason.IsDeleted,
								CreatedByID = fnsSeason.CreatedByID,
								CreatedDate = fnsSeason.CreatedDate,
								ModifiedByID = fnsSeason.ModifiedByID,
								ModifiedDate = fnsSeason.ModifiedDate
							});
						}

						// ** Create result value package
						var resultValue = new RuTechInfo
						{
							UserID = fnsResultValue.UserID,
							CompanyID = fnsResultValue.CompanyID,
							TeamLocationId = fnsResultValue.TeamLocationId,
							DefaultOfficeName = fnsResultValue.DefaultOfficeName,
							FirstName = fnsResultValue.FirstName,
							LastName = fnsResultValue.LastName,
							CompanyName = fnsResultValue.CompanyName,
							UserName = fnsResultValue.UserName,
							BirthDate = fnsResultValue.BirthDate,
							HomeTown = fnsResultValue.HomeTown,
							Sex = fnsResultValue.Sex,
							ShirtSize = fnsResultValue.ShirtSize,
							HatSize = fnsResultValue.HatSize,
							PhoneHome = fnsResultValue.PhoneHome,
							PhoneCell = fnsResultValue.PhoneCell,
							Email = fnsResultValue.Email,
							SSN = fnsResultValue.SSN,
							ImagePath = AvatarImage.GetImagePath(fnsResultValue.UserID),
							Seasons = seasonsList
						};

						result.Value = resultValue;
					}
				}
					#endregion TRY

				#region CATCH

				catch (Exception ex)
				{
					result.Code = (int) CmsResultCodes.ExceptionThrown;
					result.Message = string.Format("The following exception was thrown from '{0}' method: {1}", METHOD_NAME,
						ex.Message);
				}

				#endregion CATCH

				#region Result

				return result;

				#endregion Result
			});
		}
    }
}
