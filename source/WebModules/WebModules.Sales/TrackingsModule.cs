using NXS.Data.Sales;
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
	public class TrackingsModule : BaseModule
	{
		BlahService Srv { get { return new BlahService(this.User.GPEmployeeID); } }

		public TrackingsModule()
			: base("/Sales/Tracking")
		{
			this.RequiresPermission((string)null, null);

			Post["/", true] = async (x, ct) =>
			{
				var inputItem = this.BindBody<TrackingInput>();
				await Srv.TrackLocationAsync(inputItem).ConfigureAwait(false);
				return new Result<bool>(value: true);
			};
		}
	}
}
