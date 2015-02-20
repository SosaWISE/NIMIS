using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models;
using SOS.Lib.Core;
using SSE.Services.CmsCORS.Helpers;
using System.Web.Http;
using AuthApplications = SOS.Data.AuthenticationControl.AC_Application.MetaData;

namespace SSE.Services.CmsCORS.Controllers.HumanResource
{
	[RoutePrefix("HumanResourceSrv")]
	public class TypesController : ApiController
	{
		internal static Result<T> getTypeList<T>(string name, System.Func<IHumanResourceService, IFnsResult<T>> fetchFunc)
		{
			return CORSSecurity.Authorize(name, AuthApplications.HiringManagerID, null, user =>
			{
				var service = SosServiceEngine.Instance.FunctionalServices.Instance<IHumanResourceService>();
				var fnsResult = fetchFunc(service);

				return new Result<T>(fnsResult.Code, fnsResult.Message, fnsResult.GetTValue());
			});
		}

		[HttpGet, Route("Payscales")]
		public Result<object> Payscales()
		{
			return getTypeList("Payscales", (service) => service.Payscales());
		}
		[HttpGet, Route("PhoneCellCarriers")]
		public Result<object> PhoneCellCarriers()
		{
			return getTypeList("PhoneCellCarriers", (service) => service.PhoneCellCarriers());
		}
		[HttpGet, Route("RoleLocations")]
		public Result<object> RoleLocations()
		{
			return getTypeList("RoleLocations", (service) => service.RoleLocations());
		}
		[HttpGet, Route("Schools")]
		public Result<object> Schools()
		{
			return getTypeList("Schools", (service) => service.Schools());
		}
		[HttpGet, Route("Seasons")]
		public Result<object> Seasons()
		{
			return getTypeList("Seasons", (service) => service.Seasons());
		}
		[HttpGet, Route("UserEmployeeTypes")]
		public Result<object> UserEmployeeTypes()
		{
			return getTypeList("UserEmployeeTypes", (service) => service.UserEmployeeTypes());
		}
		[HttpGet, Route("UserTypes")]
		public Result<object> UserTypes()
		{
			return getTypeList("UserTypes", (service) => service.UserTypes());
		}
		[HttpGet, Route("TeamLocations")]
		public Result<object> TeamLocations()
		{
			return getTypeList("TeamLocations", (service) => service.TeamLocations());
		}
	}
}
