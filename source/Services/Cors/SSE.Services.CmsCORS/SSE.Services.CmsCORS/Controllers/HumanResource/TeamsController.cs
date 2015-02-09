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
using System.Net.Http;
using SOS.Lib.Core;

namespace SSE.Services.CmsCORS.Controllers.HumanResource
{
	[RoutePrefix("HumanResourceSrv")]
	public class TeamsController : ApiController
	{
		[Route("Teams/{id}")]
		[HttpGet]
		public Result<IFnsRuTeam> GetTeam(int id)
		{
			return CORSSecurity.Authorize("GetTeam", AuthApplications.HiringManagerID, null, user =>
			{
				var result = new Result<IFnsRuTeam>();

				var service = SosServiceEngine.Instance.FunctionalServices.Instance<IHumanResourceService>();
				var fnsResult = service.TeamGet(id);
				return result.FromFnsResult(fnsResult);
			});
		}
		[HttpGet, Route("Teams")]
		public Result<object> Teams()
		{
			return TypesController.getTypeList("Teams", (service) => service.Teams());
		}
		[HttpPost, Route("Teams")]
		public Result<IFnsRuTeam> SaveTeam(FnsRuTeam fnsTeam)
		{
			return CORSSecurity.Authorize("SaveTeam", AuthApplications.HiringManagerID, AuthActions.Hr_Team_EditID, user =>
			{
				var result = new Result<IFnsRuTeam>();

				//@TODO: validate fnsTeam

				var service = SosServiceEngine.Instance.FunctionalServices.Instance<IHumanResourceService>();
				var fnsResult = service.TeamSave(fnsTeam, user.GPEmployeeID);
				return result.FromFnsResult(fnsResult);
			});
		}
		[Route("Teams/Search")]
		[HttpPost]
		public Result<object> TeamsSearch(FnsTeamSearchInfo teamSearchInfo)
		{
			return CORSSecurity.Authorize("TeamsSearch", AuthApplications.HiringManagerID, null, user =>
			{
				var result = new Result<object>();

				//@TODO: validate teamSearchInfo

				var service = SosServiceEngine.Instance.FunctionalServices.Instance<IHumanResourceService>();
				var fnsResult = service.TeamsSearch(teamSearchInfo);
				return result.FromFnsResult(fnsResult);
			});
		}
	}
}
