using SOS.Data.SosCrm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOS.FunctionalServices.Models.TicketService
{
	public class Tech
	{
		public long ID { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		public bool IsDeleted { get; set; }
		public int Version { get; set; }
		public int RecruitId { get; set; }
		public string StartLocation { get; set; }
		public double? StartLocLatitude { get; set; }
		public double? StartLocLongitude { get; set; }
		public int MaxRadius { get; set; }

		public Tech() { }

		//public Tech(TS_Tech item)
		//{
		//	this.ID = item.ID;
		//	this.CreatedOn = item.CreatedOn;
		//	this.CreatedBy = item.CreatedBy;
		//	this.ModifiedOn = item.ModifiedOn;
		//	this.ModifiedBy = item.ModifiedBy;
		//	this.IsDeleted = item.IsDeleted;
		//	this.Version = item.Version;
		//	this.RecruitId = item.RecruitId;
		//	this.StartLocation = item.StartLocation;
		//	this.StartLocLatitude = item.StartLocLatitude;
		//	this.StartLocLongitude = item.StartLocLongitude;
		//	this.MaxRadius = item.MaxRadius;
		//}

		public void ToDb(TS_Tech item)
		{
			if (item.ID != this.ID)
				throw new Exception("IDs don't match");
			SOS.Data.VersionException.CheckVersions(item.Version, this.Version);
			item.Version++; // increment version

			//item.ID = this.ID;
			// don't copy over record keeping fields
			//item.CreatedOn = this.CreatedOn;
			//item.CreatedBy = this.CreatedBy;
			//item.ModifiedOn = this.ModifiedOn;
			//item.ModifiedBy = this.ModifiedBy;
			item.IsDeleted = this.IsDeleted;

			item.RecruitId = this.RecruitId;
			item.StartLocation = this.StartLocation;
			item.StartLocLatitude = this.StartLocLatitude; //???
			item.StartLocLongitude = this.StartLocLongitude; //???
			item.MaxRadius = this.MaxRadius;
		}
	}
}
