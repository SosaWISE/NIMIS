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
	public class OfficesModule : BaseModule
	{
		BlahService Srv { get { return new BlahService(/*this.User.GPEmployeeID*/); } }

		public OfficesModule()
			: base("/Office", "/ng")
		{
			//$http.get('ng/Office/get_offices')
			Get["/get_offices", true] = async (x, ct) =>
			{
				return new
				{
					results = await Srv.SalesOfficesAsync().ConfigureAwait(false),
				};
			};
		}
	}
}
