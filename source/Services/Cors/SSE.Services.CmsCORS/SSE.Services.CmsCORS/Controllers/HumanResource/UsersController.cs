using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models.Data;
using SOS.FunctionalServices.Contracts.Models.HumanResource;
using SOS.FunctionalServices.Models.HumanResource;
using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using AuthActions = SOS.Data.AuthenticationControl.AC_Action.MetaData;
using AuthApplications = SOS.Data.AuthenticationControl.AC_Application.MetaData;
using FnsRuUser = SOS.FunctionalServices.Models.FnsRuUser;
using System.Net.Http;
using SOS.Lib.Core;

namespace SSE.Services.CmsCORS.Controllers.HumanResource
{
	[RoutePrefix("HumanResourceSrv")]
	public class UsersController : ApiController
	{
		[Route("Users/{id}")]
		[HttpGet]
		public Result<IFnsRuUser> GetUser(int id)
		{
			return CORSSecurity.Authorize("GetUser", AuthApplications.HiringManagerID, null, user =>
			{
				var result = new Result<IFnsRuUser>();

				var service = SosServiceEngine.Instance.FunctionalServices.Instance<IHumanResourceService>();
				var fnsResult = service.UserGet(id);
				return result.FromFnsResult(fnsResult);
			});
		}
		[Route("Users")]
		[HttpPost]
		public Result<IFnsRuUser> SaveUser(FnsRuUser fnsUser)
		{
			return CORSSecurity.Authorize("SaveUser", AuthApplications.HiringManagerID, AuthActions.Hr_User_EditID, user =>
			{
				var result = new Result<IFnsRuUser>();

				//@TODO: validate fnsUser

				var service = SosServiceEngine.Instance.FunctionalServices.Instance<IHumanResourceService>();
				var fnsResult = service.UserSave(fnsUser, user.GPEmployeeID, user.UserID);
				return result.FromFnsResult(fnsResult);
			});
		}

		[Route("Users/{id}/Upload")]
		[HttpPost]
		public async Task<Result<bool>> Upload(int id)
		{
			SOS.Services.Interfaces.Models.SseCmsUser user = null;
			var result = (Result<bool>)CORSSecurity.Authorize("Upload", AuthApplications.HiringManagerID, AuthActions.Hr_User_EditID, loggedInUser =>
			{
				user = loggedInUser;
				return new Result<bool>();
			});
			if (result.Code != 0)
			{
				return result;
			}

			if (!Request.Content.IsMimeMultipartContent())
			{
				result.Code = -1;
				result.Message = "Expected multipart form";
				return result;
			}

			var provider = new MultipartMemoryStreamProvider();
			await Request.Content.ReadAsMultipartAsync(provider);
			foreach (var file in provider.Contents)
			{
				// save
				var buffer = await file.ReadAsByteArrayAsync();
				var service = SosServiceEngine.Instance.FunctionalServices.Instance<IHumanResourceService>();
				var fnsResult = service.UserPhotoSave(id, buffer, "image/jpeg", user.GPEmployeeID);
				// only use the first in the collection
				return result.FromFnsResult(fnsResult);
			}

			result.Code = -1;
			result.Message = "Nothing uploaded";
			return result;
		}
		[Route("Users/{id}/Photo")]
		[HttpGet]
		public HttpResponseMessage UserPhoto(int id)
		{
			var result = new HttpResponseMessage();

			//@TODO: get pixijs to work with adding authorization headers
			//var authResult = CORSSecurity.Authorize("UserPhoto", AuthApplications.HiringManagerID, null, user =>
			//{
			//	return new Result<object>();
			//});
			//if (authResult.Code != 0)
			//{
			//	result.StatusCode = System.Net.HttpStatusCode.Forbidden;
			//	return result;
			//}

			var service = SosServiceEngine.Instance.FunctionalServices.Instance<IHumanResourceService>();
			var fnsResult = service.UserPhotoGet(id);
			if (fnsResult.Code != 0)
			{
				result.StatusCode = System.Net.HttpStatusCode.NotFound;
				return result;
			}
			var photo = fnsResult.GetTValue();
			if (photo != null)
			{
				result.Content = new ByteArrayContent(photo.PhotoFile);
				result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(photo.MimeType);
			}

			return result;
		}

		[Route("Users/Search")]
		[HttpPost]
		public Result<object> UsersSearch(FnsUserSearchInfo userSearchInfo)
		{
			return CORSSecurity.Authorize("UsersSearch", AuthApplications.HiringManagerID, null, user =>
			{
				var result = new Result<object>();

				//@TODO: validate userSearchInfo

				var service = SosServiceEngine.Instance.FunctionalServices.Instance<IHumanResourceService>();
				var fnsResult = service.UsersSearch(userSearchInfo);
				return result.FromFnsResult(fnsResult);
			});
		}
		[Route("Users/{userid}/Recruits")]
		[HttpGet]
		public Result<List<IFnsRuRecruit>> UserRecruits(int userid)
		{
			return CORSSecurity.Authorize("UserRecruits", AuthApplications.HiringManagerID, null, user =>
			{
				var result = new Result<List<IFnsRuRecruit>>();

				var service = SosServiceEngine.Instance.FunctionalServices.Instance<IHumanResourceService>();
				var fnsResult = service.UserRecruits(userid);
				return result.FromFnsResult(fnsResult);
			});
		}
	}
}
