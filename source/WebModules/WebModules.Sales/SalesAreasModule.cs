using NXS.DataServices.Sales;
using NXS.DataServices.Sales.Models;
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
	public class SalesAreasModule : BaseModule
	{
		SalesAreaService Srv { get { return new SalesAreaService(this.User.GPEmployeeID); } }

		public SalesAreasModule()
			//: base("/SalesArea", "/ng")
			: base("/Sales/Areas")
		{
			this.RequiresPermission((string)null, null);

			//$http.get('ng/SalesArea/get_sales_areas/salesRepId=' + sr_id + '&officeId=' + o_id + '&minlat=' + sw.lat() + '&maxlat=' + ne.lat() + '&minlng=' + sw.lng() + '&maxlng=' + ne.lng())
			Get["/get_sales_areas/{splat*}", true] = async (x, ct) =>
			{
				string splat = x.splat;
				var ray = splat.Split(new char[] { '&' });

				int salesRepId = 0, officeId = 0;
				double minlat = 0, minlng = 0, maxlat = 0, maxlng = 0;
				foreach (var txt in ray)
				{
					var kvp = txt.Split(new char[] { '=' });
					var v = kvp[1];
					switch (kvp[0])
					{
						case "sr_id":
							salesRepId = int.Parse(v);
							break;
						case "o_id":
							officeId = int.Parse(v);
							break;
						case "minlat":
							minlat = double.Parse(v);
							break;
						case "minlng":
							minlng = double.Parse(v);
							break;
						case "maxlat":
							maxlat = double.Parse(v);
							break;
						case "maxlng":
							maxlng = double.Parse(v);
							break;
					}
				}
				return new
				{
					results = await Srv.SalesAreasAsync(salesRepId, officeId, minlat, minlng, maxlat, maxlng),
				};
			};

			//$http.post('ng/SalesArea/save_area', {
			//Post["/save_area", true] = async (x, ct) =>
			Post["/", true] = async (x, ct) =>
			{
				var inputItem = this.BindBody<AreaInput>();
				return await Srv.SaveSalesAreaAsync(inputItem);
				//return new
				//{
				//	results = item.id,
				//};
			};

			//$http.post("ng/SalesArea/delete_sales_area", {
			//Post["/delete_sales_area", true] = async (x, ct) =>
			Delete["/", true] = async (x, ct) =>
			{
				var inputItem = this.BindBody<AreaInput>();
				return await Srv.DeleteSalesAreaAsync(inputItem.AreaId);
			};
		}
	}
}
