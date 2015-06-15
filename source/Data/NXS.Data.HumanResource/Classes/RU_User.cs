using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NXS.Data.HumanResource
{
	public partial class RU_User
	{
		[IgnorePropertyAttribute(true)]
		public List<RU_Recruit> Recruits { get; set; }
	}
}
