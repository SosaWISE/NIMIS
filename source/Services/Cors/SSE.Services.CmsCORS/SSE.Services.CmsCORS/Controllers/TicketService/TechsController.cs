using SOS.Data.SosCrm;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Models.TicketService;
using SOS.Lib.Core;
using SSE.Services.CmsCORS.Helpers;
using System;
using System.Collections.Generic;
using System.Web.Http;
using AuthActions = SOS.Data.AuthenticationControl.AC_Action.MetaData;
using AuthApplications = SOS.Data.AuthenticationControl.AC_Application.MetaData;

namespace SSE.Services.CmsCORS.Controllers.Ticket
{
	[RoutePrefix("TicketSrv")]
	public class TechsController : ApiController
	{
		private static string AppID = null;//AuthApplications.TicketServiceID;
		private static TicketService Service
		{
			get { return SosServiceEngine.Instance.FunctionalServices.Instance<TicketService>(); }
		}

		[HttpPost, Route("Techs")]
		public Result<TS_TechView> Create([FromBody]Tech tech)
		{
			return CORSSecurity.Authorize("Create Tech", AppID, null, user =>
			{
				return Service.SaveTech(tech, user.GPEmployeeID);
			});
		}
		[HttpGet, Route("Techs")]
		public Result<List<TS_TechView>> ReadAll()
		{
			return CORSSecurity.Authorize("Read All Techs", AppID, null, user =>
			{
				return Service.GetTechs();
			});
		}
		[HttpGet, Route("Techs/{id}")]
		public Result<TS_TechView> Read(int id)
		{
			return CORSSecurity.Authorize("Read Tech", AppID, null, user =>
			{
				return Service.GetTech(id);
			});
		}
		[HttpGet, Route("Techs/{id}/RecruitId")]
		public Result<TS_TechView> ReadByRecruitId(int id)
		{
			return CORSSecurity.Authorize("Read Tech ByRecruitId", AppID, null, user =>
			{
				return Service.GetTechByRecruitId(id);
			});
		}
		[HttpPost, Route("Techs/{id}")]
		public Result<TS_TechView> Update(int id, [FromBody]Tech tech)
		{
			return CORSSecurity.Authorize("Update Tech", AppID, null, user =>
			{
				tech.ID = id; //???
				return Service.SaveTech(tech, user.GPEmployeeID);
			});
		}


		[HttpGet, Route("Techs/{id}/Skills")]
		public Result<List<TechSkill>> ReadSkills(int id)
		{
			return CORSSecurity.Authorize("Read Tech Skills", AppID, null, user =>
			{
				return Service.GetTechSkills(id);
			});
		}
		[HttpPost, Route("Techs/{id}/Skills")]
		public Result<List<TechSkill>> UpdateSkills(int id, List<TechSkill> skills)
		{
			return CORSSecurity.Authorize("Update Tech Skills", AppID, null, user =>
			{
				return Service.SaveTechSkills(id, skills, user.GPEmployeeID);
			});
		}


		[HttpGet, Route("Techs/{id}/WeekDays")]
		public Result<List<TechWeekDay>> ReadWeekDays(int id)
		{
			return CORSSecurity.Authorize("Read Tech WeekDays", AppID, null, user =>
			{
				return Service.GetTechWeekDays(id);
			});
		}
		[HttpPost, Route("Techs/{id}/WeekDays")]
		public Result<List<TechWeekDay>> UpdateWeekDays(int id, List<TechWeekDay> weekDays)
		{
			return CORSSecurity.Authorize("Update Tech WeekDays", AppID, null, user =>
			{
				return Service.SaveTechWeekDays(id, weekDays, user.GPEmployeeID);
			});
		}


		[HttpGet, Route("Techs/{id}/Appointments")]
		public Result<List<TS_ServiceTicketStatusView>> ReadAppointments(int id, DateTime start, DateTime end)
		{
			return CORSSecurity.Authorize("Read Tech Appointments", AppID, null, user =>
			{
				// the body parser deserializes to UTC but the router does not
				start = start.ToUniversalTime();
				end = end.ToUniversalTime();
				return Service.GetTechAppointments(id, start, end);
			});
		}
	}
}
