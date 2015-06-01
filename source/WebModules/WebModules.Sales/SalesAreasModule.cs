using NXS.DataServices.Sales;
using NXS.DataServices.Sales.Models;
using NXS.Lib;
using NXS.Lib.Authentication;
using SOS.Lib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebModules.Sales
{
	public class SalesAreasModule : BaseModule
	{
		SalesAreaService Srv { get { return new SalesAreaService(this.User.GPEmployeeID); } }

		public SalesAreasModule()
			: base("/Sales/Areas")
		{
			this.RequiresPermission((string)null, null);

			Post["/", true] = async (x, ct) =>
			{
				var inputItem = this.BindBody<AreaInput>();
				return await Srv.SaveSalesAreaAsync(inputItem);
			};

			Delete["/", true] = async (x, ct) =>
			{
				var inputItem = this.BindBody<AreaInput>();
				return await Srv.DeleteSalesAreaAsync(inputItem.AreaId);
			};

			Get["/InBounds", true] = async (x, ct) =>
			{
				var qry = this.Request.Query;
				return new Result<object>(
					value: await Srv.SalesAreasAsync((int)qry.salesRepId, (int)qry.officeId, (decimal)qry.minlat, (decimal)qry.minlng, (decimal)qry.maxlat, (decimal)qry.maxlng)
				);
			};
		}
	}
}
