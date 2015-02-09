using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOS.FunctionalServices.Models.HumanResource
{
	public class FnsUserSearchInfo
	{
		public bool SearchLike { get; set; }
		public int? Top { get; set; }

		public int? UserID { get; set; }
		public int? RecruitID { get; set; }
		public int? SeasonID { get; set; }
		public short? UserTypeID { get; set; }

		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string CompanyID { get; set; }
		public string SSN { get; set; }
		public string CellPhone { get; set; }
		public string HomePhone { get; set; }
		public string Email { get; set; }
		public string UserName { get; set; }
		public string UserEmployeeTypeId { get; set; }
	}
}
