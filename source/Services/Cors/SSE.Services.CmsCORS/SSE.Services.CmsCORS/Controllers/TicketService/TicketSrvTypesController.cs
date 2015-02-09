using SOS.FunctionalServices;
using SOS.Lib.Core;
using SSE.Services.CmsCORS.Helpers;
using System.Web.Http;
using AuthActions = SOS.Data.AuthenticationControl.AC_Action.MetaData;
using AuthApplications = SOS.Data.AuthenticationControl.AC_Application.MetaData;

namespace SSE.Services.CmsCORS.Controllers.Ticket
{
	[RoutePrefix("TicketSrv")]
	public class TicketSrvTypesController : ApiController
	{
		private static string AppID = null;//AuthApplications.TicketServiceID;
		private static TicketService Service
		{
			get { return SosServiceEngine.Instance.FunctionalServices.Instance<TicketService>(); }
		}
		private static Result<T> TypeList<T>(string name, System.Func<TicketService, Result<T>> fetchFunc)
		{
			return CORSSecurity.Authorize(name, AppID, null, user =>
			{
				return fetchFunc(Service);
			});
		}

		[HttpGet, Route("ServiceTypes")]
		public Result<object> ServiceTypes()
		{
			return TypeList("ServiceTypes", (service) => service.ServiceTypes());
		}
		[HttpGet, Route("Skills")]
		public Result<object> Skills()
		{
			return TypeList("Skills", (service) => service.Skills());
		}
		[HttpGet, Route("StatusCodes")]
		public Result<object> StatusCodes()
		{
			return TypeList("StatusCodes", (service) => service.StatusCodes());
		}
	}
}
