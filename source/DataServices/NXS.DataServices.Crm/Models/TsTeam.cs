using NXS.Data.Crm;
using System;

namespace NXS.DataServices.Crm.Models
{
	public class TsTeam
	{
		public int Version { get; set; }
		public int TeamId { get; set; }
		public long AddressId { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }

		public McAddress Address { get; set; }
		public RuTeam Team { get; set; }

		internal static TsTeam FromDb(TS_Team item, bool nullable = false)
		{
			if (item == null)
			{
				if (nullable)
					return null;
				else
					throw new Exception("team is null");
			}

			var result = new TsTeam();
			result.Version = item.Version;
			result.TeamId = item.TeamId;
			result.AddressId = item.AddressId;
			result.CreatedOn = item.CreatedOn;
			result.CreatedBy = item.CreatedBy;
			result.ModifiedOn = item.ModifiedOn;
			result.ModifiedBy = item.ModifiedBy;

			result.Address = McAddress.FromDb(item.Address, true);
			result.Team = RuTeam.FromDb(item.Team);

			return result;
		}

		internal void ToDb(TS_Team item)
		{
			if (item.TeamId != this.TeamId)
				throw new Exception("IDs don't match");
			NXS.Data.VersionException.CheckVersions(item.Version, this.Version);
			item.Version++; // increment version

			item.Version = this.Version;
			item.TeamId = this.TeamId;
			item.AddressId = this.AddressId;
			//item.ModifiedOn = this.ModifiedOn;
			//item.ModifiedBy = this.ModifiedBy;
			//item.CreatedOn = this.CreatedOn;
			//item.CreatedBy = this.CreatedBy;
		}
	}
}
