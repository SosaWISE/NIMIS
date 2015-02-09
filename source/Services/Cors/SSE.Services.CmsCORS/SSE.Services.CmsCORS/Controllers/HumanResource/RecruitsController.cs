using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models.HumanResource;
using SOS.FunctionalServices.Models.HumanResource;
using SOS.Lib.Core;
using SSE.Services.CmsCORS.Helpers;
using System.Web.Http;
using AuthActions = SOS.Data.AuthenticationControl.AC_Action.MetaData;
using AuthApplications = SOS.Data.AuthenticationControl.AC_Application.MetaData;

namespace SSE.Services.CmsCORS.Controllers.HumanResource
{
	[RoutePrefix("HumanResourceSrv")]
	public class RecruitsController : ApiController
	{
		[Route("Recruits/{id}")]
		[HttpGet]
		public Result<IFnsRuRecruit> GetRecruit(int id)
		{
			return CORSSecurity.Authorize("GetRecruit", AuthApplications.HiringManagerID, null, user =>
			{
				var result = new Result<IFnsRuRecruit>();

				var service = SosServiceEngine.Instance.FunctionalServices.Instance<IHumanResourceService>();
				var fnsResult = service.RecruitGet(id);
				return result.FromFnsResult(fnsResult);
			});
		}
		[Route("Recruits")]
		[HttpPost]
		public Result<IFnsRuRecruit> SaveRecruit(FnsRuRecruit fnsRecruit)
		{
			return CORSSecurity.Authorize("SaveRecruit", AuthApplications.HiringManagerID, AuthActions.Hr_User_EditID, user =>
			{
				var result = new Result<IFnsRuRecruit>();

				//@TODO: validate fnsRecruit

				var service = SosServiceEngine.Instance.FunctionalServices.Instance<IHumanResourceService>();
				var fnsResult = service.RecruitSave(fnsRecruit, user.GPEmployeeID);
				return result.FromFnsResult(fnsResult);
			});
		}
	}
}
