using Nancy.ModelBinding;
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
	public class ContactsModule : BaseModule
	{
		ContactsService Srv { get { return new ContactsService(this.User.GPEmployeeID); } }

		public ContactsModule()
			: base("/Sales/Contacts")
		{
			this.RequiresPermission((string)null, null);

			Post["/", true] = async (x, ct) =>
			{
				var inputItem = this.BindBody<ContactInput>();
				return await Srv.SaveContactAndInfoAsync(inputItem).ConfigureAwait(false);
			};

			Get["/InBounds", true] = async (x, ct) =>
			{
				var qry = this.Request.Query;
				return await Srv.ContactsInAreaAsync((decimal)qry.minlat, (decimal)qry.minlng, (decimal)qry.maxlat, (decimal)qry.maxlng);
			};

			Get["/ByHour", true] = async (x, ct) =>
			{
				var qry = this.Request.Query;
				return await Srv.ContactsByHourAsync((int)qry.salesRepId, (int)qry.officeId, (DateTime)qry.startTimestamp, (DateTime)qry.endTimestamp);
			};
		}
	}
}
