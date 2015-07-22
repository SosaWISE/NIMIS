using System;
using Nancy.ModelBinding;
using NXS.DataServices.Sales;
using NXS.DataServices.Sales.Models;

namespace WebModules.Sales
{
	public class ContactsModule : BaseModule
	{
		ContactsService Srv { get { return new ContactsService(User.GPEmployeeID); } }

		public ContactsModule()
			: base("/Sales/Contacts")
		{
			RequiresPermission((string)null, null);

			Post["/", true] = async (x, ct) =>
			{
				var inputItem = BindBody<ContactInput>();
				return await Srv.SaveContactAndInfoAsync(inputItem).ConfigureAwait(false);
			};

			Get["/InBounds", true] = async (x, ct) =>
			{
				var qry = this.Bind<BoundsQuery>();
				return await Srv.ContactsInBoundsAsync(qry.minlat, qry.minlng, qry.maxlat, qry.maxlng);
			};

			Get["/ByHour", true] = async (x, ct) =>
			{
				var qry = Request.Query;
				return await Srv.ContactsByHourAsync((int)qry.salesRepId, (int)qry.officeId, (DateTime)qry.startTimestamp, (DateTime)qry.endTimestamp);
			};
		}
	}
}
