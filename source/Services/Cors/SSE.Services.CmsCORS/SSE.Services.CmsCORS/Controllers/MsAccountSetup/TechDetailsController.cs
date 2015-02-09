using System;
using System.Collections.Generic;
using System.Web.Http;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Helper;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.HumanResource;
using SOS.Services.Interfaces.Models.HumanResources;
using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;

namespace SSE.Services.CmsCORS.Controllers.MsAccountSetup
{
	[RoutePrefix("MsAccountSetupSrv")]
	public class TechDetailsController : ApiController
    {

		[Route("TechDetails/{id}")]
		[HttpGet]
		public CmsCORSResult<RuSalesRepInfo> Get(long id)
        {
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Get Technician by id";
			var result = new CmsCORSResult<RuSalesRepInfo>((int)CmsResultCodes.Initializing
				, string.Format("Initializing {0}.", METHOD_NAME));
			result.Value = null;

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
			, user =>
			{
				#region Parameter Validation
				var argArray = new List<CORSArg>
					{
						new CORSArg(id, (id == 0), "<li>'AccountId' must be passed.</li>"),
					};
				if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

				#endregion Parameter Validation

				#region TRY

				try
				{
					// ** Create Service
					var msSrv = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();
					IFnsResult<IFnsSalesRepInfo> fnsResult = msSrv.GetTechDetails(id, user.GPEmployeeID);

					// ** Save result
					result.Code = fnsResult.Code;
					result.SessionId = user.SessionID;
					result.Message = fnsResult.Message;
					if (result.Code != (int)CmsResultCodes.Success)
					{
						return result;
					}

					// ** Get Values
					var fnsResultValue = (IFnsSalesRepInfo)fnsResult.GetValue();
					if (fnsResultValue == null || fnsResultValue.UserID == 0)
					{
						return result;
					}
					if (fnsResultValue.Seasons.Count == 0)
					{
						result.Code = (int)ErrorCodes.GeneralError;
						result.Message =
							string.Format(
								"The ID '{0}' is for {1}; however, this tech does not have a contract.  Please have him contact HR and fix the problem.", id, fnsResultValue.FirstName);
						return result;
					}

					if (result.Code == (int)CmsResultCodes.Success)
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
						var resultValue = new RuSalesRepInfo
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
					result.Code = (int)CmsResultCodes.ExceptionThrown;
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
