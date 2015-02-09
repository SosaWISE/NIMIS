using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOS.Services.Interfaces.Models.CmsModels
{
	#region McPoliticalTimeZone
	public interface IMcPoliticalTimeZone
	{
		int TimeZoneID { get; set; }
		string TimeZoneName { get; set; }
		string TimeZoneAB { get; set; }
		string CentralTime { get; set; }
		int HourDifference { get; set; }
		bool IsActive { get; set; }
		bool IsDeleted { get; set; }
	}

	public class McPoliticalTimeZone : IMcPoliticalTimeZone
	{
		#region Implementation of IMcPoliticalTimeZone

		public int TimeZoneID { get; set; }
		public string TimeZoneName { get; set; }
		public string TimeZoneAB { get; set; }
		public string CentralTime { get; set; }
		public int HourDifference { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }

		#endregion Implementation of IMcPoliticalTimeZone
	}

	#endregion McPoliticalTimeZone
}
