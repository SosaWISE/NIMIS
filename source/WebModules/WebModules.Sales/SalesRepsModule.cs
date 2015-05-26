using NXS.DataServices.Sales;
using NXS.Lib.Web;
using NXS.Lib.Web.Authentication;
using SOS.Lib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebModules.Sales
{
	public class SalesRepsModule : BaseModule
	{
		BlahService Srv { get { return new BlahService(/*this.User.GPEmployeeID*/); } }

		public SalesRepsModule()
			: base("/SalesRep", "/ng")
		{
			//$http.get('ng/SalesRep/get_sales_reps')
			Get["/get_sales_reps", true] = async (x, ct) =>
			{
				var officeid = 0;
				return new
				{
					results = await Srv.SalesRepsAsync(officeid).ConfigureAwait(false),
				};
			};
		}
	}
}
