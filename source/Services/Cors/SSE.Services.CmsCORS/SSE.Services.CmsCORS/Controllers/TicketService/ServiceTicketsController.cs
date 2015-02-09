using SOS.Data.SosCrm;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Models.TicketService;
using SOS.Lib.Core;
using SSE.Services.CmsCORS.Helpers;
using System.Collections.Generic;
using System.Web.Http;
using AuthActions = SOS.Data.AuthenticationControl.AC_Action.MetaData;
using AuthApplications = SOS.Data.AuthenticationControl.AC_Application.MetaData;

namespace SSE.Services.CmsCORS.Controllers.Ticket
{
	[RoutePrefix("TicketSrv")]
	public class ServiceTicketsController : ApiController
	{
		private static string AppID = null;//AuthApplications.TicketServiceID;
		private static TicketService Service
		{
			get { return SosServiceEngine.Instance.FunctionalServices.Instance<TicketService>(); }
		}

		[HttpPost, Route("ServiceTickets")]
		public Result<TS_ServiceTicketStatusView> Create([FromBody]ServiceTicket serviceTicket)
		{
			return CORSSecurity.Authorize("Create ServiceTicket", AppID, null, user =>
			{
				return Service.SaveServiceTicket(serviceTicket, user.GPEmployeeID);
			});
		}
		[HttpGet, Route("ServiceTickets")]
		public Result<List<TS_ServiceTicketStatusView>> ReadList(int? techId = null)
		{
			return CORSSecurity.Authorize("Read ServiceTickets", AppID, null, user =>
			{
				return Service.GetServiceTickets(techId);
			});
		}
		[HttpGet, Route("ServiceTickets/{id}")]
		public Result<TS_ServiceTicketStatusView> Read(long id = 0)
		{
			return CORSSecurity.Authorize("Read ServiceTicket", AppID, null, user =>
			{
				return Service.GetServiceTicket(id);
			});
		}
		[HttpPost, Route("ServiceTickets/{id}")]
		public Result<TS_ServiceTicketStatusView> Update(long id, [FromBody]ServiceTicket serviceTicket)
		{
			return CORSSecurity.Authorize("Update ServiceTicket", AppID, null, user =>
			{
				serviceTicket.ID = id; //???
				return Service.SaveServiceTicket(serviceTicket, user.GPEmployeeID);
			});
		}
		[HttpPost, Route("ServiceTickets/{id}/Close")]
		public Result<TS_ServiceTicketStatusView> Close(long id, [FromBody]CloseTicket closeTicket)
		{
			return CORSSecurity.Authorize("Close ServiceTicket", AppID, null, user =>
			{
				return Service.CloseServiceTicket(id, closeTicket, user.GPEmployeeID);
			});
		}
		[HttpDelete, Route("ServiceTickets/{id}/Appointment")]
		public Result<TS_ServiceTicketStatusView> Update(long id, int v)
		{
			return CORSSecurity.Authorize("Cancel ServiceTicket Appointment", AppID, null, user =>
			{
				return Service.CancelAppointment(id, serviceTicketVersion: v, user: user.GPEmployeeID);
			});
		}

		[HttpGet, Route("ServiceTickets/{id}/Skills")]
		public Result<List<int>> ReadSkills(int id)
		{
			return CORSSecurity.Authorize("Read ServiceTicket Skills", AppID, null, user =>
			{
				return Service.GetServiceTicketSkills(id);
			});
		}
		[HttpPost, Route("ServiceTickets/{id}/Skills")]
		public Result<List<int>> UpdateSkills(int id, List<int> skills)
		{
			return CORSSecurity.Authorize("Update ServiceTicket Skills", AppID, null, user =>
			{
				return Service.SaveServiceTicketSkills(id, skills, user.GPEmployeeID);
			});
		}
	}
}
