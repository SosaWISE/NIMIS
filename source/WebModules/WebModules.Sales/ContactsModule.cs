using Nancy.ModelBinding;
using NXS.Data.Sales;
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
	public class ContactsModule : BaseModule
	{
		ContactsService Srv { get { return new ContactsService(this.User.GPEmployeeID); } }

		public ContactsModule()
			//: base("/Contact", "/ng")
			: base("/Sales/Contacts")
		{
			this.RequiresPermission((string)null, null);

			////$http.post('ng/Contact/save_contact', {
			//Post["/save_contact", true] = async (x, ct) =>
			Post["/", true] = async (x, ct) =>
			{
				var inputItem = this.BindBody<ContactInput>();

				var tracking = new SalesTracking();
				//tracking.RepCompanyID = this.User.GPEmployeeID;
				tracking.Latitude = inputItem.SalesRepLatitude;
				tracking.Longitude = inputItem.SalesRepLongitude;

				var contact = new SalesContact();
				contact.ID = inputItem.ID;
				contact.Latitude = inputItem.Latitude;
				contact.Longitude = inputItem.Longitude;

				SalesContactNote note = null;
				if (!string.IsNullOrEmpty(inputItem.Address))
				{
					note = new SalesContactNote();
					//note.salesRepId = salesRepId;
					note.FirstName = inputItem.FirstName;
					note.LastName = inputItem.LastName;
					note.CategoryId = inputItem.CategoryId;
					note.SystemId = inputItem.SystemId;
					note.Note = inputItem.Note;
				}

				SalesContactAddress address = null;
				if (!string.IsNullOrEmpty(inputItem.Address) ||
					!string.IsNullOrEmpty(inputItem.City) ||
					!string.IsNullOrEmpty(inputItem.State) ||
					!string.IsNullOrEmpty(inputItem.Zip))
				{
					address = new SalesContactAddress();
					address.Address = inputItem.Address;
					//address.Address2 = inputItem.address2;
					address.State = inputItem.State;
					address.City = inputItem.City;
					address.Zip = inputItem.Zip;
				}

				SalesContactFollowup followup = null;
				if (inputItem.Followup.HasValue)
				{
					followup = new SalesContactFollowup();
					followup.FollowupOn = inputItem.Followup.Value;
				}

				return await Srv.SaveContactAndInfoAsync(tracking, contact, note, address, followup).ConfigureAwait(false);
			};

			//$http.get('ng/Contact/get_contacts_in_area/salesRepId=' + sr_id + '&officeId=' + o_id + '&minlat=' + sw.lat() + '&maxlat=' + ne.lat() + '&minlng=' + sw.lng() + '&maxlng=' + ne.lng()
			//Get["/get_contacts_in_area/{splat*}", true] = async (x, ct) =>
			//{
			//	string splat = x.splat;
			//	var ray = splat.Split(new char[] { '&' });
			//
			//	int salesRepId = 0, officeId = 0;
			//	double minlat = 0, minlng = 0, maxlat = 0, maxlng = 0;
			//	foreach (var txt in ray)
			//	{
			//		var kvp = txt.Split(new char[] { '=' });
			//		var v = kvp[1];
			//		if (string.Equals("null", v, StringComparison.OrdinalIgnoreCase))
			//			v = "0";
			//		switch (kvp[0])
			//		{
			//			case "salesRepId":
			//				salesRepId = int.Parse(v);
			//				break;
			//			case "officeId":
			//				officeId = int.Parse(v);
			//				break;
			//			case "minlat":
			//				minlat = double.Parse(v);
			//				break;
			//			case "minlng":
			//				minlng = double.Parse(v);
			//				break;
			//			case "maxlat":
			//				maxlat = double.Parse(v);
			//				break;
			//			case "maxlng":
			//				maxlng = double.Parse(v);
			//				break;
			//		}
			//	}
			//	return await Srv.ContactsInAreaAsync(salesRepId, officeId, minlat, minlng, maxlat, maxlng);
			//};
			Get["/InBounds", true] = async (x, ct) =>
			{
				var query = this.Bind<ContactsQuery>();
				return await Srv.ContactsInAreaAsync(query.minlat, query.minlng, query.maxlat, query.maxlng);
			};

			//$http.get('ng/Contact/get_contacts_by_hour/salesRepId=' + $scope.salesRepId + '&officeId=' + $scope.officeId + '&startTimestamp=' + this.startTimestamp.formatTimestamp() + '&endTimestamp=' + this.endTimestamp.formatTimestamp() )
			Get["/get_contacts_by_hour", true] = async (x, ct) =>
			{
				string splat = x.splat;
				var ray = splat.Split(new char[] { '&' });

				int salesRepId = 0, officeId = 0;
				DateTime startTimestamp = DateTime.Now, endTimestamp = DateTime.Now;
				foreach (var txt in ray)
				{
					var kvp = txt.Split(new char[] { '=' });
					var v = kvp[1];
					switch (kvp[0])
					{
						case "salesRepId":
							salesRepId = int.Parse(v);
							break;
						case "officeId":
							officeId = int.Parse(v);
							break;
						case "startTimestamp":
							startTimestamp = DateTime.Parse(v);
							break;
						case "endTimestamp":
							endTimestamp = DateTime.Parse(v);
							break;
					}
				}
				return await Srv.ContactsByHourAsync(salesRepId, officeId, startTimestamp, endTimestamp);
			};
		}
	}

	class ContactsQuery
	{
		public int salesRepId { get; set; }
		public int officeId { get; set; }
		public double minlat { get; set; }
		public double minlng { get; set; }
		public double maxlat { get; set; }
		public double maxlng { get; set; }
	}
}
