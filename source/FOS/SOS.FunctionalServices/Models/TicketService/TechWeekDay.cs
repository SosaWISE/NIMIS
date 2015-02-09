using SOS.Data.SosCrm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOS.FunctionalServices.Models.TicketService
{
	public class TechWeekDay
	{
		// commented out fields don't need to sent to client

		//public int ID { get; set; }
		//public DateTime CreatedOn { get; set; }
		//public string CreatedBy { get; set; }
		//public DateTime ModifiedOn { get; set; }
		//public string ModifiedBy { get; set; }
		public int Version { get; set; }

		public int WeekDay { get; set; }
		//public int TechId { get; set; }
		public DateTime? StartTime { get; set; }
		public DateTime? EndTime { get; set; }
		public bool SameDayReserved { get; set; }

		public TechWeekDay() { }

		public TechWeekDay(TS_TechWeekDay item)
		{
			//this.ID = item.ID;
			//this.CreatedOn = item.CreatedOn;
			//this.CreatedBy = item.CreatedBy;
			//this.ModifiedOn = item.ModifiedOn;
			//this.ModifiedBy = item.ModifiedBy;
			this.Version = item.Version;

			this.WeekDay = item.WeekDay;
			//this.TechId = item.TechId;
			this.StartTime = item.StartTime;
			this.EndTime = item.EndTime;
			this.SameDayReserved = item.SameDayReserved;
		}

		public void ToDb(TS_TechWeekDay item)
		{
			if (item.WeekDay != this.WeekDay)
				throw new Exception("WeekDays don't match");
			SOS.Data.VersionException.CheckVersions(item.Version, this.Version);
			item.Version++; // increment version

			//if (item.ID != this.ID)
			//{
			//	throw new Exception("ID's don't match");
			//}
			//item.ID = this.ID;
			// don't copy over record keeping fields
			//item.CreatedOn = this.CreatedOn;
			//item.CreatedBy = this.CreatedBy;
			//item.ModifiedOn = this.ModifiedOn;
			//item.ModifiedBy = this.ModifiedBy;

			//item.WeekDay = this.WeekDay;
			//item.TechId = this.TechId; 
			item.StartTime = this.StartTime;
			item.EndTime = this.EndTime;
			item.SameDayReserved = this.SameDayReserved;
		}
	}
}
