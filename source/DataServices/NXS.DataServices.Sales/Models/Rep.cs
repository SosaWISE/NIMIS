using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NXS.DataServices.Sales.Models
{
	public class Rep
	{
		public int id { get; set; }
		public string GPID { get; set; }
		public string firstName { get; set; }
		public string lastName { get; set; }
		public string officeId { get; set; }
		public string email { get; set; }
	}
}
