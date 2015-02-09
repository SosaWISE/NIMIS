using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Helper;
using SOS.FunctionalServices.Helpers;
using SOS.FunctionalServices.Models.TicketService;
using SOS.Lib.Core;
using SOS.Lib.Util.Extensions;
using System.Linq;
using System;
using System.Collections.Generic;

namespace SOS.FunctionalServices
{
	public class TicketService
	{
		public Result<object> ServiceTypes()
		{
			return new Result<object>(value: SosCrmDataContext.Instance.TS_ServiceTypes.LoadAll());
		}
		public Result<object> Skills()
		{
			return new Result<object>(value: SosCrmDataContext.Instance.TS_Skills.LoadAll());
		}
		public Result<object> StatusCodes()
		{
			return new Result<object>(value: SosCrmDataContext.Instance.TS_StatusCodes.LoadAll());
		}


		public Result<object> GetServiceTicketsForAccount(int accountId)
		{
			return new Result<object>(value: SosCrmDataContext.Instance.TS_ServiceTicketStatusViews.ForAccountId(accountId));
		}

		public Result<List<TS_ServiceTicketStatusView>> GetServiceTickets(int? techId)
		{
			List<TS_ServiceTicketStatusView> list;
			if (techId.HasValue)
			{
				list = SosCrmDataContext.Instance.TS_ServiceTicketStatusViews.ForTech(techId.Value).ToList();
			}
			else
			{
				list = SosCrmDataContext.Instance.TS_ServiceTicketStatusViews.LoadAll().ToList();
			}
			return new Result<List<TS_ServiceTicketStatusView>>(value: list);
		}

		public Result<TS_ServiceTicketStatusView> GetServiceTicket(long id)
		{
			//var item = SosCrmDataContext.Instance.TS_ServiceTickets.LoadByPrimaryKey(id);
			//if (item == null)
			//{
			//	return new Result<ServiceTicket>((int)ErrorCodes.SqlItemNotFound, "ServiceTicket not found");
			//}
			var item = SosCrmDataContext.Instance.TS_ServiceTicketStatusViews.LoadByPrimaryKey(id);
			if (item == null)
			{
				return new Result<TS_ServiceTicketStatusView>((int)ErrorCodes.SqlItemNotFound, "ServiceTicket not found");
			}
			return new Result<TS_ServiceTicketStatusView>(value: item);
		}
		public Result<TS_ServiceTicketStatusView> SaveServiceTicket(ServiceTicket item, string user)
		{
			TS_ServiceTicket serviceTicket;
			if (item.ID > 0)
			{
				serviceTicket = SosCrmDataContext.Instance.TS_ServiceTickets.LoadByPrimaryKey(item.ID);
				if (serviceTicket == null)
				{
					return new Result<TS_ServiceTicketStatusView>((int)ErrorCodes.SqlItemNotFound, "ServiceTicket not found");
				}
			}
			else
			{
				serviceTicket = new TS_ServiceTicket()
				{
					CreatedBy = user,
					CreatedOn = DateTime.UtcNow,
				};
			}


			item.ToDb(serviceTicket);

			// get appointment
			TS_Appointment appt;
			if (serviceTicket.CurrentAppointmentId.HasValue)
			{
				if (serviceTicket.CurrentAppointmentId != item.AppointmentId)
				{
					return new Result<TS_ServiceTicketStatusView>(-1, "AppointmentId cannot be changed");
				}
				appt = serviceTicket.CurrentAppointment;
				item.ToDbAppt(appt);
			}
			else
			{
				// test for appointment data
				if (item.TechId.HasValue)
				{
					appt = new TS_Appointment()
					{
						CreatedBy = user,
						CreatedOn = DateTime.UtcNow,
					};
					item.ToDbAppt(appt);
				}
				else
				{
					appt = null;
				}
			}

			// check for overlapping tickets
			if (appt != null)
			{
				var overlappingServiceTicket = SosCrmDataContext.Instance.TS_ServiceTicketStatusViews.OverlappingAppointment(appt);
				if (overlappingServiceTicket != null)
				{
					return new Result<TS_ServiceTicketStatusView>(-1, string.Format("Appointment overlaps appointment for Ticket #{0}", overlappingServiceTicket.ID));
				}
			}

			// save service ticket and appointment
			DatabaseHelper.UseTransaction(SOS.Data.SubSonicConfigHelper.SOS_CRM_PROVIDER_NAME, () =>
			{
				serviceTicket.Save(user);

				if (appt != null)
				{
					// ensure service ticket id is set
					appt.ServiceTicketId = serviceTicket.ID;
					appt.Save(user);

					// ensure this appointment is set as the current appointment
					serviceTicket.CurrentAppointmentId = appt.ID;
					serviceTicket.Save(user);
				}

				return true;
			});
			return GetServiceTicket(serviceTicket.ID);
		}
		public Result<TS_ServiceTicketStatusView> CloseServiceTicket(long id, CloseTicket item, string user)
		{
			var serviceTicket = SosCrmDataContext.Instance.TS_ServiceTickets.LoadByPrimaryKey(id);
			if (serviceTicket == null)
			{
				return new Result<TS_ServiceTicketStatusView>((int)ErrorCodes.SqlItemNotFound, "ServiceTicket not found");
			}
			if (serviceTicket.CompletedOn.HasValue)
			{
				return new Result<TS_ServiceTicketStatusView>(-1, "Ticket is already closed");
			}

			item.ToDb(serviceTicket);
			serviceTicket.Save(user);

			return GetServiceTicket(serviceTicket.ID);
		}
		public Result<TS_ServiceTicketStatusView> CancelAppointment(long id, int serviceTicketVersion, string user)
		{
			var serviceTicket = SosCrmDataContext.Instance.TS_ServiceTickets.LoadByPrimaryKey(id);
			if (serviceTicket == null)
			{
				return new Result<TS_ServiceTicketStatusView>((int)ErrorCodes.SqlItemNotFound, "ServiceTicket not found");
			}
			SOS.Data.VersionException.CheckVersions(serviceTicket.Version, serviceTicketVersion);
			serviceTicket.Version++; // increment version

			var appt = serviceTicket.CurrentAppointment;
			if (appt == null)
			{
				return new Result<TS_ServiceTicketStatusView>((int)ErrorCodes.SqlItemNotFound, "No current appointment");
			}

			// save service ticket and appointment
			DatabaseHelper.UseTransaction(SOS.Data.SubSonicConfigHelper.SOS_CRM_PROVIDER_NAME, () =>
			{
				serviceTicket.CurrentAppointmentId = null;
				serviceTicket.Save(user);

				appt.IsDeleted = true;
				appt.Save(user);

				return true;
			});
			return GetServiceTicket(serviceTicket.ID);
		}

		public Result<List<int>> GetServiceTicketSkills(long id)
		{
			var item = SosCrmDataContext.Instance.TS_ServiceTickets.LoadByPrimaryKey(id);
			if (item == null)
			{
				return new Result<List<int>>((int)ErrorCodes.SqlItemNotFound, "ServiceTicket not found");
			}
			return new Result<List<int>>(value: item.TS_ServiceTicketSkills_MapsCol.ConvertAll(a => a.SkillId));
		}
		public Result<List<int>> SaveServiceTicketSkills(long id, List<int> skills, string user)
		{
			DatabaseHelper.UseTransaction(SOS.Data.SubSonicConfigHelper.SOS_CRM_PROVIDER_NAME, () =>
			{
				// delete current skills
				int t = SosCrmDataContext.Instance.TS_ServiceTicketSkills_Maps.DeleteAllForServiceTicket(id);
				// save new skills
				foreach (var skillId in skills)
				{
					var skillsMap = new TS_ServiceTicketSkills_Map
					{
						ServiceTicketId = id,
						SkillId = skillId,
					};
					skillsMap.Save(user);
				}
				return true;
			});
			return GetServiceTicketSkills(id);
		}

		/*
		public Result<Appointment> GetAppointment(long id)
		{
			var item = SosCrmDataContext.Instance.TS_Appointments.LoadByPrimaryKey(id);
			if (item == null)
			{
				return new Result<Appointment>((int)ErrorCodes.SqlItemNotFound, "Appointment not found");
			}
			return new Result<Appointment>(value: new Appointment(item));
		}
		public Result<Appointment> SaveAppointment(Appointment appt, string user)
		{
			TS_Appointment item;
			TS_ServiceTicket serviceTicket;
			if (appt.ID > 0)
			{
				item = SosCrmDataContext.Instance.TS_Appointments.LoadByPrimaryKey(appt.ID);
				if (item == null)
				{
					return new Result<Appointment>((int)ErrorCodes.SqlItemNotFound, "Appointment not found");
				}
				if (item.ServiceTicketId != appt.ServiceTicketId)
				{
					return new Result<Appointment>((int)ErrorCodes.SqlArgValidationFailed, "Appointments cannot be assigned to another ServiceTicket");
				}
				if (item.IsDeleted && !appt.IsDeleted)
				{
					return new Result<Appointment>((int)ErrorCodes.SqlArgValidationFailed, "Appointments cannot be undeleted");
				}
				//
				serviceTicket = item.ServiceTicket;
				if (serviceTicket.CurrentAppointmentId != item.ID)
				{
					// not the current appointment for the service ticket
					serviceTicket = null;
				}
			}
			else
			{
				item = new TS_Appointment()
				{
					CreatedBy = user,
					CreatedOn = DateTime.UtcNow,
					ModifiedBy = user,
					ModifiedOn = DateTime.UtcNow,

					ServiceTicketId = appt.ServiceTicketId,
				};
				//
				serviceTicket = SosCrmDataContext.Instance.TS_ServiceTickets.LoadByPrimaryKey(item.ServiceTicketId);
				if (serviceTicket == null)
				{
					return new Result<Appointment>((int)ErrorCodes.SqlItemNotFound, "ServiceTicket not found");
				}
				if (serviceTicket.CurrentAppointmentId.HasValue)
				{
					//@REVIEW: should an Over Due appointment be deleted before adding a new appointment???
					//var serviceTicketStatus = SosCrmDataContext.Instance.TS_ServiceTicketStatusViews.ByID(serviceTicket.ID);
					//if (serviceTicketStatus.StatusCodeId != (int)TS_StatusCode.Enum.Over_Due)
					//{
					return new Result<Appointment>((int)ErrorCodes.SqlArgValidationFailed,
						"The Appointment cannot be assigned to the ServiceTicket. It already has an Appointment.");
					//}
				}
			}

			appt.ToDb(item);

			DatabaseHelper.UseTransaction(SOS.Data.SubSonicConfigHelper.SOS_CRM_PROVIDER_NAME, () =>
			{
				item.Save(user);

				if (serviceTicket != null)
				{
					if (!item.IsDeleted)
					{
						// ensure this appointment is set as the current appointment
						serviceTicket.CurrentAppointmentId = item.ID;
					}
					else
					{
						// remove from current
						serviceTicket.CurrentAppointmentId = null;
					}
					serviceTicket.Save(user);
				}

				return true;
			});

			return GetAppointment(item.ID);
		}
		*/

		public Result<List<TS_TechView>> GetTechs()
		{
			var items = SosCrmDataContext.Instance.TS_TechViews.LoadAll().ToList();
			return new Result<List<TS_TechView>>(value: items);
		}
		public Result<TS_TechView> GetTech(long id)
		{
			var item = SosCrmDataContext.Instance.TS_TechViews.LoadByPrimaryKey(id);
			if (item == null)
			{
				return new Result<TS_TechView>((int)ErrorCodes.SqlItemNotFound, "Tech not found");
			}
			return new Result<TS_TechView>(value: item);
		}
		public Result<TS_TechView> GetTechByRecruitId(int recruitId)
		{
			var item = SosCrmDataContext.Instance.TS_TechViews.ByRecruitId(recruitId);
			if (item == null)
			{
				return new Result<TS_TechView>(value: null);
			}
			return new Result<TS_TechView>(value: item);
		}
		public Result<TS_TechView> SaveTech(Tech tech, string user)
		{
			TS_Tech item;
			if (tech.ID > 0)
			{
				item = SosCrmDataContext.Instance.TS_Teches.LoadByPrimaryKey(tech.ID);
				if (item == null)
				{
					return new Result<TS_TechView>((int)ErrorCodes.SqlItemNotFound, "Tech not found");
				}
			}
			else
			{
				item = new TS_Tech()
				{
					CreatedBy = user,
					CreatedOn = DateTime.UtcNow,
				};
			}

			tech.ToDb(item);
			item.Save(user);

			return GetTech(item.ID);
		}
		public Result<List<TechSkill>> GetTechSkills(int id)
		{
			var item = SosCrmDataContext.Instance.TS_Teches.LoadByPrimaryKey(id);
			if (item == null)
			{
				return new Result<List<TechSkill>>((int)ErrorCodes.SqlItemNotFound, "Tech not found");
			}
			return new Result<List<TechSkill>>(value: item.TS_TechSkills_MapsCol.ConvertAll(a =>
			{
				return new TechSkill
				{
					SkillId = a.SkillId,
					Other = a.Other,
				};
			}));
		}
		public Result<List<TechSkill>> SaveTechSkills(int id, List<TechSkill> skills, string user)
		{
			DatabaseHelper.UseTransaction(SOS.Data.SubSonicConfigHelper.SOS_CRM_PROVIDER_NAME, () =>
			{
				// delete current skills
				int t = SosCrmDataContext.Instance.TS_TechSkills_Maps.DeleteAllForTech(id);
				// save new skills
				foreach (var skill in skills)
				{
					var skillsMap = new TS_TechSkills_Map
					{
						TechId = id,
						SkillId = skill.SkillId,
						Other = skill.Other,
					};
					skillsMap.Save(user);
				}
				return true;
			});
			return GetTechSkills(id);
		}
		public Result<List<TechWeekDay>> GetTechWeekDays(int id)
		{
			var item = SosCrmDataContext.Instance.TS_Teches.LoadByPrimaryKey(id);
			if (item == null)
			{
				return new Result<List<TechWeekDay>>((int)ErrorCodes.SqlItemNotFound, "Tech not found");
			}
			return new Result<List<TechWeekDay>>(value: item.TS_TechWeekDaysCol.ConvertAll(a => new TechWeekDay(a)));
		}
		public Result<List<TechWeekDay>> SaveTechWeekDays(int id, List<TechWeekDay> weekDays, string user)
		{
			var item = SosCrmDataContext.Instance.TS_Teches.LoadByPrimaryKey(id);
			if (item == null)
			{
				return new Result<List<TechWeekDay>>((int)ErrorCodes.SqlItemNotFound, "Tech not found");
			}

			//@NOTE: tech week days are from 0 to 6
			// 0-Sunday, 1-Monday, 2-Tuesday, 3-Wednesday, 4-Thursday, 5-Friday, 6-Saturday

			// create lookups for current and new
			var currWeekDayDict = new Dictionary<int, TS_TechWeekDay>(7);
			foreach (var weekDay in item.TS_TechWeekDaysCol)
			{
				if (!currWeekDayDict.ContainsKey(weekDay.WeekDay))
				{
					currWeekDayDict.Add(weekDay.WeekDay, weekDay);
				}
			}
			var newWeekDayDict = new Dictionary<int, TechWeekDay>(7);
			foreach (var weekDay in weekDays)
			{
				if (!newWeekDayDict.ContainsKey(weekDay.WeekDay))
				{
					newWeekDayDict.Add(weekDay.WeekDay, weekDay);
				}
			}

			// save
			DatabaseHelper.UseTransaction(SOS.Data.SubSonicConfigHelper.SOS_CRM_PROVIDER_NAME, () =>
			{
				for (var day = 0; day < 7; day++)
				{
					TS_TechWeekDay weekDay;
					if (!currWeekDayDict.TryGetValue(day, out weekDay))
					{
						weekDay = new TS_TechWeekDay
						{
							CreatedBy = user,
							CreatedOn = DateTime.UtcNow,

							WeekDay = day,
							TechId = id,
						};
					}

					TechWeekDay newWeekDay;
					if (!newWeekDayDict.TryGetValue(day, out newWeekDay))
					{
						newWeekDay = new TechWeekDay
						{
							Version = weekDay.Version,
							WeekDay = day,
						};
					}

					newWeekDay.ToDb(weekDay);
					weekDay.Save(user);
				}

				return true;
			});

			return GetTechWeekDays(id);
		}


		public Result<List<TS_ServiceTicketStatusView>> GetTechAppointments(int id, DateTime start, DateTime end)
		{
			var items = SosCrmDataContext.Instance.TS_ServiceTicketStatusViews.AppointmentsForTechId(id, start, end).ToList();
			return new Result<List<TS_ServiceTicketStatusView>>(value: items);
		}
	}
}
