using Nxs.Services.CorsConnext.Helpers;
using Nxs.Services.CorsConnext.Models;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models.HumanResource;
using SOS.Lib.Core;
using SOS.Services.Interfaces.Models.HumanResources;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Nxs.Services.CorsConnext.Controllers.HumanResourceSrvcs
{
	[RoutePrefix("HumanResourceSrvcs")]
	public class AccountListController : ApiController
	{
		[HttpGet, Route("AccountList/{userID}")]
		public Result<List<RuUsersConnextAccountList>> AccountList(int userID, DateTime? beginDate = null, DateTime? endDate = null, bool isActive = true)
		{
			#region Initialize

			const string METHOD_NAME = "Get AccountList";

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.Authorize(METHOD_NAME, null, user =>
			{
				#region Parameter Validation

				var argArray = new List<CORSArg>
				{
					new CORSArg(userID, (userID == 0), "<li>'UserID' must be passed.</li>")
				};
				Result<List<RuUsersConnextAccountList>> result;
				if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

				#endregion Parameter Validation

				#region TRY
				try
				{
					// ** Create a service object
					var service = SosServiceEngine.Instance.FunctionalServices.Instance<IHumanResourceService>();

					// ** Execute FOS Call
					var fnsResult = service.ConnextAccountList(userID,
						beginDate.HasValue ? beginDate.Value : DateTime.UtcNow,
						endDate.HasValue ? endDate.Value : DateTime.UtcNow, isActive);

					// ** Save result
					result.Code = fnsResult.Code;
					result.Message = fnsResult.Message;

					// Check for success
					if (result.Code != 0)
						return result;

					// ** Create result value package
					// ReSharper disable once RedundantCast
					var fnsResultList = (List<IFnsConnextAccountList>)fnsResult.GetTValue();
					var resultList = new List<RuUsersConnextAccountList>();
					foreach (IFnsConnextAccountList ranking in fnsResultList)
					{
						var item = new RuUsersConnextAccountList
						{
							UserID = ranking.UserID,
							CustomerID = ranking.CustomerID,
							CustomerFirstName = ranking.CustomerFirstName,
							CustomerMiddleName = ranking.CustomerMiddleName,
							CustomerLastName = ranking.CustomerLastName,
							ContractDate = ranking.ContractDate,
							CreditQuality = ranking.CreditQuality,
							ActivationFee = ranking.ActivationFee,
							ContractLength = ranking.ContractLength,
							ServiceType = ranking.ServiceType,
							MonthlyPayment = ranking.MonthlyPayment,
							PaymentMethod = ranking.PaymentMethod,
							TotalCommission = ranking.TotalCommission,
							IsActive = ranking.IsActive,
						};
						resultList.Add(item);
					}

					result.Value = resultList;

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
