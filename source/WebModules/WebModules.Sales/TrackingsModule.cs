using NXS.Data.Sales;
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
	public class TrackingsModule : BaseModule
	{
		BlahService Srv { get { return new BlahService(/*this.User.GPEmployeeID*/); } }

		public TrackingsModule()
			: base("/Tracking", "/ng")
		{
			//$http.post('ng/Tracking/track_location', {salesRepId:1, latitude:position.coords.latitude, longitude:position.coords.longitude});
			Post["/track_location", true] = async (x, ct) =>
			{
				var inputItem = this.BindBody<SalesTracking>();
				return await Srv.TrackLocationAsync(inputItem).ConfigureAwait(false);
			};
		}
	}
}
