using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOS.FunctionalServices.Models.HumanResource
{
	public class FnsTeamSearchInfo
	{
		public bool SearchLike { get; set; }
		public int? Top { get; set; }
		public int? TeamID { get; set; }
		public string TeamName { get; set; }
		public string OfficeName { get; set; }
		public int? SeasonID { get; set; }
		public string SeasonName { get; set; }
		public string City { get; set; }
		public string StateAB { get; set; }
		public int? RoleLocationID { get; set; }
	}
}
