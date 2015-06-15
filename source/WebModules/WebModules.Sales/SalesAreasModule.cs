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
using Nancy.ModelBinding;

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
				var inputItem = this.BindBody<SlArea>();
				return await Srv.SaveSalesAreaAsync(inputItem);
			};
			Post["/{id:int}", true] = async (x, ct) =>
			{
				var inputItem = this.BindBody<SlArea>();
				inputItem.ID = (int)x.id;
				return (await Srv.SaveSalesAreaAsync(inputItem).ConfigureAwait(false));
			};

			Delete["/{id:int}", true] = async (x, ct) =>
			{
				return await Srv.DeleteSalesAreaAsync((int)x.id);
			};

			Get["/InBounds", true] = async (x, ct) =>
			{
				var qry = this.Bind<BoundsQuery>();
				return await Srv.SalesAreasInBoundsAsync(qry.repCompanyID, qry.teamId, qry.minlat, qry.minlng, qry.maxlat, qry.maxlng);
			};
		}
	}
	public class BoundsQuery
	{
		public string repCompanyID { get; set; }
		public int teamId { get; set; }
		public decimal minlat { get; set; }
		public decimal minlng { get; set; }
		public decimal maxlat { get; set; }
		public decimal maxlng { get; set; }
	}
}
